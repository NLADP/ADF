using System;

namespace Adf.Core.Domain
{
    /// <summary>
    /// Defines a generalized definition for SmartReferences.
    /// </summary>
    public interface ISmartReference : IDomainObject
    {
        /// <summary>
        /// Gets the <see cref="Type"/> of SmartReference.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the Name of SmartReference.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Description of SmartReference.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets whether the object is default or not.
        /// </summary>
        bool IsDefault { get; }
    }
}
