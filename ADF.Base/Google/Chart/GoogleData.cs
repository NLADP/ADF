using System.Collections.Generic;

namespace Adf.Base.Google.Chart
{
    public class GoogleData
    {
        public string Header { get; set; }
        public List<string> Rows { get; set; }

        public GoogleData()
        {
            Header = string.Empty;
            Rows = new List<string>();
        }
    }
}