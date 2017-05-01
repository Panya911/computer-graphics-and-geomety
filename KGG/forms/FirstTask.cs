using System;
using System.Drawing;
using System.Windows.Forms;
using DrawAlogorithms;

namespace KGG
{
    public partial class FirstTask : Form
    {
        private double a = 1;
        private double b = 1;
        private double c = 1;
        private double left = -10;
        private double right = 10;

        public FirstTask()
        {
            InitializeComponent();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var image = new Bitmap(Panel.Width, Panel.Height);
            image.Fill(Color.White);
            image.DrawGraphic(
                x => (x * x - a * a) / (x * x - b * x - c),
                left,
                right,
                Color.Black);
            var g = Panel.CreateGraphics();
            g.DrawImage(image, 0, 0);
            base.OnPaint(e);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            a = double.Parse(coefficientATextBox.Text);
            b = double.Parse(coefficientBTextBox.Text);
            c = double.Parse(coefficientCTextBox.Text);
            left = double.Parse(leftBorderTextBox.Text);
            right = double.Parse(rightBorderTextBox.Text);
        }
    }
}