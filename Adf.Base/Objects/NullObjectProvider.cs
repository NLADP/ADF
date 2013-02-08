using System;
using System.Collections.Generic;
using System.Linq;

namespace Adf.Core.Objects
{
    public class NullObjectProvider : IObjectProvider
    {
        public object BuildUp(Type serviceType, string instanceName)
        {
            return null;
        }

        public T BuildUp<T>(string instanceName = null)
        {
            return default(T);
        }

        public IEnumerable<object> BuildAll(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> BuildAll(Type serviceType, bool inherited)
        {
            return Enumerable.Empty<object>();
        }

        public IEnumerable<T> BuildAll<T>()
        {
            return Enumerable.Empty<T>();
        }

        public void Register<TInterface, TImplementation>(string instanceName = null) where TImplementation : TInterface
        {
            throw new NotImplementedException();
        }
    }
}