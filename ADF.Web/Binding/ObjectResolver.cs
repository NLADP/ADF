using System;
using System.Collections;
using System.Collections.Generic;

namespace Adf.Web.Binding
{
    public class ObjectResolver : Dictionary<object, Func<IEnumerable>>
    {
    }
}
