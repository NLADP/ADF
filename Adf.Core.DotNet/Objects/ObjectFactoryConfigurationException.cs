using System;
using System.Runtime.Serialization;

namespace Adf.Core.Objects
{
    [Serializable]
    public class ObjectFactoryConfigurationException : Exception
    {
        public ObjectFactoryConfigurationException()
        {
        }

        public ObjectFactoryConfigurationException(string message)
            :base(message)
        {
        }

        public ObjectFactoryConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ObjectFactoryConfigurationException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
        }
    }
}