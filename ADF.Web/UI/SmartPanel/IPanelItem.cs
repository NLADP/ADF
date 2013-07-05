using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Defines the properties and methode that can be used to configure <see cref="System.Web.UI.WebControls.WebControl"/>. 
    /// Also configure style and visibility for a panel.
    /// </summary>
    public interface IPanelItem
    {
        /// <summary>
        /// Gets the identifier of the pannel.
        /// </summary>
        /// <returns>The identifier of the pannel.</returns>
        string Id { get; }

        /// <summary>
        /// Gets the array of label control which display text on panel.
        /// </summary>
        /// <returns>The array of label control which display text on panel.</returns>
        IList<WebControl> LabelControls { get; }

        /// <summary>
        /// Gets the array of item control which contains the child control names within the panel.
        /// </summary>
        /// <returns>The array of item control which contains the child control names within the panel.</returns>
        IList<Control> Controls { get; }

        /// <summary>
        /// Gets or sets the visibility of controls within the panel.
        /// </summary>
        /// <returns>The visibility of controls within the panel.</returns>
        bool Visible { get; set; }

        bool Enabled { get; set; }

        /// <summary>
        /// Add the controls of <see cref="System.Web.UI.WebControls.WebControl"/> to panel.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.WebControl"/> that defines the controls which will be added.</param>
        void AddControl(WebControl control);
    }
}
