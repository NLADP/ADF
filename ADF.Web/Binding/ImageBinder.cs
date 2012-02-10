using System;
using System.Collections;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Core.Extensions;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for the properties of a <see cref="System.Web.UI.WebControls.Label"/>.
    /// Provides methods to bind the properties of a <see cref="System.Web.UI.WebControls.Label"/> 
    /// with values.
    /// </summary>
    public class ImageBinder : IControlBinder
    {
        private readonly string[] types = { "img" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.Label"/> id prefixes that 
        /// support binding.
        /// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.Label"/> id prefixes.</value>
        public IEnumerable Types
        {
            get { return types; }
        }

        /// <summary>
        /// Binds the 'Text' property of the specified <see cref="System.Web.UI.WebControls.Label"/> 
        /// with the specified object.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.Label"/>, the 
        /// 'Text' property of which is to bind to.</param>
        /// <param name="value">The object to bind.</param>
        /// <param name="pi">The Property of the <see cref="System.Web.UI.WebControls.Label"/> to bind to.
        /// Currently not being used.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            if (value == null) return;

            var image = control as Image;

            if (image == null) return;
            
            if (value is Enum)
            {
                image.ImageUrl = (value as Enum).GetDescription();
            }
            else
            {
                image.ImageUrl = value.ToString();
            }
        }

        /// <summary>
        /// Binds the properties of the specified <see cref="System.Web.UI.WebControls.Label"/> 
        /// with the specified array of objects.
        /// Currently not supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.Label"/>, the 
        /// properties of which are to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, object[] values, params object[] p)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Binds the properties of the specified <see cref="System.Web.UI.WebControls.Label"/> 
        /// with the specified list.
        /// Currently not supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.Label"/>, the 
        ///   properties of which are to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, IEnumerable values, params object[] p)
        {
            throw new NotSupportedException();
        }
    }
}