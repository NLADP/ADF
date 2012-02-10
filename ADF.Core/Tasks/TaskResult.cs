namespace Adf.Core.Tasks
{
    /// <summary>
    /// Defines the current status of a task.
    /// </summary>
    public class TaskResult : Descriptor
    {
        /// <summary>
        /// Represent the task status for successfull validation.
        /// </summary>
        public static readonly TaskResult ValidateTrue = new TaskResult("ValidateTrue");

        /// <summary>
        /// Represent the task status for validation failure.
        /// </summary>
        public static readonly TaskResult ValidateFalse = new TaskResult("ValidateFalse");

        /// <summary>
        /// Represent the task status for successfull completion of task.
        /// </summary>
        public static readonly TaskResult Ok = new TaskResult("Ok", true);

        /// <summary>
        /// Represent the task status to cancel a task.
        /// </summary>
        public static readonly TaskResult Cancel = new TaskResult("Cancel");

        /// <summary>
        /// Represent the task status for error task.
        /// </summary>
        public static readonly TaskResult Error = new TaskResult("Error");

        public TaskResult(string name, bool isDefault = false) : base(name, isDefault)
        {
        }
    }
}