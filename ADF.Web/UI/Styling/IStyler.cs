using System.Web.UI;

namespace Adf.Web.UI.Styling
{
	/// <summary>
    /// Defines a method that a class implements to set CSS styles to a Control.
	/// </summary>
	public interface IStyler
	{
        /// <summary>
        /// Sets the CSS styles to the specified Control.
        /// </summary>
        /// <param name="c">The Control to give CSS styles to.</param>
		void SetStyles(Control c);
	}
}
