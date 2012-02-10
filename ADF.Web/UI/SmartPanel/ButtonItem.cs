using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represent a text box control for user input.
    /// Used as a control in the panel.
    /// </summary>
    public class ButtonItem : BasePanelItem
    {
        private const string Prefix = "lb";

        private LinkButton _linkButton    { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.TextBoxItem"/> class with the specified label and text box.
        /// </summary>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="linkButton">The <see cref="System.Web.UI.WebControls.TextBox"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public ButtonItem(Label label, LinkButton linkButton)
        {
            _linkButton = linkButton;

            _labelControls.Add(label);
            _itemControls.Add(_linkButton);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.TextBoxItem"/> class with the specified label and text box.
        /// </summary>
        /// <param name="linkButton">The <see cref="System.Web.UI.WebControls.TextBox"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public ButtonItem(LinkButton linkButton)
        {
            _linkButton = linkButton;

            _labelControls.Add(_linkButton);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.TextBox"/> for single-line entry and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.TextBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.TextBox"/> control is enabled or not.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.TextBoxItem"/> class.</returns>
        public static ButtonItem Create(string name, string label, string cssclass = "tbutton", bool enabled = true)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var linkbutton = new LinkButton { ID = Prefix + name.Replace(" ", ""), Text = label, Enabled = enabled, CssClass = cssclass };

            return new ButtonItem(l, linkbutton);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.TextBox"/> for single-line entry and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the text box within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.TextBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.TextBox"/> control is enabled or not.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.TextBoxItem"/> class.</returns>
        public static ButtonItem CreateWithText(string name, string label, string text, string cssclass = "tbutton", bool enabled = true)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var linkbutton = new LinkButton { ID = Prefix + name.Replace(" ", ""), Text = text, Enabled = enabled, CssClass = cssclass };

            return new ButtonItem(l, linkbutton);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.TextBox"/> for single-line entry and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.TextBox"/> control.</param>
        /// <param name="enabled">A value indicating whether the <see cref="System.Web.UI.WebControls.TextBox"/> control is enabled or not.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.TextBoxItem"/> class.</returns>
        public static ButtonItem CreateWithoutLabel(string name, string text, string cssclass = "tbutton", bool enabled = true)
        {
            var linkbutton = new LinkButton { ID = Prefix + name.Replace(" ", ""), Text = text, Enabled = enabled, CssClass = cssclass };

            return new ButtonItem(linkbutton);
        }

        /// <summary>
        /// Gets the <see cref="System.Web.UI.WebControls.TextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class.
        /// </summary>
        /// <returns>The <see cref="System.Web.UI.WebControls.TextBox"/> from the items list of <see cref="Adf.Web.UI.BasePanelItem"/> class if it found in items list; otherwise, null.</returns>
        public LinkButton LinkButton
        {
            get { return _linkButton; }
        }

        #region Overrides of BasePanelItem

        public override bool Enabled
        {
            get { return _linkButton.Enabled; }
            set { _linkButton.Enabled = value; }
        }

        #endregion
    }
}
