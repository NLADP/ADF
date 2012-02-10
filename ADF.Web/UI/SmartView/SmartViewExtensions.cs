using Adf.Core.Identity;

namespace Adf.Web.UI.SmartView
{
    public static class SmartViewExtensions
    {
        public static bool TryGetId(this SmartView view, int index, out ID id)
        {
            id = IdManager.Empty();
            if (index < 0 || view.DataKeys.Count == 0 || view.DataKeys.Count <= index) return false;

            var dataKey = view.DataKeys[index];
            if (dataKey != null)
            {
                id = (ID) dataKey.Value;
                return true;
            }

            return false;
        }
    }
}
