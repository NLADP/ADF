using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Identity;

namespace Adf.Web.UI.SmartView
{
    public static class SmartViewExtensions
    {
        public static ID ExtractId(this DataKey key)
        {
            return (key == null) ? IdManager.Empty() : IdManager.New(key.Value);
        }

        public static bool TryGetId(this SmartView view, out ID id)
        {
            if (view == null) throw new ArgumentNullException("view");

            id = view.SelectedDataKey.ExtractId();

            return !id.IsEmpty;
        }

        public static bool TryGetId(this SmartView view, GridViewCommandEventArgs e, out ID id)
        {
            if (view == null) throw new ArgumentNullException("view");

            id = (e.CommandArgument == null) ? IdManager.Empty() : IdManager.New(e.CommandArgument);

            return !id.IsEmpty;
        }

        public static bool TryGetId(this SmartView view, GridViewDeleteEventArgs e, out ID id)
        {
            if (view == null) throw new ArgumentNullException("view");

            id = view.DataKeys[e.RowIndex].ExtractId();

            return !id.IsEmpty;         
        }

        public static bool TryGetId(this SmartView view, GridViewEditEventArgs e, out ID id)
        {
            if (view == null) throw new ArgumentNullException("view");

            id = view.DataKeys[e.NewEditIndex].ExtractId();

            return !id.IsEmpty;         
        }

        public static bool TryGetId(this SmartView view, GridViewRow row, out ID id)
        {
            if (view == null) throw new ArgumentNullException("view");

            id = view.DataKeys[row.DataItemIndex].ExtractId();

            return !id.IsEmpty;         
        }

        public static bool TryGetId(this DropDownTreeView treeView, CommandEventArgs e, out ID id)
        {
            if (treeView == null) throw new ArgumentNullException("treeView");

            id = (e.CommandArgument == null) ? IdManager.Empty() : IdManager.New(e.CommandArgument);

            return !id.IsEmpty;
        }

        public static void SelectItems<T>(this SmartView grid, string field) where T : IDomainObject
        {
            if (field == null) throw new ArgumentNullException("field");

            PropertyInfo pi = typeof(T).GetProperty(field);
            if (pi == null) throw new ArgumentException(string.Format("Property '{0}' could not be found on type '{1}'", field, typeof(T)));

            var source = ((IEnumerable<T>)grid.DataSource).ToList();

            foreach (GridViewRow row in grid.Rows)
            {
                bool selected = ((CheckBox) row.FindControl(field)).Checked;

                PropertyHelper.SetValue(source[row.DataItemIndex], pi, selected);
            }
        }

        public static List<T> GetSelectedItems<T>(this SmartView grid, string field = null) where T : IDomainObject
        {
            if (field.IsNullOrEmpty()) field = "IsSelected";

            var source = ((IEnumerable<T>) grid.DataSource).ToList();

            return (from row in grid.Rows.Cast<GridViewRow>()
                    where ((CheckBox) row.FindControl(field)).Checked
                    select source[row.DataItemIndex]
                   ).ToList();
        }
    }
}
