using System.Collections;

namespace Adf.Core.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class TestItem
    {
        public TestItemType Type { get; set; }
        public object Subject { get; set; }
        public TestAction Action { get; set; }
        public IList Parameters { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Type, Subject, Action);
        }
    }
}
