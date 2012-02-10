using System;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a line control, which draws a horizontal line on a panel.
    /// </summary>
    public class LineItem : BasePanelItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.LineItem"/> class with the specified label.
        /// </summary>
        /// <param name="l">The <see cref="System.Web.UI.WebControls.Label"/> that defines a line to display within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public LineItem(Label l)
        {
            _itemControls.Add(l);
        }

        /// <summary>
        /// Create a horizontal line as <see cref="System.Web.UI.WebControls.Label"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.LineItem"/> class.</returns>
        public static LineItem Create()
        {
            Label line = new Label();

            line.Text = string.Format(@"<HR noshade>");

            return new LineItem(line);
        }

        #region Overrides of BasePanelItem

        public override bool Enabled { get; set; }

        #endregion
    }
}
