using Adf.Core.Styling;

namespace Adf.Web.Styling
{
	/// <summary>
    /// Represents a styler which does not apply any style to a <see cref="System.Web.UI.Control"/>.
    /// Provides property, method to return an empty Adf.Web.UI.Styling.NullStyler, to do 
    /// nothing to a <see cref="System.Web.UI.Control"/>.
	/// </summary>
	public class NullStyler : IStyler
	{
        /// <summary>
        /// Does nothing to the specified <see cref="System.Web.UI.Control"/>.
        /// Does not apply any style to the specified <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <param name="target">The <see cref="System.Web.UI.Control"/> to do nothing to.</param>
		public void Style(object target)
		{
			// Do nothing
		}

        private static readonly NullStyler empty = new NullStyler();

        /// <summary>
        /// Gets an empty <see cref="NullStyler"/>.
        /// </summary>
        /// <returns>
        /// An empty <see cref="NullStyler"/>.
        /// </returns>
		public static NullStyler Empty
		{
			get { return empty; }
		}
	}
}
