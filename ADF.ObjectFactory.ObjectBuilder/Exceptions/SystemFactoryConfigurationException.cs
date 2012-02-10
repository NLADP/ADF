using System;
using System.Runtime.Serialization;

namespace Adf.ObjectFactory.ObjectBuilder.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), Serializable]
    public class SystemFactoryException : Exception, ISerializable
    {
        public SystemFactoryException()
        {
        }

        public SystemFactoryException(string message)
            :base(message)
        {
        }

        public SystemFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SystemFactoryException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}