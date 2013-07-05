using System;
using Adf.Core.Domain;

namespace Adf.WinRT.UI.Events
{
    public delegate void DomainObjectClickEventHandler(object sender, IDomainObject e);

    public class DomainObjectClickEventArgs : EventArgs
    {
        public IDomainObject Object { get; set; }

        public DomainObjectClickEventArgs(IDomainObject o)
        {
            Object = o;
        }

    }
}
