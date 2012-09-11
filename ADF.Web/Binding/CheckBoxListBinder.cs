using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Base.Validation;
using Adf.Core.Binding;
using Adf.Core.Domain;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for the properties of a <see cref="System.Web.UI.WebControls.RadioButtonList"/>.
    /// Provides methods to bind the properties of a <see cref="System.Web.UI.WebControls.RadioButtonList"/> 
    /// with values.
    /// </summary>
    public class CheckBoxListBinder : IControlBinder
    {
        private readonly string[] types = { "cbl" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.RadioButtonList"/> id prefixes that 
        /// support binding.
        /// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.RadioButtonList"/> id prefixes.</value>
        public IEnumerable Types
        {
            get { return types; }
        }

        /// <summary>
        /// Populates the specified <see cref="System.Web.UI.WebControls.RadioButtonList"/> with 
        /// the specified collection of values using the specified parameters.
        /// </summary>
        /// <remarks>
        /// The supported types of items in the specified collection of values can be of type 
        /// Adf.Business.DomainObject, Adf.Business.SmartReferences.SmartReference&lt;T&gt;, 
        /// Adf.Core.Descriptor, System.Enum etc.
        /// </remarks>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.RadioButtonList"/> 
        /// to bind to.</param>
        /// <param name="value">The collection of values to bind.</param>
        /// <param name="pi">The property used to check whether an empty item will be included 
        /// in the <see cref="System.Web.UI.WebControls.RadioButtonList"/>. Currently not being 
        /// used.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            var checkBoxList = control as CheckBoxList;

            if (checkBoxList == null) return;

            checkBoxList.Items.Clear();

            IEnumerable<ValueItem> list;

            var items = BindManager.GetListFor(pi);

            if (value == null)
            {
                list = PropertyHelper.GetCollectionWithDefault(pi.PropertyType, false, items);
            }
            else
            {
                list = PropertyHelper.GetCollectionWithDefault(value, false, items);
            }

            foreach (ValueItem item in list)
            {
                var listitem = new ListItem(item.Text, item.Value.ToString()) {Selected = item.Selected};

                checkBoxList.Items.Add(listitem);
            }
        }

        /// <summary>
        /// Populates the specified <see cref="System.Web.UI.WebControls.DropDownList"/> with 
        /// the specified array of objects using the specified parameters.
        /// Currently not supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.RadioButtonList"/> 
        /// to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, object[] values, params object[] p)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Populates the specified <see cref="System.Web.UI.WebControls.RadioButtonList"/> with 
        /// the specified list using the specified parameters.
        /// Currently not supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.RadioButtonList"/> 
        ///   to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, IEnumerable values, params object[] p)
        {
            throw new NotSupportedException();
        }
    }
}