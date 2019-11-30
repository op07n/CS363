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
    public class Plane
    {
        private int speed;
        public string ID;
        public char control = ' ';
        public int altitude;
        private int heading;
        public string destAP;
        private int expectedSpeed;
        private int expectedAltitude;
        private int expectedHeading;
        private bool turnCW;
        private bool landing = false;
        public PictureBox Airplane = new PictureBox();
        TextBox txtIDTitle;
        TextBox txtID;
        TextBox txtSpdTitle;
        TextBox txtSpd;
        public TableLayoutPanel tblPlaneInfo;
        TableLayoutPanel tblTurnDirection;
        TextBox txtAlt;
        TextBox txtAltTitle;
        TextBox txtHead;
        TextBox txtHeadTitle;
        Button btnSendCmd;
        RadioButton rbCW;
        RadioButton rbCCW;
        TextBox txtTurnTitle;
        public Label planeinfo = new Label();
        Bitmap bmp;
        Timer tm = new Timer();
        int x = 100;
        int y = 100;
        Form1 f;



        public void mkPlane(Form1 form, int startLoc)
        {
            f = form;
            //Assign starting location based on mouse click location
            if (startLoc == 1)
            {
                Airplane.Location = new Point(334, 10); //startLoc 1
                planeinfo.Location = new Point(359, 10);
                heading = 121;
            }
            else if (startLoc == 2)
            {
                Airplane.Location = new Point(f.ClientSize.Width-15, 0); //startLoc 2
                planeinfo.Location = new Point(f.ClientSize.Width+10, 0);
                heading = 253;
            }
            else if (startLoc == 3)
            {
                Airplane.Location = new Point(1040,f.ClientSize.Height); //startLoc 3
                planeinfo.Location = new Point(1065, f.ClientSize.Height);
                heading = 4;
            }
            else  //Plane is departing
            {
                Airplane.Location = new Point(820, 375); //startLoc 4
                planeinfo.Location = new Point(845, 375);
                heading = 220;
                control = 'D';
            }
            //Generate random airplane info
            if (string.IsNullOrEmpty(ID))
            {
                infoGenerator();
            }         
            //Generate and size bitmap
            bmp = new Bitmap("C:\\Users\\patri\\Source\\Repos\\CS363\\CS363_TeamP\\Resources\\planeIconSmall.png");
            int dpi = 96;
            bmp.SetResolution(dpi, dpi);
            //Assign airplane picture box attributes
            Airplane.ClientSize = new Size(25,25);
            Airplane.BackColor = Color.Transparent;
            Airplane.ForeColor = Color.Red;
            Airplane.Tag = ID;
            Airplane.Image = rotateImage(bmp, heading);
            Airplane.SizeMode = PictureBoxSizeMode.Zoom;
            //Generate eventHandler for clicking on Airplane
            Airplane.Click += new EventHandler(this.Airplane_Click);
            //Add Airplane to form1
            form.Controls.Add(Airplane);
            //Generate plane info label
            planeinfo.AutoSize = true;
            planeinfo.Text = String.Format("{0} {1} {2}" + Environment.NewLine + "{3} {4} {5}", Airplane.Tag, destAP, control, altitude, speed, heading);
            planeinfo.Size = new System.Drawing.Size(198, 60);
            planeinfo.Tag = ID;
            planeinfo.BackColor = Color.Transparent;
            planeinfo.ForeColor = Color.White;
            planeinfo.BorderStyle = BorderStyle.None;
            form.Controls.Add(planeinfo);
            //
            //Instantiate individual timers for each plane - Uncomment this section to restore individual timers
            //tm.Interval = 1000;
            //tm.Tick += new EventHandler(tm_Tick);
            //tm.Start();
            //
            //Synronize all planes to the form1 timer - Comment this section out if switching to individual timers
            form.timer1.Tick += new EventHandler(tm_Tick);
            
        }

        public void infoGenerator() 
        {
            //Variables needed for generating random info
            var random = new Random();
            var list = new List<string> { "N", "P", "DL", "AA", "NZ", "UA", "FX", "F9", "KL", "NK" };
            var APlist = new List<string> { "ORD", "LAX", "SFO", "ELP", "GFK", "JFK", "HNL", "BWI", "HOU", "SLC" };
            //Generate random Airline ID
            int index = random.Next(list.Count);
            ID = list[index];
            for (int i=ID.Length; i<6; i++)
            {
                ID += random.Next(9);
            }
            //Generate random destination Airport 
            if (control == 'D')
            {
                index = random.Next(APlist.Count);
                destAP = APlist[index];
            }
            else
            {
                destAP = "UML";
            }
            
            //Generate speed info - Outbound start at 20 and increase to 200, Inbound start at random amount and  follow speed limit
            if (control == 'D')  
            {
                speed = 20;
                expectedSpeed = 200;
            }
            else
            {
                speed = random.Next(150, 350);
                if (speed > 250)
                {
                    expectedSpeed = 250;
                }
                else
                {
                    expectedSpeed = speed;
                }
            }
            //Generate altitude info - Outbound start at 0 and increase to 100,  Inbound start at random amount and follow ceiling        
            if (control == 'D') 
            {
                altitude = 0;
                expectedAltitude = 100;
            }
            else
            {
                altitude = random.Next(80, 200);
                if(altitude > 100)
                {
                    expectedAltitude = 100;
                }
                else
                {
                    expectedAltitude = altitude;
                }
                
            }
            //Set expected heading
            if (control == 'D')
            {
                if (index < 5)
                {
                    expectedHeading = 240;
                    turnCW = true;
                }
                else
                {
                    expectedHeading = 94;
                    turnCW = false;
                }
            }
            else
            {
                expectedHeading = heading;
            }

        }
        
        
        //
        //rotateImage - Performs rotation of airplane icon based on current heading angle
        //
        private Bitmap rotateImage(Bitmap b, int angle)
        {
            int maxside = (int)(Math.Sqrt(b.Width * b.Width + b.Height * b.Height));
            Bitmap returnBitmap = new Bitmap(maxside, maxside);
            Graphics g = Graphics.FromImage(returnBitmap);

            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            g.DrawImage(b, new Point(0, 0));

            return returnBitmap;

        }

        //
        //Scale movement in X and Y coordinates depending on heading angle
        //
        private (double scaleX, double scaleY) vectorScale(int heading)
        {
            double scaleY = Math.Sin(heading * (Math.PI / 180));
            double scaleX = Math.Cos(heading * (Math.PI / 180));
            return (scaleX,scaleY);
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            
            //Curtail speed changes to 20kts per update
            if (expectedSpeed == speed || Math.Abs(expectedSpeed - speed) < 20)
            {
                speed = expectedSpeed;
            }
            else if (expectedSpeed - speed < 0)
            {
                speed = speed + Math.Max(expectedSpeed - speed, -20);
            }
            else
            {
                speed = speed + Math.Min(expectedSpeed - speed, 20);
            }
            //Curtail altitude changes to 1000ft (10) per update
            if (expectedAltitude == altitude || Math.Abs(expectedAltitude - altitude) < 10)
            {
                altitude = expectedAltitude;
            }
            else if (expectedAltitude - altitude < 0)
            {
                altitude = altitude + Math.Max(expectedAltitude - altitude, -10);
            }
            else
            {
                altitude = altitude + Math.Min(expectedAltitude - altitude, 10);
            }
            //Curtail heading changes to 20 deg per update   
            double landingAngle = (Math.Atan2(Airplane.Location.Y - 360 + 12, Airplane.Location.X - 850 + 12) * (180 / Math.PI));
            if (heading == 220 && ((landingAngle > -58 && landingAngle < -50 && altitude <= 50 && altitude >= 10) || landing == true))
            {
                double tx = 850 - Airplane.Location.X -12;
                double ty = 360 - Airplane.Location.Y -12;
                double l = Math.Sqrt(tx * tx + ty * ty);
                Airplane.Location = new Point(Airplane.Location.X + (int)(speed / 10 * (tx / l)), Airplane.Location.Y + (int)(speed / 10 * (ty / l)));  
                planeinfo.Location = new Point(planeinfo.Location.X + (int)(speed / 10 * (tx / l)), planeinfo.Location.Y + (int)(speed / 10 * (ty / l)));  //Use this to gauge speed difference - Uncomment when ready fro use
                landing = true;
            }
            else if (turnCW && heading != expectedHeading)
            {
                int tempHeading;
                landing = false;
                tempHeading = (heading + Math.Min((expectedHeading - heading+360)%360, 20)) % 360;
                if (tempHeading < 0)
                {
                    heading = tempHeading + 360;
                }
                else
                {
                    heading = tempHeading;
                }
            }
            else if (!turnCW && heading!= expectedHeading)
            {
                int tempHeading;
                landing = false;
                tempHeading = (heading - Math.Min(Math.Abs(expectedHeading - heading)%360,20)) % 360;
                if (tempHeading < 0)
                {
                    heading = tempHeading + 360;
                }
                else
                {
                    heading = tempHeading;
                }
                
            }
            //Rotate image to match heading
            (double scaleX, double scaleY) = vectorScale(heading);
            Airplane.Image = rotateImage(bmp, heading);
            //Update plane and info location with new information
            if (landing == false)
            {
                Airplane.Location = new Point(Airplane.Location.X + (int)(speed / 10 * scaleY), Airplane.Location.Y - (int)(speed / 10 * scaleX));
                planeinfo.Location = new Point(planeinfo.Location.X + (int)(speed / 10 * scaleY), planeinfo.Location.Y - (int)(speed / 10 * scaleX));
            }
            //Update plane info display with new info
            planeinfo.Text = String.Format("{0} {1} {2}\r\n{3} {4} {5}", Airplane.Tag, destAP, control, altitude, speed, heading);
            //When on approach incrementally decrease altitude and speed
            if (landing == true)
            {
                double distToRunway = (Math.Sqrt(Math.Pow(Airplane.Location.X - 850 + 12, 2) + Math.Pow(Airplane.Location.Y - 360 + 12, 2)));
                if (distToRunway < 15)
                {
                    f.Controls.Remove(Airplane);
                    f.Controls.Remove(planeinfo);
                    f.Controls.Remove(tblPlaneInfo);
                    f.InFlightList.Remove(this);
                    
                }
                else if (distToRunway < 55)
                {
                    expectedAltitude = 5;
                    if (expectedSpeed > 125)
                    {
                        expectedSpeed = 125;
                    }
                    
                }
                else if (distToRunway < 100)
                {
                    expectedAltitude = 10;
                    if (expectedSpeed > 150)
                    {
                        expectedSpeed = 150;
                    }
                }
                else if (distToRunway < 150)
                {
                    if (expectedAltitude > 30)
                    {
                        expectedAltitude = 30;
                    }
                    if (expectedSpeed > 170)
                    {
                        expectedSpeed = 170;
                    }
                }
                else if (distToRunway < 200)
                {
                    if (expectedAltitude > 40)
                    {
                        expectedAltitude = 40;
                    }
                    if (expectedSpeed > 200)
                    {
                        expectedSpeed = 200;
                    }
                   
                }
            }
            if (Airplane.Location.X <= 335 || Airplane.Location.X >= f.ClientSize.Width || Airplane.Location.Y <= 0 || Airplane.Location.Y >= f.ClientSize.Height)
            {
                f.Controls.Remove(Airplane);
                f.Controls.Remove(planeinfo);
                f.Controls.Remove(tblPlaneInfo);
                f.InFlightList.Remove(this);
            }

        }

        private void tableMaker()
        {
            //f.InitializeComponent();
            
            TextBox txtIDTitle = new System.Windows.Forms.TextBox();
            TextBox txtID = new System.Windows.Forms.TextBox();
            TextBox txtSpdTitle = new System.Windows.Forms.TextBox();
            txtSpd = new System.Windows.Forms.TextBox();
            tblPlaneInfo = new System.Windows.Forms.TableLayoutPanel();
            TableLayoutPanel tblTurnDirection = new System.Windows.Forms.TableLayoutPanel();
            txtAlt = new System.Windows.Forms.TextBox();
            TextBox txtAltTitle = new System.Windows.Forms.TextBox();
            txtHead = new System.Windows.Forms.TextBox();
            TextBox txtHeadTitle = new System.Windows.Forms.TextBox();
            btnSendCmd = new System.Windows.Forms.Button();
            rbCW = new System.Windows.Forms.RadioButton();
            rbCCW = new System.Windows.Forms.RadioButton();
            TextBox txtTurnTitle = new System.Windows.Forms.TextBox();
            tblPlaneInfo.SuspendLayout();
            f.SuspendLayout();
            // 
            // txtIDTitle
            // 
            txtIDTitle.Location = new System.Drawing.Point(3, 3);
            txtIDTitle.Name = "txtIDTitle";
            txtIDTitle.ReadOnly = true;
            txtIDTitle.Size = new System.Drawing.Size(75, 26);
            txtIDTitle.TabIndex = 1;
            txtIDTitle.Text = "ID";
            // 
            // txtID
            // 
            txtID.Location = new System.Drawing.Point(78, 3);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new System.Drawing.Size(132, 26);
            txtID.TabIndex = 2;
            txtID.Text = ID;
            // 
            // txtSpdTitle
            // 
            txtSpdTitle.Location = new System.Drawing.Point(3, 35);
            txtSpdTitle.Name = "txtSpdTitle";
            txtSpdTitle.ReadOnly = true;
            txtSpdTitle.Size = new System.Drawing.Size(75, 26);
            txtSpdTitle.TabIndex = 3;
            txtSpdTitle.Text = "Speed";
            // 
            // txtSpd
            // 
            txtSpd.Location = new System.Drawing.Point(78, 35);
            txtSpd.Name = "txtSpd";
            txtSpd.Size = new System.Drawing.Size(132, 26);
            txtSpd.TabIndex = 4;
            txtSpd.Text = speed.ToString();
            txtSpd.LostFocus += new EventHandler(this.txtSpd_LostFocus);
            // 
            // tblPlaneInfo
            // 
            tblPlaneInfo.ColumnCount = 2;
            tblPlaneInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tblPlaneInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tblPlaneInfo.Controls.Add(btnSendCmd, 1, 5);
            tblPlaneInfo.Controls.Add(txtTurnTitle, 0, 4);
            tblPlaneInfo.Controls.Add(tblTurnDirection, 1, 4); //FIXME: Add layoutTable with two radio buttons for CW and CCW turns.  Also, add "Turn Direction" title to first column!
            tblPlaneInfo.Controls.Add(txtHead, 1, 3);
            tblPlaneInfo.Controls.Add(txtAltTitle, 0, 2);
            tblPlaneInfo.Controls.Add(txtAlt, 1, 2);
            tblPlaneInfo.Controls.Add(txtSpd, 1, 1);
            tblPlaneInfo.Controls.Add(txtSpdTitle, 0, 1);
            tblPlaneInfo.Controls.Add(txtID, 1, 0);
            tblPlaneInfo.Controls.Add(txtIDTitle, 0, 0);
            tblPlaneInfo.Controls.Add(txtHeadTitle, 0, 3);
            tblPlaneInfo.Name = "tblPlaneInfo";
            tblPlaneInfo.RowCount = 6;
            tblPlaneInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tblPlaneInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tblPlaneInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tblPlaneInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tblPlaneInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tblPlaneInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tblPlaneInfo.Size = new System.Drawing.Size(250, 167); //Original 217,141
            tblPlaneInfo.Location = new System.Drawing.Point(40, f.ClientSize.Height - tblPlaneInfo.Bottom-10);
            tblPlaneInfo.TabIndex = 0;
            //
            //tblTurnDirection
            //
            tblTurnDirection.ColumnCount = 2;
            tblTurnDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tblTurnDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tblTurnDirection.Name = "tblTurnDirection";
            tblTurnDirection.RowCount = 1;
            tblTurnDirection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tblTurnDirection.Controls.Add(rbCW, 0, 0);
            tblTurnDirection.Controls.Add(rbCCW, 1, 0);
            tblTurnDirection.Location = new System.Drawing.Point(78, 131);
            tblTurnDirection.Size = new System.Drawing.Size(132, 26);
            //
            //rbCW
            //
            rbCW.Location = new System.Drawing.Point(1, 1);
            rbCW.Size = new System.Drawing.Size(50, 26);
            rbCW.Text = "CW";
            //
            //rbCCW
            //
            rbCCW.Location = new System.Drawing.Point(35, 1);
            rbCCW.Size = new System.Drawing.Size(50, 26);
            rbCCW.Text = "CCW";
            //
            //txtTurnTitle
            //
            txtTurnTitle.Location = new System.Drawing.Point(3, 133);
            txtTurnTitle.Name = "txtTurnTitle";
            txtTurnTitle.Text = "Turn Direction";
            txtTurnTitle.ReadOnly = true;
            txtTurnTitle.Size = new System.Drawing.Size(75, 26);
            // 
            // txtAlt
            // 
            txtAlt.Location = new System.Drawing.Point(78, 67);
            txtAlt.Name = "txtAlt";
            txtAlt.Size = new System.Drawing.Size(132, 26);
            txtAlt.TabIndex = 5;
            txtAlt.Text = altitude.ToString();
            txtAlt.LostFocus += new EventHandler(this.txtAlt_LostFocus);
            // 
            // txtAltTitle
            // 
            txtAltTitle.Location = new System.Drawing.Point(3, 67);
            txtAltTitle.Name = "txtAltTitle";
            txtAltTitle.ReadOnly = true;
            txtAltTitle.Size = new System.Drawing.Size(75, 26);
            txtAltTitle.TabIndex = 6;
            txtAltTitle.Text = "Altitude";
            // 
            // txtHead
            // 
            txtHead.Location = new System.Drawing.Point(78, 99);
            txtHead.Name = "txtHead";
            txtHead.Size = new System.Drawing.Size(132, 26);
            txtHead.TabIndex = 7;
            txtHead.Text = heading.ToString();
            txtHead.LostFocus += new EventHandler(this.txtHead_LostFocus);
            // 
            // txtHeadTitle
            // 
            txtHeadTitle.Location = new System.Drawing.Point(3, 99);
            txtHeadTitle.Name = "txtHeadTitle";
            txtHeadTitle.ReadOnly = true;
            txtHeadTitle.Size = new System.Drawing.Size(75, 26);
            txtHeadTitle.TabIndex = 8;
            txtHeadTitle.Text = "Heading";
            // 
            // btnSendCmd
            // 
            btnSendCmd.Location = new System.Drawing.Point(78, 163); //Originally 78, 131
            btnSendCmd.Name = "btnSendCmd";
            btnSendCmd.Size = new System.Drawing.Size(132, 26);
            btnSendCmd.TabIndex = 1;
            btnSendCmd.Text = "Send Command";
            btnSendCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnSendCmd.UseVisualStyleBackColor = true;
            btnSendCmd.Click += new EventHandler((sender, e) => btnSendCmd_Click(sender, e, txtHead.Text, txtSpd.Text, txtAlt.Text, rbCW.Checked, rbCCW.Checked));
            //
            // Add table control
            //
            f.Controls.Add(tblPlaneInfo);
            tblPlaneInfo.ResumeLayout(false);
            tblPlaneInfo.PerformLayout();
            f.ResumeLayout(false);
            f.PerformLayout();
        }
        //
        //Airplane_Click - Changes blank control indicators to A (only incomming aircraft have blank control indicators), shows border on selected aircraft, and displays directive input table
        //
        public void Airplane_Click(object sender, EventArgs e)
        {
            if (control == ' ')
            {
                control = 'A';
            }
            PictureBox pic = sender as PictureBox;
            foreach (Control item in f.Controls.OfType<TableLayoutPanel>())
            {
                if (item.Name == "tblPlaneInfo")
                {
                    f.Controls.Remove(item);
                    item.Dispose();
                }

            }
            foreach (PictureBox item in f.Controls.OfType<PictureBox>())
            {
                item.BorderStyle = BorderStyle.None;
            }
            Airplane.BorderStyle = BorderStyle.Fixed3D;
            tableMaker();
        }
        //
        //btnSendCmd_Click - Commits directives to expected heading, speed and altitude.  Also, closes input table and removes icon border.
        //
        public void btnSendCmd_Click(object sender, EventArgs e, string newHeading, string newSpeed, string newAltitude, bool CW, bool CCW)
        {
            if(!CW && !CCW && (Int32.Parse(newHeading) != heading))
            {
                MessageBox.Show("You must select a turn direction!");
                return;
            }
            foreach (Control item in f.Controls.OfType<TableLayoutPanel>())
            {
                if (item.Name == "tblPlaneInfo")
                {
                    f.Controls.Remove(item);
                    item.Dispose();
                }

            }
            Airplane.BorderStyle = BorderStyle.None;
            expectedSpeed = Int32.Parse(newSpeed);
            expectedAltitude = Int32.Parse(newAltitude);
            if (CW)
            {
                turnCW = true;
            }
            else
            {
                turnCW = false;
            }
            expectedHeading = Int32.Parse(newHeading);
        }
        //
        //txtInfo_Changed - Ensures no textBoxes are left blank
        //
        private void txtAlt_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAlt.Text))
            {
                txtAlt.Text = altitude.ToString();
                MessageBox.Show("Altitude cannot be blank!");
            }
        }
        //
        //txtHead_Changed
        //
        private void txtHead_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtHead.Text))
            {
                txtHead.Text = heading.ToString();
                MessageBox.Show("Heading cannot be blank!");
            }
        }
        //
        //txtSpd_Changed
        //
        private void txtSpd_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSpd.Text))
            {
                txtSpd.Text = speed.ToString();
                MessageBox.Show("Speed cannot be blank!");
            }
        }
    }


}
