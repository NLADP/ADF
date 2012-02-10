using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Authorization;
using Adf.Core.Objects;
using Adf.Core.State;

namespace Adf.Core.Logging
{
    /// <summary>
    /// Represents manager for logging a message or <see cref="System.Exception"/>.
    /// Provides methods for logging message or <see cref="System.Exception"/>s.
    /// </summary>
    public static class LogManager
    {
        private static IEnumerable<ILogProvider> _loggers;

        private static object _lock = new object();

        /// <summary>
        /// Gets the list of loggers mentioned in the configuration file of the application.
        /// </summary>
        /// <returns>
        /// The list of loggers mentioned in the configuration file of the application.
        /// </returns>
        private static IEnumerable<ILogProvider> Loggers
        {
            get { lock(_lock) return _loggers ?? (_loggers = ObjectFactory.BuildAll<ILogProvider>().ToList()); }
        }

        /// <summary>
        /// Logs the specified message with the specified <see cref="LogLevel"/> and user information.
        /// </summary>
        /// <param name="user">The user information to log.</param>
        public static void Log(string message, IUser user = null, params object[] p)
        {
            message = StateManager.Settings.GetOrDefault(message, message);
            message = String.Format(message, p);

            var currentUser = GetCurrentUser();

            foreach (var logger in Loggers)
            {
                logger.Log(message, user ?? currentUser);
            }
        }

        /// <summary>
        /// Logs the specified message with the specified <see cref="LogLevel"/> and user information.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel"/> to log.</param>
        /// <param name="user">The user information to log.</param>
        public static void Log(LogLevel level, string message, IUser user = null)
        {
            var currentUser = GetCurrentUser();

            foreach (var logger in Loggers)
            {
                logger.Log(level, message, user ?? currentUser);
            }
        }

        /// <summary>
        /// Logs the specified <see cref="System.Exception"/> with the specified user information.
        /// </summary>
        /// <param name="exception">The <see cref="System.Exception"/> to log.</param>
        /// <param name="user">The user information to log.</param>
        public static void Log(Exception exception, IUser user = null)
        {
            IUser currentUser = GetCurrentUser();

            foreach (var logger in Loggers)
            {
                logger.Log(exception, user ?? currentUser);
            }
        }

        private static IUser GetCurrentUser()
        {
            // getting the current user when not logged in might throw an exception and cause a loop calling LogManager..
            return AuthorizationManager.IsLoggedOn ? AuthorizationManager.CurrentUser : null;
        }
    }
}
