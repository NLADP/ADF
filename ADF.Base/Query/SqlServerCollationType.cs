using System.Globalization;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public class SqlServerCollationType : CollationType
    {
        public bool CaseSensitive { get; protected set; }
        public bool AccentSensitive { get; protected set; }

        public SqlServerCollationType(string name, bool caseSensitive = false, bool accentSensitive = false) : base(name)
        {
            CaseSensitive = caseSensitive;
            AccentSensitive = accentSensitive;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}_C{1}_A{2}", Name, CaseSensitive ? "S" : "I", AccentSensitive ? "S" : "I");
        }

        public static readonly SqlServerCollationType Latin1General = new SqlServerCollationType("Latin1_General");
        public static readonly SqlServerCollationType GermanPhoneBook = new SqlServerCollationType("German_PhoneBook");
    }
}
