using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace SzkicPrzekroju.Domena
{
    /// <summary>
    /// Element szkicu
    /// </summary>
    public abstract class ElementSzkicu
    {
        protected bool _rysuj = true;

        /// <summary>
        /// Czy rysować obiekt na szkicu
        /// </summary>
        [Category("Atrybuty"), XmlAttribute("rysuj"), Browsable(false), DisplayName("Rysuj na szkicu")]
        public bool RysujNaSzkicu
        {
            get { return _rysuj; }
            set
            {
                _rysuj = value;
                _zmieniony = true;
            }
        }

        protected bool _selected = false;

        protected bool _zmieniony = false;

        public void WymagaZapisu(bool zmieniony)
        {
            _zmieniony = zmieniony;
        }

        /// <summary>
        /// Czy obiekt został zmieniony
        /// </summary>
        [Browsable(false)]
        public bool Zmieniony { get { return _zmieniony; } }

        protected int _x;

        /// <summary>
        /// Położenie na szkicu
        /// </summary>
        [Browsable(false)]
        public int X { get { return _x; } set { _x = value; } }

        protected int _y;

        /// <summary>
        /// Położenie na szkicu
        /// </summary>
        [Browsable(false)]
        public int Y { get { return _y; } set { _y = value; } }

        /// <summary>
        /// Rysuj element szkicu
        /// </summary>
        /// <param name="g"></param>
        public abstract void Rysuj(Graphics g);

        /// <summary>
        /// Rysuj element na szkicu
        /// </summary>
        /// <param name="g"></param>
        /// <param name="szkic"></param>
        public virtual void Rysuj(Graphics g, Szkic szkic) { }

        /// <summary>
        /// Przesuń element do punktu (x, y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual bool MoveTo(int x, int y)
        {
            _x = x;
            _y = y;

            return _zmieniony = true;
        }

        /// <summary>
        /// Przesuń w kierunku
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public virtual bool MoveBy(int dx, int dy)
        {
            MoveTo(_x + dx, _y + dy);
            return _zmieniony = true;
        }

        /// <summary>
        /// Czy jest wybrany
        /// </summary>
        /// <param name="selected"></param>
        public void SelectState(bool selected)
        {
            _selected = selected;
        }
    }
}
