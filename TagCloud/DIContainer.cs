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
using TagCloud.Factories;
using System.Drawing;

namespace TagCloud
{
    public static class DIContainer
    {
        public static IContainer ConfigureContainer(CommandLineOptions options)
        {
            var builder = new ContainerBuilder();

            RegisterSimpleSevice<WordReader, IWordReader>(builder);
            RegisterSimpleSevice<WordFilterFactory>(builder);
            RegisterIWordFillterSevice(builder, options);
            RegisterSimpleSevice<WordCounter, IWordCounter>(builder);
            RegisterSimpleSevice<Normalizer, INormalizer>(builder);
            RegisterSimpleSevice<CircularCloudLayouter, ICloudLayouter>(builder);
            RegisterSimpleSevice<ImageSaver, IImageSaver>(builder);
            RegisterSimpleSevice<CloudLayouterWorkerFactory>(builder);

            var imageSize = SizeParser.ParseImageSize(options.ImageSize);
            RegisterICloudLayouterPainterSevice(builder, options, imageSize);

            RegisterICloudLayouterWorkerSevice(builder, options);

            RegisterProgramExecutorService(builder, options, imageSize);

            return builder.Build();
        }

        private static void RegisterSimpleSevice<TImplementation, TService>(ContainerBuilder builder)
            where TImplementation : TService
            where TService : notnull
        {
            builder
                .RegisterType<TImplementation>()
                .As<TService>()
                .SingleInstance();
        }

        private static void RegisterSimpleSevice<TImplementation>(ContainerBuilder builder)
            where TImplementation : notnull
        {
            builder
                .RegisterType<TImplementation>()
                .AsSelf()
                .SingleInstance();
        }

        private static void RegisterICloudLayouterPainterSevice(
            ContainerBuilder builder,
            CommandLineOptions options,
            Size imageSize)
        {
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
        }

        private static void RegisterICloudLayouterWorkerSevice(
            ContainerBuilder builder,
            CommandLineOptions options)
        {
            builder.Register(c =>
            {
                var factory = c.Resolve<CloudLayouterWorkerFactory>();
                return factory.Create(
                    options.DataFileName,
                    options.MaxRectangleWidth,
                    options.MaxRectangleHeight,
                    BoolParser.ParseIsSorted(options.IsSorted));
            }).As<ICloudLayouterWorker>().SingleInstance();
        }

        private static void RegisterIWordFillterSevice(
            ContainerBuilder builder,
            CommandLineOptions options)
        {
            builder.Register(c =>
            {
                var factory = c.Resolve<WordFilterFactory>();
                return factory.Create(
                    options.WordsToIncludeFileName,
                    options.WordsToExcludeFileName,
                    c.Resolve<IWordReader>());
            }).As<IWordFilter>().SingleInstance();
        }

        private static void RegisterProgramExecutorService(
            ContainerBuilder builder,
            CommandLineOptions options,
            Size imageSize)
        {
            builder.RegisterType<ProgramExecutor>()
                .WithParameter("size", imageSize)
                .WithParameter("resultFormat", options.ResultFormat)
                .WithParameter("maxRectangleWidth", options.MaxRectangleWidth)
                .WithParameter("maxRectangleHeight", options.MaxRectangleHeight)
                .WithParameter("imageFileName", options.ImageFileName)
                .WithParameter("dataFileName", options.DataFileName)
                .SingleInstance();
        }
    }
}
