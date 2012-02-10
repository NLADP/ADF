using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Web.UI.SmartView
{
    public class SelectButton : IconButton
    {
        public SelectButton()
        {
            CommandName = "Select";
            Image = "bullet_select.gif";
            ToolTip = "Select current item";
        }
    }
}
