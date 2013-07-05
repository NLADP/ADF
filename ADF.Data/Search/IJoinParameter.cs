namespace Adf.Data.Search
{
    public interface IJoinParameter
    {
        string NativeTable { get; }

        string NativeColumn { get; }

        string ForeignTable { get; }

        string ForeignColumn { get; }
    }
}