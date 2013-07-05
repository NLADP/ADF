using System;
using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Display the title of a panel. Used as a control in the panel.
    /// </summary>
    public class TitleItem : BasePanelItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.TitleItem"/> class with the specified item.
        /// </summary>
        /// <param name="l">The <see cref="System.Web.UI.WebControls.Label"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public TitleItem(Label l)
        {
            _itemControls.Add(l);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.Label"/> to display the title of a panel and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="title">The title of a panel within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.TitleItem"/> class.</returns>
        public static TitleItem Create(string title)
        {
            var l = new Label
                        {
                            Text = ResourceManager.GetString(title), 
                            CssClass = "TitleItem"
                        };

            return new TitleItem(l);
        }

        #region Overrides of BasePanelItem

        public override bool Enabled { get; set; }

        #endregion
    }
}
