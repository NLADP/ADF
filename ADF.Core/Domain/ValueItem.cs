using System;
using System.Collections;

namespace Adf.Core.Domain
{
    /// <summary>
    /// Represents ValueItem extended arraylist item.
    /// </summary>
    public class ValueItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueItem"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value object.</param>
        /// <param name="selected">If set to <c>true</c>, this item is selected.</param>
        private ValueItem(string text, object value, bool selected)
        {
            Text = text;
            Value = value;
            Selected = selected;
        }

        /// <summary>
        /// Gets or sets the text of the <see cref="ValueItem"/>.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the selected <see cref="ValueItem"/>.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the text of the <see cref="ValueItem"/>.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Creates a new <see cref="ValueItem"/> object.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value object.</param>
        /// <param name="selected">If set to <c>true</c>, this item is selected.</param>
        /// <returns>The new <see cref="ValueItem"/> object.</returns>
        public static ValueItem New(string text, object value, bool selected)
        {
            return new ValueItem(text, value, selected);
        }

        /// <summary>
        /// Creates a new unselected <see cref="ValueItem"/> object.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value object.</param>
        /// <returns>The new <see cref="ValueItem"/> object.</returns>
        public static ValueItem New(string text, object value)
        {
            return new ValueItem(text, value, false);
        }

        /// <summary>
        /// Creates a new unselected <see cref="ValueItem"/> object using the text representation of 
        /// the value object.
        /// </summary>
        /// <param name="value">The value object.</param>
        /// <returns>The new <see cref="ValueItem"/> object.</returns>
        public static ValueItem New(object value)
        {
            return value == null ? null : new ValueItem(value.ToString(), value, false);
        }

        /// <summary>
        /// Returns a <see cref="string"></see> that represents the current <see cref="ValueItem"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"></see> that represents the current <see cref="ValueItem"/>.
        /// </returns>
        public override string ToString()
        {
            return Text;
        }

        public static bool operator ==(ValueItem x, ValueItem y)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(x, y))
                return true;

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
                return false;

            if (x.GetType() != y.GetType()) return false;

            if (x.Value == null || y.Value == null) return false;
            if (x.Value.GetType() != y.Value.GetType()) return false;

            return x.Value.Equals(y.Value);
        }

        public static bool operator !=(ValueItem x, ValueItem y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var other = obj as ValueItem;

            if (other == null) return false;

            return this == other;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
