using Adf.Core.Tasks;

namespace Adf.Base.Tasks
{
    public interface ISearchableTask : ITask
    {
        void ContinueFromSearch(string arg);
    }
}
