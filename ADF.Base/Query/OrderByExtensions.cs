using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Query;
using System.Linq;

namespace Adf.Base.Query
{
    public static class OrderByExtensions
    {
        public static Q OrderBy<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            query.OrderBys.Add(new OrderBy { Column = column });

            return query;
        }

        /// <summary>
        /// This is the default, so you can safely ommit this.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Q Ascending<Q>(this Q query) where Q : IAdfQuery
        {
            query.OrderBys.Last().SortOrder = SortOrder.Ascending;

            return query;
        }

        public static Q Descending<Q>(this Q query) where Q : IAdfQuery
        {
            query.OrderBys.Last().SortOrder = SortOrder.Descending;

            return query;
        }
    }
}
