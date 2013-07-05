using System;
using System.Collections.Generic;
using Adf.Core.Objects;

namespace Adf.Core.Identity
{
    ///<summary>
    /// 
    ///</summary>
    public static class IdManager
    {
        private static readonly Dictionary<Type, IIdProvider> providers = new Dictionary<Type, IIdProvider>();

        private static readonly object _lock = new object();

        internal static IIdProvider GetProvider<T>()
        {
            lock (_lock)
            {
                if (!providers.ContainsKey(typeof (T)))
                    providers.Add(typeof (T), ObjectFactory.BuildUp<IIdProvider>(typeof (T).Name));
            }
            return providers[typeof(T)];
        }

        ///<summary>
        /// Returns a new Id
        ///</summary>
        ///<typeparam name="T">type of id to get</typeparam>
        ///<returns></returns>
        public static ID New<T>()
        {
            return GetProvider<T>().NewId();
        }

        ///<summary>
        /// Returns a new Id, used for cre-creating Ids
        ///</summary>
        ///<returns></returns>
        public static ID New(object newvalue)
        {
            return new ID(newvalue);
        }

        /// <summary>
        /// Returns an empty ID
        /// </summary>
        /// <returns></returns>
        public static ID Empty()
        {
            return new ID(null);
        }
       
    }
}
