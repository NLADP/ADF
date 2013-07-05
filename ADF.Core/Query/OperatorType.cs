using System;

namespace Adf.Core.Query
{
    /// <summary>
    /// Specifies the different types of SQL queries.
    /// Used to create query statement for execution.
    /// </summary>
    [Serializable]
    public class OperatorType : Descriptor
    {
        public static readonly OperatorType IsEqual = new OperatorType("IsEqual", "=");
        public static readonly OperatorType IsNotEqual = new OperatorType("IsNotEqual", "<>");
        public static readonly OperatorType IsNotEqualOrIsNull = new OperatorType("IsNotEqualOrIsNull", "<>");
        public static readonly OperatorType IsLarger = new OperatorType("IsLarger", ">");
        public static readonly OperatorType IsSmaller = new OperatorType("IsSmaller", "<");
        public static readonly OperatorType IsLargerOrEqual = new OperatorType("IsLargerOrEqual", ">=");
        public static readonly OperatorType IsSmallerOrEqual = new OperatorType("IsSmallerOrEqual", "<=");
        public static readonly OperatorType Like = new OperatorType("Like", "LIKE");
        public static readonly OperatorType LikeLeft = new OperatorType("LikeLeft", "LIKE");
        public static readonly OperatorType LikeRight = new OperatorType("LikeRight", "LIKE");
        public static readonly OperatorType IsNull = new OperatorType("IsNull", "IS NULL");
        public static readonly OperatorType IsNotNull = new OperatorType("IsNotNull", "IS NOT NULL");
        public static readonly OperatorType In = new OperatorType("In", "IN");
        public static readonly OperatorType NotIn = new OperatorType("NotIn", "NOT IN");

        public OperatorType(string name, string value) : base(name)
        {
            _value = value;
        }

        private readonly string _value;
        public string Value
        {
            get { return _value; }
        }
    }
}