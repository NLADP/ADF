using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Adf.Base.Data;
using Adf.Base.Domain;
using Adf.Core.Data;
using Adf.Core.Extensions;
using Adf.Core.Messaging;

namespace Adf.Base.Messaging
{
    public class CsvMessageHandler : IMessageHandler
    {
        protected string Delimiter { get; set; }

        public virtual object Retrieve(string messagename, params object[] p)
        {
            if (p.Length == 0) throw new FileNotFoundException("File to read was not specified");

            var stream = (Stream)p[0];
            if (p.Length > 1 && p[1] != null) Delimiter = p[1].ToString();

            var reader = new StreamReader(stream);

            MessageDefinition definition = MessagingManager.Read(MessageDefinitionType.Xml, messagename);

            return BreakIntoRecords(definition, reader).Select(record => record.State).ToList();
        }

        protected virtual IEnumerable<Record> BreakIntoRecords(MessageDefinition definition, StreamReader reader)
        {
            var currentLine = 1;
            var currentRecord = string.Empty;

            try
            {
                var records = new List<Record>();
                var recordDefinition = definition.Records[0];
                var table = new TableDescriber(recordDefinition.Name, DataSources.NoSource);

                if (Delimiter.IsNullOrEmpty()) Delimiter = recordDefinition.FieldSeparator;

                var fields = ReadCsvLine(reader, Delimiter);
                if (definition.HasHeader) fields = ReadCsvLine(reader, Delimiter);

                while (fields != null)
                {
                    var state = new DictionaryState { IsNew = true };

                    if (!recordDefinition.Fields.Exists(f => f.Name.Equals("ID", StringComparison.OrdinalIgnoreCase)))
                    {
                        state[new ColumnDescriber("ID", table, isIdentity: true)] = Guid.NewGuid();
                    }
                    state[new ColumnDescriber("TimeStamp", table, isTimestamp: true)] = null;

                    foreach (var fieldDefinition in recordDefinition.Fields)
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

        protected virtual void ReadRecordField(DictionaryState state, TableDescriber table, FieldDefinition fieldDefinition, string[] fields)
        {
            if (fieldDefinition.StartPosition >= fields.Length)
            {
                if (fieldDefinition.IsOptional) return;
                throw new MessagingException("Message does not match definition. Too few fields in current record.");
            }

            var describer = new ColumnDescriber(fieldDefinition.Name, table);

            if (fieldDefinition.Type.IsIn(FieldDefinitionType.DateTime))
            {
                state[describer] = DateTime.ParseExact(fields[fieldDefinition.StartPosition].Trim('"'), fieldDefinition.Format, CultureInfo.InvariantCulture);
            }
            else if (fieldDefinition.Type.IsIn(FieldDefinitionType.Amount, FieldDefinitionType.InvertedAmount, FieldDefinitionType.Decimal))
            {
                object oldvalue;

                decimal oldAmount = state.TryGetValue(describer, out oldvalue) && oldvalue is decimal
                                        ? (decimal) oldvalue
                                        : 0;

                decimal amount = !string.IsNullOrWhiteSpace(fields[fieldDefinition.StartPosition].Trim('"'))
                            ? Decimal.Parse(fields[fieldDefinition.StartPosition].Trim('"'),
                                fieldDefinition.Format.IsNullOrEmpty() ? CultureInfo.InvariantCulture : new CultureInfo(fieldDefinition.Format))
                            : 0;

                if (fieldDefinition.Type == FieldDefinitionType.InvertedAmount) amount *= -1;

                state[describer] = oldAmount + amount;
            }
            else if (fieldDefinition.Type == FieldDefinitionType.AmountSign)
            {
                if (IsNegativeAmountSign(fields[fieldDefinition.StartPosition], fieldDefinition))
                {
                    state[describer] = Decimal.Negate(((decimal)state[describer]));
                }
            }
            else if (fieldDefinition.Type == FieldDefinitionType.Number)
            {
                var number = !string.IsNullOrWhiteSpace(fields[fieldDefinition.StartPosition].Trim('"'))
                             ? int.Parse(fields[fieldDefinition.StartPosition].Trim('"'),
                               fieldDefinition.Format.IsNullOrEmpty() ? CultureInfo.InvariantCulture : new CultureInfo(fieldDefinition.Format))
                             : null as int?;

                state[describer] = number;
            }
            else
            {
                object oldvalue;
                state[describer] = state.TryGetValue(describer, out oldvalue)
                                       ? oldvalue + " " + fields[fieldDefinition.StartPosition]
                                       : fields[fieldDefinition.StartPosition];
            }
        }

        protected virtual bool IsNegativeAmountSign(string value, FieldDefinition fieldDefinition)
        {
            return fieldDefinition.Format.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        public virtual object Commit(string messagename, params object[] p)
        {
            MessageDefinition definition = MessagingManager.Read(MessageDefinitionType.Xml, messagename);

            if (definition.Records.Count == 0)
                throw new MessagingException("Empty message definition.");

            var lines = new List<string>();

            if (definition.HasHeader)
                lines.Add(ConstructHeader(definition.Records[0]));

            var internalStates = p[0] as IEnumerable<IInternalState>;

            if (internalStates != null)
                lines.AddRange(internalStates.Select(state => CreateLine(definition.Records[0], state)));

            return Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(string.Join("\r\n", lines))).ToArray();
        }

        private string CreateLine(RecordDefinition recordDefinition, IInternalState state)
        {
            var columns = new List<string>();
            int currentPosition = 0;

            foreach (FieldDefinition fieldDefinition in recordDefinition.Fields)
            {
                while (currentPosition < fieldDefinition.StartPosition)
                {
                    columns.Add(string.Empty);
                    currentPosition++;
                }

                columns.Add(FormatFieldValue(GetValueForField(fieldDefinition, state), Delimiter ?? recordDefinition.FieldSeparator));
                currentPosition++;
            }

            return string.Join(Delimiter ?? recordDefinition.FieldSeparator, columns);
        }

        private static string FormatFieldValue(string value, string separator)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            value = value.Replace("\"", "\"\"");

            if (value.ContainsOneOf("\n", "\"", separator) || value.StartsWith(" ") || value.EndsWith(" "))
                return string.Format("\"{0}\"", value);

            return value;
        }

        private static string GetValueForField(FieldDefinition fieldDefinition, IInternalState state)
        {
            string format = fieldDefinition.Format;
            var columnDescriber = new ColumnDescriber(fieldDefinition.Name, fieldDefinition.Table);

            if(fieldDefinition.Type.IsIn(FieldDefinitionType.DateTime))
            {
                var dateTime = state.Get<DateTime?>(columnDescriber);

                if (!dateTime.HasValue)
                    return fieldDefinition.Default;

                // DateTime needs a format specifier
                return !format.IsNullOrEmpty() ? dateTime.Value.ToString(format, CultureInfo.InvariantCulture) : dateTime.Value.ToShortDateString();
            }

            if(fieldDefinition.Type.IsIn(FieldDefinitionType.Amount, FieldDefinitionType.InvertedAmount, FieldDefinitionType.Decimal))
            {
                var money = state.Get<Money>(columnDescriber);

                if (money.IsEmpty) return fieldDefinition.Default;
                if (fieldDefinition.Type == FieldDefinitionType.InvertedAmount) money *= -1;
                
                return ToString(money, format);
            }

            if(fieldDefinition.Type.IsIn(FieldDefinitionType.Int16, FieldDefinitionType.Int32, FieldDefinitionType.Int64))
            {
                var value = state.Get<long?>(columnDescriber);

                return !value.HasValue ? fieldDefinition.Default : ToString(value, format);
            }

            return !state.Get<string>(columnDescriber).IsNullOrEmpty() ? state.Get<string>(columnDescriber) : fieldDefinition.Default;
        }

        private static string ToString(IFormattable value, string format)
        {
            if (format.IsNullOrEmpty()) return value.ToString(); // return value.ToString(null, CultureInfo.InvariantCulture); // w/o format Money is formatted dirrerently...

            CultureInfo cultureInfo;

            return TryGetCultureInfo(format, out cultureInfo) ? value.ToString(null, cultureInfo) : value.ToString(format, CultureInfo.InvariantCulture);
        }

        private static bool TryGetCultureInfo(string format, out CultureInfo cultureInfo)
        {
            cultureInfo = null;

            if(!format.IsNullOrEmpty())
            {
                try
                {
                    cultureInfo = CultureInfo.GetCultureInfo(format);
                    return true;
                }
                catch
                { }
            }
            return false;
        }

        private string ConstructHeader(RecordDefinition recordDefinition)
        {
            var header = new List<string>();
            int currentPosition = 0;

            foreach (FieldDefinition fieldDefinition in recordDefinition.Fields)
            {
                while (currentPosition < fieldDefinition.StartPosition)
                {
                    header.Add(string.Empty);
                    currentPosition++;
                }

                header.Add(fieldDefinition.Label);
                currentPosition++;
            }

            return string.Join(Delimiter ?? recordDefinition.FieldSeparator, header);
        }

        public IInternalState GetEmpty(string messagename, string tablename)
        {
            throw new NotSupportedException();
        }

        protected static string[] ReadCsvLine(StreamReader reader, string separator)
        {
            var line = reader.ReadLine();

            if (line == null) return null;

            while (line.Count(c => c == '"') % 2 != 0)
            {
                var nextLine = reader.ReadLine();

                if (nextLine == null) throw new MessagingException("Unable to import file, missing quote in the last line.");

                line += Environment.NewLine + nextLine;
            }

            return line.SplitCsvLine(separator);
        }
    }
}
