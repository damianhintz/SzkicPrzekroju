using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace SzkicPP.Model
{
    /// <summary>
    /// Skarpa
    /// </summary>
    public class Skarpa : Teren
    {
        protected bool _naBrzegu = false;

        /// <summary>
        /// Czy skarpa jest brzegiem
        /// </summary>
        [XmlAttribute("brzeg"), Browsable(false)]
        public bool NaBrzegu { get { return _naBrzegu; } set { _naBrzegu = value; } }

        /// <summary>
        /// Konstruktor skarpy (domyślny)
        /// </summary>
        public Skarpa() : this(-1, -1, false) { }

        /// <summary>
        /// Konstruktor skarpy
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="naBrzegu"></param>
        public Skarpa(int start, int end, bool naBrzegu)
            : base(start, end)
        {
            _naBrzegu = naBrzegu;
        }

        /// <summary>
        /// Rysuj skarpę na szkicu
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

            //Rysowanie linii pionowych |
            for (int x = x1; x < x2; x += h) //przesuwamy się w prawo co jedną wysokość
            {
                g.DrawLine(StylSzkicu.TekstPen, x, y1, x, y2);
            }

            //Rysowanie linii pionowych o połowe niższych |
            for (int x = x1 + h / 2; x < x2; x += h) //przesuwamy się w prawo co jedną wysokość (od połowy wysokości)
            {
                g.DrawLine(StylSzkicu.TekstPen, x, y1, x, y);

            }

            //Rysowanie linii poziomych
            for (int x = x1; x < x2; x += h / 2) //przesuwamy się w prawo co jedną wysokość
            {
                int dx = x + h / 3;
                if (dx > x2) dx = x2;
                
                g.DrawLine(StylSzkicu.TekstPen, x, y1, dx, y1);

                //Dla brzegu pomijamy rysowanie drugiej linii
                if (!_naBrzegu) g.DrawLine(StylSzkicu.TekstPen, x, y2, dx, y2);
            }
        }

        public override string ToString()
        {
            string[] cols = GetType().ToString().Split('.');

            string type = cols[cols.Length - 1];
            if (_naBrzegu) type = "Brzeg";

            return string.Format("{0}", type);
        }
    }
}
