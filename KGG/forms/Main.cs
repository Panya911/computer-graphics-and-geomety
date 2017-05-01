using System;
using System.Windows.Forms;

namespace KGG.forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var firstTaskForm = new FirstTask();
            firstTaskForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var secondTaskForm = new SecondTask();
            secondTaskForm.Show();
        }
    }
}