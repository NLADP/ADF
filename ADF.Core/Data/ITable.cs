using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Data
{
    public interface ITable
    {
        DataSources DataSource { get; }
        string Name { get; }
        string FullName { get; }
    }
}
