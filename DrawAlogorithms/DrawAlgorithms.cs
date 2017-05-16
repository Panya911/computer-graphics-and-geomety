using System;
using System.Drawing;
using KGG;

namespace DrawAlogorithms
{
    public static class DrawAlgorithms
    {
        public static void DrawGraphic(this Bitmap graphics, Func<double, double> f, double from, double to, Color color)
        {
            var Fa = f(from);
            var ymin = Fa;
            var ymax = Fa;
            var xMin = 0d;
            var xMax = 0d;
            for (var xx = 0; xx < graphics.Width; ++xx)
            {
                var x = from + xx * (to - from) / graphics.Width;
                var y = f(x);

                if (double.IsInfinity(y) || double.IsNaN(y))
                {
                    continue;
                }
                if (x < xMin) { xMin = x; }
                if (x > xMax) { xMax = x; }

                if (y < ymin) ymin = y;
                if (y > ymax) ymax = y;
            }

            PointD previousPoint = null;
            graphics.DrawLine(0, (graphics.Height / 2) - 1, graphics.Width - 1, (graphics.Width / 2) - 1, Color.Red);
            graphics.DrawLine((graphics.Width / 2) - 1, 0, (graphics.Width / 2) - 1, graphics.Height - 1, Color.Red);
            for (int i = (int)from + 1; i < to; i++)
            {
                var pixelX = ScaleOnIntegerSegment(i - xMin, graphics.Width, xMin, xMax);
                graphics.DrawText(pixelX, (graphics.Height / 2) + 6, i.ToString(), Color.Red);

                graphics.DrawLine(pixelX, (graphics.Height / 2) - 3, pixelX, (graphics.Height / 2) + 1, Color.Red);
            }

            var yStep = (ymax - ymin) / 30;
            for (var y = ymin; y < ymax; y += yStep)
            {
                var pixelY = ScaleOnIntegerSegment(ymax - y, graphics.Height, ymin, ymax);
                graphics.DrawText((graphics.Width / 2) + 6, pixelY, y.ToString("0.00"), Color.Red);

                graphics.DrawLine((graphics.Width / 2) - 3, pixelY, (graphics.Width / 2) + 1, pixelY, Color.Red);
            }

            for (var pixelX = 0; pixelX < graphics.Width; pixelX++)
            {
                var x = from + pixelX * (to - from) / graphics.Width;
                var point = new PointD(x, f(x));
                if (double.IsInfinity(point.Y) || double.IsNaN(point.Y))
                {
                    previousPoint = null;
                    continue;
                }
                if (previousPoint == null)
                {
                    previousPoint = point;
                    continue;
                }
                var pixelY = ScaleOnIntegerSegment(ymax - point.Y, graphics.Height, ymin, ymax);

                var previousPixelY = ScaleOnIntegerSegment(ymax - previousPoint.Y, graphics.Height, ymin, ymax);

                graphics.DrawLine(pixelX - 1, previousPixelY, pixelX, pixelY, color);

                previousPoint = point;
            }
        }
        public static void DrawGraphicInPolar(this Bitmap image, Func<double, double> f, double angleFrom, double angleTo, Color color)
        {
            const double step = 0.1d;
            var xMax = 0d;
            var xMin = 0d;
            var yMax = 0d;
            var yMin = 0d;
            for (var angle = angleFrom; angle < angleTo; angle += step)
            {
                var point = PointD.FromPolar(f(angle), angle);
                if (double.IsInfinity(point.Y) || double.IsNaN(point.Y))
                {
                    continue;
                }
                if (point.X < xMin) { xMin = point.X; }
                if (point.X > xMax) { xMax = point.X; }
                if (point.Y < yMin) { yMin = point.Y; }
                if (point.Y > yMax) { yMax = point.Y; }
            }

            PointD previousPoint = null;

            for (var angle = angleFrom; angle < angleTo; angle += step)
            {
                var point = PointD.FromPolar(f(angle), angle);
                if (double.IsInfinity(point.Y) || double.IsNaN(point.Y))
                {
                    previousPoint = null;
                    continue;
                }
                if (previousPoint == null)
                {
                    previousPoint = point;
                    continue;
                }
                var previousX = ScaleOnIntegerSegment(previousPoint.X - xMin, image.Width, xMin, xMax);
                var previousY = ScaleOnIntegerSegment(yMax - previousPoint.Y, image.Height, yMin, yMax);

                var pixelX = ScaleOnIntegerSegment(point.X - xMin, image.Width, xMin, xMax);
                var pixelY = ScaleOnIntegerSegment(yMax - point.Y, image.Height, yMin, yMax);
                image.DrawLine(previousX, previousY, pixelX, pixelY, color);
                previousPoint = point;
            }
        }

        private static int ScaleOnIntegerSegment(double point, int segmentLength, double from, double to)
        {
            var intPoint = (int)ScaleOnSegment(point, segmentLength, from, to);
            if (intPoint >= segmentLength)
                intPoint = segmentLength - 1;
            if (intPoint < 0)
                intPoint = 0;
            return intPoint;
        }

        private static double ScaleOnSegment(double point, double segmentLength, double from, double to)
        {
            return point * segmentLength / (to - from);
        }
    }
}
