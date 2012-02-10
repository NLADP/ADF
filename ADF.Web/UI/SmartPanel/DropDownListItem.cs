using System;
using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a control that allows the user to select a single item from a drop-down list.
    /// Used as a control in the panel.
    /// </summary>
    public class DropDownListItem : BasePanelItem
    {
        private const string Prefix = "ddl";

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.DropDownList"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.DropDownList"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public DropDownList DropDownList { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.DropDownListItem"/> class with the specified label and drop-down list.
        /// </summary>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the drop-down list within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="dropDownList">The <see cref="System.Web.UI.WebControls.DropDownList"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public DropDownListItem(Label label, DropDownList dropDownList)
        {
            DropDownList = dropDownList;

            _labelControls.Add(label);

            _itemControls.Add(dropDownList);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.DropDownList"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the drop-down list within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.DropDownList"/> control.</param>
        /// <param name="width">Width of <see cref="System.Web.UI.WebControls.DropDownList"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.DropDownList"/> control is enabled or not.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.DropDownListItem"/> class.</returns>
        public static DropDownListItem Create(string label, string name, int width, bool enabled = true)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var dropDownList = new DropDownList
                                   {
                                       ID = Prefix + name,
                                       Enabled = enabled,
                                       Width = new Unit(width, UnitType.Ex)
                                   };

            dropDownList.PreRender += delegate { dropDownList.CssClass = (dropDownList.Enabled) ? ("DropDownList") : ("DropDownList ReadOnly"); };

            return new DropDownListItem(l, dropDownList);
        }

        #region Overrides of BasePanelItem

        public override bool Enabled
        {
            get { return DropDownList.Enabled; }
            set { DropDownList.Enabled = value; }
        }

        #endregion
    }
}
