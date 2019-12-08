using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CS363_TeamP
{
    public class Bullet
    {
        public int direction;
        public int speed = 20;
        double scaleX, scaleY;
        public PictureBox bullet = new PictureBox();
        public Timer tm = new Timer();
        Form1 f;

        public void mkBullet(Form1 form)
        {
            f = form;
            bullet.BackColor = System.Drawing.Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Location = new System.Drawing.Point(850, 360);
            bullet.BringToFront();
            form.Controls.Add(bullet);
            (scaleX, scaleY) = vectorScale(direction);
            tm.Interval = 16;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
        }
        public (double scaledX, double scaledY) vectorScale(int heading)
        {
            double scaledY = Math.Sin(heading * (Math.PI / 180));
            double scaledX = Math.Cos(heading * (Math.PI / 180));
            return (scaledX, scaledY);
        }
        public void tm_Tick(object sender, EventArgs e)
        {
            bullet.Location = new Point(bullet.Location.X + (int)(speed * scaleX), bullet.Location.Y + (int)(speed * scaleY));

            if (bullet.Location.X <= 335 || bullet.Location.X >= f.ClientSize.Width || bullet.Location.Y <= 0 || bullet.Location.Y >= f.ClientSize.Height)
            {
                tm.Stop();
                tm.Dispose();
                bullet.Dispose();
                tm = null;
                bullet = null;
                f.Bullets.Remove(this);
            }
        }

    }
}
