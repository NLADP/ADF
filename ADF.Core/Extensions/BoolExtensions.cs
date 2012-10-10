namespace Adf.Core.Extensions
{
    public static class BoolExtensions
    {
        public static bool Value(this bool? boolean)
        {
            return boolean.GetValueOrDefault(false);
        }
    }
}
