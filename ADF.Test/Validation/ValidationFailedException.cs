﻿using System;
using Adf.Core.Validation;

namespace Adf.Test.Validation
{
    public class ValidationFailedException : Exception
    {
        private readonly ValidationResultCollection results;

        public ValidationFailedException(ValidationResultCollection results)
        {
            this.results = results;
        }

        public override string ToString()
        {
            return results.ConvertToString("; ");
        }
    }
}
