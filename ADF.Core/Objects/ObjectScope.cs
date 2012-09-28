using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Adf.Core.Objects
{
    public sealed class ObjectScope<T> : IDisposable
    {
        public ObjectScope(ICollection<T> collection = null)
        {
            ObjectManager.Register(collection ?? new Collection<T>());
        }

        public void Dispose()
        {
            ObjectManager.Unregister<T>();
        }
    }
}
