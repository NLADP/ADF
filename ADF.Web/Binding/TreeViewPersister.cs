using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.Core.Identity;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Web.UI;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a persister for a <see cref="System.Web.UI.WebControls.DropDownList"/>.
    /// Provides method to persist the selected value of a <see cref="System.Web.UI.WebControls.DropDownList"/>.
    /// </summary>
    public class TreeViewPersister : IControlPersister
    {
        readonly string[] types = { RenderItemType.TreeView.Prefix };

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
        public virtual void Persist(object bindableObject, PropertyInfo pi, object control)
        {
            if (pi == null) return;

            var tree = control as DropDownTreeView;
            if (tree == null) return;

            if (!tree.Enabled || !tree.Visible) return;

            var value = pi.GetValue(bindableObject, null);
            if (value is IDomainObject)
            {
                PropertyHelper.SetValue(bindableObject, pi, IdManager.New(tree.SelectedValue));
            }
            else if (value is Enum)
            {
                PropertyHelper.SetValue(bindableObject, pi, tree.SelectedValue);
            }
            else
            {
                PropertyHelper.SetValue(bindableObject, pi, tree.SelectedValue);
            }
        }
    }
}
