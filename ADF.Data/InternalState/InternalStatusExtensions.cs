using Adf.Core.Domain;

namespace Adf.Data.InternalState
{
    ///<summary>
    /// Extension to help implement Internal Status changes.
    ///</summary>
    public static class InternalStatusExtensions
    {
        private static readonly InternalStatusEngine engine = new InternalStatusEngine();

        /// <summary>
        /// Changes the current <see cref="InternalStatus"/> to the desired <see cref="InternalStatus"/>.
        /// </summary>
        /// <param name="current">Current <see cref="InternalStatus"/></param>
        /// <param name="desired">The desired <see cref="InternalStatus"/>.</param>
        /// <returns>
        /// If <see cref="InternalStatusEngine"/> of this instance exists the method 
        /// returns the desired <see cref="InternalStatus"/>, otherwise the current 
        /// <see cref="InternalStatus"/> is returned.
        /// </returns>
        public static InternalStatus DetermineStatus(this InternalStatus current, InternalStatus desired)
        {
            return engine == null ? current : engine.ChangeStatus(current, desired);
        }
    }
}
