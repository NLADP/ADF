using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public static class SmartFieldExtensions
    {
        public static TableItemStyle Set(this TableItemStyle style, string value)
        {
            if (style.CssClass.IsNullOrEmpty() && !value.IsNullOrEmpty()) style.CssClass = value;

            return style;
        }

        public static object GetDataItem(this TableCell origin)
        {
            if (origin == null) return null;

            var item = origin.NamingContainer as GridViewRow;
            if (item == null || item.DataItem == null) return null;

            return item.DataItem;
        }

        public static string Compose(this SmartField smartfield, object entity, string field, string format)
        {
            if (smartfield == null) throw new ArgumentNullException("smartfield");

            var result = String.Empty;

            if (entity != null && !field.IsNullOrEmpty()) result = FormatHelper.ToString(PropertyHelper.GetValue(entity, field));
            if (!format.IsNullOrEmpty()) result = String.Format(format, result);

            return result;
        }

        public static bool IsEnabled(this SmartField smartfield, object entity, string isEmpty)
        {
            if (smartfield == null) throw new ArgumentNullException("smartfield");

            if (smartfield.HideOnEmpty && isEmpty.IsNullOrEmpty()) return false;
            if (smartfield.IsEnabledField.IsNullOrEmpty()) return true;

            var signed = smartfield.IsEnabledField.StartsWith("!");
            var visibleField = signed ? smartfield.IsEnabledField.Substring(1) : smartfield.IsEnabledField;

            return signed ^ Convert.ToBoolean(PropertyHelper.GetValue(entity, visibleField));
        }

        public static string Compose(this SmartField smartfield, object entity, string value, string field, string format)
        {
            if (smartfield == null) throw new ArgumentNullException("smartfield");

            var result = (entity != null && !field.IsNullOrEmpty()) ? FormatHelper.ToString(PropertyHelper.GetValue(entity, field)) : value;
            if (!format.IsNullOrEmpty()) result = String.Format(format, result);

            return result;
        }

        public static string ComposeIcon(this SmartField smartfield, object entity, string value, string field, string format)
        {
            if (smartfield == null) throw new ArgumentNullException("smartfield");

            var result = (entity != null && !field.IsNullOrEmpty()) ? PropertyHelper.GetValue(entity, field).ToString() : value;
            if (!format.IsNullOrEmpty()) result = String.Format(format, result);

            return result;
        }


        public static void SetId(this Control control, string childId)
        {
            if (!childId.IsNullOrEmpty()) control.ID = childId;
        }
    }
}