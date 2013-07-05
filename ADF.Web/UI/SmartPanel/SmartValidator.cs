using System;
using System.Web.UI.WebControls;
using Adf.Core.Styling;
using Adf.Web.Helper;

namespace Adf.Web.UI
{
    /// <summary>
    /// Performs smart validation on an input control.
    /// </summary>
    public class SmartValidator : CustomValidator
    {
        /// <summary>
        /// A variable to hold the name of panel item.
        /// </summary>
        public string Name { get; set; }
        public string Label { get; set; }

        /// <summary>
        /// Create a <see cref="Adf.Web.UI.SmartValidator"/> control and set its identifier by the specified panel item name.
        /// </summary>
        /// <param name="name">Set the identifier of <see cref="Adf.Web.UI.SmartValidator"/> control.</param>
        /// <returns>The <see cref="Adf.Web.UI.SmartValidator"/> control with ID value.</returns>
        public static SmartValidator Create(string name, string label)
        {
            return new SmartValidator { Name = name, ID = "val" + name, Label = label };
        }

        /// <summary>
        /// Raises the <see cref="System.Web.UI.Control.Init"/> event.
        /// Also set true for the associated input control validation.
        /// </summary>
        /// <param name="e">A <see cref="System.EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            IsValid = true;
        }

        /// <summary>
        /// Raises the <see cref="System.Web.UI.Control.OnPreRender"/> event.
        /// Also set the default style to smart panel item if the associated input control passes validation; otherwise error style.
        /// </summary>
        /// <param name="e">A <see cref="System.EventArgs"/> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var container = Parent.Parent ?? Page;

            StyleManager.Style(IsValid ? StyleType.Panel : StyleType.PanelError, ControlHelper.Find(container, "panelLabelItem_" + Name));
            StyleManager.Style(IsValid ? StyleType.Panel : StyleType.PanelError, ControlHelper.Find(container, "panelControlItem_" + Name));
        }
    }
}

