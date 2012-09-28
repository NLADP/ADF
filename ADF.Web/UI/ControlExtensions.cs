using System.Collections.Generic;
using System.Web.UI;

namespace Adf.Web.UI
{
    public static class ControlExtensions
    {
        public static ControlCollection AddRange(this ControlCollection collection, IEnumerable<Control> controls)
        {
            if (collection == null || controls == null) return collection;

            foreach (Control control in controls)
            {
                if (control != null) collection.Add(control);
            }

            return collection;
        }
    }
}
