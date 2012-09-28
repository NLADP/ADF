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
    public class DropDownListBinder : IControlBinder
    {
        private string[] types = { "ddl" };

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

            var ddl = control as DropDownList;
            if (ddl == null) return;

            ddl.Items.Clear();

            var items = BindManager.GetListFor(pi);
            
            if (p != null && p.Length > 0)
            {
                var typeResolver = p[0] as ObjectResolver;

                if (typeResolver != null)
                {
                    Func<IEnumerable> func;
                    if (typeResolver.TryGetValue(ddl.ID, out func))
                    {
                        items = func.Invoke();
                    }
                    else if (typeResolver.TryGetValue(value.GetType(), out func))
                    {
                        items = func.Invoke();
                    }
                }
            }
            
            var includeEmpty = !pi.IsNonEmpty();

            foreach (var item in PropertyHelper.GetCollectionWithDefault(value, includeEmpty, items))
            {
                ddl.Items.Add(new ListItem(item.Text, item.Value.ToString()) {Selected = item.Selected});
            }

            if (ddl.SelectedValue.IsNullOrEmpty() && !PropertyHelper.IsEmpty(value)) ddl.Items.Insert(0, new ListItem("<invalid value>") { Selected = true });
        }

        /// <summary>
        /// Populates the specified <see cref="System.Web.UI.WebControls.DropDownList"/> with 
        /// the specified array of objects using the specified parameters.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.DropDownList"/> to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public virtual void Bind(object control, object[] values, params object[] p)
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

            var ddl = control as DropDownList;
            if (ddl == null) return;

            ddl.Items.Clear();

            var selectval = string.Empty;
            if (p != null && p.Length > 0) selectval = (p[0] is IDomainObject) ? (p[0] as IDomainObject).Id.ToString() : p[0].ToString();             

            foreach (var value in values)
            {
                var text = (value is Enum) ? (value as Enum).GetDescription() : value.ToString();
                var val = (value is IDomainObject) ? (value as IDomainObject).Id.ToString() : text;

                var listItem = new ListItem(text, val);

                if (p != null && p.Length > 0)
                {
                    listItem.Selected = val == selectval;
                }

                ddl.Items.Add(listItem);
            }

            if (p != null && p.Length > 0)
            {
                if (ddl.SelectedValue.IsNullOrEmpty() && !p[0].ToString().IsNullOrEmpty())
                    ddl.Items.Insert(0, new ListItem("<invalid value>") { Selected = true });
            }
        }
    }
}