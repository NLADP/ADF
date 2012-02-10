namespace Adf.Core.Messaging
{
    public class FieldDefinition
    {
        /// <summary>
        /// Name of the field, needs to correspond to valid property name of the DomainObject.
        /// DomainObject is determined by the RecordDefinition.Name property.
        /// </summary>
        public string Name { get; set; }
        public FieldDefinitionType Type { get; set; }
        public string Format { get; set; }
        public int Length { get; set; }
        public bool IsOptional { get; set; }
        public int StartPosition { get; set; }
        public string Label { get; set; }
        public string Table { get; set; }
        public string Default { get; set; }

        public static FieldDefinition Empty
        {
            get { return new FieldDefinition {Type = FieldDefinitionType.Undefined };  }
        }

        public bool IsEmpty()
        {
            return Type == FieldDefinitionType.Undefined;
        }
    }
}
