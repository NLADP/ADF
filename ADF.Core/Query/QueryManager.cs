using System;
using System.Collections.Generic;
using System.Diagnostics;
using Adf.Core.Data;
using Adf.Core.Logging;
using Adf.Core.Objects;
using Adf.Core.Extensions;

namespace Adf.Core.Query
{
    public static class QueryManager
    {
        #region Handling Query

        private static readonly Dictionary<DataSources, IAdfQueryHandler> handlers = new Dictionary<DataSources, IAdfQueryHandler>();

        private static readonly object _handlerLock = new object();

        internal static IAdfQueryHandler GetHandler(DataSources source)
        {
            lock (_handlerLock)
            {
                if (!handlers.ContainsKey(source))
                {
                    IAdfQueryHandler handler = ObjectFactory.BuildUp<IAdfQueryHandler>(source.Name);

                    handler.DataSource = source;
                    handlers.Add(source, handler);
                }
            }
            return handlers[source];
        }

        #endregion Handling Query

        #region Parsing Query

        private static readonly Dictionary<DataSources, IQueryParser> Parsers = new Dictionary<DataSources, IQueryParser>();
        
        private static readonly object _parserLock = new object();

        private static IQueryParser Get(DataSources type)
        {
            IQueryParser parser;

            lock (_parserLock)
            {
                if (!Parsers.TryGetValue(type, out parser))
                {
                    parser = ObjectFactory.BuildUp<IQueryParser>(type.ToString());
                    Parsers.Add(type, parser);
                }
            }

            return parser;
        }

        public static string Parse(DataSources type, IAdfQuery query)
        {
            return Get(type).Parse(query);
        }

        #endregion Parsing Query

        #region Running Query

        /// <summary>
        /// Executes a specified <see cref="IAdfQuery"/> statement and returns the affected row.
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The query result into <see cref="IInternalState"/> object, if found the <see cref="IAdfQuery"/>; 
        /// otherwise, null value object of <see cref="Adf.Core.Data.NullInternalState"/>.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        public static IInternalState Run(DataSources datasource, IAdfQuery query)
        {
            using (new TracingScope())
            {
                return query == null ? NullInternalState.Null : GetHandler(datasource).Run(query);
            }
        }

        /// <summary>
        /// Executes the specified query and return the result, where each row in the result is stored in an
        /// instance of <see cref="IInternalState"/>.
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>Query results, where each row is in a specific <see cref="IInternalState"/> object, if found the <see cref="IAdfQuery"/>; 
        /// otherwise, a NullArray of <see cref="Adf.Core.Data.NullInternalState"/>.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        public static IEnumerable<IInternalState> RunSplit(DataSources datasource, IAdfQuery query)
        {
            using (new TracingScope())
            {
                return query == null ? NullInternalState.NullList : GetHandler(datasource).RunAndSplit(query);
            }
        }

        /// <summary>
        /// Saved the specified data of <see cref="IInternalState"/> into database.
        /// </summary>
        /// <param name="datasource">the datasource to run the query against</param>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <param name="state">The data of <see cref="IInternalState"/> that needs to be saved.</param>
        /// <returns>True if data is successfully saved; otherwise, false. 
        /// This method also returns false if <see cref="IAdfQuery"/> or <see cref="IInternalState"/> is null.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        public static bool Save(DataSources datasource, IAdfQuery query, IInternalState state)
        {
            using (new TracingScope())
            {
                return query != null && state != null && GetHandler(datasource).Save(query, state);
            }
        }

        /// <summary>
        /// Create a new data state of <see cref="IInternalState"/> to insert data into database.
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The query result into <see cref="IInternalState"/> object, if found the <see cref="IAdfQuery"/>; 
        /// otherwise, null value object of <see cref="Adf.Core.Data.NullInternalState"/>.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        public static IInternalState New(DataSources datasource, IAdfQuery query)
        {
            using (new TracingScope())
            {
                return query == null ? NullInternalState.Null : GetHandler(datasource).New(query);
            }
        }

        /// <summary>
        /// Provides the first column of the first row from the query resultset.
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <param name="datasource"></param>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>First item of query result. This method also returns 0 if <see cref="IAdfQuery"/> is null.</returns>
        /// <exception cref="System.InvalidOperationException">The current state of the connection is closed.</exception>
        public static object RunScalar(DataSources datasource, IAdfQuery query)
        {
            using (new TracingScope())
            {
                return query == null ? 0 : GetHandler(datasource).RunScalar(query);
            }
        }

        #endregion Running Query

        public static int Delete(DataSources dataSource, IAdfQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            using (new TracingScope())
            {
                return GetHandler(dataSource).Delete(query);
            }
        }
    }

    public sealed class TracingScope : IDisposable
    {
        private readonly DateTime _started;
        private readonly string _name;

        public TracingScope(string name = null)
        {
            _name = name;
            _started = DateTime.Now;
        }

        public void Dispose()
        {
            //Trace.Write(message);

            if (_name.IsNullOrEmpty())
            {
#if DEBUG
                Debug.WriteLine("trace: {0} ms", Convert.ToInt32((DateTime.Now - _started).TotalMilliseconds));
#endif
            }
            else
            {
                var message = string.Format("trace {1}: {0} ms", Convert.ToInt32((DateTime.Now - _started).TotalMilliseconds), _name);

                LogManager.Log(LogLevel.Verbose, message);
            }
        }
    }
}
