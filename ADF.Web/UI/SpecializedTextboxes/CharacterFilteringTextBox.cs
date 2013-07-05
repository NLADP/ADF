using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Base class for all <see cref="System.Web.UI.WebControls.TextBox"/>s that use a filter 
    /// to enable a subset of the keys.
    /// </summary>
    public abstract class CharacterFilteringTextBox : TextBox
    {
        private static string altKey = "if (evt.altKey) return; // Alt Key";
        private static string ctrlKey = "if (evt.ctrlKey) return; // Ctrl Key";
        private static string specialKeys = "if (charCode<32) return; // Special Keys";
        private static string arrowKeys = "if (charCode>=33 && charCode<=40) return; // Arrow Keys";
        private static string functionKeys = "if (charCode>=112 && charCode<=123) return; // F-Keys";
        private static string numericKeys = "if(charCode>=48 && charCode<=57) return; // Numeric Keys";
        private static string spaceKey = "if (charCode==32) return;";

        /// <summary>
        /// Gets a string containing JavaScript code to check the negative sign character.
        /// </summary>
        /// <returns>
        /// A string containing JavaScript code to check the negative sign character.
        /// </returns>
        private static string _negativeSignKey
        {
            get { return "if (ch=='" + NumberFormatInfo.CurrentInfo.NegativeSign + "') return;"; }
        }

        /// <summary>
        /// Gets a string containing JavaScript code to check the decimal number separator character.
        /// </summary>
        /// <returns>
        /// A string containing JavaScript code to check the decimal number separator character.
        /// </returns>
        private static string _numberDecimalSeparatorKey
        {
            get { return "if (ch=='" + NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "') return;"; }
        }

        /// <summary>
        /// Gets the JavaScript function name for the 'OnKeyPress' event of the 
        /// <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>.
        /// </summary>
        /// <remarks>
        /// The property returns a string of '"Filter" + TypeName', so every descendant of the 
        /// <see cref="Adf.Web.UI.CharacterFilteringTextBox"/> class will have it's unique
        /// function name.
        /// </remarks>
        protected string OnKeyPressFunctionName
        {
            get { return "Filter" + GetType().Name; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/> 
        /// allows the Alt + Key combinations.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
        protected virtual bool AllowAltKey
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/> 
        /// allows the Ctrl + Key combinations.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
        protected virtual bool AllowCtrlKey
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/> 
        /// allows the arrow keys (Home, PgUp, End, PgDown, Left, Right, Up, Down).
        /// Descendants may override this property.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
        protected virtual bool AllowArrowKeys
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>
        /// allows the function keys (F1, F2, ..., F12).
        /// Descendants may override this property.
        /// </summary>
        /// <returns>
        /// Always returns false.
        /// </returns>
        protected virtual bool AllowFunctionKeys
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>
        /// allows the special keys (Tab, BackSpace, Return, etc).
        /// Descendants may override this property.
        /// </summary>
        /// <returns>
        /// Always returns true.
        /// </returns>
        protected virtual bool AllowSpecialKeys
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>
        /// allows the space key.
        /// Descendants may override this property.
        /// </summary>
        /// <returns>
        /// Always returns false.
        /// </returns>
        protected virtual bool AllowSpaceKey
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>
        /// allows the numeric keys (0, 1, ..., 9). 
        /// Descendants may override this property.
        /// </summary>
        /// <returns>
        /// Always returns false.
        /// </returns>
        protected virtual bool AllowNumericKeys
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>
        /// allows the decimal separator key. 
        /// Descendants may override this function.
        /// </summary>
        /// <returns>
        /// Always returns false.
        /// </returns>
        protected virtual bool AllowDecimalSeparatorKey
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>
        /// allows the negative sign key. 
        /// Descendants may override this function.
        /// </summary>
        /// <returns>
        /// Always returns false.
        /// </returns>
        protected virtual bool AllowNegativeSignKey
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the stylesheet "text-align" property (CSS) value for the 
        /// <see cref="Adf.Web.UI.CharacterFilteringTextBox"/>.
        /// Descendants may override this function.
        /// <remarks>
        /// When this property is not set, the stylesheet "text-align" property will not be set.
        /// Valid values to return are '', left', 'center' and 'right'.
        /// </remarks>
        /// </summary>
        /// <returns>
        /// Always returns an empty string.
        /// </returns>
        protected virtual string TextAlign
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Creates a JavaScript function.
        /// </summary>
        /// <remarks>
        /// This method can be implemented by descendants and it must call the
        /// RegisterClientScriptFunction(string innerCode) function. Overriding this
        /// method is only needed, when special javascript is needed, that goes
        /// behond the standard protected virtual boolean properties.
        /// </remarks>
        protected virtual void CreateJavascriptFunction()
        {
            RegisterClientScriptFunction(String.Empty);
        }

        /// <summary>
        /// Creates default code for the descendants and puts the specified special JavaScript code
        /// (innerCode) from the descendants inside the created function.
        /// </summary>
        /// <param name="innerCode">The descendant dependent JavaScript code.</param>
        protected void RegisterClientScriptFunction(string innerCode)
        {
            // Check if the function has already been registered, 
            // before we're doing more than strictly needed.
            if (Page.ClientScript.IsClientScriptBlockRegistered(OnKeyPressFunctionName) == false)
            {
                StringBuilder jsCode = new StringBuilder();

                jsCode.Append("<script language=\"javascript\"><!--\n");
                jsCode.Append("function " + OnKeyPressFunctionName + "(evt)");
                jsCode.Append("{" + "	evt = (evt) ? evt : ((window.event) ? event : null);" + "	if (evt)" + "	{" + "		var charCode = (evt.charCode) ? evt.charCode :" + "		((evt.keyCode) ? evt.keyCode : " + "		((evt.which) ? evt.which : 0));" + "		var ch = String.fromCharCode(charCode);");

                // Add the code from the descendant whitch implements valid key's and characters.
                jsCode.Append(innerCode);

                if (AllowAltKey) jsCode.Append(altKey).Append("\n");
                if (AllowCtrlKey) jsCode.Append(ctrlKey).Append("\n");
                if (AllowArrowKeys) jsCode.Append(arrowKeys).Append("\n");
                if (AllowDecimalSeparatorKey) jsCode.Append(_numberDecimalSeparatorKey).Append("\n");
                if (AllowFunctionKeys) jsCode.Append(functionKeys).Append("\n");
                if (AllowNegativeSignKey) jsCode.Append(_negativeSignKey).Append("\n");
                if (AllowNumericKeys) jsCode.Append(numericKeys).Append("\n");
                if (AllowSpaceKey) jsCode.Append(spaceKey).Append("\n");
                if (AllowSpecialKeys) jsCode.Append(specialKeys).Append("\n");

                jsCode.Append(@"
							if (window.event) evt.returnValue = false;
							else evt.preventDefault();
						}
					}
				");
                jsCode.Append("\n--></script>");

                Page.ClientScript.RegisterClientScriptBlock(GetType(), //type of the control injecting the script
                                                            OnKeyPressFunctionName, // the name of the function
                                                            jsCode.ToString() // convert to string
                    );
            }
        }

        /// <summary>
        /// Registers JavaScript function to a web page and adds the 'OnKeyPress' event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> containing event data.</param>
        [SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength")]
        protected override void OnPreRender(EventArgs e)
        {
            CreateJavascriptFunction();

            // Adding the TextAlign property to the control's stylesheet.
            if (TextAlign != null && TextAlign != String.Empty)
                Style.Add(HtmlTextWriterStyle.TextAlign, TextAlign);

            Attributes.Add("onkeypress", OnKeyPressFunctionName + "(event)");

            base.OnPreRender(e);
        }
    }
}