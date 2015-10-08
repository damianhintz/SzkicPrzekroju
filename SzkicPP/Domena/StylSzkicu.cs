using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SzkicPrzekroju.Domena
{
    /// <summary>
    /// Styl szkicu
    /// </summary>
    public class StylSzkicu
    {
        public static Color Budynekcolor = Properties.Settings.Default.BudynekColor;
        public static Brush BudynekBrush = new SolidBrush(Color.White);
        public static Pen BudynekPen = new Pen(Properties.Settings.Default.BudynekColor, 2);
        public static Pen ZabudowaPen = new Pen(Color.Black, 2);

        public static Font InvalidFont = Properties.Settings.Default.InvalidFont;

        public static Pen TekstPen = new Pen(Properties.Settings.Default.DefaultColor, 1);
        public static Pen SzkicPen = new Pen(Properties.Settings.Default.SzkicColor, 2);
        public static Color SzkicColor = Properties.Settings.Default.SzkicColor;
        public static Size SzkicSize = Properties.Settings.Default.SzkicSize;

        public static Font TekstFont = Properties.Settings.Default.DefaultFont;
        public static Brush TekstBrush = new SolidBrush(Properties.Settings.Default.DefaultColor);

        public static Font PikietaFont = Properties.Settings.Default.PikietaFont;
        public static Brush PikietaBrush = new SolidBrush(Properties.Settings.Default.DefaultColor);
        public static int PikietyWidth = Properties.Settings.Default.PikietyWidth;

        public static Color FotoColor = Properties.Settings.Default.FotoColor;
        public static Font FotoFont = Properties.Settings.Default.FotoFont;
        public static Brush FotoBrush = new SolidBrush(Properties.Settings.Default.FotoColor);

        public static Brush SelectedBrush = new SolidBrush(Color.Red);
        public static Pen SelectedPen = new Pen(Color.Red, 2);

        public static void DrawDart(Graphics g, Point punkt, Color kolor, int rozmiar, float angle, bool fill)
        {
            DrawDart(g, punkt.X, punkt.Y, kolor, rozmiar, angle, fill);
        }

        public static void DrawDart(Graphics g, int x, int y, Color kolor, int rozmiar, float angle, bool fill)
        {
            Kierunek.Rysuj(g, x, y, kolor, rozmiar, angle, fill, false, true);
        }

        public static void DrawDart(Graphics g, int x, int y, Color kolor, int rozmiar, float angle, bool fill, bool border)
        {
            Kierunek.Rysuj(g, x, y, kolor, rozmiar, angle, fill, border, true);
        }
    }
}
