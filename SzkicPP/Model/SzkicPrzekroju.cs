using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Drawing2D;

namespace SzkicPP.Model
{
    /// <summary>
    /// Szkic przekroju cieku
    /// </summary>
    public class SzkicPrzekroju
    {
        /// <summary>
        /// Nazwa pliku szkicu
        /// </summary>
        [Browsable(false)]
        public string Plik { get { return string.Format("{0}.xml", _numerPrzekroju); } }

        bool _zmodyfikowany = false;

        /// <summary>
        /// Czy szkic został zmieniony i nie zapisany
        /// </summary>
        [Category("Atrybuty"), Browsable(false)]
        public bool Zmieniony { get { return _zmodyfikowany; } }

        public void WymagaZapisu(bool zmieniony)
        {
            _zmodyfikowany = zmieniony;
        }

        Size _rozmiar = Size.Empty;

        Rectangle _granica = Rectangle.Empty;

        /// <summary>
        /// Rozmiar papieru
        /// </summary>
        public void NowaGranica(Rectangle value)
        {
            _granica = value;

            //Obliczamy początek szkicu, w ramach granicy/strony (staramy sie wyśrodkować szkic na stronie), 
            //na podstawie szerokości szkicu
            int brzegLewyX = (_granica.Width - _rozmiar.Width) / 2;

            //Szkic będzie tak samo odległy od granic strony (od góry i z boku)
            int brzegLewyY = brzegLewyX;
            
            //Aktualizujemy obszar szkicu
            _obszar = new Rectangle(brzegLewyX, brzegLewyY, _rozmiar.Width, _rozmiar.Height);
        }

        Rectangle _obszar;

        /// <summary>
        /// Obszar szkicu
        /// </summary>
        [Browsable(false)]
        public Rectangle Obszar { get { return _obszar; } }

        /// <summary>
        /// Jednostka odległości między pikietami
        /// </summary>
        [Browsable(false)]
        public int Alignment { get { return _obszar.Height / (_pikiety.Count - 1); } }

        string _nazwaRzeki = "";

        /// <summary>
        /// Nazwa rzeki/cieku
        /// </summary>
        [Category("Atrybuty"), DisplayName("Nazwa cieku"), ReadOnly(false), XmlAttribute("nazwaCieku")]
        public string NazwaRzeki
        {
            get { return _nazwaRzeki; }
            set
            {
                _nazwaRzeki = value;
                _zmodyfikowany = true;
            }
        }
        
        string _numerObiektu = "";

        /// <summary>
        /// Numer przekroju
        /// </summary>
        [Category("Atrybuty"), DisplayName("Numer obiektu"), ReadOnly(false), XmlAttribute("numerObiektu")]
        public string NumerObiektu
        {
            get { return _numerObiektu; }
            set
            {
                _numerObiektu = value;
                _zmodyfikowany = true;
            }
        }

        string _numerPrzekroju = "";

        /// <summary>
        /// Numer przekroju
        /// </summary>
        [Category("Atrybuty"), DisplayName("Numer przekroju"), ReadOnly(false), XmlAttribute("numerPrzekroju")]
        public string NumerPrzekroju
        {
            get { return _numerPrzekroju; }
            set
            {
                _numerPrzekroju = value;
                _zmodyfikowany = true;
            }
        }

        string _dataPomiaru = "";

        /// <summary>
        /// Data pomiaru
        /// </summary>
        [Category("Atrybuty"), DisplayName("Data pomiaru"), ReadOnly(false), XmlAttribute("dataPomiaru")]
        public string DataPomiaru { get { return _dataPomiaru; } set { _dataPomiaru = value; } }

        List<Pikieta> _pikiety = new List<Pikieta>();

        /// <summary>
        /// Pikiety szkicu
        /// </summary>
        [Browsable(false)]
        public List<Pikieta> Pikiety { get { return _pikiety; } set { _pikiety = value; } }

        //[Category("Widok"), DisplayName("Początek prostej"), ReadOnly(false), XmlAttribute("startProstej"), Browsable(false)]
        //public int StartProstej { get { return _startProstej; } set { _startProstej = value; } }

        //int _startProstej = -1;

        //[Category("Widok"), DisplayName("Koniec prostej"), ReadOnly(false), XmlAttribute("koniecProstej"), Browsable(false)]
        //public int KoniecProstej { get { return _koniecProstej; } set { _koniecProstej = value; } }

        //int _koniecProstej = -1;

        //Point _strzalka = Point.Empty;

