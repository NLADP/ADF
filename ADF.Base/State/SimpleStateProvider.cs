using System.Collections.Generic;
using Adf.Core.State;

namespace Adf.Base.State
{
    public class SimpleStateProvider : IStateProvider
    {
        private const string Format = "{0}:{1}";
        
        private readonly Dictionary<object, object> _state = new Dictionary<object, object >();

        #region IState Members

        public bool Has(object o)
        {
            return _state.ContainsKey(o);
        }

        public bool Has(string key)
        {
            return _state.ContainsKey(key);
        }

        public bool Has(object o, string key)
        {
            return _state.ContainsKey(string.Format(Format, o, key));
        }

        public object this[object o]
        {
            get { return (o != null) ? _state[o] : null; }
            set { if (o != null) _state[o] = value; } 
        }

        public object this[string key]
        {
            get
            {
                if (key == null) return null;

                object value;
                _state.TryGetValue(key, out value);

                return value;
            }
            set { if (key != null) _state[key] = value; }
        }

        public object this[object o, string key]
        {
            get
            {
                if (key == null || o == null) return null;

                key = string.Format(Format, o, key);

                object value;
                _state.TryGetValue(key, out value);

                return value;
            }
            set { if ((key != null) & (o != null)) _state[string.Format(Format, o, key)] = value; }
        }

        public void Remove(string key)
        {
            if (key != null) _state.Remove(key);
        }

        public void Remove(object o)
        {
            if (o != null) _state.Remove(o);
        }

        public void Clear()
        {
            _state.Clear();
        }

        #endregion
    }
}
