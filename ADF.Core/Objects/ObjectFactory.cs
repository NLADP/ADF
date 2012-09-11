using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Adf.Core.Extensions;

namespace Adf.Core.Objects
{
    /// <summary>
    /// Static facade for the generic building mechanism.
    /// </summary>
    /// <remarks>
    /// The facade requires you to bootstrap the implementation of the actual system factory instance.
    /// </remarks>
    public static class ObjectFactory
    {
        private static IObjectProvider _objectProvider;

        private static IObjectProvider ObjectProvider
        {
            get
            {
                if (_objectProvider == null)
                {
                    ObjectFactorySection section = (ObjectFactorySection)ConfigurationManager.GetSection(ObjectFactorySection.SectionName) ??
                                                         new ObjectFactorySection();
                    _objectProvider = section.FactoryType.New<IObjectProvider>();
                }
                return _objectProvider;
            }
        }

        /// <overloads>
        /// Returns an instance of type <typeparamref name="TServiceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a new default instance of type <typeparamref name="TServiceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <typeparam name="TServiceType">The type to build.</typeparam>
        /// <returns>A new instance of <typeparamref name="TServiceType"/> or any of it subtypes.</returns>
        public static TServiceType BuildUp<TServiceType>()
        {
            return BuildUp<TServiceType>(null);
        }

        /// <overloads>
        /// Returns an instance of type <typeparamref name="TServiceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a new default instance of type <typeparamref name="TServiceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <typeparam name="TServiceType">The type to build.</typeparam>
        /// <param name="instanceName">The name of the instance to build.</param>
        /// <returns>A new instance of <typeparamref name="TServiceType"/> or any of it subtypes.</returns>
        public static TServiceType BuildUp<TServiceType>(string instanceName)
        {
            return (TServiceType)BuildUp(typeof(TServiceType), instanceName);
        }

        /// <overloads>
        /// Returns an instance of type <paramref name="serviceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a new default instance of type <paramref name="serviceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <param name="serviceType">The type to build.</param>
        /// <returns>A new instance of <paramref name="serviceType"/> or any of it subtypes.</returns>
        public static object BuildUp(Type serviceType)
        {
            return BuildUp(serviceType, null);
        }

        /// <overloads>
        /// Returns an instance of type <paramref name="serviceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a new default instance of type <paramref name="serviceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <param name="serviceType">The type to build.</param>
        /// <param name="instanceName">The name of the instance to build.</param>
        /// <returns>A new instance of <paramref name="serviceType"/> or any of it subtypes.</returns>
        public static object BuildUp(Type serviceType, string instanceName)
        {
            return ObjectProvider.BuildUp(serviceType, instanceName);
        }

        /// <overloads>
        /// Returns a list of instances of type <typeparamref name="TServiceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a list of instance of type <typeparamref name="TServiceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <typeparam name="TServiceType">The type to build.</typeparam>
        /// <returns>A list of instance of <typeparamref name="TServiceType"/> or any of it subtypes.</returns>
        public static IEnumerable<TServiceType> BuildAll<TServiceType>()
        {
            return BuildAll<TServiceType>(false);
        }

        /// <overloads>
        /// Returns a list of instances of type <paramref name="serviceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a list of instance of type <paramref name="serviceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <param name="serviceType">The type to build.</param>
        /// <returns>A list of instance of <paramref name="serviceType"/> or any of it subtypes.</returns>
        public static IEnumerable<object> BuildAll(Type serviceType)
        {
            return BuildAll(serviceType, false);
        }

        /// <overloads>
        /// Returns a list of instances of type <typeparamref name="TServiceType"/>.
        /// </overloads>
        /// <summary>
        /// Returns a list of instance of type <typeparamref name="TServiceType"/> based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <typeparam name="TServiceType">The type to build.</typeparam>
        /// <param name="inherited">Also search subtypes.</param>
        /// <returns>A list of instance of <typeparamref name="TServiceType"/> or any of it subtypes.</returns>
        public static IEnumerable<TServiceType> BuildAll<TServiceType>(bool inherited)
        {
            IEnumerable<object> items = BuildAll(typeof(TServiceType), inherited);
            foreach (TServiceType service in items)
            {
                yield return service;
            }
        }

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
        public static IEnumerable<object> BuildAll(Type serviceType, bool inherited)
        {
            return ObjectProvider.BuildAll(serviceType, inherited);
        }
    }

    public class NullObjectProvider : IObjectProvider
    {
        public object BuildUp(Type serviceType, string instanceName)
        {
            return null;
        }

        public IEnumerable<object> BuildAll(Type serviceType, bool inherited)
        {
            return Enumerable.Empty<object>();
        }
    }
}
