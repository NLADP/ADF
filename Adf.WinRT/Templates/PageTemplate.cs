using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.Templates
{
    public class DataTemplate : Control
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(DataTemplate), new PropertyMetadata(null));
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
    }

    public class PageTemplate : Control
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(PageTemplate), new PropertyMetadata(null));
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }


        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register("Footer", typeof(object), typeof(PageTemplate), new PropertyMetadata(null));
        public object Footer
        {
            get { return GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }


        public static readonly DependencyProperty BodyProperty = DependencyProperty.Register("Body", typeof(object), typeof(PageTemplate), new PropertyMetadata(null));
        public object Body
        {
            get { return GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }
    }
}
