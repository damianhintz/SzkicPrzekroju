using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SzkicPP.Model
{
    /// <summary>
    /// Droga
    /// </summary>
    public class Droga : Teren
    {
        protected StringFormat _format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

        /// <summary>
        /// Konstruktor drogi (domyślny)
        /// </summary>
        public Droga() : this(-1, -1) { }

        /// <summary>
        /// Konstruktor drogi
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Droga(int start, int end)
            : base(start, end)
        {
            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Rysowanie drogi na szkicu
        /// </summary>
        /// <param name="g"></param>
        /// <param name="szkic"></param>
        public override void Rysuj(Graphics g, SzkicPrzekroju szkic)
        {
            if (!AktualizujPikiety(szkic)) return;
            
            int x1 = szkic.Obszar.Left, y1 = _startPikieta.Y;
            int x2 = szkic.Obszar.Right, y2 = _endPikieta.Y;

            int w = x2 - x1; //szerokość
            int h = y2 - y1; //wysokość
            int y = (y1 + y2) / 2; //środek wysokości

            if (h < 0) h = -h;
            if (h == 0) h = 2;

            g.DrawString(KodTerenu.OpisDrogi(_endPikieta.KodFormyTerenu), 
                StylSzkicu.TekstFont, StylSzkicu.TekstBrush, x1, y, _format);

            //Rysowanie przerywanych linii drogi
            for (int x = x1; x < x2; x += h / 2) //przesuwamy się w prawo co jedną wysokość
            {
                int dx = x + h / 4;
                if (dx > x2) dx = x2;

                g.DrawLine(StylSzkicu.TekstPen, x, y1, dx, y1);
                g.DrawLine(StylSzkicu.TekstPen, x, y2, dx, y2);
            }
        }
    }
}
