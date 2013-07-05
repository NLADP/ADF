using System.Linq;
using Adf.Core.Data;
using Adf.Core.Messaging;

namespace Adf.Base.Messaging
{
    public static class MessagingExtensions
    {
        public static FieldDefinition FindField(this RecordDefinition record, string name)
        {
            if (string.IsNullOrEmpty(name)) return FieldDefinition.Empty;
            if (record.Fields == null) return FieldDefinition.Empty;

            var fields = record.Fields.Where(f => f.Name == name);

            return fields.Count() > 0 ? fields.First() : FieldDefinition.Empty;
        }

        public static bool IsNumeric(this FieldDefinition definition)
        {
            return (definition.Type == FieldDefinitionType.Amount || definition.Type == FieldDefinitionType.Decimal || definition.Type == FieldDefinitionType.Int16 ||
                    definition.Type == FieldDefinitionType.Int32 || definition.Type == FieldDefinitionType.Int64 || definition.Type == FieldDefinitionType.Number ||
                    definition.Type == FieldDefinitionType.UInt16 ||definition.Type == FieldDefinitionType.UInt32 || definition.Type == FieldDefinitionType.UInt64 ||
                    definition.Type == FieldDefinitionType.InvertedAmount);
        }

        public static RecordDefinition FindRecord(this MessageDefinition message, string name)
        {
            if (string.IsNullOrEmpty(name)) return RecordDefinition.Empty;
            if (message.Records == null) return RecordDefinition.Empty;

            var records = message.Records.Where(r => r.Name == name);

            return (records.Count() > 0) ? records.First() : RecordDefinition.Empty;
        }

        public static Record Create(this RecordDefinition recordDefinition, IInternalState state)
        {
            return new Record {DomainObjectName = recordDefinition.DomainObjectName, State = state };
        }
    }
}
