﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Adf.Base.Query;
using Adf.Core.Data;
using Adf.Core.Logging;
using Adf.Core.Query;
using Adf.Core.State;
using Adf.Core.Validation;
using DataException = Adf.Core.Data.DataException;

namespace Adf.Base.Data
{
    /// <summary>
    /// Represents database provider that is used for SqlServer database.
    /// Provides functionality for connecting to a SQL Server database, managing transaction and generating SQLCommand.
    /// </summary>
    public class SqlProvider : IDataProvider
    {
        /// <summary>
        /// Gets the Sql Server data source type of <see cref="DataSourceType"/>.
        /// </summary>
        /// <returns>The Sql Server data source type.</returns>
        public DataSourceType Type 
        {
            get { return DataSourceType.SqlServer; }
        }

        /// <summary>
        /// Provides information about SQL command generated by the connection of <see cref="System.Data.IDbConnection"/> and query of <see cref="IAdfQuery"/>.
        /// </summary>
        /// <param name="connection">The <see cref="System.Data.IDbConnection"/> represents an open connection to a data source.</param>
        /// <param name="datasource"></param>
        /// <param name="query">The <see cref="IAdfQuery"/> whose SQL command information is to be retrieved.</param>
        /// <returns>The requested information of SQL command object.</returns>
        public virtual IDbCommand GetCommand(DataSources datasource, IDbConnection connection, IAdfQuery query)
        {
            var cmd = query.QueryType == QueryType.StoredProcedure
                          ? new SqlCommand(query.LeadTable(), (SqlConnection) connection) {CommandType = CommandType.StoredProcedure}
                          : new SqlCommand(QueryManager.Parse(datasource, query), (SqlConnection) connection);

            if (query.TimeOut.HasValue) cmd.CommandTimeout = query.TimeOut.Value;
            
            var allWheres = GetWhereParameters(query);

            foreach (IWhere w in allWheres.Where(w => w.Parameter != null))
            {
                if ((w.Parameter.Type == ParameterType.Value || w.Parameter.Type == ParameterType.QueryParameter) && w.Parameter.Value != null)
                {
                    cmd.Parameters.Add(new SqlParameter(w.Parameter.Name, w.Parameter.Value));
                }

                if (w.Parameter.Type == ParameterType.ValueList && w.Parameter.Value != null)
                {
                    int i = 0;
                    foreach (var v in ((IEnumerable) w.Parameter.Value))
                    {
                        cmd.Parameters.AddWithValue(string.Format("{0}_v{1}", w.Parameter.Name, i++), v ?? DBNull.Value);
                    }
                }
            }

            return cmd;
        }

        /// <summary>
        /// Returns the Where clauses of the given <see cref="IAdfQuery"/> and the possible contained sub-queries. 
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> whose Where clause information is to be retrieved.</param>
        /// <returns></returns>
        private IEnumerable<IWhere> GetWhereParameters(IAdfQuery query)
        {
            var whereParameters = new List<IWhere>();

            foreach (IWhere w in query.Wheres )
            {
                if (w.Parameter.Type == ParameterType.Query)
                    whereParameters = new List<IWhere>(whereParameters.Concat(GetWhereParameters((IAdfQuery) w.Parameter.Value)));
                else
                    whereParameters.Add(w);
            }
            return whereParameters;
        }

        /// <summary>
        /// Specifies the connection from the connection list or 
        /// create a new connection if it is not yet defined.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> that indicates the connection.</param>
        /// <returns>The connection object to associate with the transaction.</returns>
        public virtual IDbConnection GetConnection(DataSources source)
        {
//            IDbConnection connection = StateManager.Personal[source, "Connection.SqlServer"] as IDbConnection;
            
            return CreateConnection(source);
        }

        /// <summary>
        /// Adds a connection to the connection list.
        /// </summary>
        /// <param name="dataSource">The data source that is used to indicate the connection.</param>
        /// <param name="connection">The connection to set.</param>
        protected static void SetConnection(DataSources dataSource, IDbConnection connection)
        {
            StateManager.Personal[dataSource, "Connection.SqlServer"] = connection;
        }

        /// <summary>
        /// Creates a new SQL Server database connection based on the connection string specified in the data source.
        /// </summary>
        /// <param name="source">The data source containing the connection string.</param>
        /// <returns>The new SQL Server database connection.</returns>
        protected static IDbConnection CreateConnection(DataSources source)
        {
            string connectionString = StateManager.Settings[source.ToString()] as string;

            IDbConnection connection = new SqlConnection(connectionString);

//            SetConnection(source, connection);

            return connection;
        }

        /// <summary>
        /// Create a new instance of the <see cref="System.Data.SqlClient.SqlDataAdapter"/> class.
        /// </summary>
        /// <returns>The new instance of the <see cref="System.Data.SqlClient.SqlDataAdapter"/> class.</returns>
        public virtual IDbDataAdapter GetAdapter()
        {
            return new SqlDataAdapter();
        }

