using Adf.Core.Rendering;
using Windows.UI.Xaml;
using Adf.Core.Panels;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Adf.WinRT.UI.Panels
{
    public partial class SmartPanel
    {
        public SmartPanel()
        {
            InitializeComponent();

            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled) Render();
        }

        protected void Render()
        {
            var panel = BuildUp();

            Content = RenderManager.Render(RenderType.Panel, panel) as UIElement;
        }

        protected virtual PanelObject BuildUp()
        {
            return null;
        }
    }
}
