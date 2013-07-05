using System;

namespace Adf.Core.Query
{
    /// <summary>
    /// Specifies the different types of SQL queries.
    /// Used to create query statement for execution.
    /// </summary>
    [Serializable]
    public class QueryType : Descriptor
    {
        /// <summary>
        /// Represent the SQL SELECT Statement. 
        /// The SELECT statement is used to select data from a database.
        /// </summary>
        public static readonly QueryType Select = new QueryType("Select");

        /// <summary>
        /// Represent the SQL SELECT DISTINCT Statement. 
        /// The SELECT DISTINCT statement is used to select distinct data from a database.
        /// </summary>
        public static readonly QueryType SelectDistinct = new QueryType("SelectDistinct");

        /// <summary>
        /// Represent the SQL DELETE Statement. 
        /// The DELETE statement is used to delete data from a database.
        /// </summary>
        public static readonly QueryType Delete = new QueryType("Delete");

        /// <summary>
        /// Represent the SQL INSERT Statement. 
        /// The INSERT statement is used to insert data into a database.
        /// </summary>
        public static readonly QueryType Insert = new QueryType("Insert");

        public static readonly QueryType Update = new QueryType("Update");

        /// <summary>
        /// Represent the SQL free Statement of total query expression.
        /// Use to create DDL or DML statement.
        /// </summary>
        public static readonly QueryType Free = new QueryType("Free");

        /// <summary>
        /// Represent the SQL COUNT Statement. 
        /// The COUNT statement is used to count the records from a database table.
        /// </summary>
        public static readonly QueryType Count = new QueryType("Count");

        /// <summary>
        /// Represent a stored procedure. 
        /// </summary>
        public static readonly QueryType StoredProcedure = new QueryType("StoredProcedure");

        public QueryType(string name) : base(name)
        {
        }
    }
}