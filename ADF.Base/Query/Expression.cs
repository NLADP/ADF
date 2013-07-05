using System;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    [Serializable]
    public class Expression : IExpression
    {
        public IColumn Column { get; set; }
        public ExpressionType Type { get; set; }
        public string Alias { get; set; }
    }
}
