using System;

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
            if (!provider.Has(key)) { provider[key] = Activator.CreateInstance<T>(); }

            return (T) provider[key];
        }
        
        public static T GetOrSetValue<T>(this IStateProvider provider, string key, Func<T> value)
        {
            if (!provider.Has(key)) { provider[key] = value.Invoke(); }

            return (T)provider[key];
        }

        public static T GetOrSetValue<T>(this IStateProvider provider, string key, T value)
        {
            if (!provider.Has(key)) { provider[key] = value; }

            return (T)provider[key];
        }
        
        public static T GetOrDefault<T>(this IStateProvider provider, string key, T value)
        {
            if (provider.Has(key)) { return provider[key] == null ? value : (T) provider[key]; }

            return value;
        }

    }
}
