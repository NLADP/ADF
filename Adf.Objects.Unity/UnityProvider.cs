using System;
using System.Collections.Generic;
using System.Configuration;
using Adf.Base.Configuration;
using Adf.Core.Logging;
using Adf.Core.Objects;
using Adf.Objects.Unity.Properties;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Adf.Objects.Unity
{
    public class UnityProvider : IObjectProvider, IDisposable
    {
        private readonly IUnityContainer unityContainer;

        public UnityProvider()
        {
            unityContainer = new UnityContainer();

            if (ConfigurationManager.GetSection("unity") != null) // prevent ArgumentNullException
            {
                unityContainer.LoadConfiguration();
            }

            LoadAdfConfiguration();
        }

        private void LoadAdfConfiguration()
        {
            ObjectFactoryConfigurationSection _objectFactoryConfiguration =
                ConfigurationManager.GetSection(ObjectFactoryConfigurationSection.ObjectFactoryConfigurationSectionName)
                as ObjectFactoryConfigurationSection;

            if (_objectFactoryConfiguration == null) return;

            foreach (ObjectFactoryConfigurationElement service in _objectFactoryConfiguration.Services)
            {
                unityContainer.RegisterType(service.ServiceInterfaceType, service.ServiceRealizationType,
                                            service.InstanceName, GetLifeTime(service.LifeCycle), new InjectionConstructor());
            }
        }

        private static LifetimeManager GetLifeTime(LifeCycle lc)
        {
            switch(lc)
            {
                case LifeCycle.InstancePerBuildUp:
                    return new PerResolveLifetimeManager();
                case LifeCycle.InstancePerThread:
                    return new PerThreadLifetimeManager();
                case LifeCycle.SharedInstance:
                    return new TransientLifetimeManager();
            }
            return null;
        }

        /// <overloads>
        /// Returns an instance of type serviceType.
        /// </overloads>
        /// <summary>
        /// Returns a new default instance of type serviceType based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <returns>A new instance of serviceType or any of it subtypes.</returns>
        public object BuildUp(Type serviceType, string instanceName)
        {
            try
            {
                return unityContainer.Resolve(serviceType, instanceName);
            }
            catch (ArgumentException ae)
            {
                var exception = new ObjectFactoryConfigurationException(string.Format(Resources.CannotBuildUpInstanceOfType, serviceType, instanceName), ae);

                LogManager.Log(exception);

                throw exception;
            }
        }

        public T BuildUp<T>(string instanceName = null)
        {
            try
            {
                return unityContainer.Resolve<T>(instanceName);
            }
            catch (ArgumentException ae)
            {
                var exception = new ObjectFactoryConfigurationException(string.Format(Resources.CannotBuildUpInstanceOfType, typeof(T), instanceName), ae);

                LogManager.Log(exception);

                throw exception;
            }
        }

        public IEnumerable<object> BuildAll(Type serviceType, bool inherited)
        {
            return unityContainer.ResolveAll(serviceType);
        }

        public IEnumerable<T> BuildAll<T>()
        {
            return unityContainer.ResolveAll<T>();
        }

        public void Register<TInterface, TImplementation>(string instanceName = null)
        {
            throw new NotImplementedException();
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            unityContainer.Dispose();
        }

        #endregion
    }

}
