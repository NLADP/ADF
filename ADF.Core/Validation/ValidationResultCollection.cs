using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Adf.Core.Extensions;

namespace Adf.Core.Validation
{
    /// <summary>
    /// Represents collection of <see cref="ValidationResult"/>s.
    /// Provides methods, properties to convert a ValidationResultCollection to a string, to check 
    /// whether the ValidationResultCollection contais error, warning, whether the 
    /// ValidationResultCollection succeedes, whether the ValidationResultCollection contains error 
    /// for a specified property etc.
    /// </summary>
    [Serializable]
    public class ValidationResultCollection : List<ValidationResult>
    {
        /// <summary>
        /// Initializes an instance of <see cref="ValidationResultCollection"/> class.
        /// </summary>
        public ValidationResultCollection()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="ValidationResultCollection"/> class with the 
        /// specified array of <see cref="ValidationResult"/>s.
        /// </summary>
        /// <param name="results">The array of <see cref="ValidationResult"/>s.</param>
        public ValidationResultCollection(IEnumerable<ValidationResult> results) : base(results)
        {
        }
        
        /// <summary>
        /// Converts the <see cref="ValidationResultCollection"/> to a string using the specified 
        /// string as the separator.
        /// </summary>
        /// <param name="separator">The separator.</param>
        /// <returns>
        /// The generated string.
        /// </returns>
        public string ConvertToString(string separator)
        {
            return ToArray().ConvertToString(separator);
        }
        

        private readonly List<ValidationResultCollection> _childResults = new List<ValidationResultCollection>();

        /// <summary>
        /// Gets the list of child <see cref="ValidationResultCollection"/>s of this <see cref="ValidationResultCollection"/>.
        /// </summary>
        /// <returns>
        /// The list of child <see cref="ValidationResultCollection"/>s of this 
        /// <see cref="ValidationResultCollection"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<ValidationResultCollection> ChildResults
        {
            get { return _childResults; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResultCollection"/> contains error.
        /// </summary>
        /// <returns>
        /// true if the <see cref="ValidationResultCollection"/> contains error; 
        /// otherwise, false.
        /// </returns>
        public bool ContainsErrorResult
        {
            get
            {
                bool result = Exists(validationResult => validationResult.IsError);

                if (!result && _childResults != null)
                {
                    result = _childResults.Exists(collection => collection.ContainsErrorResult);
                }
                return result;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResultCollection"/> contains warning.
        /// </summary>
        /// <returns>
        /// true if the <see cref="ValidationResultCollection"/> contains warning; 
        /// otherwise, false.
        /// </returns>
        public bool ContainsWarningResult
        {
            get
            {
                bool result = Exists(validationResult => validationResult.IsWarning);

                if (!result && _childResults != null)
                {
                    result = _childResults.Exists(collection => collection.ContainsWarningResult);
                }
                return result;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResultCollection"/> is succeeded.
        /// </summary>
        /// <returns>
        /// true if the <see cref="ValidationResultCollection"/> is succeeded; otherwise, false.
        /// </returns>
        public bool IsSucceeded
        {
            get
            {
                bool result = Exists(validationResult => validationResult.IsError || validationResult.IsWarning);

                if (!result && _childResults != null)
                {
                    result = _childResults.Exists(collection => collection.IsSucceeded);
                }
                return !result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResultCollection"/> contains
        /// error for the specified property.
        /// </summary>
        /// <param name="property">The property for which the checking is to be done.</param>
        /// <returns>
        /// true if the <see cref="ValidationResultCollection"/> contains
        /// error for the specified property; otherwise, false.
        /// </returns>
        public bool ContainsErrorForProperty(PropertyInfo property)
        {
            bool exists = Exists(validationResult => validationResult.AffectedProperty == property);

            if (!exists && _childResults != null)
            {
                exists = _childResults.Exists(collection => collection.ContainsErrorForProperty(property));
            }
            return exists;
        }
    }
}