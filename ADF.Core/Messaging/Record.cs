using Adf.Core.Data;

namespace Adf.Core.Messaging
{
    public class Record
    {
        public string DomainObjectName { get; set; }
        public IInternalState State { get; set; }
    }
}
