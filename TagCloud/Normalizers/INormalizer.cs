namespace TagCloud.Normalizers
{
    // Интерфейс нормализации количества каждого слова
    internal interface INormalizer
    {
        public Dictionary<string, double> Normalize(
            Dictionary<string, uint> values,
            double minCoefficient = 0.25,
            int decimalPlaces = 4);
    }
}
