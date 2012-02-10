using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;

namespace Adf.Web.UI
{
    /// <summary>
    /// Validate a check box that allows the user to select a true or false condition.
    /// </summary>
    public class ValidatedCheckBoxItem : ValidatedPanelItem
    {
        /// <summary>
        /// A variable of <see cref="Adf.Web.UI.CheckBoxItem"/> that define the panel item which will validated.
        /// </summary>
        protected CheckBoxItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.ValidatedCheckBoxItem"/> class with the specified <see cref="Adf.Web.UI.CheckBoxItem"/>.
        /// </summary>
        /// <param name="panelItem">The <see cref="Adf.Web.UI.CheckBoxItem"/> that define the panel item which will validated.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.CheckBoxItem"/> is mandatory; else none.</param>
        public ValidatedCheckBoxItem(CheckBoxItem panelItem, string name, bool mandatory)
            : base(panelItem, name, mandatory)
        {
            item = panelItem;
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.CheckBox"/> with validation and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the checkbox within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/> and set the identification of <see cref="System.Web.UI.WebControls.CheckBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.CheckBox"/> control is enabled or not.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.CheckBoxItem"/> is mandatory; else none.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.ValidatedCheckBoxItem"/> class.</returns>
        public static ValidatedCheckBoxItem Create(string label, string name, bool enabled, bool mandatory)
        {
            return new ValidatedCheckBoxItem(CheckBoxItem.Create(label, name, enabled), name, mandatory);
        }

        public static ValidatedCheckBoxItem Create<T>(Expression<Func<T, object>> property, bool enabled = true, bool mandatory = true)
        {
            return Create(property.GetExpressionMember().Name, property, enabled, mandatory);
        }

        public static ValidatedCheckBoxItem Create<T>(string label, Expression<Func<T, object>> property, bool enabled = true, bool mandatory = true)
        {
            return new ValidatedCheckBoxItem(CheckBoxItem.Create(label, property.GetControlName(), enabled), property.GetControlName(), mandatory);
        }


        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.CheckBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.CheckBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.</returns>
        public virtual CheckBox CheckBox
        {
            get { return item.CheckBox; }
        }

        public override bool Enabled
        {
            get { return item.Enabled; }
            set { item.Enabled = value; }
        }
    }
}

