using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawAlogorithms
{
    public struct TempPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsInPoint { get; set; }
        public bool IsOutPoint { get; set; }
        public bool IsFictitiousPoint { get; set; }

        public TempPoint(double x, double y)
        {
            X = x;
            Y = y;
            IsInPoint = false;
            IsOutPoint = false;
            IsFictitiousPoint = false;
        }

        public TempPoint(TempPoint a, TempPoint b)
        {
            this.X = b.X - a.X;
            this.Y = b.Y - a.Y;
            IsInPoint = false;
            IsOutPoint = false;
            IsFictitiousPoint = false;
        }

        public TempPoint(double x, double y, bool isIn, bool isOut)
        {
            X = x;
            Y = y;
            IsInPoint = isIn;
            IsOutPoint = isOut;
            IsFictitiousPoint = false;
        }

        public double DistanceTo(TempPoint other)
        {
            return Math.Sqrt((other.X - this.X) * (other.X - this.X) + (other.Y - this.Y) * (other.Y - this.Y));
        }

        public double value()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public double ScalarMult(TempPoint other)
        {
            return Math.Sqrt(this.X * other.X + this.Y * other.Y);
        }

        public double AngleBetween(TempPoint other)
        {
            var cosOfAngle = (this.ScalarMult(other)) / (this.value() * other.value());
            return Math.Acos(cosOfAngle);
        }

        public double VectorMult(TempPoint other)
        {
            return this.X * other.Y - this.Y * other.X;
        }

        public override string ToString()
        {
            var result = this.X.ToString() + ", " + this.Y.ToString();
            if (this.IsFictitiousPoint)
                result += " " + "fictious";
            if (this.IsInPoint)
                result += " " + "in";
            if (this.IsOutPoint)
                result += " " + "out";
            return result;
        }

        public override bool Equals(object obj)
        {
            var epsilon = 0.0001;
            var other = (TempPoint)obj;
            return Math.Abs(this.X - other.X) < epsilon && Math.Abs(this.Y - other.Y) < epsilon;
        }
    }
}
