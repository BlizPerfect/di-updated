using System.Drawing;
using TagCloud.CloudLayouters;
using TagCloud.CloudLayouterPainters;
using TagCloud.CloudLayouterWorkers;
using TagCloud.ImageSavers;
using TagCloud.Normalizers;
using TagCloud.WordCounters;
using TagCloud.WordFilters;
using TagCloud.WordReaders;

namespace TagCloud
{
    internal class ProgramExecutor(
        string imageFileName,
        IWordCounter wordCounter,
        INormalizer normalizer,
        ICloudLayouter layouter,
        ICloudLayouterPainter painter,
        ICloudLayouterWorker worker,
        IImageSaver imageSaver)
    {
        public void Execute(string resultFormat)
        {
            var normalizedWordWeights = normalizer.Normalize(wordCounter.Values);

            var tags = new List<Tag>();
            foreach (var rectangleProperty in worker.GetNextRectangleProperties())
            {
                tags.Add(new Tag(rectangleProperty.word, layouter.PutNextRectangle(rectangleProperty.size)));
            }

            imageSaver.SaveFile(painter.Draw(tags), imageFileName, resultFormat);
        }
    }
}
