using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represent a text box control for user input.
    /// Used as a control in the panel.
    /// </summary>
    public class TextBoxItem : BasePanelItem
    {
        private const string Prefix = "txt";

        private TextBox Textbox { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.TextBoxItem"/> class with the specified label and text box.
        /// </summary>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="textBox">The <see cref="System.Web.UI.WebControls.TextBox"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public TextBoxItem(Label label, TextBox textBox, Image infoIcon = null)
        {
            Textbox = textBox;

            _labelControls.Add(label);

            _itemControls.Add(textBox);

            if(infoIcon != null)
                _itemControls.Add(infoIcon);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.TextBox"/> for single-line entry and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.TextBox"/> control.</param>
        /// <param name="width">Width of <see cref="System.Web.UI.WebControls.TextBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.TextBox"/> control is enabled or not.</param>
        /// <param name="height">Height of control (0 = not multiline)</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.TextBoxItem"/> class.</returns>
        public static TextBoxItem Create(string label, string name, int width, bool enabled = true, int height = 0, TextBoxMode textBoxMode = TextBoxMode.SingleLine, string infoText = null)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var textBox = new TextBox
            {
                ID = Prefix + name,
                Wrap = true,
                ReadOnly = !enabled,
                Width = new Unit(width, UnitType.Ex),
                TextMode = textBoxMode,
            };

            textBox.PreRender += delegate { textBox.CssClass = (textBox.ReadOnly) ? ("TextBox ReadOnly") : ("TextBox"); };

            if (height > 0)
            {
                textBox.TextMode = TextBoxMode.MultiLine;
                textBox.Height = new Unit(height, UnitType.Ex);
            }

            Image infoIcon = null;

            if(!string.IsNullOrEmpty(infoText))
            {
                infoIcon = new Image
                {
                    ID = "imgInfo" + name,
                    ImageUrl = @"../images/info.png",
                    ToolTip = ResourceManager.GetString(infoText),
                    Enabled = false,
                    Width = new Unit(16, UnitType.Pixel),
                };
                infoIcon.Style.Add("margin-left", "5px");
            }

            return new TextBoxItem(l, textBox, infoIcon);
        }

        public static TextBoxItem Create<T>(Expression<Func<T, object>> property, int width, bool enabled = true, int height = 0, string infoText = null)
        {
            return Create(property.GetMemberInfo().Name, property, width, enabled, height, infoText);
        }

        public static TextBoxItem Create<T>(string label, Expression<Func<T, object>> property, int width, bool enabled = true, int height = 0, string infoText = null)
        {
            return Create(label, property.GetControlName(), width, enabled, height, infoText: infoText);
        }

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.TextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.TextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public TextBox TextBox
        {
            get { return (_itemControls.Count > 0) ? _itemControls[0] as TextBox : null; }
        }

        #region Overrides of BasePanelItem

        public override bool Enabled
        {
            get { return !Textbox.ReadOnly; }
            set { Textbox.ReadOnly = !value; }
        }

        #endregion
    }
}
