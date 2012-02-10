using System;
using System.Collections.Generic;
using Adf.Core.Data;
using Adf.Core.Query;
using Adf.Core.Resources;

namespace Adf.Data.Search
{
    public class TextProperty : FilterProperty
    {
        public TextProperty(string displayname, IColumn column)
        {
            Type = FilterType.Text;
//            DisplayPrefix = "txt";
            DisplayName = ResourceManager.GetString(displayname);
            Column = column;
            Operators = new[] { OperatorType.IsEqual, OperatorType.Like, OperatorType.IsNotEqualOrIsNull };
        }
    }

    public class BooleanProperty : FilterProperty
    {
        public BooleanProperty(string displayname, IColumn column)
        {
            Type = FilterType.Boolean;
//            DisplayPrefix = "cbx";
            DisplayName = ResourceManager.GetString(displayname);
            Column = column;
            Operators = new[] { OperatorType.IsEqual };
        }
    }

    public class DateTimeProperty : FilterProperty
    {
        public DateTimeProperty(string displayname, IColumn column)
        {
            Type = FilterType.DateTime;
//            DisplayPrefix = "txt";
            DisplayName = ResourceManager.GetString(displayname);
            Column = column;
            Operators = new[] { OperatorType.IsEqual, OperatorType.IsLarger, OperatorType.IsLargerOrEqual, OperatorType.IsSmaller, OperatorType.IsSmallerOrEqual };
        }
    }

    public class NumericProperty : FilterProperty
    {
        public NumericProperty(string displayname, IColumn column)
        {
            Type = FilterType.Number;
//            DisplayPrefix = "txt";
            DisplayName = ResourceManager.GetString(displayname);
            Column = column;
            Operators = new[] { OperatorType.IsEqual, OperatorType.IsLarger, OperatorType.IsLargerOrEqual, OperatorType.IsSmaller, OperatorType.IsSmallerOrEqual, OperatorType.IsNotEqualOrIsNull };
        }
    }

    public class ListProperty : FilterProperty
    {
        public ListProperty(string displayname, IColumn column, Func<Dictionary<string, string>> valueList)
        {
            Type = FilterType.List;
//            DisplayPrefix = "ddl";
            DisplayName = ResourceManager.GetString(displayname);
            Column = column;
            Operators = new[] { OperatorType.IsEqual, OperatorType.IsNotEqualOrIsNull };
            ValueList = valueList;
        }
    }
}
