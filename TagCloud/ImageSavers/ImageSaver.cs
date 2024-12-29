using System.Drawing;

namespace TagCloud.ImageSavers
{
    // Реализован пункт на перспективу:
    // Формат результата.
    // Поддерживать разные форматы изображений.
    internal class ImageSaver : IImageSaver
    {
        public void SaveFile(Bitmap image, string fileName, string format = "png")
        {
            if (image is null)
            {
                throw new ArgumentNullException("Передаваемое изображение не должно быть null");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Некорректное имя файла для создания");
            }

            image.Save($"{fileName}.{format}");
        }
    }
}
