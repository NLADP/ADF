using Adf.Core.Tasks;

namespace Adf.Core.Views
{
    /// <summary>
    /// Repesents methods to activate or deactivate the task view.
    /// </summary>
    public interface IViewProvider
    {
        /// <summary>
        /// Activates the view of a specified task of <see cref="ITask"/>.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/> that defines task which will activated.</param>
        /// <param name="p">Variable no of objects used for future purpose.</param>
        void ActivateView(ITask task, params object[] p);

        /// <summary>
        /// Activates the view of a specified task of <see cref="ITask"/> and set the status of new task view for activation.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/> that defines task which will activated.</param>
        /// <param name="newView">The view status of a task which will set for activation.</param>
        /// <param name="p">Variable no of objects used for future purpose.</param>
        void ActivateView(ITask task, bool newView, params object[] p);

        /// <summary>
        /// Deactivates the view of a specified task of <see cref="ITask"/>.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/> that defines task which will deactivated.</param>
        /// <param name="p">Variable no of objects used for future purpose.</param>
        void DeactivateView(ITask task, params object[] p);
    }
}
