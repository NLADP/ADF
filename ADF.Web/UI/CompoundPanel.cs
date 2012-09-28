using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Adf.Web.Helper;
using Adf.Web.UI.Styling;

namespace Adf.Web.UI
{
    /// <summary>
    /// Defination of compatible control types.
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// Declaring TextBox as 0
        /// </summary>
        TextBox,
        /// <summary>
        /// Declaring MultiTextBox as 1
        /// </summary>
        MultiTextBox,
        /// <summary>
        /// Declaring DateTextBox as 2
        /// </summary>
        DateTextBox,
        /// <summary>
        /// Declaring DateTimeTextBox as 3
        /// </summary>
        DateTimeTextBox,
        /// <summary>
        /// Declaring NumericTextBox as 4
        /// </summary>
        NumericTextBox,
        /// <summary>
        /// Declaring SignedIntegerTextbox as 5
        /// </summary>
        SignedIntegerTextbox,
        /// <summary>
        /// Declaring UnsignedIntegerTextbox as 6
        /// </summary>
        UnsignedIntegerTextbox,
        /// <summary>
        /// Declaring Label as 7
        /// </summary>
        Label,
        /// <summary>
        /// Declaring DropDownList as 8
        /// </summary>
        DropDownList,
        /// <summary>
        /// Declaring CheckBox as 9
        /// </summary>
        CheckBox,
        /// <summary>
        /// Declaring RadioVertical as 10
        /// </summary>
        RadioVertical,
        /// <summary>
        /// Declaring RadioHorizontal as 11
        /// </summary>
        RadioHorizontal,
        /// <summary>
        /// Declaring Line as 12
        /// </summary>
        Line,
        /// <summary>
        /// Declaring Blank as 13
        /// </summary>
        Blank,
        /// <summary>
        /// Declaring Title as 14
        /// </summary>
        Title,
        /// <summary>
        /// Declaring Calendar as 15
        /// </summary>
        Calendar,
        /// <summary>
        /// Declaring Password as 16
        /// </summary>
        Password,
        /// <summary>
        /// Declaring HyperLink as 17
        /// </summary>
        HyperLink
    }

    /// <summary>
    /// Represents various properties of a control
    /// </summary>
    public class ControlStruct
    {
        private ControlType type;
        private string label;
        private bool hasLabel = true;
        private string name;
        private int width;
        private bool enabled = true;
        private int height;

        /// <summary>
        /// Gets or sets the <see cref="ControlType"/>
        /// </summary>
        /// <returns><see cref="ControlType"/></returns>
        public ControlType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Gets or sets the Label text
        /// </summary>
        /// <returns>The text of the label</returns>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        /// <summary>
        /// Gets or sets whether control has a label
        /// </summary>
        /// <returns>Returns True if control has label, else False</returns>
        public bool HasLabel
        {
            get { return hasLabel; }
            set { hasLabel = value; }
        }

        /// <summary>
        /// Gets or sets the Name of control
        /// </summary>
        /// <returns>Returns the name of the control</returns>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the width of control
        /// </summary>
        /// <returns>Returns the width of the control</returns>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets the Enabled status of control
        /// </summary>
        /// <returns>True if control is Enabled, else False</returns>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// Gets or sets the Height of control
        /// </summary>
        /// <returns>Returns the Height of the control</returns>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
    }

    /// <summary>
    /// Provides methods to register controls in panel.
    /// </summary>
    public abstract class CompoundPanel : WebControl, INamingContainer
    {
        /// <summary>
        /// Declare a new variable of <see cref="List{T}"/>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        protected List<ControlStruct> Lines = new List<ControlStruct>();

        /// <summary>
        /// Declare a new variable of <see cref="HtmlTable"/>
        /// </summary>
        protected HtmlTable innerTable = new HtmlTable();
        //todo replace default widths by constant        
        private const int DEFAULT_WIDTH = 100;

        /// <summary>
        /// Finds a control against a specified ID
        /// </summary>
        /// <param name="id">ID of control</param>
        /// <returns>The <see cref="Control"/> against the specified ID</returns>
        public Control Find(string id)
        {
            return ControlHelper.Find(this, id);
        }

        /// <summary>
        /// Gets the total count of controls
        /// </summary>
        /// <returns>The count of controls found"/></returns>
        public int Count
        {
            get { return Lines.Count; }
        }

        #region Bindable Properties

        private int labelCellWidth = 25;
        /// <summary>
        /// Gets or sets the width of LabelCell
        /// </summary>
        /// <returns>The width of the LabelCell</returns>
        [Bindable(true), Category("Compound Panel"), DefaultValue(25)]
        public int LabelCellWidth
        {
            get { return labelCellWidth; }
            set { labelCellWidth = value; }
        }

        private int controlCellWidth = 75;
        /// <summary>
        /// Gets or sets the width of ControlCell
        /// </summary>
        /// <returns>The width of ControlCell"/></returns>
        [Bindable(true), Category("Compound Panel"), DefaultValue(75)]
        public int ControlCellWidth
        {
            get { return controlCellWidth; }
            set { controlCellWidth = value; }
        }

        private bool editable = true;
        /// <summary>
        /// Gets or sets the boolean value for editable or not.
        /// </summary>
        /// <returns>Returns True if Editable, else False"/></returns>
        [Bindable(true), Category("Compound Panel"), DefaultValue(true)]
        public bool Editable
        {
            get { return editable; }
            set { editable = value; }
        }

        #endregion

        #region Enumerator for Values

        /// <summary>
        /// Gets the value of a control against which the specified field is bound.
        /// </summary>
        /// <param name="fieldname">Name of the field.</param>
        /// <returns>The value of the control against which the specified field is bound.</returns>
        public string this[string fieldname]
        {
            get
            {
                Control c = ControlHelper.Find(this, fieldname);

                return ControlHelper.GetValue(c);
            }
        }

        #endregion

        #region Styles

        private static IStyler styler = NullStyler.Empty;
        private bool autostyle = true;

        /// <summary>
        /// Gets or sets <see cref="IStyler"/>
        /// </summary>
        /// <returns><see cref="IStyler"/></returns>
        public static IStyler Styler
        {
            get { return styler; }
            set { styler = value; }
        }

        /// <summary>
        /// Gets or sets auto style property to specify whether styling should be done when control is created
        /// </summary>
        /// <returns>Returns True if auto styler property is on, else False.</returns>
        [Bindable(true), Category("BusinessGrid"), DefaultValue(true)]
        public bool AutoStyle
        {
            get { return autostyle; }
            set { autostyle = value; }
        }

        /// <summary>
        /// Declare variable TitleStyle of type <see cref="TableItemStyle"/>
        /// </summary>
        public TableItemStyle TitleStyle = StyleHelper.Empty;

        /// <summary>
        /// Declare variable TitleLabelStyle of type <see cref="TableItemStyle"/>
        /// </summary>
        public TableItemStyle TitleLabelStyle = StyleHelper.Empty;

        /// <summary>
        /// Declare variable RowStyle of type <see cref="TableItemStyle"/>
        /// </summary>
        public TableItemStyle RowStyle = StyleHelper.Empty;

        /// <summary>
        /// Declare variable LabelCellStyle of type <see cref="TableItemStyle"/>
        /// </summary>
        public TableItemStyle LabelCellStyle = StyleHelper.Empty;

        /// <summary>
        /// Declare variable ControlCellStyle of type <see cref="TableItemStyle"/>
        /// </summary>
        public TableItemStyle ControlCellStyle = StyleHelper.Empty;

        #endregion

        #region Control Registration

        /// <summary>
        /// Registers a control with specified <see cref="ControlType"/>
        /// </summary>
        /// <param name="type"><see cref="ControlType"/>.</param>
        protected void Register(ControlType type)
        {
            Register(type, string.Empty, string.Empty, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a control with specified <see cref="ControlType"/>, display name and name
        /// </summary>
        /// <param name="type"><see cref="ControlType"/>.</param>
        /// <param name="label">Control display name.</param>
        /// <param name="name">Control name.</param>
        protected void Register(ControlType type, string label, string name)
        {
            Register(type, label, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a control with specified <see cref="ControlType"/>, display name, name and width
        /// </summary>
        /// <param name="type"><see cref="ControlType"/>.</param>
        /// <param name="label">Control display name.</param>
        /// <param name="name">Control name.</param>
        /// <param name="width">Control width.</param>
        protected void Register(ControlType type, string label, string name, int width)
        {
            Register(type, label, name, 0, width, true, true);
        }

        /// <summary>
        /// Registers a control as per specified parameter.
        /// </summary>
        /// <param name="type"><see cref="ControlType"/>.</param>
        /// <param name="label">Control display name.</param>
        /// <param name="name">Control name.</param>
        /// <param name="height">Control height.</param>
        /// <param name="width">Control width.</param>
        /// <param name="enabled">Enable the control.</param>
        /// <param name="haslabel">Set HasLabel of <see cref="ControlType"/>.</param>
        protected void Register(ControlType type, string label, string name, int height, int width, bool enabled, bool haslabel)
        {
            ControlStruct cs = new ControlStruct();

            cs.Type = type;
            cs.Label = label;
            cs.Name = name;
            cs.Height = height;
            cs.Width = width;
            cs.Enabled = enabled;
            cs.HasLabel = haslabel;

            Lines.Add(cs);
        }

        #endregion

        #region Register Line

        /// <summary>
        /// Registers a Line with default width
        /// </summary>
        public void Line()
        {
            Line(1, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a Line with the specified width
        /// </summary>
        /// <param name="width">Control width.</param>
        public void Line(int width)
        {
            Register(ControlType.Line, string.Empty, string.Empty, 1, width, false, false);
        }

        /// <summary>
        /// Registers a Line with the specified height and width
        /// </summary>
        /// <param name="height">Control height.</param>
        /// <param name="width">Control width.</param>
        public void Line(int height, int width)
        {
            Register(ControlType.Line, string.Empty, string.Empty, height, width, false, false);
        }

        #endregion

        #region Register CheckBox

        /// <summary>
        /// Registers a CheckBox with the specified name.
        /// </summary>
        /// <param name="name">Control name.</param>
        public void CheckBox(string name)
        {
            Register(ControlType.CheckBox, name, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a CheckBox with the specified display name and name.
        /// </summary>
        /// <param name="label">Control display name.</param>
        /// <param name="name">Control name.</param>
        public void CheckBox(string label, string name)
        {
            Register(ControlType.CheckBox, label, name, DEFAULT_WIDTH);
        }

        #endregion

        #region Register Blank

        /// <summary>
        /// Registers a blank space.
        /// </summary>
        public void Blank()
        {
            Register(ControlType.Blank, string.Empty, string.Empty, 0, DEFAULT_WIDTH, false, false);
        }

        #endregion

        #region Register Title

        /// <summary>
        /// Register title of control.
        /// </summary>
        /// <param name="name">Control name.</param>
        public void Title(string name)
        {
            Register(ControlType.Title, name, name, 0, DEFAULT_WIDTH, false, false);
        }

        #endregion Register Title

        #region Register TextBox

        /// <summary>
        /// Registers a TextBox with the specified name.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        public void TextBox(string name)
        {
            TextBox(name, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a TextBox with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void TextBox(string name, int width)
        {
            TextBox(name, name, width, true);
        }

        /// <summary>
        /// Registers a TextBox with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        public void TextBox(string label, string name)
        {
            TextBox(label, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a TextBox with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void TextBox(string label, string name, int width)
        {
            TextBox(label, name, width, true);
        }

        /// <summary>
        /// Registers a TextBox with the specified display name, name, width and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void TextBox(string label, string name, bool enabled)
        {
            TextBox(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a TextBox with the specified display name, name, width and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void TextBox(string label, string name, int width, bool enabled)
        {
            Register(ControlType.TextBox, label, name, 0, width, enabled, true);
        }

        #endregion TextBox

        #region Register NumericTextBox

        /// <summary>
        /// Registers a Numeric TextBox with the specified name.
        /// </summary>
        /// <param name="name">Name of the Numeric Textbox.</param>
        public void NumericTextBox(string name)
        {
            NumericTextBox(name, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Numeric TextBox with the specified name and width
        /// </summary>
        /// <param name="name">Name of the Numeric Textbox.</param>
        /// <param name="width">Width of the Numeric Textbox.</param> 
        public void NumericTextBox(string name, int width)
        {
            NumericTextBox(name, name, width, true);
        }

        /// <summary>
        /// Registers a Numeric TextBox with the specified display name and name
        /// </summary>
        /// <param name="label">Display Name of the Numeric Textbox.</param>
        /// <param name="name">Name of the Numeric Textbox.</param>
        public void NumericTextBox(string label, string name)
        {
            NumericTextBox(label, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Numeric TextBox with the specified display name, name and width
        /// </summary>
        /// <param name="label">Display Name of the Numeric Textbox.</param>
        /// <param name="name">Name of the Numeric Textbox.</param>
        /// <param name="width">Width of the Numeric Textbox.</param>        
        public void NumericTextBox(string label, string name, int width)
        {
            NumericTextBox(label, name, width, true);
        }

        /// <summary>
        /// Registers a Numeric TextBox with the specified display name, name and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Numeric Textbox.</param>
        /// <param name="name">Name of the Numeric Textbox.</param>        
        /// <param name="enabled">Enable the Numeric Textbox.</param>
        public void NumericTextBox(string label, string name, bool enabled)
        {
            NumericTextBox(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a Numeric TextBox with the specified display name, name, width and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Numeric Textbox.</param>
        /// <param name="name">Name of the Numeric Textbox.</param>
        /// <param name="width">Width of the Numeric Textbox.</param>
        /// <param name="enabled">Enable the Numeric Textbox.</param>
        public void NumericTextBox(string label, string name, int width, bool enabled)
        {
            Register(ControlType.NumericTextBox, label, name, 0, width, enabled, true);
        }

        #endregion NumericTextBox

        #region Register SignedIntegerTextBox

        /// <summary>
        /// Registers a Signed Integer TextBox with the specified name.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        public void SignedIntegerTextBox(string name)
        {
            SignedIntegerTextBox(name, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Signed Integer TextBox with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void SignedIntegerTextBox(string name, int width)
        {
            SignedIntegerTextBox(name, name, width, true);
        }

        /// <summary>
        /// Registers a Signed Integer TextBox with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        public void SignedIntegerTextBox(string label, string name)
        {
            SignedIntegerTextBox(label, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Signed Integer TextBox with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void SignedIntegerTextBox(string label, string name, int width)
        {
            SignedIntegerTextBox(label, name, width, true);
        }

        /// <summary>
        /// Registers a Signed Integer TextBox with the specified display name, name, width and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void SignedIntegerTextBox(string label, string name, bool enabled)
        {
            SignedIntegerTextBox(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a Signed Integer TextBox with the specified display name, name, width and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void SignedIntegerTextBox(string label, string name, int width, bool enabled)
        {
            Register(ControlType.SignedIntegerTextbox, label, name, 0, width, enabled, true);
        }

        #endregion SignedIntegerTextBox

        #region Register UnsignedIntegerTextBox

        /// <summary>
        /// Registers a Unsigned Integer TextBox with the specified name.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        public void UnsignedIntegerTextBox(string name)
        {
            UnsignedIntegerTextBox(name, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Unsigned Integer TextBox with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void UnsignedIntegerTextBox(string name, int width)
        {
            UnsignedIntegerTextBox(name, name, width, true);
        }

        /// <summary>
        /// Registers a Unsigned Integer TextBox with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        public void UnsignedIntegerTextBox(string label, string name)
        {
            UnsignedIntegerTextBox(label, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Unsigned Integer TextBox with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void UnsignedIntegerTextBox(string label, string name, int width)
        {
            UnsignedIntegerTextBox(label, name, width, true);
        }

        /// <summary>
        /// Registers a Unsigned Integer TextBox with the specified display name, name, width and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void UnsignedIntegerextBox(string label, string name, bool enabled)
        {
            UnsignedIntegerTextBox(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a Unsigned Integer TextBox with the specified display name, name, width and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void UnsignedIntegerTextBox(string label, string name, int width, bool enabled)
        {
            Register(ControlType.UnsignedIntegerTextbox, label, name, 0, width, enabled, true);
        }

        #endregion UnsignedIntegerTextBox

        #region Register MultiTextBox

        /// <summary>
        /// Registers a Multiline TextBox with the specified name and height.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="height">Height of the Textbox.</param>
        public void MultiTextBox(string name, int height)
        {
            MultiTextBox(name, name, height, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Multiline TextBox with the specified name, height and width.
        /// </summary>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="height">Height of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void MultiTextBox(string name, int height, int width)
        {
            MultiTextBox(name, name, height, width, true);
        }

        /// <summary>
        /// Registers a Multiline TextBox with the specified display name, name and height.
        /// </summary>
        /// <param name="label">Display name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="height">Height of the Textbox.</param>
        public void MultiTextBox(string label, string name, int height)
        {
            MultiTextBox(label, name, height, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Multiline TextBox with the specified display name, name, height and width.
        /// </summary>
        /// <param name="label">Display name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="height">Height of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        public void MultiTextBox(string label, string name, int height, int width)
        {
            MultiTextBox(label, name, height, width, true);
        }

        /// <summary>
        /// Registers a Multiline TextBox with the specified display name, name, height
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="height">Height of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void MultiTextBox(string label, string name, int height, bool enabled)
        {
            MultiTextBox(label, name, height, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a Multiline TextBox with the specified display name, name, height
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display name of the Textbox.</param>
        /// <param name="name">Name of the Textbox.</param>
        /// <param name="height">Height of the Textbox.</param>
        /// <param name="width">Width of the Textbox.</param>
        /// <param name="enabled">Enable the Textbox.</param>
        public void MultiTextBox(string label, string name, int height, int width, bool enabled)
        {
            Register(ControlType.MultiTextBox, label, name, height, width, enabled, true);
        }

        #endregion TextBox

        #region Register Label

        /// <summary>
        /// Registers a Label with the specified name.
        /// </summary>
        /// <param name="name">Name of the Label.</param>
        public void Label(string name)
        {
            Label(name, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a Label with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the Label.</param>
        /// <param name="width">Width of the Label.</param>
        public void Label(string name, int width)
        {
            Label(name, name, width);
        }

        /// <summary>
        /// Registers a Label with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the Label.</param>
        /// <param name="labelname">Display Name of the Label.</param>
        /// <param name="width">Width of the Label.</param>        
        public void Label(string labelname, string name, int width)
        {
            Register(ControlType.Label, labelname, name, DEFAULT_WIDTH);
        }

        #endregion Register Label

        #region Register HyperLink

        /// <summary>
        /// Registers a HyperLink with the specified name.
        /// </summary>
        /// <param name="name">Name of the HyperLink.</param>
        public void HyperLink(string name)
        {
            HyperLink(name, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a HyperLink with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the HyperLink.</param>
        /// <param name="width">Width of the HyperLink.</param>
        public void HyperLink(string name, int width)
        {
            HyperLink(name, name, width);
        }

        /// <summary>
        /// Registers a HyperLink with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display name of the HyperLink.</param>
        /// <param name="name">Name of the HyperLink.</param>
        /// <param name="width">Width of the HyperLink.</param>
        public void HyperLink(string label, string name, int width)
        {
            Register(ControlType.HyperLink, label, name, DEFAULT_WIDTH);
        }

        #endregion Register Label

        #region Register DateTextBox

        /// <summary>
        /// Registers a Date TextBox with the specified name.
        /// </summary>
        /// <param name="name">Name of the Date TextBox</param>
        public void DateTextBox(string name)
        {
            Register(ControlType.DateTextBox, name, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a Date TextBox with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the Date TextBox</param>
        /// <param name="width">Width of the Date TextBox.</param>
        public void DateTextBox(string name, int width)
        {
            Register(ControlType.DateTextBox, name, name, width);
        }

        /// <summary>
        /// Registers a Date TextBox with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the Date TextBox.</param>
        /// <param name="name">Name of the Date TextBox.</param>
        public void DateTextBox(string label, string name)
        {
            Register(ControlType.DateTextBox, label, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a Date TextBox with the specified display name, name
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Date TextBox.</param>
        /// <param name="name">Name of the Date TextBox.</param>
        /// <param name="enabled">Enable the Date TextBox.</param>
        public void DateTextBox(string label, string name, bool enabled)
        {
            DateTextBox(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a Date TextBox with the specified display name, name, width 
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Date Textbox.</param>
        /// <param name="name">Name of the Date Textbox.</param>
        /// <param name="width">Width of the Date Textbox.</param>
        /// <param name="enabled">Enable the Date Textbox.</param>
        public void DateTextBox(string label, string name, int width, bool enabled)
        {
            Register(ControlType.DateTextBox, label, name, 0, width, enabled, true);
        }

        #endregion DateTextBox

        #region Register DateTimeTextBox

        /// <summary>
        /// Registers a DateTime TextBox with the specified name.
        /// </summary>
        /// <param name="name">Name of the DateTime TextBox</param>
        public void DateTimeTextBox(string name)
        {
            Register(ControlType.DateTimeTextBox, name, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a DateTime TextBox with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the DateTime TextBox</param>
        /// <param name="width">Width of the DateTime TextBox.</param>
        public void DateTimeTextBox(string name, int width)
        {
            Register(ControlType.DateTimeTextBox, name, name, width);
        }

        /// <summary>
        /// Registers a DateTime TextBox with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the DateTime TextBox.</param>
        /// <param name="name">Name of the DateTime TextBox.</param>
        public void DateTimeTextBox(string label, string name)
        {
            Register(ControlType.DateTimeTextBox, label, name, DEFAULT_WIDTH);
        }

        /// <summary>
        /// Registers a DateTime TextBox with the specified display name, name
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the DateTime TextBox.</param>
        /// <param name="name">Name of the DateTime TextBox.</param>
        /// <param name="enabled">Enable the DateTime TextBox.</param>
        public void DateTimeTextBox(string label, string name, bool enabled)
        {
            DateTimeTextBox(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a DateTime TextBox with the specified display name, name, width 
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the DateTime Textbox.</param>
        /// <param name="name">Name of the DateTime Textbox.</param>
        /// <param name="width">Width of the DateTime Textbox.</param>
        /// <param name="enabled">Enable the DateTime Textbox.</param>
        public void DateTimeTextBox(string label, string name, int width, bool enabled)
        {
            Register(ControlType.DateTimeTextBox, label, name, 0, width, enabled, true);
        }

        #endregion DateTimeTextBox

        #region Register Password

        /// <summary>
        /// Registers a Password TextBox with the specified name.
        /// </summary>
        /// <param name="name">Name of the TextBox.</param>
        public void Password(string name)
        {
            Password(name, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Password TextBox with the specified name and width.
        /// </summary>
        /// <param name="name">Name of the TextBox.</param>
        /// <param name="width">Width of the TextBox..</param>
        public void Password(string name, int width)
        {
            Password(name, name, width, true);
        }

        /// <summary>
        /// Registers a Password TextBox with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the TextBox.</param>
        /// <param name="name">Name of the TextBox.</param>
        public void Password(string label, string name)
        {
            Password(label, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Password TextBox with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the TextBox.</param>
        /// <param name="name">Name of the TextBox.</param>
        /// <param name="width">Width of the TextBox.</param>
        public void Password(string label, string name, int width)
        {
            Password(label, name, width, true);
        }

        /// <summary>
        /// Registers a Password TextBox with the specified display name, name
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the TextBox.</param>
        /// <param name="name">Name of the TextBox.</param>
        /// <param name="enabled">Enable the TextBox.</param>
        public void Password(string label, string name, bool enabled)
        {
            Password(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a Password TextBox with the specified display name, name
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the TextBox.</param>
        /// <param name="name">Name of the TextBox.</param>
        /// <param name="width">Width of the TextBox.</param>
        /// <param name="enabled">Enable the TextBox.</param>        
        public void Password(string label, string name, int width, bool enabled)
        {
            Register(ControlType.Password, label, name, 0, width, enabled, true);
        }

        #endregion TextBox

        #region Register Calendar

        /// <summary>
        /// Registers a Calendar control with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the Calendar control.</param>
        /// <param name="name">Name of the Calendar control.</param>
        public void Calendar(string label, string name)
        {
            Calendar(label, name, DEFAULT_WIDTH, true);
        }

        /// <summary>
        /// Registers a Calendar control  with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the Calendar control.</param>
        /// <param name="name">Name of the Calendar control.</param>
        /// <param name="width">Width of the Calendar control.</param>
        public void Calendar(string label, string name, int width)
        {
            Calendar(label, name, width, true);
        }

        /// <summary>
        /// Registers a Calendar control  with the specified display name, name
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Calendar control .</param>
        /// <param name="name">Name of the Calendar control.</param>
        /// <param name="enabled">Enable the Calendar control.</param>
        public void Calendar(string label, string name, bool enabled)
        {
            Calendar(label, name, DEFAULT_WIDTH, enabled);
        }

        /// <summary>
        /// Registers a Calendar control  with the specified display name, name, width
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the Calendar control.</param>
        /// <param name="name">Name of the Calendar control.</param>
        /// <param name="width">Width of the Calendar control.</param>
        /// <param name="enabled">Enable the Calendar control.</param>
        public void Calendar(string label, string name, int width, bool enabled)
        {
            Register(ControlType.Calendar, label, name, 0, width, enabled, true);
        }

        #endregion

        #region Register DropDownList

        /// <summary>
        /// Registers a DropDownList with the specified name.
        /// </summary>
        /// <param name="name">Name of the DropDownList.</param>
        public void DropDown(string name)
        {
            Register(ControlType.DropDownList, name, name);
        }

        /// <summary>
        /// Registers a DropDownList with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the DropDownList.</param>
        /// <param name="name">Name of the DropDownList.</param>     
        public void DropDown(string label, string name)
        {
            Register(ControlType.DropDownList, label, name);
        }

        /// <summary>
        /// Registers a DropDownList with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the DropDownList.</param>
        /// <param name="name">Name of the DropDownList.</param>  
        /// <param name="width">Width of the DropDownList.</param>
        public void DropDown(string label, string name, int width)
        {
            Register(ControlType.DropDownList, label, name, 0, width, true, true);
        }

        /// <summary>
        /// Registers a DropDownList with the specified display name, name, width
        /// and have its state enabled or disabled as per value provided for parameter Enabled.
        /// </summary>
        /// <param name="label">Display Name of the DropDownList.</param>
        /// <param name="name">Name of the DropDownList.</param>
        /// <param name="width">Width of the DropDownList.</param>
        /// <param name="enabled">Enable the DropDownList.</param>
        public void DropDown(string label, string name, int width, bool enabled)
        {
            Register(ControlType.DropDownList, label, name, 0, width, enabled, true);
        }

        #endregion

        #region Register RadioButtonList

        /// <summary>
        /// Registers a horizontally aligned Radio button list with the specified name.
        /// </summary>
        /// <param name="name">Name of the Radio button.</param>
        public void RadioHorizontal(string name)
        {
            Register(ControlType.RadioHorizontal, name, name);
        }

        /// <summary>
        /// Registers a horizontally aligned Radio button list with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the Radio button.</param>
        /// <param name="name">Name of the Radio button.</param>
        public void RadioHorizontal(string label, string name)
        {
            Register(ControlType.RadioHorizontal, label, name);
        }

        /// <summary>
        /// Registers a horizontally aligned Radio button with list the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the Radio button.</param>
        /// <param name="name">Name of the Radio button.</param>
        /// <param name="width">Width of the Radio button.</param>
        public void RadioHorizontal(string label, string name, int width)
        {
            Register(ControlType.RadioHorizontal, label, name, width);
        }

        /// <summary>
        /// Registers a vertically aligned Radio button list with the specified name.
        /// </summary>
        /// <param name="name">Name of the Radio button.</param>
        public void RadioVertical(string name)
        {
            Register(ControlType.RadioVertical, name, name);
        }

        /// <summary>
        /// Registers a vertically aligned Radio button list with the specified display name and name.
        /// </summary>
        /// <param name="label">Display Name of the Radio button.</param>
        /// <param name="name">Name of the Radio button.</param>
        public void RadioVertical(string label, string name)
        {
            Register(ControlType.RadioVertical, label, name);
        }

        /// <summary>
        /// Registers a vertically aligned Radio button list with the specified display name, name and width.
        /// </summary>
        /// <param name="label">Display Name of the Radio button.</param>
        /// <param name="name">Name of the Radio button.</param>
        /// <param name="width">Width of the Radio button.</param>
        public void RadioVertical(string label, string name, int width)
        {
            Register(ControlType.RadioVertical, label, name, width);
        }

        #endregion

        #region Control Rendering

        /// <summary>
        /// Render line of <see cref="HtmlTableCell"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="HtmlTableCell"/></returns>
        protected static HtmlTableCell RenderLine(ControlStruct cs)
        {
            HtmlTableCell tc = new HtmlTableCell();

            if (cs != null)
            {
                tc.ColSpan = 100;
                tc.InnerHtml = "<HR noshade class=\"line\" width=\"" + cs.Width + "%\">";
            }
            return tc;
        }

        /// <summary>
        /// Render the blank space of <see cref="HtmlTableCell"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="HtmlTableCell"/></returns>
        protected static HtmlTableCell RenderBlank(ControlStruct cs)
        {
            HtmlTableCell tc = new HtmlTableCell();

            if (cs != null)
            {
                tc.ColSpan = 100;
                tc.InnerHtml = "&nbsp;";
                tc.Attributes.Add("class", "Blank");
            }
            return tc;
        }

        /// <summary>
        /// Render the title of <see cref="HtmlTableCell"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="HtmlTableCell"/></returns>
        protected HtmlTableCell RenderTitle(ControlStruct cs)
        {
            HtmlTableCell tc = new HtmlTableCell();

            if (cs != null)
            {
                Label title = new Label();
                title.Text = cs.Name;
                title.CssClass = TitleStyle.CssClass;
                title.Width = Unit.Percentage(100);

                tc.Controls.Add(title);
                tc.ColSpan = 100;
            }
            return tc;
        }

        /// <summary>
        /// Render the label
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.Label"/></returns>
        protected Label RenderLabel(ControlStruct cs)
        {
            Label label = new Label();

            if (cs != null)
            {
                label.ID = "lbl" + cs.Name;
                label.CssClass = ControlCellStyle.CssClass;
                label.Enabled = cs.Enabled;
                label.Width = Unit.Percentage(cs.Width);
            }
            return label;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.HyperLink"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.HyperLink"/></returns>
        protected HyperLink RenderHyperLink(ControlStruct cs)
        {
            HyperLink hyperLink = new HyperLink();

            if (cs != null)
            {
                hyperLink.ID = "hl" + cs.Name;
                hyperLink.CssClass = ControlCellStyle.CssClass;
                hyperLink.Enabled = cs.Enabled;
                hyperLink.Width = Unit.Percentage(cs.Width);
                hyperLink.Target = "new";
            }
            return hyperLink;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.Calendar"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.Calendar"/></returns>
        protected static Calendar RenderCalendar(ControlStruct cs)
        {
            BusinessCalendar calendar = new BusinessCalendar();

            if (cs != null)
            {
                calendar.ID = "cal" + cs.Name;
                //calendar.CssClass = ControlCellStyle.CssClass;
                calendar.Enabled = cs.Enabled;
                calendar.Width = Unit.Percentage(cs.Width);
            }
            return calendar;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.CheckBox"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.CheckBox"/></returns>
        protected CheckBox RenderCheckBox(ControlStruct cs)
        {
            CheckBox box = new CheckBox();

            if (cs != null)
            {
                box.ID = "cbx" + cs.Name;
                box.CssClass = ControlCellStyle.CssClass;
                box.Enabled = cs.Enabled;
                box.Width = Unit.Percentage(cs.Width);
            }
            return box;
        }

        /// <summary>
        /// Render the <see cref="DropDownList"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="DropDownList"/></returns>
        protected DropDownList RenderDropDown(ControlStruct cs)
        {
            DropDownList down = new DropDownList();

            if (cs != null)
            {
                down.ID = "ddl" + cs.Name;
                down.CssClass = ControlCellStyle.CssClass;
                down.Enabled = cs.Enabled;
                down.Width = Unit.Percentage(cs.Width);
            }
            return down;
        }

        /// <summary>
        /// Render the <see cref="RadioButtonList"/> by vertical repeatDirection
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="RadioButtonList"/></returns>
        protected RadioButtonList RenderRadioVertical(ControlStruct cs)
        {
            RadioButtonList list = new RadioButtonList();

            if (cs != null)
            {
                list.ID = "rbl" + cs.Name;
                list.CssClass = ControlCellStyle.CssClass;
                list.Enabled = cs.Enabled;
                list.RepeatDirection = RepeatDirection.Vertical;
                list.Width = Unit.Percentage(cs.Width);
            }
            return list;
        }

        /// <summary>
        /// Render the <see cref="RadioButtonList"/> by horizontal repeatDirection
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="RadioButtonList"/></returns>
        protected RadioButtonList RenderRadioHorizontal(ControlStruct cs)
        {
            RadioButtonList list = new RadioButtonList();

            if (cs != null)
            {
                list.ID = "rbl" + cs.Name;
                list.CssClass = ControlCellStyle.CssClass;
                list.Enabled = cs.Enabled;
                list.RepeatDirection = RepeatDirection.Horizontal;
                list.Width = Unit.Percentage(cs.Width);
            }
            return list;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.TextBox"/>
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.TextBox"/></returns>
        protected TextBox RenderTextBox(ControlStruct cs)
        {
            TextBox textBox = new TextBox();

            if (cs != null)
            {
                textBox.ID = "txt" + cs.Name;
                textBox.CssClass = ControlCellStyle.CssClass;
                textBox.TextMode = TextBoxMode.SingleLine;
                textBox.Wrap = true;
                textBox.Enabled = cs.Enabled;
                textBox.Width = Unit.Percentage(cs.Width);
                //textBox.Attributes["Width"] = "100%";
            }
            return textBox;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.TextBox"/> for Numeric value
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.TextBox"/></returns>
        protected TextBox RenderNumericTextBox(ControlStruct cs)
        {
            NumericTextBox numericTextBox = new NumericTextBox();

            if (cs != null)
            {
                numericTextBox.ID = "txt" + cs.Name;
                numericTextBox.CssClass = ControlCellStyle.CssClass;
                numericTextBox.TextMode = TextBoxMode.SingleLine;
                numericTextBox.Wrap = false;
                numericTextBox.Enabled = cs.Enabled;
                numericTextBox.Width = Unit.Percentage(cs.Width);
            }
            return numericTextBox;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.TextBox"/> for Signed Integer value
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.TextBox"/></returns>
        protected TextBox RenderSignedIntegerTextBox(ControlStruct cs)
        {
            SignedIntegerTextBox signedIntegerTextBox = new SignedIntegerTextBox();

            if (cs != null)
            {
                signedIntegerTextBox.ID = "txt" + cs.Name;
                signedIntegerTextBox.CssClass = ControlCellStyle.CssClass;
                signedIntegerTextBox.TextMode = TextBoxMode.SingleLine;
                signedIntegerTextBox.Wrap = false;
                signedIntegerTextBox.Enabled = cs.Enabled;
                signedIntegerTextBox.Width = Unit.Percentage(cs.Width);
            }
            return signedIntegerTextBox;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.TextBox"/> for Unsigned Integer value
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.TextBox"/></returns>
        protected TextBox RenderUnsignedIntegerTextBox(ControlStruct cs)
        {
            UnsignedIntegerTextBox unsignedIntegerTextBox = new UnsignedIntegerTextBox();

            if (cs != null)
            {
                unsignedIntegerTextBox.ID = "txt" + cs.Name;
                unsignedIntegerTextBox.CssClass = ControlCellStyle.CssClass;
                unsignedIntegerTextBox.TextMode = TextBoxMode.SingleLine;
                unsignedIntegerTextBox.Wrap = false;
                unsignedIntegerTextBox.Enabled = cs.Enabled;
                unsignedIntegerTextBox.Width = Unit.Percentage(cs.Width);
            }
            return unsignedIntegerTextBox;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.TextBox"/> for Password
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.TextBox"/></returns>
        protected TextBox RenderPassword(ControlStruct cs)
        {
            TextBox box = new TextBox();

            if (cs != null)
            {
                box.ID = "txt" + cs.Name;
                box.CssClass = ControlCellStyle.CssClass;
                box.TextMode = TextBoxMode.Password;
                box.Wrap = true;
                box.Enabled = cs.Enabled;
                box.Width = Unit.Percentage(cs.Width);
            }
            return box;
        }

        /// <summary>
        /// Render the <see cref="System.Web.UI.WebControls.TextBox"/> for Multi line text value
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="System.Web.UI.WebControls.TextBox"/></returns>
        protected TextBox RenderMultiTextBox(ControlStruct cs)
        {
            TextBox textBox = new TextBox();

            if (cs != null)
            {
                textBox.ID = "txt" + cs.Name;
                textBox.CssClass = ControlCellStyle.CssClass;
                textBox.TextMode = TextBoxMode.MultiLine;
                textBox.Wrap = true;
                textBox.Enabled = cs.Enabled;
                textBox.Height = Unit.Pixel(cs.Height);
                textBox.Width = Unit.Percentage(cs.Width);
            }
            return textBox;
        }

        /// <summary>
        /// Render the <see cref="Adf.Web.UI.DateTextBox"/> for DateTextBox
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="Adf.Web.UI.DateTextBox"/></returns>
        protected DateTextBox RenderDateTextBox(ControlStruct cs)
        {
            DateTextBox box = new DateTextBox();

            if (cs != null)
            {
                box.ID = "dtb" + cs.Name;
                box.CssClass = ControlCellStyle.CssClass;
                box.Enabled = cs.Enabled;
                box.Width = Unit.Percentage(cs.Width);
            }
            return box;
        }

        /// <summary>
        /// Render the <see cref="Adf.Web.UI.DateTextBox"/> for DateTime value
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="Adf.Web.UI.DateTextBox"/></returns>
        protected DateTextBox RenderDateTimeTextBox(ControlStruct cs)
        {
            DateTextBox box = new DateTextBox("dd-MM-yyyy HH:mm");
            if (cs != null)
            {
                box.ID = "dtb" + cs.Name;
                box.CssClass = ControlCellStyle.CssClass;
                box.Enabled = cs.Enabled;
                box.Width = Unit.Percentage(cs.Width);

            }

            return box;
        }


        #endregion

        #region Additional Rendering

        /// <summary>
        /// Create html table row
        /// </summary>
        /// <param name="cellstyle"><see cref="TableItemStyle"/></param>
        /// <param name="cellwidth">Cell width.</param>
        /// <param name="controls">Variable parameters.</param>
        /// <returns><see cref="HtmlTableRow"/></returns>
        protected HtmlTableRow ComposeRow(TableItemStyle cellstyle, int cellwidth, params Control[] controls)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = ComposeCell(cellstyle, cellwidth, controls);

            row.Attributes["class"] = RowStyle.CssClass;
            row.Controls.Add(cell);

            return row;
        }

        /// <summary>
        /// Create html table column.
        /// </summary>
        /// <param name="cellstyle"><see cref="TableItemStyle"/></param>
        /// <param name="width">Column width.</param>
        /// <param name="controls">Variable parameters.</param>
        /// <returns><see cref="HtmlTableCell"/></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected static HtmlTableCell ComposeCell(TableItemStyle cellstyle, int width, params Control[] controls)
        {
            HtmlTableCell cell = null;

            // Some controls are already HtmlTableCell, in which case they should not be encapsulated.
            if (controls.Length == 1)
            {
                cell = controls[0] as HtmlTableCell;
            }

            // If the above is not the case, initialise the HtmlTableCell and add the controls.
            if (cell == null)
            {
                cell = new HtmlTableCell();

                foreach (Control control in controls)
                {
                    cell.Controls.Add(control);
                }
            }

            if (cellstyle != null)
                cell.Attributes["class"] = cellstyle.CssClass;

            cell.Width = width + "%";

            return cell;
        }

        #endregion

        #region General Rendering

        /// <summary>
        /// Creates a label control.
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns>Created label.</returns>
        protected virtual Control CreateLabel(ControlStruct cs)
        {
            Label label = new Label();

            if ((cs.Type == ControlType.Blank) || (cs.Type == ControlType.Line)) return label;

            label.Text = cs.Label;
            label.CssClass = TitleLabelStyle.CssClass;

            return label;
        }

        /// <summary>
        /// Add and create controls
        /// </summary>
        /// <param name="cs"><see cref="ControlStruct"/></param>
        /// <returns><see cref="ArrayList"/> of controls</returns>
        protected virtual Control[] CreateControls(ControlStruct cs)
        {
            ArrayList controls = new ArrayList();

            if (cs.Type == ControlType.Label) controls.Add(RenderLabel(cs));
            else if (cs.Type == ControlType.TextBox) controls.Add(RenderTextBox(cs));
            else if (cs.Type == ControlType.NumericTextBox) controls.Add(RenderNumericTextBox(cs));
            else if (cs.Type == ControlType.SignedIntegerTextbox) controls.Add(RenderSignedIntegerTextBox(cs));
            else if (cs.Type == ControlType.UnsignedIntegerTextbox) controls.Add(RenderUnsignedIntegerTextBox(cs));
            else if (cs.Type == ControlType.Password) controls.Add(RenderPassword(cs));
            else if (cs.Type == ControlType.MultiTextBox) controls.Add(RenderMultiTextBox(cs));
            else if (cs.Type == ControlType.Line) controls.Add(RenderLine(cs));
            else if (cs.Type == ControlType.Label) controls.Add(RenderLabel(cs));
            else if (cs.Type == ControlType.Blank) controls.Add(RenderBlank(cs));
            else if (cs.Type == ControlType.Calendar) controls.Add(RenderCalendar(cs));
            else if (cs.Type == ControlType.DateTextBox) controls.Add(RenderDateTextBox(cs));
            else if (cs.Type == ControlType.DateTimeTextBox) controls.Add(RenderDateTimeTextBox(cs));
            else if (cs.Type == ControlType.HyperLink) controls.Add(RenderHyperLink(cs));
            else if (cs.Type == ControlType.DropDownList) controls.Add(RenderDropDown(cs));
            else if (cs.Type == ControlType.CheckBox) controls.Add(RenderCheckBox(cs));
            else if (cs.Type == ControlType.RadioHorizontal) controls.Add(RenderRadioHorizontal(cs));
            else if (cs.Type == ControlType.RadioVertical) controls.Add(RenderRadioVertical(cs));
            else if (cs.Type == ControlType.Title) controls.Add(RenderTitle(cs));

            return controls.ToArray(typeof(Control)) as Control[];
        }

        /// <summary>
        /// Provides a signature for rendering row in a HtmlTable with the specified display name and control.
        /// </summary>
        /// <param name="table"><see cref="HtmlTable"/></param>
        /// <param name="label"><see cref="Control"/></param>
        /// <param name="controls">An array of <see cref="Control"/>.</param>
        protected abstract void RenderRow(HtmlTable table, Control label, Control[] controls);

        /// <summary>
        /// Provides a signature of render row in a HtmlTable with the specified array of controls.
        /// </summary>
        /// <param name="table"><see cref="HtmlTable"/></param>
        /// <param name="controls">An array of <see cref="Control"/>.</param>
        protected abstract void RenderRow(HtmlTable table, Control[] controls);

        /// <summary>
        /// Provides a signature to Initialize rendering of <see cref="HtmlTable"/>.
        /// </summary>
        /// <param name="table"><see cref="HtmlTable"/></param>
        protected virtual void InitRendering(HtmlTable table) { }

        /// <summary>
        /// Provides a signature to finalise rendering of <see cref="HtmlTable"/>
        /// </summary>
        /// <param name="table"><see cref="HtmlTable"/></param>
        protected virtual void FinaliseRendering(HtmlTable table) { }

        /// <summary>
        /// Renders a <see cref="HtmlTable"/>
        /// </summary>
        /// <param name="table"><see cref="HtmlTable"/></param>
        protected virtual void RenderTable(HtmlTable table)
        {
            //table.Width = "100%";
            table.CellPadding = 0;
            table.CellSpacing = 0;
            table.Border = 0;
        }

        /// <summary>
        /// Creates child control in a htmlTable
        /// </summary>
        protected override void CreateChildControls()
        {
            if (AutoStyle) Styler.SetStyles(this);

            HtmlTable htmlTable = new HtmlTable();

            InitRendering(htmlTable);

            foreach (ControlStruct cs in Lines)
            {
                Control label = CreateLabel(cs);
                Control[] controls = CreateControls(cs);

                if (cs.HasLabel) RenderRow(htmlTable, label, controls);
                else RenderRow(htmlTable, controls);
            }

            FinaliseRendering(htmlTable);

            RenderTable(htmlTable);

            Controls.Add(htmlTable);
        }

        /// <summary>
        /// Load control
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/></param>
        protected override void OnLoad(EventArgs e)
        {
        }

        /// <summary>
        /// Render control
        /// </summary>
        /// <param name="writer"><see cref="HtmlTextWriter"/></param>
        protected override void Render(HtmlTextWriter writer)
        {
            EnsureChildControls();
            EnableViewState = true;

            base.Render(writer);
        }

        #endregion
    }
}