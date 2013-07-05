namespace Adf.Core
{
    public static class Config
    {
        public static class Data
        {
            public const string UnderlyingDataChanged = "Adf.Data.UnderlyingDataChanged";
            public const string ForeignKeyConstraintsViolated = "Adf.Data.ForeignKeyConstraintsViolated";
            public const string Timeout = "Adf.Data.Timeout";
            public const string NotASqlAdatper = "Not a SqlDataAdapter";
        }

        public static class Domain
        {
            public const string AttributeNonEmptyInvalid = "Adf.Business.AttributeNonEmptyInvalid";
            public const string AttributeNotInPastInvalid = "Adf.Business.AttributeNotInPastInvalid";
            public const string AttributeRegexInvalid = "Adf.Business.AttributeRegexInvalid";
            public const string AttributeMinLengthInvalid = "Adf.Business.AttributeMinLengthInvalid";
            public const string AttributeMaxLengthInvalid = "Adf.Business.AttributeMaxLengthInvalid";
            public const string AttributeInRangeInvalid = "Adf.Business.AttributeInRangeInvalid";
            public const string AttributeInRangeInvalidRange = "Adf.Business.AttributeInRangeInvalidRange";
            public const string NotInstantiable = "Adf.Business.NotInstantiable";
        }
    }
}
