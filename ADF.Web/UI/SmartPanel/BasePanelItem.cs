using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Objects;
using Adf.Web.UI.Styling;

namespace Adf.Web.UI
{
    /// <summary>
    /// Serves as the base class that defines the methods and properties common to all controls in the <see cref="Adf.Web.UI"/> namespace.
    /// </summary>
    public abstract class BasePanelItem : IPanelItem
    {
        /// <summary>
        /// Represent strongly typed list of <see cref="System.Web.UI.WebControls.WebControl"/> that can be used to hold 
        /// the display name of child controls within panel.
        /// </summary>
        protected List<WebControl> _labelControls = new List<WebControl>();

        /// <summary>
        /// Represent strongly typed list of <see cref="System.Web.UI.WebControls.WebControl"/> that can be used to hold 
        /// the object of child controls within panel.
        /// </summary>
        protected List<Control> _itemControls = new List<Control>();

        /// <summary>
        /// Represent the identifier of the pannel.
        /// </summary>
        protected string _id;

        /// <summary>
        /// Represent the visibility of controls within the panel
        /// </summary>
        protected bool _visible = true;

        private static readonly IStyler _styler = ObjectFactory.BuildUp<IStyler>("SmartPanelDefaultStyler");

        /// <summary>
        /// Gets the identifier of the pannel.
        /// </summary>
        /// <returns>The identifier of the pannel.</returns>
        public virtual string Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the array of label control which display text on panel.
        /// </summary>
        /// <returns>The array of label control which display text on panel.</returns>
        public virtual IList<WebControl> LabelControls { get { return _labelControls; } }

        public virtual Label Label { get { return LabelControls.Count > 0 ? LabelControls[0] as Label : null; } }

        /// <summary>
        /// Gets the array of item control which contains the child control names within the panel.
        /// </summary>
        /// <returns>The array of item control which contains the child control names within the panel.</returns>
        public virtual IList<Control> Controls { get { return _itemControls; } }

        /// <summary>
        /// Gets the style of a control within the panel.
        /// </summary>
        /// <returns>The style of a control within the panel.</returns>
        public virtual IStyler Styler { get { return _styler; } }

        /// <summary>
        /// Gets or sets the visibility of controls within the panel.
        /// </summary>
        /// <returns>The visibility of controls within the panel.</returns>
        public virtual bool Visible
        {
            get { return _visible; }
            set
            {
                foreach (var label in LabelControls)
                {
                    label.Visible = value;
                }

                foreach (var control in Controls)
                {
                    control.Visible = value;
                }
                _visible = value;
            }
        }

        public abstract bool Enabled { get; set; }

        /// <summary>
        /// Add the <see cref="System.Web.UI.WebControls.WebControl"/> to item control list.
        /// Used to hold the object of child controls within panel.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.WebControl"/> that defines the controls which will be added.</param>
        public void AddControl(WebControl control)
        {
            _itemControls.Add(control);
        }

        /// <summary>
        /// Add the <see cref="System.Web.UI.WebControls.WebControl"/> to label control list.
        /// Used to hold the label of child controls within panel.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.WebControl"/> that defines the controls which will be added.</param>
        public void AddLabelControl(WebControl control)
        {
            _labelControls.Add(control);
        }
    }
}
