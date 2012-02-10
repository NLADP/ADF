using Adf.Core.Objects;

namespace Adf.Core.Data
{
    /// <summary>
    /// Specifies the data _source type to <see cref="ObjectFactory"/> in order to get the correct handler name.
    /// </summary>
    public class DataSourceType : Descriptor
    {
        /// <summary>
        /// Represent a DB2 data _source.
        /// </summary>
        public static readonly DataSourceType Db2 = new DataSourceType("Db2");
        /// <summary>
        /// Represent a MySql data _source.
        /// </summary>
        public static readonly DataSourceType MySql = new DataSourceType("MySql");
        /// <summary>
        /// Represent a NHibernate data _source.
        /// It provides a framework for mapping an object-oriented domain model to a traditional relational database.
        /// </summary>
        public static readonly DataSourceType NHibernate = new DataSourceType("NHibernate");
        /// <summary>
        /// Represent a Odbc data _source.
        /// Provides an interface to access databases via SQL queries.
        /// </summary>
        public static readonly DataSourceType Odbc = new DataSourceType("Odbc");
        /// <summary>
        /// Represent an OleDB data _source. 
        /// It provides an application with uniform access to data stored in diverse formats, such as indexed-sequential files, personal databases, productivity tools and SQL-based DBMSs.
        /// </summary>
        public static readonly DataSourceType OleDB = new DataSourceType("OleDB");
        /// <summary>
        /// Represent an Oracle data _source.
        /// </summary>
        public static readonly DataSourceType Oracle = new DataSourceType("Oracle");
        /// <summary>
        /// Represent a data _source such as Web Service.
        /// </summary>
        public static readonly DataSourceType Service = new DataSourceType("Service");
        /// <summary>
        /// Represent a SharePoint data _source.
        /// </summary>
        public static readonly DataSourceType SharePoint = new DataSourceType("SharePoint");
        /// <summary>
        /// Represent a SqlServer data _source.
        /// </summary>
        public static readonly DataSourceType SqlServer = new DataSourceType("SqlServer");
        /// <summary>
        /// An Unknown data _source type in the <see cref="ObjectFactory"/>.
        /// </summary>
        public static readonly DataSourceType Unknown = new DataSourceType("Unknown");

        public DataSourceType(string name) : base(name)
        {
        }
    }
}
