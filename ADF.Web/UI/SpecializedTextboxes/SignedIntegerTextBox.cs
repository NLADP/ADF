using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a <see cref="System.Web.UI.WebControls.TextBox"/> that only allows positive and 
    /// negative integer values through javascript.
    /// WARNING: A <see cref="Adf.Web.UI.SignedIntegerTextBoxValidator"/> should always be enclosed 
    /// when using this control.
    /// </summary>
    /// <remarks>
    /// <para>This <see cref="System.Web.UI.WebControls.TextBox"/> contains a onkeypress event 
    /// that catches illegal characters, based on the CurrentCulture of the user.</para>
    /// <para>This Control should always be used together with a 
    /// <see cref="Adf.Web.UI.SignedIntegerTextBoxValidator"/>, because users could have
    /// JavaScript turned off. This is the code for the validator:
    /// <code>
    ///		SignedIntegerTextBox ntb = new SignedIntegerTextBox();
    ///		ntb.ID = "SignedIntegerTextBox1";
    ///		SignedIntegerTextBoxValidator cv = new SignedIntegerTextBoxValidator();
    ///		cv.ControlToValidate = ntb.ID;
    /// </code>
    /// </para>
    /// </remarks>
    [DefaultProperty("Text"), ToolboxData("<{0}:SignedIntegerTextBox runat=server></{0}:SignedIntegerTextBox>")]
    public class SignedIntegerTextBox : CharacterFilteringTextBox
    {
        /// <summary>
        /// Gets a value indicating whether the numeric characters are allowed in the 
        /// <see cref="System.Web.UI.WebControls.TextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
        protected override bool AllowNumericKeys
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the negative sign character is allowed in the 
        /// <see cref="System.Web.UI.WebControls.TextBox"/>.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
        protected override bool AllowNegativeSignKey
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating the alignment of the text in the 
        /// <see cref="System.Web.UI.WebControls.TextBox"/>.
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
    /// together with a <see cref="Adf.Web.UI.SignedIntegerTextBox"/>.
    /// Used to validate the characters entering into the <see cref="Adf.Web.UI.SignedIntegerTextBox"/>
    /// so that only numeric characters and the negative sign character can be entered into the 
    /// <see cref="Adf.Web.UI.SignedIntegerTextBox"/>.
    /// </summary>
    public class SignedIntegerTextBoxValidator : CompareValidator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Adf.Web.UI.SignedIntegerTextBoxValidator"/> class.
        /// Sets its operator to System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck
        /// and its type to System.Web.UI.WebControls.ValidationDataType.Integer.
        /// </summary>
        public SignedIntegerTextBoxValidator() : base()
        {
            Operator = ValidationCompareOperator.DataTypeCheck;
            Type = ValidationDataType.Integer;
        }
    }
}