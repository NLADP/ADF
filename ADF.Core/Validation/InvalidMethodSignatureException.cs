using System;

namespace Adf.Core.Validation
{
    /// <summary>
    /// Represents an error related to the invalid method signature.
    /// Thrown when the method signature is wrong.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    public class InvalidMethodSignatureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMethodSignatureException"/> class.
        /// </summary>
        public InvalidMethodSignatureException() : base() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMethodSignatureException"/> class with the 
        /// specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidMethodSignatureException(string message) : base(message) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMethodSignatureException"/> class with a 
        /// specified error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference
        /// if no inner exception is specified.</param>
        public InvalidMethodSignatureException(string message, Exception inner) : base(message, inner) {}
    }
}