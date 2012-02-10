using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Identity;

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

        public static ID CurrentId(this SmartView.SmartView view, GridViewCommandEventArgs args)
        {
            // Convert the row index stored in the CommandArgument
            var index = Convert.ToInt32(args.CommandArgument);

            // Retrieve the bookingrecord id behind the row that contains the button clicked
            return (ID)view.DataKeys[index].Value;
        }
    }
}
