namespace TagCloud.WordCounters
{
    // Интерфейс подсчёта количества каждого уникального слова
    internal interface IWordCounter
    {
        public void AddWord(string word);
        public Dictionary<string, uint> Values { get; }
    }
}
