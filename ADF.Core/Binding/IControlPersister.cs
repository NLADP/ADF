using System.Collections.Generic;
using System.Reflection;

namespace Adf.Core.Binding
{
	/// <summary>	
    /// Defines a generalized Persist method for persisting a DomainObject with a specified control
	/// </summary>
	public interface IControlPersister
	{
		/// <summary>
        /// Gets the types of <see cref="IControlPersister"/>.
		/// </summary>
        /// <return>
        /// String array
        /// </return>
        IEnumerable<string> Types { get; }

		/// <summary>
        /// Persists the supplied DomainObject to the specified property of the supplied control.
        /// </summary>
        /// <param name="bindableObject">The supplied DomainObject.</param>
		/// <param name="pi">The supplied PropertyInfo.</param>
		/// <param name="control">The supplied control.</param>
        void Persist(object bindableObject, PropertyInfo pi, object control);

	}
}
