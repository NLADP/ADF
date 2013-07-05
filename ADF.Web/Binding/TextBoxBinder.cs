using System;
using System.Collections;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;
using Adf.Base.Validation;
using Adf.Core.Binding;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Web.UI;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for the properties of a <see cref="System.Web.UI.WebControls.TextBox"/>.
    /// Provides methods to bind the properties of a <see cref="System.Web.UI.WebControls.TextBox"/> 
    /// with values.
    /// </summary>
    public class TextBoxBinder : IControlBinder
    {
        private readonly string[] types = { RenderItemType.EditableText.Prefix };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.TextBox"/> id prefixes that 
        /// support binding.
        /// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.TextBox"/> id prefixes.</value>
        public IEnumerable Types
        {
            get { return types; }
        }

        /// <summary>
        /// Binds the text of the specified <see cref="System.Web.UI.WebControls.TextBox"/> 
        /// with the specified object. A property is specified to determine whether the 'MaxLength'
        /// property of the specified <see cref="System.Web.UI.WebControls.TextBox"/> will be set.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.TextBox"/>, the text
        /// of which is to bind to.</param>
        /// <param name="value">The object to bind.</param>
        /// <param name="pi">The Property to determine whether the 'MaxLength' property of the specified 
        /// <see cref="System.Web.UI.WebControls.TextBox"/> will be set.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            var t = control as TextBox;

            if (t == null) return;

            if (pi != null)
            {
                var attributes = (MaxLengthAttribute[]) pi.GetCustomAttributes(typeof (MaxLengthAttribute), false);
                if (attributes.Length > 0)
                    t.MaxLength = attributes[0].Length;
            }

            t.Text = FormatHelper.ToString(value);
        }

        /// <summary>
        /// Binds the properties of the specified <see cref="System.Web.UI.WebControls.TextBox"/> 
        /// with the specified array of objects.
        /// Currently not supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.TextBox"/>, the 
        /// properties of which are to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, object[] values, params object[] p)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Binds the properties of the specified <see cref="System.Web.UI.WebControls.TextBox"/> 
        /// with the specified list.
        /// Currently not supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.TextBox"/>, the 
        ///   properties of which are to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, IEnumerable values, params object[] p)
        {
            throw new NotSupportedException();
        }
    }
}
