using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Grids
{
    public class GridItemType : Descriptor
    {
        public GridItemType(string name) : base(name) { }

        public static readonly GridItemType Unknown = new GridItemType("Unknown");
        public static readonly GridItemType Text = new GridItemType("Text");
        public static readonly GridItemType Date = new GridItemType("Date");
        public static readonly GridItemType Image = new GridItemType("Image");
        public static readonly GridItemType Number = new GridItemType("Number");
        public static readonly GridItemType Money = new GridItemType("Money");
        public static readonly GridItemType Bool = new GridItemType("Bool");
    }
}
