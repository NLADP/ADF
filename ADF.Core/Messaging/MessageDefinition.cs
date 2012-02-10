﻿using System.Collections.Generic;

namespace Adf.Core.Messaging
{
    public class MessageDefinition
    {
        public MessageType Type { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string RecordSeparator { get; set; }
        public bool HasHeader { get; set; }
        public List<RecordDefinition> Records { get; set; }

        public MessageDefinition()
        {
            Records = new List<RecordDefinition>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
