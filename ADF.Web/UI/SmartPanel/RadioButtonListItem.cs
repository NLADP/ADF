using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a list control that encapsulates a group of radio button controls. 
    /// Used as a control in the panel.
    /// </summary>
    public class RadioButtonListItem : BasePanelItem
    {
        private const string prefix = "rbl";

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.RadioButtonList"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.RadioButtonList"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public RadioButtonList List { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.RadioButtonListItem"/> class with the specified label and radio button list.
        /// </summary>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the radio button list within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="list">The <see cref="System.Web.UI.WebControls.RadioButtonList"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public RadioButtonListItem(Label label, RadioButtonList list)
        {
            List = list;

            _labelControls.Add(label);

            _itemControls.Add(list);
        }

        /// <summary>
        /// Create a horizontal <see cref="System.Web.UI.WebControls.RadioButtonList"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// Items of a list are displayed horizontally in rows from left to right, then top to bottom, until all items are rendered.
        /// </summary>
        /// <param name="label">The display text of the radio button list within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.RadioButtonList"/>.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.RadioButtonList"/> control is enabled or not.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.RadioButtonListItem"/> class.</returns>
        public static RadioButtonListItem Create(string label, string name, bool enabled)
        {
            return Create(label, name, enabled, RepeatDirection.Horizontal);
        }

        /// <summary>
        /// Create a horizontal or vertical <see cref="System.Web.UI.WebControls.RadioButtonList"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the radio button list within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.RadioButtonList"/>.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.RadioButtonList"/> control is enabled or not.</param>
        /// <param name="direction">Specifies the direction in which items of a list control are displayed.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.RadioButtonListItem"/> class.</returns>
        public static RadioButtonListItem Create(string label, string name, bool enabled, RepeatDirection direction)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var list = new RadioButtonList
                           {
                               ID = prefix + name, 
                               Enabled = enabled, 
                               RepeatDirection = direction
                           };

            return new RadioButtonListItem(l, list);
        }

        #region Overrides of BasePanelItem

        public override bool Enabled
        {
            get { return List.Enabled; }
            set { List.Enabled = value; }
        }

        #endregion
    }
}
