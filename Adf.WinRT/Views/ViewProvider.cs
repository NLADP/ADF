using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Adf.Core.Views;
using Adf.Core.Tasks;
using Adf.Core.Objects;
using Adf.Core.State;

namespace Adf.WinRT.Views
{
    public class ViewProvider : IViewProvider
    {
        private readonly Frame _mainFrame;
        private readonly IView _main = ObjectFactory.BuildUp<IView>();
        private Dictionary<string, Type> _allviews;

        private Dictionary<string, Type> AllViews
        {
            get { return _allviews ?? (_allviews = GetViews()); }
        }

        private Dictionary<string, Type> GetViews()
        {
            if (_main == null) throw new InvalidOperationException("MainView is not initialized");

            var views = new Dictionary<string, Type>();

            var viewtypeinfo = typeof(IView).GetTypeInfo();

            foreach (var type in _main.GetType().GetTypeInfo().Assembly.ExportedTypes.Where(t => viewtypeinfo.IsAssignableFrom(t.GetTypeInfo())).Where(type => !views.ContainsKey(type.Name)))
            {
                views.Add(type.Name, type);
            }

            return views;
        }

        public ViewProvider()
        {
            _mainFrame = Window.Current.Content as Frame;
        }

        public void ActivateView(ITask task, params object[] p)
        {
            if (_mainFrame == null) return;

            StateManager.Settings["PageName"] = task.Name.Label;

            _mainFrame.Navigate(AllViews[task.Name.ToString()], task);
        }

        public void ActivateView(ITask task, bool newView, params object[] p)
        {
            ActivateView(task, p);
        }

        public void DeactivateView(ITask task, params object[] p)
        {
            if (_mainFrame != null && _mainFrame.CanGoBack)
                _mainFrame.GoBack();
        }
    }
}
