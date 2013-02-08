using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
            ValidationManager.AddError(message);
        }
    }
}
