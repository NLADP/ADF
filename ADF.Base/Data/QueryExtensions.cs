using System;
using System.Collections.Generic;
using Adf.Base.Query;
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
    }
}
