using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using Adf.Core.Extensions;

namespace Adf.Core.Types
{
    public class ObjectActivator
    {
        class CtorType
        {
            private readonly int _hashCode;

            public CtorType(Type type, IEnumerable<Type> argTypes)
            {
                int i = 0;  // take order of types into account
                _hashCode = type.GetHashCode() * ++i;

                foreach (var t in argTypes)
                {
                    _hashCode ^= t.GetHashCode() * ++i;
                }
            }

            public override bool Equals(object obj)
            {
                var other = obj as CtorType;

                if (other == null) return false;

                return _hashCode == other._hashCode;
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }
        }

        public delegate T ActivateObject<out T>(params object[] args);

        private static readonly Dictionary<CtorType, object> _activators = new Dictionary<CtorType, object>();

        private static readonly object _lock = new object();

        public static ActivateObject<T> GetActivator<T>(Type type, Type[] argTypes)
        {
            var ctype = new CtorType(type, argTypes);

            ActivateObject<T> activator;
            object a;

            if (_activators.TryGetValue(ctype, out a))
            {
                activator = (ActivateObject<T>)a;
            }
            else
            {
                activator = CreateActivator<T>(type, argTypes);

                lock (_lock)  // prevent concurrent adds
                {
                    if (!_activators.ContainsKey(ctype))
                    {
                        _activators.Add(ctype, activator);
                    }
                }
            }

            return activator;
        }

        private static ActivateObject<T> CreateActivator<T>(Type type, Type[] argTypes)
        {
            var ctor = FindConstructor(type, argTypes);

            if (ctor == null) throw new InvalidOperationException("Could not find corresponding constructor on Type");

            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

            var argsExp = new Expression[paramsInfo.Length];

            //pick each arg from the params array and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);

                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramsInfo[i].ParameterType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls thector with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the NewExpression as body and our param object[] as arg
            LambdaExpression lambda = Expression.Lambda(typeof(ActivateObject<T>), newExp, param);

            //compile it
            return (ActivateObject<T>)lambda.Compile();
        }

        private static ConstructorInfo FindConstructor(Type type, Type[] argTypes)
        {
            return type.GetTypeInfo().DeclaredConstructors
                .FirstOrDefault(c =>
                {
                    var parameters = c.GetParameters();

                    if (parameters.Length != argTypes.Length) return false;

                    return !parameters.Where((p, i) => !p.ParameterType.IsAssignableFrom(argTypes[i]) && argTypes[i] != typeof(object)).Any();
                });
        }
    }
}
