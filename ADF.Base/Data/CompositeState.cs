using System.Collections.Generic;
using System.Linq;
using Adf.Base.Domain;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Base.Data
{
    public class CompositeState : IInternalState
    {
        protected Dictionary<string, IInternalState> states = new Dictionary<string, IInternalState>();

        public CompositeState(params DomainObject[] domainObjects)
        {
            AddRange(domainObjects);
        }

        public void Add(DomainObject domainObject)
        {
            states[domainObject.GetType().Name] = domainObject.GetState();
        }

        public void AddRange(params DomainObject[] domainObjects)
        {
            foreach (var domainObject in domainObjects)
            {
                Add(domainObject);
            }
        }

        public ID ID { get; set; }

        public bool IsEmpty
        {
            get { return states.All(state => state.Value.IsEmpty); }
        }

        public bool IsAltered
        {
            get { return states.Any(state => state.Value.IsAltered); }
        }

        public bool IsNew
        {
            get { return states.All(state => state.Value.IsNew); }
        }

        public T Get<T>(IColumn property)
        {
            return states.ContainsKey(property.Table.Name) ? states[property.Table.Name].Get<T>(property) : default(T);
        }

        public void Set<T>(IColumn property, T value)
        {
            if (states.ContainsKey(property.Table.Name)) states[property.Table.Name].Set(property, value);
        }

        public T? GetNullable<T>(IColumn property) where T : struct
        {
            return states.ContainsKey(property.Table.Name) ? states[property.Table.Name].GetNullable<T>(property) : null;
        }

        public void SetNullable<T>(IColumn property, T? value) where T : struct
        {
            if (states.ContainsKey(property.Table.Name)) states[property.Table.Name].SetNullable(property, value);
        }

        public T GetValue<T>(IColumn property) where T : IValueObject
        {
            return states.ContainsKey(property.Table.Name) ? states[property.Table.Name].GetValue<T>(property) : default(T);
        }

        public void SetValue<T>(IColumn property, T value) where T : IValueObject
        {
            if (states.ContainsKey(property.Table.Name)) states[property.Table.Name].SetValue(property, value);
        }
    }
}
