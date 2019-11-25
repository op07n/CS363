using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace CS363_TeamP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            
            

        }

        private void Form1_Click(object sender, EventArgs e)    //FIXME: Need logic to determine generation point and initial heading
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Plane plane = new Plane();
            int X = MousePosition.X;
            int Y = MousePosition.Y;
            ;
            //plane.heading = 121;
            //MessageBox.Show(string.Format("X: {0}, Y: {1}, D: {2}", me.X, me.Y, Math.Sqrt(Math.Pow(me.X - 410, 2) + Math.Pow(me.Y - 285, 2))));
            if (me.Button == MouseButtons.Right)
            {
                //Outbound aircraft generation
                if (Math.Sqrt(Math.Pow(me.X - 410, 2) + Math.Pow(me.Y - 285, 2)) < 275) 
                {
                    plane.mkPlane(this, 4);
                }
                else    //FIXME: Need logic to determine inbound start location
                {
                    if (me.Y < 285)
                    {
                        if (me.X < 410)
                        {
                            plane.mkPlane(this, 1);
                        }
                        else
                        {
                            plane.mkPlane(this, 2);
                        }
                    }
                    else
                    {
                        plane.mkPlane(this, 3);
                    }
                }
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            //pictureBox1.Location = new Point(pictureBox1.Location.X + 30, pictureBox1.Location.Y + 30);

        }

        //Give us a rough idea of where the middle is.  We can use this for the math to determine if the mouse click is within or outside the airspace.
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.LawnGreen, 2);
            
            g.DrawEllipse(p, 135, 10, 550, 550);
            
        }
    }
}