        //[Category("Widok"), DisplayName("Strzałka północy")]
        //public Point Strzalka { get { return _strzalka; } set { _strzalka = value; } }
        bool _strzalkaPolnocy = true;

        public void RysujStrzalkaPolnocy(bool rysuj)
        {
            _strzalkaPolnocy = rysuj;
        }

        [Browsable(false)]
        public PointD[] Punkty
        {
            get
            {
                PointD[] punkty = new PointD[_pikiety.Count];

                for (int i = 0; i < _pikiety.Count; i++)
                {
                    //punkty[i] = _pikiety[i].Punkt;
                    punkty[i].X = _pikiety[i].Punkt.Y;
                    punkty[i].Y = _pikiety[i].Punkt.X;
                }

                return punkty;
            }
        }

        /// <summary>
        /// Koryto cieku
        /// </summary>
        public Koryto Koryto = null;

        /// <summary>
        /// Pobranie pikiety po indeksie
        /// </summary>
        /// <param name="numerPikiety"></param>
        /// <returns></returns>
        public Pikieta this[int numerPikiety]
        {
            get
            {
                int index = numerPikiety - 1;
                return index >= 0 && index < _pikiety.Count ? _pikiety[index] : null;
            }
        }

        /// <summary>
        /// Pobranie pikiety po numerze
        /// </summary>
        /// <param name="numer"></param>
        /// <returns></returns>
        public Pikieta this[string numer]
        {
            get { return null; }
        }

        List<Fotografia> _fotografie = new List<Fotografia>();

        /// <summary>
        /// Fotografie na szkicu
        /// </summary>
        [Browsable(false)]
        public List<Fotografia> Fotografie { get { return _fotografie; } set { _fotografie = value; } }

        List<Skarpa> _skarpy = new List<Skarpa>();

        /// <summary>
        /// Skarpy na szkicu
        /// </summary>
        [Browsable(false)]
        public List<Skarpa> Skarpy { get { return _skarpy; } set { _skarpy = value; } }

        List<Droga> _drogi = new List<Droga>();

        /// <summary>
        /// Drogi na szkicu
        /// </summary>
        [Browsable(false)]
        public List<Droga> Drogi { get { return _drogi; } set { _drogi = value; } }

        List<Zabudowa> _zabudowy = new List<Zabudowa>();

        /// <summary>
        /// Zabudowa na szkicu
        /// </summary>
        [Browsable(false)]
        public List<Zabudowa> Zabudowy { get { return _zabudowy; } set { _zabudowy = value; } }

        List<Tekst> _teksty = new List<Tekst>();

        /// <summary>
        /// Teksty szkicu
        /// </summary>
        [Browsable(false)]
        public List<Tekst> Teksty { get { return _teksty; } set { _teksty = value; } }

        protected StringFormat _format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

        /// <summary>
        /// Konstruktor szkicu (domyślny)
        /// </summary>
        public SzkicPrzekroju() : this("", "") { }

        /// <summary>
        /// Konstruktor szkicu
        /// </summary>
        /// <param name="nazwaRzeki"></param>
        /// <param name="numerPrzekroju"></param>
        public SzkicPrzekroju(string nazwaRzeki, string numerPrzekroju)
        {
            _nazwaRzeki = nazwaRzeki;
            _numerPrzekroju = numerPrzekroju;
            _rozmiar = StylSzkicu.SzkicSize;
            _zmodyfikowany = false;
            _format.Alignment = StringAlignment.Far;
            _format.LineAlignment = StringAlignment.Near;
        }

        /// <summary>
        /// Dodanie obiektu do szkicu
        /// </summary>
        /// <param name="obiekt"></param>
        /// <returns></returns>
        public bool DodajElement(ElementSzkicu obiekt)
        {
            if (obiekt == null) return false;

            if (obiekt is Tekst) return DodajTekst(obiekt as Tekst);
            if (obiekt is Fotografia) return DodajFotografia(obiekt as Fotografia, false);
            if (obiekt is Zabudowa) return DodajZabudowa(obiekt as Zabudowa);

            return false;
        }

        /// <summary>
        /// Dodanie pikiety do szkicu
        /// </summary>
        /// <param name="pikieta"></param>
        /// <returns></returns>
        public bool DodajPikieta(Pikieta pikieta)
        {
            if (pikieta == null) return false;

            _pikiety.Add(pikieta);

            return _zmodyfikowany = true;
        }

