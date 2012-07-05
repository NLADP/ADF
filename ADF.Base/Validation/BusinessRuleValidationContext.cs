using System;
using System.Collections.Generic;
using Adf.Core.Logging;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Represents BusinessRuleValidationContext to manage Context specific validation scope.
    /// </summary>
    [Serializable]
    public class BusinessRuleValidationContext : IValidationContext
    {
        readonly Stack<ValidationResultCollectionScope> _scope = new Stack<ValidationResultCollectionScope>();

        /// <summary>
        /// Initializes an instance of <see cref="BusinessRuleValidationContext"/>.
        /// </summary>
        public BusinessRuleValidationContext()
        {
            _scope.Push(new ValidationResultCollectionScope(this));
        }
        
        /// <summary>
        /// Creates and returns an instance of <see cref="ValidationResultCollectionScope"/> with the 
        /// supplied scope name.
        /// </summary>
        /// <param name="scopeName">The scope name.</param>
        /// <returns>The newly created instance of <see cref="ValidationResultCollectionScope"/></returns>
        public IDisposable CreateValidationScope(string scopeName)
        {
            _scope.Push(new ValidationResultCollectionScope(this));
            return _scope.Peek();
        }

        /// <summary>
        /// Clears the stack of type <see cref="ValidationResultCollectionScope"/> of this instance.
        /// </summary>
        public void Clear()
        {
            //TODO: audit Olaf Conijn
            _scope.Clear();
            _scope.Push(new ValidationResultCollectionScope(this));
        }

        /// <summary>
        /// Adds the supplied <see cref="ValidationResult"/> to its current <see cref="ValidationResultCollectionScope"/>.
        /// </summary>
        /// <param name="result">The supplied <see cref="ValidationResult"/>.</param>
        public void AddValidationResult(ValidationResult result)
        {
            var currentScope = CurrentScope;
            currentScope.ValidationResult.Add(result);

            if (result.Severity > ValidationResultSeverity.Informational)
            {
                LogManager.Log(LogLevel.Informational, result.ToString()); // log validation messages with informational level only for debugging purposes
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="ValidationResult"/> from its current <see cref="ValidationResultCollectionScope"/>.
        /// </summary>
        public ValidationResultCollection ValidationResults
        {
            get
            {
                if (_scope.Count == 0) _scope.Push(new ValidationResultCollectionScope(this));
                var currentScope = CurrentScope;
                return currentScope.ValidationResult;
            }
        }

        /// <summary>
        /// Deletes the current scope and merges it to the next to the current scope.
        /// </summary>
        private void CloseScope()
        {
            try
            {
                var closedScope = _scope.Pop();
                if (closedScope.ValidationResult.Count != 0)
                {
                    var currentScope = CurrentScope;
                    currentScope.Merge(closedScope);
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
            }
        }

        /// <summary>
        /// Gets the current scope.
        /// </summary>
        internal ValidationResultCollectionScope CurrentScope
        {
            get
            {
                return _scope.Peek();
            }
        }

        /// <summary>
        /// Represents ValidationResultCollectionScope.
        /// Provides methods to get <see cref="ValidationResultCollection"/>, merge child scopes and dispose current scope. 
        /// </summary>
        internal class ValidationResultCollectionScope : IDisposable
        {
            readonly ValidationResultCollection _validationResult = new ValidationResultCollection();
            readonly BusinessRuleValidationContext _context;
            
            /// <summary>
            /// Initializes an instance of <see cref="ValidationResultCollectionScope"/> with the 
            /// supplied <see cref="BusinessRuleValidationContext"/>.
            /// </summary>
            /// <param name="context">The supplied <see cref="BusinessRuleValidationContext"/>.</param>
            public ValidationResultCollectionScope(BusinessRuleValidationContext context)
            {
                _context = context;
            }
            
            /// <summary>
            /// Gets its <see cref="ValidationResultCollection"/>.
            /// </summary>
            public ValidationResultCollection ValidationResult
            {

                get { return _validationResult; }
            }

            /// <summary>
            /// Adds the supplied <see cref="ValidationResultCollectionScope"/>.
            /// </summary>
            /// <param name="childScope">The supplied <see cref="ValidationResultCollectionScope"/>.</param>
            internal void Merge(ValidationResultCollectionScope childScope)
            {
                _validationResult.ChildResults.Add(childScope.ValidationResult);
            }

            /// <summary>
            /// Closes the scope of its <see cref="BusinessRuleValidationContext"/>.
            /// </summary>
            public void Dispose()
            {
                _context.CloseScope();
            }
        }
    }
}