using System;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    [Serializable]
    public class Where : IWhere
    {
        protected PredicateType _predicate = PredicateType.And;
        protected OperatorType _operator = OperatorType.IsEqual;
        public int OpenBracket { get; set; }
        public int CloseBracket { get; set; }

        public IColumn Column { get; set; }
        public OperatorType Operator  { get { return _operator;  } set { _operator = value;  } }
        public Parameter Parameter  { get; set; }
        public PredicateType Predicate { get { return _predicate; } set { _predicate = value; } }

        // note: tostring just for debugging purposes
        public override string ToString()
        {
            return string.Format("{3} {0} {1} {2}", Column, Operator.Value, Parameter, Predicate);
        }
    }
}
