using System;
using Adf.Core.Data;

namespace Adf.Data.SmartReferences
{
    /// <summary>
    /// Represents the configured connection to database for the source of SmartReference.
    /// Provides the initialization of database connection.
    /// </summary>
    [Serializable]
    public class SmartReferenceSources : DataSources
    {
        /// <summary>
        /// Create a new instance of <see cref="DataSources"/> class with the specified database connection name.
        /// </summary>
        public static readonly SmartReferenceSources SmartReference = new SmartReferenceSources("SmartReference");

        public SmartReferenceSources(string name) : base(name)
        {
        }
    }
}
