namespace Adf.Core.Validation
{
    /// <summary>
    /// Specifies different types of severities of a <see cref="ValidationResult"/>.
    /// Used to indicate the result of a validation.
    /// </summary>
    public enum ValidationResultSeverity
    {
        /// <summary>
        /// Success as a <see cref="ValidationResult"/>.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Informational as a <see cref="ValidationResult"/>.
        /// </summary>
        Informational = 1,

        /// <summary>
        /// Warning as a <see cref="ValidationResult"/>.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Error as a <see cref="ValidationResult"/>.
        /// </summary>
        Error = 3
    }
}