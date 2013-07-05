using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Adf.WinRT.UI.Grids
{
    public static class ListViewExtensions
    {
        public static ListView DefineItemTemplate(this ListView listView, int number)
        {
            listView.ItemTemplate =(DataTemplate)XamlReader.Load("<DataTemplate><Grid></Grid></DataTemplate");

            var grid = listView.ItemTemplate.FindControlsByType<Grid>().FirstOrDefault(); 

            if(grid == null) throw new ElementNotAvailableException("DataTemplate of the ListView should contain a grid");

            for (var i = 0; i < number; i++) grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            return listView;
        }
    }
}
