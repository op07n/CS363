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
            //MessageBox.Show(string.Format("X: {0}, Y: {1}, A: {2}", me.X, me.Y, (Math.Atan2(me.Y-285,me.X-410) * (180/Math.PI))));
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
        //***************************************************************************************************************
        //These paint events are used for troubleshooting/dev purposes and need to be removed for the final product!!!!!
        //***************************************************************************************************************
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.LawnGreen, 2);
            //Give us a rough idea of where the middle is.  We can use this for the math to determine if the mouse click is within or outside the airspace.
            g.DrawEllipse(p, 135, 10, 550, 550); 
            //This graphic gives us an arc for the runway approach.
            Point point1 = new Point(410, 285);
            Point point2 = new Point(410 + (int)(Math.Cos(-51 * (Math.PI / 180)) * 400), 285 + (int)(Math.Sin(-51 * (Math.PI / 180)) * 400));
            Point point3 = new Point(410 + (int)(Math.Cos(-57 * (Math.PI / 180)) * 400), 285 + (int)(Math.Sin(-57 * (Math.PI / 180)) * 400));
            g.DrawLine(p, point1, point2);
            g.DrawLine(p, point1, point3);
            

        }
    }
}
