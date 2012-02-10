using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Extensions
{
    public static class DescriptorExtensions
    {
        public static bool IsIn(this Descriptor descriptor, params Descriptor[] p)
        {
            return p.Any(d => d.Name == descriptor.Name);
        }

        public static bool IsNullOrEmpty(this Descriptor value)
        {
            return value == null || value.IsEmpty;
        }
    }
}
