using System.Collections;
using System.Linq;
using System.Reflection;
using Adf.Base.Domain;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.Core.Panels;
using Adf.Web.UI;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for a <see cref="System.Web.UI.WebControls.CheckBox"/>.
    /// Provides methods to bind the 'Checked' property of a 
    /// <see cref="System.Web.UI.WebControls.CheckBox"/> to a value.
    /// </summary>
    public class TreeViewBinder : IControlBinder
    {
        private readonly string[] types = {PanelItemType.TreeView.Prefix};

        public IEnumerable Types { get { return types; }}

        /// <summary>
        /// Binds the 'Checked' property of the specified <see cref="System.Web.UI.WebControls.CheckBox"/> 
        /// with the specified value.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.CheckBox"/>, the 'Checked' 
        /// property of which is to bind to.</param>
        /// <param name="value">The value to bind.</param>
        /// <param name="pi">The Property of the <see cref="System.Web.UI.WebControls.CheckBox"/> to bind to.
        /// Currently not being used.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            var tree = control as DropDownTreeView;
            if (tree == null) return; 
            
            var target = value as IDomainHierarchy;
            if (target == null) return;

            tree.Clear();

            tree.Bind(target.GetAll().Cast<IDomainHierarchy>().ToList(), target, true);

        }


        /// <summary>
        /// Binds the specified <see cref="System.Web.UI.WebControls.CheckBox"/> with the specified 
        /// array of objects.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.CheckBox"/> to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, object[] values, params object[] p)
        {
            var tree = control as DropDownTreeView;
            if (tree == null) return;

            tree.Clear();

            tree.Bind(values.Cast<IDomainHierarchy>().ToList(), null);

        }

        /// <summary>
        /// Binds the specified <see cref="System.Web.UI.WebControls.CheckBox"/> with the specified list.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.CheckBox"/> to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, IEnumerable values, params object[] p)
        {
            var tree = control as DropDownTreeView;
            if (tree == null) return;

            tree.Clear();

            tree.Bind(values.Cast<IDomainHierarchy>().ToList(), null);

        }

    }
}