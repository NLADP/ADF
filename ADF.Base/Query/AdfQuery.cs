using System;
using System.Collections.Generic;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    [Serializable]
    public class AdfQuery : IAdfQuery
    {
        private QueryType _type = QueryType.Select;
        private readonly List<IExpression> _selects = new List<IExpression>();
        private readonly List<ITable> _tables = new List<ITable>();
        private readonly List<IJoin> _joins = new List<IJoin>();
        private readonly List<IWhere> _wheres =  new List<IWhere>();
        private readonly List<IOrderBy> _orderBys = new List<IOrderBy>();
        private readonly List<IExpression> _groupBys = new List<IExpression>();

        public AdfQuery()
        {
            Top = null;
            Distinct = null;
        }

        public int? Top { get; set; }
        public int? TimeOut { get; set; }
        public bool? Distinct { get; set; }

        public List<IExpression> Selects { get { return _selects; } }

        public QueryType QueryType { get { return _type; } set { _type = value; } }

        public List<ITable> Tables { get { return _tables; } }

        public List<IJoin> Joins { get { return _joins; } }

        public List<IWhere> Wheres { get { return _wheres; } }

        public List<IOrderBy> OrderBys { get { return _orderBys; } }

        public List<IExpression> GroupBys
        {
            get { return _groupBys; }
        }
    }
}
