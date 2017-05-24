using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DrawAlogorithms;

namespace KGG.forms
{
    public partial class FourthTask : Form
    {
        private List<TempPoint> first = new List<TempPoint>();
        private List<TempPoint> second = new List<TempPoint>();
        private IEnumerable<List<TempPoint>> result;
        private List<List<TempPoint>> result2;

        public FourthTask()
        {
            InitializeComponent();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    first.Add(new TempPoint(e.X, e.Y));
                    break;
                case MouseButtons.Right:
                    second.Add(new TempPoint(e.X, e.Y));
                    break;
            }
            base.OnMouseClick(e);
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    first = new List<TempPoint>();
                    second = new List<TempPoint>();
                    result = null;
                    break;
                case Keys.Enter:
                    var cp = DrawAlgorithms.SortVertices(first);
                    var rp = DrawAlgorithms.SortVertices(second);
                    result = DrawAlgorithms.GetDifference(cp, rp).ToList();
                    result2 = DrawAlgorithms.GetDifference(rp, cp).ToList();
                    break;
            }
            base.OnKeyDown(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var image = new Bitmap(Width, Height);
            DrawPoligon(image, first, Color.Black, false);
            DrawPoligon(image, second, Color.Black, false);
            if (result != null)
            {
                foreach (var poligon in result)
                {
                    DrawPoligon(image, poligon, Color.Red, true);
                    image.FillPoligon(poligon.Select(x => new Point((int)x.X, (int)x.Y)).ToArray(), Color.Red);
                }
                foreach (var poligon in result2)
                {
                    image.FillPoligon(poligon.Select(x => new Point((int)x.X, (int)x.Y)).ToArray(), Color.Red);
                    DrawPoligon(image, poligon, Color.Blue, true);
                }
            }
            e.Graphics.DrawImage(image, 0, 0);
            base.OnPaint(e);
        }

        private void DrawPoligon(Bitmap image, IList<TempPoint> poligon, Color color, bool fromAlg)
        {
            if (poligon.Count == 0)
                return;
            for (int i = 0; i < poligon.Count - 1; i++)
            {
                var start = poligon[i];
                var end = poligon[i + 1];
                image.DrawLine((int)start.X, (int)start.Y, (int)end.X, (int)end.Y, color);
            }
            if (fromAlg)
            {
                return;
            }
            var first = poligon[0];
            var last = poligon[poligon.Count - 1];
            image.DrawLine((int)first.X, (int)first.Y, (int)last.X, (int)last.Y, color);
        }
    }
}
