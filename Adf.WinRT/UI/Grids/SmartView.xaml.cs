using Adf.Core.Domain;
using Adf.Core.Panels;
using Adf.WinRT.UI.Styling;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Adf.WinRT.UI.Grids
{
    public partial class SmartView
    {
        public string SmartTemplate { get; set; }

        public CollectionViewSource Objects
        {
            get { return Items; }
            set { Items = value; }
        }

        public SmartView()
        {
            InitializeComponent();

            View.ItemClick += ItemClick;
        }

        protected override void Render()
        {
            View.ItemTemplate = FrameworkElementStyleExtensions.GetTemplate(SmartTemplate);
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
