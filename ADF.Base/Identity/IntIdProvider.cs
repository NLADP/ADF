using Adf.Core.Identity;

namespace Adf.Base.Identity
{
   
    /// <summary>
    /// Represents <see cref="int"/> Provider.
    /// Provides methods to generate new <see cref="int"/>.
    /// </summary>
    public class IntIdProvider : IIdProvider
    {
        /// <summary>
        /// Creates and returns a new <see cref="int"/>.
        /// </summary>
        /// <param name="p">The parameters. This is not currently being used.</param>
        /// <returns>
        /// The newly created <see cref="System.Guid"/>.
        /// </returns>
        public ID NewId(params object[] p)
        {
            return new ID(1);
        }
       
    }
}
