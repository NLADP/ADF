using System.Linq;
using Adf.Core.Domain;
using Adf.Core.Panels;
using Adf.WinRT.UI.Events;
using Adf.WinRT.UI.Styling;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Adf.Core.Extensions;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Adf.WinRT.UI.Grids
{
    public partial class SmartGrid
    {
        public string SmartTemplate { get; set; }
        public string SmartHeaderTemplate { get; set; }

        public CollectionViewSource Objects
        {
            get { return Items; }
            set { Items = value; }
        }

        public SmartGrid()
        {
            InitializeComponent();

            View.ItemClick += ItemClick;

            // Hack to get the header size correct
            View.LayoutUpdated += (sender, o) =>
                                      {
                                          foreach (var grid in View.FindControlsByType<Grid>().Where(g => g.Style.IsEqual("SmartGridHeaderStyle")))
                                              grid.Width = View.ActualWidth;
                                      };
        }

        protected override void Render()
        {
            View.ItemTemplate = FrameworkElementStyleExtensions.GetTemplate(SmartTemplate);
            
            if(!SmartHeaderTemplate.IsNullOrEmpty())
                View.HeaderTemplate = FrameworkElementStyleExtensions.GetTemplate(SmartHeaderTemplate);
        }

        protected virtual PanelObject BuildUp()
        {
            return null;
        }

        private void SelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DomainObjectClick != null) DomainObjectClick.Invoke(sender, e.AddedItems[0] as IDomainObject);
        }
    }
}
