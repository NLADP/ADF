using System;
using System.Reflection;
using Adf.Core.State;
using Adf.WinRT.Converters;
using Adf.WinRT.UI.Styling;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.WinRT.UI.Panels
{
    public static class PanelExtensions
    {
        public static StackPanel AddTextBlock(this StackPanel panel, string text, string style = "PanelLabel")
        {
            if (panel == null) throw new ArgumentNullException("panel");

            var block = new TextBlock { Text = text, Width = StateManager.Settings.GetOrDefault("PanelDefaultLabelWidth", 50) };
            block.Style(style);

            panel.Children.Add(block);

            return panel;
        }

        public static StackPanel AddLabel(this StackPanel panel, string bindto, string style = "PanelItemBlock", int width = 50)
        {
            if (panel == null) throw new ArgumentNullException("panel");

            var box = new TextBlock { Width = width * 6 };
            box.Style(style);
            
            var b = new Windows.UI.Xaml.Data.Binding { Path = new PropertyPath(bindto), Mode = BindingMode.TwoWay };
            box.SetBinding(TextBlock.TextProperty, b);

            panel.Children.Add(box);

            return panel;
        }
        
        public static StackPanel AddTextBox(this StackPanel panel, MemberInfo bindto, string style = "PanelItem", int width = 50)
        {
            if (panel == null) throw new ArgumentNullException("panel");

            var box = new TextBox { Width = width * 6 };
            box.Style(style);

            var b = new Windows.UI.Xaml.Data.Binding { Path = new PropertyPath(bindto.Name), Mode = BindingMode.TwoWay };
            box.SetBinding(TextBox.TextProperty, b);
            box.SetValue(FrameworkElementDependencyProperties.BindedMemberInfoProperty, bindto);

            panel.Children.Add(box);

            return panel;
        }
        
        public static StackPanel AddMultiLineTextBox(this StackPanel panel, MemberInfo bindto, string style = "PanelItem", int width = 50, int height = 0)
        {
            if (panel == null) throw new ArgumentNullException("panel");

            var box = new TextBox { Width = width * 6, TextWrapping = TextWrapping.Wrap};
            box.Style(style);

            if (height > 0) box.Height = height * 6;

            var b = new Windows.UI.Xaml.Data.Binding { Path = new PropertyPath(bindto.Name), Mode = BindingMode.TwoWay };
            box.SetBinding(TextBox.TextProperty, b);
            box.SetValue(FrameworkElementDependencyProperties.BindedMemberInfoProperty, bindto);

            panel.Children.Add(box);

            return panel;
        }
        
        public static StackPanel AddCheckBox(this StackPanel panel, MemberInfo bindto, string style = "PanelItem")
        {
            if (panel == null) throw new ArgumentNullException("panel");

            var box = new CheckBox();
            box.Style(style);

            var b = new Windows.UI.Xaml.Data.Binding { Path = new PropertyPath(bindto.Name), Mode = BindingMode.TwoWay };
            box.SetBinding(CheckBox.IsCheckedProperty, b);
            box.SetValue(FrameworkElementDependencyProperties.BindedMemberInfoProperty, bindto);

            panel.Children.Add(box);

            return panel;
        }

        public static ComboBox GetCombo(this StackPanel panel, MemberInfo bindto, string style = "PanelItem", int width = 50)
        {
            if (panel == null) throw new ArgumentNullException("panel");

            var combo = new ComboBox { Width = width * 6 };
            combo.Style(style);

            var converter = GetConverter(((PropertyInfo) bindto).PropertyType);

            var b = new Windows.UI.Xaml.Data.Binding
                        {
                            Path = new PropertyPath(bindto.Name),
                            Mode = BindingMode.TwoWay,
                            Converter = converter,
                            ConverterParameter = converter != null ? ((PropertyInfo)bindto).PropertyType : null
                        };
            combo.SetBinding(ComboBox.SelectedItemProperty, b);
            combo.SetValue(FrameworkElementDependencyProperties.BindedMemberInfoProperty, bindto);
            
            panel.Children.Add(combo);

            return combo;
        }

        private static IValueConverter GetConverter(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if(typeInfo.IsEnum) return new EnumToValueItemConverter();
            
            return null;
        }

        public static StackPanel AddCombo(this StackPanel panel, MemberInfo bindto, string style = "PanelItem", int width = 50)
        {
            panel.GetCombo(bindto, style, width);

            return panel;
        }

        public static Grid DefineRow(this Grid grid, int height)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(height) });

            return grid;
        }

        public static Grid DefineRows(this Grid grid, int number, int height)
        {
            for (int i= 0; i <number; i++) grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(height, GridUnitType.Pixel) });

            return grid;
        }

        public static StackPanel AddPanel(this Panel element, int row)
        {
            var panel = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };
            panel.SetValue(Grid.RowProperty, row);

            element.Children.Add(panel);

            return panel;
        }
    }
}
