using System;

namespace Adf.Core.Messaging
{
    /// <summary>
    /// Represents an error of the message defintion.
    /// </summary>
    [Serializable]
    public class MessageDefinitionNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Core.Messaging.MessageDefinitionNotFoundException"/> class.
        /// </summary>
        public MessageDefinitionNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Core.Messaging.MessageDefinitionNotFoundException"/> class.
        /// </summary>
        public MessageDefinitionNotFoundException(string definitionFileName, string messageName, string tableName) 
            : base(string.Format("Message Definition was not found for definition file name: {0}, message name: {1} and table name {2}", definitionFileName, messageName, tableName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Core.Messaging.MessageDefinitionNotFoundException"/> class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MessageDefinitionNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Core.Messaging.MessageDefinitionNotFoundException"/> class 
        /// with a specified error message and a reference to the inner exception that is the cause of 
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified.</param>
        public MessageDefinitionNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
