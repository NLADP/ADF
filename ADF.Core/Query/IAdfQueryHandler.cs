using System.Collections.Generic;
using Adf.Core.Data;

namespace Adf.Core.Query
{
    /// <summary>
    /// A general service handler that is independent of the type of handler or service that it is associated with.
    /// Provides functionality to run query and get its result(s), save and update the data.
    /// </summary>
    public interface IAdfQueryHandler
    {
        DataSources DataSource { get; set; }

        /// <summary>
        /// Provides the query handler to executes a specified <see cref="IAdfQuery"/> statement and returns the affected row.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The query result into <see cref="IInternalState"/> object, if found the <see cref="IAdfQuery"/>; 
        /// otherwise, null value object of <see cref="Adf.Core.Data.NullInternalState"/>.</returns>
        IInternalState Run(IAdfQuery query);

        /// <summary>
        /// Provides the query handler to executes the specified query and return the result, 
        /// where each row in the result is stored in an instance of <see cref="IInternalState"/>.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>Query results, where each row is in a specific <see cref="IInternalState"/> object, if found the <see cref="IAdfQuery"/>; 
        /// otherwise, a NullArray of <see cref="Adf.Core.Data.NullInternalState"/>.</returns>
        IEnumerable<IInternalState> RunAndSplit(IAdfQuery query);

        /// <summary>
        /// Provides the query handler to fetch the first column of the first row from the query resultset.
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The first column of the first row in the resultset. 
        /// This method also returns 0 if <see cref="IAdfQuery"/> is null.</returns>
        object RunScalar(IAdfQuery query);

        /// <summary>
        /// Provides the query handler to save the specified data of <see cref="IInternalState"/> into database.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <param name="data">The data of <see cref="IInternalState"/> that needs to be saved.</param>
        /// <returns>True if data is successfully saved; otherwise, false. 
        /// This method also returns false if <see cref="IAdfQuery"/> or <see cref="IInternalState"/> is null.</returns>
        bool Save(IAdfQuery query, IInternalState data);

        /// <summary>
        /// Provides the query handler to create a new data state of <see cref="IInternalState"/> to insert data into database.
        /// </summary>
        /// <param name="query">The <see cref="IAdfQuery"/> that defines the datasource name and query statement.</param>
        /// <returns>The query result into <see cref="IInternalState"/> object, if found the <see cref="IAdfQuery"/>; 
        /// otherwise, null value object of <see cref="Adf.Core.Data.NullInternalState"/>.</returns>
        IInternalState New(IAdfQuery query);

        int Delete(IAdfQuery query);
    }
}
