using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS363_TeamP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap bmp;
        float angle = 0f;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CreateGr
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X + 30, pictureBox1.Location.Y + 30);

        }
    }
}
