namespace Adf.Core.Data
{
    public interface IColumn
    {
        string Attribute { get; }
        ITable Table { get; }
        string ColumnName { get; }

        bool IsIdentity { get; }
        bool IsAutoIncrement { get; }
        bool IsTimestamp { get; }
    }
}
