using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Adf.Core.Extensions;
using Adf.Core.Grids;

namespace Adf.Base.Grids
{
    public static class GridItemExtensions
    {
        private static void CreateItem<TDomainObject>(this GridControl grid, GridItemType itemType, Expression<Func<TDomainObject, object>> property, string header = null, int? width = null, int? height = null)
        {
            var gridItem = new GridItem { Member = property.GetMemberInfo(), Type = itemType };

            if (grid.AutoGenerateHeaders) gridItem.Header = gridItem.Member.Name;
            if (width != null) gridItem.Width = width.Value;
            if (height != null) gridItem.Height = height.Value;
            if (header != null) gridItem.Header = header;

            grid.Add(gridItem);
        }

        public static GridControl ShowTextColumn<TDomainObject>(this GridControl grid, Expression<Func<TDomainObject, object>> property, string header = null, int? width = null)
        {
            grid.CreateItem(GridItemType.Text, property, header, width);

            return grid;
        }

        public static GridControl ShowImageColumn<TDomainObject>(this GridControl grid, Expression<Func<TDomainObject, object>> property, string header = null, int? width = null, int? height = null)
        {
            grid.CreateItem(GridItemType.Image, property, header, width, height);

            return grid;
        }

        public static GridControl ShowMoneyColumn<TDomainObject>(this GridControl grid, Expression<Func<TDomainObject, object>> property, string header = null, int? width = null)
        {
            grid.CreateItem(GridItemType.Money, property, header, width);

            return grid;
        }

        public static GridControl ShowNumberColumn<TDomainObject>(this GridControl grid, Expression<Func<TDomainObject, object>> property, string header = null, int? width = null)
        {
            grid.CreateItem(GridItemType.Number, property, header, width);

            return grid;
        }

        public static GridControl ShowBoolColumn<TDomainObject>(this GridControl grid, Expression<Func<TDomainObject, object>> property, string header = null)
        {
            grid.CreateItem(GridItemType.Bool, property, header);

            return grid;
        }

        public static GridControl ShowDateColumn<TDomainObject>(this GridControl grid, Expression<Func<TDomainObject, object>> property, string header = null, int? width = null)
        {
            grid.CreateItem(GridItemType.Date, property, header, width);

            return grid;
        }
    }
}
