using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Adf.Base.Query;
using Adf.Core.Data;
using Adf.Core.Objects;
using Adf.Core.Query;
using Adf.Data.InternalState;
using DataException = Adf.Core.Data.DataException;

namespace Adf.Data
{
    /// <summary>
    /// Repesents a service handler that is dependent to the use of <see cref="RowState"/> for database tranaction.
    /// Provides the actual database handling for Select, Delete, Insert and Update data.
    /// </summary>
    public class DatabaseQueryHandler : IAdfQueryHandler
    {
        public DataSources DataSource { get; set; }
        private IDataProvider _provider;

        protected IDataProvider Provider
        {
            get
            {
                IDataProvider value;

                lock (this)
                {
                    value = _provider ?? (_provider = ObjectFactory.BuildUp<IDataProvider>(DataSource.ToString()));
                }

                return value;
            }
        }

        #region Run a Query

        protected virtual bool IsResultEmpty(DataSet set)
        {
            if (set == null) throw new ArgumentNullException("set", @"Result set is unavailable.");

            return set.Tables.Count == 0 || set.Tables[0].Rows.Count == 0;
        }

        /// <summary>
        /// Runs the specified <see cref="IAdfQuery"/> and get the executed result set into a <see cref="System.Data.DataSet"/>.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The <see cref="System.Data.DataSet"/> that contains the executed results. If the specified <see cref="IAdfQuery"/> is null then return a blank <see cref="System.Data.DataSet"/>.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        private DataSet RunQuery(IAdfQuery query)
        {
            var result = new DataSet { EnforceConstraints = false };
            if (query == null) return result;

            IDbConnection connection = Provider.GetConnection(DataSource);
            IDbDataAdapter da = Provider.GetAdapter();

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                da.TableMappings.Add("Table", query.LeadTable());

                IDbCommand command = Provider.GetCommand(DataSource, connection, query);
                da.SelectCommand = command;

                IDbTransaction transaction = Provider.GetTransaction(DataSource);
                if (transaction != null) da.SelectCommand.Transaction = transaction;

                result.Load(command.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly), LoadOption.OverwriteChanges, query.LeadTable());

                da.Fill(result);
            }
            catch(Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
            }
            finally
            {
                if (da.SelectCommand == null || da.SelectCommand.Transaction == null)
                    if (connection.State == ConnectionState.Open) connection.Close();
            }

