using System.Collections.Generic;
using Adf.Core.Objects;
using Adf.Core.Data;

namespace Adf.Core.Messaging
{
    public static class MessagingManager
    {
        #region Message Definitions

        private static readonly Dictionary<MessageDefinitionType, IMessageDefinitionProvider> providers = new Dictionary<MessageDefinitionType, IMessageDefinitionProvider>();

        private static readonly object ProvidersLock = new object();

        internal static IMessageDefinitionProvider GetProvider(MessageDefinitionType type)
        {
            lock (ProvidersLock)
            {
                if (!providers.ContainsKey(type))
                {
                    IMessageDefinitionProvider provider = ObjectFactory.BuildUp<IMessageDefinitionProvider>(type.Name);

                    providers.Add(type, provider);
                }
            }
            return providers[type];
        }

        public static MessageDefinition Read(MessageDefinitionType type, string message, string section = null)
        {
            return GetProvider(type).Read(message, section);
        }

        #endregion Message Definition

        private static readonly Dictionary<MessageType, IMessageHandler> handlers = new Dictionary<MessageType, IMessageHandler>();

        private static readonly object HandlersLock = new object();

        internal static IMessageHandler GetHandler(MessageType type)
        {
            lock (HandlersLock)
            {
                if (!handlers.ContainsKey(type))
                {
                    IMessageHandler handler = ObjectFactory.BuildUp<IMessageHandler>(type.Name);

                    handlers.Add(type, handler);
                }
            }
            return handlers[type];
        }

        public static object Retrieve(MessageType type, string messagename, params object[] p)
        {
            return GetHandler(type).Retrieve(messagename, p);
        }

        public static object Commit(MessageType type, string messagename, params object[] p)
        {
            return GetHandler(type).Commit(messagename, p);
        }

        public static IInternalState GetEmpty(MessageType type, string messagename, string tablename)
        {
            return GetHandler(type).GetEmpty(messagename, tablename);
        }
    }
}