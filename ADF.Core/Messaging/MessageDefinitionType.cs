namespace Adf.Core.Messaging
{
    public class MessageDefinitionType : Descriptor
    {
        public MessageDefinitionType(string name, int order) : base(name, order: order) {}

        public static readonly MessageDefinitionType XSD = new MessageDefinitionType("XSD", 0);
        public static readonly MessageDefinitionType Xml = new MessageDefinitionType("Xml", 0);
   }
}
