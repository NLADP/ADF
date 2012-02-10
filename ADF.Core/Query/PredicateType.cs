using System;

namespace Adf.Core.Query
{
    /// <summary>
    /// Parameter can be either a single value or a subquery.
    /// Used to specify it.
    /// </summary>
    [Serializable]
    public class PredicateType : Descriptor
    {
        /// <summary>
        /// And operand.
        /// </summary>
        public static readonly PredicateType And = new PredicateType("AND");

        /// <summary>
        /// Or operand.
        /// </summary>
        public static readonly PredicateType Or = new PredicateType("OR");

        public PredicateType(string name) : base(name)
        {
        }
    }
}