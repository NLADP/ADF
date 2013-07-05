using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Adf.Base.State;
using Adf.Core;
using Adf.Core.Domain;
using Adf.Core.State;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Structure representing the value object Money.
    /// </summary>
    [Serializable]
    public struct Money : IValueObject, IComparable, IComparable<Money>, IEquatable<Money>, IFormattable
    {
        private static string MoneyFormat;

        private readonly decimal? value;

        #region CodeGuard(Constructors)

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public Money(string amount)
        {
            Money money;
            if (TryParse(amount, out money))
            {
                value = money.Amount;
            }
            else
            {
                throw new FormatException("format of amount is invalid");
            }
        }

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount and 
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public Money(decimal? amount)
        {
            value = amount;
        }

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount and 
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private Money(long amount)
        {
            value = amount;
        }

        public static implicit operator Money(decimal? amount)
        {
            return new Money(amount);
        }

        public static implicit operator decimal?(Money money)
        {
            return money.Amount;
        }

        #endregion CodeGuard(Constructors)

        #region CodeGuard(New)
        /// <summary>
        /// Creates and returns an instance of <see cref="Money"/> with the current culture.
        /// </summary>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New()
        {
            return new Money();
        }

        /// <summary>
        /// Creates and returns an instance of <see cref="Money"/> with the supplied amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New(string amount)
        {
            return new Money(amount);
        }

        /// <summary>
        /// Creates and returns an instance of <see cref="Money"/> with the supplied amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New(long amount)
        {
            return new Money(amount);
        }

        /// <summary>
        /// Creates and returns an instance of <see cref="Money"/> with the supplied amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New(decimal? amount)
        {
            return new Money(amount);
        }

        #endregion

        #region CodeGuard(Operators)
        /// <summary>
        /// Equals the instance of <see cref="Money"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>True if the instance of <see cref="Money"/> is equal to the supplied object, 
        /// false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Money)
                return Equals((Money)obj);

            return false;
        }

        /// <summary>
        /// Equals the instance of <see cref="Money"/> to the supplied instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="other">The supplied instance of <see cref="Money"/>.</param>
        /// <returns>True if the instance of <see cref="Money"/> is equal to the supplied instance of 
        /// <see cref="Money"/>, false otherwise.</returns>
        public bool Equals(Money other)
        {
            return value == other.value;
        }

        /// <summary>
        /// Checks whether the supplied first instance of <see cref="Money"/> is equal to the supplied 
        /// second instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>True if the supplied first instance of <see cref="Money"/> is equal to the supplied 
        /// second instance of <see cref="Money"/>, false otherwise.</returns>
        public static bool operator ==(Money first, Money second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Checks whether the supplied first instance of <see cref="Money"/> is not equal to the supplied 
        /// second instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>True if the supplied first instance of <see cref="Money"/> is not equal to the 
        /// supplied second instance of <see cref="Money"/>, false otherwise.</returns>
        public static bool operator !=(Money first, Money second)
        {
            return !first.Equals(second);
        }

        /// <summary>
        /// Checks whether the supplied first instance of <see cref="Money"/> is greater than the supplied 
        /// second instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>True if the supplied first instance of <see cref="Money"/> is greater than the 
        /// supplied second instance of <see cref="Money"/>, false otherwise.</returns>
        public static bool operator >(Money first, Money second)
        {
            return (first.Amount > second.Amount);
        }

        /// <summary>
        /// Checks whether the supplied first instance of <see cref="Money"/> is greater than or equal 
        /// to the supplied second instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>True if the supplied first instance of <see cref="Money"/> is greater than or equal 
        /// to the supplied second instance of <see cref="Money"/>, false otherwise.</returns>
        public static bool operator >=(Money first, Money second)
        {
            return (first.Amount >= second.Amount);
        }

        /// <summary>
        /// Checks whether the supplied first instance of <see cref="Money"/> is less than the supplied 
        /// second instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>True if the supplied first instance of <see cref="Money"/> is less than the 
        /// supplied second instance of <see cref="Money"/>, false otherwise.</returns>
        public static bool operator <(Money first, Money second)
        {
            return (first.Amount < second.Amount);
        }

        /// <summary>
        /// Checks whether the supplied first instance of <see cref="Money"/> is less than or equal 
        /// to the supplied second instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>True if the supplied first instance of <see cref="Money"/> is less than or equal 
        /// to the supplied second instance of <see cref="Money"/>, false otherwise.</returns>
        public static bool operator <=(Money first, Money second)
        {
            return (first.Amount <= second.Amount);
        }

        /// <summary>
        /// Creates and returns a new instance of <see cref="Money"/> with the summation of two 
        /// supplied instances of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first supplied instance of <see cref="Money"/>.</param>
        /// <param name="second">The second supplied instance of <see cref="Money"/>.</param>
        /// <returns>The newly created instance of <see cref="Money"/>.</returns>
        public static Money operator +(Money first, Money second)
        {
            if (first.IsEmpty) throw new ArgumentException("Empty argument", "first");
            if (second.IsEmpty) throw new ArgumentException("Empty argument", "second");

            return new Money(first.Amount + second.Amount);
        }

        /// <summary>
        /// Adds the two supplied instances of <see cref="Money"/> and the result is returned.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>The result of the addition.</returns>
        public static Money Add(Money first, Money second)
        {
            return first + second;
        }

        /// <summary>
        /// Creates and returns a new instance of <see cref="Money"/> with the amount of second supplied 
        /// instance of <see cref="Money"/> subtracted from the amount of first supplied instance of 
        /// <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>The newly created instance of <see cref="Money"/>.</returns>
        public static Money operator -(Money first, Money second)
        {
            if (first.IsEmpty) throw new ArgumentException("Empty argument", "first");
            if (second.IsEmpty) throw new ArgumentException("Empty argument", "second");

            return new Money(first.Amount - second.Amount);
        }

        /// <summary>
        /// Subtracts the second supplied instance of <see cref="Money"/> from the first supplied 
        /// instance of <see cref="Money"/> and the result is returned.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Money Subtract(Money first, Money second)
        {
            return first - second;
        }

        /// <summary>
        /// Multiplies the two supplied instances of <see cref="Money"/> and the result is returned.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Money operator *(Money first, Money second)
        {
            if (first.IsEmpty) throw new ArgumentException("Empty argument", "first");
            if (second.IsEmpty) throw new ArgumentException("Empty argument", "second");

            return Multiply(first, second);
        }

        /// <summary>
        /// Creates and returns a new instance of <see cref="Money"/> with the amount which the result 
        /// of the multiplication of the amounts of the two supplied instances of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>The newly created instance of <see cref="Money"/>.</returns>
        public static Money Multiply(Money first, Money second)
        {
            return new Money(first.Amount * second.Amount);
        }

        /// <summary>
        /// Returns an instance of <see cref="Money"/> with the supplied 
        /// instance of <see cref="Money"/> divided by the supplied decimal value.
        /// </summary>
        /// <param name="first">The instance of <see cref="Money"/>.</param>
        /// <param name="value">The decimal value.</param>
        /// <returns>An instance of <see cref="Money"/>.</returns>
        public static Money Divide(Money first, decimal value)
        {
            return first / value;
        }

        /// <summary>
        /// Creates and returns a new instance of <see cref="Money"/> with the amount of the supplied 
        /// first instance of <see cref="Money"/> divided by the amount of the supplied second instance 
        /// of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>The newly created instance of <see cref="Money"/>.</returns>
        public static Money operator /(Money first, Money second)
        {
            if (first.IsEmpty) throw new ArgumentException("Empty argument", "first");
            if (second.IsEmpty) throw new ArgumentException("Empty argument", "second");

            return new Money(first.Amount / second.Amount);
        }

        /// <summary>
        /// Returns an instance of <see cref="Money"/> which is the supplied first instance of 
        /// <see cref="Money"/> divided by the supplied second instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        /// <returns>An instance of <see cref="Money"/>.</returns>
        public static Money Divide(Money first, Money second)
        {
            return first/second;
        }

        /// <summary>
        /// Returns the result of multiplying the specified <see cref="Money"/> value by
        /// negative one.
        /// </summary>
        /// <param name="money">The instance of <see cref="Money"/>.</param>
        /// <returns>The newly created instance of <see cref="Money"/>.</returns>
        public static Money Negate(Money money)
        {
            if (money.IsEmpty) throw new ArgumentException("Empty argument", "money");
            return new Money(decimal.Negate(money.Amount.Value));
        }

        public static Money operator -(Money money)
        {
            return new Money(-money.Amount);
        }

        /// <summary>
        /// Returns the hash code of the instance of <see cref="Money"/>.
        /// </summary>
        /// <returns>The hash code of the instance of <see cref="Money"/>.</returns>
        public override int GetHashCode()
        {
            if (IsEmpty) return 0;

            return value.GetHashCode();
        }

        /// <summary>
        /// Compares the instance of <see cref="Money"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="Money"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="Money"/> is equal to the supplied object. Value greater than zero indicates 
        /// that the <see cref="Money"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Money)) throw new ArgumentException("obj is not a Money");
            Money other = (Money) obj;
            return CompareTo(other);
        }

        /// <summary>
        /// Compares an instance of <see cref="Money"/> to the supplied instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="other">The instance of <see cref="Money"/> to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the <see cref="Money"/> 
        /// is less than the supplied <see cref="Money"/>. Value zero indicates that the 
        /// <see cref="Money"/> is equal to the supplied <see cref="Money"/>. Value greater than 
        /// zero indicates that the <see cref="Money"/> is greater than the supplied 
        /// <see cref="Money"/>.</returns>
        public int CompareTo(Money other)
        {
            if (this.IsEmpty && other.IsEmpty) return 0;

            if (this.IsEmpty || other.IsEmpty) return this.IsEmpty ? -1 : 1;

            if (this < other)
            {
                return -1;
            }
            if (this > other)
            {
                return 1;
            }
            return 0;
        }

        #endregion CodeGuard(Operators)

        #region CodeGuard(Empty)

        private static readonly Money empty = new Money();

        /// <summary>
        /// The empty <see cref="Money"/>.
        /// </summary>
        public static Money Empty
        {
            get { return empty; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return !value.HasValue; }
        }

        #endregion CodeGuard(Empty)

        #region CodeGuard(Value)
        /// <summary>
        /// Returns the value as string
        /// </summary>
        /// <returns>The value in string format</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Decimal.ToString(System.String)")]
        public override string ToString()
        {
            if (!value.HasValue)
            {
                return string.Empty;
            }

            if (MoneyFormat == null)
            {
                MoneyFormat = StateManager.Settings.GetOrCreate<string>("MoneyFormat");
            }

            return string.IsNullOrEmpty(MoneyFormat) ? Amount.Value.ToString() : Amount.Value.ToString(MoneyFormat);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return value.HasValue
                       ? value.Value.ToString(format ?? "N", formatProvider)
                       : string.Empty;
        }

        /// <summary>
        /// Returns the amount of this instance.
        /// </summary>
        public object Value
        {
            get { return Amount; }
        }

        /// <summary>
        /// Returns the amount of this instance.
        /// </summary>
        public decimal? Amount
        {
            get { return value; }
        }

        #endregion  CodeGuard(Value)

        #region CodeGuard(Publics)

        ///<summary>
        /// Tries to parse the supplied string into the supplied instance of <see cref="Money"/>.
        ///</summary>
        ///<param name="s">The string to parse.</param>
        ///<param name="result">The instance of <see cref="Money"/>.</param>
        ///<returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out Money result)
        {
            return TryParse(s, CultureInfo.CurrentCulture, out result);
        }

        /// <summary>
        /// Tries to parse the supplied string into the supplied instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="provider">A format provider like CultureInfo</param>
        /// <param name="result">The instance of <see cref="Money"/>.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, IFormatProvider provider, out Money result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = Empty;
                return true;
            }
            decimal parseValue;
            if (decimal.TryParse(s, NumberStyles.Number, provider, out parseValue))
            {
                result = new Money(parseValue);
                return true;
            }
            
            result = Empty;
            return false;
        }


        #endregion

    }
}