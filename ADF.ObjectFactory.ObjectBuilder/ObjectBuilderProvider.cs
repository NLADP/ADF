using System;
using System.Collections.Generic;
using System.Configuration;
using Adf.Core.Logging;
using Adf.Core.Objects;
using Adf.ObjectFactory.ObjectBuilder.BuilderPolicies;
using Adf.ObjectFactory.ObjectBuilder.Configuration;
using Adf.ObjectFactory.ObjectBuilder.Exceptions;
using Adf.ObjectFactory.ObjectBuilder.Properties;
using Microsoft.Practices.ObjectBuilder;

namespace Adf.ObjectFactory.ObjectBuilder
{
    public class ObjectBuilderProvider : IObjectProvider
    {
        private List<DependencyResolutionLocatorKey> threadStaticTypes = new List<DependencyResolutionLocatorKey>();
        private SystemFactoryConfigurationSection systemFactoryConfiguration;
        private IBuilder<BuilderStage> builder;

        public ObjectBuilderProvider()
        {
            builder = new BuilderBase<BuilderStage>();
            builder.Strategies.AddNew<TypeMappingStrategy>(BuilderStage.PreCreation);
            builder.Strategies.AddNew<SingletonStrategy>(BuilderStage.PreCreation);
            builder.Strategies.AddNew<CreationStrategy>(BuilderStage.Creation);
            builder.Policies.SetDefault<ICreationPolicy>(new CreationPolicy());

            systemFactoryConfiguration = ConfigurationManager.GetSection(SystemFactoryConfigurationSection.SystemFactoryConfigurationSectionName) as SystemFactoryConfigurationSection;
            if (systemFactoryConfiguration == null) systemFactoryConfiguration = new SystemFactoryConfigurationSection();

            //PolicyList policyList = new PolicyList();
            foreach (SystemFactoryConfigurationElement serviceElement in systemFactoryConfiguration.Services)
            {
                if (serviceElement.LifeCycle != LifeCycle.InstancePerBuildUp)
                {
                    if (serviceElement.LifeCycle == LifeCycle.InstancePerThread)
                    {
                        threadStaticTypes.Add(new DependencyResolutionLocatorKey(serviceElement.ServiceRealizationType, serviceElement.InstanceName));
                    }
                    builder.Policies.Set<ISingletonPolicy>(new SingletonPolicy(true), serviceElement.ServiceRealizationType, serviceElement.InstanceName);
                }
                builder.Policies.Set<ITypeMappingPolicy>(new TypeMappingPolicy(serviceElement.ServiceRealizationType, serviceElement.InstanceName), serviceElement.ServiceInterfaceType, serviceElement.InstanceName);
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
        public TServiceType BuildUp<TServiceType>()
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
        /// <returns>A new instance of <typeparamref name="TServiceType"/> or any of it subtypes.</returns>
        public TServiceType BuildUp<TServiceType>(string instanceName)
        {
            return (TServiceType)BuildUp(typeof(TServiceType), instanceName);
        }

        /// <overloads>
        /// Returns an instance of specified type />.
        /// </overloads>
        /// <summary>
        /// Returns a new default instance of specified type based on configuration information 
        /// from the default configuration source.
        /// </summary>
        /// <returns>A new instance of serviceType or any of it subtypes.</returns>
        public object BuildUp(Type serviceType)
        {
            return BuildUp(serviceType, null);
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
                return builder.BuildUp(new SystemFactoryLocator(threadStaticTypes), serviceType, string.IsNullOrEmpty(instanceName) ? string.Empty : instanceName, null);
            }
            catch (ArgumentException ae)
            {
                var exception = new SystemFactoryException(string.Format(Resources.CannotBuildUpInstanceOfType, serviceType, instanceName), ae);

                LogManager.Log(exception);

                throw exception;
            }
        }

        public IEnumerable<TServiceType> BuildAll<TServiceType>()
        {
            return BuildAll<TServiceType>(false);
        }

        public void Register<TInterface, TImplementation>(string instanceName = null) where TImplementation : TInterface
        {
            throw new NotSupportedException();
        }

        public IEnumerable<object> BuildAll(Type serviceType)
        {
            return BuildAll(serviceType, false);
        }

        public IEnumerable<TServiceType> BuildAll<TServiceType>(bool inherited)
        {
            foreach (TServiceType service in BuildAll(typeof(TServiceType), inherited))
            {
                yield return service;
            }
        }

        public IEnumerable<object> BuildAll(Type serviceType, bool inherited)
        {
            foreach (SystemFactoryConfigurationElement serviceElement in systemFactoryConfiguration.Services)
            {
                if (serviceElement.ServiceInterfaceType == serviceType || inherited && serviceType.IsAssignableFrom(serviceElement.ServiceInterfaceType))
                {
                    yield return BuildUp(serviceElement.ServiceInterfaceType, serviceElement.InstanceName);
                }
            }
        }

        private class SystemFactoryLocator : Locator
        {
            static ILifetimeContainer container = new LifetimeContainer();

            static Dictionary<object, object> _staticInstances = new Dictionary<object, object>();

            [ThreadStatic]
            static Dictionary<object, object> _threadStaticInstances = new Dictionary<object, object>();

            private List<DependencyResolutionLocatorKey> threadStaticTypes;

            public SystemFactoryLocator(List<DependencyResolutionLocatorKey> threadStaticTypes)
            {
                this.threadStaticTypes = threadStaticTypes;

                if (_staticInstances == null) _staticInstances = new Dictionary<object, object>();

                if (_threadStaticInstances == null) _threadStaticInstances = new Dictionary<object, object>();
            }

            public override object Get(object key, SearchMode options)
            {
                if (BelongsToThreadStaticScope(key))
                {
                    if (_threadStaticInstances.ContainsKey(key))
                    {
                        return _threadStaticInstances[key];
                    }
                }
                else
                {
                    if (_staticInstances.ContainsKey(key))
                    {
                        return _staticInstances[key];
                    }
                }
                if (key is Type)
                {
                    if (typeof(ILifetimeContainer) == (Type)key)
                    {
                        return container;
                    }
                }
                return null;
            }

            public override bool Contains(object key, SearchMode options)
            {
                if (BelongsToThreadStaticScope(key))
                {
                    return _threadStaticInstances.ContainsKey(key);
                }
                else
                {
                    return _staticInstances.ContainsKey(key);
                }
            }

            public override void Add(object key, object value)
            {
                if (BelongsToThreadStaticScope(key))
                {
                    _threadStaticInstances.Add(key, value);
                }
                else if (!_staticInstances.ContainsKey(key))
                {
                    _staticInstances.Add(key, value);
                }
            }

            public override bool Remove(object key)
            {
                if (BelongsToThreadStaticScope(key))
                {
                    return _threadStaticInstances.Remove(key);
                }
                else
                {
                    return _staticInstances.Remove(key);
                }
            }

            public override int Count
            {
                get
                {
                    return _staticInstances.Count + _threadStaticInstances.Count;
                }
            }

            private bool BelongsToThreadStaticScope(object o)
            {
                var dependencyResolutionLocatorKey = o as DependencyResolutionLocatorKey;

                if (o != null)
                {
                    return threadStaticTypes.Contains(dependencyResolutionLocatorKey);
                }
                return false;
            }

//            private Type GetTypeFromKey(object o)
//            {
//                if (o is DependencyResolutionLocatorKey)
//                {
//                    return ((DependencyResolutionLocatorKey)o).Type;
//                }
//                else if (o is Type)
//                {
//                    return o as Type;
//                }
//                throw new InvalidOperationException();
//            }

        }
    }

}
