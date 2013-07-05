namespace Adf.Core.Messaging
{
    public class MessageType : Descriptor
    {
        public MessageType(string name, int order) : base(name, order: order) {}

        public static readonly MessageType FixedLength = new MessageType("FixedLength", 0);
        public static readonly MessageType Csv = new MessageType("Csv", 10);
        public static readonly MessageType Xml = new MessageType("Xml", 20);
   }
}
