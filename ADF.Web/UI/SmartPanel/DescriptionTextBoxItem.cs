using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Displays a text box that allows the user to entry multiple lines of text.
    /// Used as a control in the panel.
    /// </summary>
    public class DescriptionTextBoxItem : ValidatedTextBoxItem
    {
        /// <summary>
        /// Create a new variable of <see cref="Adf.Web.UI.ValidatedTextBoxItem"/> to validate text.
        /// </summary>
        new protected ValidatedTextBoxItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.DescriptionTextBoxItem"/> class with the specified label and <see cref="Adf.Web.UI.TextBoxItem"/>.
        /// </summary>
        /// <param name="panelItem">The <see cref="Adf.Web.UI.TextBoxItem"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="Adf.Web.UI.TextBoxItem"/> control.</param>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="mandatory">The text box will filled or not.</param>
        public DescriptionTextBoxItem(TextBoxItem panelItem, string name, Label label, bool mandatory)
            : base(panelItem, name, mandatory)
        {
            _itemControls.Add(label);

            item = this;
        }

        /// <summary>
        /// Create a <see cref="Adf.Web.UI.DescriptionTextBoxItem"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the <see cref="Adf.Web.UI.DescriptionTextBoxItem"/> within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="Adf.Web.UI.DescriptionTextBoxItem"/> control.</param>
        /// <param name="width">Width of <see cref="Adf.Web.UI.DescriptionTextBoxItem"/> control.</param>
        /// <param name="enabled">A value indicating whether the contents of <see cref="Adf.Web.UI.DescriptionTextBoxItem"/> control can be changed or not.</param>
        /// <param name="mandatory">The text box will filled or not.</param>
        /// <param name="description">Defines description to show the format of data which will entered.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.DescriptionTextBoxItem"/> class.</returns>
        public static DescriptionTextBoxItem Create(string label, string name, int width, bool enabled, bool mandatory, string description)
        {
            var descriptionLabel = new Label
                                       {
                                           Text = ResourceManager.GetString(description),
                                           CssClass = "SmartPanelDescription"
                                       };

            return new DescriptionTextBoxItem(TextBoxItem.Create(label, name, width, enabled), name, descriptionLabel, mandatory);
        }

        /// <summary>
        /// Gets the text of a base class <see cref="Adf.Web.UI.ValidatedTextBoxItem"/>.
        /// </summary>
        /// <returns>The text of a base class <see cref="Adf.Web.UI.ValidatedTextBoxItem"/>.</returns>
        public override TextBox TextBox
        {
            get { return item.TextBox; }
        }
    }
}