        /// <summary>
        /// Provides information to setup the data adapter by the connection of <see cref="System.Data.IDbConnection"/>.
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="connection">The <see cref="System.Data.IDbConnection"/> represents an open connection to a data source.</param>
        /// <param name="query">The <see cref="IAdfQuery"/> whose SQL information is to be retrieved.</param>
        /// <returns>The requested <see cref="System.Data.IDbDataAdapter"/> information object.</returns>
        public virtual IDbDataAdapter SetUpAdapter(DataSources datasource, IDbConnection connection, IAdfQuery query)
        {
            var da = (SqlDataAdapter) GetAdapter();

            if (query != null)
            {
                da.TableMappings.Add("Table", query.LeadTable());

                var selectCommand = (SqlCommand) GetCommand(datasource, connection, query);
                
                da.SelectCommand = selectCommand;

                // create command builder for a new command, so we can customize the insert command
                var newda = new SqlDataAdapter(selectCommand);

                // Create and associate a commandbuilder which can generate insert and update queries for the DataAdapter. This is a necessary step!
                var commandBuilder = new SqlCommandBuilder(newda);

                da.InsertCommand = commandBuilder.GetInsertCommand();
                da.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;

                da.UpdateCommand = commandBuilder.GetUpdateCommand();
                da.DeleteCommand = commandBuilder.GetDeleteCommand();
            }
            return da;
        }

        /// <summary>
        /// Handles exceptions thrown when excequring queries.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="datasource"></param>
        /// <param name="query"></param>
        public virtual void HandleException(Exception exception, DataSources datasource, IAdfQuery query)
        {
            LogManager.Log(exception);

            if (exception is DBConcurrencyException)
            {
                ValidationManager.AddError("Adf.Data.UnderlyingDataChanged", query.LeadTable());
            }
            else if (exception is SqlException)
            {
                switch ((exception as SqlException).Number)
                {
                    case 547: 
                        ValidationManager.AddError("Adf.Data.ForeignKeyConstraintsViolated", query.LeadTable());
                        break;
                    case -2:
                        ValidationManager.AddError("Adf.Data.Timeout", query.LeadTable());
                        break;
                    default:
                        throw new DataException(exception.Message, exception);
                }
            }
            else
            {
                throw new DataException(exception.Message, exception);
            }
        }

        /// <summary>
        /// Updates the selected rows from the dataset.
        /// </summary>
        /// <param name="adapter">The dataadapter to use.</param>
        /// <param name="dataRows">The rows to update.</param>
        /// <returns></returns>
        public int Update(IDbDataAdapter adapter, params DataRow[] dataRows)
        {
            var sqlAdapter = adapter as SqlDataAdapter;
            
            if (sqlAdapter == null) throw new InvalidOperationException("Not a SqlDataAdapter");

            return sqlAdapter.Update(dataRows);
        }

        #region Transactions

        /// <summary>
        /// Get a transaction object from the transactions list. If none present a null value will be returned.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> used to get the data source name.</param>
        /// <returns>The <see cref="System.Data.IDbTransaction"/> object for the currently executing transaction.</returns>
        public virtual IDbTransaction GetTransaction(DataSources source)
        {
            return StateManager.Personal[source, "Transaction"] as IDbTransaction;
        }

        /// <summary>
        /// Add a transaction for a specific database to the transactions list.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> used to get the data source name.</param>
        /// <param name="transaction">The <see cref="System.Data.IDbTransaction"/> represents a transaction to be performed at a data source.</param>
        public static void SetTransaction(DataSources source, IDbTransaction transaction)
        {
            StateManager.Personal[source, "Transaction"] = transaction;
        }

        /// <summary>
        /// Reset all database transaction for the specified database.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> used to get the data source name.</param>
        public static void ResetTransaction(DataSources source)
        {
            StateManager.Personal[source, "Transaction"] = null;
        }

        /// <summary>
        /// Destroy any connection and reset all the transactions on the specified database.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> used to get the data source name.</param>
        public virtual void DestroyConnection(DataSources source)
        {
            ResetTransaction(source);
            IDbConnection connection = GetConnection(source);
            connection.Close();
            StateManager.Personal.Remove(source);
        }

        /// <summary>
        /// Provides information to start a transaction to be performed at a data source.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> used to get the data source name.</param>
        /// <returns>The <see cref="System.Data.IDbTransaction"/> object for the currently executing transaction.</returns>
        /// <exception cref="System.Exception">An error occurred while trying to start the transaction.</exception>
        public virtual IDbTransaction StartTransaction(DataSources source)
        {
            IDbConnection connection = GetConnection(source);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            IDbTransaction transaction = GetTransaction(source) ?? connection.BeginTransaction(IsolationLevel.Serializable);

            SetTransaction(source, transaction);

            return transaction;
        }

        /// <summary>
        /// Commit the transaction for the database specified.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> used to get the data source name.</param>
        /// <exception cref="System.Exception">An error occurred while trying to commit the transaction.</exception>
        /// <exception cref="System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken.</exception>
        public virtual void Commit(DataSources source)
        {
            IDbTransaction transaction = GetTransaction(source);

            if (transaction != null)
            {
                transaction.Commit();
                DestroyConnection(source);
            }
        }

        /// <summary>
        /// Rolls back a transaction from a pending state.
        /// </summary>
        /// <param name="source">The <see cref="DataSources"/> used to get the data source name.</param>
        /// <exception cref="System.Exception">An error occurred while trying to rollback the transaction.</exception>
        /// <exception cref="System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken.</exception>
        public virtual void Rollback(DataSources source)
        {
            IDbTransaction transaction = GetTransaction(source);

            if (transaction != null)
            {
                transaction.Rollback();

                DestroyConnection(source);
            }
        }

        #endregion
    }
}
