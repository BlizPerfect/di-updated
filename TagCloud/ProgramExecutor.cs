using TagCloud.CloudLayouters;
using TagCloud.CloudLayouterPainters;
using TagCloud.CloudLayouterWorkers;
using TagCloud.ImageSavers;

namespace TagCloud
{
    internal class ProgramExecutor(
        string imageFileName,
        string resultFormat,
        ICloudLayouter layouter,
        ICloudLayouterPainter painter,
        ICloudLayouterWorker worker,
        IImageSaver imageSaver)
    {
        public void Execute()
        {
            var tags = new List<Tag>();
            foreach (var rectangleProperty in worker.GetNextRectangleProperties())
            {
                var tagSize = layouter.PutNextRectangle(rectangleProperty.size);
                var newTag = new Tag(rectangleProperty.word, tagSize);
                tags.Add(newTag);
            }
            imageSaver.SaveFile(painter.Draw(tags), imageFileName, resultFormat);
        }
    }
}
