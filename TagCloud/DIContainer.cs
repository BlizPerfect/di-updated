using Autofac;
using TagCloud.CloudLayouterPainters;
using TagCloud.CloudLayouters.CircularCloudLayouter;
using TagCloud.CloudLayouters;
using TagCloud.CloudLayouterWorkers;
using TagCloud.ImageSavers;
using TagCloud.Normalizers;
using TagCloud.WordCounters;
using TagCloud.WordFilters;
using TagCloud.WordReaders;
using TagCloud.Parsers;

namespace TagCloud
{
    public static class DIContainer
    {
        // 1. Разбить содержимое этого метода на отдельны части
        // 2. Добавить проверку корректностей значений:
        //    - options.ImageSize;
        //    - options.MaxRectangleWidth;
        //    - options.MaxRectangleHeight;
        public static IContainer ConfigureContainer(CommandLineOptions options)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<WordReader>()
                .As<IWordReader>()
                .SingleInstance();

            builder
                .RegisterType<WordFilter>()
                .As<IWordFilter>()
                .SingleInstance();

            builder
                .RegisterType<WordCounter>()
                .As<IWordCounter>()
                .SingleInstance();

            builder
                .RegisterType<Normalizer>()
                .As<INormalizer>()
                .SingleInstance();

            builder
                .RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .SingleInstance();

            builder
                .RegisterType<ImageSaver>()
                .As<IImageSaver>()
                .SingleInstance();

            var imageSize = SizeParser.ParseImageSize(options.ImageSize);
            var backgroundColor = ColorParser.ParseColor(options.BackgroundColor);
            var textColor = ColorParser.ParseColor(options.TextColor);
            var font = FontParser.ParseFont(options.Font);
            builder.RegisterType<CloudLayouterPainter>()
                .As<ICloudLayouterPainter>()
                .WithParameter("imageSize", imageSize)
                .WithParameter("backgroundColor", backgroundColor)
                .WithParameter("textColor", textColor)
                .WithParameter("fontName", font)
                .SingleInstance();

            var isSorted = BoolParser.ParseIsSorted(options.IsSorted);
            builder.Register((c, p) =>
            {
                var wordReader = c.Resolve<IWordReader>();
                var wordCounter = c.Resolve<IWordCounter>();
                var normalizer = c.Resolve<INormalizer>();
                var wordFilter = c.Resolve<IWordFilter>();

                foreach (var word in wordReader.ReadByLines(options.DataFileName))
                {
                    var wordInLowerCase = word.ToLower();
                    if (!wordFilter.IsCorrectWord(wordInLowerCase))
                    {
                        continue;
                    }
                    wordCounter.AddWord(wordInLowerCase);
                }

                var normalizedValues = normalizer.Normalize(wordCounter.Values);
                return new NormalizedFrequencyBasedCloudLayouterWorker(
                    options.MaxRectangleWidth,
                    options.MaxRectangleHeight,
                    normalizedValues,
                    isSorted);
            }).As<ICloudLayouterWorker>().SingleInstance();

            builder.RegisterType<ProgramExecutor>()
                .WithParameter("size", imageSize)
                .WithParameter("maxRectangleWidth", options.MaxRectangleWidth)
                .WithParameter("maxRectangleHeight", options.MaxRectangleHeight)
                .WithParameter("imageFileName", options.ImageFileName)
                .WithParameter("dataFileName", options.DataFileName)
                .SingleInstance();

            return builder.Build();
        }
    }
}
