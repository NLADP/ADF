using System;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Attribute to determine a description to the empty select item. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class EmptySelectItemAttribute : Attribute
    {
        public EmptySelectItemAttribute()
        {
            EmptySelectItem = null;
        }

        public EmptySelectItemAttribute(string emptySelectItem)
        {
            EmptySelectItem = emptySelectItem;
        }

        public string EmptySelectItem { get; private set; }
    }
}

