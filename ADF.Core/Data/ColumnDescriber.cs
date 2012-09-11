using System;
using System.Globalization;

namespace Adf.Core.Data
{

    /// <summary>
    /// Represents the functionality and characteristics to describe a column of database table.
    /// Provides the formated column name and attributes of <see cref="ColumnDescriber"/>.
    /// </summary>
    [Serializable]
    public class ColumnDescriber : IColumn
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDescriber"/> class with the specified column's attribute, name and table of <see cref="ITable"/>.
        /// </summary>
        /// <param name="attribute">The attribute which contains the datatype, length etc.</param>
        /// <param name="table">The <see cref="ITable"/> which contains the table name.</param>
        /// <param name="column">Column name of a table.</param>
        public ColumnDescriber(string attribute, ITable table, string column = null, bool isIdentity = false, bool isAutoIncrement = false, bool isTimestamp = false)
        {
            _attribute = attribute;
            _columnName = column ?? attribute;
            _table = table;
            _isIdentity = isIdentity;
            _isAutoIncrement = isAutoIncrement;
            _isTimestamp = isTimestamp;
            _hashCode = _columnName.ToLower().GetHashCode();    // memory optimization
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDescriber"/> class with the specified column's attribute, name and table name.
        /// </summary>
        /// <param name="attribute">The attribute which contains the datatype, length etc.</param>
        /// <param name="table">The table name.</param>
        /// <param name="column">Column name of a table.</param>
        public ColumnDescriber(string attribute, string table, string column = null) : this(attribute, TableDescriber.New(table, DataSources.NoSource), column ?? attribute) { }

        #endregion

        private readonly ITable _table;

        /// <summary>
        /// Gets or sets the <see cref="ITable"/> object for the table name.
        /// </summary>
        /// <returns>The <see cref="ITable"/> object for the table name.</returns>
        public ITable Table
        {
            get { return _table; }
        }

        private readonly string _attribute;

        /// <summary>
        /// Gets or sets the attribute (datatype, length etc) of <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <value>The attribute (datatype, length etc) of <see cref="ColumnDescriber"/>.</value>
        public string Attribute
        {
            get { return _attribute; }
        }

        private readonly string _columnName;

        /// <summary>
        /// Gets or sets the column name of <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <value>The column name of <see cref="ColumnDescriber"/>.</value>
        public string ColumnName
        {
            get { return _columnName; }
        }

        private readonly bool _isIdentity;

        public bool IsIdentity
        {
            get { return _isIdentity; }
        }

        private readonly bool _isAutoIncrement;

        public bool IsAutoIncrement
        {
            get { return _isAutoIncrement; }
        }

        private readonly bool _isTimestamp;

        public bool IsTimestamp
        {
            get { return _isTimestamp; }
        }

        private readonly int _hashCode;

        #region FullName

//        /// <summary>
//        /// Gets the formated column name like <code>[TableName].[ColumnName]</code>.
//        /// </summary>
//        /// <returns>The formated column name like <code>[TableName].[ColumnName]</code>.</returns>
//        protected virtual string FullNameFormat
//        {
//            get { return "[{0}].[{1}]"; }
//        }

//        /// <summary>
//        /// Gets the formated column name by the values of composite formate string that is culture-independent (invariant).
//        /// </summary>
//        /// <returns>The formated column name by the values of composite formate string that is culture-independent (invariant).</returns>
//        public string FullName
//        {
//            get { return string.Format(CultureInfo.InvariantCulture, FullNameFormat, EscapeFunction(Table.Name), ColumnName); }
//        }
//
//        private static string EscapeFunction(string table)
//        {
//            var indexOf = table.IndexOf('(');                   // check if this is a function
//
//            return indexOf == -1 ? table : table.Substring(0, indexOf); // remove () part
//        }

        #endregion

        #region Alias

        private const string AliasFormat = "[{0}].[{1}] AS [{0}{1}]";

        /// <summary>
        /// Gets the alias of column in the format like 
        /// <code>....[TableName].[ColumnName] AS [TableNameColumnName]....</code>
        /// </summary>
        /// <returns>The alias of column in the format like [TableName].[ColumnName] AS [TableNameColumnName].</returns>
        public string Alias
        {
            get { return string.Format(CultureInfo.InvariantCulture, AliasFormat, Table.Name, ColumnName); }
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns the columns attribute.
        /// </summary>
        /// <returns>The columns attribute.</returns>
        public override string ToString()
        {
            return ColumnName;
        }

        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as ColumnDescriber;

            if (other == null) return false;

            return ColumnName.Equals(other.ColumnName, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}
