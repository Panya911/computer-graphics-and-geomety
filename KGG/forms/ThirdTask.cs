using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            image.DrawElipse(panel1.Width/2, panel1.Height/2, panel1.Width/2, panel1.Height/2, Color.Black);
            var g = panel1.CreateGraphics();
            g.DrawImage(image, 0, 0);
            base.OnPaint(e);
        }
    }
}
