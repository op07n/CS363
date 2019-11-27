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
        public List<Plane> list;

        public Form1()
        {
            InitializeComponent();
            list = new List<Plane>();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            
            

        }

        private void Form1_Click(object sender, EventArgs e)    //FIXME: Need logic to determine generation point and initial heading
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Plane plane = new Plane();
            list.Add(plane);
            int X = MousePosition.X;
            int Y = MousePosition.Y;
            
            //MessageBox.Show(string.Format("X: {0}, Y: {1}, A: {2}", me.X, me.Y, (Math.Atan2(me.Y-360,me.X-850) * (180/Math.PI)))); //useful for determining mouse click location and angle from center
            if (me.Button == MouseButtons.Right)
            {
                //Outbound aircraft generation
                if (Math.Sqrt(Math.Pow(me.X - 850, 2) + Math.Pow(me.Y - 360, 2)) < 355) 
                {
                    plane.mkPlane(this, 4);
                }
                else    //FIXME: Need logic to determine inbound start location
                {
                    if (me.Y < 360)
                    {
                        if (me.X < 850)
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
            if (list.Count > 1)
            {
                bool collide = false;
                foreach (var aircraft in list)
                {
                    Rectangle collideRadius = new Rectangle(aircraft.Airplane.Left, aircraft.Airplane.Top, aircraft.Airplane.Width, aircraft.Airplane.Height);
                    
                    foreach (var aircraft2 in list)
                    {
                        if (aircraft == aircraft2)
                        {
                            continue;
                        }
                        else
                        {
                            Rectangle collideRadius2 = new Rectangle(aircraft2.Airplane.Left, aircraft2.Airplane.Top, aircraft2.Airplane.Width, aircraft2.Airplane.Height);
                            if (collideRadius2.IntersectsWith(collideRadius))
                            {
                                PictureBox collision = new PictureBox();
                                collision.BackColor = this.pictureBox1.BackColor;
                                collision.Image = this.pictureBox1.Image;
                                collision.Location = aircraft.Airplane.Location;
                                collision.Name = "Collision";
                                collision.Size = this.pictureBox1.Size;
                                collision.SizeMode = this.pictureBox1.SizeMode;
                                this.Controls.Remove(aircraft.Airplane);
                                this.Controls.Remove(aircraft.planeinfo);
                                this.Controls.Remove(aircraft.tblPlaneInfo);
                                this.Controls.Remove(aircraft2.Airplane);
                                this.Controls.Remove(aircraft2.planeinfo);
                                this.Controls.Remove(aircraft2.tblPlaneInfo);
                                this.Controls.Add(collision);
                                list.Remove(aircraft);
                                list.Remove(aircraft2);
                                collide = true;
                                break;
                            }
                            
                        }
                    }
                    if (collide == true)
                    {
                        break;
                    }
                }
            }

        }
        //***************************************************************************************************************
        //These paint events are used for troubleshooting/dev purposes and need to be removed for the final product!!!!!
        //***************************************************************************************************************
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int centerX = 850;
            int centerY = 360;

            Graphics g = e.Graphics;
            Pen p = new Pen(Color.LawnGreen, 2);
            //Give us a rough idea of where the middle is.  We can use this for the math to determine if the mouse click is within or outside the airspace.
            g.DrawEllipse(p, 495, 8, 710, 710); 
            //This graphic gives us an arc for the runway approach.
            Point point1 = new Point(centerX, centerY);
            Point point2 = new Point(centerX + (int)(Math.Cos(-52 * (Math.PI / 180)) * 400), centerY + (int)(Math.Sin(-52 * (Math.PI / 180)) * 400));
            Point pointC = new Point(centerX + (int)(Math.Cos(-55 * (Math.PI / 180)) * 400), centerY + (int)(Math.Sin(-55 * (Math.PI / 180)) * 400));
            Point point3 = new Point(centerX + (int)(Math.Cos(-58 * (Math.PI / 180)) * 400), centerY + (int)(Math.Sin(-58 * (Math.PI / 180)) * 400));
            g.DrawLine(p, point1, point2);
            g.DrawLine(p, point1, pointC);
            g.DrawLine(p, point1, point3);
            

        }
    }
}
