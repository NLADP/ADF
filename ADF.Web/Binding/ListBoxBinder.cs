using System;
using System.Collections;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Base.Validation;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for a <see cref="System.Web.UI.WebControls.DropDownList"/>.
    /// Provides methods to bind the values to the properties of a 
    /// <see cref="System.Web.UI.WebControls.DropDownList"/>.
    /// </summary>
    public class ListBoxBinder : IControlBinder
    {
        private readonly string[] types = { "lbx" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.DropDownList"/> id prefixes that support binding.
        /// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.DropDownList"/> id prefixes.</value>
        public IEnumerable Types
        {
            get { return types; }
        }

        /// <summary>
        /// Populates the specified <see cref="System.Web.UI.WebControls.DropDownList"/> with 
        /// the specified collection of values.
        /// </summary>
        /// <remarks>
        /// The supported types of items in the specified collection of values can be of type 
        /// Adf.Business.DomainObject, Adf.Core.Descriptor, System.Enum, SmartReference etc.
        /// </remarks>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.DropDownList"/> 
        /// to bind to.</param>
        /// <param name="value">The collection of values to bind.</param>
        /// <param name="pi">The property used to check whether an empty item will be included in the 
        /// <see cref="System.Web.UI.WebControls.DropDownList"/>.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            if (value == null) return;

            var listBox = control as ListBox;
            if (listBox == null) return;

            listBox.Items.Clear();
            
            var includeEmpty = !pi.IsNonEmpty();

            foreach (var item in PropertyHelper.GetValueItems(value, PropertyHelper.GetCollection(pi.PropertyType, value, includeEmpty), pi.PropertyType))
            {
                var listitem = new ListItem(item.Text, item.Value.ToString()) {Selected = item.Selected};

                listBox.Items.Add(listitem);
            }
        }

        /// <summary>
        /// Populates the specified <see cref="System.Web.UI.WebControls.DropDownList"/> with 
        /// the specified array of objects using the specified parameters.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.DropDownList"/> to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, object[] values, params object[] p)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Populates the specified <see cref="System.Web.UI.WebControls.DropDownList"/> with 
        /// the specified list using the specified parameters.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.DropDownList"/> 
        ///   to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public virtual void Bind(object control, IEnumerable values, params object[] p)
        {
            if (values == null) return;

            var listBox = control as ListBox;
            if (listBox == null) return;

            listBox.Items.Clear();

            foreach (var value in values)
            {
                var text = (value is Enum) ? (value as Enum).GetDescription() : value.ToString();

                //ListItem listItem = new ListItem(value.ToString());
                var listItem = new ListItem(text);

                if (p != null && p.Length > 0)
                {
                    listItem.Selected = text == p[0] as string;
                }

                listBox.Items.Add(listItem);
            }
        }
    }
}
