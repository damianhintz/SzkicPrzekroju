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
    /// Tekst
    /// </summary>
    public class Tekst : ElementSzkicu
    {
        protected string _tekst;

        /// <summary>
        /// Wartość tekstu
        /// </summary>
        [Category("Atrybuty"), ReadOnly(false), XmlAttribute("value")]
        public string Nazwa
        {
            get { return _tekst; }
            set
            {
                _tekst = value;
                _zmieniony = true;
            }
        }

        protected StringFormat _format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

        /// <summary>
        /// Konstuktor tekstu (domyślny)
        /// </summary>
        public Tekst() : this("Tekst") { }

        /// <summary>
        /// Konstuktor tekstu
        /// </summary>
        /// <param name="tekst"></param>
        public Tekst(string tekst) : this(tekst, 0, 0) { }

        /// <summary>
        /// Konstuktor tekstu
        /// </summary>
        /// <param name="tekst"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Tekst(string tekst, int x, int y)
        {
            _tekst = tekst;
            _x = x;
            _y = y;
            _format.Alignment = StringAlignment.Near;
            _format.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Rysuj tekst
        /// </summary>
        /// <param name="g"></param>
        public override void Rysuj(Graphics g)
        {
            if (_selected)
                g.DrawString(_tekst, StylSzkicu.TekstFont, StylSzkicu.SelectedBrush, _x, _y, _format);
            else
                g.DrawString(_tekst, StylSzkicu.TekstFont, StylSzkicu.TekstBrush, _x, _y, _format);
        }

        public override string ToString()
        {
            return string.Format("Tekst - {0}", _tekst);
        }
    }
}
