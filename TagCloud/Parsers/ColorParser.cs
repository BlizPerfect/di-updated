using System.Drawing;

namespace TagCloud.Parsers
{
    internal static class ColorParser
    {
        public static Color ParseColor(string color)
        {
            var result = Color.FromName(color);
            if (!result.IsKnownColor)
            {
                throw new ArgumentException($"Неизвестный цвет {color}");
            }
            return result;
        }
    }
}
