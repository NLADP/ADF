using System;
using System.Configuration;

namespace Adf.Core.Objects
{
    class ObjectFactorySection : ConfigurationSection
    {
        /// <summary>
        /// Specifies the name of the Section.
        /// </summary>
        public const string SectionName = "objectFactorySection";

        /// <summary>
        /// Returns the Type specified in the configuration file, which is the full typename of the Dependency Injector.
        /// </summary>
        [ConfigurationProperty("type", DefaultValue = "Adf.Core.Objects.NullObjectProvider, Adf.Core", IsRequired = false, IsKey = false)]
//        [ConfigurationProperty("type", IsRequired = true, IsKey = false)]
        private string FactoryTypeName
        {
            get { return this["type"].ToString(); }
        }

        public Type FactoryType
        {
            get { return Type.GetType(FactoryTypeName, true); }
        }
    }
}
