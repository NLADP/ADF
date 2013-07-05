using System.Collections.Generic;
using System.Linq;

namespace Adf.Core.Messaging
{
    public class RecordDefinition
    {
        /// <summary>
        /// Table and DomainObject name. Needs to correspond to a valid domainobject name
        /// </summary>
        public string Name { get; set; }
        public string DomainObjectName { get; set; }
        public string FieldSeparator { get; set; }
        public int Repeats { get; set; }
        public int? StartingPositionInMessage { get; set; }
        public List<FieldDefinition> Fields { get; set; }

        public RecordDefinition()
        {
            Fields = new List<FieldDefinition>();
        }

        public int Length
        {
            get { return Fields.Sum(f => f.Length); }
        }

        public static RecordDefinition Empty
        {
            get { return new RecordDefinition(); }
        }

        public bool IsEmpty()
        {
            return Name == null;
        }
    }
}
