using System;
using System.Collections;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for a <see cref="System.Web.UI.WebControls.HyperLink"/>.
    /// Provides methods to bind the properties of a <see cref="System.Web.UI.WebControls.HyperLink"/> 
    /// to values.
    /// </summary>
    public class HyperLinkBinder : IControlBinder
    {
        /// <summary>
        /// The array of <see cref="System.Web.UI.WebControls.HyperLink"/> id prefixes that 
        /// support binding.
        /// </summary>
        private string[] types = { "hl" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.HyperLink"/> id prefixes that 
        /// support binding.
        /// </summary>
        /// <returns>
        /// The array of <see cref="System.Web.UI.WebControls.HyperLink"/> id prefixes.
        /// </returns>
        public IEnumerable Types
        {
            get { return types; }
        }

        /// <summary>
        /// Binds the properties of the specified <see cref="System.Web.UI.WebControls.HyperLink"/> 
        /// with the specified value.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.HyperLink"/>, the 
        /// properties of which are to bind to.</param>
        /// <param name="value">The value to bind.</param>
        /// <param name="pi">The Property of the <see cref="System.Web.UI.WebControls.HyperLink"/> 
        /// to bind to.
        /// Currently not being used.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {

            if (value == null) return;

            HyperLink hl = control as HyperLink;

            if (hl != null)
            {


                string[] parts = value.ToString().Split('|');

                if (parts.Length == 0) return;

                hl.Text = parts[0];
                hl.NavigateUrl = (parts.Length == 1) ? parts[0] : parts[1];
            }
        }

        /// <summary>
        /// Binds the properties of the specified <see cref="System.Web.UI.WebControls.HyperLink"/> 
        /// with the specified array of objects.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.HyperLink"/>, the 
        /// properties of which are to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, object[] values, params object[] p)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Binds the properties of the specified <see cref="System.Web.UI.WebControls.HyperLink"/> 
        /// with the specified list.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.HyperLink"/>, the 
        ///   properties of which are to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, IEnumerable values, params object[] p)
        {
            throw new NotSupportedException();
        }
    }
}