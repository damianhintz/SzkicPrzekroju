using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace SzkicPP.Model
{
    /// <summary>
    /// Zabudowa
    /// </summary>
    public class Zabudowa : ElementSzkicu
    {
        protected bool _budynek = false;

        /// <summary>
        /// Czy skarpa jest brzegiem
        /// </summary>
        [XmlAttribute("budynek"), Browsable(true), DisplayName("Budynek"), Category("Atrybuty"), Description("Zabudowa czy budynek")]
        public bool IsBudynek { get { return _budynek; } set { _budynek = value; } }

        protected Size _rozmiar;

        /// <summary>
        /// Szerokość
        /// </summary>
        [XmlAttribute("width"), DisplayName("Szerokość"), Category("Atrybuty")]
        public int Width
        {
            get { return _rozmiar.Width; }
            set
            {
                _rozmiar.Width = value;
                _zmieniony = true;
            }
        }

        /// <summary>
        /// Wysokość
        /// </summary>
        [XmlAttribute("height"), DisplayName("Wysokość"), Category("Atrybuty")]
        public int Height
        {
            get { return _rozmiar.Height; }
            set
            {
                _rozmiar.Height = value;
                _zmieniony = true;
            }
        }

        /// <summary>
        /// Konstruktor zabudowy (domyślny)
        /// </summary>
        public Zabudowa()
            : this(1, 1)
        {
        }

        /// <summary>
        /// Konstruktor zabudowy
        /// </summary>
        /// <param name="rozmiar"></param>
        public Zabudowa(Size rozmiar)
            : this(rozmiar.Width, rozmiar.Height)
        {
        }

        /// <summary>
        /// Konstruktor zabudowy
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Zabudowa(int width, int height)
            : this(width, height, false)
        {
        }

        /// <summary>
        /// Konstruktor zabudowy
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="budynek"></param>
        public Zabudowa(int width, int height, bool budynek)
        {
            _rozmiar.Width = width;
            _rozmiar.Height = height;
            _budynek = budynek;
        }

        /// <summary>
        /// Rysowanie zabudowy
        /// </summary>
        /// <param name="g"></param>
        public override void Rysuj(Graphics g)
        {
            g.FillRectangle(StylSzkicu.BudynekBrush, X, Y, Width, Height);

            if (_selected)
            {
                g.DrawRectangle(StylSzkicu.SelectedPen, X, Y, Width, Height);
            }
            else
            {
                if (_budynek)
                    g.DrawRectangle(StylSzkicu.BudynekPen, X, Y, Width, Height);
                else
                    g.DrawRectangle(StylSzkicu.ZabudowaPen, X, Y, Width, Height);
            }
        }

        public override string ToString()
        {
            return string.Format("Zabudowa - {0}", _rozmiar);
        }
    }
}
