using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Adf.Core.Binding;

namespace Adf.Web.Binding
{
    public static class ControlBindingExtensions
    {
        public static Control Bind(this Control control, IEnumerable bindableObjects, params object[] p)
        {
            BindManager.Bind(control, bindableObjects, p);

            return control;
        }

        public static Control Bind(this Control control, object bindableObject, params object[] p)
        {
            BindManager.Bind(control, bindableObject, p);

            return control;
        }

        public static Control Persist(this Control control, object bindableObject, params object[] p)
        {
            BindManager.Persist(bindableObject, control, p);

            return control;
        }
    }
}
