namespace Adf.Core.Test
{
    public class TestAction : Descriptor
    {
        public static readonly TestAction ViewActivated = new TestAction("ViewActivated");
        public static readonly TestAction ViewDeactivated = new TestAction("ViewDeactivated");
        public static readonly TestAction TaskStarted = new TestAction("TaskStarted");
        public static readonly TestAction ValidationFailed = new TestAction("ValidationFailed");
        public static readonly TestAction ValidationSucceeded = new TestAction("ValidationSucceeded");

        public TestAction(string name) : base(name)
        {
        }
    }
}
