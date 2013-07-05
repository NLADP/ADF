using System;
using System.Runtime.Serialization;
using Adf.Base.State;
using Adf.Core.State;
using Newtonsoft.Json;

namespace Adf.Web.State
{
    [Serializable]
    public class SerializableWrapper : ISerializable
    {
        private readonly object _value;

        public SerializableWrapper(object value)
        {
            _value = value;
        }

        protected SerializableWrapper(SerializationInfo info, StreamingContext context)
        {
            _value = JsonConvert.DeserializeObject(info.GetString("_value"));
        }

        public object Value { get { return _value; } }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_value", JsonConvert.SerializeObject(_value));
        }
    }

	public class SerializableWebStateProvider : IStateProvider
	{
        private readonly IStateProvider _sessionProvider = new WebStateProvider();

	    private const string Key = "SWSPState";

	    private IStateProvider Provider
	    {
	        get
	        {
                var state = _sessionProvider[Key] as SerializableWrapper;
                if (state == null)
                {
                    state = new SerializableWrapper(new SimpleStateProvider());
                    _sessionProvider[Key] = state;
                }
	            return (IStateProvider) state.Value;
	        }
	    }

		public object this[string key]
		{
            get { return Provider[key]; }
            set { Provider[key] = value; }
		}

		public bool Has(object o)
		{
            return Provider.Has(o);
		}

		public bool Has(string key)
		{
			return Provider.Has(key);
		}

		public bool Has(object o, string key)
		{
			return Provider.Has(o, key);
		}

		public object this[object o]
		{
            get { return Provider[o]; }
			set { Provider[o] = value; }
		}

		public object this[object o, string key]
		{
            get { return Provider[o, key]; }
			set { Provider[o, key] = value; }
		}

		public void Remove(string key)
		{
			Provider.Remove(key);
		}

        public void Remove(object o)
		{
			Provider.Remove(o);
		}

        public void Clear()
        {
            Provider.Clear();
        }
	}
}