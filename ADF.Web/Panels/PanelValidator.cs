using System;
using System.Collections.Generic;
using Adf.Core.Panels;
using Adf.Core.Styling;
using Adf.Web.Helper;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public class PanelValidator : SmartValidator
    {
        public string IDs { get; set; }

        /// <summary>
        /// Create a <see cref="Adf.Web.UI.SmartValidator"/> control and set its identifier by the specified panel item name.
        /// </summary>
        /// <param name="name">Set the identifier of <see cref="Adf.Web.UI.SmartValidator"/> control.</param>
        /// <returns>The <see cref="Adf.Web.UI.SmartValidator"/> control with ID value.</returns>
        public static PanelValidator Create(string name, string label, IEnumerable<string> ids)
        {
            return new PanelValidator { Name = name, ID = "val" + name, Label = label, IDs = string.Join(",", ids)};
        }

        public static PanelValidator Create(PanelItem item)
        {
            return Create(string.Format("{0}{1}", item.GetPropertyName(), item.Alias), item.Label, item.IDs);
        }

        protected override void OnPreRender(EventArgs e)
        {
            var container = Parent.Parent ?? Page;

            foreach (var id in IDs.Split(','))
            {
                StyleManager.Style(IsValid ? StyleType.Panel : StyleType.PanelError, ControlHelper.Find(container, id));
            }
        }
    }
}