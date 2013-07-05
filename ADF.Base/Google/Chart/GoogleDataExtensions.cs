using System;
using System.Linq;
using Adf.Base.Query;
using Adf.Core.Extensions;

namespace Adf.Base.Google.Chart
{
    public static class GoogleDataExtensions
    {
        public static GoogleData AddHeader(this GoogleData data, params string[] headers)
        {
            data.Header = string.Format("[{0}]", headers.FormatAndJoin("'{0}'", ", "));

            return data;
        }

        public static GoogleData AddRow(this GoogleData data, string text, params double[] values)
        {
            var row = values.Select(v => v.ToString()).FormatAndJoin("{0}", ", ");

            data.Rows.Add(string.Format("['{0}', {1}]", text, row));

            return data;
        }

        public static string Format(this GoogleData data)
        {
            var result = data.Rows.FormatAndJoin("{0}", ", ");

            return string.Format("[{0}, {1}]", data.Header, result);
        }


    }
}