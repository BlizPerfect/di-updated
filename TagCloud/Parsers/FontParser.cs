using System.Drawing;

namespace TagCloud.Parsers
{
    internal static class FontParser
    {
        public static string ParseFont(string font)
        {
            if (!FontFamily.Families.Any(
                x => x.Name.Equals(font, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Неизвестный шрифт {font}");
            }
            return font;
        }

    }
}
