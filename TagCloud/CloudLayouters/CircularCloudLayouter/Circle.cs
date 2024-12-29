using System.Drawing;

namespace TagCloud.CloudLayouters.CircularCloudLayouter
{
    internal class Circle(float startRadius = 2.0f)
    {
        private readonly Point center = new Point(0, 0);
        public float Radius { get; set; } = startRadius;

        public IEnumerable<Point> GetCoordinatesOnCircle(
            int startAngle,
            int step = 1)
        {
            for (var dAngle = 0; dAngle < 360; dAngle += step)
            {
                var angle = (startAngle + dAngle) % 360;

                double angleInRadians = angle * Math.PI / 180;
                var x = (int)(center.X + Radius * Math.Cos(angleInRadians));
                var y = (int)(center.Y + Radius * Math.Sin(angleInRadians));
                yield return new Point(x, y);
            }
        }
    }
}
