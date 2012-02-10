using System.Collections;
using Adf.Core.Objects;

namespace Adf.Core.Binding
{
	/// <summary>
	/// Provides functionality to bind a Domain object with a control.
	/// </summary>
    public static class BindManager
	{
	    private static IPlatformBinder _platformbinder;

	    private static readonly object _lock = new object();

	    /// <summary>
	    /// The PlatformBinder of this instance.
	    /// </summary>
	    internal static IPlatformBinder PlatformBinder
	    {
	        get { lock(_lock) return _platformbinder ?? (_platformbinder = ObjectFactory.BuildUp<IPlatformBinder>());  }
	    }

        /// <summary>
        /// Binds the specified DomainObject to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObject">Domain object to bind.</param>
        /// <param name="isPostback">True if this is a repeatation.</param>
        /// <param name="p">Optional list of parameters.</param>
        public static void Bind(object control, object bindableObject, bool isPostback, params object[] p)
        {
            PlatformBinder.Bind(control, bindableObject, isPostback, p);
        }

        /// <summary>
        /// Binds the specified DomainObject to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObject">Domain object to bind.</param>
        /// <param name="p">Optional list of parameters.</param>
        public static void Bind(object control, object bindableObject, params object[] p)
        {
            PlatformBinder.Bind(control, bindableObject, false, p);
        }

        /// <summary>
        /// Binds the specified DomainObject array to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObjects">Array of Domain objects to bind.</param>
        /// <param name="isPostback">True if this is a repeatation.</param>
        /// <param name="p">Optional list of parameters.</param>
        public static void Bind(object control, object[] bindableObjects, bool isPostback, params object[] p)
        {
            PlatformBinder.Bind(control, bindableObjects, isPostback, p);
        }

        /// <summary>
        /// Binds the specified DomainObject array to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObjects">Array of Domain objects to bind.</param>
        /// <param name="p">Optional list of parameters.</param>
        public static void Bind(object control, object[] bindableObjects, params object[] p)
        {
            PlatformBinder.Bind(control, bindableObjects, false, p);
        }

	    /// <summary>
	    /// Binds the specified DomainObject array to the control provided.
	    /// </summary>
	    /// <param name="control">Control to bind to.</param>
	    /// <param name="bindableObjects">List of Domain objects to bind.</param>
	    /// <param name="isPostback">True if this is a repeatation.</param>
	    /// <param name="p">Optional list of parameters.</param>
	    public static void Bind(object control, IEnumerable bindableObjects, bool isPostback, params object[] p)
        {
            PlatformBinder.Bind(control, bindableObjects, isPostback, p);
        }

        /// <summary>
        /// Binds the specified DomainObject array to the control provided.
        /// </summary>
        /// <param name="control">Control to bind to.</param>
        /// <param name="bindableObjects">List of Domain objects to bind.</param>
        /// <param name="p">Optional list of parameters.</param>
        public static void Bind(object control, IEnumerable bindableObjects, params object[] p)
        {
            PlatformBinder.Bind(control, bindableObjects, false, p);
        }

        /// <summary>
        /// Persists the specified DomainObject to the control provided.
        /// </summary>
        /// <param name="bindableObject">The object to persist.</param>
        /// <param name="control">The control where the object will be persisted.</param>
        /// <param name="p">Optional list of parameters.</param>
        public static void Persist(object bindableObject, object control, params object[] p)
        {
            PlatformBinder.Persist(bindableObject, control, p);
        }

        /// <summary>
        /// Gets a list of prefixes of the pluged in control binders. 
        /// </summary>
        /// <returns>
        /// Keys for the specified <see cref="ICollection"/> object
        /// </returns>
        public static ICollection Keys
        {
            get { return PlatformBinder.Keys; }
        }
	}
}
