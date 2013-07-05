using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.Core.Identity;
using Adf.Core.Types;

namespace Adf.Web.Binding
{
	/// <summary>
    /// Represents a persister for a <see cref="System.Web.UI.WebControls.RadioButtonList"/>.
    /// Provides methods to persist the selected value of a <see cref="System.Web.UI.WebControls.RadioButtonList"/>.
	/// </summary>
	public class CheckBoxListPersister : IControlPersister
	{
		string[] types = {"cbl"};

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
			var checkBoxList = control as CheckBoxList;

			if (checkBoxList != null)
			{
			    if (!checkBoxList.Enabled || !checkBoxList.Visible) return;

			    var list = (IList) pi.GetValue(bindableObject, null);

                var argTypes = list.GetType().GetGenericArguments();

                if (argTypes.Length != 1) throw new InvalidOperationException("Please use a IList<T>");

			    list.Clear();

			    foreach (ListItem item in checkBoxList.Items)
			    {
			        if (item.Selected) list.Add(Converter.To(argTypes[0], item.Value));
			    }
			}
		}
	}
}
