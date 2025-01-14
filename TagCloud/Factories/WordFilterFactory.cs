using TagCloud.WordFilters;
using TagCloud.WordReaders;

namespace TagCloud.Factories
{
    internal class WordFilterFactory
    {
        public IWordFilter Create(
            string wordsToIncludeFileName,
            string wordsToExcludeFileName,
            IWordReader wordReader)
        {
            var result = new WordFilter();

            if (IsFileNameCorrect(wordsToIncludeFileName))
            {
                AddWords(wordReader, wordsToIncludeFileName, result);
            }

            if (IsFileNameCorrect(wordsToExcludeFileName))
            {
                RemoveWords(wordReader, wordsToExcludeFileName, result);
            }

            return result;
        }

        private bool IsFileNameCorrect(string fileName)
            => !string.IsNullOrEmpty(fileName) && !string.IsNullOrWhiteSpace(fileName);

        private void AddWords(
            IWordReader wordReader,
            string wordsToIncludeFileName,
            WordFilter wordFilter)
        {
            foreach (var word in wordReader.ReadByLines(wordsToIncludeFileName))
            {
                wordFilter.Add(word.ToLower());
            }
        }

        private void RemoveWords(
            IWordReader wordReader,
            string wordsToExcludeFileName,
            WordFilter wordFilter)
        {
            foreach (var word in wordReader.ReadByLines(wordsToExcludeFileName))
            {
                wordFilter.Remove(word.ToLower());
            }
        }
    }
}
