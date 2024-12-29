using System.Drawing;

namespace TagCloud.CloudLayouterWorkers
{
    // Класс, со старого задания TagCloud,
    // выдающий случайный размер прямоугольника
    // Оставил его для пары тестов.
    internal class RandomCloudLayouterWorker : ICloudLayouterWorker
    {
        private Random random = new Random();
        public readonly int MinRectangleWidth;
        public readonly int MaxRectangleWidth;
        public readonly int MinRectangleHeight;
        public readonly int MaxRectangleHeight;

        public RandomCloudLayouterWorker(
            int minRectangleWidth,
            int maxRectangleWidth,
            int minRectangleHeight,
            int maxRectangleHeight)
        {
            if (minRectangleWidth <= 0 || maxRectangleWidth <= 0
                || minRectangleHeight <= 0 || maxRectangleHeight <= 0)
            {
                throw new ArgumentException(
                    "Ширина или высота прямоугольника должна быть положительной");
            }

            if (minRectangleWidth > maxRectangleWidth
                || minRectangleHeight > maxRectangleHeight)
            {
                throw new ArgumentException(
                    "Минимальное значение ширины или высоты не может быть больше максимального");
            }

            MinRectangleWidth = minRectangleWidth;
            MaxRectangleWidth = maxRectangleWidth;
            MinRectangleHeight = minRectangleHeight;
            MaxRectangleHeight = maxRectangleHeight;
        }

        public IEnumerable<(string word, Size size)> GetNextRectangleProperties()
        {
            while (true)
            {
                var width = random.Next(MinRectangleWidth, MaxRectangleWidth);
                var height = random.Next(MinRectangleHeight, MaxRectangleHeight);
                yield return (string.Empty, new Size(width, height));
            }
        }
    }
}
