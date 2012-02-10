namespace Adf.Core.Logging
{
    ///<summary>
    /// Possible levels for logging, as used in <see cref="ILogProvider"/>. <see cref="LogLevel"/> is implemented using the <see cref="Descriptor"/> pattern, which allows applications to extend and add their own log levels.
    ///</summary>
    public class LogLevel : Descriptor
    {
        public static readonly LogLevel Unspecified = new LogLevel("Unspecified", 0);
        public static readonly LogLevel Verbose = new LogLevel("Verbose", 10);
        public static readonly LogLevel Informational = new LogLevel("Informational", 20);
        public static readonly LogLevel Warning = new LogLevel("Warning", 30);
        public static readonly LogLevel Error = new LogLevel("Error", 40);

        public LogLevel(string name, int order) : base(name, order: order)
        {
        }
    }
}
