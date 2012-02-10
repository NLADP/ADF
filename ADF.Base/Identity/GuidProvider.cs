using System;
using Adf.Core.Identity;

namespace Adf.Base.Identity
{
	/// <summary>
    /// Represents <see cref="System.Guid"/> Provider.
    /// Provides methods to generate new <see cref="System.Guid"/>, check the equality of two <see cref="System.Guid"/>s etc.
	/// </summary>
	public class GuidProvider : IIdProvider
	{
		/// <summary>
        /// Creates and returns a new <see cref="System.Guid"/>.
		/// </summary>
		/// <param name="p">The parameters. This is not currently being used.</param>
        /// <returns>
        /// The newly created <see cref="System.Guid"/>.
        /// </returns>
		public ID NewId(params object[] p)
		{
			return new ID(Guid.NewGuid());
		}

        ///// <summary>
        ///// Gets an empty <see cref="System.Guid"/>.
        ///// </summary>
        ///// <value>The empty <see cref="System.Guid"/>.</value>
        //public ID Empty
        //{
        //    get { return new ID(Guid.Empty); }
        //}

        ///// <summary>
        ///// Returns a value indicating whether the two supplied <see cref="System.Guid"/>s are equal or not.
        ///// </summary>
        ///// <param name="x">The first <see cref="System.Guid"/>.</param>
        ///// <param name="y">The second <see cref="System.Guid"/>.</param>
        ///// <returns>
        ///// true if the two supplied <see cref="System.Guid"/>s are equal; otherwise, false.
        ///// </returns>
        ///// <exception cref="System.ArgumentNullException">
        ///// x is null.
        ///// </exception>
        ///// <exception cref="System.ArgumentNullException">
        ///// y is null.
        ///// </exception>
        ///// <exception cref="System.NullReferenceException">
        ///// Object reference not set to an instance of an object.
        ///// </exception>
        //public bool Equals(ID x, ID y)
        //{
        //    Guid xvalue = new Guid(x.Value.ToString());
        //    Guid yvalue = new Guid(y.Value.ToString());

        //    return (xvalue == yvalue);
        //} 
	}
}
