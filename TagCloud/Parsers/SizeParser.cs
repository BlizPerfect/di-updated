using System.Drawing;

namespace TagCloud.Parsers
{
    internal static class SizeParser
    {
        public static Size ParseImageSize(string size)
        {
            var dimensions = size.Split(':');
            if (dimensions.Length != 2
                || !int.TryParse(dimensions[0], out var width)
                || !int.TryParse(dimensions[1], out var height))
            {
                throw new ArgumentException(
                    $"Некорректный формат размера изображения: {size}." +
                    $" Используйте формат Ширина:Высота, например 5000:5000.");
            }
            return new Size(width, height);
        }
    }
}
