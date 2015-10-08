using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace SzkicPrzekroju.Domena
{
    /// <summary>
    /// Pikieta
    /// </summary>
    public class Pikieta : ElementSzkicu
    {
        //protected int _lp; //1 np. 1
        //protected string _nazwaCieku; //2 np. Pasłęka
        
        //3. Numer obiekt
        protected string _numerPrzekroju = ""; //4 i punktu pomiarowego np. 240.01

        /// <summary>
        /// Numer punktu
        /// </summary>
        [Category("Atrybuty"), ReadOnly(true), XmlAttribute("numer")]
        public string Numer
        {
            get { return _numerPrzekroju; }
            set
            {
                _numerPrzekroju = value;
                //_zmieniony = true;
            }
        }

        //protected string _typObiektu; //5 np. przekrój, śluza, most, przepust
        //protected string _kodPunktu; //6 np. 1, 7, 12, ZWW
        
        protected string _kodTerenu = ""; //7 np. pusty (pierwszy), K01-K05, T01-T14

        /// <summary>
        /// Kod formy terenu
        /// </summary>
        [Category("Atrybuty"), ReadOnly(false), XmlAttribute("kodTerenu"), DisplayName("Kod formy terenu")]
        public string KodFormyTerenu
        {
            get { return _kodTerenu; }
            set
            {
                _kodTerenu = value;
                _zmieniony = true;
            }
        }

        //8. Kilometraż
        //9. Współrzędna X np. 640647.49	587558.00	147.86
        //10. Współrzędna Y
        //11. Z - rzędna m n.p.m.]
        //...
        //15. Data pomiaru
        //16. Numer fotografii np. 243a.jpg; 243b.jpg
        //17. Administrator obiektu
        
        protected PointD _wsp;

        /// <summary>
        /// Współrzędne pikiety
        /// </summary>
        [Category("Atrybuty"), ReadOnly(true)]
        public PointD Punkt { get { return _wsp; } set { _wsp = value; } }

        protected double _rzedna = 0;

        /// <summary>
        /// Rzędna
        /// </summary>
        [Category("Atrybuty"), ReadOnly(false), DisplayName("Rzędna")]
        public double Rzedna
        {
            get { return _rzedna; }
            set
            {
                _rzedna = value;
                _zmieniony = true;
            }
        }

        [Category("Opis pikiety")]
        public string Opis { get { return KodTerenu.OpisKodu(_kodTerenu); } }

        protected StringFormat _format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

        /// <summary>
        /// Konstruktor pikiety
        /// </summary>
        /// <param name="numerPunktu"></param>
        /// <param name="kodTerenu"></param>
        /// <param name="wspX"></param>
        /// <param name="wspY"></param>
        public Pikieta(string numerPunktu, string kodTerenu, double wspX, double wspY)
        {
            _numerPrzekroju = numerPunktu;
            _kodTerenu = kodTerenu;
            _wsp.X = wspX;
            _wsp.Y = wspY;

            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Far;
        }

        /// <summary>
        /// Konstruktor pikiety
        /// </summary>
        /// <param name="numerPunktu"></param>
        /// <param name="kodTerenu"></param>
        /// <param name="wsp"></param>
        public Pikieta(string numerPunktu, string kodTerenu, PointF wsp)
            : this(numerPunktu, kodTerenu, wsp.X, wsp.Y)
        {
        }

        /// <summary>
        /// Konstruktor pikiety (domyślny)
        /// </summary>
        public Pikieta()
            : this("", "", 0, 0)
        {
        }

        /// <summary>
        /// Rysowanie pikiety
        /// </summary>
        /// <param name="g"></param>
        public override void Rysuj(Graphics g)
        {
            Font font = StylSzkicu.PikietaFont;

            if (!KodTerenu.IsValid(_kodTerenu)) font = StylSzkicu.InvalidFont;

            string tekst = string.Format("{0} {1}", _numerPrzekroju,
                string.IsNullOrEmpty(_kodTerenu) ? "" : "(" + _kodTerenu + ")");

            int size = 4;
            int x = _x - size / 2;
            int y = _y - size / 2;

            if (_selected)
                g.DrawString(tekst, font, StylSzkicu.SelectedBrush, _x + size * 2, y, _format);
            else
                g.DrawString(tekst, font, StylSzkicu.PikietaBrush, _x + size * 2, y, _format);

            g.FillEllipse(new SolidBrush(Color.DarkGreen), x, y, size, size); //wypełnienie
            g.DrawEllipse(StylSzkicu.TekstPen, x, y, size, size); //obwódka punktu
        }

        /// <summary>
        /// Czy jest korytem
        /// </summary>
        /// <returns></returns>
        public bool JestKorytem()
        {
            return KodTerenu.IsKoryto(_kodTerenu);
        }

        /// <summary>
        /// Czy jest drogą
        /// </summary>
        /// <returns></returns>
        public bool JestDroga()
        {
            return KodTerenu.IsDroga(_kodTerenu);
        }

        /// <summary>
        /// Przesuń pikietę, tylko do góry lub dołu
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public override bool MoveBy(int dx, int dy)
        {
            return base.MoveBy(dx, dy);
        }

        public override string ToString()
        {
            return string.Format("Pikieta - {0} [{1} m n.p.m] {2}", _numerPrzekroju, _rzedna, Opis);
        }
    }
}
