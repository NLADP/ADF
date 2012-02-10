using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Adf.Core.Objects;

namespace Adf.Base.Domain
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
