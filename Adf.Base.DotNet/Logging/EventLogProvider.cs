using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Adf.Core;
using Adf.Core.Authorization;
using Adf.Core.Logging;
using Adf.Core.State;

namespace Adf.Base.Logging
{
    public class EventLogProvider : ILogProvider
    {
        private readonly EventLog appLog;

        public EventLogProvider()
        {
            var source = StateManager.Settings["EventLogSource"] as string;
            
            var log = StateManager.Settings["EventLog"] as string;

            appLog = new EventLog { Source = source, Log = log };
        }

        private static bool IsAllowed(LogLevel level)
        {
            var levelConfigured = StateManager.Settings["EventLogLevel"] as string;

            var config = Descriptor.Get<LogLevel>(levelConfigured) ?? LogLevel.Unspecified;
            
            return level.Order >= config.Order;
        }

        #region Implementation of ILogProvider

        /// <summary>
        /// Logs the specified message and the user information.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="user">The user information to log.</param>
        public void Log(string message, IUser user)
        {
            appLog.WriteEntry(message);
        }

        /// <summary>
        /// Logs the specified message with the specified loglevel and user information.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The loglevel to log.</param>
        /// <param name="user">The user information to log.</param>
        public void Log(LogLevel level, string message, IUser user)
        {
            message = string.Format("{0} - ({1}).", message, Assembly.GetCallingAssembly().FullName);

            if (IsAllowed(level))
            {
                if (level == LogLevel.Informational) appLog.WriteEntry(message, EventLogEntryType.Information);
                else if (level == LogLevel.Warning) appLog.WriteEntry(message, EventLogEntryType.Warning);
                else if (level == LogLevel.Error) appLog.WriteEntry(message, EventLogEntryType.Error);
                else appLog.WriteEntry(message, EventLogEntryType.Information);
            }
        }

        /// <summary>
        /// Logs the specified <see cref="System.Exception"/> and the user information.
        /// </summary>
        /// <param name="exception">The <see cref="System.Exception"/> to log.</param>
        /// <param name="user">The user information to log.</param>
        public void Log(Exception exception, IUser user)
        {
            if (!(exception is ThreadAbortException))
            {
                Log(LogLevel.Error, exception.ToString(), user);
            }
        }

        #endregion
    }
}
