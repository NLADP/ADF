using System;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents methods to create a space or blank.
    /// Used as a control in the panel.
    /// </summary>
    public class BlankItem : BasePanelItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.BlankItem"/> class with the specified blank label.
        /// </summary>
        /// <param name="l">The <see cref="System.Web.UI.WebControls.Label"/> that defines the blank text.</param>
        public BlankItem(Label l)
        {
            _itemControls.Add(l);
        }

        /// <summary>
        /// Add a blank or space into the item control of <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.BlankItem"/> class with the specified blank label.</returns>
        public static BlankItem Create()
        {
            Label blank = new Label();

            blank.Text = "&nbsp;";

            return new BlankItem(blank);
        }

        #region Overrides of BasePanelItem

        public override bool Enabled { get; set; }

        #endregion
    }
}
