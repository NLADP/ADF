using System;
using System.Globalization;

namespace Adf.Core.Data
{
    /// <summary>
    /// Represents the functionality and characteristics to describe a database table.
    /// Provides the formated table name of <see cref="TableDescriber"/>.
    /// </summary>
//    [Serializable]
    public class TableDescriber : ITable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableDescriber"/> class with the specified table name and <see cref="DataSources"/> object.
        /// </summary>
        /// <param name="tableName">The name of a table.</param>
        /// <param name="datasource">Initialize the connection by <see cref="DataSources"/>.</param>
        public TableDescriber(string tableName, DataSources datasource)
        {
            _name = tableName;
            _dataSource = datasource;
        }

        private readonly string _name;

        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        /// <returns>The table name.</returns>
        public string Name
        {
            get { return _name; }
        }

        private readonly DataSources _dataSource = DataSources.NoSource;

        /// <summary>
        /// Gets or sets the <see cref="DataSources"/> to configured database connection.
        /// </summary>
        /// <returns>The configured object of <see cref="DataSources"/>.</returns>
        public DataSources DataSource
        {
            get { return _dataSource; }
        }

        /// <summary>
        /// Gets the formated table name like <code>[TableName]</code>.
        /// </summary>
        /// <returns>The formated table name like <code>[TableName]</code>.</returns>
        public virtual string FullNameFormat
        {
            get { return "[{0}]"; }
        }

        /// <summary>
        /// Gets the formated column name by the values of composite formate string that is culture-independent (invariant).
        /// </summary>
        /// <returns>The formated column name by the values of composite formate string that is culture-independent (invariant).</returns>
        public string FullName
        {
            get
            {
                // don't put [] around if it's a function
                return Name.IndexOf("(") > 0 ? Name : string.Format(CultureInfo.InvariantCulture, FullNameFormat, Name);
            }
        }

        private static TableDescriber _nullDesc = new TableDescriber(string.Empty, DataSources.NoSource);

        /// <summary>
        /// Gets or sets the empty instance of <see cref="TableDescriber"/> class with empty table name and empty datasource.
        /// </summary>
        /// <returns>The empty instance of <see cref="TableDescriber"/> class with empty table name and empty datasource.</returns>
        public static TableDescriber Null
        {
            get { return _nullDesc; }
            protected set { _nullDesc = value; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TableDescriber"/> class with the specified table name and <see cref="DataSources"/> object.
        /// </summary>
        /// <param name="tableName">The name of a table.</param>
        /// <param name="datasource">Initialize the connection by <see cref="DataSources"/>.</param>
        /// <returns>The new instance of the <see cref="TableDescriber"/> class with the specified table name and <see cref="DataSources"/> object</returns>
        public static TableDescriber New(string tableName, DataSources datasource)
        {
            return new TableDescriber(tableName, datasource);
        }

        public bool Equals(ITable other)
        {
            if (other == null) return false;

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(this, other)) return true;

            return (Name == other.Name);
        }

        /// <summary>
        /// Returns a table name.
        /// </summary>
        /// <returns>The table name.</returns>
        public override string ToString()
        {
            return FullName;
        }
    }
}
