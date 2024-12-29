using FluentAssertions;
using TagCloud.WordCounters;

namespace TagCloud.Tests.WordCountersTests
{
    [TestFixture]
    internal class WordCounterTest
    {
        private WordCounter wordCounter;

        [SetUp]
        public void SetUp()
        {
            wordCounter = new WordCounter();
        }

        [Test]
        public void WordCounter_CountsCorrect()
        {
            var expected = new Dictionary<string, uint>()
            {
                { "One", 2 },
                { "Two", 1 },
                { "Three", 1 },
                { "Four", 4 },
            };
            var values = new string[]
            {
                "One",
                "One",
                "Two",
                "Three",
                "Four",
                "Four",
                "Four",
                "Four"
            };

            foreach (var value in values)
            {
                wordCounter.AddWord(value);
            }
            wordCounter.Values.Should().BeEquivalentTo(expected);
        }
    }
}
