using System;

namespace Adf.Web.UI.SmartView
{
    public class FormatEventArgs : EventArgs
    {
        internal FormatEventArgs(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
