using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Adf.Core.Extensions;

namespace Adf.Core.Binding
{
    public sealed class BindScope<T> : IDisposable
    {
        private readonly PropertyInfo _property;

        public BindScope(Expression<Func<T, object>> property, Func<IEnumerable> collection)
        {
            _property = property.GetPropertyInfo();

            BindManager.RegisterScope(_property, collection);
        }

        public void Dispose()
        {
            BindManager.UnregisterScope(_property);
        }
    }
}
