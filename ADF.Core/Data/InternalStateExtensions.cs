using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Data
{
    public static class InternalStateExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
