using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SzkicPrzekroju.Domena
{
    /// <summary>
    /// Operacja na wektorach/punktach
    /// </summary>
    public class Wektor
    {
        public static PointD BuildWektor(PointD p1, PointD p2)
        {
            return new PointD(p2.X - p1.X, p2.Y - p1.Y);
        }

        public static double LenWektor(PointD v)
        {
            return Wektor.PointDistance(v.X, v.Y, 0, 0);
        }

        public static void NormWektor(PointD v)
        {
            double len = LenWektor(v);
            v.X /= len;
            v.Y /= len;
        }

        /* w lewo czy w prawo */
        public static double PointDirection(PointD pi, PointD pj, PointD pk)
        {
            PointD a, b;

            a = PointSubstract(pk, pi);
            b = PointSubstract(pj, pi);

            return PointMultiply(a, b);
        }

        /// <summary>
        /// Odległość punktów
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double PointDistance(PointD p1, PointD p2)
        {
            return Wektor.PointDistance(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// Odległość punktów
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double PointDistance(double x1, double y1, double x2, double y2)
        {
            return (double)Math.Sqrt(Wektor.PointDistance2(x1, y1, x2, y2));
        }

        public static double PointDistance2(double x1, double y1, double x2, double y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        public static double PointDistance2(PointD p1, PointD p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }

        /// <summary>
        /// Iloczyn skalarny
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double PointMultiply(double x1, double y1, double x2, double y2)
        {
            return x1 * y2 - x2 * y2;
        }

        /// <summary>
        /// Iloczyn skalarny p1->x * p2->y - p2->x * p1->y;
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double PointMultiply(PointD p1, PointD p2)
        {
            return p1.X * p2.Y - p2.X * p1.Y;
        }

        /// <summary>
        /// p3 = p1 - p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>p1 - p2</returns>
        public static PointD PointSubstract(PointD p1, PointD p2)
        {
            PointD p3 = new PointD();
            p3.X = p1.X - p2.X;
            p3.Y = p1.Y - p2.Y;
            return p3;
        }

        /// <summary>
        /// p3 = p1 - p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        public static void PointSubstract(PointD p1, PointD p2, PointD p3)
        {
            p3.X = p1.X - p2.X;
            p3.Y = p1.Y - p2.Y;
        }

        public static void PointsRange(PointD[] points, out double minX, out double minY, out double maxX, out double maxY, double margin)
        {
            minX = points[0].X;
            minY = points[0].Y;
            maxX = points[0].X;
            maxY = points[0].Y;

            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].X < minX)
                    minX = points[i].X;

                if (points[i].Y < minY)
                    minY = points[i].Y;

                if (points[i].X > maxX)
                    maxX = points[i].X;

                if (points[i].Y > maxY)
                    maxY = points[i].Y;
            }

            minX -= margin;
            minY -= margin;
            maxX += margin;
            maxY += margin;
        }

        public static PointD[] PointsScaled(PointD[] points, double windowSize, double windowMargin)
        {
            double minX, minY, maxX, maxY;

            PointsRange(points, out minX, out minY, out maxX, out maxY, windowMargin);

            double skala = (maxX - minX);

            if (maxY - minY > skala) skala = maxY - minY;

            PointD[] pointsScaled = new PointD[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                double dx = points[i].X - minX;
                double dy = points[i].Y - minY;

                pointsScaled[i].X = (dx * windowSize) / (skala);
                pointsScaled[i].Y = windowSize - (dy * windowSize) / (skala);
            }

            return pointsScaled;
        }

        public static bool RegresjaProstej(PointD[] punkty, ref double a)
        {
            double x = 1;
            return RegresjaWektoraProstej(punkty, ref x, ref a);
        }

        public static bool RegresjaWektoraProstej(PointD[] punkty, ref double x, ref double y)
        {
            if (punkty.Length < 2) return false;

            List<double> aList = new List<double>();

            double x1 = punkty[0].X;
            double y1 = punkty[0].Y;

            double xMin = x1, xMax = x1;

            for (int i = 1; i < punkty.Length; i++)
            {
                double x2 = punkty[i].X;
                double y2 = punkty[i].Y;

                if (x2 < xMin) xMin = x2;
                if (x2 > xMax) xMax = x2;

                if (Math.Abs(x2 - x1) < 0.001) continue;

                double ai = (y2 - y1) / (x2 - x1);

                aList.Add(ai);
            }

            if (Math.Abs(xMax - xMin) < 0.001) //punkty w osi x
            {
                x = 0; //x < 1 oznacza prostą równoległą z osią Y
                y = 0; //y przyjmuję dowolną wartość
                return true;
            }

            if (aList.Count < 1) return false;

            aList.Sort();

            int i1 = (aList.Count - 1) / 2;
            int i2 = (aList.Count) / 2;

            double aMid = (aList[i1] + aList[i2]) / 2;

            /*
            double aMin = aList[0];
            double aMax = aList[aList.Count - 1];

            int n = 0;
            double a = 0;
            double eMin = (aMid - aMin) / 2;
            double eMax = (aMax - aMid) / 2;
            double aLeft = aMid - eMin;
            double aRight = aMid + eMax;

            for (int i = 0; i < aList.Count; i++)
            {
                double ai = aList[i];
                
                if (ai > aLeft && ai < aRight)
                {
                    n++;
                    a += ai;
                }
            }

            a /= n;
            */

            x = 1; //b = 0
            y = aMid; //y = a * x + b

            return true;
        }

        public static bool RegresjaWektoraProstej(PointD[] punkty, PointD v)
        {
            //double x = 0, y = 0;
            return RegresjaWektoraProstej(punkty, ref v.X, ref v.Y);
        }

        private static void RegresjaLiniowaProstej(PointD[] punkty, ref double a, ref double b)
        {
            double xySuma = 0, xSuma = 0, ySuma = 0, x2Suma = 0;
            int n = punkty.Length;

            for (int i = 0; i < n; i++)
            {
                xySuma += punkty[i].X * punkty[i].Y;
                xSuma += punkty[i].X;
                ySuma += punkty[i].Y;
                x2Suma += punkty[i].X * punkty[i].X;
            }

            a = (n * xySuma - xSuma * ySuma) / (n * x2Suma - xSuma * xSuma);
            b = (ySuma - a * xSuma) / n;
        }
    }
}
