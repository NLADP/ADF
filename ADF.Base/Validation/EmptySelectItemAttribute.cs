using System;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Attribute to determine a description to the empty select item. 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class EmptySelectItemAttribute : Attribute
    {
        private readonly string emptySelectItem;

        public EmptySelectItemAttribute()
        {
            emptySelectItem = null;
        }

        public EmptySelectItemAttribute(string emptySelectItem)
        {
            this.emptySelectItem = emptySelectItem;
        }

        public string EmptySelectItem
        {
            get { return emptySelectItem; }
        }
    }
}

