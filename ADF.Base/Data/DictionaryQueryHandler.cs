using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Adf.Base.Query;
using Adf.Core.Data;
using Adf.Core.Objects;
using Adf.Core.Query;

namespace Adf.Base.Data
{
    /// <summary>
    /// Repesents a service handler that is dependent to the use of <see cref="DictionaryState"/> for database tranaction.
    /// Provides the actual database handling for Select, Delete, Insert and Update data.
    /// Uses a timestamp to check for concurrency. 
    /// Warning: @@DBTS is used to get the new timestamp after insert or update. Be aware that this works only in the current database, 
    /// so a view to a diffentent database is not supported!
    /// </summary>
    public class DictionaryQueryHandler : IAdfQueryHandler
    {
        private const string Timestamp = "Timestamp";

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

        /// <summary>
        /// Runs the specified <see cref="IAdfQuery"/>.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The states that contains the executed results. If the specified <see cref="IAdfQuery"/> is null then return an empty list.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        private IEnumerable<IInternalState> RunQuery(IAdfQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            IDbConnection connection = Provider.GetConnection(DataSource);
            IDbDataAdapter da = Provider.GetAdapter();

            var result = new List<IInternalState>();

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                da.TableMappings.Add("Table", query.LeadTable());

                var command = (SqlCommand) Provider.GetCommand(DataSource, connection, query);
                da.SelectCommand = command;

                // try get cached schema
                var describers = GetSchema(query);

