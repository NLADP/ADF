using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Web.UI
{
    /// <summary>
    /// Validate a text box control for user input.
    /// </summary>
    public class ValidatedTextBoxItem : ValidatedPanelItem
    {
        /// <summary>
        /// A variable of <see cref="Adf.Web.UI.TextBoxItem"/> that define the panel item which will validated.
        /// </summary>
        protected TextBoxItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.ValidatedTextBoxItem"/> class with the specified <see cref="Adf.Web.UI.TextBoxItem"/>.
        /// </summary>
        /// <param name="panelItem">The <see cref="Adf.Web.UI.TextBoxItem"/> that define the panel item which will validated.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.TextBoxItem"/> is mandatory; else none.</param>
        public ValidatedTextBoxItem(TextBoxItem panelItem, string name, bool mandatory)
            : base(panelItem, name, mandatory)
        {
            item = panelItem;
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.TextBox"/> with validation for single-line entry and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/> and set the identification of <see cref="System.Web.UI.WebControls.TextBox"/> control.</param>
        /// <param name="width">Width of <see cref="System.Web.UI.WebControls.TextBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.TextBox"/> control is enabled or not.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.TextBoxItem"/> is mandatory; else none.</param>
        /// <param name="height">Height (0 = singleline</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.ValidatedTextBoxItem"/> class.</returns>
        public static ValidatedTextBoxItem Create(string label, string name, int width, bool enabled, bool mandatory, int height = 0, string infoText = null, TextBoxMode textBoxMode = TextBoxMode.SingleLine)
        {
            return new ValidatedTextBoxItem(TextBoxItem.Create(label, name, width, enabled, height, infoText: infoText, textBoxMode: textBoxMode), name, mandatory);
        }

        public static ValidatedTextBoxItem Create<T>(Expression<Func<T, object>> property, int width, bool enabled = true, bool mandatory = true, int height = 0, string infoText = null)
        {
            return Create(property.GetMemberInfo().Name, property, width, enabled, mandatory, height, infoText);
        }

        public static ValidatedTextBoxItem Create<T>(string label, Expression<Func<T, object>> property, int width, bool enabled = true, bool mandatory = true, int height = 0, string infoText = null, int tabIndex = 0)
        {
            return new ValidatedTextBoxItem(TextBoxItem.Create(label, property.GetControlName(), width, enabled, height, infoText: infoText), property.GetControlName(), mandatory);
        }

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.TextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.TextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.</returns>
        public virtual TextBox TextBox
        {
            get { return item.TextBox; }
        }

        public override bool Enabled
        {
            get { return item.Enabled; }
            set { item.Enabled = value; }
        }
    }
}
