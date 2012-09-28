using System.Collections.Generic;
using System.Linq;
using Adf.Core.Data;

namespace Adf.Base.Data
{
    public static class InternalStateExtensions
    {
        public static IInternalState Merge(this IInternalState state, params IInternalState[] states)
        {
            CompositeState s = (state is CompositeState) ? (CompositeState) state : new CompositeState(state);
            s.States.AddRange(states);
            return s;
        }

        public static IInternalState Merge(this IInternalState state, IEnumerable<IInternalState> states)
        {
            CompositeState s = (state is CompositeState) ? (CompositeState) state : new CompositeState(state);
            s.States.AddRange(states);
            return s;
        }

        public static IEnumerable<IInternalState> Merge(this IEnumerable<IInternalState> states, params IInternalState[] statesToMerge)
        {
            int i = 0;
            List<IInternalState> s = states.Select(state => statesToMerge.Length > i ? state.Merge(statesToMerge[i++]) : state).ToList();
            return s;
        }

        public static IEnumerable<IInternalState> Merge(this IEnumerable<IInternalState> states, IEnumerable<IInternalState> statesToMerge)
        {
            IEnumerator<IInternalState> stateToMergeEnum = statesToMerge.GetEnumerator();
            return (from state in states let mergeState = stateToMergeEnum.MoveNext() ? stateToMergeEnum.Current : null select mergeState != null ? state.Merge(mergeState) : state).ToList();
        }
    }
}
