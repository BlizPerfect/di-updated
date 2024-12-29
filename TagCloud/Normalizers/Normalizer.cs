namespace TagCloud.Normalizers
{
    // Слово, которое встречается чаще всего, будет иметь вес 1.0.
    // Это означает, что оно в дальнейшем будет иметь прямоугольник
    // с максимальным размером.
    // Слово с минимальной частотой будет иметь
    // minCoefficient *  максимальный размеро прямоугольника.
    internal class Normalizer : INormalizer
    {
        public Dictionary<string, double> Normalize(
            Dictionary<string, uint> values,
            double minCoefficient = 0.25,
            int decimalPlaces = 4)
        {
            if (minCoefficient < 0)
            {
                throw new ArgumentException("Минимальный коэффициент не может быть меньше 0");
            }

            var result = new Dictionary<string, double>();

            var maxValue = values.Values.Max();
            var minValue = values.Values.Min();

            var scale = 1.0 - minCoefficient;

            foreach (var pair in values)
            {
                result[pair.Key] = CalculateNormalizedValue(
                    minCoefficient,
                    scale,
                    pair.Value,
                    minValue,
                    maxValue,
                    decimalPlaces);
            }

            return result;
        }

        private double CalculateNormalizedValue(
            double minCoefficient,
            double scale,
            double value,
            uint minValue,
            uint maxValue,
            int decimalPlaces)
        {
            if (minValue == maxValue)
            {
                return 1.0;
            }
            return Math.Round(
                minCoefficient + scale * ((double)(value - minValue) / (maxValue - minValue)),
                decimalPlaces);
        }
    }
}
