using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Data.Search
{
    public class FilterProperty
    {
        public FilterType Type { get; set; }
        public string DisplayName { get; set; }
        public IColumn Column { get; set; }
        public IEnumerable<OperatorType> Operators { get; set; }
        public Func<Dictionary<string, string>> ValueList { get; set; }

        public override string ToString()
        {
            return Column.ColumnName;
        }

        public FilterProperty()
        {
            Operators = Enumerable.Empty<OperatorType>();
        }
    }

    public class JoinProperty
    {
        public IColumn Join { get; protected set;  }
        public IColumn Source { get; protected set;  }

        public JoinProperty(IColumn source, IColumn join)
        {
            Join = join;
            Source = source;
        }
    }
}