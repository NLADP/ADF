using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Web.UI;

namespace Adf.Web.Helper
{
    /// <summary>
    /// Represents a utility class for a <see cref="System.Web.UI.Control"/>.
    /// Provides methods to find a <see cref="System.Web.UI.Control"/>, list of 
    /// <see cref="System.Web.UI.Control"/>s, to get the value of a control etc.
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        /// Finds a <see cref="System.Web.UI.Control"/> with the specified string as its ID and 
        /// returns the same. The <see cref="System.Web.UI.Control"/> will be either the specified
        /// <see cref="System.Web.UI.Control"/> or any of its child <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <remarks>
        /// Returns null if no control is found.
        /// </remarks>
        /// <param name="control">The container <see cref="System.Web.UI.Control"/> to check.</param>
        /// <param name="id">The ID of the <see cref="System.Web.UI.Control"/> to find.</param>
        /// <returns>The <see cref="System.Web.UI.Control"/> with the specified ID.</returns>
        public static Control Find(Control control, string id)
        {
            //TODO: Think about improved stopping conditions, e.g. based on IComponent
            if (control != null)
            {
                if (control.ID == id)
                    return control;

                var c = control.FindControl(id);
                // RJB 13-09-2007: RadioButtonList.FindControl always returns the RadioButtonList object
                if (c != null && c.ID == id)
                    return c;

                return (from Control co in control.Controls select Find(co, id)).FirstOrDefault(co2 => co2 != null);
            }
            return null;
        }

        public static Dictionary<string, Control> GetControls(this Control root, Dictionary<string, Control> dictionary = null)
        {
            if (dictionary == null) dictionary = new Dictionary<string, Control>();

            if (root.ID != null && !dictionary.ContainsKey(root.ID))
            {
                dictionary.Add(root.ID, root);
            }

            foreach (Control control in root.Controls)
            {
                control.GetControls(dictionary);
            }

            return dictionary;
        }

        /// <summary>
        /// Gets the string value of the specified <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.Control"/> to get the value of.</param>
        /// <returns>The string value of the <see cref="System.Web.UI.Control"/>.</returns>
        public static string GetValue(Control control)
        {
            Label l = control as Label;
            if (l != null) return l.Text;

            TextBox t = control as TextBox;
            if (t != null) return t.Text;

            CheckBox cb = control as CheckBox;
            if (cb != null) return cb.Text;

            DateTextBox dt = control as DateTextBox;
            if (dt != null) return dt.Date.ToString();

            RadioButtonList rbl = control as RadioButtonList;
            if (rbl != null) return rbl.SelectedValue;

            DropDownList ddl = control as DropDownList;
            if (ddl != null) return ddl.SelectedValue;

            Calendar c = control as Calendar;
            if (c != null) return c.SelectedDate.ToString();

            return string.Empty;
        }

        /// <summary>
        /// Searches the specified container <see cref="System.Web.UI.Control"/> for Controls 
        /// of the specified <see cref="System.Type"/> or any of its subtypes.
        /// </summary>
        /// <param name="container">The container <see cref="System.Web.UI.Control"/> to search.</param>
        /// <param name="controlType">The <see cref="System.Type"/> to check against.</param>
        /// <returns>The list of <see cref="System.Web.UI.Control"/>s of the specified 
        /// <see cref="System.Type"/> or any of its subtypes.</returns>
        public static List<Control> List(Control container, Type controlType)
        {
            var list = new List<Control>();

            return List(list, container, controlType);
        }

        /// <summary>
        /// Searches the specified container <see cref="System.Web.UI.Control"/> for Controls 
        /// of the specified <see cref="System.Type"/> or any of its subtypes.
        /// The matching Controls are added to the specified list and returned.
        /// </summary>
        /// <param name="l">The list of <see cref="System.Web.UI.Control"/>s to add to.</param>
        /// <param name="container">The container <see cref="System.Web.UI.Control"/> to search.</param>
        /// <param name="controlType">The <see cref="System.Type"/> to check against.</param>
        /// <returns>The list of <see cref="System.Web.UI.Control"/>s of the specified 
        /// <see cref="System.Type"/> or any of its subtypes.</returns>
        private static List<Control> List(List<Control> l, Control container, Type controlType)
        {
            foreach (Control c in container.Controls)
            {
                if (c.GetType() == controlType || c.GetType().IsSubclassOf(controlType))
                    l.Add(c);

                List(l, c, controlType);
            }

            return l;
        }
    }
}
