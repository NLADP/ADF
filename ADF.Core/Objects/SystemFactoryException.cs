using System;
using System.Runtime.Serialization;

namespace Adf.Core.Objects
{
    [Serializable]
    public class SystemFactoryConfigurationException : Exception
    {
        public SystemFactoryConfigurationException()
        {
        }

        public SystemFactoryConfigurationException(string message)
            :base(message)
        {
        }

        public SystemFactoryConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SystemFactoryConfigurationException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
        }
    }
}