using CommandLine;

namespace TagCloud
{
    // 1. Нужно добавить опцию для указания имени файла,
    //    который содержит слова, для иссключения из фильтра скучных слов
    // 2. Нужно добавить опцию для указания имени файла,
    //    который содержит слова, для добавления в фильтр скучных слов
    public class CommandLineOptions
    {
        [Option(
            "backgroundColor",
            Required = false,
            HelpText = "Цвет заднего фона изображения, например White.")]
        public string BackgroundColor { get; set; } = "White";

        [Option(
            "textColor",
            Required = false,
            HelpText = "Цвет текста на изображении, например Black.")]
        public string TextColor { get; set; } = "Black";

        [Option(
            "font",
            Required = false,
            HelpText = "Шрифт текста на изображении, например Arial.")]
        public string Font { get; set; } = "Arial";

        [Option(
            "nonSorted",
            Required = false,
            HelpText = "Отключение сортировки слов, например False")]
        public string IsSorted { get; set; } = true.ToString();

        [Option(
            "size",
            Required = false,
            HelpText = "Размер изображения в формате ШИРИНА:ВЫСОТА, например 5000:5000.")]
        public string ImageSize { get; set; } = "5000:5000";

        [Option(
            "maxRectangleWidth",
            Required = false,
            HelpText = "Максимальная ширина прямоугольника.")]
        public int MaxRectangleWidth { get; set; } = 500;

        [Option(
            "maxRectangleHeight",
            Required = false,
            HelpText = "Максимальная высота прямоугольника.")]
        public int MaxRectangleHeight { get; set; } = 200;

        [Option(
            "imageFile",
            Required = false,
            HelpText = "Имя выходного файла изображения.")]
        public string ImageFileName { get; set; } = "Result";

        [Option(
            "dataFile",
            Required = true,
            HelpText = "Имя файла с исходными данными.")]
        public required string DataFileName { get; set; }

        [Option(
            "resultFormat",
            Required = false,
            HelpText = "Формат создаваемого изображение, например png.")]
        public string ResultFormat { get; set; } = "png";
    }

}