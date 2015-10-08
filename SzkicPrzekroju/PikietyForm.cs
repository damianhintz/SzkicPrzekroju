using System;
using System.Drawing;
using System.Windows.Forms;
using SzkicPrzekroju.Domena;
using SzkicPrzekroju.Domena.Matematyka;

namespace SzkicPrzekroju
{
    public partial class PikietyForm : Form
    {
        public PikietyForm(PointD[] points)
        {
            InitializeComponent();

            int size = mPictureBox.Width;

            PointD[] pointsScaled = Wektor.PointsScaled(points, size, 1);

            Bitmap bitmap = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Pen greenPen = new Pen(Color.Green);

                mPictureBox.Image = bitmap;

                RysujProsta(g, points);
                //RysujProsta(g, pointsScaled);

                for (int i = 0; i < points.Length; i++)
                {
                    Point p = new Point((int)pointsScaled[i].X, (int)pointsScaled[i].Y);

                    g.DrawEllipse(greenPen, p.X - 2, p.Y - 2, 4, 4);
                    g.DrawString((i + 1).ToString(), StylSzkicu.PikietaFont, StylSzkicu.PikietaBrush, p);
                }
            }
        }

        void RysujProsta(Graphics g, PointD[] punkty)
        {
            double a = 0;

            if (!Wektor.RegresjaProstej(punkty, ref a))
                return;

            int size = mPictureBox.Width;

            double x1 = 5;
            double y1 = (size - a * x1);

            double x2 = size - 5;
            double y2 = (size - a * x2);

            if (y2 > size)
            {
                x1 = size;
                y1 = (size - a * x1) - size;

                x2 = 0;
                y2 = (size - a * x2) - size;
            }

            Pen blackPen = new Pen(Color.Black);
            g.DrawLine(blackPen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        private void WidokForm_Load(object sender, EventArgs e)
        {

        }
    }
}
