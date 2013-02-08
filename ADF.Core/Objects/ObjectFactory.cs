using System;
using System.Collections.Generic;

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
        public static IObjectProvider ObjectProvider { get; set; }

        public static TServiceType BuildUp<TServiceType>(string instanceName = null)
        {
            return ObjectProvider.BuildUp<TServiceType>(instanceName);
        }

        public static object BuildUp(Type serviceType, string instanceName = null)
        {
            return ObjectProvider.BuildUp(serviceType, instanceName);
        }

        public static IEnumerable<TServiceType> BuildAll<TServiceType>()
        {
            return ObjectProvider.BuildAll<TServiceType>();
        }

        public static IEnumerable<object> BuildAll(Type serviceType, bool inherited)
        {
            return ObjectProvider.BuildAll(serviceType, inherited);
        }

        public static void Register<TInterface, TImplementation>(string instanceName = null) where TImplementation : TInterface
        {
            ObjectProvider.Register<TInterface, TImplementation>(instanceName);
        }
    }
}
