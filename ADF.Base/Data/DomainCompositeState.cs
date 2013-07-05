using System.Collections.Generic;
using System.Linq;
using Adf.Base.Domain;
using Adf.Core.Data;
using Adf.Core.Identity;

namespace Adf.Base.Data
{
    public class DomainCompositeState : IInternalState
    {
        protected Dictionary<string, IInternalState> states = new Dictionary<string, IInternalState>();

        public DomainCompositeState(params DomainObject[] domainObjects)
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

        public bool Has(IColumn property)
        {
            return states.ContainsKey(property.Table.Name);
        }

        public T Get<T>(IColumn property)
        {
            return states.ContainsKey(property.Table.Name) ? states[property.Table.Name].Get<T>(property) : default(T);
        }

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>The column value as it is.</returns>
        public object Get(IColumn property)
        {
            return states.ContainsKey(property.Table.Name) ? states[property.Table.Name].Get(property) : null;
        }

        public void Set<T>(IColumn property, T value)
        {
            if (states.ContainsKey(property.Table.Name)) states[property.Table.Name].Set(property, value);
        }
    }
}
