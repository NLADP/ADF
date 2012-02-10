namespace Adf.Core.Domain
{
	/// <summary>
	/// Represents the internal status of an object.
    /// Provides properties and methods to get or set different types to internal status, change 
    /// internal status etc.
	/// </summary>
	public class InternalStatus : Descriptor
	{
        ///<summary>
        ///  Undefined status, this is the initial status
        ///</summary>
        public static readonly InternalStatus Undefined = new InternalStatus("Undefined", 0);
        ///<summary>
        /// New, domain object is created but not yet modified
        ///</summary>
        public static readonly InternalStatus New = new InternalStatus("New", 10);
        ///<summary>
        /// Newly created object which is modified
        ///</summary>
        public static readonly InternalStatus NewChanged = new InternalStatus("NewChanged", 20);
        ///<summary>
        /// Exisiting object which is modified
        ///</summary>
        public static readonly InternalStatus Changed = new InternalStatus("Changed", 30);
        ///<summary>
        /// Newly created object which has been removed. Does not yet exist in the underlying data store!
        ///</summary>
        public static readonly InternalStatus NewRemoved = new InternalStatus("NewRemoved", 40);
        ///<summary>
        /// Existing object which needs to be removed
        ///</summary>
        public static readonly InternalStatus Removed = new InternalStatus("Removed", 50);
        ///<summary>
        /// Existing object, unmodified
        ///</summary>
        public static readonly InternalStatus Ok = new InternalStatus("Ok", 60);

	    public InternalStatus(string name, int order) : base(name, order: order)
	    {
	    }

	    /// <summary>
        /// Gets a value indicating whether the <see cref="InternalStatus"/> is altered.
        /// </summary>
        /// <returns>
        /// true if the name of the <see cref="InternalStatus"/> is 'New' or 'NewChanged' 
        /// or 'Changed'; otherwise, false.
        /// </returns>
		public bool IsAltered
		{
			get { return (this == New || this == NewChanged || this == Changed); }
		}

        public bool IsNew
        {
            get { return (this == New || this == NewChanged); }
        }
	}
}
