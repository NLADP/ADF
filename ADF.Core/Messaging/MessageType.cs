namespace Adf.Core.Messaging
{
    public class MessageType : Descriptor
    {
        public MessageType(string name, int order) : base(name, order: order) {}

        public static readonly MessageType FixedLength = new MessageType("FixedLength", 0);
        public static readonly MessageType CSV = new MessageType("CSV", 10);
        public static readonly MessageType XML = new MessageType("XML", 20);
   }
}
