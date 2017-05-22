using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DrawAlogorithms._3D;

namespace KGG.forms
{
    public partial class SixthTask : Form
    {
        private const double doubleCompareEpsilon = 0.000001;
        private double angle = 0;
        private Point3D location = new Point3D(0, 0, -5);
        public SixthTask()
        {
            InitializeComponent();
            Width = 1000;
            Height = 500;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    angle += 0.1;
                    break;
                case Keys.A:
                    angle -= 0.1;
                    break;
                case Keys.W:
                    location += new Point3D(0, 0, 0.1);
                    break;
                case Keys.S:
                    location += new Point3D(0, 0, -0.1);
                    break;
                case Keys.Space:
                    location += new Point3D(0.1, 0 , 0);
                    break;
                case Keys.C:
                    location += new Point3D(-0.1, 0, 0);
                    break;
            }
            Invalidate();
            base.OnKeyDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bottom = new[]
            {
                new Point3D(1, 0, -1),
                new Point3D(1, 0, 1),
                new Point3D(-1, 0, 0)
            };

            var side1 = new[]
            {
                new Point3D(1, 0, -1),
                new Point3D(1, 0, 1),
                new Point3D(0, 1, 0)
            };

            var side2 = new[]
            {
                new Point3D(1, 0, -1),
                new Point3D(0, 1, 0),
                new Point3D(-1, 0, 0)
            };

            var side3 = new[]
            {
                new Point3D(0, 1, 0),
                new Point3D(1, 0, 1),
                new Point3D(-1, 0, 0)
            };

            var spec = new TransformSpecification()
                .Rotate(RotateVector.Y, angle)
                .Move(location.X, location.Y, location.Z)
                .Project(Math.PI / 2, 1, 1, 100);
            var projector = new Projector();

            var image = new Bitmap(Width, Height);
            var buffer = new double[Height, Width];
            for (var i = 0; i < buffer.GetLength(0); i++)
                for (var j = 0; j < buffer.GetLength(1); j++)
                    buffer[i, j] = double.MaxValue;

            var pBottom = bottom
                .Select(x => projector.Project(x, spec))
                .Select(x => new Point3D(
                    (int)(image.Width * (1 + x.X) / 2),
                    (int)(image.Height * (1 - x.Y) / 2),
                    x.Z))
                .ToArray();
            DrawRectangle(image, pBottom[0], pBottom[1], pBottom[2], Color.Black, buffer);

            var pSide1 = side1.Select(x => projector.Project(x, spec))
                .Select(x => new Point3D(
                    (int)(image.Width * (1 + x.X) / 2),
                    (int)(image.Height * (1 - x.Y) / 2),
                    x.Z))
                .ToArray();
            DrawRectangle(image, pSide1[0], pSide1[1], pSide1[2], Color.Green, buffer);

            var pSide2 = side2.Select(x => projector.Project(x, spec))
                .Select(x => new Point3D(
                    (int)(image.Width * (1 + x.X) / 2),
                    (int)(image.Height * (1 - x.Y) / 2),
                    x.Z))
                .ToArray();
            DrawRectangle(image, pSide2[0], pSide2[1], pSide2[2], Color.Red, buffer);
            //image.DrawLine((int)pSide2[0].X, (int)pSide2[0].Y, (int)pSide2[1].X, (int)pSide2[1].Y, Color.Black);
            //image.DrawLine((int)pSide2[1].X, (int)pSide2[1].Y, (int)pSide2[2].X, (int)pSide2[2].Y, Color.Black);
            //image.DrawLine((int)pSide2[0].X, (int)pSide2[0].Y, (int)pSide2[2].X, (int)pSide2[2].Y, Color.Black);
            var pSide3 = side3.Select(x => projector.Project(x, spec))
                .Select(x => new Point3D(
                    (int)(image.Width * (1 + x.X) / 2),
                    (int)(image.Height * (1 - x.Y) / 2),
                    x.Z))
                .ToArray();
            DrawRectangle(image, pSide3[0], pSide3[1], pSide3[2], Color.Yellow, buffer);

            e.Graphics.DrawImage(image, 0, 0);
            base.OnPaint(e);
        }


        private void Swap<T>(ref T o1, ref T o2)
        {
            var temp = o1;
            o1 = o2;
            o2 = temp;
        }


        private void DrawRectangle(Bitmap image, Point3D t0, Point3D t1, Point3D t2, Color color, double[,] zbuffer)
        {
            if (Math.Abs(t0.Y - t1.Y) < doubleCompareEpsilon &&
                Math.Abs(t0.Y - t2.Y) < doubleCompareEpsilon) return; // i dont care about degenerate triangles
            if (t0.Y > t1.Y) Swap(ref t0, ref t1);
            if (t0.Y > t2.Y) Swap(ref t0, ref t2);
            if (t1.Y > t2.Y) Swap(ref t1, ref t2);
            var total_height = t2.Y - t0.Y;
            for (var i = 0; i < total_height; i++)
            {
                var second_half = i > t1.Y - t0.Y || Math.Abs(t1.Y - t0.Y) < doubleCompareEpsilon;
                var segment_height = second_half ? t2.Y - t1.Y : t1.Y - t0.Y;
                var alpha = i / total_height;
                var beta = (i - (second_half ? t1.Y - t0.Y : 0)) / segment_height;
                // be careful: with above conditions no division by zero here
                var A = t0 + (t2 - t0) * alpha;
                var B = second_half ? t1 + (t2 - t1) * beta : t0 + (t1 - t0) * beta;
                if (A.X > B.X) Swap(ref A, ref B);
                for (var j = A.X; j <= B.X; j++)
                {
                    var phi = Math.Abs(B.X - A.X) < doubleCompareEpsilon ? 1.0 : (j - A.X) / (B.X - A.X);
                    var p = A + (B - A) * phi;
                    var x = (int)p.X;
                    var y = (int)p.Y;
                    if (zbuffer[y, x] > p.Z && p.Z>0)
                    {
                        zbuffer[y, x] = p.Z;
                        image.SetPixel(x, y, color);
                    }
                }
            }
        }
    }
}