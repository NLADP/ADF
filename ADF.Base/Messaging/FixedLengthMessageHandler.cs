using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Adf.Base.Data;
using Adf.Core.Data;
using Adf.Core.Messaging;

namespace Adf.Base.Messaging
{
    public class FixedLengthMessageHandler : IMessageHandler
    {
        public object Retrieve(string messagename, params object[] p)
        {
            if (p.Length == 0 || p[0] == null) return new List<Record>();

            StreamReader reader = p[0] as StreamReader;

            if (reader == null) return new List<Record>();

            MessageDefinition definition = MessagingManager.Read(MessageDefinitionType.Xsd, messagename);

            // Fixedlength means no seperator indicators
            // This means that all data is in the messagedefinition:
            // No optional fields
            IEnumerable<Record> records = BreakIntoRecords(definition, reader);

            return records;
        }

        private IEnumerable<Record> BreakIntoRecords(MessageDefinition definition, StreamReader reader)
        {
            List<Record> records = new List<Record>();

            // each record in the definition leads to one record in the answer. 1-on-1 correlation
            foreach (RecordDefinition recordDef in definition.Records)
            {

                if (recordDef.Repeats > 0)
                {
                    for (int counter = 0; counter < recordDef.Repeats; counter++)
                    {
                        records.Add(CreateRecord(reader, recordDef, definition));
                    }
                }
            }

            return records;
        }

        private static Record CreateRecord(StreamReader reader, RecordDefinition recordDef, MessageDefinition definition)
        {
            Record newRecord = new Record { DomainObjectName = recordDef.Name };

            // each field in the definition leads to one value in the answer. 1-on-1 correlation, no optionals
            foreach (FieldDefinition fieldDef in recordDef.Fields)
            {

                if (fieldDef.Type == FieldDefinitionType.Record)
                {
                    RecordDefinition childRecordDef = definition.FindRecord(fieldDef.Name);

                    if (childRecordDef.Repeats > 1 || childRecordDef.Repeats == -1)
                    {
                        // This element appears more than once
                        List<Record> existing = newRecord.State.Get<List<Record>>(new ColumnDescriber(fieldDef.Name, recordDef.Name));
                        // If not initialized, do so now
                        if (existing == null) existing = new List<Record>();

                        existing.Add(CreateRecord(reader,childRecordDef,definition));

                        newRecord.State.Set(new ColumnDescriber(fieldDef.Name, recordDef.Name), existing);
                    }
                    else
                    {
                        newRecord.State.Set(new ColumnDescriber(fieldDef.Name, recordDef.Name), CreateRecord(reader, childRecordDef, definition));
                    }
                }
                else
                {
                    char[] buffer = new char[fieldDef.Length];
                    int recordsRead = reader.Read(buffer, 0, fieldDef.Length);

                    if (recordsRead > 0)
                    {
                        newRecord.State.Set(new ColumnDescriber(fieldDef.Name, recordDef.Name), buffer);
                    }
                }
            }
            return newRecord;
        }



        public object Commit(string messagename, params object[] p)
        {
            MessageDefinition definition = MessagingManager.Read(MessageDefinitionType.Xsd, messagename);

            return new List<Record>();
        }

        public IInternalState GetEmpty(string messagename, string tablename)
        {
            throw new NotImplementedException();
        }
    }
}
