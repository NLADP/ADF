using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a persister for a <see cref="System.Web.UI.WebControls.DropDownList"/>.
    /// Provides method to persist the selected value of a <see cref="System.Web.UI.WebControls.DropDownList"/>.
    /// </summary>
    public class ListBoxPersister : IControlPersister
    {
        readonly string[] types = { "lbx" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.DropDownList"/> id prefixes 
        /// that support persistance.
        /// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.DropDownList"/> id prefixes.</value>
        public IEnumerable<string> Types
        {
            get { return types; }
        }

        /// <summary>
        /// Persists the selected value of the specified <see cref="System.Web.UI.WebControls.DropDownList"/>
        /// to the specified property of the specified object.
        /// </summary>
        /// <param name="bindableObject">The object where to persist.</param>
        /// <param name="pi">The property of the object where to persist.</param>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.DropDownList"/>,
        /// the selected value of which is to persist.</param>
        public void Persist(object bindableObject, PropertyInfo pi, object control)
        {
            var listBox = control as ListBox;

            if (listBox == null)
                return;

            if (!listBox.Enabled || !listBox.Visible)
                return;

            object value = pi.GetValue(bindableObject, null);
            if (value is IDomainObject)
            {
                PropertyHelper.SetValue(bindableObject, pi, IdManager.New(listBox.SelectedValue));
            }
            else if (value is Enum)
            {
                PropertyHelper.SetValue(bindableObject, pi, listBox.SelectedValue);
            }
            else
            {
                PropertyHelper.SetValue(bindableObject, pi, listBox.SelectedValue);
            }
        }
    }
}
