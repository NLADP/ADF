using System;
using System.Collections.Generic;

namespace Adf.Core.Objects
{
    /// <summary>
    /// Defines methods that a class implements to create objects.
    /// </summary>
    public interface IObjectProvider
    {
        /// <overloads>
        /// Returns an instance of type <paramref name="serviceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a new default instance of type <paramref name="serviceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <param name="serviceType">The type to build.</param>
        /// <param name="instanceName">The name of the instance to build, or null to build the default instance.</param>
        /// <returns>A new instance of <paramref name="serviceType"/> or any of it subtypes.</returns>
        object BuildUp(Type serviceType, string instanceName);

        /// <overloads>
        /// Returns a list of instances of type <paramref name="serviceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a list of instance of type <paramref name="serviceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <param name="serviceType">The type to build.</param>
        /// <param name="inherited">Also search subtypes.</param>
        /// <returns>A list of instance of <paramref name="serviceType"/> or any of it subtypes.</returns>
        IEnumerable<object> BuildAll(Type serviceType, bool inherited);
    }
}
