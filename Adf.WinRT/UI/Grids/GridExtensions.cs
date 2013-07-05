using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Adf.WinRT.UI.Grids
{
    public static class GridExtensions
    {
        public static Grid AddTextBlock(this Grid grid, string bindTo, string style = "PanelItemBlock", int width = 50, int column = -1)
        {
            if(grid == null) throw new ArgumentNullException("grid");

            var block = new TextBlock { Width = width * 6 };

            var binding = new Windows.UI.Xaml.Data.Binding { Path = new PropertyPath(bindTo), Mode = BindingMode.OneWay };
            block.SetBinding(TextBox.TextProperty, binding);

            if(column >= 0) block.SetValue(Grid.ColumnProperty, column);

            grid.Children.Add(block);

            return grid;
        }

        public static Grid AddImage(this Grid grid, string bindTo, int width = 20, int height = 20, int column = -1)
        {
            if(grid == null) throw new ArgumentNullException("grid");

            var image = new Image { Width = width * 6, Height = height * 6 };

            var binding = new Windows.UI.Xaml.Data.Binding { Path = new PropertyPath(bindTo), Mode = BindingMode.OneWay };
            image.SetBinding(Image.SourceProperty, binding);

            if(column >= 0) image.SetValue(Grid.ColumnProperty, column);

            grid.Children.Add(image);

            return grid;
        }
    }
}
