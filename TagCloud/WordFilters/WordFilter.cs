namespace TagCloud.WordFilters
{
    // Реализован пункт на перспективу:
    // Предобработка слов.
    // Дать возможность влиять на список скучных слов, которые не попадут в облако.
    internal class WordFilter : IWordFilter
    {
        private readonly HashSet<string> bannedWords = new HashSet<string>()
        {
            // Просто скучные по моему мнению
            "not", "also", "how", "let",
            // To have
            "have", "has", "had", "having",
            // To be
            "am", "is", "are", "was", "were", "be", "been", "being",
            // Артикли
            "a", "an", "the",
            // Местоимения
            "i", "you", "he", "she", "it", "we", "they", "me", "him",
            "her", "us", "them", "my", "your", "his", "its", "our", "their",
            "mine", "yours", "hers", "theirs", "myself", "yourself", "himself",
            "herself", "itself", "ourselves", "yourselves", "themselves", "this",
            "that", "these", "those", "who", "whom", "whose", "what", "which",
            "some", "any", "none", "all", "many", "few", "several",
            "everyone", "somebody", "anybody", "nobody", "everything", "anything",
            "nothing", "each", "every", "either", "neither",
            // Предлоги
            "about", "above", "across", "after", "against", "along", "amid", "among",
            "around", "as", "at", "before", "behind", "below", "beneath", "beside",
            "besides", "between", "beyond", "but", "by", "despite", "down", "during",
            "except", "for", "from", "in", "inside", "into", "like", "near", "of", "off",
            "on", "onto", "out", "outside", "over", "past", "since", "through", "throughout",
            "till", "to", "toward", "under", "underneath", "until", "up", "upon", "with",
            "within", "without",
            // Союзы
            "and", "but", "or", "nor", "for", "yet", "so", "if", "because", "although", "though",
            "since", "until", "unless", "while", "whereas", "when", "where", "before", "after",
            // Междометия
            "o","ah", "aha", "alas", "aw", "aye", "eh", "hmm", "huh", "hurrah", "no", "oh", "oops",
            "ouch", "ow", "phew", "shh", "tsk", "ugh", "um", "wow", "yay", "yes", "yikes"
        };

        public WordFilter(IList<string>? toAdd = null, IList<string>? toExclude = null)
        {
            if (toAdd is not null)
            {
                foreach (var word in toAdd)
                {
                    Add(word);
                }
            }

            if (toExclude is not null)
            {
                foreach (var word in toExclude)
                {
                    Remove(word);
                }
            }
        }

        public bool Add(string word) => bannedWords.Add(word);

        public bool Remove(string word) => bannedWords.Remove(word);

        public void Clear() => bannedWords.Clear();

        public HashSet<string> BannedWords => bannedWords;

        public bool IsCorrectWord(string word)
        {
            return !bannedWords.Contains(word);
        }
    }
}
