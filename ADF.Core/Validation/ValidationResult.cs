using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Adf.Core.Resources;
using Adf.Core.State;

namespace Adf.Core.Validation
{
    /// <summary>
    /// Represents result after validation.
    /// Provides properties and methods to get a success ValidationResult, to get the severity, message
    /// of the ValidationResult, to know whether the severity of the ValidationResult is error, 
    /// warning or success, to get the affected property of the ValidationResult, to create error, 
    /// warning, informational ValidationResult with the specified message etc.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    [Serializable]
    public struct ValidationResult
    {
        private static readonly ValidationResult _successObject = new ValidationResult(ValidationResultSeverity.Success, string.Empty, null);

        private readonly ValidationResultSeverity _severity;
        private readonly string _message;
        private readonly object[] _args;
        private readonly PropertyInfo _property;

        /// <summary>
        /// Initializes an instance of the <see cref="ValidationResult"/> class with the specified 
        /// <see cref="ValidationResultSeverity"/>, message, property and arguments.
        /// </summary>
        /// <param name="severity">The <see cref="ValidationResultSeverity"/> of the <see cref="ValidationResult"/>.</param>
        /// <param name="message">The message of the <see cref="ValidationResult"/>.</param>
        /// <param name="property">The property of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        private ValidationResult(ValidationResultSeverity severity, string message, PropertyInfo property, params object[] args)
        {
            _severity = severity;
            _message = ResourceManager.GetString(StateManager.Settings[message].ToString(), CultureInfo.CurrentUICulture); 
            _property = property;
            _args = args;
        }
        
        /// <summary>
        /// Converts the <see cref="ValidationResult"/> to a string using.
        /// Returns the message of the <see cref="ValidationResult"/> formatted with its arguments.
        /// </summary>
        /// <returns>
        /// The message of the <see cref="ValidationResult"/> formatted with its arguments.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentUICulture, _message, _args);
//            return string.Format(CultureInfo.InvariantCulture, _message, _args);
        }

        public string Title
        {
            get { return ToString(); }
        }

        
        /// <summary>
        /// Gets a <see cref="ValidationResult"/> with the <see cref="ValidationResultSeverity"/>
        /// as 'Success', empty message and no property.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResult"/> with the <see cref="ValidationResultSeverity"/>
        /// as 'Success', empty message and no property.
        /// </returns>
        public static ValidationResult Success
        {
            get { return _successObject; }
        }

        /// <summary>
        /// Gets the <see cref="ValidationResultSeverity"/> of the <see cref="ValidationResult"/>.
        /// </summary>
        /// <returns>The <see cref="ValidationResultSeverity"/> of the <see cref="ValidationResult"/>.</returns>
        public ValidationResultSeverity Severity
        {
            get { return _severity; }
        }

        /// <summary>
        /// Gets the massage of the <see cref="ValidationResult"/>.
        /// </summary>
        /// <returns>
        /// The massage of the <see cref="ValidationResult"/>.
        /// </returns>
        public string Message
        {
            get { return _message; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Error'.
        /// </summary>
        /// <returns>true if the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Error'; otherwise, false.</returns>
        public bool IsError
        {
            get { return _severity == ValidationResultSeverity.Error; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Warning'.
        /// </summary>
        /// <returns>true if the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Warning'; otherwise, false.</returns>
        public bool IsWarning
        {
            get { return _severity == ValidationResultSeverity.Warning; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Informational'.
        /// </summary>
        /// <returns>true if the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Informational'; otherwise, false.</returns>
        public bool IsInformational
        {
            get { return Severity == ValidationResultSeverity.Informational; }
        }

        /// <summary>
        /// Indicates whether or not the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Success'.
        /// </summary>
        /// <returns>true if the <see cref="ValidationResultSeverity"/> of the 
        /// <see cref="ValidationResult"/> is 'Success'; otherwise, false.</returns>
        public bool IsSuccess
        {
            get { return _severity == ValidationResultSeverity.Success; }
        }

        /// <summary>
        /// Gets the affected property of the <see cref="ValidationResult"/>.
        /// </summary>
        /// <returns>
        /// The affected property of the <see cref="ValidationResult"/>.
        /// </returns>
        public PropertyInfo AffectedProperty
        {
            get { return _property; }
        }

        /// <summary>
        /// Creates a new <see cref="ValidationResult"/> with the 
        /// <see cref="ValidationResultSeverity"/> as 'Error' using the specified message and arguments.
        /// </summary>
        /// <param name="message">The message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        /// <returns>
        /// The newly created <see cref="ValidationResult"/>.
        /// </returns>
        public static ValidationResult CreateError(string message, params object[] args)
        {
            return new ValidationResult(ValidationResultSeverity.Error, message, null, args);
        }
               
        /// <summary>
        /// Creates a new <see cref="ValidationResult"/> with the 
        /// <see cref="ValidationResultSeverity"/> as 'Warning' using the specified message and arguments.
        /// </summary>
        /// <param name="message">The message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        /// <returns>
        /// The newly created <see cref="ValidationResult"/>.
        /// </returns>
        public static ValidationResult CreateWarning(string message, params object[] args)
        {
            return new ValidationResult(ValidationResultSeverity.Warning, message, null, args);
        }
        
        /// <summary>
        /// Creates a new <see cref="ValidationResult"/> with the 
        /// <see cref="ValidationResultSeverity"/> as 'Informational' using the specified message and arguments.
        /// </summary>
        /// <param name="message">The message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        /// <returns>
        /// The newly created <see cref="ValidationResult"/>.
        /// </returns>
        public static ValidationResult CreateInfo(string message, params object[] args)
        {
            return new ValidationResult(ValidationResultSeverity.Informational, message, null, args);
        }

        /// <summary>
        /// Creates a new <see cref="ValidationResult"/> with the 
        /// <see cref="ValidationResultSeverity"/> as 'Error' using the specified property, message 
        /// and arguments.
        /// </summary>
        /// <param name="property">The property of the <see cref="ValidationResult"/>.</param>
        /// <param name="message">The message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        /// <returns>
        /// The newly created <see cref="ValidationResult"/>.
        /// </returns>
        public static ValidationResult CreateError(PropertyInfo property, string message, params object[] args)
        {
            return new ValidationResult(ValidationResultSeverity.Error, message, property, args);
        }
        
        /// <summary>
        /// Creates a new <see cref="ValidationResult"/> with the 
        /// <see cref="ValidationResultSeverity"/> as 'Error' using the specified property, message 
        /// and arguments.
        /// </summary>
        /// <param name="property">The property of the <see cref="ValidationResult"/>.</param>
        /// <param name="message">The message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        /// <returns>
        /// The newly created <see cref="ValidationResult"/>.
        /// </returns>
        public static ValidationResult CreateInfo(PropertyInfo property, string message, params object[] args)
        {
            return new ValidationResult(ValidationResultSeverity.Informational, message, property, args);
        }
        
        /// <summary>
        /// Creates a new <see cref="ValidationResult"/> with the 
        /// <see cref="ValidationResultSeverity"/> as 'Error' using the specified property, message 
        /// and arguments.
        /// </summary>
        /// <param name="property">The property of the <see cref="ValidationResult"/>.</param>
        /// <param name="message">The message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        /// <returns>
        /// The newly created <see cref="ValidationResult"/>.
        /// </returns>
        public static ValidationResult CreateWarning(PropertyInfo property, string message, params object[] args)
        {
            return new ValidationResult(ValidationResultSeverity.Warning, message, property, args);
        }
    }
}