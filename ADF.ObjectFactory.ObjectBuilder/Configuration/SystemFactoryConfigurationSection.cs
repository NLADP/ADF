using System;
using System.ComponentModel;
using System.Configuration;

namespace Adf.ObjectFactory.ObjectBuilder.Configuration
{

    public class SystemFactoryConfigurationSection : ConfigurationSection
    {
        public SystemFactoryConfigurationSection()
        {
        }

        public const string SystemFactoryConfigurationSectionName = "systemFactoryConfiguration";

        private const string ServicesPropertyKey = "services";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), ConfigurationProperty(ServicesPropertyKey)]
        public SystemFactoryConfigurationElementCollection Services
        {
            get { return (SystemFactoryConfigurationElementCollection)base[ServicesPropertyKey]; }
            set { base[ServicesPropertyKey] = value; }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface"), ConfigurationCollection(typeof(SystemFactoryConfigurationElement))]
    public class SystemFactoryConfigurationElementCollection : ConfigurationElementCollection
    {
        public SystemFactoryConfigurationElementCollection()
        {
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SystemFactoryConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            SystemFactoryConfigurationElement serviceElement = (SystemFactoryConfigurationElement)element;
            return new TypeAndName(serviceElement.ServiceInterfaceType, serviceElement.InstanceName);
        }

        private struct TypeAndName
        {
            Type _type;
            string _name;

            public TypeAndName(Type type, string name)
            {
                _type = type;
                _name = name;
            }
        }
    }

    public class SystemFactoryConfigurationElement : ConfigurationElement
    {
        private const string ServiceInterfaceTypePropertyKey = "serviceInterfaceType";
        private const string ServiceRealizationTypePropertyKey = "serviceRealizationType";
        private const string LifeCyclePropertyKey = "lifeCycle";
        private const string InstanceNamePropertyKey = "instanceName";

        public SystemFactoryConfigurationElement()
        {
        }

        [ConfigurationProperty(ServiceInterfaceTypePropertyKey, IsKey = true, IsRequired = true)]
        [TypeConverter(typeof(AssemblyQualifiedTypeNameConverter))]
        public Type ServiceInterfaceType
        {
            get { return (Type)base[ServiceInterfaceTypePropertyKey]; }
            set { base[ServiceInterfaceTypePropertyKey] = value; }
        }

        [ConfigurationProperty(ServiceRealizationTypePropertyKey, IsKey = false, IsRequired = true)]
        [TypeConverter(typeof(AssemblyQualifiedTypeNameConverter))]
        public Type ServiceRealizationType
        {
            get { return (Type)base[ServiceRealizationTypePropertyKey]; }
            set { base[ServiceRealizationTypePropertyKey] = value; }
        }

        [ConfigurationProperty(LifeCyclePropertyKey, IsKey = false, IsRequired = false, DefaultValue = LifeCycle.InstancePerBuildUp)]
        public LifeCycle LifeCycle
        {
            get { return (LifeCycle)base[LifeCyclePropertyKey]; }
            set { base[LifeCyclePropertyKey] = value; }
        }

        [ConfigurationProperty(InstanceNamePropertyKey, IsKey = true, IsRequired = false, DefaultValue = null)]
        public string InstanceName
        {
            get { return (string)base[InstanceNamePropertyKey]; }
            set { base[InstanceNamePropertyKey] = value; }
        }
    }

    public enum LifeCycle : int
    {
        /// <summary>
        /// standard new behaviour and default for the system factory
        /// </summary>
        InstancePerBuildUp = 0,
        /// <summary>
        /// used for contextual service which are not shared across threads or asp.net requests
        /// </summary>
        InstancePerThread = 1,
        /// <summary>
        /// instance that is shared amongs all threads and asp.net requests.
        /// </summary>
        SharedInstance = 2,

    }
}
