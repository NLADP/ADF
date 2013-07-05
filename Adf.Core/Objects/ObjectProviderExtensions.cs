using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Objects
{
    public static class ObjectProviderExtensions
    {
        public static IObjectProvider Add<TInterface, TImplementation>(this IObjectProvider provider, string instanceName = null) where TImplementation : TInterface
        {
            provider.Register<TInterface, TImplementation>(instanceName);

            return provider;

        }
    }
}
