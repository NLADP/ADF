using System.Collections;
using System.Reflection;

namespace Adf.Core.Binding
{
    /// <summary>
    /// Defines a generalized Bind method that all controls must implement to bind 
    /// its state to the business layer.
    /// </summary>
    
    public interface IControlBinder
    {
        /// <summary>
        /// Property definition for the types of a <see cref="IControlBinder"/>.
        /// </summary>
        IEnumerable Types { get; }
        
        /// <summary>
        /// Binds a single object to a control.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="value">Value for property from original object to bind.</param>
        /// <param name="pi">Property from original object to bind.</param>
        /// <param name="p">Optional list of parameters.</param>
        void Bind(object control, object value, PropertyInfo pi, params object[] p);

        /// <summary>
        /// Binding a List of values to a control.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="values">List of values to bind to control.</param>
        /// <param name="p">Optional list of parameters.</param>
        void Bind(object control, object[] values, params object[] p);

        /// <summary>
        /// Binding an Object array to a control.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="values">Values to bind.</param>
        /// <param name="p">Optional list of parameters.</param>
        void Bind(object control, IEnumerable values, params object[] p);
    }
}