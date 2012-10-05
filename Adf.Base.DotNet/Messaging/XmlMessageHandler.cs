using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adf.Base.Data;
using Adf.Core.Data;
using Adf.Core.Messaging;
using System.Xml.Linq;

namespace Adf.Base.Messaging
{

    public class XmlMessageHandler : IMessageHandler
    {
        public object Retrieve(string messagename, params object[] p)
        {
            if (p.Length == 0 || p[0] == null) return new List<Record>();

            StreamReader reader = p[0] as StreamReader;

            if (reader == null) return new List<Record>();

            MessageDefinition definition = MessagingManager.Read(MessageDefinitionType.XSD, messagename);
            XElement xmlMessage = XElement.Load(reader);

            return xmlMessage.Elements().Select(element => CreateRecord(definition, element)).ToList();
        }

        private static Record CreateRecord(MessageDefinition definition, XElement element)
        {
            RecordDefinition recordDef = definition.FindRecord(element.Name.LocalName);
            TableDescriber table = new TableDescriber(recordDef.Name, DataSources.NoSource);

            Record record = recordDef.Create(new DictionaryState());

            record.State.Set(new ColumnDescriber("ID", table, isIdentity: true), Guid.NewGuid());

            foreach (XElement subelement in element.Elements())
            {
                FieldDefinition fieldDef = recordDef.FindField(subelement.Name.LocalName);
                if (fieldDef.Type == FieldDefinitionType.Record)
                {
                    RecordDefinition subRecordDef = definition.FindRecord(subelement.Name.LocalName);
                    if (subRecordDef.Repeats > 1 || subRecordDef.Repeats == -1)
                    {
                        // This element appears more than once
                        List<Record> existing = record.State.Get<List<Record>>(new ColumnDescriber(fieldDef.Name, recordDef.Name)) ?? new List<Record>();
                        // If not initialized, do so now

                        existing.Add(CreateRecord(definition, subelement));

                        record.State.Set(new ColumnDescriber(fieldDef.Name, recordDef.Name), existing);
                    }
                    else
                    {
                        Record value = CreateRecord(definition, subelement);
                        record.State.Set(new ColumnDescriber(fieldDef.Name, recordDef.Name), value);
                    }
                }
                else
                {
                    record.State.Set(new ColumnDescriber(fieldDef.Name, recordDef.Name), subelement.Value);
                }
            }

            return record;
        }

        public object Commit(string messagename, params object[] p)
        {
            throw new NotSupportedException();
        }


        public IInternalState GetEmpty(string messagename, string tablename)
        {
            throw new NotSupportedException();
        }
    }
}
