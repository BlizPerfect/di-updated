using FluentAssertions;
using System.Drawing;
using TagCloud.CloudLayouterPainters;
using TagCloud.CloudLayouters.CircularCloudLayouter;
using TagCloud.CloudLayouterWorkers;
using TagCloud.ImageSavers;
using TagCloud.Tests.Extensions;

namespace TagCloud.Tests.CloudLayouterTests.CircularCloudLayouterTests
{
    [TestFixture]
    internal class CircularCloudLayouterMainRequirementsTest
    {
        private Point center = new Point();
        private Rectangle[] rectangles;
        private List<Tag> tags;
        private readonly string failedTestsDirectory = "FailedTest";

        private readonly ImageSaver imageSaver = new ImageSaver();
        private readonly CloudLayouterPainter cloudLayouterPainter
            = new CloudLayouterPainter(new Size(5000, 5000));

        [OneTimeSetUp]
        public void Init()
        {
            Directory.CreateDirectory(failedTestsDirectory);
        }

        [SetUp]
        public void SetUp()
        {
            var minRectangleWidth = 30;
            var maxRectangleWidth = 70;
            var minRectangleHeight = 20;
            var maxRectangleHeight = 50;
            var rectanglesCount = 1000;

            tags = new List<Tag>();
            var circularCloudLayouter = new CircularCloudLayouter();

            var randomWorker = new RandomCloudLayouterWorker(
                minRectangleWidth,
                maxRectangleWidth,
                minRectangleHeight,
                maxRectangleHeight);
            foreach (var rectangleProperty in randomWorker
                .GetNextRectangleProperties().Take(rectanglesCount))
            {
                tags.Add(
                    new Tag(
                        rectangleProperty.word,
                        circularCloudLayouter.PutNextRectangle(rectangleProperty.size)));
            }
            rectangles = tags.Select(x => x.Rectangle).ToArray();
        }

        [TestCase(0.7, 1000)]
        [Repeat(10)]
        public void ShouldPlaceRectanglesInCircle(double expectedCoverageRatio, int gridSize)
        {
            var maxRadius = rectangles.Max(
                x => x.GetMaxDistanceFromPointToRectangleAngles(center));
            var step = 2 * maxRadius / gridSize;

            var occupancyGrid = GetOccupancyGrid(gridSize, maxRadius, step);

            var actualCoverageRatio = GetOccupancyGridRatio(occupancyGrid, maxRadius, step);
            actualCoverageRatio.Should().BeGreaterThanOrEqualTo(expectedCoverageRatio);
        }

        [TestCase(15)]
        [Repeat(10)]
        public void ShouldPlaceCenterOfMassOfRectanglesNearCenter(int tolerance)
        {
            var centerX = rectangles.Average(r => r.Left + r.Width / 2.0);
            var centerY = rectangles.Average(r => r.Top + r.Height / 2.0);
            var actualCenter = new Point((int)centerX, (int)centerY);

            var distance = Math.Sqrt(Math.Pow(actualCenter.X - center.X, 2)
                                     + Math.Pow(actualCenter.Y - center.Y, 2));

            distance.Should().BeLessThanOrEqualTo(tolerance);
        }

        [Test]
        [Repeat(10)]
        public void ShouldPlaceRectanglesWithoutOverlap()
        {
            for (var i = 0; i < rectangles.Length; i++)
            {
                for (var j = i + 1; j < rectangles.Length; j++)
                {
                    Assert.That(
                        rectangles[i].IntersectsWith(rectangles[j]) == false,
                        $"Прямоугольники пересекаются:\n" +
                        $"{rectangles[i].ToString()}\n" +
                        $"{rectangles[j].ToString()}");
                }
            }
        }

        [TearDown]
        public void Cleanup()
        {
            if (TestContext.CurrentContext.Result.FailCount == 0)
            {
                return;
            }

            var name = $"{TestContext.CurrentContext.Test.Name}.png";
            var path = Path.Combine(failedTestsDirectory, name);
            imageSaver.SaveFile(cloudLayouterPainter.Draw(tags), path);
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            if (Directory.Exists(failedTestsDirectory)
                && Directory.GetFiles(failedTestsDirectory).Length == 0)
            {
                Directory.Delete(failedTestsDirectory);
            }
        }

        private (int start, int end) GetGridIndexesInterval(
            int rectangleStartValue,
            int rectangleCorrespondingSize,
            double maxRadius,
            double step)
        {
            var start = (int)((rectangleStartValue - center.X + maxRadius) / step);
            var end = (int)((rectangleStartValue
                + rectangleCorrespondingSize - center.X + maxRadius) / step);
            return (start, end);
        }

        private bool[,] GetOccupancyGrid(int gridSize, double maxRadius, double step)
        {
            var result = new bool[gridSize, gridSize];
            foreach (var rect in rectangles)
            {
                var xInterval = GetGridIndexesInterval(rect.X, rect.Width, maxRadius, step);
                var yInterval = GetGridIndexesInterval(rect.Y, rect.Height, maxRadius, step);
                for (var x = xInterval.start; x <= xInterval.end; x++)
                {
                    for (var y = yInterval.start; y <= yInterval.end; y++)
                    {
                        result[x, y] = true;
                    }
                }
            }
            return result;
        }

        private double GetOccupancyGridRatio(bool[,] occupancyGrid, double maxRadius, double step)
        {
            var totalCellsInsideCircle = 0;
            var coveredCellsInsideCircle = 0;
            for (var x = 0; x < occupancyGrid.GetLength(0); x++)
            {
                for (var y = 0; y < occupancyGrid.GetLength(0); y++)
                {
                    var cellCenterX = x * step - maxRadius + center.X;
                    var cellCenterY = y * step - maxRadius + center.Y;

                    var distance = Math.Sqrt(
                        Math.Pow(cellCenterX - center.X, 2) + Math.Pow(cellCenterY - center.Y, 2));

                    if (distance > maxRadius)
                    {
                        continue;
                    }

                    totalCellsInsideCircle += 1;
                    if (occupancyGrid[x, y])
                    {
                        coveredCellsInsideCircle += 1;
                    }
                }
            }
            return (double)coveredCellsInsideCircle / totalCellsInsideCircle;
        }
    }
}
