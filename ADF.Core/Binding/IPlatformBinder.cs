using System.Collections;

namespace Adf.Core.Binding
{
    /// <summary>
    /// Defines a generalized Bind method that all Page controls must implement to bind 
    /// its state to the business layer.
    /// </summary>
    public interface IPlatformBinder
    {
        /// <summary>
        /// Method definition for binding the specified DomainObject to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObject">Value to bind.</param>
        /// <param name="isPostback">True if this is a repeatation.</param>
        /// <param name="p">Optional list of parameters.</param>
        void Bind(object control, object bindableObject, bool isPostback, params object[] p);


        /// <summary>
        /// Method definition for binding the specified DomainObject array to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObjects">Value to bind.</param>
        /// <param name="isPostback">True if this is a repeatation.</param>
        /// <param name="p">Optional list of parameters.</param>
        void Bind(object control, object[] bindableObjects, bool isPostback, params object[] p);

        /// <summary>
        /// Method definition for binding the specified DomainObject array to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObjects">Value to bind.</param>
        /// <param name="isPostback">True if this is a repeatation.</param>
        /// <param name="p">Optional list of parameters.</param>
        void Bind(object control, IEnumerable bindableObjects, bool isPostback, params object[] p);

        /// <summary>
        /// Method definition for persisting the specified DomainObject to the control provided.
        /// </summary>
        /// <param name="bindableObject">The DomainObject to persist.</param>
        /// <param name="control">The control where the supplied DomainObject to persist to.</param>
        /// <param name="p">Optional list of parameters.</param>
        void Persist(object bindableObject, object control, params object[] p);

        /// <summary>
        /// Gets the keys of a ICollection object. 
        /// </summary>
        ICollection Keys { get; }
    }
}