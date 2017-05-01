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
            for (var xx = 0; xx < graphics.Width; ++xx)
            {
                var x = from + xx * (to - from) / graphics.Width;
                var y = f(x);
                if (y < ymin) ymin = y;
                if (y > ymax) ymax = y;
            }

            var previousX = 0;
            var previousY = (int)((f(from) - ymax) * graphics.Height / (ymin - ymax));
            for (var pixelX = 1; pixelX < graphics.Width; pixelX++)
            {
                var x = from + pixelX * (to - from) / graphics.Width;
                var pixelY = (int)((f(x) - ymax) * graphics.Height / (ymin - ymax));
                if (pixelY >= graphics.Height)
                    pixelY = graphics.Height - 1;
                if (pixelY < 0)
                    pixelY = 0;
                graphics.DrawLine(previousX, previousY, pixelX, pixelY, color);
                previousX = pixelX;
                previousY = pixelY;
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
