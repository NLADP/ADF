using System.Collections.Generic;
using System.Linq;
using Adf.Base.Query;
using Adf.Core.Extensions;
using Adf.Core.Data;
using Adf.Core.Query;
using Adf.Core.Validation;

namespace Adf.Base.Data
{
    ///<summary>
    ///</summary>
    public static class QueryExtensions
    {
        public static IEnumerable<IInternalState> RunSplit<Q>(this Q query, DataSources dataSource) where Q : IAdfQuery
        {
            return QueryManager.RunSplit(dataSource, query);
        }

        public static IInternalState Run<Q>(this Q query, DataSources dataSource) where Q : IAdfQuery
        {
            return QueryManager.Run(dataSource, query);
        }

        public static T RunScalar<T>(this IAdfQuery query, DataSources dataSource)
        {
            object scalar = QueryManager.RunScalar(dataSource, query);

            return scalar == null ? default(T) : (T) scalar;
        }

        public static bool Save<Q>(this Q query, DataSources dataSource, IInternalState state) where Q : IAdfQuery
        {
            if (!QueryManager.Save(dataSource, query, state))
            {
                ValidationManager.AddError("Adf.Data.SaveError");
                return false;
            }
            return true;
        }

        public static int Remove<Q>(this Q query, DataSources dataSource) where Q : IAdfQuery
        {
            query.SetType(QueryType.Delete);

            return QueryManager.Delete(dataSource, query);
        }

        public static bool Remove<Q>(this Q query, DataSources dataSource, int expectedCount) where Q : IAdfQuery
        {
            var actualCount = query.Remove(dataSource);

            if (expectedCount != actualCount)
            {
                ValidationManager.AddError("Adf.Data.RemoveError", actualCount);
                return false;
            }

            return true;
        }

        public static IInternalState New<Q>(this Q query, DataSources dataSource) where Q : IAdfQuery
        {
            return QueryManager.New(dataSource, query);
        }

        public static void RenameFunctionParameters(this IAdfQuery query, int paramExtension)
        {
            for(int i = query.Tables.Count - 1; i >= 0; i--)
            {
                var table = query.Tables[i];
                var parameters = table.GetParameters().ToList();

                if(parameters.Count == 0)
                    continue;

                var newTableName = table.Name;
                
                query.Tables.Remove(query.Tables[i]);

                foreach (var paramName in parameters)
                {
                    newTableName = newTableName.Replace(paramName, paramName + paramExtension);
                    var where = query.Wheres.FirstOrDefault(w => (!w.Parameter.Name.IsNullOrEmpty() ? w.Parameter.Name : w.Column.ColumnName)
                                                                 == paramName.Substring(1)); // Compare without @ in front

                    if (where != null) where.Parameter.Name = (!where.Parameter.Name.IsNullOrEmpty()
                                                                       ? where.Parameter.Name
                                                                       : where.Column.ColumnName) + paramExtension;
                }

                query.Tables.Insert(i, new TableDescriber(newTableName, table.DataSource));
            }
        }

        public static void RenameJoinFunctionParameters(this IAdfQuery query, int paramExtension)
        {
            foreach (var join in query.Joins)
            {
                var parameters = join.JoinColumn.Table.GetParameters().ToList();

                if (parameters.Count == 0)
                    continue;

                var newTableName = join.JoinColumn.Table.Name;

                foreach(var paramName in parameters)
                {
                    newTableName = newTableName.Replace(paramName, paramName + paramExtension);
                    var where = query.Wheres.FirstOrDefault(w => (!w.Parameter.Name.IsNullOrEmpty() ? w.Parameter.Name : w.Column.ColumnName)
                                                                 == paramName.Substring(1)); // Compare without @ in front

                    if (where != null) where.Parameter.Name = (!where.Parameter.Name.IsNullOrEmpty()
                                                                        ? where.Parameter.Name
                                                                        : where.Column.ColumnName) + paramExtension;
                }

                join.JoinColumn = new ColumnDescriber(join.JoinColumn.Attribute,
                                                      new TableDescriber(newTableName, join.JoinColumn.Table.DataSource),
                                                      join.JoinColumn.ColumnName,
                                                      join.JoinColumn.IsIdentity,
                                                      join.JoinColumn.IsAutoIncrement,
                                                      join.JoinColumn.IsTimestamp);
            }
        }
    }
}
