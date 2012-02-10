using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Base.Data;
using Adf.Base.Query;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Data.Search
{
    public static class SearchFilterExtensions
    {
        public static Q BuildQuery<Q>(this Q query, Type businessdescriber, IEnumerable<ISearchParameter> searchParameters) where Q : AdfQuery
        {
            foreach (var parameter in searchParameters)
            {
                var columnDescriber = BusinessDescriber.GetColumn(businessdescriber, parameter.Column);
                if (columnDescriber == null || columnDescriber.IsEmpty)
                {
                    throw new InvalidColumnException(businessdescriber.ToString(), parameter.Column);
                }

                if (parameter.Value == null) continue;

                var where = new Where
                {
                    Column = columnDescriber,
                    Operator = parameter.OperatorType,
                    Parameter = new Parameter(parameter.Value, parameter.ParameterType)
                };

                query.Wheres.Add(where);
            }

            return query;
        }

        public static Q BuildQuery<Q>(this Q query, IEnumerable<IFilterParameter> filterParameters, IEnumerable<JoinProperty> joins, bool encapsulateInBrackets) where Q : AdfQuery
        {
            bool isFirstWhereClause = true;

            foreach (var filterParameter in filterParameters)
            {
                var joinsForParameter = joins.GetJoinsFor(filterParameter, query.LeadTable());

                query.Wheres.Add(new Where
                {
                    Column = filterParameter.Property.Column,
                    Operator = filterParameter.Operator,
                    Parameter = new Parameter(filterParameter.Value),
                    Predicate = Descriptor.Parse<PredicateType>(filterParameter.Predicate.ToString())
                });

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

            if (filterParameters.Count() > 0 && encapsulateInBrackets)
                query.CloseBracket();

            return query;
        }

        private static IEnumerable<Join> GetJoinsFor(this IEnumerable<JoinProperty> joins, IFilterParameter filterParameter, string leadTable)
        {
            var list = new List<Join>();

            var table = filterParameter.Property.Column.Table.FullName;

            while (table != leadTable)
            {
                var join = joins.First(j => j.Join.Table.FullName == table);

                table = join.Source.Table.FullName;

                list.Insert(0, new Join { SourceColumn = join.Source, JoinColumn = join.Join, Type = JoinType.Left });
            }

            return list;
        }

    }
}
