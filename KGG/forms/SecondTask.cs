using System;
using System.Drawing;
using System.Windows.Forms;
using DrawAlogorithms;

namespace KGG
{
    public partial class SecondTask : Form
    {
        private double a = 1;
        private double b = 1;
        private double c = 1;
        private double left = 0;
        private double right = Math.PI * 2;

        public SecondTask()
        {
            InitializeComponent();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var image = new Bitmap(Panel.Width, Panel.Height);
            image.Fill(Color.White);
            image.DrawGraphicInPolar(
                x => Math.PI / x,
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
            left = FromDegree(double.Parse(leftBorderTextBox.Text));
            right = FromDegree(double.Parse(rightBorderTextBox.Text));
        }

        private double FromDegree(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}