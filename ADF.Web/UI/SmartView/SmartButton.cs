using Adf.Core.Resources;

namespace Adf.Web.UI.SmartView
{
    public abstract class SmartButton : SmartField
    {
        public string CommandName { get; set; }

        private string _messageSubject;

        public string MessageSubject
        {
            get { return _messageSubject; }
            set { _messageSubject = ResourceManager.GetString(value); }
        }

        private string _messageformat;
        public string MessageFormat
        {
            get { return _messageformat; }
            set { _messageformat = ResourceManager.GetString(value); }
        }
        public string MessageField { get; set; }

        public string IdField { get; set; }
    }
}