            return result;
        }

        #endregion

        #region Read

        /// <summary>
        /// Executes a specified <see cref="IAdfQuery"/> statement and returns the affected row of data.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The row of data into <see cref="IInternalState"/> object, if the <see cref="System.Data.DataSet"/> is not empty; 
        /// otherwise, null value object of <see cref="NullInternalState"/>.</returns>
        public IInternalState Run(IAdfQuery query)
        {
            DataSet result = RunQuery(query);

            return IsResultEmpty(result) ? NullInternalState.Null : RowState.New(result.Tables[0].Rows.Cast<DataRow>().Single());
        }

        /// <summary>
        /// Executes the specified query and return the result, 
        /// where each row in the result is stored in an instance of <see cref="IInternalState"/>.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>Query results, where each row is in a specific <see cref="IInternalState"/> object, if found the <see cref="System.Data.DataSet"/> is not empty; 
        /// otherwise, a NullArray of <see cref="NullInternalState"/>.</returns>
        public IEnumerable<IInternalState> RunAndSplit(IAdfQuery query)
        {
            DataSet result = RunQuery(query);

            if (IsResultEmpty(result)) return NullInternalState.NullList;
            
            return RowState.Fill(result.Tables[0]);
        }


        /// <summary>
        /// Executes the <see cref="IAdfQuery"/>, and returns the first column of the first row in the
        /// resultset returned by the <see cref="IAdfQuery"/>. Extra columns or rows are ignored.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>object that is the first column of the first row in the resultset. 
        /// This method also returns a new instance of <see cref="System.Object"/> class if <see cref="IAdfQuery"/> is null.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        public object RunScalar(IAdfQuery query)
        {
            var result = new object();
            if (query == null) return result;

            IDbConnection connection = Provider.GetConnection(DataSource);
            IDbDataAdapter da = Provider.GetAdapter();

            try
            {
                da.TableMappings.Add("Table", query.LeadTable());

                if (connection.State == ConnectionState.Closed) connection.Open();

                da.SelectCommand = Provider.GetCommand(DataSource, connection, query);

                IDbTransaction transaction = Provider.GetTransaction(DataSource);
                if (transaction != null) da.SelectCommand.Transaction = transaction;

                result = da.SelectCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
            }

            finally
            {
                if (da.SelectCommand.Transaction == null) connection.Close();
            }

            return result;
        }

        #endregion

        #region Insert

        /// <summary>
        /// Create a new data row of <see cref="IInternalState"/> to insert new data into database.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The query result into <see cref="IInternalState"/> object, if found the <see cref="IAdfQuery"/>; 
        /// otherwise, null value object of <see cref="NullInternalState"/>.</returns>
        public IInternalState New(IAdfQuery query)
        {
            if (query == null) return NullInternalState.Null;

            var result = new DataSet { EnforceConstraints = false };

            IDbConnection connection = Provider.GetConnection(DataSource);
            IDbDataAdapter da = Provider.GetAdapter();

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                da.TableMappings.Add("Table", query.LeadTable());

                IDbCommand command = Provider.GetCommand(DataSource, connection, query);
                da.SelectCommand = command;

                result.Load(command.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly), LoadOption.OverwriteChanges, query.LeadTable());
                result.Tables[0].Rows.Add(result.Tables[0].NewRow());
            }
            catch (Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return IsResultEmpty(result) ? NullInternalState.Null : RowState.New(result.Tables[0].Rows[0]);
        }

        public int Delete(IAdfQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            using (var connection = Provider.GetConnection(DataSource))
            {
                using (var command = Provider.GetCommand(DataSource, connection, query))
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();

                        return command.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        Provider.HandleException(exception, DataSource, query);
                        return 0;
                    }
                }
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Save the specified data of <see cref="IInternalState"/> into database.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <param name="data">The data of <see cref="IInternalState"/> that needs to be saved.</param>
        /// <returns>True if data is successfully saved; otherwise, false. 
        /// This method also returns false if <see cref="IAdfQuery"/> or <see cref="IInternalState"/> is null.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        /// <exception cref="System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected.</exception>
        public bool Save(IAdfQuery query, IInternalState data)
        {
            var result = false;

            if (query == null || data == null) return false;
            if (!data.IsAltered) return true;

            var rowState = data as RowState;
            if (rowState == null) return false;

            var row = rowState.BuildUpDataRow();
            if (row == null) return false;

            IDbConnection connection = Provider.GetConnection(DataSource);
            IDbDataAdapter da = Provider.SetUpAdapter(DataSource, connection, query);

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                IDbTransaction transaction = Provider.GetTransaction(DataSource);
                if (transaction != null) da.SelectCommand.Transaction = transaction;

                var autoincrement = row.Table.Columns.Cast<DataColumn>().FirstOrDefault(column => column.AutoIncrement);
                if (autoincrement != null)
                {
                    da.InsertCommand.CommandText += "; if (IsNumeric(SCOPE_IDENTITY()) = 1) select SCOPE_IDENTITY() as " + autoincrement.ColumnName;
                }

                var count = Provider.Update(da, row);

                // The count should never be more than 1, since we request to update only 1 record. If the count is more than 1,
                // it means that the Provider updates the DataSet instead of the DataRow.
                if (count != 1) throw new DataException(string.Format("Saving {0} ({1}) changed {2} rows", query.LeadTable(), rowState.ID, count));

                rowState.AcceptChanges();

                result = true;
            }
            catch (Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
            }
            finally
            {
                if (da.SelectCommand.Transaction == null) connection.Close();
            }

            return result;
        }

        #endregion

    }
}