using System.Drawing;

namespace TagCloud.CloudLayouterPainters
{
    internal class CloudLayouterPainter(
        Size imageSize,
        Color? backgroundColor = null,
        Color? textColor = null,
        FontFamily? fontName = null) : ICloudLayouterPainter
    {
        private readonly Color backgroundColor = backgroundColor ?? Color.White;
        private readonly Color textColor = textColor ?? Color.Black;
        private readonly FontFamily fontName = fontName ?? new FontFamily("Arial");

        public Bitmap Draw(IList<Tag> tags)
        {
            ArgumentNullException.ThrowIfNull(tags);

            if (tags.Count == 0)
            {
                throw new ArgumentException("Список тегов пуст");
            }

            var result = new Bitmap(imageSize.Width, imageSize.Height);

            using var graphics = Graphics.FromImage(result);
            graphics.Clear(backgroundColor);

            foreach (var tag in tags)
            {
                var positionOnCanvas = GetPositionOnCanvas(tag.Rectangle);
                var rectOnCanvas = new Rectangle(
                    positionOnCanvas.X,
                    positionOnCanvas.Y,
                    tag.Rectangle.Width,
                    tag.Rectangle.Height);
                DrawText(graphics, rectOnCanvas, tag.Text);
            }

            return result;
        }

        private Point GetPositionOnCanvas(Rectangle rectangle)
            => new Point(rectangle.X + imageSize.Width / 2, rectangle.Y + imageSize.Height / 2);

        private void DrawText(Graphics graphics, Rectangle rectangle, string text)
        {
            var fontSize = FindFittingFontSize(graphics, text, rectangle);
            var fittingFont = new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);

            using var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            using var brush = new SolidBrush(textColor);
            graphics.DrawString(text, fittingFont, brush, rectangle, stringFormat);
        }

        private int FindFittingFontSize(Graphics graphics, string text, Rectangle rectangle)
        {
            var minSize = 1;
            var maxSize = Math.Min(rectangle.Width, rectangle.Height);
            var result = minSize;

            while (minSize <= maxSize)
            {
                var midSize = (minSize + maxSize) / 2;
                using var font = new Font(fontName, midSize, FontStyle.Regular, GraphicsUnit.Pixel);

                var textSize = graphics.MeasureString(text, font);
                if (textSize.Width <= rectangle.Width && textSize.Height <= rectangle.Height)
                {
                    result = midSize;
                    minSize = midSize + 1;
                }
                else
                {
                    maxSize = midSize - 1;
                }
            }

            return result;
        }
    }
}
