using System.Drawing;
using System.Windows.Forms;
using DrawAlogorithms;

namespace KGG.forms
{
    public partial class ThirdTask : Form
    {
        public ThirdTask()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var image = new Bitmap(panel1.Width, panel1.Height);
            image.Fill(Color.White);
            image.DrawElipse(50, 50, 70, 50, 5, Color.Black);
            var g = panel1.CreateGraphics();
            g.DrawImage(image, 0, 0);
            base.OnPaint(e);
        }
    }
}
