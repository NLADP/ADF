using System;

namespace Adf.Core.Query
{
    /// <summary>
    /// Parameter can be either a single value or a subquery.
    /// Used to specify it.
    /// </summary>
    [Serializable]
    public class JoinType : Descriptor
    {
        public static readonly JoinType Inner = new JoinType("INNER");
        public static readonly JoinType Left = new JoinType("LEFT");
        public static readonly JoinType Right = new JoinType("RIGHT");
        public static readonly JoinType Full = new JoinType("FULL");

        public JoinType(string name) : base(name)
        {
        }
    }
}