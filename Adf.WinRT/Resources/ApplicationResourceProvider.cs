using System.Globalization;
using Adf.Core.Resources;
using Adf.Core.State;

namespace Adf.WinRT.Resources
{
    public class ApplicationResourceProvider : IResourceProvider
    {
        public string GetString(string key)
        {
            return StateManager.Settings[key].ToString();
        }

        public string GetString(string key, CultureInfo culture)
        {
            return GetString(key);
        }
    }
}
