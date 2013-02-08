using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Extensions;
using Adf.Core.Objects;

namespace Adf.Base.Objects
{
    public class CompiledLambdaObjectProvider : IObjectProvider
    {
        struct TypeKey
        {
            public readonly Type Type;
//            public readonly string Instance;
            private readonly int _hashCode;

            public TypeKey(Type type, string instance)
            {
                Type = type;
//                Instance = instance;
                _hashCode = type.GetHashCode() ^ (instance ?? "").GetHashCode();
            }

            public override bool Equals(object other)
            {
                return _hashCode == ((TypeKey)other)._hashCode;
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }
        }

        private readonly Dictionary<TypeKey, Type> _types = new Dictionary<TypeKey, Type>();

        public T BuildUp<T>(string instanceName = null)
        {
            Type type;
            if (_types.TryGetValue(new TypeKey(typeof(T), instanceName), out type))
            {
                return type.New<T>();
            }
            throw new InvalidOperationException("Type not found: " + typeof(T));
        }

        public object BuildUp(Type serviceType, string instanceName = null)
        {
            Type type;
            if (_types.TryGetValue(new TypeKey(serviceType, instanceName), out type))
            {
                return type.New<object>();
            }
            throw new InvalidOperationException("Type not found: " + serviceType);
        }

        public IEnumerable<T> BuildAll<T>()
        {
            return from type in _types where type.Key.Type == typeof(T) select type.Value.New<T>();
        }

        public IEnumerable<object> BuildAll(Type serviceType, bool inherited)
        {
            return from type in _types where type.Key.Type == serviceType select type.Value.New<object>();
        }

        public void Register<TInterface, TImplementation>(string instanceName = null) where TImplementation : TInterface
        {
            _types.Add(new TypeKey(typeof(TInterface), instanceName), typeof(TImplementation));
        }
    }
}
