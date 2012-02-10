﻿using System;

namespace Adf.Base.Messaging
{
    [Serializable]
    public class MessagingException : Exception
    {
        public MessagingException(string message, Exception innerException = null) : base(message, innerException) {}
    }
}
