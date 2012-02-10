using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a label control, which displays text on a panel.
    /// </summary>
    public class ImageItem : BasePanelItem
    {
        private const string prefix = "img";

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.LabelItem"/> class with the specified label and checkbox.
        /// </summary>
        /// <param name="label">The <see cref="System.Web.UI.WebControls.Label"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="image">The <see cref="System.Web.UI.WebControls.Image"/> that defines the control which will be added into <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        public ImageItem(Label label, Image image)
        {
            this._labelControls.Add(label);

            _itemControls.Add(image);
        }

        /// <summary>
        /// Create a <see cref="System.Web.UI.WebControls.Label"/> and add it into <see cref="Adf.Web.UI.BasePanelItem"/>.
        /// </summary>
        /// <param name="label">The display text of the checkbox within <see cref="Adf.Web.UI.BasePanelItem"/>.</param>
        /// <param name="name">Set the identification of <see cref="System.Web.UI.WebControls.Label"/> control.</param>
        /// <param name="width">Width of <see cref="System.Web.UI.WebControls.Label"/> control.</param>
        /// <returns>A new instance of the <see cref="Adf.Web.UI.LabelItem"/> class.</returns>
        public static ImageItem Create(string label, string name, string url = "", int width = 16, int height = 16)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var image = new Image
                            {
                                ID = prefix + name,
                                Enabled = false,
                                ImageUrl = url,
                                Width = new Unit(width, UnitType.Pixel),
                                Height = new Unit(height, UnitType.Pixel)
                            };

            return new ImageItem(l, image);
        }

        public static ImageItem Create<T>(Expression<Func<T, object>> property, string url = "", int width = 16, int height = 16)
        {
            return Create(property.GetExpressionMember().Name, property, url, width, height);
        }

        public static ImageItem Create<T>(string label, Expression<Func<T, object>> property, string url = "", int width = 16, int height = 16)
        {
            return Create(label, property.GetControlName(), url, width, height);
        }

        public Image Image
        {
            get { return (_itemControls.Count > 0) ? _itemControls[0] as Image : null; }
        }

        #region Overrides of BasePanelItem

        public override bool Enabled { get; set; }

        #endregion
    }
}