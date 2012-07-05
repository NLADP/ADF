using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;

namespace Adf.Web.UI
{
    /// <summary>
    /// Validate a list control that encapsulates a group of radio button controls.
    /// </summary>
    public class ValidatedRadioButtonListItem : ValidatedPanelItem
    {
        /// <summary>
        /// A variable of <see cref="Adf.Web.UI.RadioButtonListItem"/> that define the panel item which will validated.
        /// </summary>
        protected RadioButtonListItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.ValidatedRadioButtonListItem"/> class with the specified <see cref="Adf.Web.UI.RadioButtonListItem"/>.
        /// </summary>
        /// <param name="item">The <see cref="Adf.Web.UI.RadioButtonListItem"/> that define the panel item which will validated.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.RadioButtonListItem"/> is mandatory; else none.</param>
        public ValidatedRadioButtonListItem(RadioButtonListItem item, string name, bool mandatory)
            : base(item, name, mandatory)
        {
            this.item = item;
        }

        /// <summary>
        /// Create a horizontal <see cref="System.Web.UI.WebControls.RadioButtonList"/> with validation and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// Items of a list are displayed horizontally in rows from left to right, then top to bottom, until all items are rendered.
        /// </summary>
        /// <param name="label">The display text of the radio button list within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/> and set the identification of <see cref="System.Web.UI.WebControls.RadioButtonList"/>.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.RadioButtonList"/> control is enabled or not.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.RadioButtonListItem"/> is mandatory; else none.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.ValidatedRadioButtonListItem"/> class.</returns>
        public static ValidatedRadioButtonListItem Create(string label, string name, bool enabled, bool mandatory)
        {
            return new ValidatedRadioButtonListItem(RadioButtonListItem.Create(label, name, enabled), name, mandatory);
        }

        /// <summary>
        /// Create a horizontal or vertical <see cref="System.Web.UI.WebControls.RadioButtonList"/> with validation and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the radio button list within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/> and set the identification of <see cref="System.Web.UI.WebControls.RadioButtonList"/>.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.RadioButtonList"/> control is enabled or not.</param>
        /// <param name="mandatory">Add an asterix with item label if <see cref="Adf.Web.UI.RadioButtonListItem"/> is mandatory; else none.</param>
        /// <param name="direction">Specifies the direction in which items of a list control are displayed.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.ValidatedRadioButtonListItem"/> class.</returns>
        public static ValidatedRadioButtonListItem Create(string label, string name, bool enabled, bool mandatory, RepeatDirection direction)
        {
            return new ValidatedRadioButtonListItem(RadioButtonListItem.Create(label, name, enabled, direction), name, mandatory);
        }

        public static ValidatedRadioButtonListItem Create<T>(Expression<Func<T, object>> property, bool enabled = true, bool mandatory = true, RepeatDirection direction = RepeatDirection.Horizontal)
        {
            return Create(property.GetMemberInfo().Name, property, enabled, mandatory, direction);
        }

        public static ValidatedRadioButtonListItem Create<T>(string label, Expression<Func<T, object>> property, bool enabled = true, bool mandatory = true, RepeatDirection direction = RepeatDirection.Horizontal)
        {
            return new ValidatedRadioButtonListItem(RadioButtonListItem.Create(label, property.GetControlName(), enabled, direction), property.GetControlName(), mandatory);
        }

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.RadioButtonList"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.RadioButtonList"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.</returns>
        public RadioButtonList List
        {
            get { return item.List; }
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
