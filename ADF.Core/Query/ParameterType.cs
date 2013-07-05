using System;

namespace Adf.Core.Query
{
    /// <summary>
    /// Parameter can be either a single value or a subquery.
    /// Used to specify it.
    /// </summary>
    [Serializable]
    public class ParameterType : Descriptor
    {
        /// <summary>
        /// Single value.
        /// </summary>
        public static readonly ParameterType Value = new ParameterType("Value");

        /// <summary>
        /// Value as subquery.
        /// </summary>
        public static readonly ParameterType Query = new ParameterType("Query");

        public static readonly ParameterType ValueList = new ParameterType("ValueList");

        ///<summary>
        /// Use to add a parameter without clause to the query
        ///</summary>
        public static readonly ParameterType QueryParameter = new ParameterType("QueryParameter");

        public static readonly ParameterType Column = new ParameterType("Column");

        public ParameterType(string name) : base(name)
        {
        }
    }
}