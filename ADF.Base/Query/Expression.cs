using System;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    [Serializable]
    public class Expression : IExpression
    {
        public Expression() {}

        public Expression(IColumn column)
        {
            Column = column;
            Type = ExpressionType.Column;
        }

        public IColumn Column { get; set; }
        public ExpressionType Type { get; set; }
        public string Alias { get; set; }
    }
}
