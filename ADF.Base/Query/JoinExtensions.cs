using System.Linq;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static class JoinExtensions
    {
        public static Q Join<Q>(this Q query, IColumn joincolumn) where Q : IAdfQuery
        {
            query.Joins.Add(new Join {Type = JoinType.Inner, JoinColumn = joincolumn});

            return query;
        }

        public static Q LeftJoin<Q>(this Q query, IColumn joincolumn) where Q : IAdfQuery
        {
            query.Joins.Add(new Join { Type = JoinType.Left, JoinColumn = joincolumn });

            return query;
        }

        public static Q RightJoin<Q>(this Q query, IColumn joincolumn) where Q : IAdfQuery
        {
            query.Joins.Add(new Join { Type = JoinType.Right, JoinColumn = joincolumn });

            return query;
        }

        public static Q FullJoin<Q>(this Q query, IColumn joincolumn) where Q : IAdfQuery
        {
            query.Joins.Add(new Join { Type = JoinType.Full, JoinColumn = joincolumn });

            return query;
        }

        public static Q On<Q>(this Q query, IColumn sourcecolumn) where Q : IAdfQuery
        {
            IJoin j = query.Joins.Last();

            j.SourceColumn = sourcecolumn;
            return query;
        }
    }
}
