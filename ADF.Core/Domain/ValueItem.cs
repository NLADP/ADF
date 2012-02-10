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
    }
}
