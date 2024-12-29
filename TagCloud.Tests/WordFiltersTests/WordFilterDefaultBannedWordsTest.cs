using FluentAssertions;
using TagCloud.WordFilters;

namespace TagCloud.Tests.WordFiltersTests
{
    [TestFixture]
    internal class WordFilterDefaultBannedWordsTest
    {
        private readonly WordFilter wordFilter = new WordFilter();

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.CustomBans))]
        public void IsCorrectWord_ShouldBeFalse_WithCustomBans(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.ToHaveForms))]
        public void IsCorrectWord_ShouldBeFalse_WithToHaveForms(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.ToBeForms))]
        public void IsCorrectWord_ShouldBeFalse_WithToBeForms(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.Articles))]
        public void IsCorrectWord_ShouldBeFalse_WithArticles(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.Pronouns))]
        public void IsCorrectWord_ShouldBeFalse_WithPronouns(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.Prepositions))]
        public void IsCorrectWord_ShouldBeFalse_WithPrepositions(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.Conjunctions))]
        public void IsCorrectWord_ShouldBeFalse_WithConjunctions(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }

        [TestCaseSource(typeof(BannedWordLists), nameof(BannedWordLists.Interjections))]
        public void IsCorrectWord_ShouldBeFalse_WithInterjections(string word)
        {
            wordFilter.IsCorrectWord(word).Should().BeFalse();
        }
    }
}
