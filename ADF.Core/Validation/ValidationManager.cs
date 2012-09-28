using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Adf.Core.Extensions;
using Adf.Core.Objects;
using Adf.Core.State;

namespace Adf.Core.Validation
{
    /// <summary>
    /// Represents manager for doing all the validation related activities.
    /// Provides methods for validating objects, handling validations, adding validation results,   
    /// creating validating scope, clearing validating context etc.
    /// </summary>
    public static class ValidationManager
    {
        private static IEnumerable<IValidationPolicy> _policies;
        private static IEnumerable<IValidationHandler> _handlers;

        private static IValidationContext _context
        {
            get { return StateManager.Personal["IValidationContext"] as IValidationContext; }
            set { StateManager.Personal["IValidationContext"] = value; }
        }

        /// <summary>
        /// Gets a list of a particular type of validation policies.
        /// </summary>
        /// <returns>
        /// The list of a particular type of validation policies.
        /// </returns>
        private static IEnumerable<IValidationPolicy> Policies
        {
            get { lock(_polLock) return _policies ?? (_policies = ObjectFactory.BuildAll<IValidationPolicy>().ToList()); }
        }

        private static readonly object _polLock = new object();

        /// <summary>
        /// Gets a list of a particular type of validation handlers.
        /// </summary>
        /// <returns>
        /// The list of a particular type of validation handlers.
        /// </returns>
        private static IEnumerable<IValidationHandler> ValidationHandlers
        {
            get { lock(_handlerLock) return _handlers ?? (_handlers = ObjectFactory.BuildAll<IValidationHandler>().ToList()); }
        }

        private static readonly object _handlerLock = new object();

        /// <summary>
        /// Gets the validation context.
        /// </summary>
        /// <returns>
        /// The validation context.
        /// </returns>
        private static IValidationContext ValidationContext
        {
            get { lock (_contaxtLock) return _context ?? (_context = ObjectFactory.BuildUp<IValidationContext>());  }
        }

        private static readonly object _contaxtLock = new object();

        #region Clearing ValudationResults
        /// <summary>
        /// Clears the validation context.
        /// </summary>
        public static void Clear()
        {
            ValidationContext.Clear();
        }

        #endregion

