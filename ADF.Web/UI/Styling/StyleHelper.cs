using System.Web.UI.WebControls;

namespace Adf.Web.UI.Styling
{
    /// <summary>
    /// Represents a utility class to give styles to a Control.
    /// Provides method to create and return a new <see cref="System.Web.UI.WebControls.TableItemStyle"/> 
    /// with the specified CSS Class.
    /// </summary>
	public static class StyleHelper
	{
        /// <summary>
        /// Creates and returns a new <see cref="System.Web.UI.WebControls.TableItemStyle"/> with 
        /// the specified CSS Class.
        /// </summary>
        /// <param name="cssclass">The CSS Class name which is set as the CSS Class of the 
        /// newly created <see cref="System.Web.UI.WebControls.TableItemStyle"/>.</param>
        /// <returns>
        /// The newly created <see cref="System.Web.UI.WebControls.TableItemStyle"/>.
        /// </returns>
		public static TableItemStyle New(string cssclass)
		{
			TableItemStyle style = new TableItemStyle();

			style.CssClass = cssclass;

			return style;
		}

        /// <summary>
        /// The <see cref="System.Web.UI.WebControls.TableItemStyle"/> with no CSS Class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static TableItemStyle Empty = New(string.Empty);
	}
}
