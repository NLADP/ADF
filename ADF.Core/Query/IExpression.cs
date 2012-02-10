using Adf.Core.Data;

namespace Adf.Core.Query
{
    public interface IExpression
    {
        IColumn Column { get; }
        ExpressionType Type { get; }
        string Alias { get; }
    }
}
