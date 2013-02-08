using System;

namespace Adf.Core.Data
{
    /// <summary>
    /// Represents the configured connection to a database or other source of data.
    /// Provides the initialization of connection.
    /// </summary>
    [Serializable]
    public class DataSources : Descriptor
    {

        /// <summary>
        /// Gets or sets the connection of <see cref="DataSources"/> class when no data source is there.
        /// </summary>
        public static readonly DataSources NoSource = new DataSources("NoSource");

        public DataSources(string name) : base(name)
        {
        }
    }
}
