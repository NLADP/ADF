using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Adf.Core.Extensions
{
    public static class CultureInfoExtensions
    {
        public static string GetLongDateNotationWithoutDayName(this CultureInfo cultureInfo)
        {
            return cultureInfo.DateTimeFormat.LongDatePattern
                                            .Replace("dddd ", "")
                                            .Replace("dddd, ", "")
                                            .Replace("dddd", "");
        }
    }
}
