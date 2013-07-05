using System;
using System.Linq.Expressions;
using Adf.Core.Extensions;

namespace Adf.Web.UI
{
    /// <summary>
    /// Validate a text box that allows the user to input date and time value.
    /// </summary>
    public class ValidatedDateTextBoxItem : ValidatedPanelItem
    {
        /// <summary>
        /// A variable of <see cref="Adf.Web.UI.DateTextBoxItem"/> that define the panel item which will validated.
        /// </summary>
        protected DateTextBoxItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.ValidatedDateTextBoxItem"/> class with the specified <see cref="Adf.Web.UI.DateTextBoxItem"/>.
        /// </summary>
        /// <param name="panelItem">The <see cref="Adf.Web.UI.DateTextBoxItem"/> that define the panel item which will validated.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.DateTextBoxItem"/> is mandatory; else none.</param>
        public ValidatedDateTextBoxItem(DateTextBoxItem panelItem, string name, bool mandatory)
            : base(panelItem, name, mandatory)
        {
            item = panelItem;
        }

        /// <summary>
        /// Create a <see cref="Adf.Web.UI.SmartDateTextBox"/> with validation and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the <see cref="Adf.Web.UI.SmartDateTextBox"/> within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/> and set the identification of <see cref="Adf.Web.UI.SmartDateTextBox"/> control.</param>
        /// <param name="width">Width of <see cref="Adf.Web.UI.SmartDateTextBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the contents of <see cref="Adf.Web.UI.SmartDateTextBox"/> control can be changed or not.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.CheckBoxItem"/> is mandatory; else none.</param>
        /// <param name="format">The format to display data in <see cref="Adf.Web.UI.SmartDateTextBox"/> control.</param>
        /// <param name="description">Defines description to show the datetime format.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.ValidatedDateTextBoxItem"/> class.</returns>
        public static ValidatedDateTextBoxItem Create(string label, string name, int width, bool enabled, bool mandatory, string format = null, string description = null)
        {
            return new ValidatedDateTextBoxItem(DateTextBoxItem.Create(label, name, width, enabled, format, description), name, mandatory);
        }

        public static ValidatedDateTextBoxItem Create<T>(Expression<Func<T, object>> property, int width, bool enabled = true, bool mandatory = true, string format = null, string description = null)
        {
            return Create(property.GetMemberInfo().Name, property, width, enabled, mandatory, format, description);
        }

        public static ValidatedDateTextBoxItem Create<T>(string label, Expression<Func<T, object>> property, int width, bool enabled = true, bool mandatory = true, string format = null, string description = null)
        {
            return new ValidatedDateTextBoxItem(DateTextBoxItem.Create(label, property.GetControlName(), width, enabled, format, description), property.GetControlName(), mandatory);
        }
        /// <summary>
        /// Gets the <see cref="Adf.Web.UI.SmartDateTextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="Adf.Web.UI.SmartDateTextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.</returns>
        public virtual SmartDateTextBox DateTextBox
        {
            get { return item.DateTextBox; }
        }

        #region Overrides of BasePanelItem

        public override bool Enabled
        {
            get { return item.Enabled; }
            set { item.Enabled = value; }
        }

        #endregion
    }
}
