using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SzkicPP.Model
{
    public struct PointD
    {
        public double X;
        public double Y;

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("{0:F2}; {1:F2}", X, Y);
        }
    }
}
