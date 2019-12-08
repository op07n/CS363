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
using System.Media;
using System.Windows.Input;

namespace CS363_TeamP
{
    public partial class Form1 : Form
    {
        //Instantiate lists to store all Plane classes generated
        public List<Plane> InFlightList;
        public List<Plane> TakeoffList;
        public List<Plane> EnemyPlanes;
        public List<Bullet> Bullets;
        
        //Instantiate soundplayer for collision imminent alerts
        SoundPlayer collisionAlert;
        int pointX = 0, pointY = 0;
        public bool airportDefenses = true;
        //Declare defense variables
        int turretAngle = 0;
        int score = 0;
        

        public Form1()
        {
            InitializeComponent();
            //Define InFlightList and TakeoffQueue as lists of planes
            InFlightList = new List<Plane>();
            TakeoffList = new List<Plane>();
            EnemyPlanes = new List<Plane>();
            Bullets = new List<Bullet>();
            //Define collisionAlert sound player
            collisionAlert = new SoundPlayer(CS363_TeamP.Properties.Resources.WeaponHoming);
            
        }
        //
        //Form1_Load
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }
        //
        //collisionAvoidance - Generates warnings if two aircraft violate CA distances from eachother. Also removes aircraft that collide and places collision indicator.
        //
        public void collisionAvoidance()
        {
            
            if (InFlightList.Count > 1)
            {
                bool collide = false;
                bool collisionImminent = false;
                foreach (var aircraft in InFlightList)
                {
                    Rectangle collideBox1 = new Rectangle(aircraft.Airplane.Left, aircraft.Airplane.Top, aircraft.Airplane.Width, aircraft.Airplane.Height);
                    (double X1, double Y1) = aircraft.vectorScale(aircraft.heading);
                    int x1 = aircraft.Airplane.Location.X + (int)(aircraft.speed / 10 * Y1);// + aircraft.Airplane.Size.Width / 2;
                    int y1 = aircraft.Airplane.Location.Y - (int)(aircraft.speed / 10 * X1);// + aircraft.Airplane.Size.Height / 2;
                    foreach (var aircraft2 in InFlightList)
                    {
                        if (aircraft == aircraft2)
                        {
                            continue;
                        }
                        else
                        {
                            Rectangle collideBox2 = new Rectangle(aircraft2.Airplane.Left, aircraft2.Airplane.Top, aircraft2.Airplane.Width, aircraft2.Airplane.Height);
                            (double X2, double Y2) = aircraft2.vectorScale(aircraft2.heading);
                            int x2 = aircraft2.Airplane.Location.X + (int)(aircraft2.speed / 10 * Y2);// + aircraft2.Airplane.Size.Width/2;
                            int y2 = aircraft2.Airplane.Location.Y - (int)(aircraft2.speed / 10 * X2);// + aircraft2.Airplane.Size.Height/2;
                            if (collideBox2.IntersectsWith(collideBox1) && Math.Abs(aircraft.altitude - aircraft2.altitude) <= 1)
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
                                InFlightList.Remove(aircraft);
                                InFlightList.Remove(aircraft2);
                                collide = true;
                                break;
                            }
                            int dx = x2 - x1;
                            int dy = y2 - y1;
                            double dist = Math.Sqrt(dx * dx + dy * dy);
                            //MessageBox.Show(string.Format("D: {0}", dist));
                            if (dist <= 75 && Math.Abs(aircraft.altitude - aircraft2.altitude) <= 10)
                            {
                                collisionImminent = true;
                                //MessageBox.Show("Collision Imminent");
                                this.txtCollisionImminent.Visible = true;
                                aircraft.planeinfo.ForeColor = Color.Red;
                                aircraft2.planeinfo.ForeColor = Color.Red;
                            }
                            else if (collisionImminent == false)
                            {
                                //   this.txtCollisionAlert.Visible = false;
                                aircraft.planeinfo.ForeColor = Color.White;
                                aircraft2.planeinfo.ForeColor = Color.White;
                                this.txtCollisionImminent.Visible = false;
                            }

                        }
                    }
                    if (collide == true)
                    {
                        break;
                    }
                }
                if (collisionImminent == true)
                {
                    collisionAlert.Play();
                }
            }
        }
        //
        //Form1_Click - Generates aircraft on right mouse click.  Determines plane generation location based on mouse click location.
        //
        private void Form1_Click(object sender, EventArgs e)    //FIXME: Need logic to determine generation point and initial heading
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Plane plane = new Plane();
            int X = MousePosition.X;
            int Y = MousePosition.Y;

            //MessageBox.Show(string.Format("X: {0}, Y: {1}, A: {2}", me.X, me.Y, (Math.Atan2(me.Y-360,me.X-850) * (180/Math.PI)))); //useful for determining mouse click location and angle from center
            if (me.Button == MouseButtons.Right)
            {
                //Outbound aircraft generation
                if (Math.Sqrt(Math.Pow(me.X - 850, 2) + Math.Pow(me.Y - 360, 2)) < 355)
                {
                    TakeoffList.Add(plane);
                    plane.control = 'D';
                    plane.infoGenerator();
                    //plane.mkPlane(this, 4);
                    this.dgvTakeoffQueue.Rows.Add(plane.ID, plane.destAP, plane.runwayID, "0:00");
                }
                else    //FIXME: Need logic to determine inbound start location
                {
                    InFlightList.Add(plane);
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
        //
        //timer1_Tick - main timer used by all classes for syncronize movement and events
        //
        public void timer1_Tick(object sender, EventArgs e)
        {
            //Make enemy planes
            if(airportDefenses == true)
            {
                Plane plane = new Plane();
                EnemyPlanes.Add(plane);
                if(EnemyPlanes.Count <= 20)
                {
                    plane.mkEnemyPlane(this);
                }
                
            }

            //Check if any planes are violating eachother's safe space
            //Update cloud location
            if(airportDefenses == false)
            {
                collisionAvoidance();
                pointX += 5;
                pointY += 5;
                Invalidate();
            }
            else
            {
                pointX = 1000;
                pointY = 1000;
            }
            
            //Increment wait time for any planes awaiting takeoff
            foreach (DataGridViewRow row in dgvTakeoffQueue.Rows)
            {
                string time = row.Cells[3].Value.ToString();
                string secs;
                int sec = Int32.Parse(time.Split(':')[1]);
                int min = Int32.Parse(time.Split(':')[0]);
                sec += timer1.Interval/1000;
                if(sec > 59)
                {
                    min += 1;
                    sec = sec % 60;
                }
                if (sec < 10)
                {
                    secs = 0 + sec.ToString();
                }
                else
                {
                    secs = sec.ToString();
                }
                row.Cells[3].Value = min.ToString() + ':' + secs;
            }
        }
        //
        // Timer2_Tick
        //
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if(airportDefenses == true)
            {
                bool shotdown = false;
                foreach (Plane x in EnemyPlanes)
                {
                    foreach(Bullet y in Bullets)
                    {
                        if (x.Airplane.Bounds.IntersectsWith(y.bullet.Bounds))
                        {
                            score++;
                            x.Airplane.Dispose();
                            x.Airplane = null;
                            x.planeinfo.Dispose();
                            x.planeinfo = null;
                            EnemyPlanes.Remove(x);
                            x.tm.Stop();
                            x.tm.Dispose();
                            x.tm = null;
                            y.bullet.Dispose();
                            y.bullet = null;
                            y.tm.Dispose();
                            y.tm = null;
                            Bullets.Remove(y);
                            shotdown = true;
                            break;
                        }
                    }
                    if (shotdown)
                    {
                        break;
                    }
                }
                Invalidate();
            }
        }
        //
        //Form1_Paint - Used to draw weather graphics and make troubleshooting graphics
        //
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Attempt to use a weather class - Cannot get graphics to update using Invalidate or Refresh
            //Weather weather = new Weather();
            //weather.mkCloud(this, g);
            //var bmp = new Bitmap(Properties.Resources.Cloud1);
            Graphics g = e.Graphics;
            //Directly drawing weather graphics to form for demonstration purposes.  We would need weather class to be functional for production.
            if (airportDefenses == false)
            {
                
                g.DrawImage(Properties.Resources.Cloud4, pointX, pointY);
                g.DrawImage(Properties.Resources.Cloud3, pointX, pointY - 200);
                g.DrawImage(Properties.Resources.Cloud1, pointX + 600, pointY + 100);
                g.DrawImage(Properties.Resources.Cloud3, pointX + 600, pointY + 500);
            }

            //***************************************************************************************************************
            //These paint events are used for troubleshooting/dev purposes and need to be removed/commented for the final product!!!!!
            //***************************************************************************************************************
            /*
            int centerX = 850;
            int centerY = 360;
            Pen p = new Pen(Color.LawnGreen, 2);
            //Give us a rough idea of where the middle is.  We can use this for the math to determine if the mouse click is within or outside the airspace.
            g.DrawEllipse(p, 495, 8, 710, 710); 
            //This graphic gives us an arc for the runway approach.
            Point point1 = new Point(centerX, centerY);
            Point point2 = new Point(centerX + (int)(Math.Cos(-51 * (Math.PI / 180)) * 400), centerY + (int)(Math.Sin(-52 * (Math.PI / 180)) * 400));
            Point pointC = new Point(centerX + (int)(Math.Cos(-55 * (Math.PI / 180)) * 400), centerY + (int)(Math.Sin(-55 * (Math.PI / 180)) * 400));
            Point point3 = new Point(centerX + (int)(Math.Cos(-59 * (Math.PI / 180)) * 400), centerY + (int)(Math.Sin(-58 * (Math.PI / 180)) * 400));
            g.DrawLine(p, point1, point2);
            g.DrawLine(p, point1, pointC);
            g.DrawLine(p, point1, point3);
            */
            if (airportDefenses == true)
            {
                Pen p = new Pen(Color.LawnGreen, 2);
                int centerX = 850;
                int centerY = 360;
                Point point1 = new Point(centerX, centerY);
                Point pointC = new Point(centerX + (int)(Math.Cos(turretAngle * (Math.PI / 180)) * 400), centerY + (int)(Math.Sin(turretAngle * (Math.PI / 180)) * 400));
                g.DrawLine(p, point1, pointC);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(airportDefenses == true)
            {
                if (keyData == Keys.Left)
                {
                    turretAngle = (turretAngle - 1) % 360;
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    turretAngle = (turretAngle + 1) % 360;
                    return true;
                }
                else if (keyData == Keys.Space)
                {
                    Bullet bullet = new Bullet();
                    bullet.direction = turretAngle;
                    bullet.mkBullet(this);
                    Bullets.Add(bullet);
                    return true;
                }
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        

        public void dgvTakeoffQueue_CellClick(Object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == dgvTakeoffQueue.Columns["btnTakeoff"].Index)
            {
                int selectedRow = e.RowIndex;
                DataGridViewRow Row = dgvTakeoffQueue.Rows[selectedRow];
                foreach (var airplane in TakeoffList)
                {
                    if (airplane.ID == Row.Cells[0].Value.ToString())
                    {
                        //Plane planeToTakeoff = airplane;
                        InFlightList.Add(airplane);
                        airplane.mkPlane(this, 4);
                        dgvTakeoffQueue.Rows.Remove(Row);
                        TakeoffList.Remove(airplane);
                        break;
                    }
                }
                
                   

                

            }
        }
    }
}
