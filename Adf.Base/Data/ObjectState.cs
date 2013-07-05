using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Adf.Core.Data;
using Adf.Core.Extensions;
using Adf.Core.Identity;
using Adf.Core.Types;

namespace Adf.Base.Data
{
    public class ObjectState : IInternalState
    {
        private Dictionary<string, PropertyInfo> _infos;
        private Dictionary<string, PropertyInfo> Infos
        {
            get
            {
                if (_infos == null)
                {
                    _infos = new Dictionary<string, PropertyInfo>();
                    foreach (var propertyInfo in GetType().GetProperties())
                    {
                        _infos[propertyInfo.Name] = propertyInfo;
                    }
                }

                return _infos;
            }
        }

        [DataMember]
        public string Id { get; set; }

        [IgnoreDataMember]
        public ID ID
        {
            get { return new ID(Id); }
            set { Id = value.ToString(); }
        }

        public bool IsEmpty { get; private set; }
        public bool IsAltered { get; private set; }
        public bool IsNew { get; set; }

        public bool Has(IColumn property)
        {
            return Infos.ContainsKey(property.ColumnName);
        }

        public T Get<T>(IColumn property)
        {
            return Converter.To<T>(Infos[property.ColumnName].GetValue(this, null));
        }

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>The column value as it is.</returns>
        public object Get(IColumn property)
        {
            return Infos[property.ColumnName].GetValue(this, null);
        }

        public void Set<T>(IColumn property, T value)
        {
            Infos[property.ColumnName].SetValue(this, Converter.ToPrimitive(value), null);

            IsAltered = true;
        }

        public void AcceptChanges()
        {
            IsAltered = false;
            IsNew = false;
        }
    }
}
