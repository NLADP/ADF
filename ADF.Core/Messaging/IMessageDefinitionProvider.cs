using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Messaging
{
    public interface IMessageDefinitionProvider
    {
        MessageDefinition Read(string messageName, string section = null);
    }
}
