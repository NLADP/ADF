using System;
using Adf.Core.Authorization;

namespace Adf.Core.Logging
{
    /// <summary>
    /// Defines methods that a class implements to log message, exception etc.
    /// </summary>
    public interface ILogProvider
    {
        /// <summary>
        /// Logs the specified message and the user information.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="user">The user information to log.</param>
        void Log(string message, IUser user = null);

        /// <summary>
        /// Logs the specified message with the specified loglevel and user information.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The loglevel to log.</param>
        /// <param name="user">The user information to log.</param>
        void Log(LogLevel level, string message, IUser user = null);

        /// <summary>
        /// Logs the specified <see cref="System.Exception"/> and the user information.
        /// </summary>
        /// <param name="exception">The <see cref="System.Exception"/> to log.</param>
        /// <param name="user">The user information to log.</param>
        void Log(Exception exception, IUser user = null);
    }
}
