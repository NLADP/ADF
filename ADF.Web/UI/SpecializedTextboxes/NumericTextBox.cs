using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
	/// <summary>
    /// Represents a <see cref="System.Web.UI.WebControls.TextBox"/> that only allows numeric 
    /// characters through JavaScript.
    /// WARNING: A <see cref="Adf.Web.UI.NumericTextBoxValidator"/> should always be enclosed
	/// when using this control.
	/// </summary>
	/// <remarks>
    /// <para>This <see cref="System.Web.UI.WebControls.TextBox"/> contains a onkeypress event 
    /// that catches illegal characters, based on the CurrentCulture of the user.</para>
	/// <para>This Control should always be used together with a 
    /// <see cref="Adf.Web.UI.NumericTextBoxValidator"/>, because users could have
	/// JavaScript turned off. This is the code for the validator:
	/// <code>
	///		NumericTextBox ntb = new NumericTextBox();
	///		ntb.ID = "NumericTextBox1";
	///		NumericTextBoxValidator cv = new NumericTextBoxValidator();
	///		cv.ControlToValidate = ntb.ID;
	/// </code>
	/// </para>
	/// </remarks>
    [DefaultProperty("Text"), ToolboxData("<{0}:NumericTextBox runat=server></{0}:NumericTextBox>")]
	public class NumericTextBox : CharacterFilteringTextBox
	{
		/// <summary>
        /// Gets a value indicating whether the numeric characters are allowed in the 
        /// <see cref="System.Web.UI.WebControls.TextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
		protected override bool AllowNumericKeys { get { return true; } }

        /// <summary>
        /// Gets a value indicating whether the negative sign character is allowed in the 
        /// <see cref="System.Web.UI.WebControls.TextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
		protected override bool AllowNegativeSignKey { get { return true; } }

        /// <summary>
        /// Gets a value indicating whether the decimal separator key is allowed for the 
        /// <see cref="System.Web.UI.WebControls.TextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
		protected override bool AllowDecimalSeparatorKey { get { return true; } }

        /// <summary>
        /// Gets a value indicating the alignment of the text in the 
        /// <see cref="System.Web.UI.WebControls.TextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns 'left'.
        /// </returns>
		protected override string TextAlign { get { return "left"; } }
	}


	/// <summary>
    /// Represents a <see cref="System.Web.UI.WebControls.CompareValidator"/> which is used 
    /// together with a <see cref="Adf.Web.UI.NumericTextBox"/>.
    /// Used to validate the characters entering into the <see cref="Adf.Web.UI.NumericTextBox"/>
    /// so that only a numeric value (positive or negative) can be entered into the 
    /// <see cref="Adf.Web.UI.NumericTextBox"/>.
	/// </summary>
	public class NumericTextBoxValidator : CompareValidator
	{
        /// <summary>
        /// Initializes an instance of the <see cref="Adf.Web.UI.NumericTextBoxValidator"/> class.
        /// Sets its operator to System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck
        /// and its type to System.Web.UI.WebControls.ValidationDataType.Double.
        /// </summary>
		public NumericTextBoxValidator() : base()
		{
			Operator = ValidationCompareOperator.DataTypeCheck;
			Type = ValidationDataType.Double;
		}
	}
}