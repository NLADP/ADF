using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Web.UI.Styling;

namespace Adf.Web.UI
{
	/// <summary>
    /// Represents a customized <see cref="Calendar"/> control.
	/// </summary>
	public class BusinessCalendar : Calendar, INamingContainer
	{

		#region Styles

		private static IStyler styler = NullStyler.Empty;
		private bool autostyle = true;

        /// <summary>
        /// Gets or sets a style of type <see cref="IStyler"/>
        /// </summary>
        /// <returns>Returns a style of type <see cref="IStyler"/>.</returns>
		public static IStyler Styler
		{
			get { return styler; }
			set { styler = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the styles are automatically applied on load.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if autostyling is turned on; otherwise, <c>false</c>.
        /// </returns>
		[Bindable(true), Category("BusinessCalendar"), DefaultValue(true)]
		public bool AutoStyle
		{
			get {return autostyle;}
			set {autostyle = value;}
		}

		#endregion

		/// <summary>
		/// Set the styles when autostyle is used.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnLoad(EventArgs e)
		{
			if (AutoStyle) Styler.SetStyles(this);
		}
	}
}
