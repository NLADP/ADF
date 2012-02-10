using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Adf.Core.Data;

namespace Adf.Core.Messaging
{
    public interface IMessageHandler
    {
        object Retrieve(string messagename, params object[] p);
        object Commit(string messagename, params object[] p);
        IInternalState GetEmpty(string messagename, string tablename);
    }
}
