using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CS363_TeamP
{
    class Weather
    {
        public int pointX;
        public int pointY;
        Form1 f;
        


        public void mkCloud(Form1 form, Graphics a)
        {
            f = form;
            Graphics g = a;
            g.DrawImage(Properties.Resources.Cloud1, this.pointX, this.pointY);
            form.timer1.Tick += new EventHandler(tm_Tick);
        }

        public void tm_Tick(object sender, EventArgs e)
        {
            pointX += 5;
            pointY += 5;
            f.Invalidate();
        }
    }
}
