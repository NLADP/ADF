using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Adf.Core;
using Adf.Core.Domain;
using Adf.Core.State;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Structure representing the value object Money.
    /// </summary>
    [Serializable]
    public struct Money : IValueObject, IComparable, IComparable<Money>, IEquatable<Money>, IFormattable
    {
        private static string MoneyFormat;

        private readonly decimal? value;
        private readonly RegionInfo regionInfo;
        private readonly CultureInfo cultureInfo;

        #region CodeGuard(Constructors)
        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private Money(decimal? amount) : this(amount, CultureInfo.CurrentCulture) {}

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private Money(long amount) : this(amount, CultureInfo.CurrentCulture) {}

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public Money(string amount)
        {
            Money money;
            if (TryParse(amount, out money))
            {
                cultureInfo = money.cultureInfo;
                regionInfo = money.regionInfo;
                value = money.Amount;
            }
            else
            {
                throw new FormatException("format of amount is invalid");
            }
        }

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount and culture name.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="cultureName">The culture name.</param>
        private Money(decimal? amount, string cultureName)
            : this(amount, new CultureInfo(cultureName))
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>.</param>
        private Money(CultureInfo cultureInfo)
        {
            if (cultureInfo == null) throw new ArgumentNullException("cultureInfo");
            this.cultureInfo = cultureInfo;
            regionInfo = new RegionInfo(cultureInfo.LCID);
            value = new decimal?();
        }

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount and 
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>.</param>
        private Money(decimal? amount, CultureInfo cultureInfo)
        {
            if (cultureInfo == null) throw new ArgumentNullException("cultureInfo");
            this.cultureInfo = cultureInfo;
            regionInfo = new RegionInfo(cultureInfo.LCID);
            value = amount;
        }

        /// <summary>
        /// Initializes an instance of <see cref="Money"/> with the supplied amount and 
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>.</param>
        private Money(long amount, CultureInfo cultureInfo)
        {
            if (cultureInfo == null) throw new ArgumentNullException("cultureInfo");
            this.cultureInfo = cultureInfo;
            regionInfo = new RegionInfo(cultureInfo.LCID);
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
            return new Money(CultureInfo.CurrentCulture);
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
        /// Creates and returns an instance of <see cref="Money"/> with the supplied 
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>.</param>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New(CultureInfo cultureInfo)
        {
            return new Money(cultureInfo);
        }

        /// <summary>
        /// Creates and returns an instance of <see cref="Money"/> with the supplied amount and 
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>.</param>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New(decimal? amount, CultureInfo cultureInfo)
        {
            return new Money(amount, cultureInfo);
        }

        /// <summary>
        /// Creates and returns an instance of <see cref="Money"/> with the supplied amount and 
        /// culture name.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="cultureName">The culture name.</param>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New(decimal? amount, string cultureName)
        {
            return new Money(amount, cultureName);
        }

        /// <summary>
        /// Creates and returns an instance of <see cref="Money"/> with the supplied amount and 
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>.</param>
        /// <returns>The new instance of <see cref="Money"/>.</returns>
        public static Money New(long amount, CultureInfo cultureInfo)
        {
            return new Money(amount, cultureInfo);
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

            //Money money = (Money)obj;
            //return (money != null) && Equals(money);
        }

        /// <summary>
        /// Equals the instance of <see cref="Money"/> to the supplied instance of <see cref="Money"/>.
        /// </summary>
        /// <param name="other">The supplied instance of <see cref="Money"/>.</param>
        /// <returns>True if the instance of <see cref="Money"/> is equal to the supplied instance of 
        /// <see cref="Money"/>, false otherwise.</returns>
        public bool Equals(Money other)
        {
            return ((IsoCurrencySymbol == other.IsoCurrencySymbol) && (value == other.value));
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
            return (first.IsoCurrencySymbol == second.IsoCurrencySymbol && first.Amount == second.Amount);
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

        public static bool operator ==(Money first, decimal? second)
        {
            return (first.Amount == second);
        }

        public static bool operator !=(Money first, decimal? second)
        {
            return !(first == second);
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
            AssertSameCurrency(first, second);
            return (first.Amount > second.Amount);
        }

        public static bool operator >(Money first, decimal? second)
        {
            return first.Amount > second;
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
            AssertSameCurrency(first, second);
            return (first.Amount >= second.Amount);
        }

        public static bool operator >=(Money first, decimal? second)
        {
            return first.Amount >= second;
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
            AssertSameCurrency(first, second);
            return (first.Amount < second.Amount);
        }

        public static bool operator <(Money first, decimal? second)
        {
            return first.Amount < second;
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
            AssertSameCurrency(first, second);
            return (first.Amount <= second.Amount);
        }

        public static bool operator <=(Money first, decimal? second)
        {
            return first.Amount <= second;
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
            AssertSameCurrency(first, second);

            return new Money(first.Amount + second.Amount, first.EnglishCultureName);
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
            AssertSameCurrency(first, second);

            return new Money(first.Amount - second.Amount, first.EnglishCultureName);
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
            AssertSameCurrency(first, second);

            return Multiply(first, second);
        }

        /// <summary>
        /// Creates and returns a new instance of <see cref="Money"/> with the amount found by 
        /// multiplying the supplied instance of <see cref="Money"/> with a supplied decimal value.
        /// </summary>
        /// <param name="money">The instance of <see cref="Money"/>.</param>
        /// <param name="value">The decimal value.</param>
        /// <returns>The newly created instance of <see cref="Money"/>.</returns>
        public static Money operator *(Money money, decimal value)
        {
            return (money * New(value));
        }

        /// <summary>
        /// Multiplies the supplied instance of <see cref="Money"/> with the supplied decimal value and
        /// the result is returned.
        /// </summary>
        /// <param name="money">The instance of <see cref="Money"/>.</param>
        /// <param name="value">The decimal value.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Money Multiply(Money money, decimal value)
        {
            return money * value;
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
        /// Creates and returns a new instance of <see cref="Money"/> with the amount of the supplied 
        /// instance of <see cref="Money"/> divided by the supplied decimal value.
        /// </summary>
        /// <param name="money">The instance of <see cref="Money"/>.</param>
        /// <param name="value">The decimal value.</param>
        /// <returns>The newly created instance of <see cref="Money"/>.</returns>
        public static Money operator /(Money money, decimal value)
        {
            return new Money(money.Amount/value, money.EnglishCultureName);
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
            return first/value;
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
            AssertSameCurrency(first, second);

            return new Money(first.Amount/second.Amount, first.EnglishCultureName);
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

            return value.GetHashCode() ^ cultureInfo.GetHashCode();
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

//        public Money Round(int decimals = 2, MidpointRounding midpointRounding = MidpointRounding.ToEven)
//        {
//            return IsEmpty ? this : new Money(decimal.Round(Amount.Value, decimals, midpointRounding));
//        }

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
                if (StateManager.Settings.Has("MoneyFormat")) MoneyFormat = StateManager.Settings["MoneyFormat"].ToString();
            }

            return string.IsNullOrEmpty(MoneyFormat)
                       ? Amount.Value.ToString(cultureInfo)
                       : Amount.Value.ToString(MoneyFormat, cultureInfo);
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

        /// <summary>
        /// Gets the ISOCurrencySymbol of this instance.
        /// </summary>
        public string IsoCurrencySymbol
        {
            get { return regionInfo.ISOCurrencySymbol; }
        }

        /// <summary>
        /// Gets the EnglishCultureName of this instance.
        /// </summary>
        public string EnglishCultureName
        {
            get { return cultureInfo.Name; }
        }

        /// <summary>
        /// Gets the number of decimal places of this instance.
        /// </summary>
        public int DecimalDigits
        {
            get { return cultureInfo.NumberFormat.CurrencyDecimalDigits; }
        }

        /// <summary>
        /// Gets the new instance of <see cref="Money"/> with the CurrentCulture.
        /// </summary>
        public static Money LocalCurrency
        {
            get { return new Money(CultureInfo.CurrentCulture); }
        }

        #endregion

        #region CodeGuard(Privates)
        /// <summary>
        /// Determines whether the currencies are same for the two supplied instances of <see cref="Money"/>.
        /// </summary>
        /// <param name="first">The first instance of <see cref="Money"/>.</param>
        /// <param name="second">The second instance of <see cref="Money"/>.</param>
        private static void AssertSameCurrency(Money first, Money second)
        {
            if (!first.IsEmpty && !second.IsEmpty)
            {
                if (first.IsoCurrencySymbol != second.IsoCurrencySymbol)

                throw new ArgumentException("currency differs", "second");
            }
        }

        #endregion
    }
}