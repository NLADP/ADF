using System;
using System.Globalization;
using System.IO;
using Adf.Core;
using Adf.Core.Authorization;
using Adf.Core.Logging;
using Adf.Core.State;
using Adf.Base.State;

namespace Adf.Base.Logging
{
    /// <summary>
    /// Provides functionality to log a messages or exception into a flat file. 
    /// This class does not use any logging frameworks and impliments <see cref="ILogProvider"/> interface.
    /// </summary>
    public class FlatFileLogProvider : ILogProvider
    {
        private readonly string logFileName = string.Empty;
        private readonly string logFilePath = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatFileLogProvider"/> class with no argument.
        /// Note that the logfile path should be mentioned in web.config file key="FlatFileLogPath" before initializing the object.        
        /// </summary>
        public FlatFileLogProvider()
        {
            if (StateManager.Settings.Has("FlatFileLogPath"))
            {
                logFilePath =
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                 StateManager.Settings["FlatFileLogPath"].ToString());


                //logFilePath = Path.Combine(System.Environment.CurrentDirectory,
                //                 StateManager.Settings["FlatFileLogPath"].ToString());

            }
            else
            {
                logFilePath = Path.GetTempPath();
            }

            string fileName = StateManager.Settings.Has("ApplicationName") ? StateManager.Settings["ApplicationName"] as string : "ADF";

            logFileName = string.Format(CultureInfo.InvariantCulture, @"{0}_{1}.log", fileName, GetDateForFilename());

            if (!string.IsNullOrEmpty(logFilePath))
            {
                logFilePath = Path.Combine(logFilePath, logFileName);
            }
        }

        #region ILogProvider implementation

        /// <summary>
        /// Log the specified error messages with current date.
        /// </summary>
        /// <param name="message">The error messages which will logged.</param>
        /// <param name="user">The Adf.Core.IDomainObject object - not in use.</param>
        public void Log(string message, IUser user)
        {
            if (message == null) throw new ArgumentNullException("message");

            try
            {
                lock (this)
                {
                    using (StreamWriter logWriter = OpenWriter())
                    {
                        logWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, @"({0} - {2}) {1}",
                                                          GetDateForMessage(), message, user));
//                        logWriter.Flush();
//                        logWriter.Close();
                    }
                }
            }
            catch
            {
                // this should not be logged, this could cause a hang-up
                //LogManager.Log(ex);
            }
        }

        /// <summary>
        /// Log the error message as per the specified log level.
        /// </summary>
        /// <param name="message">The error messages which will logged.</param>
        /// <param name="level">The Adf.Core.LogLevel that defines the comparison with configuration loglevel.</param>
        /// <param name="user">The Adf.Core.IDomainObject object - not in use.</param>
        public void Log(LogLevel level, string message, IUser user)
        {
            if (message == null) throw new ArgumentNullException("message");

            if (IsAllowed(level))
            {
                Log(message, user);
            }
        }

        /// <summary>
        /// Log the exception messages with frames on the call stack at the time the current exception was thrown.
        /// </summary>
        /// <param name="exception">The exception of <see cref="System.Exception"/> that will logged.</param>
        /// <param name="user">The Adf.Core.IDomainObject object - not in use.</param>
        public void Log(Exception exception, IUser user)
        {
            if (exception == null) throw new ArgumentNullException("exception");

            Log(LogLevel.Error, exception + Environment.NewLine + Environment.StackTrace, user);
        }

        #endregion ILogProvider implementation


        /// <summary>
        /// Creates a <see cref="System.IO.StreamWriter"/> that appends or create UTF-8 encoded text to an existing or newly created file respectively.
        /// </summary>
        /// <returns>
        /// A StreamWriter that appends the text if file exists; otherwise, create a file and write text.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Path is null.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The file path is invalid.</exception>
        /// <exception cref="System.NotSupportedException">Path is in an invalid format.</exception>
        private StreamWriter OpenWriter()
        {
            string path = string.Format(CultureInfo.InvariantCulture, logFilePath, GetDateForFilename());

            return File.Exists(path) ? File.AppendText(path) : File.CreateText(path);
        }

        /// <summary>
        /// Provides a string having current date in a format yyyy-MM-dd HH:mm:ss.
        /// Use the current date time for writing message in log file.
        /// </summary>
        /// <returns>The string having current date in a format yyyy-MM-dd HH:mm:ss.</returns>
        private static string GetDateForMessage()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Provides a string with current date in the format yyyyMMdd.
        /// Used to create the file name with current date.
        /// </summary>
        /// <returns>The string with current date in the format yyyyMMdd.</returns>
        private static string GetDateForFilename()
        {
            return DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Checks a given log level with configuartion loglevel.
        /// </summary>
        /// <param name="level">The Adf.Core.LogLevel that defines the log level which would compare with configuration loglevel.</param>
        /// <returns>True if given log level greater than or equal to the configuration loglevel; otherwise, false.</returns>
        private static bool IsAllowed(LogLevel level)
        {
            var levelConfigured = StateManager.Settings["FlatFileLogLevel"] as string;

            var config = Descriptor.Get<LogLevel>(levelConfigured) ?? LogLevel.Unspecified;

            return level.Order >= config.Order;
        }
    }
}
