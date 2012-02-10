using System;
using System.Xml.Linq;
using Adf.Core.Extensions;
using Adf.Core.Messaging;
using Adf.Core.State;
using System.IO;

namespace Adf.Base.Messaging
{
    public class XmlMessageDefinitionProvider : IMessageDefinitionProvider
    {
//        protected string directory = StateManager.Settings["XmlMessageDefinition.Directory"].ToString();

        public MessageDefinition Read(string messageName, string section = null)
        {
            var filename = messageName;
            
            if (!File.Exists(filename)) throw new FileNotFoundException("Unable to locate message definition file {0}", filename);

            var definition = XDocument.Load(filename);
            var message = definition.Element("message");

            if (message == null) return null;

            var messageDefinition = new MessageDefinition
                                        {
                                            Name = message.GetAttributeOrDefault("name"), 
                                            FileName = filename,
                                            RecordSeparator = message.GetAttributeOrDefault("recordSeparator", ","),
                                            HasHeader = message.GetAttributeOrDefault<bool>("hasHeader"),
                                        };

            foreach (XElement record in message.Elements("record"))
            {
                var newRecord = new RecordDefinition
                {
                    Name = record.GetAttributeOrDefault("name"), 
                    DomainObjectName = record.GetAttributeOrDefault("domainobjectname"),
                    FieldSeparator = record.GetAttributeOrDefault("fieldSeparator", ","),
                };

                int defaultStartPos = 0;
                foreach (XElement field in record.Elements("field"))
                {
                    var newField = new FieldDefinition
                    {
                        Format = field.GetAttributeOrDefault("format"),
                        IsOptional = field.GetAttributeOrDefault<bool>("optional"),
                        StartPosition = field.GetAttributeOrDefault<int>("position", defaultStartPos),
                        Name = field.GetAttributeOrDefault("name"),
                        Type = field.GetAttributeFromDescriptorOrDefault<FieldDefinitionType>("type"),
                        Label = field.GetAttributeOrDefault("label", field.GetAttributeOrDefault("name")),
                        Table = field.GetAttributeOrDefault("table", newRecord.DomainObjectName),
                        Default = field.GetAttributeOrDefault("default")
                    };
                    defaultStartPos = newField.StartPosition + 1;

                    if (newField.Name.IsNullOrEmpty() || newField.Type.IsNullOrEmpty()) throw new MessagingException("Error reading message definition " + messageDefinition.Name);

                    newRecord.Fields.Add(newField);
                }
                messageDefinition.Records.Add(newRecord);
            }

            return messageDefinition;
        }
    }

}
