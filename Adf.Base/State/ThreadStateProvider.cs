using System.Collections.Generic;
using System.Threading;
using Adf.Core.State;

namespace Adf.Base.State
{
    public class ThreadStateProvider : IStateProvider
    {
        private const string Format = "{0}:{1}";

        private static readonly Dictionary<int, Dictionary<object, object>> AllState = new Dictionary<int, Dictionary<object, object>>();
        
        private readonly object _lock = new object();

        private Dictionary<object, object> State
        {
            get
            {
                lock (_lock)
                {
                    Dictionary<object, object> state;
                    if (!AllState.TryGetValue(Thread.CurrentThread.ManagedThreadId, out state))
                    {
                        state = new Dictionary<object, object>();
                        AllState[Thread.CurrentThread.ManagedThreadId] = state;
                    }
                    return state;
                }
            }
        }

        #region IState Members

        public bool Has(object o)
        {
            return State.ContainsKey(o);
        }

        public bool Has(string key)
        {
            return State.ContainsKey(key);
        }

        public bool Has(object o, string key)
        {
            return State.ContainsKey(string.Format(Format, o, key));
        }

        public object this[object o]
        {
            get { return (o != null) ? State[o] : null; }
            set { if (o != null) State[o] = value; }
        }

        public object this[string key]
        {
            get
            {
                if (key == null) return null;

                object value;

                State.TryGetValue(key, out value);

                return value;
            }
            set { if (key != null) State[key] = value; }
        }

        public object this[object o, string key]
        {
            get
            {
                if (key == null || o == null) return null;

                key = string.Format(Format, o, key);

                object value;

                State.TryGetValue(key, out value);

                return value;
            }
            set { if ((key != null) & (o != null)) State[string.Format(Format, o, key)] = value; }
        }

        public void Remove(string key)
        {
            if (key != null) State.Remove(key);
        }

        public void Remove(object o)
        {
            if (o != null) State.Remove(o);
        }

        #endregion
    }
}