        #region Validate
        /// <summary>
        /// Validates the specified validatable object.
        /// </summary>
        /// <param name="validatable">The validatable object to validate.</param>
        public static void Validate(object validatable)
        {
            if (validatable == null) throw new ArgumentNullException("validatable");

            foreach (var policy in Policies) { policy.Validate(validatable); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ValidationResult"/>s of the validation context are succeeded.
        /// </summary>
        /// <returns>
        /// true if the <see cref="ValidationResult"/>s are succeeded; otherwise, false.
        /// </returns>
        public static bool IsSucceeded
        {
            get { return ValidationContext.ValidationResults.IsSucceeded; }
        }

        #endregion

        #region Adding ValidationResults
        /// <summary>
        /// Adds the specified <see cref="ValidationResult"/> to the ValidationContext.
        /// </summary>
        /// <param name="validationResult">The <see cref="ValidationResult"/> to add.</param>
        public static void AddValidationResult(ValidationResult validationResult)
        {
            ValidationContext.AddValidationResult(validationResult);
        }

        /// <summary>
        /// Adds a <see cref="ValidationResult"/> with the warning message and arguments to the ValidationContext.
        /// </summary>
        /// <param name="message">The warning message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        public static void AddWarning(string message, params object[] args)
        {
            AddValidationResult(ValidationResult.CreateWarning(message, args));
        }

        public static void AddWarning(PropertyInfo property, string message, params object[] args)
        {
            AddValidationResult(ValidationResult.CreateWarning(property, message, args));
        }

        public static void AddWarning<T>(Expression<Func<T, object>> expression, string message, params object[] args)
        {
            AddWarning(expression.GetPropertyInfo(), message, args);
        }

        /// <summary>
        /// Adds a <see cref="ValidationResult"/> with the information message to the ValidationContext.
        /// </summary>
        /// <param name="message">The information message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        public static void AddInfo(string message, params object[] args)
        {
            AddValidationResult(ValidationResult.CreateInfo(message, args));
        }

        public static void AddInfo(PropertyInfo property, string message, params object[] args)
        {
            AddValidationResult(ValidationResult.CreateInfo(property, message, args));
        }

        public static void AddInfo<T>(Expression<Func<T, object>> expression, string message, params object[] args)
        {
            AddInfo(expression.GetPropertyInfo(), message, args);
        }

        /// <summary>
        /// Adds a <see cref="ValidationResult"/> with the specified error message and arguments to the 
        /// ValidationContext.
        /// </summary>
        /// <param name="message">The error message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        public static void AddError(string message, params object[] args)
        {
            AddValidationResult(ValidationResult.CreateError(message, args));
        }

        public static void AddError(PropertyInfo property, string message, params object[] args)
        {
            AddValidationResult(ValidationResult.CreateError(property, message, args));
        }

        /// <summary>
        /// Adds a <see cref="ValidationResult"/> with the specified property, error message and arguments to the ValidationContext.
        /// </summary>
        /// <param name="expression">The property of the <see cref="ValidationResult"/>.</param>
        /// <param name="message">The error message of the <see cref="ValidationResult"/>.</param>
        /// <param name="args">The arguments of the <see cref="ValidationResult"/>.</param>
        public static void AddError<T>(Expression<Func<T, object>> expression, string message, params object[] args)
        {
            AddError(expression.GetPropertyInfo(), message, args);
        }

        #endregion

        #region Handle Validations

        /// <summary>
        /// Gets the <see cref="ValidationResult"/>s of the ValidationContext.
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>s of the ValidationContext.
        /// </returns>
        public static ValidationResultCollection ValidationResults
        {
            get
            {
                return ValidationContext.ValidationResults;
            }
        }

        /// <summary>
        /// Handles the <see cref="ValidationResult"/>s with the specified parameters.
        /// </summary>
        /// <param name="p">The parameters to use for handing the <see cref="ValidationResult"/>s.</param>
        public static void Handle(params object[] p)
        {
            Handle(ValidationResults, true, p);
        }

        /// <summary>
        /// Handles the <see cref="ValidationResult"/>s and clears the 
        /// same depending on true or false is specified.
        /// </summary>
        /// <param name="clearResults">The value to indicate whether the <see cref="ValidationResult"/>s
        /// will be cleared or not.</param>
        public static void Handle(bool clearResults)
        {
            Handle(ValidationResults, clearResults);
        }

        /// <summary>
        /// Handles the specified <see cref="ValidationResultCollection"/>.
        /// </summary>
        /// <param name="validationResults">The <see cref="ValidationResultCollection"/> to handle.</param>
        public static void Handle(ValidationResultCollection validationResults)
        {
            Handle(validationResults, true);
        }

        /// <summary>
        /// Handles the specified <see cref="ValidationResultCollection"/> with the specified parameters.
        /// A value is specified to indicate whether the <see cref="ValidationResultCollection"/> will be
        /// cleared or not.
        /// </summary>
        /// <param name="validationResults">The <see cref="ValidationResultCollection"/> to handle.</param>
        /// <param name="clearResults">The value to indicate whether the 
        /// <see cref="ValidationResultCollection"/> will be cleared or not.</param>
        /// <param name="p">The parameters to use for handling the <see cref="ValidationResultCollection"/>.</param>
        public static void Handle(ValidationResultCollection validationResults, bool clearResults, params object[] p)
        {
            foreach (var handler in ValidationHandlers) { handler.Handle(validationResults, p); }

            if (clearResults) Clear();
        }

        #endregion

        #region ValidationScope
        /// <summary>
        /// Creates and returns a ValidationScope with the specified name.
        /// </summary>
        /// <param name="scopeName">The name of the ValidationScope to create and return.</param>
        /// <returns>
        /// The newly created ValidationScope.
        /// </returns>
        public static IDisposable CreateValidationScope(string scopeName)
        {
            return ValidationContext.CreateValidationScope(scopeName);
        }

        #endregion
    }
}