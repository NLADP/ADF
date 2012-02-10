using System;

namespace Adf.Core.Data
{
    [Serializable]
    public class DataException : Exception
    {
        public DataException(string message, Exception innerException = null) : base(message, innerException) {}
    }
}
