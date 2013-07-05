namespace Adf.Core.Domain
{
    /// <summary>
    /// Represents the references used for Desriptor References. A descriptor is a type that acts like an enum,
    /// but unlike an enum, it can be specialised.    
    /// </summary>
    public class References : Descriptor
    {
        public References(string name = null) : base(name)
        {
        }
    }
}