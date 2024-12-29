using FluentAssertions;
using TagCloud.Normalizers;

namespace TagCloud.Tests.WordCountersTests
{
    [TestFixture]
    internal class NormalizerTest
    {
        private readonly Normalizer normalizer = new Normalizer();
        private readonly Dictionary<string, uint> values = new Dictionary<string, uint>
        {
            { "one", 14 },
            { "two", 15 },
            { "three", 23 },
            { "four", 32 },
        };
        private readonly Dictionary<string, double> expectedResult = new Dictionary<string, double>
        {
            { "one", 0.25 },
            { "two",0.29166666666666669 },
            { "three", 0.625 },
            { "four", 1.0 },

        };
        private readonly int defaultDecimalPlaces = 4;
        private readonly double defaultMinCoefficient = 0.25;

        [TestCase(-0.1)]
        public void Normalize_ThrowsArgumentException_WithMinCoefficientLessThanZero(
            double minCoefficient)
        {
            Assert.Throws<ArgumentException>(()
                => normalizer.Normalize(values, minCoefficient, defaultDecimalPlaces));
        }

        [TestCase(0.25, 4)]
        [TestCase(0.25, 2)]
        public void Normalize_CalculatesСorrectly(double minCoefficient, int decimalPlaces)
        {
            foreach (var pair in expectedResult)
            {
                expectedResult[pair.Key] = Math.Round(pair.Value, decimalPlaces);
            }
            var actual = normalizer.Normalize(values, minCoefficient, decimalPlaces);

            actual.Should().BeEquivalentTo(expectedResult);
        }
    }
}
