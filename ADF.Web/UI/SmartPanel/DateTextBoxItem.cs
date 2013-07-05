using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Displays a text box that allows the user to input date and time value.
    /// Used as a control in the panel.
    /// </summary>
    public class DateTextBoxItem : BasePanelItem
    {
        private const string Prefix = "dtb";

        /// <summary>
        /// Gets the <see cref="Adf.Web.UI.SmartDateTextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="Adf.Web.UI.SmartDateTextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public SmartDateTextBox DateTextBox { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.DateTextBoxItem"/> class with the specified label and text box.
        /// </summary>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="textBox">The <see cref="System.Web.UI.WebControls.TextBox"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="description">The <see cref="System.Web.UI.WebControls.Label"/> that defines description to show the datetime format.</param>
        public DateTextBoxItem(Label label, SmartDateTextBox textBox, Label description)
        {
            DateTextBox = textBox;

            _labelControls.Add(label);

            _itemControls.Add(textBox);
            if (description != null)
            {
                _itemControls.Add(description);
            }
        }

        /// <summary>
        /// Create a <see cref="Adf.Web.UI.SmartDateTextBox"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the <see cref="Adf.Web.UI.SmartDateTextBox"/> within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="Adf.Web.UI.SmartDateTextBox"/> control.</param>
        /// <param name="width">Width of <see cref="Adf.Web.UI.SmartDateTextBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the contents of <see cref="Adf.Web.UI.SmartDateTextBox"/> control can be changed or not.</param>
        /// <param name="format">The format to display data in <see cref="Adf.Web.UI.SmartDateTextBox"/> control.</param>
        /// <param name="description">Defines description to show the datetime format.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.DateTextBoxItem"/> class.</returns>
        public static DateTextBoxItem Create(string label, string name, int width, bool enabled, string format = null, string description = null, int tabIndex = 0)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var descriptionLabel = new Label
                                       {
                                           Text = ResourceManager.GetString(description),
                                           CssClass = "SmartPanelDescription"
                                       };

            var box = new SmartDateTextBox
                          {
                              ID = Prefix + name,
                              TextMode = TextBoxMode.SingleLine,
                              Wrap = true,
                              ReadOnly = !enabled,
                              Width = new Unit(width, UnitType.Ex),
                              FormatDisplay = format
                          };

            box.PreRender += delegate { box.CssClass = (box.ReadOnly) ? ("TextBox ReadOnly") : ("TextBox"); };

            return new DateTextBoxItem(l, box, descriptionLabel);
        }

        public static DateTextBoxItem Create<T>(Expression<Func<T, object>> property, int width, bool enabled = true, string format = null, string description = null)
        {
            return Create(property.GetMemberInfo().Name, property, width, enabled, format, description);
        }

        public static DateTextBoxItem Create<T>(string label, Expression<Func<T, object>> property, int width, bool enabled = true, string format = null, string description = null)
        {
            return Create(label, property.GetControlName(), width, enabled, format, description);
        }

        #region Overrides of BasePanelItem

        public override bool Enabled
        {
            get { return !DateTextBox.ReadOnly; }
            set { DateTextBox.ReadOnly = !value; }
        }

        #endregion
    }
}
