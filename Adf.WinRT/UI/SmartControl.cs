using System;
using System.Threading.Tasks;
using Adf.Core.Domain;
using Adf.WinRT.UI.Events;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.UI
{
    public class SmartControl : UserControl
    {
        public SmartControl()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Render();
        }

        protected virtual void Render()
        {
        }

        public DomainObjectClickEventHandler DomainObjectClick;

        public void ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DomainObjectClick != null) DomainObjectClick.Invoke(sender, e.ClickedItem as IDomainObject);
        }
    }
}
