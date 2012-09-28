using System;
using System.Collections.Generic;
using Adf.Core.Objects;

namespace Adf.Core.Styling
{
    public class StyleType : Descriptor
    {
        public StyleType(string name) : base(name) {}

        public static readonly StyleType Panel = new StyleType("Panel.Default");
        public static readonly StyleType PanelError = new StyleType("Panel.Error");
        public static readonly StyleType Grid = new StyleType("Grid");
        public static readonly StyleType Calendar = new StyleType("Calendar");
    }

    public static class StyleManager
    {
        private static readonly Dictionary<StyleType, IStyler> _stylers = new Dictionary<StyleType, IStyler>();

        private static readonly object _lock = new object();

        public static void Style(StyleType type, object target)
        {
            if (target == null) return;

            Get(type).Style(target);
        }

        private static IStyler Get(StyleType type)
        {
            IStyler styler;

            lock (_lock)
            {
                if (!_stylers.TryGetValue(type, out styler))
                {
                    styler = _stylers[type] = ObjectFactory.BuildUp<IStyler>(type.ToString());
                }
            }

            return styler;
        }
    }
}
