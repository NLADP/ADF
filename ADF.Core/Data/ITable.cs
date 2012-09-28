using System;

namespace Adf.Core.Data
{
    public interface ITable : IEquatable<ITable>
    {
        DataSources DataSource { get; }
        string Name { get; }
        string FullName { get; }
    }
}
