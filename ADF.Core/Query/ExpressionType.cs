using System;

namespace Adf.Core.Query
{
    [Serializable]
    public class ExpressionType : Descriptor
    {
        public static readonly ExpressionType Column = new ExpressionType("");
        public static readonly ExpressionType Table = new ExpressionType("TABLE");
        public static readonly ExpressionType Max = new ExpressionType("MAX");
        public static readonly ExpressionType Min = new ExpressionType("MIN");
        public static readonly ExpressionType Average = new ExpressionType("AVG");
        public static readonly ExpressionType Count = new ExpressionType("COUNT");
        public static readonly ExpressionType Sum = new ExpressionType("SUM");
        // date functions
        public static readonly ExpressionType Year = new ExpressionType("YEAR");
        public static readonly ExpressionType Month = new ExpressionType("MONTH");
        public static readonly ExpressionType Day = new ExpressionType("DAY");

        public ExpressionType(string name) : base(name)
        {
        }
    }
}
