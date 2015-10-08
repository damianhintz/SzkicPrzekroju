using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace SzkicPrzekroju.Domena
{
    /// <summary>
    /// Szkielet
    /// </summary>
    public class Teren
    {
        protected bool _rysuj = true;

        /// <summary>
        /// Czy rysować obiekt na szkicu
        /// </summary>
        [Category("Atrybuty"), XmlAttribute("rysuj"), Browsable(true), DisplayName("Rysuj na szkicu")]
        public bool RysujNaSzkicu
        {
            get { return _rysuj; }
            set
            {
                _rysuj = value;
                _zmieniony = true;
            }
        }

        protected bool _zmieniony = false;

        [Browsable(false)]
        public bool Zmieniony { get { return _zmieniony; } }

        public void WymagaZapisu(bool zmieniony)
        {
            _zmieniony = zmieniony;
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false), Category("Atrybuty")]
        public string Zakres
        {
            get
            {
                return string.Format("{2}{0}->{3}{1}",
                    _startIndex, _endIndex,
                    _startIndex < 10 ? "0" : "", _endIndex < 10 ? "0" : "");
            }
        }

        protected int _startIndex = -1;

        /// <summary>
        /// Numer pierszej pikiety (od 1)
        /// </summary>
        [Category("Atrybuty"), XmlAttribute("startPikieta"), DisplayName("Pierwsza pikieta")]
        public int PierwszaPikieta
        {
            get { return _startIndex; }
            set
            {
                _startIndex = value;
                _zmieniony = true;
            }
        }

        protected int _endIndex = -1;

        /// <summary>
        /// Numer drugiej pikiety (od 1)
        /// </summary>
        [Category("Atrybuty"), XmlAttribute("endPikieta"), DisplayName("Druga pikieta")]
        public int DrugaPikieta
        {
            get { return _endIndex; }
            set
            {
                _endIndex = value;
                _zmieniony = true;
            }
        }

        protected Pikieta _startPikieta;
        protected Pikieta _endPikieta;

        /// <summary>
        /// Konstruktor szkieletu
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        protected Teren(int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
        }

        /// <summary>
        /// Aktualizuj zmienne pikiet, na podstawie podanych indeksów
        /// </summary>
        /// <param name="szkic"></param>
        /// <returns></returns>
        protected bool AktualizujPikiety(Szkic szkic)
        {
            if (_startIndex == _endIndex) return false;

            _startPikieta = szkic[_startIndex];
            _endPikieta = szkic[_endIndex];

            if (_startPikieta == null || _endPikieta == null) return false;

            if (this is Skarpa)
            {
                if (_startPikieta.Rzedna < _endPikieta.Rzedna)
                {
                    _startPikieta = szkic[_endIndex];
                    _endPikieta = szkic[_startIndex];
                }
            }

            return true;
        }

        /// <summary>
        /// Rysowanie obiektu między pikietami
        /// </summary>
        /// <param name="g"></param>
        /// <param name="szkic"></param>
        public virtual void Rysuj(Graphics g, Szkic szkic)
        {
        }

        public override string ToString()
        {
            string[] cols = GetType().ToString().Split('.');

            string type = cols[cols.Length - 1];

            return string.Format("{0}", type);
        }
    }
}
