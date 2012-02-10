using Adf.Core.Tasks;

namespace Adf.Core.Views
{
    /// <summary>
    /// Provide the functionality to view the UI part.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets a task name by the task identifier.
        /// </summary>
        /// <returns>The task name by the task identifier.</returns>
        ITask Task { get; }
    }
}