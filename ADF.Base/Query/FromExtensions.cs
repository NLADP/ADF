using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static class FromExtensions
    {
        public static Q From<Q>(this Q query, ITable table) where Q : IAdfQuery
        {
            query.Tables.Add(table);

            return query;
        }

        public static Q From<Q>(this Q query, params ITable[] tables) where Q : IAdfQuery
        {
            query.Tables.AddRange(tables);

            return query;
        }

        public static string LeadTable(this IAdfQuery query)
        {
            return query.Tables.Count == 0 ? string.Empty : query.Tables[0].FullName;
        }
    }
}
