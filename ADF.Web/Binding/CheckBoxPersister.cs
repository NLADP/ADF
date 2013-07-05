using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Core.Domain;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a persister for a <see cref="System.Web.UI.WebControls.CheckBox"/>.
    /// Provides method to persist the value of the 'Checked' property of a 
    /// <see cref="System.Web.UI.WebControls.CheckBox"/>.
    /// </summary>
    public class CheckBoxPersister : IControlPersister
    {
        private string[] types = { "cbx" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.CheckBox"/> ID prefixes that 
        /// support persisting.
        /// </summary>
        /// <returns>
        /// The array of <see cref="System.Web.UI.WebControls.CheckBox"/> ID prefixes.
        /// </returns>
        public IEnumerable<string> Types
        {
            get { return types; }
        }

        /// <summary>
        /// Persists the value of the 'Checked' property of the specified 
        /// <see cref="System.Web.UI.WebControls.CheckBox"/> to a specified property of a specified object.
        /// </summary>
        /// <param name="bindableObject">The object where to persist.</param>
        /// <param name="pi">The property of the object where to persist.</param>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.CheckBox"/> control
        /// of which the 'Checked' property is to persist.</param>
        public virtual void Persist(object bindableObject, PropertyInfo pi, object control)
        {
            CheckBox c = control as CheckBox;

            if (c != null)
            {
                if (c.Enabled) PropertyHelper.SetValue(bindableObject, pi, (bool?)c.Checked);
            }
        }
    }
}