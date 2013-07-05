namespace Adf.Core.Test
{
    ///<summary>
    /// Possible types for test items, as used in <see cref="TestItem"/>. <see cref="TestItemType"/> is implemented using the <see cref="Descriptor"/> pattern.
    ///</summary>
    public class TestItemType : Descriptor
    {
        public static readonly TestItemType ValidationResult = new TestItemType("ValidationResult");
        public static readonly TestItemType View = new TestItemType("View");
        public static readonly TestItemType Task = new TestItemType("Task");
        public static readonly TestItemType Error = new TestItemType("Error");
        public static readonly TestItemType Mail = new TestItemType("Mail");

        public TestItemType(string name) : base(name)
        {
        }
    }
}
