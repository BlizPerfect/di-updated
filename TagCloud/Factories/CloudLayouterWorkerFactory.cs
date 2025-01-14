using TagCloud.CloudLayouterWorkers;
using TagCloud.Normalizers;
using TagCloud.WordCounters;
using TagCloud.WordFilters;
using TagCloud.WordReaders;

namespace TagCloud.Factories
{
    internal class CloudLayouterWorkerFactory(
        IWordReader wordReader,
            IWordCounter wordCounter,
            INormalizer normalizer,
            IWordFilter wordFilter)
    {
        public ICloudLayouterWorker Create(
            string dataFileName,
            int maxRectangleWidth,
            int maxRectangleHeight,
            bool isSorted)
        {
            foreach (var word in wordReader.ReadByLines(dataFileName))
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
                maxRectangleWidth,
                maxRectangleHeight,
                normalizedValues,
                isSorted);
        }
    }
}
