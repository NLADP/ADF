using System;
using Windows.UI.Xaml.Data;
using Adf.Core.Extensions;

namespace Adf.WinRT.UI
{
    public class ImageUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value as string).IsNullOrEmpty() ? @"/Assets/project.png" : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return string.Empty;
        }
    }
}
