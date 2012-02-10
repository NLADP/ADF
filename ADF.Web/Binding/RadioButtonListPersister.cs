using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Web.Binding
{
	/// <summary>
    /// Represents a persister for a <see cref="System.Web.UI.WebControls.RadioButtonList"/>.
    /// Provides methods to persist the selected value of a <see cref="System.Web.UI.WebControls.RadioButtonList"/>.
	/// </summary>
	public class RadioButtonListPersister : IControlPersister
	{
		string[] types = {"rbl"};

		/// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.RadioButtonList"/> id prefixes 
        /// that support persistance.
		/// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.RadioButtonList"/> id prefixes.</value>
		public IEnumerable<string> Types
		{
			get { return types; }
		}

		/// <summary>
        /// Persists the selected value of the specified <see cref="System.Web.UI.WebControls.RadioButtonList"/>
        /// to the specified property of the specified object.
		/// </summary>
        /// <param name="bindableObject">The object where to persist.</param>
        /// <param name="pi">The property of the object where to persist.</param>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.RadioButtonList"/>,
        /// the selected value of which is to persist.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public virtual void Persist(object bindableObject, PropertyInfo pi, object control)
		{
			RadioButtonList rbl = control as RadioButtonList;

			if (rbl != null) 
			{
				if (!rbl.Enabled || !rbl.Visible) return;

				object value = pi.GetValue(bindableObject, null);
				if( value is IDomainObject )
				{
                    PropertyHelper.SetValue(bindableObject, pi, IdManager.New(rbl.SelectedValue));
				}
				else
				{
                    PropertyHelper.SetValue(bindableObject, pi, rbl.SelectedValue);
				}
			}
		}
	}
}
