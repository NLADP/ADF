namespace Adf.Core.Styling
{
	/// <summary>
    /// Defines a method that a class implements to set CSS styles to a Control.
	/// </summary>
	public interface IStyler
	{
        /// <summary>
        /// Sets the CSS styles to the specified Control.
        /// </summary>
        /// <param name="target">The object to give CSS styles to. Usually a control, or a class styling other controls.</param>
		void Style(object target);
	}
}
