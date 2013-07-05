using Adf.Core.State;
using Windows.Storage;

namespace Adf.WinRT.State
{
    public class LocalStorageStateProvider : IStateProvider
    {
        protected ApplicationDataContainer Storage
        {
            get { return Windows.Storage.ApplicationData.Current.LocalSettings; }
        }

        public string Get(string key)
        {
            if (Storage.Values.ContainsKey(key))
                return Storage.Values[key].ToString();

            return null;
        }

        public void Set(string key, string value)
        {
            Storage.Values[key] = value;
        }

        public bool Has(object o)
        {
            throw new System.NotImplementedException();
        }

        public bool Has(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Has(object o, string key)
        {
            throw new System.NotImplementedException();
        }

        object IStateProvider.this[object o]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        object IStateProvider.this[string key]
        {
            get { return Get(key); }
            set { Set(key, value == null ? null : value.ToString()); }
        }

        object IStateProvider.this[object o, string key]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public void Remove(string key)
        {
            Storage.Values.Remove(key);
        }

        public void Remove(object o)
        {
            throw new System.NotImplementedException();
        }
    }
}
