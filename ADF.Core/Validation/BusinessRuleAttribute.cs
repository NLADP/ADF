using System;

namespace Adf.Core.Validation
{
    /// <summary>
    /// Represents custom attribute of an <see cref="System.Object"/> to indicate whether the 
    /// <see cref="System.Object"/> will be considered for the business rule validation process.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BusinessRuleAttribute : Attribute   // sealed for performance
    {
    }
}
