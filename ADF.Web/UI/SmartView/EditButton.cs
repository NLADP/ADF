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
            IconFormat = "~/Images/{0}";
            Icon = "edit.png";
            Header = "Edit";
            ToolTipFormat = "Click to edit {0}";
            ToolTipField = "Title";
        }
    }
}
