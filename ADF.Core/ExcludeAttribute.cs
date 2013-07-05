using System;
using System.Linq;
using System.Reflection;

namespace Adf.Core
{
    /// <summary>
    /// Represents custom attribute of a field to indicate whether the value of the field of a 
    /// <see cref="System.Type"/> will be excleded from a list.
    /// Provides method to check whether the value of the specified field will be excleded from a list.
    /// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class ExcludeAttribute : Attribute
	{
        /// <summary>
        /// Returns a value indicating whether the value of the specified field will be excluded from a list.
        /// </summary>
        /// <param name="mi">The field to check.</param>
        /// <returns>
        /// true if the value of the specified field will be excleded from a list; otherwise, false.
        /// </returns>
        /// <exception cref="System.NullReferenceException">
        /// Object reference not set to an instance of an object.
        /// </exception>
        public static bool IsExcluded(MemberInfo mi)
		{
			if (mi == null)
				return false;
        	
			return (mi.GetCustomAttributes(typeof(ExcludeAttribute), false).Any());
		}

	}


}
