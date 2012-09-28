using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Adf.Core.Resources;
using Adf.Core.State;
using ResourceManager = System.Resources.ResourceManager;

namespace Adf.Base.Resources
{
    /// <summary>
    /// Represents the resource provider for the resource files.
    /// Provides methods to get the string value corresponding to the specified key from the resource files etc.
    /// </summary>
    public class ResourcesFileResourceProvider : IResourceProvider
    {
        private static Assembly resourceAssembly;

        private static Assembly ResourceAssembly
        {
            get
            {
                if (resourceAssembly == null)
                {
                    string resAssemblyName = (string)StateManager.Settings["ResourceAssembly"];

                    // Try to find the assembly in the current appdomain
                    resourceAssembly =
                        AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(
                            o => o.GetName().Name == Path.GetFileNameWithoutExtension(resAssemblyName));

                    resourceAssembly = resourceAssembly ?? Assembly.Load(resAssemblyName);
                    if (resourceAssembly == null)
                        throw new ConfigurationErrorsException(
                            "ResourceAssembly specified in settings can not be found.");
                }
                return resourceAssembly;
            }
        }

        /// <summary>
        /// Gets the file based <see cref="System.Resources.ResourceManager"/>.
        /// </summary>
        /// <returns>
        /// The file based <see cref="System.Resources.ResourceManager"/>.
        /// </returns>
        private static System.Resources.ResourceManager Resource
        {
            get
            {
                if (StateManager.Settings.Has("ResourceDir"))
                {
                    return ResourceManager.CreateFileBasedResourceManager(
                        (string) StateManager.Settings["ResourceFile"],
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                        (string) StateManager.Settings["ResourceDir"]),
                        null);
                }
                if (StateManager.Settings.Has("ResourceAssembly"))
                {
                    return new ResourceManager((string)StateManager.Settings["ResourceFile"], ResourceAssembly);
                }
                return null;
            }
        }

        #region IResourceProvider Members
        /// <summary>
        /// Returns the value from the resource file corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <returns>
        /// The corresponding value for the specified key.
        /// </returns>
        public string GetString(string key)
        {
            System.Resources.ResourceManager resource = Resource;

            string value = resource.GetString(key);

            resource.ReleaseAllResources();

            return value;
        }

        /// <summary>
        /// Returns a value from the resource file corresponding to the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <param name="culture">The specified <see cref="System.Globalization.CultureInfo"/>.</param>
        /// <returns>
        /// The corresponding value for the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </returns>
        public string GetString(string key, CultureInfo culture)
        {
            System.Resources.ResourceManager resource = Resource;

            string value = resource.GetString(key, culture);

            resource.ReleaseAllResources();

            return value;
        }

        #endregion
    }
}
