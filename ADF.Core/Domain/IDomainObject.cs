using System;
using Adf.Core.Identity;

namespace Adf.Core.Domain
{
	/// <summary>
    /// Defines properties, methods that a value type or class implements to save, remove the DomainObject etc. 
	/// </summary>
	public interface IDomainObject: IComparable
	{
		/// <summary>
		/// Gets the ID of the DomainObject.
		/// </summary>
        /// <returns>The ID of the DomainObject.</returns>
        ID Id { get; }
		
		/// <summary>
		/// Gets the title of the DomainObject.
		/// </summary>
        /// <returns>The title of the DomainObject.</returns>
		string Title { get; }

        /// <summary>
        /// Gets the value indicating whether the DomainObject is empty.
        /// </summary>
        /// <returns>true if the DomainObject is empty; otherwise, false.</returns>
        bool IsEmpty {get;}

        /// <summary>
        /// Gets the value indicating whether the DomainObject is altered.
        /// </summary>
        /// <returns>true if the DomainObject is altered; otherwise, false.</returns>
        bool IsAltered { get; }
	}
}