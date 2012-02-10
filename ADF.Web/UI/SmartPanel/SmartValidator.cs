using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Objects;
using Adf.Web.Helper;
using Adf.Web.UI.Styling;

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
        protected string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.SmartValidator"/> class with the specified panel item name.
        /// </summary>
        /// <param name="name">Panel item name used for validation.</param>
        protected SmartValidator(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Create a <see cref="Adf.Web.UI.SmartValidator"/> control and set its identifier by the specified panel item name.
        /// </summary>
        /// <param name="name">Set the identifier of <see cref="Adf.Web.UI.SmartValidator"/> control.</param>
        /// <returns>The <see cref="Adf.Web.UI.SmartValidator"/> control with ID value.</returns>
        public static CustomValidator Create(string name)
        {
            return new SmartValidator(name) {ID = "val" + name};
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

        private static readonly IStyler DefaultStyler = ObjectFactory.BuildUp<IStyler>("SmartPanelDefaultStyler");
        private static readonly IStyler ErrorStyler = ObjectFactory.BuildUp<IStyler>("SmartPanelErrorStyler");

        /// <summary>
        /// Raises the <see cref="System.Web.UI.Control.OnPreRender"/> event.
        /// Also set the default style to smart panel item if the associated input control passes validation; otherwise error style.
        /// </summary>
        /// <param name="e">A <see cref="System.EventArgs"/> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //TODO beter systeem bedenken, Validator hoort niks met Parents te maken te hebben.
            Control findInContainerControl = Page;
            if (Parent.Parent != null)
            {
                findInContainerControl = Parent.Parent;
            }
            WebControl c = ControlHelper.Find(findInContainerControl, "panelLabelItem_" + name) as WebControl;

            if (c != null)
            {
                IStyler s = IsValid ? DefaultStyler : ErrorStyler;

                s.SetStyles(c);
            }
            
            c = ControlHelper.Find(findInContainerControl, "panelControlItem_" + name) as WebControl;

            if (c != null)
            {
                IStyler s = IsValid ? DefaultStyler : ErrorStyler;

                s.SetStyles(c);
            }
        }

    }
}

