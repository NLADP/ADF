using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Adf.Core.Binding;
using Adf.Core.Objects;
using Adf.Web.Helper;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder which binds a <see cref="System.Web.UI.Control"/> with an object 
    /// or a list of objects.
    /// Provides methods to bind a <see cref="System.Web.UI.Control"/> with an object, an array 
    /// of objects, a list of objects etc.
    /// </summary>
    public class WebBinder : IPlatformBinder
    {

        #region Construction

        private readonly Dictionary<string, List<IControlBinder>> _binders = new Dictionary<string, List<IControlBinder>>();
        private readonly Dictionary<string, List<IControlBinder>> _postbackBinders = new Dictionary<string, List<IControlBinder>>();
        private readonly Dictionary<string, List<IControlPersister>> _persisters = new Dictionary<string, List<IControlPersister>>();

        /// <summary>
        /// Initializes an instance of the <see cref="Adf.Web.Binding.WebBinder"/> class.
        /// </summary>
        public WebBinder()
        {
            IEnumerable<IControlBinder> b = ObjectFactory.BuildAll<IControlBinder>().ToList();
            IEnumerable<IControlPersister> p = ObjectFactory.BuildAll<IControlPersister>().ToList();

            foreach (IControlBinder binder in b)
            {
                AddBinder(binder);
            }
            foreach (IControlPersister persister in p)
            {
                AddPersister(persister);
            }
        }

        #endregion Construction

        #region Add Binders and Persisters

        /// <summary>
        /// Adds the specified control binder to its collection of control binders.
        /// </summary>
        /// <param name="controlBinder">The control binder to add.</param>
        private void AddBinder(IControlBinder controlBinder)
        {
            if (controlBinder != null)
            {
                foreach (string type in controlBinder.Types)
                {
                    _binders[type] = (_binders.ContainsKey(type)) ? _binders[type] : new List<IControlBinder>();

                    _binders[type].Add(controlBinder);
                }

                if (BindOnPostbackAttribute.MustBindOnPostback(controlBinder))
                {
                    foreach (string type in controlBinder.Types)
                    {
                        _postbackBinders[type] = (_postbackBinders.ContainsKey(type)) ? _postbackBinders[type] : new List<IControlBinder>();

                        _postbackBinders[type].Add(controlBinder);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the specified control persister to the collection of control persisters.
        /// </summary>
        /// <param name="controlPersister">The control persister to add.</param>
        private void AddPersister(IControlPersister controlPersister)
        {
            if (controlPersister != null)
            {
                foreach (string type in controlPersister.Types)
                {
                    _persisters[type] = (_persisters.ContainsKey(type)) ? _persisters[type] : new List<IControlPersister>();

                    _persisters[type].Add(controlPersister);
                }
            }
        }

        #endregion Add Binders and Persisters

        #region Get Values

        /// <summary>
        /// Gets the value of the specified property of the specified business object.
        /// </summary>
        /// <param name="bindableObject">The business object to get the value from.</param>
        /// <param name="pi">The property of the business object to get the value of.</param>
        /// <returns>The value of the property of the business object.</returns>
        private static object GetValue(object bindableObject, PropertyInfo pi)
        {
            return pi == null ? null : pi.GetValue(bindableObject, null);
        }

        #endregion Get Values

        #region Bind object

        /// <summary>
        /// Binds the specified business object to the specified <see cref="System.Web.UI.Control"/>
        /// or its child <see cref="System.Web.UI.Control"/> using the specified list of 
        /// control binders.
        /// </summary>
        /// <param name="c">The <see cref="System.Web.UI.Control"/> or the child 
        ///   <see cref="System.Web.UI.Control"/> of which is to bind to.</param>
        /// <param name="bindableObject">The business object to bind.</param>
        /// <param name="controlBinders">The list of control binders used to bind.</param>
        /// <param name="p"></param>
        private static void BindToObject(Control c, object bindableObject, Dictionary<string, List<IControlBinder>> controlBinders, object[] p)
        {
            if (c == null || bindableObject == null)
                return;

            string domainobject = bindableObject.GetType().Name;
            PropertyInfo[] properties = bindableObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            
            Dictionary<string, Control> dictionary = c.GetControls();

            foreach (PropertyInfo pi in properties)
            {
                foreach (string key in controlBinders.Keys)
                {
                    string name = key + domainobject + pi.Name;

                    if (!dictionary.ContainsKey(name)) continue;

                    foreach (IControlBinder binder in controlBinders[key])
                    {
                        binder.Bind(dictionary[name], GetValue(bindableObject, pi), pi, p);
                    }
                }
            }
        }

        /// <summary>
        /// Binds the specified business object to the specified <see cref="System.Web.UI.Control"/>
        /// or its child <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.Control"/> or the child 
        /// <see cref="System.Web.UI.Control"/> of which is to bind to.</param>
        /// <param name="bindableObject">The business object to bind.</param>
        /// <param name="isPostback">The value indicating whether this is a repeatation.</param>
        /// <param name="p">The parameters used during binding. Currently not being used.</param>
        public void Bind(object control, object bindableObject, bool isPostback, params object[] p)
        {
            BindToObject(control as Control, bindableObject, (isPostback) ? _postbackBinders : _binders, p);
        }

        #endregion Bind object

        #region Bind object[]

        /// <summary>
        /// Binds the specified array of business objects to the specified <see cref="System.Web.UI.Control"/>
        /// or its child <see cref="System.Web.UI.Control"/> using the specified list of 
        /// control binders.
        /// </summary>
        /// <param name="c">The <see cref="System.Web.UI.Control"/> or the child 
        /// <see cref="System.Web.UI.Control"/> of which is to bind to.</param>
        /// <param name="bindableObjects">The array of business objects to bind.</param>
        /// <param name="controlBinders">The list of control binders used to bind.</param>
        private static void BindToArray(Control c, object[] bindableObjects, Dictionary<string, List<IControlBinder>> controlBinders)
        {
            if (c == null || bindableObjects == null || bindableObjects.Length < 1)
                return;

            foreach (string key in controlBinders.Keys)
            {
                // First, check if the control itself needs to be bound
                if (c.ID.StartsWith(key))
                {
                    foreach (IControlBinder controlBinder in controlBinders[key])
                    {
                        controlBinder.Bind(c, bindableObjects);
                    }
                }

                // Next, check if there are children that need to be bound
                string name = key + bindableObjects.GetType().Name.Replace("[]", "");

                Control controlToBind = ControlHelper.Find(c, name);
                if (controlToBind != null)
                {
                    foreach (IControlBinder controlBinder in controlBinders[key])
                    {
                        controlBinder.Bind(controlToBind, bindableObjects);
                    }
                }
            }
        }

        #endregion Bind object[]

        #region Bind IList

        /// <summary>
        /// Binds the specified array of business objects to the specified <see cref="System.Web.UI.Control"/>
        /// or its child <see cref="System.Web.UI.Control"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.Control"/> or the child 
        /// <see cref="System.Web.UI.Control"/> of which is to bind to.</param>
        /// <param name="bindableObjects">The array of business objects to bind.</param>
        /// <param name="isPostback">The value indicating whether this is a repeatation.</param>
        /// <param name="p">The parameters used during binding. Currently not being used.</param>
        public void Bind(object control, object[] bindableObjects, bool isPostback, params object[] p)
        {
            BindToArray(control as Control, bindableObjects, (isPostback) ? _postbackBinders : _binders);
        }

        /// <summary>
        /// Binds the specified list of business objects to the specified <see cref="System.Web.UI.Control"/>
        /// or its child <see cref="System.Web.UI.Control"/> using the specified list of 
        /// control binders.
        /// </summary>
        /// <param name="c">The <see cref="System.Web.UI.Control"/> or the child 
        /// <see cref="System.Web.UI.Control"/> of which is to bind to.</param>
        /// <param name="bindableObjects">The list of business objects to bind.</param>
        /// <param name="controlBinders">The list of control binders used to bind.</param>
        /// <param name="p"></param>
        private void BindToList(Control c, IEnumerable bindableObjects, Dictionary<string, List<IControlBinder>> controlBinders, params object[] p)
        {
            if (c == null) return;

            Dictionary<string, Control> dictionary = c.GetControls();

            foreach (string key in controlBinders.Keys)
            {
                // First, check if the control itself needs to be bound
                if (c.ID.StartsWith(key))
                {
                    foreach (IControlBinder binder in controlBinders[key])
                    {
                        binder.Bind(c, bindableObjects, p);
                    }
                }

                if (bindableObjects != null)
                {
                    // Next, check if there are children that need to be bound
                    string name = key + bindableObjects.GetType().Name.Replace("[]", "");

                    if (!dictionary.ContainsKey(name)) continue;

                    foreach (IControlBinder binder in _binders[key])
                    {
                        binder.Bind(dictionary[name], bindableObjects);
                    }
                }
            }
        }

        /// <summary>
        /// Binds the specified list of business objects to the specified <see cref="System.Web.UI.Control"/>
        /// or its child <see cref="System.Web.UI.Control"/> using its list of 
        /// control binders.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.Control"/> or the child 
        ///   <see cref="System.Web.UI.Control"/> of which is to bind to.</param>
        /// <param name="bindableObjects">The list of business objects to bind.</param>
        /// <param name="isPostback">The value indicating whether this is a repeatation.</param>
        /// <param name="p">The parameters used during binding. Currently not being used.</param>
        public void Bind(object control, IEnumerable bindableObjects, bool isPostback, params object[] p)
        {
            BindToList(control as Control, bindableObjects, (isPostback) ? _postbackBinders : _binders, p);
        }

        #endregion Bind IList

        #region Persist

        /// <summary>
        /// Persists the data of the specified <see cref="System.Web.UI.Control"/> to the 
        /// specified business object.
        /// </summary>
        /// <param name="bindableObject">The business object where to persist.</param>
        /// <param name="control">The <see cref="System.Web.UI.Control"/>, the data of which is to persist.</param>
        /// <param name="p">The optional list of parameters used during persistance. Currently not being used.</param>
        public void Persist(object bindableObject, object control, params object[] p)
        {
            Control c = control as Control;
            if (c == null || bindableObject == null)
                return;

            string domainobject = bindableObject.GetType().Name;
            PropertyInfo[] properties = bindableObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            Dictionary<string, Control> dictionary = c.GetControls();

            foreach (PropertyInfo pi in properties)
            {
                foreach (string key in _persisters.Keys)
                {
                    string name = key + domainobject + pi.Name;

                    if (!dictionary.ContainsKey(name)) continue;

                    foreach (IControlPersister persister in _persisters[key])
                    {
                        persister.Persist(bindableObject, pi, dictionary[name]);
                    }
                }
            }
        }

        #endregion Persist

        /// <summary>
        /// Gets a collection containing the keys in its list of control binders.
        /// </summary>
        /// <returns>
        /// The collection containing the keys in its list of control binders.
        /// </returns>
        public ICollection Keys
        {
            get { return _binders.Keys; }
        }
    }
}
