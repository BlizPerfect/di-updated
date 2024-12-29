using System.Drawing;
using TagCloud.CloudLayouterPainters;

namespace TagCloud.Tests.CloudLayouterPaintersTest
{
    internal class CloudLayouterPainterTest
    {
        private CloudLayouterPainter painter;

        [SetUp]
        public void SetUp()
        {
            painter = new CloudLayouterPainter(new Size(1, 1));
        }

        [Test]
        public void Draw_ThrowsArgumentException_WithEmptyTags()
        {
            var painter = new CloudLayouterPainter(new Size(1, 1));
            Assert.Throws<ArgumentException>(() => painter.Draw(new List<Tag>()));
        }

        [Test]
        public void Draw_ThrowsArgumentNullException_WithTagsAsNull()
        {
            var painter = new CloudLayouterPainter(new Size(1, 1));
            Assert.Throws<ArgumentNullException>(() => painter.Draw(null!));
        }
    }
}
