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
    class Plane
    {
        public int speed;
        public string ID;
        public char control;
        public int altitude;
        public int heading;
        PictureBox Airplane = new PictureBox();
        Bitmap bmp;

        public void mkPlane(Form1 form)
        {
            bmp = (Bitmap)Bitmap.FromFile(@"plane.png");
            int dpi = 96;
            using (Graphics G = Airplane.CreateGraphics()) dpi = (int)G.DpiX;
            bmp.SetResolution(dpi, dpi);
            Airplane.ClientSize = bmp.Size;
            Airplane.BackColor = System.Drawing.Color.Transparent;
            Airplane.Tag = "plane";
        }
            
    }

    
    }
