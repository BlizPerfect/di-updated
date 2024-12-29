using FluentAssertions;
using TagCloud.WordFilters;

namespace TagCloud.Tests.WordFiltersTests
{
    [TestFixture]
    internal class WordFilterChangeBannedWordsTest
    {
        private WordFilter wordFilter;

        [SetUp]
        public void SetUp()
        {
            wordFilter = new WordFilter();
        }

        [Test]
        public void Clear_ShouldClearBannedWordList()
        {
            wordFilter.Clear();
            wordFilter.BannedWords.Should().BeEmpty();
        }

        [TestCase("WordToAdd")]
        public void Add_ShouldAddWord_ToBannedWords(string word)
        {
            wordFilter.Clear();
            wordFilter.Add(word);
            wordFilter.BannedWords.Should().Contain(word).And.HaveCount(1);
        }

        [TestCase("WordToRemove")]
        public void Remove_ShouldRemoveWord_InBannedWords(string word)
        {
            wordFilter.Clear();
            wordFilter.Add(word);
            wordFilter.Remove(word);
            wordFilter.BannedWords.Should().NotContain(word);
        }
    }
}
