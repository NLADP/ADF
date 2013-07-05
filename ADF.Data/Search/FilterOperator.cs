//using System;
//
//namespace Adf.Data.Search
//{
//    public struct FilterOperator : IEquatable<FilterOperator>, IComparable, IComparable<FilterOperator>
//    {
//        public static readonly FilterOperator Empty;
//
//        public static readonly FilterOperator Is = new FilterOperator() {Name = "Is", Value = "="};
//        
//        public static readonly FilterOperator Like = new FilterOperator() {Name = "Like", Value = "Like"};
//
//        public static readonly FilterOperator IsLargerThan = new FilterOperator() {Name = "IsLargerThan", Value = ">"};
//        
//        public static readonly FilterOperator IsSmallerThan = new FilterOperator() {Name = "IsSmallerThan", Value = "<"};
//        
//        public static readonly FilterOperator IsLargerThanOrEqual = new FilterOperator() {Name = "IsLargerThanOrEqual", Value = ">="};
//        
//        public static readonly FilterOperator IsSmallerThanOrEqual = new FilterOperator() {Name = "IsSmallerThanOrEqual", Value = "<="};
//        
//        public static readonly FilterOperator In = new FilterOperator() {Name = "In", Value = "In"};
//        
//        public static readonly FilterOperator NotIs = new FilterOperator() {Name = "NotIs", Value = "<>"};
//
//
//        public string Name { get; private set; }
//
//        public string Value {get; private set; }
//
//        public override string ToString()
//        {
//            return Name ?? string.Empty;
//        }
//
//        #region Validity
//
//
//        /// <summary>
//        /// Checks whether the value presented is a valid filteroperator.
//        /// </summary>
//        /// <param name="s">The string that is validated to see if it contains a valid <see cref="FilterOperator"/>.</param>
//        /// <param name="result">The resulting filteroperator, or the FilterOperator.Empty if the string did not contain a valid <see cref="FilterOperator"/>.</param>
//        /// <returns>
//        /// A <see cref="FilterOperator"></see> containing a fully qualified type name.
//        /// </returns>
//        public static bool TryParse(string s, out FilterOperator result)
//        {
//            result = Empty;
//
//            if (string.IsNullOrEmpty(s))
//            {
//                return true;
//            }
//
//            var field = typeof (FilterOperator).GetField(s);
//
//            if (field == null) return false;
//
//            result = (FilterOperator) field.GetValue(null);
//
//            return true;
//        }
//
//        #endregion
//
//        #region Empty
//
//
//        /// <summary>
//        /// Gets a value indicating whether this instance is empty.
//        /// </summary>
//        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
//        public bool IsEmpty
//        {
//            get { return string.IsNullOrEmpty(Value); }
//        }
//
//        #endregion Empty
//
//        #region Equality
//
//        /// <summary>
//        /// Identifies if the internal value of the parameter is equal to this object's internal value.
//        /// </summary>
//        /// <param name="other">filteroperator</param>
//        /// <returns></returns>
//        public bool Equals(FilterOperator other)
//        {
//            return Value.Equals(other.Value);
//        }
//
//        /// <summary>
//        /// Compares this instance to another <see cref="FilterOperator"/> object.
//        /// </summary>
//        /// <param name="obj">The <see cref="FilterOperator"/> object to compare against.</param>
//        /// <returns>
//        /// <c>true</c> if both filteroperators are the same; otherwise <c>false</c>
//        /// </returns>
//        public override bool Equals(object obj)
//        {
//            if (obj == null)
//                return false;
//
//            if (obj is FilterOperator)
//                return Equals((FilterOperator)obj);
//
//            return false;
//        }
//
//        public override int GetHashCode()
//        {
//            return Value.GetHashCode();
//        }
//
//        /// <summary>
//        /// Returns the equality of two <see cref="FilterOperator"/> objects.
//        /// </summary>
//        /// <param name="i">The first <see cref="FilterOperator"/>.</param>
//        /// <param name="j">The second <see cref="FilterOperator"/>.</param>
//        /// <returns>
//        /// <c>true</c> if both filteroperators are the same; otherwise <c>false</c>
//        /// </returns>
//        public static bool operator ==(FilterOperator i, FilterOperator j)
//        {
//            return i.Equals(j);
//        }
//
//        /// <summary>
//        /// Returns the inequality of two <see cref="FilterOperator"/> objects.
//        /// </summary>
//        /// <param name="i">The first <see cref="FilterOperator"/>.</param>
//        /// <param name="j">The second <see cref="FilterOperator"/>.</param>
//        /// <returns>
//        /// <c>true</c> if the filteroperators are different; otherwise <c>false</c>
//        /// </returns>
//        public static bool operator !=(FilterOperator i, FilterOperator j)
//        {
//            return !i.Equals(j);
//        }
//
//        #endregion Equality
//
//        #region Comparison
//
//        ///<summary>
//        ///Compares the current instance with another object of the same type.
//        ///</summary>
//        ///
//        ///<returns>
//        ///A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than obj. Zero This instance is equal to obj. Greater than zero This instance is greater than obj. 
//        ///</returns>
//        ///
//        ///<param name="obj">An object to compare with this instance. </param>
//        ///<exception cref="T:System.ArgumentException">obj is not the same type as this instance. </exception><filterpriority>2</filterpriority>
//        public int CompareTo(object obj)
//        {
//            if (obj == null)
//                return 1;
//
//            if (!(obj is FilterOperator))
//                throw new ArgumentException("Object is not a filteroperator");
//
//            FilterOperator other = (FilterOperator)obj;
//
//            return Value.CompareTo(other.Value);
//        }
//
//        ///<summary>
//        ///Compares the current object with another object of the same type.
//        ///</summary>
//        ///
//        ///<returns>
//        ///A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other. 
//        ///</returns>
//        ///
//        ///<param name="other">An object to compare with this object.</param>
//        public int CompareTo(FilterOperator other)
//        {
//            return Value.CompareTo(other.Value);
//        }
//
//        #endregion
//    }
//}