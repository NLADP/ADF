using System;
using Adf.Core.Extensions;

namespace Adf.Core.State
{
    public static class StateProviderExtensions
    {
        /// <summary>
        /// This method checks with the provider if the specified key exists. If so, it will return the value for the key.
        /// If not, it will create an instance of T, set the value for the key with the provider to this instance, and return this value.
        /// </summary>
        /// <typeparam name="T">Type that is expected for the value of the key.</typeparam>
        /// <param name="provider">Current IStateProvider to check for value with key.</param>
        /// <param name="key">Key to check.</param>
        /// <returns>Instance of T.</returns>
        public static T GetOrCreate<T>(this IStateProvider provider, string key)
        {
            object value = provider[key];

            if (value == null) { provider[key] = value = typeof(T).New<T>(); }

            return (T)Convert.ChangeType(value, typeof(T));
        }
        
        public static T GetOrSetValue<T>(this IStateProvider provider, string key, Func<T> defaultvalue)
        {
            object value = provider[key];

            if (value == null)
            {
                value = defaultvalue.Invoke();

                provider[key] = value;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T GetOrSetValue<T>(this IStateProvider provider, string key, T value)
        {
            return provider.GetOrSetValue(key, () => value);
        }
        
        public static T GetOrDefault<T>(this IStateProvider provider, string key, T defaultValue = default(T))
        {
            object value = provider[key];

            return value == null ? defaultValue : (T) Convert.ChangeType(value, typeof (T));
        }
    }
}
