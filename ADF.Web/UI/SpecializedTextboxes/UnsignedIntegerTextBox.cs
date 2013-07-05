using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a <see cref="Adf.Web.UI.CharacterFilteringTextBox"/> that only allows positive
    /// integer values through javascript.
    /// WARNING: A <see cref="Adf.Web.UI.UnsignedIntegerTextBoxValidator"/> should always be enclosed 
    /// when using this control.
    /// </summary>
    /// <remarks>
    /// <para>This <see cref="Adf.Web.UI.CharacterFilteringTextBox"/> contains a onkeypress event 
    /// that catches illegal characters, based on the CurrentCulture of the user.</para>
    /// <para>This Control should always be used together with a 
    /// <see cref="Adf.Web.UI.UnsignedIntegerTextBoxValidator"/>, because users could have
    /// JavaScript turned off. This is the code for the validator:
    /// <code>
    ///		UnsignedIntegerTextBox ntb = new UnsignedIntegerTextBox();
    ///		ntb.ID = "UnsignedIntegerTextBox1";
    ///		UnsignedIntegerTextBoxValidator cv = new UnsignedIntegerTextBoxValidator();
    ///		cv.ControlToValidate = ntb.ID;
    /// </code>
    /// </para>
    /// </remarks>
    public class UnsignedIntegerTextBox : CharacterFilteringTextBox
    {
        /// <summary>
        /// Gets a value indicating whether the numeric characters are allowed in the 
        /// <see cref="Adf.Web.UI.UnsignedIntegerTextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
        protected override bool AllowNumericKeys
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating the alignment of the text in the 
        /// <see cref="Adf.Web.UI.UnsignedIntegerTextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns 'left'.
        /// </returns>
        protected override string TextAlign
        {
            get { return "left"; }
        }
    }

    /// <summary>
    /// Represents a <see cref="System.Web.UI.WebControls.CompareValidator"/> which is used 
    /// together with a <see cref="Adf.Web.UI.UnsignedIntegerTextBox"/>.
    /// Used to validate the characters entering into the <see cref="Adf.Web.UI.UnsignedIntegerTextBox"/>
    /// so that only a positive integer value can be entered into the 
    /// <see cref="Adf.Web.UI.UnsignedIntegerTextBox"/>.
    /// </summary>
    public class UnsignedIntegerTextBoxValidator : CompareValidator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Adf.Web.UI.UnsignedIntegerTextBoxValidator"/> 
        /// class.
        /// Sets its operator to System.Web.UI.WebControls.ValidationCompareOperator.GreaterThanEqual,
        /// its type to System.Web.UI.WebControls.ValidationDataType.Integer and
        /// its ValueToCompare to '0'.
        /// </summary>
        public UnsignedIntegerTextBoxValidator() : base()
        {
            Operator = ValidationCompareOperator.GreaterThanEqual;
            Type = ValidationDataType.Integer;
            ValueToCompare = "0";
        }
    }
}