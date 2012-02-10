using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Web.UI.SmartView
{
    public class EditButton : IconButton
    {
        public EditButton()
        {
            CommandName = "Edit";
            Image = "Edit";
            ToolTip = "Edit current item";

        }
    }
}
