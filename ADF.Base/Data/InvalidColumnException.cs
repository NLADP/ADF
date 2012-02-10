using System;

namespace Adf.Base.Data
{
    /// <summary>
    /// Provides the exception when column does not exist on investigated business describer.
    /// </summary>
    [Serializable]
    public class InvalidColumnException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnException"/> class with the specified describer and column name.
        /// </summary>
        /// <param name="describer">The business describer which will investigated.</param>
        /// <param name="column">The column name which will search for.</param>
        public InvalidColumnException(string describer, string column) : base(string.Format("Column {0} does not exist on investigated business describer {1}.", column, describer)) { }
    }
}
 