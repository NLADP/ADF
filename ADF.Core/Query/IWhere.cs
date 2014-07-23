using Adf.Core.Data;

namespace Adf.Core.Query
{
    public interface IWhere
    {
        IExpression Column { get; set; }
        OperatorType Operator { get; set; }
        Parameter Parameter { get; set; }
        PredicateType Predicate { get; set; }
        int OpenBracket { get; set; }
        int CloseBracket { get; set; }
        CollationType Collation { get; set; }
    }
}