        /// <summary>
        /// Dodanie tekstu do szkicu
        /// </summary>
        /// <param name="tekst"></param>
        /// <returns></returns>
        public bool DodajTekst(Tekst tekst)
        {
            if (tekst == null) return false;

            _teksty.Add(tekst);

            return _zmodyfikowany = true;
        }

        /// <summary>
        /// Dodanie fotografii do szkicu
        /// </summary>
        /// <param name="fotografia"></param>
        /// <param name="autoPosition"></param>
        /// <returns></returns>
        public bool DodajFotografia(Fotografia fotografia, bool autoPosition)
        {
            if (fotografia == null) return false;

            _fotografie.Add(fotografia);

            if (autoPosition)
                fotografia.MoveTo(_obszar.Left + StylSzkicu.PikietyWidth, _obszar.Bottom + (_fotografie.Count * 30));

            return _zmodyfikowany = true;
        }

        public bool DodajFotografia(Fotografia fotografia)
        {
            return DodajFotografia(fotografia, true);
        }

        /// <summary>
        /// Dodanie zabudowy do szkicu
        /// </summary>
        /// <param name="zabudowa"></param>
        /// <returns></returns>
        public bool DodajZabudowa(Zabudowa zabudowa)
        {
            if (zabudowa == null) return false;

            _zabudowy.Add(zabudowa);

            return _zmodyfikowany = true;
        }

        /// <summary>
        /// Dodanie skarpy do szkicu (od podanej pikiety)
        /// </summary>
        /// <param name="pikieta"></param>
        /// <returns></returns>
        public bool DodajSkarpa(Pikieta pikieta)
        {
            if (pikieta == null) return false;

            for (int i = 1; i <= _pikiety.Count; i++)
            {
                if (pikieta.Numer == _pikiety[i - 1].Numer)
                {
                    Skarpa skarpa = new Skarpa(i, i + 1, false);
                    _skarpy.Add(skarpa);
                    return _zmodyfikowany = true;
                }
            }

            return false;
        }

        /// <summary>
        /// Dodanie drogi do szkicu (od podanej pikiety)
        /// </summary>
        /// <param name="pikieta"></param>
        /// <returns></returns>
        public bool DodajDroga(Pikieta pikieta)
        {
            if (pikieta == null) return false;

            for (int i = 1; i <= _pikiety.Count; i++)
            {
                if (pikieta.Numer == _pikiety[i - 1].Numer)
                {
                    Droga droga = new Droga(i, i + 1);
                    Drogi.Add(droga);
                    return _zmodyfikowany = true;
                }
            }

            return false;
        }

        /// <summary>
        /// Obliczenie odległości pikiety od krawędzi przekroju wyznaczonej przez pikiety koryta
        /// </summary>
        /// <param name="pikieta"></param>
        /// <returns></returns>
        public double PikietaDistanceH(Pikieta pikieta)
        {
            if (pikieta == null) return 999;

            //Koryto wyznacza prostą przekroju, obliczamy odległość pikiety do tej prostej
            if (Koryto == null) return 999;

            PointD p0 = pikieta.Punkt;

            Pikieta a = this[Koryto.PierwszaPikieta];
            Pikieta b = this[Koryto.DrugaPikieta];

            if (a == null || b == null) return 999;

            PointD p1 = a.Punkt;
            PointD p2 = b.Punkt;

            float d = (float)
                (Math.Abs((p2.X - p1.X) * (p1.Y - p0.Y) - (p1.X - p0.X) * (p2.Y - p1.Y)) /
                Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)));

            if (Wektor.PointDirection(p1, p2, p0) > 0) d = -d;

