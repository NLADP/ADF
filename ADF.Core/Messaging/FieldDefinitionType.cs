namespace Adf.Core.Messaging
{
    public class FieldDefinitionType : Descriptor
    {
        public FieldDefinitionType(string name) : base(name) {}
        public FieldDefinitionType(string name, bool isDefault) : base(name, isDefault) {}

        public static readonly FieldDefinitionType Undefined = new FieldDefinitionType("Undefined");
        public static readonly FieldDefinitionType String = new FieldDefinitionType("String", Default);
        public static readonly FieldDefinitionType Number = new FieldDefinitionType("Number");
        public static readonly FieldDefinitionType Amount = new FieldDefinitionType("Amount");
        public static readonly FieldDefinitionType DateTime = new FieldDefinitionType("DateTime");
        public static readonly FieldDefinitionType Boolean = new FieldDefinitionType("Boolean");
        public static readonly FieldDefinitionType Record = new FieldDefinitionType("Record");
        public static readonly FieldDefinitionType Byte = new FieldDefinitionType("Byte");
        public static readonly FieldDefinitionType SByte = new FieldDefinitionType("SByte");
        public static readonly FieldDefinitionType Decimal = new FieldDefinitionType("Decimal");
        public static readonly FieldDefinitionType Int64 = new FieldDefinitionType("Int64");
        public static readonly FieldDefinitionType UInt64 = new FieldDefinitionType("UInt64");
        public static readonly FieldDefinitionType Int32 = new FieldDefinitionType("Int32");
        public static readonly FieldDefinitionType UInt32 = new FieldDefinitionType("UInt32");
        public static readonly FieldDefinitionType Int16 = new FieldDefinitionType("Int16");
        public static readonly FieldDefinitionType UInt16 = new FieldDefinitionType("UInt16");
        public static readonly FieldDefinitionType AmountSign = new FieldDefinitionType("AmountSign");
        public static readonly FieldDefinitionType InvertedAmount = new FieldDefinitionType("InvertedAmount");
    }
}
