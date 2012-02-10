using System;
using System.Diagnostics;
using Adf.Core.Authorization;
using Adf.Core.Logging;

namespace Adf.Base.Logging
{
    /// <summary>
    /// Represents logger for logging debug related message or <see cref="System.Exception"/>.
    /// Provides methods to log debug related message or <see cref="System.Exception"/>.
    /// </summary>
    public class DebugLogProvider : ILogProvider
    {
        /// <summary>
        /// Logs the specified message with the specified user information and the default <see cref="LogLevel"/> set at Warming.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="user">The user information to log.</param>
        public void Log(string message, IUser user = null)
        {
            Log(LogLevel.Warning, message, user);
        }

        /// <summary>
        /// Logs the specified message with the specified <see cref="LogLevel"/> and user information.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel"/> to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="user">The user information to log.</param>
        public void Log(LogLevel level, string message, IUser user = null)
        {
            Debug.WriteLine(level + ": " + message);
        }

        /// <summary>
        /// Logs the specified <see cref="System.Exception"/> with the specified user information.
        /// </summary>
        /// <param name="exception">The <see cref="System.Exception"/> to log.</param>
        /// <param name="user">The user information to log.</param>
        public void Log(Exception exception, IUser user = null)
        {
            Debug.WriteLine(exception + Environment.NewLine + Environment.StackTrace);
        }
    }
}
