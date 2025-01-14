namespace TagCloud.Parsers
{
    internal static class BoolParser
    {
        public static bool ParseIsSorted(string value)
        {
            if (value == bool.FalseString || value == bool.TrueString)
            {
                return Convert.ToBoolean(value);
            }
            throw new ArgumentException($"Неизвестный параметр сортировки {value}");
        }
    }
}
