using Adf.Core.Tasks;
using Adf.Core.Test;
using Adf.Core.Views;

namespace Adf.Test.Views
{
    public class TestViewProvider : IViewProvider
    {
        #region Implementation of IViewProvider

        /// <summary>
        /// Activates the view of a specified task of <see cref="ITask"/>.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/> that defines task which will activated.</param>
        /// <param name="p">Variable no of objects used for future purpose.</param>
        public void ActivateView(ITask task, params object[] p)
        {
            TestManager.Register(TestItemType.View, task.Name, TestAction.ViewActivated);
        }

        /// <summary>
        /// Activates the view of a specified task of <see cref="ITask"/> and set the status of new task view for activation.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/> that defines task which will activated.</param>
        /// <param name="newView">The view status of a task which will set for activation.</param>
        /// <param name="p">Variable no of objects used for future purpose.</param>
        public void ActivateView(ITask task, bool newView, params object[] p)
        {
            TestManager.Register(TestItemType.View, task.Name, TestAction.ViewActivated);
        }

        /// <summary>
        /// Deactivates the view of a specified task of <see cref="ITask"/>.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/> that defines task which will deactivated.</param>
        /// <param name="p">Variable no of objects used for future purpose.</param>
        public void DeactivateView(ITask task, params object[] p)
        {
            TestManager.Register(TestItemType.View, task.Name, TestAction.ViewDeactivated);
        }

        #endregion
    }
}
