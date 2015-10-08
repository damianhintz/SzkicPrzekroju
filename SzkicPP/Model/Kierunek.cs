using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SzkicPP.Model
{
    /// <summary>
    /// Kierunek
    /// </summary>
    public class Kierunek
    {
        /// <summary>
        /// Rysujemy strzałkę w kształcie trójkąta
        /// a
        ///      c
        /// b
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="kolor">Kolor strzałki</param>
        /// <param name="rozmiar">Rozmiar strzałki (długość podstawy strzałki)</param>
        /// <param name="angle">Kierunek strzałki</param>
        /// <param name="fill">Wypełnienie strzałki</param>
        /// <param name="border">Rysuj obwódkę strzałki</param>
        public static void Rysuj(Graphics g, int x, int y, Color kolor, int rozmiar, float angle, bool fill, bool border, bool dart)
        {
            //Umieszczamy punkt w środku układu
            Point a = new Point(0, (-rozmiar / 2)); //> lewy, górny
            Point b = new Point(0, (rozmiar / 2)); //> lewy, dolny
            Point c = new Point((int)(rozmiar * 1.75), 0); //> prawy, środkowy

            if (!dart)
            {
                a = new Point((int)(rozmiar * 1.75), (-rozmiar / 2)); //> lewy, górny
                b = new Point((int)(rozmiar * 1.75), (rozmiar / 2)); //> lewy, dolny
                c = new Point(0, 0); //> prawy, środkowy
            }

            Point[] punkty = new Point[] { a, b, c };

            //Tworzymy macierze obrotu i przesunięcia dla punktów trójkąta
            g.TranslateTransform(x, y);
            g.RotateTransform(-angle);
            //Obracamy i przesuwamy punkty w odpowiednie miejsce
            g.Transform.TransformPoints(punkty);
            //Resetujemy transformację
            g.ResetTransform();

            if (fill)
            {
                Brush brush = new SolidBrush(kolor);
                g.FillPolygon(brush, punkty);
            }
            else
            {
                Pen pen = new Pen(kolor);
                g.DrawPolygon(pen, punkty);
            }

            if (border) g.DrawPolygon(new Pen(Color.Black, 0.5f), punkty);
        }

        public static void RysujStrzalka(Graphics g, int x, int y, float angle)
        {
            int r = 6;
            //Umieszczamy punkt w środku układu
            Point a1 = new Point(0, 0); //> lewy, górny
            Point a2 = new Point(0, r * 8); //> lewy, dolny
            Point a3 = new Point(r, r * 4); //> prawy, środkowy
            Point a4 = new Point(-r, r * 4); //> prawy, środkowy

            Point a5 = new Point(-r / 2, 0); //> prawy, środkowy
            Point a6 = new Point(-r / 2, r * 3); //> prawy, środkowy
            Point a7 = new Point(r / 2, 0); //> prawy, środkowy
            Point a8 = new Point(r / 2, r * 3); //> prawy, środkowy

            Point a9 = new Point(r, 3 * r - r / 2); //> prawy, środkowy
            Point a10 = new Point(r, r * 3); //> prawy, środkowy
            Point a11 = new Point(-r, r / 2); //> prawy, środkowy
            Point a12 = new Point(-r, 0); //> prawy, środkowy

            Point[] p = new Point[] { a1, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12};

            for(int i = 0 ; i < p.Length;i++)
                p[i].Y -= r * 4;

            //Tworzymy macierze obrotu i przesunięcia dla punktów trójkąta
            g.TranslateTransform(x, y);
            angle += 180;
            g.RotateTransform(-angle);
            //Obracamy i przesuwamy punkty w odpowiednie miejsce
            g.Transform.TransformPoints(p);
            //Resetujemy transformację
            g.ResetTransform();

            //if (border) g.DrawPolygon(new Pen(Color.Black, 0.5f), punkty);
            Pen pen = new Pen(Color.Black);

            g.DrawLine(pen, p[1], p[2]);
            g.DrawLine(pen, p[2], p[3]);
            g.DrawLine(pen, p[3], p[4]);
            g.DrawLine(pen, p[5], p[6]);
            g.DrawLine(pen, p[7], p[8]);

            g.DrawLine(pen, p[9], p[12]);
            g.DrawLine(pen, p[10], p[11]);
        }
    }
}
