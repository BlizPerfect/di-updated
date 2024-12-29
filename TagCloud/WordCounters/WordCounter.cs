namespace TagCloud.WordCounters
{
    internal class WordCounter : IWordCounter
    {
        private readonly Dictionary<string, uint> counts = new Dictionary<string, uint>();
        public Dictionary<string, uint> Values => counts;

        public void AddWord(string word)
        {
            if (!counts.ContainsKey(word))
            {
                counts[word] = 1;
                return;
            }
            counts[word] += 1;
        }
    }
}
