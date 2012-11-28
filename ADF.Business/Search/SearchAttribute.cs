using System;
using System.Collections;
using Adf.Core.Query;
using Adf.Data.Search;

namespace Adf.Business.Search
{
    /// <summary>
    /// Attribute to perform search operations.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class SearchAttribute : Attribute, ISearchParameter
    {
        /// <summary>
        /// Gets the method name on which the search operation to be performed.
        /// </summary>
        public string Operator { get; private set; }

        public OperatorType OperatorType { get; private set; }
        
        public ParameterType ParameterType { get; private set; }

        /// <summary>
        /// Gets the column name on which the search operation to be performed.
        /// </summary>
        public string Column { get; private set; }

        /// <summary>
        /// Gets or sets the value for the search operation to be performed.
        /// </summary>
        public object Value { get; set; }

        public bool IncludeWhenEmpty { get; protected set; }

        public CollationType Collation { get; private set; }

        /// <summary>
        /// Sets the method name and column name of this instance with the supplied method name and 
        /// column name.
        /// </summary>
        /// <param name="op">The supplied method name.</param>
        /// <param name="column">The supplied column name.</param>
        public SearchAttribute(string op, OperatorType operatorType, string column, ParameterType parameterType = null, CollationType collation = null)
        {
            Operator = op;
            OperatorType = operatorType;
            Column = column;
            ParameterType = parameterType;
            Collation = collation;
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'Like'(similar to Sql 'Like' Operation) on a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class LikeAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="LikeAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public LikeAttribute(string column) : base("Like", OperatorType.Like, column)
        {
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'In'(similar to Sql 'IN'Operation) on a column.
    /// Usage: Return an <see cref="IAdfQuery"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class InQueryAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="InQueryAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public InQueryAttribute(string column) : base("In", OperatorType.In, column, ParameterType.Query)
        {
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'In'(similar to Sql 'IN'Operation) on a column.
    /// Usage: Return an <see cref="IEnumerable"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class InListAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="InQueryAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public InListAttribute(string column, bool includeWhenEmpty = true) : base("In", OperatorType.In, column, ParameterType.ValueList)
        {
            IncludeWhenEmpty = includeWhenEmpty;
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'Is'(similar to Sql '='Operation) on a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class IsAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="IsAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public IsAttribute(string column) : base("Is", OperatorType.IsEqual, column)
        {
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'IsNot'(similar to Sql '&lt;&gt;'Operation) on a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class IsNotAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="IsNotAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public IsNotAttribute(string column)
            : base("IsNot", OperatorType.IsNotEqual, column)
        {
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'IsNot'(similar to Sql '&lt;&gt;'Operation) on a column
    /// and adds an extra check to get the items that have a NULL on the column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class IsNotOrIsNullAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="IsNotOrIsNullAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public IsNotOrIsNullAttribute(string column)
            : base("IsNotOrIsNull", OperatorType.IsNotEqualOrIsNull, column)
        {
        }
    }

    /// <summary>    
    /// Attribute to perform search operations of filter type 'IsLarger'(similar to Sql 'Greater Than' Operation) on a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class IsLargerAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="IsLargerAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public IsLargerAttribute(string column) : base("IsLargerThan", OperatorType.IsLarger, column)
        {
        }
    }

    /// <summary>    
    /// Attribute to perform search operations of filter type 'IsLargerOrEqual'(similar to Sql 'Greater Than or equal' Operation) on a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class IsLargerOrEqualAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="IsLargerAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public IsLargerOrEqualAttribute(string column) : base("IsLargerThanOrEqual", OperatorType.IsLargerOrEqual, column)
        {
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'IsLarger'(similar to Sql 'Lesser Than' Operation) on a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class IsSmallerAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="IsSmallerAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public IsSmallerAttribute(string column) : base("IsSmallerThan", OperatorType.IsSmaller, column)
        {
        }
    }

    /// <summary>
    /// Attribute to perform search operations of filter type 'IsLarger'(similar to Sql 'Lesser Than' Operation) on a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class IsSmallerOrEqualAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance of <see cref="IsSmallerOrEqualAttribute"/> with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public IsSmallerOrEqualAttribute(string column) : base("IsSmallerThanOrEqual", OperatorType.IsSmallerOrEqual, column)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class QueryParameterAttribute : SearchAttribute
    {
        /// <summary>
        /// Initializes an instance with the supplied column name.
        /// </summary>
        /// <param name="column">The supplied column name.</param>
        public QueryParameterAttribute(string column) : base("QueryParameter", OperatorType.IsEqual, column, ParameterType.QueryParameter)
        {
        }
    }
}