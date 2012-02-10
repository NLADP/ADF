using System;
using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Displays a label that allows the user to show date and time value.
    /// Used as a control in the panel.
    /// </summary>
    public class DateLabelItem : BasePanelItem
    {
        private const string prefix = "dlb";

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.DateLabelItem"/> class with the specified label and item.
        /// </summary>
        /// <param name="itemLabel">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the label within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public DateLabelItem(Label itemLabel, Label label)
        {
            _labelControls.Add(itemLabel);
            _itemControls.Add(label);
        }

        /// <summary>
        /// Create a <see cref="Adf.Web.UI.SmartDateLabel"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="itemLabel">The display text of the <see cref="Adf.Web.UI.SmartDateLabel"/> within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="Adf.Web.UI.SmartDateLabel"/> control.</param>
        /// <param name="width">Width of <see cref="Adf.Web.UI.SmartDateLabel"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="Adf.Web.UI.SmartDateLabel"/> control is enabled or not.</param>
        /// <param name="format">The format to display data in <see cref="Adf.Web.UI.SmartDateLabel"/> control.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.DateLabelItem"/> class.</returns>
        public static DateLabelItem Create(string itemLabel, string name, int width, bool enabled, string format)
        {
            var l = new Label {Text = ResourceManager.GetString(itemLabel)};

            var label = new SmartDateLabel
                            {
                                ID = prefix + name,
                                Enabled = enabled,
                                Width = new Unit(width, UnitType.Ex),
                                FormatDisplay = format
                            };

            return new DateLabelItem(l, label);
        }

        /// <summary>
        /// Gets the <see cref="Adf.Web.UI.SmartDateLabel"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="Adf.Web.UI.SmartDateLabel"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public new SmartDateLabel Label
        {
            get { return (_itemControls.Count > 0) ? _itemControls[0] as SmartDateLabel : null; }
        }

        #region Overrides of BasePanelItem

        public override bool Enabled { get; set; }

        #endregion
    }
}
