using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adf.Core.Tasks;
using Adf.Core.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.UI
{
    public static class WindowExtensions
    {
        public static ITask FindTask(this Window window)
        {
            if (window == null) throw new ArgumentNullException("window");

            var frame = window.Content as Frame;

            if (frame != null)
            {
                var view = frame.Content as IView;

                if (view != null)
                {
                    return view.Task;
                }
            }

            return null;
        }
    }
}
