using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.WinRT.UI.Panels;
using Adf.WinRT.UI.Styling;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.UI.Grids
{
    public class XamlGridRenderer : IRenderer
    {
        public object Render(PanelObject panel)
        {
            return FrameworkElementStyleExtensions.GetTemplate(panel.Rows[0].Items[0].Text);

            var grid = new Grid().DefineRows(panel.Rows.Count, 50);

            var number = 0;
            foreach (var row in panel.Rows)
            {
                var stack = grid.AddPanel(number);

                foreach (var item in row.Items)
                {
                    if (item.Type.IsIn(RenderItemType.Header))
                    {
                        stack.AddTextBlock(item.Label, "PanelHeaderStyle");
                    }
                    else if (item.Type.IsIn(RenderItemType.EditableText, RenderItemType.NonEditableText))
                    {
                        stack.AddTextBlock(item.Label).AddTextBox(item.Member, width: item.Width);
                    }
                    else if (item.Type.IsIn(RenderItemType.MultiLineText))
                    {
                        stack.AddTextBlock(item.Label).AddMultiLineTextBox(item.Member, width: item.Width, height: item.Height);
                    }
                    else if (item.Type.IsIn(RenderItemType.Label))
                    {
                        stack.AddTextBlock(item.Label).AddLabel(item.Member.Name, width: item.Width);
                    }
                    else if (item.Type.IsIn(RenderItemType.CheckBox))
                    {
                        stack.AddTextBlock(item.Label).AddCheckBox(item.Member);
                    }
                    else if (item.Type.IsIn(RenderItemType.Calendar))
                    {
                        stack.AddTextBlock(item.Label).AddTextBox(item.Member);
                    }
                    else if (item.Type.IsIn(RenderItemType.DropDown))
                    {
                        stack.AddTextBlock(item.Label);

                        item.Target = stack.GetCombo(item.Member, width: item.Width);
                    }
                }

                number++;
            }

            return grid;
            //var listView = new ListView().DefineItemTemplate(gridControl.Items.Count);
            //var grid = listView.ItemTemplate.FindControlsByType<Grid>().FirstOrDefault();

            //if (grid == null) throw new ElementNotAvailableException("DataTemplate of the ListView should contain a grid");

            //var column = 0;

            //foreach(var item in gridControl.Items)
            //{
            //    if (item.Type.IsIn(GridItemType.Text))
            //    {
            //        grid.AddTextBlock(item.Member.Name, width: item.Width, column: column);
            //    }
            //    else if(item.Type.IsIn(GridItemType.Image))
            //    {
            //        grid.AddImage(item.Member.Name, item.Width, item.Height, column: column);
            //    }

            //    column++;
            //}

            return null;// listView;
        }
    }
}
