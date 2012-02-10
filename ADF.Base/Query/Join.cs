using System;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    [Serializable]
    public class Join : IJoin
    {
        protected JoinType _type = JoinType.Inner;

        public JoinType Type {  get { return _type; } set { _type = value; } }
        public IColumn SourceColumn { get; set; }
        public IColumn JoinColumn { get; set; }
    }
}
