﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using KGG;

namespace DrawAlogorithms
{
    public static class DrawAlgorithms
    {
        public static void DrawGraphic(this Bitmap image, Func<double, double> f, double from, double to, Color color)
        {
            var Fa = f(from);
            var ymin = Fa;
            var ymax = Fa;
            var xMin = 0d;
            var xMax = 0d;
            for (var xx = 0; xx < image.Width; ++xx)
            {
                var x = from + xx * (to - from) / image.Width;
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
            image.DrawLine(0, (image.Height / 2) - 1, image.Width - 1, (image.Width / 2) - 1, Color.Red);
            image.DrawLine((image.Width / 2) - 1, 0, (image.Width / 2) - 1, image.Height - 1, Color.Red);
            for (var i = (int)from + 1; i < to; i++)
            {
                var pixelX = ScaleOnIntegerSegment(i - xMin, image.Width, xMin, xMax);
                image.DrawText(pixelX, (image.Height / 2) + 6, i.ToString(), Color.Red);

                image.DrawLine(pixelX, (image.Height / 2) - 3, pixelX, (image.Height / 2) + 1, Color.Red);
            }

            var yStep = (ymax - ymin) / 30;
            for (var y = ymin; y < ymax; y += yStep)
            {
                var pixelY = ScaleOnIntegerSegment(ymax - y, image.Height, ymin, ymax);
                image.DrawText((image.Width / 2) + 6, pixelY, y.ToString("0.00"), Color.Red);

                image.DrawLine((image.Width / 2) - 3, pixelY, (image.Width / 2) + 1, pixelY, Color.Red);
            }

            for (var pixelX = 0; pixelX < image.Width; pixelX++)
            {
                var x = from + pixelX * (to - from) / image.Width;
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
                var pixelY = ScaleOnIntegerSegment(ymax - point.Y, image.Height, ymin, ymax);
                var previousPixelY = ScaleOnIntegerSegment(ymax - previousPoint.Y, image.Height, ymin, ymax);
                image.DrawLine(pixelX - 1, previousPixelY, pixelX, pixelY, color);
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

            image.DrawLine(0, (image.Height / 2) - 1, image.Width - 1, (image.Width / 2) - 1, Color.Red);
            image.DrawLine((image.Width / 2) - 1, 0, (image.Width / 2) - 1, image.Height - 1, Color.Red);

            var xStep = (xMax - xMin) / 30;
            for (var x = xMin; x < xMax; x += xStep)
            {
                var pixelX = ScaleOnIntegerSegment(x - xMin, image.Width, xMin, xMax);
                image.DrawText(pixelX, (image.Height / 2) + 6, x.ToString(), Color.Red);

                image.DrawLine(pixelX, (image.Height / 2) - 3, pixelX, (image.Height / 2) + 1, Color.Red);
            }

            var yStep = (yMax - yMin) / 30;
            for (var y = yMin; y < yMax; y += yStep)
            {
                var pixelY = ScaleOnIntegerSegment(yMax - y, image.Height, yMin, yMax);
                image.DrawText((image.Width / 2) + 6, pixelY, y.ToString("0.00"), Color.Red);

                image.DrawLine((image.Width / 2) - 3, pixelY, (image.Width / 2) + 1, pixelY, Color.Red);
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

        public static void DrawGraphicInPolar1(this Bitmap image, Func<double, double> f, double rFrom, double rTo, Color color)
        {
            const double step = 0.1d;
            var xMax = double.MinValue;
            var xMin = double.MaxValue;
            var yMax = double.MinValue;
            var yMin = double.MaxValue;
            for (var r = rFrom; r < rTo; r += step)
            {
                var point = PointD.FromPolar(r, f(r));
                if (double.IsInfinity(point.Y) || double.IsNaN(point.Y))
                {
                    continue;
                }
                if (point.X < xMin) { xMin = point.X; }
                if (point.X > xMax) { xMax = point.X; }
                if (point.Y < yMin) { yMin = point.Y; }
                if (point.Y > yMax) { yMax = point.Y; }
            }

            image.DrawLine(0, (image.Height / 2) - 1, image.Width - 1, (image.Width / 2) - 1, Color.Red);
            image.DrawLine((image.Width / 2) - 1, 0, (image.Width / 2) - 1, image.Height - 1, Color.Red);

            var xStep = (xMax - xMin) / 5;
            for (var x = xMin; x < xMax; x += xStep)
            {
                var pixelX = ScaleOnIntegerSegment(x - xMin, image.Width, xMin, xMax);
                image.DrawText(pixelX, (image.Height / 2) + 6, x.ToString("0.00"), Color.Red);

                image.DrawLine(pixelX, (image.Height / 2) - 3, pixelX, (image.Height / 2) + 1, Color.Red);
            }

            var yStep = (yMax - yMin) / 30;
            for (var y = yMin; y < yMax; y += yStep)
            {
                var pixelY = ScaleOnIntegerSegment(yMax - y, image.Height, yMin, yMax);
                image.DrawText((image.Width / 2) + 6, pixelY, y.ToString("0.00"), Color.Red);

                image.DrawLine((image.Width / 2) - 3, pixelY, (image.Width / 2) + 1, pixelY, Color.Red);
            }

            PointD previousPoint = null;
            for (var r = rFrom; r < rTo; r += step)
            {
                var point = PointD.FromPolar(r, f(r));
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
                if (Math.Abs(point.X - previousPoint.X) < 0.0000001 && Math.Abs(point.Y - previousPoint.Y) < 0.0000001)
                {
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

        public static void DrawElipse(this Bitmap image, int x, int y, int width, int height, int cellSize, Color color)
        {
            var a = width / 2;
            var b = height / 2;
            var _x = 0; // Компонента x
            var _y = b; // Компонента y
            var a_sqr = a * a; // a^2, a - большая полуось
            var b_sqr = b * b; // b^2, b - малая полуось
            var delta = 4 * b_sqr * ((_x + 1) * (_x + 1)) + a_sqr * ((2 * _y - 1) * (2 * _y - 1)) - 4 * a_sqr * b_sqr; // Функция координат точки (x+1, y-1/2)
            while (a_sqr * (2 * _y - 1) > 2 * b_sqr * (_x + 1)) // Первая часть дуги
            {
                DrawSimmetricPixels(x, y, _x, _y);
                if (delta < 0) // Переход по горизонтали
                {
                    _x++;
                    delta += 4 * b_sqr * (2 * _x + 3);
                }
                else // Переход по диагонали
                {
                    _x++;
                    delta = delta - 8 * a_sqr * (_y - 1) + 4 * b_sqr * (2 * _x + 3);
                    _y--;
                }
            }
            delta = b_sqr * ((2 * _x + 1) * (2 * _x + 1)) + 4 * a_sqr * ((_y + 1) * (_y + 1)) - 4 * a_sqr * b_sqr; // Функция координат точки (x+1/2, y-1)
            while (_y + 1 != 0) // Вторая часть дуги, если не выполняется условие первого цикла, значит выполняется a^2(2y - 1) <= 2b^2(x + 1)
            {
                DrawSimmetricPixels(x, y, _x, _y);
                if (delta < 0) // Переход по вертикали
                {
                    _y--;
                    delta += 4 * a_sqr * (2 * _y + 3);
                }
                else // Переход по диагонали
                {
                    _y--;
                    delta = delta - 8 * b_sqr * (_x + 1) + 4 * a_sqr * (2 * _y + 3);
                    _x++;
                }
            }
            void DrawSimmetricPixels(int centerX, int centerY, int x1, int y1)
            {
                FillPixels(image, centerX + x1, centerY + y1, cellSize, color);
                FillPixels(image, centerX - x1, centerY + y1, cellSize, color);
                FillPixels(image, centerX + x1, centerY - y1, cellSize, color);
                FillPixels(image, centerX - x1, centerY - y1, cellSize, color);
            }
        }

        private static void FillPixels(Bitmap image, int centerX, int centerY, int cellSize, Color color)
        {
            var half = cellSize / 2;
            for (var i = centerX * cellSize; i < (centerX + 1) * cellSize; i++)
            {
                for (var j = centerY * cellSize; j < (centerY + 1) * cellSize; j++)
                {
                    if (i < image.Width && i >= 0 && j < image.Height && j >= 0)
                        image.SetPixel(i, j, color);
                }
            }
        }

        public static List<TempPoint> SortVertices(List<TempPoint> polygon)
        {
            var sortedPolygon = new List<TempPoint>();
            var indexOfMin = 0;
            var min = polygon[0].X;
            for (var i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].X < min)
                {
                    min = polygon[i].X;
                    indexOfMin = i;
                }
            }

            var normVector = new TempPoint(min, -1);
            TempPoint left = new TempPoint(0, 0);
            TempPoint right = new TempPoint(0, 0);
            if (indexOfMin != 0)
                left = new TempPoint(min, polygon[indexOfMin - 1].Y);
            else
                left = new TempPoint(min, polygon[polygon.Count - 1].Y);

            if (indexOfMin == polygon.Count - 1)
                right = new TempPoint(min, polygon[0].Y);
            else
                right = new TempPoint(min, polygon[indexOfMin + 1].Y);

            var leftDiff = normVector.AngleBetween(left);
            var rightDiff = normVector.AngleBetween(right);

            if (leftDiff < rightDiff)
            {
                // Идем влево
                for (var i = indexOfMin; i >= 0; i--)
                    sortedPolygon.Add(polygon[i]);
                for (var i = polygon.Count - 1; i > indexOfMin; i--)
                    sortedPolygon.Add(polygon[i]);
            }
            else
            {
                // Идем вправо
                for (var i = indexOfMin; i < polygon.Count; i++)
                    sortedPolygon.Add(polygon[i]);
                for (var i = 0; i < indexOfMin; i++)
                    sortedPolygon.Add(polygon[i]);
            }
            sortedPolygon.Add(sortedPolygon[0]);
            return sortedPolygon;
        }

        public static double VectorMult(double ax, double ay, double bx, double by)
        {
            return ax * by - bx * ay;
        }

        public static bool IsSegmentsIntersect(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4
            )
        {
            var v1 = VectorMult(x4 - x3, y4 - y3, x1 - x3, y1 - y3);
            var v2 = VectorMult(x4 - x3, y4 - y3, x2 - x3, y2 - y3);
            var v3 = VectorMult(x2 - x1, y2 - y1, x3 - x1, y3 - y1);
            var v4 = VectorMult(x2 - x1, y2 - y1, x4 - x1, y4 - y1);
            if ((v1 * v2 < 0) && (v3 * v4 < 0))
                return true;
            return false;
        }

        public static TempPoint GetIntersectPoint(TempPoint a, TempPoint b, TempPoint c, TempPoint d)
        {
            var q1 = a.Y - b.Y;
            var p1 = b.X - a.X;
            var t1 = a.X * b.Y - b.X * a.Y;

            var q2 = c.Y - d.Y;
            var p2 = d.X - c.X;
            var t2 = c.X * d.Y - d.X * c.Y;

            var x = (t2 * p1 - t1 * p2) / (q1 * p2 - q2 * p1);
            var y = (q2 * t1 - q1 * t2) / (q1 * p2 - q2 * p1);

            return new TempPoint(x, y);
        }

        public static List<TempPoint> LabelInOutPoints(List<TempPoint> points, bool startIsIn)
        {
            var result = new List<TempPoint>();
            var isIn = startIsIn;
            foreach (var p in points)
                if (p.IsFictitiousPoint)
                {
                    if (isIn)
                        result.Add(new TempPoint(p.X, p.Y, true, false));
                    else
                        result.Add(new TempPoint(p.X, p.Y, false, true));
                    isIn = !isIn;
                }
                else
                    result.Add(new TempPoint(p.X, p.Y));
            return result;
        }

        public static IEnumerable<List<TempPoint>> FindDiff(List<TempPoint> sp, List<TempPoint> cp)
        {
            var spOutPoints = sp.Where(p => p.IsOutPoint);
            foreach (var p in spOutPoints)
            {
                var result = new List<TempPoint>();
                result.Add(p);
                var first = p;
                var current = p;
                var isCp = false;
                while (true)
                {
                    if (isCp)
                    {
                        // Против обхода
                        var currentIndex = cp.FindIndex(point => point.Equals(current));
                        currentIndex--;
                        if (currentIndex == -1)
                            currentIndex = cp.Count - 2;
                        current = cp[currentIndex];
                    }
                    else
                    {
                        // по обходу
                        var currentIndex = sp.FindIndex(point => point.Equals(current));
                        currentIndex++;
                        if (currentIndex == sp.Count - 1)
                            currentIndex = 0;
                        current = sp[currentIndex];
                    }
                    if (current.Equals(first))
                    {
                        // Конец
                        result.Add(current);
                        yield return result;
                        break;
                    }
                    if (current.IsInPoint)
                        isCp = true;
                    if (current.IsOutPoint)
                        isCp = false;

                    result.Add(current);
                }
            }
        }

        private static bool Convex(TempPoint p, List<TempPoint> polygon)
        {
            var n = polygon.Count;
            var z = new TempPoint(polygon[0], polygon[1]).VectorMult(new TempPoint(polygon[0], p));
            for (var i = 0; i < n - 1; i++)
            {
                var k = new TempPoint(polygon[i], polygon[i + 1]).VectorMult(new TempPoint(polygon[i], p));
                if ((z * k) < 0)
                    return false;
            }
            return true;
        }

        public static IEnumerable<List<TempPoint>> GetDifference(List<TempPoint> sp, List<TempPoint> cp)
        {
            // cp внутри sp
            var count = 0;
            foreach (var p in cp)
                if (Convex(p, sp))
                    count++;
            if (count == cp.Count)
            {
                return new List<List<TempPoint>> { cp, sp };
            }

            // sp внутри cp
            count = 0;
            foreach (var p in sp)
                if (Convex(p, cp))
                    count++;
            if (count == sp.Count)
            {
                return new List<List<TempPoint>>();
            }

            var spWithIntersectPoints = new List<TempPoint>();
            for (var i = 0; i < sp.Count - 1; i++)
            {
                var cpWithIntersectPoints = new List<TempPoint>();

                var intersectPoints = new List<TempPoint>();
                for (var j = 0; j < cp.Count - 1; j++)
                {
                    if (IsSegmentsIntersect(sp[i].X, sp[i].Y, sp[i + 1].X, sp[i + 1].Y, cp[j].X, cp[j].Y, cp[j + 1].X, cp[j + 1].Y))
                    {
                        var intersectPoint = GetIntersectPoint(sp[i], sp[i + 1], cp[j], cp[j + 1]);
                        intersectPoint.IsFictitiousPoint = true;

                        if (cpWithIntersectPoints.Count == 0)
                            cpWithIntersectPoints.Add(cp[j]);
                        cpWithIntersectPoints.Add(intersectPoint);
                        cpWithIntersectPoints.Add(cp[j + 1]);

                        intersectPoints.Add(intersectPoint);
                    }
                    else
                    {
                        if (cpWithIntersectPoints.Count == 0)
                            cpWithIntersectPoints.Add(cp[j]);
                        cpWithIntersectPoints.Add(cp[j + 1]);
                    }
                }

                cp = cpWithIntersectPoints;

                intersectPoints.Sort((p1, p2) =>
                {
                    var d1 = sp[i].DistanceTo(p1);
                    var d2 = sp[i].DistanceTo(p2);
                    return d1.CompareTo(d2);
                }
                );

                if (spWithIntersectPoints.Count == 0)
                    spWithIntersectPoints.Add(sp[i]);
                foreach (var p in intersectPoints)
                    spWithIntersectPoints.Add(p);
                spWithIntersectPoints.Add(sp[i + 1]);

            }
            sp = spWithIntersectPoints;

            sp = LabelInOutPoints(sp, true);
            cp = LabelInOutPoints(cp, false);
            return FindDiff(sp, cp);
        }
    }
}
