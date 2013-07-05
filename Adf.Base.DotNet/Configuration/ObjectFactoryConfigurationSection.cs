using System;
using System.ComponentModel;
using System.Configuration;

namespace Adf.Base.Configuration
{
    public class ObjectFactoryConfigurationSection : ConfigurationSection
    {
        public const string ObjectFactoryConfigurationSectionName = "objectFactoryConfiguration";

        private const string ServicesPropertyKey = "services";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), ConfigurationProperty(ServicesPropertyKey)]
        public ObjectFactoryConfigurationElementCollection Services
        {
            get { return (ObjectFactoryConfigurationElementCollection)base[ServicesPropertyKey]; }
            set { base[ServicesPropertyKey] = value; }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface"), ConfigurationCollection(typeof(ObjectFactoryConfigurationElement))]
    public class ObjectFactoryConfigurationElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ObjectFactoryConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ObjectFactoryConfigurationElement serviceElement = (ObjectFactoryConfigurationElement)element;
            return new TypeAndName(serviceElement.ServiceInterfaceType, serviceElement.InstanceName);
        }

        private struct TypeAndName
        {
            // ReSharper disable NotAccessedField.Local, it will be used by the ConfigurationElementCollection
            // to make sure this instance is unique
            Type _type;
            string _name;
            // ReSharper restore NotAccessedField.Local

            public TypeAndName(Type type, string name)
            {
                _type = type;
                _name = name;
            }
        }
    }

    public class ObjectFactoryConfigurationElement : ConfigurationElement
    {
        private const string ServiceInterfaceTypePropertyKey = "serviceInterfaceType";
        private const string ServiceRealizationTypePropertyKey = "serviceRealizationType";
        private const string LifeCyclePropertyKey = "lifeCycle";
        private const string InstanceNamePropertyKey = "instanceName";

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

    public enum LifeCycle
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
