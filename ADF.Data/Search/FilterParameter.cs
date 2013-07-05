using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Data.Search
{
    public class FilterParameter : IFilterParameter
    {
        public FilterParameter()
        {
            Predicate = PredicateType.And;
        }

        #region Implementation of IFilterParameter

        public OperatorType Operator { get; set; }

        public FilterProperty Property { get; set; }

        public object Value { get; set; }

        public PredicateType Predicate { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Predicate, Property, Operator, Value);
        }
    }

//    public class JoinParameter : IJoinParameter
//    {
//        #region Implementation of IJoinParameter
//
//        public string NativeTable { get; private set; }
//
//        public string NativeColumn { get; private set; }
//
//        public string ForeignTable { get; private set; }
//
//        public string ForeignColumn { get; private set; }
//
//        #endregion
//
//        public JoinParameter(string foreignTable, string foreignColumn, string nativeTable, string nativeColumn)
//        {
//            ForeignTable = foreignTable;
//            ForeignColumn = foreignColumn;
//            NativeTable = nativeTable;
//            NativeColumn = nativeColumn;
//        }
//    }

//    public class SearchProperty
//    {
//        public string PropertyName { get; set; }
//
//        public ColumnDescriber ColumnDescriber { get; set; }
//
//        public JoinParameter JoinParameter { get; set; }
//
//        public SearchProperty(string propertyName, ColumnDescriber columnDescriber, JoinParameter joinParameter)
//        {
//            PropertyName = propertyName;
//            ColumnDescriber = columnDescriber;
//            JoinParameter = joinParameter;
//        }
//    }

}
