using System;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    [Serializable]
    public class OrderBy : IOrderBy
    {
        #region Implementation of IOrderBy

        public IColumn Column { get; set; }

        public SortOrder SortOrder { get; set; }

        #endregion
    }
}
