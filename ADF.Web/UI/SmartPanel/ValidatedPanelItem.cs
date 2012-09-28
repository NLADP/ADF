using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Provides the functionality to validate the panel item.
    /// </summary>
    public abstract class ValidatedPanelItem : BasePanelItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.ValidatedPanelItem"/> class with no argument.
        /// </summary>
        protected ValidatedPanelItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.ValidatedPanelItem"/> class with the specified <see cref="Adf.Web.UI.BasePanelItem"/> and its name.
        /// </summary>
        /// <param name="panelItem">The items of <see cref="Adf.Web.UI.BasePanelItem"/> will validated.</param>
        /// <param name="name">The identifier of <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="mandatory">Add an asterix with item label if an input control is mandatory; else none.</param>
        protected ValidatedPanelItem(BasePanelItem panelItem, string name, bool mandatory)
        {
            _id = name;
            _labelControls.AddRange(panelItem.LabelControls);

            if (mandatory) _labelControls.Add(new Label {Text = "*", CssClass = "mandatory"});

            var label = _labelControls.OfType<Label>().FirstOrDefault();

            if (label != null) label.ID = "itemLabel_" + name;

            _itemControls.AddRange(panelItem.Controls);
            _itemControls.Add(SmartValidator.Create(name, (label != null) ? label.Text : string.Empty));
        }

        /// <summary>
        /// A variable of <see cref="System.Web.UI.WebControls.CustomValidator"/> for user-defined validation.
        /// </summary>
        protected CustomValidator validator;

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.CustomValidator"/> for user-defined validation on an input control.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.CustomValidator"/> for user-defined validation on an input control.</returns>
        public CustomValidator Validator
        {
            get { return validator; }
        }
    }
}
