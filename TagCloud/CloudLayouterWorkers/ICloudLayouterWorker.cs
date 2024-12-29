using System.Drawing;

namespace TagCloud.CloudLayouterWorkers
{
    // Интерфейс получения свойств следующего прямоугольника
    // По хорошему, нужно возвращать IEnumerable<Size>,
    // для повышения возможности переиспользования
    internal interface ICloudLayouterWorker
    {
        public IEnumerable<(string word, Size size)> GetNextRectangleProperties();
    }
}
