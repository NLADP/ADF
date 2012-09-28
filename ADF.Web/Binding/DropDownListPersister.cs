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
    public class DropDownListPersister : IControlPersister
    {
        readonly string[] types = { "ddl" };

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public virtual void Persist(object bindableObject, PropertyInfo pi, object control)
        {
            var d = control as DropDownList;
            if (d == null) return;

            if (!d.Enabled || !d.Visible) return;

            var value = pi.GetValue(bindableObject, null);
            if (value is IDomainObject)
            {
                PropertyHelper.SetValue(bindableObject, pi, IdManager.New(d.SelectedValue));
            }
            else if (value is Enum)
            {
                PropertyHelper.SetValue(bindableObject, pi, d.SelectedValue);
            }
            else
            {
                PropertyHelper.SetValue(bindableObject, pi, d.SelectedValue);
            }
        }
    }
}
