using System.Runtime.Serialization;

namespace Adf.Base.Tasks
{
    [DataContract]
    public class AsyncTaskData
    {
        public enum StatusCode
        {
            Working,
            Done,
            Error
        }

        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public StatusCode Status { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
