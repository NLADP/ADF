using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Adf.Core.Tasks;
using Adf.Core.State;
using Adf.Core.Views;

namespace Adf.WinRT.Views
{
    public abstract class View<TTask> : Page, IView where TTask : ITask
    {
        protected Guid Id { get { return Task.Id; } }

        public TTask MyTask { get { return (TTask)Task; } }

        public ITask Task { get; protected set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                Task = e.Parameter as ITask;

                Bind();
            }

            base.OnNavigatedTo(e);
        }

        protected virtual void Bind()
        {
        }
    }
}
