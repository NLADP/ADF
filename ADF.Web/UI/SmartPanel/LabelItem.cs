using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a label control, which displays text on a panel.
    /// </summary>
    public class LabelItem : BasePanelItem
    {
        private const string prefix = "lbl";

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.LabelItem"/> class with the specified label and checkbox.
        /// </summary>
        /// <param name="itemLabel">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the label within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public LabelItem(Label itemLabel, Label label)
        {
            this._labelControls.Add(itemLabel);

            _itemControls.Add(label);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.Label"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="itemLabel">The display text of the checkbox within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.Label"/> control.</param>
        /// <param name="width">Width of <see cref="System.Web.UI.WebControls.Label"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.Label"/> control is enabled or not.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.LabelItem"/> class.</returns>
        public static LabelItem Create(string itemLabel, string name, int width, bool enabled = true)
        {
            var l = new Label {Text = ResourceManager.GetString(itemLabel)};

            var label = new Label
                            {
                                ID = prefix + name, 
                                Enabled = enabled, 
                                Width = new Unit(width, UnitType.Ex)
                            };

            return new LabelItem(l, label);
        }

        public static LabelItem Create<T>(Expression<Func<T, object>> property, int width, bool enabled = true)
        {
            return Create(property.GetMemberInfo().Name, property, width, enabled);
        }

        public static LabelItem Create<T>(string label, Expression<Func<T, object>> property, int width, bool enabled = true)
        {
            return Create(label, property.GetControlName(), width, enabled);
        }

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.Label"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.Label"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public override Label Label
        {
            get { return (_itemControls.Count > 0) ? _itemControls[0] as Label : null; }
        }

        #region Overrides of BasePanelItem

        public override bool Enabled { get; set; }

        #endregion
    }
}