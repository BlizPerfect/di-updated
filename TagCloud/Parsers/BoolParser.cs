namespace TagCloud.Parsers
{
    internal static class BoolParser
    {
        public static bool ParseIsSorted(string value)
        {
            if (value == false.ToString() || value == true.ToString())
            {
                return value == true.ToString();
            }
            throw new ArgumentException($"Неизвестный параметр сортировки {value}");
        }
    }
}
