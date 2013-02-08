using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Adf.Core.Extensions;

namespace Adf.Core
{
    /// <summary>
    /// Represents named hashtable of instances of the class itself.
    /// Provides methods to check the equality or non-equality of two descriptors, to get the descriptors
    /// of a specified <see cref="System.Type"/> etc.
    /// </summary>
    [Serializable]
    public class Descriptor : IEquatable<Descriptor>
    {
        public static readonly bool Default = true;

        public Descriptor(string name, bool isDefault = false, int order = 0)
        {
            _name = name;
            _isDefault = isDefault;
            _order = order;
        }

        [Exclude]
        private static readonly Dictionary<Type, IEnumerable<Descriptor>> Values = new Dictionary<Type, IEnumerable<Descriptor>>();

        private readonly string _name;

        /// <returns>
        /// The name of the <see cref="Descriptor"/>.
        /// </returns>
        [Exclude]
        public string Name
        {
            get { return _name; }
        }

        private readonly bool _isDefault;

        /// <returns>
        /// Is set to true if this particular value is the default for <see cref="Descriptor"/>.
        /// </returns>
        [Exclude]
        public bool IsDefault
        {
            get { return _isDefault; }
        }

        [Exclude]
        public bool IsEmpty
        {
            get { return Name.IsNullOrEmpty() || this == Empty || this == Null; }
        }

        private readonly int _order;

        ///<summary>
        /// Entries in <see cref="Descriptor"/> might have an (optinal) order associated.
        ///</summary>
        [Exclude]
        public int Order
        {
            get { return _order; }
        }

        ///<summary>
        /// Empty <see cref="Descriptor"/>.
        ///</summary>
        [Exclude]
        public static readonly Descriptor Empty = new Descriptor("Empty");

        ///<summary>
        /// Empty <see cref="Descriptor"/>.
        ///</summary>
        [Exclude]
        public static readonly Descriptor Null = new Descriptor("Null");

        /// <summary>
        /// Returns the name of the <see cref="Descriptor"/>.
        /// </summary>
        /// <returns>
        /// The name of the <see cref="Descriptor"/>.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        #region Values

        /// <summary>
        /// Returns the <see cref="Descriptor"/> with the specified name for the specified 
        /// <see cref="System.Type"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Descriptor"/>.</param>
        /// <returns>
        /// If a <see cref="Descriptor"/> with the specified name is found for the specified 
        /// <see cref="System.Type"/>, the descriptor is returned; otherwise an empty 
        /// <see cref="Descriptor"/> is returned.
        /// </returns>
        public static T Get<T>(string name) where T : Descriptor
        {
            return GetValues<T>().Where(descriptor => descriptor.Name == name).FirstOrDefault();
        }

        public static Descriptor Get(Type type, string name)
        {
            return GetValues(type).FirstOrDefault(descriptor => descriptor.Name == name);
        }

        public static T Parse<T>(string name) where T : Descriptor
        {
            return GetValues<T>().Where(descriptor => name.Equals(descriptor.Name, StringComparison.OrdinalIgnoreCase)).Single();
        }

        public static bool TryParse<T>(string name, out T value) where T : Descriptor
        {
            value = GetValues<T>().Where(descriptor => name.Equals(descriptor.Name, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

            return value != null;
        }

        public static T GetDefault<T>() where T : Descriptor
        {
            return GetValues<T>().Where(d => d.IsDefault).SingleOrDefault();
        }

        public static IEnumerable<T> GetValues<T>() where T : Descriptor
        {
            return GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Returns an array of <see cref="Descriptor"/>s for the specified <see cref="System.Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> for which the <see cref="Descriptor"/>s are required.</param>
        /// <returns>
        /// An array of <see cref="Descriptor"/>s for the specified <see cref="System.Type"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// type is null.
        /// </exception>
        /// <exception cref="System.NullReferenceException">
        /// Object reference not set to an instance of an object.
        /// </exception>
        public static IEnumerable<Descriptor> GetValues(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (Values.ContainsKey(type)) return Values[type];

            Type descriptorType = typeof(Descriptor);

            return (Values[type] =
                    (from fi in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
                     where !fi.IsExcluded() && descriptorType.IsAssignableFrom(fi.FieldType)
                     select (Descriptor)fi.GetValue(type)));
        }


        #endregion

        #region Operators

        public virtual bool ValueEquals(Descriptor other) 
        {
            return (Name == other.Name);
        }

        /// <summary>
        /// Returns a value indicating whether the two specified <seealso cref="Descriptor"/>s have 
        /// the same name.
        /// </summary>
        /// <param name="x">The first <see cref="Descriptor"/>.</param>
        /// <param name="y">The second <see cref="Descriptor"/>.</param>
        /// <returns>
        /// true if the <see cref="Descriptor"/>s have the same name; otherwise, false.
        /// </returns>
        public static bool operator ==(Descriptor x, Descriptor y)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(x, y))
                return true;

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
                return false;

            return (x.ValueEquals(y));
        }

        /// <summary>
        /// Returns a value indicating whether the specified <seealso cref="Descriptor"/>s do not have 
        /// the same name.
        /// </summary>
        /// <param name="x">The first <see cref="Descriptor"/>.</param>
        /// <param name="y">The second <see cref="Descriptor"/>.</param>
        /// <returns>
        /// true if the specified <see cref="Descriptor"/>s do not have the same name; 
        /// otherwise, false.
        /// </returns>
        public static bool operator !=(Descriptor x, Descriptor y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Returns a value indicating whether the specified <seealso cref="Descriptor"/> object has 
        /// the same name as that of the current one.
        /// </summary>
        /// <param name="obj">The <see cref="Descriptor"/> to check.</param>
        /// <returns>
        /// true if the specified <see cref="Descriptor"/> has the same name as that of 
        /// the current one; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Descriptor);
        }

        public bool Equals(Descriptor other)
        {
            if (other == null) return false;

            return this == other;
        }


        /// <summary>
        /// Returns the hash code of Name.
        /// </summary>
        /// <returns>
        /// The hash code of Name.
        /// </returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        #endregion
    }
}
