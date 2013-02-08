using System.Collections.Generic;
using System.Linq;
using Adf.Core.Extensions;
using Adf.Core.Objects;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.UI.Panels
{
    public class XamlPanelRenderer : IRenderer
    {
        private static IEnumerable<IItemRenderer> _renderers;
        private static readonly object Lock = new object();

        private static IEnumerable<IItemRenderer> Renderers
        {
            get { lock (Lock) return _renderers ?? (_renderers = ObjectFactory.BuildAll<IItemRenderer>().ToList()); }
        }

        public object Render(PanelObject panel)
        {
            var grid = new Grid()
                .DefineRows(panel.Rows.Count, 50);

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
        }
    }
}
