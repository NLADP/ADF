using Adf.Core.Query;

namespace Adf.Data.Search
{
    /// <summary>
    /// Defines properties that a value type or class implements to get value, column name and method name.
    /// </summary>
    public interface ISearchParameter
    {
        /// <summary>
        /// Gets the column name.
        /// </summary>
        /// <returns>
        /// The column name of the instance.
        /// </returns>
        string Column { get; }

        /// <summary>
        /// Gets the method name.
        /// </summary>
        /// <returns>
        /// The method name of the instance.
        /// </returns>
        string Operator { get; }

        OperatorType OperatorType { get; }

        ParameterType ParameterType { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <remarks>
        /// The value of the instance.
        /// </remarks>
        object Value { get; set; }
    }

    public interface IFilterParameter
    {
        /// <summary>
        /// Gets the column name.
        /// </summary>
        /// <returns>
        /// The column name of the instance.
        /// </returns>
        FilterProperty Property { get; set; }

        /// <summary>
        /// Gets the method name.
        /// </summary>
        /// <returns>
        /// The method name of the instance.
        /// </returns>
        OperatorType Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <remarks>
        /// The value of the instance.
        /// </remarks>
        object Value { get; set; }

        PredicateType Predicate { get; set; }
    }
}