using System;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Represents MaskedAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MaskedAttribute : Attribute
    {
        private string mask;

        /// <summary>
        /// Initializes an object of <see cref="MaskedAttribute"/> with the supplied mask.
        /// </summary>
        /// <param name="mask">The mask.</param>
        public MaskedAttribute(string mask)
        {
            this.mask = mask;
        }

        /// <summary>
        /// Returns the mask of the <see cref="MaskedAttribute"/>.
        /// </summary>
        public string Mask
        {
            get { return mask; }
        }
    }
}