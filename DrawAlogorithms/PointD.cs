using System;

namespace DrawAlogorithms
{
    internal class PointD
    {
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public static PointD FromPolar(double r, double angle)
        {
            return new PointD(r * Math.Cos(angle), r * Math.Sin(angle));
        }
    }
}