using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Displays a check box that allows the user to select a true or false condition.
    /// Used as a control in the panel.
    /// </summary>
    public class CheckBoxItem : BasePanelItem
    {
        private const string prefix = "cbx";

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.CheckBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.CheckBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public CheckBox CheckBox { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.CheckBoxItem"/> class with the specified label and checkbox.
        /// </summary>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the checkbox within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="checkBox">The <see cref="System.Web.UI.WebControls.CheckBox"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public CheckBoxItem(Label label, CheckBox checkBox)
        {
            CheckBox = checkBox;

            _labelControls.Add(label);

            _itemControls.Add(checkBox);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.CheckBox"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the checkbox within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.CheckBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.CheckBox"/> control is enabled or not.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.CheckBoxItem"/> class.</returns>
        public static CheckBoxItem Create(string label, string name, bool enabled)
        {
            Label l = new Label {Text = ResourceManager.GetString(label)};

            CheckBox checkBox = new CheckBox {ID = prefix + name, Enabled = enabled};

            return new CheckBoxItem(l, checkBox);
        }

        public static CheckBoxItem Create<T>(Expression<Func<T, object>> property, bool enabled = true)
        {
            return Create(property.GetMemberInfo().Name, property, enabled);
        }

        public static CheckBoxItem Create<T>(string label, Expression<Func<T, object>> property, bool enabled = true)
        {
            return Create(label, property.GetControlName(), enabled);
        }

        #region Overrides of BasePanelItem

        public override bool Enabled
        {
            get { return CheckBox.Enabled; }
            set { CheckBox.Enabled = value; }
        }

        #endregion
    }
}