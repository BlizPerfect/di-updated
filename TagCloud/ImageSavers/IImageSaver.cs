using System.Drawing;

namespace TagCloud.ImageSavers
{
    // Интерфейс сохранения изображения в файл
    internal interface IImageSaver
    {
        public void SaveFile(Bitmap image, string fileName, string format = "png");
    }
}
