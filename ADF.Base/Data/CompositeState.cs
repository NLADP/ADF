using System.Collections.Generic;
using System.Linq;
using Adf.Core.Data;
using Adf.Core.Identity;

namespace Adf.Base.Data
{
    public class CompositeState : IInternalState
    {
        protected Dictionary<ITable, IInternalState> _states = new Dictionary<ITable, IInternalState>();

        public CompositeState(Dictionary<ITable, IInternalState> states)
        {
            _states = states;
        }

        public void Add(ITable table, IInternalState state)
        {
            _states.Add(table, state);
        }

        public Dictionary<ITable, IInternalState> States
        {
            get
            {
                return _states;
            }
        }

        public ID ID { get; set; }

        public bool IsEmpty
        {
            get { return _states.All(state => state.Value.IsEmpty); }
        }

        public bool IsAltered
        {
            get { return _states.Any(state => state.Value.IsAltered); }
        }

        public bool IsNew
        {
            get { return _states.All(state => state.Value.IsNew); }
        }

        public bool Has(IColumn property)
        {
            return _states.Any(s => s.Key.Name == property.Table.Name && s.Value.Has(property));
        }

        public T Get<T>(IColumn property)
        {
            var state = _states.FirstOrDefault(s => s.Key.Name == property.Table.Name).Value;

            return (state == null) ? default(T) : state.Get<T>(property);
        }

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>The column value as it is.</returns>
        public object Get(IColumn property)
        {
            var state = _states.FirstOrDefault(s => s.Key.Name == property.Table.Name).Value;

            return state == null ? null : state.Get(property);
        }

        public void Set<T>(IColumn property, T value)
        {
            var state = _states.FirstOrDefault(s => s.Key.Name == property.Table.Name).Value;

            if (state != null) state.Set(property, value);
        }
    }
}
