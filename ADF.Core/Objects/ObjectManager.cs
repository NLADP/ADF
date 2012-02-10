﻿using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Domain;
using Adf.Core.State;

namespace Adf.Core.Objects
{
    public static class ObjectManager
    {
        private const string key = "ObjectManager.";

        public static void Register<T>(ICollection<T> collection)
        {
            StateManager.Personal[key + typeof (T)] = collection;
        }

        public static void Unregister<T>()
        {
            StateManager.Personal.Remove(key + typeof(T));
        }

        public static ICollection<T> Get<T>()
        {
            return StateManager.Personal[key + typeof (T)] as ICollection<T>;
        }

        public static T GetItemOrDefault<T>(Func<T, bool> predicate, Func<T> defaultValue) where T : class, IDomainObject
        {
            T item = null;

            var list = Get<T>();

            if (list != null) item = list.FirstOrDefault(predicate);

            if (item == null)
            {
                item = defaultValue.Invoke();

                if (list != null && !item.IsEmpty)
                {
                    list.Add(item);
                }
            }
            return item;
        }
    }
}
