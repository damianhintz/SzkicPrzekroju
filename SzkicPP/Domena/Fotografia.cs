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
    /// Fotografia
    /// </summary>
    public class Fotografia : ElementSzkicu
    {
        protected string _numer = "";

        /// <summary>
        /// Numer fotografii
        /// </summary>
        [Category("Atrybuty"), XmlAttribute("numer")]
        public string Numer
        {
            get { return _numer; }
            set
            {
                _numer = value;
                _zmieniony = true;
            }
        }

        protected float _rotacja = 90;

        /// <summary>
        /// Kierunek fotografii
        /// </summary>
        [Category("Atrybuty"), XmlAttribute("rotacja")]
        public float Rotacja
        {
            get { return _rotacja; }
            set
            {
                _rotacja = value;
                _zmieniony = true;
            }
        }

        protected StringFormat _format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

        /// <summary>
        /// Konstruktor fotografii
        /// </summary>
        /// <param name="numerFotografii"></param>
        public Fotografia(string numerFotografii)
        {
            _numer = numerFotografii;

            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Konstruktor fotografii (domyślny)
        /// </summary>
        public Fotografia()
            : this("")
        {
        }

        /// <summary>
        /// Rysuj fotografię
        /// </summary>
        /// <param name="g"></param>
        public override void Rysuj(Graphics g)
        {
            string tekst = string.Format("Fot. {0}", _numer);

            if (_selected)
                g.DrawString(tekst, StylSzkicu.FotoFont, StylSzkicu.SelectedBrush, _x, _y, _format);
            else
                g.DrawString(tekst, StylSzkicu.FotoFont, StylSzkicu.TekstBrush, _x, _y, _format);

            int rozmiar = (int)StylSzkicu.FotoFont.Size;

            Kierunek.Rysuj(g, _x - rozmiar * 2, _y + rozmiar,
                StylSzkicu.FotoColor,
                rozmiar, _rotacja, true, true, false);
        }

        public override string ToString()
        {
            return string.Format("Fotografia - {0}", _numer);
        }
    }
}
