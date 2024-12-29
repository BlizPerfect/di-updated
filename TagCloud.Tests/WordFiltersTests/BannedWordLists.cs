namespace TagCloud.Tests.WordFiltersTests
{
    internal static class BannedWordLists
    {
        public static string[] CustomBans = new string[]
        {
            "not", "also", "how", "let"
        };

        public static string[] ToHaveForms = new string[]
        {
            "have", "has", "had", "having",
        };

        public static string[] ToBeForms = new string[]
        {
            "am", "is", "are", "was", "were", "be", "been", "being",
        };

        public static string[] Articles = new string[]
        {
            "a", "an", "the"
        };

        public static string[] Pronouns => new string[]
        {
            "i", "you", "he", "she", "it", "we", "they", "me", "him",
            "her", "us", "them", "my", "your", "his", "its", "our", "their",
            "mine", "yours", "hers", "theirs", "myself", "yourself", "himself",
            "herself", "itself", "ourselves", "yourselves", "themselves", "this",
            "that", "these", "those", "who", "whom", "whose", "what", "which",
            "some", "any", "none", "all", "many", "few", "several",
            "everyone", "somebody", "anybody", "nobody", "everything", "anything",
            "nothing", "each", "every", "either", "neither"
        };

        public static string[] Prepositions => new string[]
        {
            "about", "above", "across", "after", "against", "along", "amid", "among",
            "around", "as", "at", "before", "behind", "below", "beneath", "beside",
            "besides", "between", "beyond", "but", "by", "despite", "down", "during",
            "except", "for", "from", "in", "inside", "into", "like", "near", "of", "off",
            "on", "onto", "out", "outside", "over", "past", "since", "through", "throughout",
            "till", "to", "toward", "under", "underneath", "until", "up", "upon", "with",
            "within", "without"
        };

        public static string[] Conjunctions => new string[]
        {
            "and", "but", "or", "nor", "for", "yet", "so", "if", "because", "although", "though",
            "since", "until", "unless", "while", "whereas", "when", "where", "before", "after"
        };

        public static string[] Interjections => new string[]
        {
            "o", "ah", "aha", "alas", "aw", "aye", "eh", "hmm", "huh", "hurrah", "no", "oh", "oops",
            "ouch", "ow", "phew", "shh", "tsk", "ugh", "um", "wow", "yay", "yes", "yikes"
        };
    }
}
