using System.Collections.Generic;

namespace Adf.Data.Search
{
    /// <summary>
    /// Defines a method that a class implements to get the parameters.
    /// The class SearchObject implements ISearchObject.
    /// </summary>
    public interface ISearchObject
    {
        /// <summary>
        /// Returns a list of <see cref="ISearchParameter"/>s.
        /// </summary>
        /// <returns>
        /// The list of <see cref="ISearchParameter"/>s.
        /// </returns>
        IEnumerable<ISearchParameter> GetParameters();

        ///<summary>
        /// returns the list of tables to join
        ///</summary>
        ///<returns></returns>
        IEnumerable<IJoinParameter> GetJoinParameters();
    }
}
 