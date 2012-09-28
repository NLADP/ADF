namespace Adf.Core.Messaging
{
    public interface IMessageDefinitionProvider
    {
        MessageDefinition Read(string messageName, string section = null);
    }
}
