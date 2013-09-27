using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adf.Base.Data;
using Adf.Core.Data;
using Adf.Core.Extensions;
using Adf.Core.Messaging;

namespace Adf.Base.Messaging
{
    public class UnorderedCsvMessageHandler : CsvMessageHandler
    {
        protected override IEnumerable<Record> BreakIntoRecords(MessageDefinition definition, StreamReader reader)
        {
            var currentLine = 2;
            var currentRecord = string.Empty;

            try
            {
                var records = new List<Record>();
                var recordDefinition = definition.Records[0];
                var table = new TableDescriber(recordDefinition.Name, DataSources.NoSource);

                if (Delimiter.IsNullOrEmpty()) Delimiter = recordDefinition.FieldSeparator;

                if (Delimiter.IsNullOrEmpty()) throw new MessagingException("The field separator is not defined in the message definition.");

                var headers = GetHeaders(reader, recordDefinition);

                var fields = ReadCsvLine(reader, Delimiter);

                while (fields != null)
                {
                    if(fields.Length != headers.Count)
                        throw new MessagingException(string.Format("Line {0} has has an incorrect amount of columns. {1} instead of {2}.",
                                                                   currentLine, fields.Length, headers.Count));

                    var state = new DictionaryState { IsNew = true };

                    if (!recordDefinition.Fields.Exists(f => f.Name.Equals("ID", StringComparison.OrdinalIgnoreCase)))
                        state[new ColumnDescriber("ID", table, isIdentity: true)] = Guid.NewGuid();

                    state[new ColumnDescriber("TimeStamp", table, isTimestamp: true)] = null;

                    foreach (var fieldDefinition in headers)
                    {
                        currentRecord = fieldDefinition.Name;
                        ReadRecordField(state, table, fieldDefinition, fields);
                    }

                    records.Add(recordDefinition.Create(state));

                    fields = ReadCsvLine(reader, Delimiter);
                    currentLine++;
                    currentRecord = string.Empty;
                }

                return records;
            }
            catch (FormatException exception)
            {
                throw new MessagingException(string.Format("Unknown message format. Line {1}, field {2}. {0}", exception.Message, currentLine, currentRecord), exception);
            }
        }

        private List<FieldDefinition> GetHeaders(StreamReader reader, RecordDefinition recordDefinition)
        {
            var headers = ReadCsvLine(reader, Delimiter);

            if (headers.IsNullOrEmpty()) throw new MessagingException("Unable to import file, the file is empty or no correct header is specified.");

            var headerDefinitions = new List<FieldDefinition>();

            var startPosition = 0;

            foreach (var header in headers)
            {
                var headerDef = recordDefinition.Fields.FirstOrDefault(f => header.Equals(f.Label.IsNullOrEmpty() ? f.Name : f.Label, StringComparison.OrdinalIgnoreCase));

                // Set the startposition so that we can reuse functions of the CsvMessageHandler
                if (headerDef != null) headerDef.StartPosition = startPosition++;

                headerDefinitions.Add(headerDef ?? new FieldDefinition { Type = FieldDefinitionType.String, Label = header, Name = "Unknown-" + header, StartPosition = startPosition++ });
            }

            // Validate that mandatory columns are present
            var missingFields = recordDefinition.Fields.Where(f => !f.IsOptional).Where(mandatoryHeader => !headerDefinitions.Contains(mandatoryHeader)).ToList();

            if(missingFields.Count > 0)
                throw new MessagingException(string.Format("Unable to import file, mandatory column(s) missing: {0}.",
                                             string.Join(", ", from field in missingFields select field.Label.IsNullOrEmpty() ? field.Name : field.Label)));

            return headerDefinitions;
        }
    }
}
