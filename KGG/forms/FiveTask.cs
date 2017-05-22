using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DrawAlogorithms._3D;

namespace KGG.forms
{
    public partial class FiveTask : Form
    {
        private readonly Panel panel;
        private Point3D figureLocation = new Point3D(0, 0, -10);
        private double xAngle;
        private double yAngle;
        private int horizontsCount;
        public FiveTask()
        {
            InitializeComponent();
            Width = 1000;
            Height = 500;
            panel = new Panel
            {
                Location = new Point(0, 0),
                Width = Width,
                Height = Height
            };
            KeyDown += FiveTask_KeyDown;
        }

        private void FiveTask_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    figureLocation += new Point3D(-0.1, 0, 0);
                    break;
                case Keys.S:
                    figureLocation += new Point3D(0, 0, -0.1);
                    break;
                case Keys.D:
                    figureLocation += new Point3D(0.1, 0, 0);
                    break;
                case Keys.W:
                    figureLocation += new Point3D(0, 0, 0.1);
                    break;
                case Keys.Space:
                    figureLocation += new Point3D(0, 0.1, 0);
                    break;
                case Keys.C:
                    figureLocation += new Point3D(0, -0.1, 0);
                    break;
                case Keys.J:
                    yAngle += 0.1;
                    break;
                case Keys.L:
                    yAngle -= 0.1;
                    break;
                case Keys.P:
                    horizontsCount++;
                    break;
            }
            Invalidate();
        }

        private Image DrawFigure(Func<double, double, double> f, double fromZ, double toZ, double stepZ, double fromX,
            double toX, double stepX)
        {
            var spec = new TransformSpecification()
                .Rotate(RotateVector.Y, yAngle)
                .Rotate(RotateVector.X, xAngle)
                .Move(figureLocation.X, figureLocation.Y, figureLocation.Z)
                .Project(Math.PI / 2, 16 / 8, 1, 100);

            var normSpec = new TransformSpecification()
                .Rotate(RotateVector.Y, yAngle)
                .Rotate(RotateVector.X, xAngle);

            var projector = new Projector();

            var image = new Bitmap(panel.Width, panel.Height);

            var planes = Range(fromZ, toZ, stepZ)
                .OrderByDescending(z => Math.Abs(projector.Project(new Point3D(0, 0, z), normSpec).Z - figureLocation.Z))
                .ToArray();

            var topHorizont = new int[image.Width];
            var downHorizont = new int[image.Width];
            InitWith(topHorizont, int.MaxValue);
            InitWith(downHorizont, int.MinValue);
            foreach (var plane in planes.Take(horizontsCount))
            {
                Point? previous = null;
                for (var xi = fromX; xi < toX; xi += stepX)
                {
                    var p1 = new Point3D(xi, f(xi, plane), plane);
                    if (double.IsInfinity(p1.Y) || double.IsNaN(p1.Y))
                    {
                        p1 = new Point3D(xi, 0, plane);
                    }
                    var leftPointX = projector.Project(p1, spec);
                    var scaleLeftPoint = new Point(
                        (int)(image.Width * (1 + leftPointX.X) / 2),
                        (int)(image.Height * (1 - leftPointX.Y) / 2));

                    var p2 = new Point3D(xi + stepX, f(xi + stepX, plane), plane);
                    if (double.IsInfinity(p2.Y) || double.IsNaN(p2.Y))
                    {
                        p2 = new Point3D(xi + stepX, 0, plane);
                    }
                    var rightPointX = projector.Project(p2, spec);
                    var scaleRightPoint = new Point(
                        (int)(image.Width * (1 + rightPointX.X) / 2),
                        (int)(image.Height * (1 - rightPointX.Y) / 2));
                    if (scaleLeftPoint.X > scaleRightPoint.X)
                    {
                        var temp = scaleLeftPoint;
                        scaleLeftPoint = scaleRightPoint;
                        scaleRightPoint = temp;
                    }
                    for (var x = scaleLeftPoint.X + 1; x <= scaleRightPoint.X; x++)
                    {
                        var y = (int)GetInterpolation(x, scaleLeftPoint.X, scaleLeftPoint.Y, scaleRightPoint.X,
                            scaleRightPoint.Y);
                        var point = new Point(x, y);
                        if (x >= image.Width || x < 0 ||
                            y >= image.Height || y < 0)
                        {
                            previous = null;
                            continue;
                        }
                        var wasDrawing = false;
                        if (/*topHorizont[x].HasValue &&*/ y < topHorizont[x])
                        {
                            topHorizont[x] = y;
                            if (previous.HasValue)
                            {
                                var p = previous.Value;
                                image.DrawLine(p.X, p.Y, x, y, Color.Black);
                            }
                            wasDrawing = true;
                            previous = point;
                        } //else
                        if (/*downHorizont[x].HasValue &&*/ y > downHorizont[x])
                        {
                            downHorizont[x] = y;
                            if (previous.HasValue)
                            {
                                var p = previous.Value;
                                image.DrawLine(p.X, p.Y, x, y, Color.Blue);
                            }
                            wasDrawing = true;
                            previous = point;
                        }
                        if (!wasDrawing)
                        {
                            previous = null;
                        }
                    }
                }
            }
            return image;

            IEnumerable<double> Range(double from, double to, double step)
            {
                for (var current = from; current <= to; current += step)
                    yield return current;
            }

            double GetInterpolation(double x, double x1, double y1, double x2, double y2)
            {
                if (Math.Abs(x1 - x2) < 0.0000001)
                    return y1;
                return y1 + (y2 - y1) / (x2 - x1) * (x - x1);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var image = new Bitmap(panel.Width, panel.Height);
            var figureImage = DrawFigure((x, z) => Math.Sqrt(x * x - z * z),
            -10, 5, 0.1, -5, 5, 0.1);
            e.Graphics.DrawImage(figureImage, 0, 0);

            using (var g = CreateGraphics())
            {
                g.DrawImage(image, 0, 0);
            }
        }

        private static void InitWith<T>(T[] array, T item)
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = item;
        }
    }
}