using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Base.Data;
using Adf.Base.Query;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Extensions;
using Adf.Core.Query;

namespace Adf.Data.Search
{
    public static class SearchFilterExtensions
    {
        public static Q BuildQuery<Q>(this Q query, Type businessdescriber, IEnumerable<ISearchParameter> searchParameters) where Q : AdfQuery
        {
            foreach (var parameter in searchParameters)
            {
                var column = BusinessDescriber.GetColumn(businessdescriber, parameter.Column);

                if (column == null) throw new InvalidColumnException(businessdescriber.ToString(), parameter.Column);

                if (parameter.Value == null) continue;

                var where = new Where
                {
                    Column = column,
                    Operator = parameter.OperatorType,
                    Parameter = new Parameter(parameter.Value, parameter.ParameterType) { Name = column.ColumnName },
                    Collation = parameter.Collation
                };

                query.Wheres.Add(where);
            }

            return query;
        }

        public static Q BuildQuery<Q>(this Q query, IEnumerable<IFilterParameter> filterParameters, IEnumerable<JoinProperty> joins, bool encapsulateInBrackets, Dictionary<string, IAdfQuery> subQueries = null) where Q : AdfQuery
        {
            bool isFirstWhereClause = true;
            var filterParamsList = filterParameters.ToList();
            var joinsList = joins.ToList();

            foreach (var filterParameter in filterParamsList)
            {
                var joinsForParameter = joinsList.GetJoinsFor(filterParameter, query.LeadTable());

                var where = new Where
                                {
                                    Column = filterParameter.Property.Column,
                                    Operator = filterParameter.Operator,
                                    Predicate = Descriptor.Parse<PredicateType>(filterParameter.Predicate.ToString()),
                                    Collation = filterParameter.Property != null ? filterParameter.Property.Collation : null
                                };

                // Handle sub queries
                if (filterParameter.Operator.IsIn(OperatorType.In, OperatorType.NotIn) && subQueries != null && subQueries.ContainsKey(filterParameter.ToString()))
                    where.Parameter = new Parameter(subQueries[filterParameter.ToString()], ParameterType.Query);
                else
                    where.Parameter = new Parameter(filterParameter.Value);

                query.Wheres.Add(where);

                if (encapsulateInBrackets && isFirstWhereClause)
                {
                    query.OpenBracket();
                    isFirstWhereClause = false;
                }

                // add joins for this filterparameter, except if the join is already added
                //                    query.Joins.AddRange(joins
                //                                             .GetJoinsFor(filterParameter, query.LeadTable())
                //                                             .Except(query.Joins, new Comparer<IJoin>(j => j.JoinColumn)));

                foreach (var join in joinsForParameter)
                {
                    if (!query.Joins.Exists(j => j.JoinColumn.Table.Name == join.JoinColumn.Table.Name))
                    {
                        query.Joins.Add(join);
                    }
                }
            }

            if (filterParamsList.Count > 0 && encapsulateInBrackets)
                query.CloseBracket();

            return query;
        }

        private static IEnumerable<Join> GetJoinsFor(this IList<JoinProperty> joins, IFilterParameter filterParameter, string leadTable)
        {
            var list = new List<Join>();

            var table = filterParameter.Property.Column.Table.FullName;

            while (table != leadTable)
            {
                var join = joins.First(j => j.Join.Table.FullName == table);

                table = join.Source.Table.FullName;

                list.Insert(0, new Join { SourceColumn = join.Source, JoinColumn = join.Join, Type = JoinType.Left });

                if (list.Count > joins.Count) throw new InvalidOperationException("Can't find your join, review your query"); // prevent deadlock in while loop
            }

            return list;
        }
    }
}