            return d;
        }

        /// <summary>
        /// Szkielet zbudowany na podstawie pikiet
        /// </summary>
        /// <returns></returns>
        public bool ZbudujSzkielet()
        {
            //Muszą być co najmniej dwie pikiety (na których będą oparte oba brzegi koryta)
            if (_pikiety.Count < 2) return false;

            int brzegLewyIndex = -1;
            int brzegPrawyIndex = -1;

            //Rozmieszczamy wszystkie pikiety równomiernie na szkicu (stała odległość)
            for (int i = 0; i < _pikiety.Count; i++)
            {
                Pikieta pikieta = _pikiety[i];
                
                int alignedPosition = Alignment * i + _obszar.Top;
                pikieta.MoveTo(_obszar.Left + StylSzkicu.PikietyWidth, alignedPosition);

                if (pikieta.JestKorytem())
                {
                    //przy każdej pikiecie koryta aktualizujemy indeks prawego koryta
                    //ostatnia pikieta koryta będzie prawym korytem
                    brzegPrawyIndex = i;

                    if (brzegLewyIndex < 0) //tylko pierwsza pikieta koryta będzie lewym korytem
                        brzegLewyIndex = i - 1; //brzeg zaczyna się jedną pikiete wcześniej
                }

                if (pikieta.JestDroga())
                {
                    Droga droga = new Droga(i, i + 1);
                    _drogi.Add(droga);
                }
            }

            if (brzegLewyIndex >= 0 && brzegPrawyIndex > 0 && brzegPrawyIndex > brzegLewyIndex)
            {
                //Tworzymy koryto
                Koryto = new Koryto(brzegLewyIndex + 1, brzegPrawyIndex + 1);

                //Tworzymy skarpę brzegu lewego
                Skarpa brzegLewy = new Skarpa(brzegLewyIndex, brzegLewyIndex + 1, true);
                _skarpy.Add(brzegLewy);

                //Tworzymy skarpę brzegu prawego
                Skarpa brzegPrawy = new Skarpa(brzegPrawyIndex + 1, brzegPrawyIndex + 2, true);
                _skarpy.Add(brzegPrawy);
            }

            return true;
        }

        /// <summary>
        /// Rysowanie szkicu (wyśrodkowany na stronie)
        /// </summary>
        /// <param name="g"></param>
        public void Rysuj(Graphics g)
        {
            if (_strzalkaPolnocy)
            {
                double a = 0, x = 1;
                PointD[] prosta = Punkty;

                if (Wektor.RegresjaWektoraProstej(prosta, ref x, ref a))
                {
                    double x1 = prosta[0].X;
                    double y1 = a * x1;

                    double x2 = prosta[prosta.Length - 1].X;
                    double y2 = a * x2;

                    if (x < 1) //prosta jest równoległa do osi Y
                    {
                        x1 = x2 = 0;
                        //obliczamy kierunek wektora prostej
                        y1 = -(prosta[0].Y - prosta[1].Y);
                        y2 = 2 * y1;
                    }

                    PointD tail = new PointD(0, 0);
                    PointD tip1 = new PointD(0, -1);
                    PointD tip2 = new PointD(x2 - x1, y2 - y1);

                    double angle = -Angles.ToDegrees(Angles.AngleBetweenOriented(tip1, tail, tip2));

                    Kierunek.RysujStrzalka(g, _obszar.Left + StylSzkicu.PikietyWidth / 2, _obszar.Top / 2, (float)angle);
                }

            }

            /*g.DrawString(string.Format("Rzeka {0} {1}", _nazwaRzeki, string.IsNullOrEmpty(_numerObiektu) ? "" : "(" + _numerObiektu + ")"),
                StylSzkicu.TekstFont, StylSzkicu.TekstBrush, 5, 5);*/
            g.DrawString(string.Format("Rzeka {0}", _nazwaRzeki), StylSzkicu.TekstFont, StylSzkicu.TekstBrush, 5, 5);
            g.DrawString(string.Format("Przekrój nr {0}", _numerPrzekroju),
                StylSzkicu.TekstFont, StylSzkicu.TekstBrush, 5, 25);

            int dataY = 5;

            g.DrawString(string.Format("Data pomiaru"),
                StylSzkicu.TekstFont, StylSzkicu.TekstBrush, _obszar.Right, dataY, _format);
            g.DrawString(string.Format("{0}", _dataPomiaru),
                StylSzkicu.TekstFont, StylSzkicu.TekstBrush, _obszar.Right, dataY);

            if (Koryto != null) Koryto.Rysuj(g, this);

            foreach (Droga d in _drogi) if (d.RysujNaSzkicu) d.Rysuj(g, this);
            foreach (Skarpa s in _skarpy) if (s.RysujNaSzkicu) s.Rysuj(g, this);

            foreach (Pikieta p in _pikiety) p.Rysuj(g);

            foreach (Fotografia f in _fotografie)
                if (f.RysujNaSzkicu) f.Rysuj(g);
            
            foreach (Zabudowa z in _zabudowy)
                if (z.RysujNaSzkicu) z.Rysuj(g);

            foreach (Tekst t in _teksty)
                if (t.RysujNaSzkicu) t.Rysuj(g);
        }

        /// <summary>
        /// Szukaj element szkicu
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ElementSzkicu Szukaj(int x, int y)
        {
            foreach (Fotografia obiekt in _fotografie)
            {
                if (Wektor.PointDistance(x, y, obiekt.X, obiekt.Y) < 10) return obiekt;
            }

            foreach (Pikieta obiekt in _pikiety)
            {
                if (Wektor.PointDistance(x, y, obiekt.X, obiekt.Y) < 10) return obiekt;
            }

            foreach (Zabudowa obiekt in Zabudowy)
            {
                if (Wektor.PointDistance(x, y, obiekt.X, obiekt.Y) < 10) return obiekt;
            }
            
            foreach (ElementSzkicu obiekt in _teksty)
            {
                if (Wektor.PointDistance(x, y, obiekt.X, obiekt.Y) < 10) return obiekt;
            }

            return null;
        }

        /// <summary>
        /// Zapisz szkic do pliku xml
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="model"></param>
        public static void ToXML(string xmlFile, SzkicPrzekroju model)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(SzkicPrzekroju));
            StreamWriter writer = new StreamWriter(xmlFile);
            xmlSer.Serialize(writer, model);
            writer.Close();
        }

        /// <summary>
        /// Zapisz szkic do pliku xml
        /// </summary>
        /// <param name="xmlFile"></param>
        public void ToXML(string xmlFile)
        {
            SzkicPrzekroju.ToXML(xmlFile, this);
            _zmodyfikowany = false;
        }

        /// <summary>
        /// Wczytaj szkic z pliku xml
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static SzkicPrzekroju FromXML(string xmlFile)
        {
            SzkicPrzekroju szkic = null;
            StreamReader reader = null;

            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(SzkicPrzekroju));
                reader = new StreamReader(xmlFile);
                szkic = (SzkicPrzekroju)xmlSer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            szkic._zmodyfikowany = false;

            return szkic;
        }

        public override string ToString()
        {
            return string.Format("Szkic {1} cieku {0}", _nazwaRzeki, _numerPrzekroju);
        }

        public bool SkalujPikiety(bool aligned)
        {
            return SkalujPikiety(aligned, false);
        }

        /// <summary>
        /// Skalowanie/wyrównania pikiet szkicu
        /// </summary>
        /// <param name="aligned"></param>
        /// <param name="onlyHeight"></param>
        /// <returns></returns>
        public bool SkalujPikiety(bool aligned, bool onlyHeight)
        {
            //Muszą być co najmniej dwie pikiety (na których będą oparte oba brzegi koryta)
            if (_pikiety.Count < 2) return false;

            PointD pA = _pikiety[0].Punkt; //Pierwszy punkt
            PointD pB = _pikiety[1].Punkt; //Ostatni punkt

            double minDist = Wektor.PointDistance(pA, pB); //Odległość między punktami Start i End
            double midDist = minDist;

            int alignmentUnit = Alignment;

            //Rozmieszczamy wszystkie pikiety równomiernie na szkicu (stała odległość)
            for (int i = 2; i < _pikiety.Count; i++)
            {
                pA = _pikiety[i-1].Punkt;
                pB = _pikiety[i].Punkt;

                double dist = Wektor.PointDistance(pA, pB);

                if (dist < 1) dist = 1;

                midDist += dist;

                if (dist < minDist) minDist = dist;
            }

            midDist /= (_pikiety.Count - 1);
            int  basePos = 0;

            pA = _pikiety[0].Punkt;
            _pikiety[0].MoveTo(_obszar.Left + StylSzkicu.PikietyWidth, _obszar.Top);

            for (int i = 1; i < _pikiety.Count; i++)
            {
                Pikieta pikieta = _pikiety[i];
                pA = _pikiety[i - 1].Punkt;
                pB = pikieta.Punkt;

                double dist = Wektor.PointDistance(pA, pB);
                int scaledDist = (int)((dist * alignmentUnit) / minDist);

                if (scaledDist < alignmentUnit / 2) scaledDist = alignmentUnit / 2;
                if (scaledDist > alignmentUnit * 3) scaledDist = alignmentUnit * 3;

                basePos += scaledDist;

                int scaledPosition = basePos + _obszar.Top;
                int alignedPosition = alignmentUnit * i + _obszar.Top;

                int xPosition = _obszar.Left + StylSzkicu.PikietyWidth;

                if (onlyHeight) xPosition = pikieta.X; //nie zmieniaj położenia X

                if (aligned)
                    pikieta.MoveTo(xPosition, alignedPosition);
                else
                    pikieta.MoveTo(xPosition, scaledPosition);
            }

            return _zmodyfikowany = true;
        }
    }
}
