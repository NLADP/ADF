using Adf.Base.Validation;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.WinRT.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.Binding
{
    public class XamlPanelBinder : IPlatformBinder
    {
        public void Bind(object control, object bindableObject, bool isPostback, params object[] p)
        {
            var element = control as FrameworkElement;

            if (element == null) return;

            element.DataContext = null;

            BindItemControls(element.FindControlsByType<ItemsControl>().ToList(), bindableObject);
            
            element.DataContext = bindableObject;
        }

        public void Bind(object control, object[] bindableObjects, bool isPostback, params object[] p)
        {
            throw new System.NotImplementedException();
        }

        public void Bind(object control, IEnumerable bindableObjects, bool isPostback, params object[] p)
        {
            var element = control as ItemsControl;

            if (element != null) element.ItemsSource = bindableObjects;
        }

        public void Persist(object bindableObject, object control, params object[] p)
        {
            throw new System.NotImplementedException();
        }

        private static void BindItemControls(ICollection<ItemsControl> itemsControls, object bindableObject)
        {
            if (!itemsControls.Any()) return;

            foreach(var control in itemsControls)
            {
                var propertyInfo = control.GetValue(FrameworkElementDependencyProperties.BindedMemberInfoProperty) as PropertyInfo;

                if (propertyInfo == null) continue;
                
                var propertyValue = GetValue(bindableObject, propertyInfo);
                if(propertyValue == null) continue;

                var items = BindManager.GetListFor(propertyInfo);

                var includeEmpty = !propertyInfo.IsNonEmpty();

                control.ItemsSource = PropertyHelper.GetCollection(propertyValue, includeEmpty, items);
            }
        }

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

        public ICollection Keys { get; private set; }
    }
}
