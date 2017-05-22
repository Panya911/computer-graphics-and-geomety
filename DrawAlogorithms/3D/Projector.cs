using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Storage;

namespace DrawAlogorithms._3D
{
    public class Projector
    {
        public Point3D Project(Point3D point, TransformSpecification transformSpecification)
        {
            var pointMatrix = ToMatrix(point);
            //var resultPoint = pointMatrix * toCamera * projectMatrix;
            var resultPoint = pointMatrix * transformSpecification.Matrix;
            var planePoint = new Point3D(resultPoint[0, 0] / resultPoint[0, 3], resultPoint[0, 1] / resultPoint[0, 3],
                resultPoint[0, 2] / resultPoint[0, 3]);
            return planePoint;
        }

        private static Matrix<double> ToMatrix(Point3D point)
        {
            return new DenseMatrix(DenseColumnMajorMatrixStorage<double>.OfRowArrays(new[]
            {
                new[] {point.X, point.Y, point.Z, 1d}
            }));
        }
    }

    public class TransformSpecification
    {
        public TransformSpecification()
        {
            Matrix = new DenseMatrix(DenseColumnMajorMatrixStorage<double>.OfDiagonalInit(4, 4, x => 1));
        }

        internal Matrix<double> Matrix { get; private set; }

        public TransformSpecification Move(double x, double y, double z)
        {
            Matrix *= new DenseMatrix(DenseColumnMajorMatrixStorage<double>.OfRowArrays(new[]
            {
                new[] {1d, 0d, 0d, 0d},
                new[] {0d, 1d, 0d, 0d},
                new[] {0d, 0d, 1d, 0d},
                new[] {x, y, z, 1d}
            }));
            return this;
        }

        public TransformSpecification Rotate(RotateVector vector, double angle)
        {
            Matrix<double> rotateMatrix;
            switch (vector)
            {
                case RotateVector.X:
                    rotateMatrix = new DenseMatrix(DenseColumnMajorMatrixStorage<double>.OfRowArrays(new[]
                    {
                        new[] {1d, 0d, 0d, 0d},
                        new[] {0d, Math.Cos(angle), -Math.Sin(angle), 0d},
                        new[] {0d, Math.Sin(angle), Math.Cos(angle), 0d},
                        new[] {0d, 0d, 0d, 1d}
                    }));
                    break;
                case RotateVector.Y:
                    rotateMatrix = new DenseMatrix(DenseColumnMajorMatrixStorage<double>.OfRowArrays(new[]
                    {
                        new[] {Math.Cos(angle), 0d, Math.Sin(angle), 0d},
                        new[] {0d, 1d, 0d, 0d},
                        new[] {-Math.Sin(angle), 0d, Math.Cos(angle), 0d},
                        new[] {0d, 0d, 0d, 1d}
                    }));
                    break;
                case RotateVector.Z:
                    rotateMatrix = new DenseMatrix(DenseColumnMajorMatrixStorage<double>.OfRowArrays(new[]
                    {
                        new[] {Math.Cos(angle), -Math.Sin(angle), 0d, 0d},
                        new[] {Math.Sin(angle), Math.Cos(angle), 0d, 0d},
                        new[] {0d, 0d, 1d, 0d},
                        new[] {0d, 0d, 0d, 1d}
                    }));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vector), vector, null);
            }
            Matrix *= rotateMatrix;
            return this;
        }

        public TransformSpecification Project(double fowY, double aspect, double n, double f)
        {
            var h = 1 / Math.Tan(fowY / 2);
            var w = aspect * h;
            var a = f / f - n;
            var b = -(n * f) / f - n;
            Matrix *= new DenseMatrix(DenseColumnMajorMatrixStorage<double>.OfRowArrays(new[]
            {
                new[] {h, 0d, 0d, 0d},
                new[] {0d, w, 0d, 0d},
                new[] {0d, 0d, a, b},
                new[] {0d, 0d, 1d, 0d}
            }));
            return this;
        }

        public TransformSpecification Inverse()
        {
            Matrix = Matrix.Inverse();
            return this;
        }
    }

    public enum RotateVector
    {
        X,
        Y,
        Z
    }
}