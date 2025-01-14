using Autofac;
using FluentAssertions;
namespace TagCloud.Tests
{
    [TestFixture]
    internal class MainTest
    {
        private static string directoryPath = "TempFilesForMainTest";
        private static readonly string dataFile = Path.Combine(directoryPath, "TestData.txt");
        private readonly string imageFile = Path.Combine(directoryPath, "Test");

        [OneTimeSetUp]
        public void Init()
        {
            Directory.CreateDirectory(directoryPath);
            File.WriteAllLines(dataFile, new string[]
            {
                "One",
                "One",
                "Two",
                "Three",
                "Four",
                "Four",
                "Four",
                "Four"
            });
        }

        [TestCase("bmp")]
        public void Program_ExecutesSuccessfully_WithValidArguments(string format)
        {
            var options = new CommandLineOptions
            {
                BackgroundColor = "Red",
                TextColor = "Blue",
                Font = "Calibri",
                IsSorted = true.ToString(),
                ImageSize = "1000:1000",
                MaxRectangleHeight = 100,
                MaxRectangleWidth = 200,
                ImageFileName = imageFile,
                DataFileName = dataFile,
                ResultFormat = format
            };

            var container = DIContainer.ConfigureContainer(options);
            using var scope = container.BeginLifetimeScope();
            var executor = scope.Resolve<ProgramExecutor>();

            Assert.DoesNotThrow(() => executor.Execute());

            File.Exists($"{imageFile}.{format}").Should().BeTrue();
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
    }
}
