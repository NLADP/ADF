using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Core.Domain;

namespace Adf.Web.Binding
{
	/// <summary>
    /// Represents the persister for a <see cref="System.Web.UI.WebControls.TextBox"/>.
    /// Provides method to persist the text of a <see cref="System.Web.UI.WebControls.TextBox"/>.
	/// </summary>
	public class TextBoxPersister : IControlPersister
	{
		string[] types = {"txt"};

		/// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.TextBox"/> id prefixes that 
        /// support persistance.
		/// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.TextBox"/> id prefixes.</value>
		public IEnumerable<string> Types
		{
			get { return types; }
		}

		/// <summary>
        /// Persists the text of the specified <see cref="System.Web.UI.WebControls.TextBox"/>
        /// to the specified property of the specified object.
		/// </summary>
		/// <remarks>For password textboxes, persistance will only occur when 
		/// the length of the password is greater than one character.</remarks>
        /// <param name="bindableObject">The object where to persist.</param>
        /// <param name="pi">The property of the object where to persist.</param>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.TextBox"/>, the 
        /// text of which is to persist.</param>
        public virtual void Persist(object bindableObject, PropertyInfo pi, object control)
		{
			TextBox t = control as TextBox;

			if (t != null)
			{
//                t.Text = HttpContext.Current.Server.HtmlEncode(t.Text);
//				if (t.TextMode == TextBoxMode.Password)
//				{
//                    if (t.Text.Length != 0 && t.Enabled)
//                        PropertyReflector.SetValue(bindableObject, pi, t.Text);
//				}
//				else
//				{
                    if (t.Enabled && t.Visible && !t.ReadOnly)
                        PropertyHelper.SetValue(bindableObject, pi, t.Text, CultureInfo.CurrentUICulture);
//				}
			}
		}
	}
}