                SqlDataReader reader;
                if (describers == null)
                {
                    reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                    describers = GetSchema(query, reader);
                }
                else
                {
                    reader = command.ExecuteReader();
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(CreateState(describers, reader));
                    }
                }
            }
            catch(Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
            }
            finally
            {
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
        public IInternalState Run(IAdfQuery query)
        {
            return RunQuery(query).SingleOrDefault() ?? NullInternalState.Null;
        }

        private readonly Dictionary<DataSources, Dictionary<ITable, Dictionary<string, ColumnDescriber>>> _cachedDescribers = new Dictionary<DataSources, Dictionary<ITable, Dictionary<string, ColumnDescriber>>>();
        private readonly object _lock = new object();

        private Dictionary<string, ColumnDescriber> GetSchema(IAdfQuery query, SqlDataReader reader = null)
        {
            Dictionary<string, ColumnDescriber> describers;

            var table = query.Tables[0];

            if (query.Selects.Count == 0 && query.Tables.Count == 1 && query.QueryType != QueryType.StoredProcedure)
            {
                Dictionary<ITable, Dictionary<string, ColumnDescriber>> datasource;
                if (_cachedDescribers.TryGetValue(table.DataSource, out datasource))
                {
                    if (datasource.TryGetValue(table, out describers))
                    {
                        return describers;
                    }
                }
                else
                {
                    lock(_lock) _cachedDescribers.Add(table.DataSource, new Dictionary<ITable, Dictionary<string, ColumnDescriber>>());
                }
            }

            if (reader == null) return null;

            describers = new Dictionary<string, ColumnDescriber>();

            var schema = reader.GetSchemaTable();

            if (schema == null) return describers; //throw new InvalidOperationException("could not load schema");

            for (int i = 0; i < reader.VisibleFieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var column = new ColumnDescriber(columnName, table,
                                                 isIdentity: (bool) schema.Rows[i]["IsKey"],
                                                 isAutoIncrement: (bool) schema.Rows[i]["IsAutoIncrement"],
                                                 isTimestamp: (bool) schema.Rows[i]["IsRowVersion"]);

                describers.Add(columnName, column);
            }

            if (query.Selects.Count == 0 && query.Tables.Count == 1)
            {
                lock (_lock) _cachedDescribers[table.DataSource][table] = describers;
            }
            return describers;
        }

        private IInternalState CreateState(Dictionary<string, ColumnDescriber> describers, SqlDataReader reader = null)
        {
            var state = new DictionaryState { IsNew = true };

            if (reader == null)     // New
            {
                foreach (var describer in describers)
                {
                    state.Add(describer.Value, null);
                }

                return state;
            }
            
            for (int i = 0; i < reader.VisibleFieldCount; i++)
            {
                ColumnDescriber column;

                if (!describers.TryGetValue(reader.GetName(i), out column)) 
                    // dont throw exception. this could be due to an added column to the database while the application is running 
                    continue; // throw new InvalidOperationException("column not found: " + reader.GetName(i));

                if (reader.HasRows)
                {
                    var value = reader.GetValue(i);
                    if (value == DBNull.Value) value = null;

                    state[column] = value;

                    state.IsNew = false;
                }
                else
                {
                    state[column] = null;   // just add column info
                }
            }
            return state;
        }

        /// <summary>
        /// Executes the specified query and return the result, 
        /// where each row in the result is stored in an instance of <see cref="IInternalState"/>.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        public IEnumerable<IInternalState> RunAndSplit(IAdfQuery query)
        {
            return RunQuery(query);
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
            if (query == null) throw new ArgumentNullException("query");

            IDbConnection connection = Provider.GetConnection(DataSource);

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                var command = Provider.GetCommand(DataSource, connection, query);

                return command.ExecuteScalar();
            }
            catch (Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Executes a query
        /// </summary>
        /// <param name="state"></param>
        /// <param name="query"></param>
        /// <returns>The new timestamp</returns>
        protected bool RunSave(DictionaryState state, IAdfQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            IDbConnection connection = Provider.GetConnection(DataSource);

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                var command = Provider.GetCommand(DataSource, connection, query);

                const string autoIncr = "autoincrement";
                var queryExt = string.Format(";SELECT @@DBTS as {0},  CAST(SCOPE_IDENTITY() as int) as {1}", Timestamp, autoIncr);

                command.CommandText += queryExt;

                var reader = (SqlDataReader) command.ExecuteReader();

                if (reader.RecordsAffected == 0)
                    throw new DBConcurrencyException("Concurrency error updating table " + query.LeadTable());

                reader.Read();

                if (state.IsNew)
                {
                    var autoIncrement = state.Keys.FirstOrDefault(col => col.IsAutoIncrement);

                    if (autoIncrement != null)
                    {
                        state[autoIncrement] = reader[autoIncr];
                    }
                }

                var timestamp = state.Keys.First(col => col.IsTimestamp);

                state[timestamp] = reader[Timestamp];

                state.IsNew = false;
                state.IsAltered = false;
            }
            catch (Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return true;
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
            if (query == null) throw new ArgumentNullException("query");

            var describers = GetSchema(query);

            if (describers != null) return CreateState(describers);

            IDbConnection connection = Provider.GetConnection(DataSource);

            IInternalState result = null;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                var command = (SqlCommand) Provider.GetCommand(DataSource, connection, query);

                var reader = command.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly);
                
                result = CreateState(GetSchema(query, reader), reader);
            }
            catch (Exception exception)
            {
                Provider.HandleException(exception, DataSource, query);
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }

            return result ?? NullInternalState.Null;
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
            if (query == null) throw new ArgumentNullException("query");
            if (data == null) throw new ArgumentNullException("data");

            if (!data.IsAltered) return true;

            var state = data as DictionaryState;
            if (state == null) throw new InvalidOperationException("State is not a " + typeof(DictionaryState));

            var table = query.Tables[0];

            var q = new AdfQuery()
                .From(table);

            q.QueryType = state.IsNew ? QueryType.Insert : QueryType.Update;

            foreach (var col in state)
            {
                if (col.Key.IsAutoIncrement || col.Key.IsTimestamp) continue;                  // auto increment fields are not saved
                if (col.Key.IsIdentity && !state.IsNew) continue;       // primary key is not saved when object is not new
                if (state.IsNew && col.Value == null) continue;         // dont specify null values when new to allow default values

                q.Selects.Add(new Expression { Column = col.Key, Type = ExpressionType.Column });
                q.Wheres.Add(new Where(col.Key) { Parameter = new Parameter(col.Value, ParameterType.QueryParameter) });
            }

            if (!state.IsNew)
            {
                var timestamp = state.Keys.FirstOrDefault(col => col.IsTimestamp);
                if (timestamp == null) throw new InvalidOperationException("Cannot save this state as it does not have a timestamp");

                q
                    .Where(timestamp).IsEqual(state[timestamp])
                    .Where(state.PrimaryKey).IsEqual(state[state.PrimaryKey]);
            }

            return RunSave(state, q);
        }

        #endregion

    }
}
