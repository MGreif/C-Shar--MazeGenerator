using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C_Sharp_MazeGenerator
{
    public class Cell
    {
        public int width, height;
        public bool active, used;
        public PictureBox p = new PictureBox();
        MainForm mf;
        public Point Location;
        public Cell(Point loc, int w, Form f)
        {
            Location = loc;
            width = w;
            height = w;
            p.Size = new Size(width,height);
            p.Location = loc;
            p.Visible = true;
            p.BackColor = Color.Gray;
            f.Controls.Add(p);
            drawLines(f);
        }
        public void drawLines(Form f)
        {
            Graphics g = f.CreateGraphics();
            Pen p = new Pen(Color.Black, 50);
            g.DrawLine(p, new Point(0, 0), new Point(500, 800));
        }
        public void setActive()
        {
            active = true;
            p.BackColor = Color.Red;
        }
        public void setUsed()
        {
            active = false;
            used = true;
            p.BackColor = Color.Blue;
        }
    }
}
