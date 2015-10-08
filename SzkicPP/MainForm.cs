﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SzkicPP.Model;

namespace SzkicPP
{
    public partial class MainForm : Form
    {
        SzkicPrzekroju _szkicPrzekroju = null;
        ElementSzkicu _selectedObiekt = null; //wybrany obiekt (przesuń do punktu)
        ElementSzkicu _newObiekt = null; //nowy obiekt

        Rectangle _granica;
        Rectangle _granicaPrint; //różne od _granica jeżeli uwzględnione zostaną marginesy

        StringFormat _format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

        public MainForm()
        {
            InitializeComponent();
        }

        private void mPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //mPrintDocument.DefaultPageSettings.Landscape = true;
            
            //e.Graphics.DrawRectangle(StylSzkicu.SzkicPen, _granica);
            //e.Graphics.DrawRectangle(StylSzkicu.SzkicPen, _granicaPrint);
            
            if (_szkicPrzekroju == null) return;

            _szkicPrzekroju.Rysuj(e.Graphics);

            if (opisyTerenuToolStripMenuItem.Checked)
            {
                _format.Alignment = StringAlignment.Far;
                _format.LineAlignment = StringAlignment.Center;

                for (int i = 1; i < _szkicPrzekroju.Pikiety.Count; i++)
                {
                    Pikieta p = _szkicPrzekroju.Pikiety[i];
                    Pikieta pp = _szkicPrzekroju.Pikiety[i - 1];
                    int x = pp.X;
                    int y = pp.Y;
                    float angle = 90;
                    int rozmiar = 7;
                    int yy = (p.Y + pp.Y) / 2 + (int)(rozmiar * 1.75) / 2;
                    int yyy = (p.Y + pp.Y) / 2 - (int)(rozmiar * 1.75) / 2;
                    Color kolor = Color.DarkRed;

                    if (pp.Rzedna > p.Rzedna)
                    {
                        angle = -angle;
                        yy = (p.Y + pp.Y) / 2 - (int)(rozmiar * 1.75) / 2;
                        kolor = Color.DarkBlue;
                    }

                    Rectangle r = new Rectangle(x, y, _szkicPrzekroju.Obszar.Right - x, p.Y - y);

                    e.Graphics.DrawString(KodTerenu.OpisKodu(p.KodFormyTerenu),
                        StylSzkicu.PikietaFont, StylSzkicu.PikietaBrush, r, _format);

                    Kierunek.Rysuj(e.Graphics, 
                        _szkicPrzekroju.Obszar.Right + 15, yy, kolor,
                        rozmiar, angle, false, false, true);

                    e.Graphics.DrawString(string.Format("{0:F2} [m] n.p.m.", pp.Rzedna - p.Rzedna),
                        StylSzkicu.PikietaFont, StylSzkicu.PikietaBrush, _szkicPrzekroju.Obszar.Right + 25, yyy);

                    e.Graphics.DrawString(string.Format("{0:F2} [m]", Wektor.PointDistance(p.Punkt, pp.Punkt)),
                        StylSzkicu.PikietaFont, StylSzkicu.PikietaBrush, 
                        _szkicPrzekroju.Obszar.Left + StylSzkicu.PikietyWidth - 100, yyy);
                }

                _format.Alignment = StringAlignment.Far;
                _format.LineAlignment = StringAlignment.Far;

                foreach (Pikieta p in _szkicPrzekroju.Pikiety)
                {
                    e.Graphics.DrawString(string.Format("{0:F2} [m]", _szkicPrzekroju.PikietaDistanceH(p)),
                        StylSzkicu.PikietaFont, StylSzkicu.PikietaBrush,
                        _szkicPrzekroju.Obszar.Left + StylSzkicu.PikietyWidth, p.Y, _format);
                }
            }
        }

        private void mForm_KeyDown(object sender, KeyEventArgs e)
        {
            Point point = mPictureBox.PointToClient(Cursor.Position);

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    _newObiekt = null;
                    mPictureBox.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void mPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_newObiekt != null)
            {
                mPictureBox.Refresh();
                _newObiekt.MoveTo(e.X, e.Y);
                _newObiekt.Rysuj(mPictureBox.CreateGraphics());
            }
        }

        private void mPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = mPictureBox.PointToClient(Cursor.Position);

            if (_szkicPrzekroju == null) return;

            if (_newObiekt != null)
            {
                _newObiekt.MoveTo(point.X, point.Y);
                _szkicPrzekroju.DodajElement(_newObiekt);
                _newObiekt = null;
            }

            if (_selectedObiekt != null) _selectedObiekt.SelectState(false);

            _selectedObiekt = _szkicPrzekroju.Szukaj(point.X, point.Y);

            if (_selectedObiekt != null)
            {
                _selectedObiekt.SelectState(true);
                AktualizujForm(_selectedObiekt.ToString());
            }

            mPictureBox.Refresh();
        }

        void AktualizujForm(string msg)
        {
            Text = string.Format("{0}@{1} {2}", Application.ProductName, Application.ProductVersion, msg);
        }

        private void dodajTekstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _newObiekt = new Tekst("Tekst");
        }

        private void dodajFotografiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point point = mPictureBox.PointToClient(Cursor.Position);
            _newObiekt = new Fotografia("0");
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mPrintDialog.ShowDialog(this);
        }

        private void podgladToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mPrintPreviewDialog.ShowDialog(this);
        }

        private void mPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (_szkicPrzekroju == null) return;
            _szkicPrzekroju.Rysuj(e.Graphics);
        }

        private void ustawieniaStronyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mPageSetupDialog.ShowDialog(this);
            mPrintDocument.DefaultPageSettings = mPageSetupDialog.PageSettings;
            mPictureBox.Refresh();
        }

        void ZapiszSzkic()
        {
            if (_szkicPrzekroju == null) return;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Zapisz szkic...";
            dialog.FileName = _szkicPrzekroju.Plik;

            if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

            _szkicPrzekroju.ToXML(dialog.FileName);
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZapiszSzkic();
        }

        private void edytujSzkicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;

            EdytorForm edytor = new EdytorForm();
            edytor.Szkic = _szkicPrzekroju;
            edytor.ShowDialog();

            AktualizujForm(_szkicPrzekroju.ToString());
            mPictureBox.Refresh();
        }

        private void edytujObiektToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;

            _selectedObiekt.WymagaZapisu(false);

            EdytorForm edytor = new EdytorForm();
            edytor.ElementSzkicu = _selectedObiekt;
            edytor.ShowDialog();

            if (_selectedObiekt.Zmieniony) _szkicPrzekroju.WymagaZapisu(true);

            mPictureBox.Refresh();
        }

        /// <summary>
        /// Rozbijamy plik z przekrojami na pojedyncze pliki (tylko obiekty typu Przekrój)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importujPrzekrojeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Plik z przekrojami (*.txt)|*.txt";
            dialog.Title = "Wybiersz plik zestawień przekroi";

            if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

            StreamReader reader = new StreamReader(dialog.FileName, Encoding.GetEncoding(1250));
            string wiersz = null;

            //(00) Lp. np. 2
            //(01) Nazwa cieku np. Pasłęka
            //(02) Numer obiekt
            //(03) Numer przekroju i punktu pomiarowego np. 240.02
            //(04) Typ obiektu np. Przekrój
            //(05) Kod punktu np. 1
            //(06) Kod formy pokrycia terenu np. T01
            //(07) Kilometraż
            //(08) Współrzędna X np. 640648.68
            //(09) Współrzędna Y np. 587563.95
            //(10) Z rzędna [m n.p.m.] np. 147.89
            //(11) Rzędna zw. Wody
            //(12) H - wysokość progu przelewowego [m]	
            //(13) α - kąt skrzyżowania głównej osi mostu z osią cieku	
            //(14) Data pomiaru np. 7-02-2012
            //(15) Numer fotografii np. 243a.jpg; 243b.jpg
            //(16) Administrator obiektu

            Dictionary<string, List<Pikieta>> przekroje = new Dictionary<string, List<Pikieta>>();
            Dictionary<string, int> obiekty = new Dictionary<string, int>();
            Dictionary<string, AtrybutySzkicu> atrybuty = new Dictionary<string, AtrybutySzkicu>();

            //00 Nazwa	
            //01 Nr_obiekt	
            //02 Typ_obiekt	
            //03 Nr_pkt	
            //04 X	
            //05 Y	
            //06 Z	
            //07 M	
            //08 Kod_pikiet	
            //09 Kod_pokr	
            //10 Rzedna_ZW	
            //11 Data	
            //12 Foto	
            //13 Szer_mostu	
            //14 H_progu	
            //15 Admin	
            //16 Uwagi

            while ((wiersz = reader.ReadLine()) != null)
            {
                string[] cols = wiersz.Split('\t'); //Kolumny powinny być rozdzielone tabulacjami

                if (cols.Length < 15) continue; //Za mało kolumn w wierszu

                string nazwaCieku = cols[Properties.Settings.Default.NazwaCieku];
                string numerObiekt = cols[Properties.Settings.Default.NumerObiekt];
                string numerPrzekroju = cols[Properties.Settings.Default.NumerPunktu]; //nazwa pliku, numer punktu
                string typObiektu = cols[Properties.Settings.Default.TypObiekt].Trim().ToLower(); //powinien być typu Przekrój
                string kodPunktu = cols[Properties.Settings.Default.KodPikiety]; //kod punktu powinien być prawidłowy
                string kodFormy = cols[Properties.Settings.Default.KodPokrycia]; //kod formy powinien być prawidłowy
                string wspX = cols[Properties.Settings.Default.WspX];
                string wspY = cols[Properties.Settings.Default.WspY];
                string wspZ = cols[Properties.Settings.Default.WspZ];
                string dataPomiaru = cols[Properties.Settings.Default.DataPomiaru];
                string zdjecia = cols[Properties.Settings.Default.Foto].Replace("\"", "").Trim(); //numery zdjęć powinny być zgodne
                //string administrator = cols[16];

                string przekroj = numerPrzekroju.Split('.')[0]; //parse np. 240.02

                if (numerPrzekroju.EndsWith(".01"))
                {
                    if (!obiekty.ContainsKey(typObiektu))
                        obiekty.Add(typObiektu, 0);

                    obiekty[typObiektu]++;
                    
                    if (!atrybuty.ContainsKey(przekroj))
                    {
                        AtrybutySzkicu atrybutySzkicu = new AtrybutySzkicu();
                        atrybuty.Add(przekroj, atrybutySzkicu);

                        atrybutySzkicu.Numer = przekroj;
                        atrybutySzkicu.NazwaCieku = nazwaCieku;
                        atrybutySzkicu.NumerObiektu = numerObiekt;
                        atrybutySzkicu.DataSzkicu = dataPomiaru;

                        if (!string.IsNullOrEmpty(zdjecia))
                        {
                            string[] fotografie = zdjecia.Split(';');

                            for (int i = 0; i < fotografie.Length; i++)
                                fotografie[i] = fotografie[i].Split('.')[0].Trim();

                            atrybutySzkicu.Fotografie = fotografie;
                        }
                        else
                            atrybutySzkicu.Fotografie = new string[0];
                    }
                }

                if (typObiektu != "przekrój") continue; //pomijamy obiekty inne niż przekroje

                //Zakładamy nową liste punktów przekroju, jeżeli jeszcze nie została utworzona
                if (!przekroje.ContainsKey(przekroj))
                    przekroje.Add(przekroj, new List<Pikieta>());

                Pikieta pikieta = new Pikieta(numerPrzekroju, kodFormy, double.Parse(wspX), double.Parse(wspY));
                pikieta.Rzedna = double.Parse(wspZ);
                przekroje[przekroj].Add(pikieta);
            }

            reader.Close();

            foreach (KeyValuePair<string, List<Pikieta>> kv in przekroje)
            {
                string numer = kv.Key;
                string numerPrzekroju = numer;
                string przekrojPlik = Path.Combine(Path.GetDirectoryName(dialog.FileName), numerPrzekroju + ".xml");
                AtrybutySzkicu atrybutySzkicu = atrybuty[numer];
                List<Pikieta> punkty = kv.Value;

                SzkicPrzekroju szkic = new SzkicPrzekroju(atrybutySzkicu.NazwaCieku, numerPrzekroju);
                szkic.DataPomiaru = atrybutySzkicu.DataSzkicu;
                szkic.NumerObiektu = atrybutySzkicu.NumerObiektu;
                szkic.NowaGranica(_granica);
                szkic.Pikiety = punkty;
                szkic.ZbudujSzkielet(); //skalowanie tylko przy imporcie

                foreach (string foto in atrybutySzkicu.Fotografie)
                {
                    Fotografia fotografia = new Fotografia(foto);
                    szkic.DodajFotografia(fotografia);
                }
                
                szkic.ToXML(przekrojPlik);
            }

            string msg = "";

            foreach (KeyValuePair<string, int> kv in obiekty)
            {
                msg += string.Format("\n{0}: {1} {2}", kv.Key, kv.Value, 
                    kv.Key == "przekrój" ? "(" + przekroje.Count + " zapisane)" : "");
            }

            MessageBox.Show(this, 
                string.Format("Zestawienie obiektów w pliku\n{0}\n", msg), 
                "Import przekroi...", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void openSzkicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skalujPikietyToolStripMenuItem.Checked = false;

            if (!ZamknijSzkic()) return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Wybierz szkic do wczytania...";
            dialog.Filter = "*.xml|*.xml";

            if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

            SzkicPrzekroju szkic = SzkicPrzekroju.FromXML(dialog.FileName);
            szkic.NowaGranica(_granica);
            
            _szkicPrzekroju = szkic;
            AktualizujForm(_szkicPrzekroju.ToString());

            mPictureBox.Refresh();
        }

        private void zakonczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(0, -1);
            mPictureBox.Refresh();
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(0, 1);
            mPictureBox.Refresh();
        }

        private void moveLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(-1, 0);
            mPictureBox.Refresh();
        }

        private void moveRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(1, 0);
            mPictureBox.Refresh();
        }

        private void moveUpFastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(0, -5);
            mPictureBox.Refresh();
        }

        private void moveDownFastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(0, 5);
            mPictureBox.Refresh();
        }

        private void moveLeftFastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(-5, 0);
            mPictureBox.Refresh();
        }

        private void moveRightFastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;
            _selectedObiekt.MoveBy(5, 0);
            mPictureBox.Refresh();
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, 
                string.Format("{0}@{1} (2012-05-17T15:11)", Application.ProductName, Application.ProductVersion), 
                "O Programie",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dodajSkarpaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;
            if (_selectedObiekt == null) return;
            if (!(_selectedObiekt is Pikieta)) return;

            _szkicPrzekroju.DodajSkarpa(_selectedObiekt as Pikieta);

            mPictureBox.Refresh();
        }

        private void dodajDrogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;
            if (_selectedObiekt == null) return;
            if (!(_selectedObiekt is Pikieta)) return;

            _szkicPrzekroju.DodajDroga(_selectedObiekt as Pikieta);

            mPictureBox.Refresh();
        }

        private void dodajZabudowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null)
                _newObiekt = new Zabudowa(30, 30);
            else
                _newObiekt = new Zabudowa(_szkicPrzekroju.Alignment, _szkicPrzekroju.Alignment);
        }

        bool ZamknijSzkic()
        {
            if (_szkicPrzekroju == null)
                return true;

            if (_szkicPrzekroju.Zmieniony)
            {
                DialogResult result;

                if ((result = MessageBox.Show(this, "Szkic został zmieniony. Zapisać zmiany?", "Zamknij szkic",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    ZapiszSzkic();
                }
                else
                {
                    if (result == System.Windows.Forms.DialogResult.Cancel) //anuluj zamykanie
                        return false;
                }
            }

            _szkicPrzekroju = null;
            _selectedObiekt = null;
            _newObiekt = null;

            return true;
        }

        private void zamknijSzkicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ZamknijSzkic())
            {
                AktualizujForm("");
                mPictureBox.Refresh();
            }
        }

        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ZamknijSzkic()) e.Cancel = true;
        }

        private void mForm_Load(object sender, EventArgs e)
        {
            AktualizujForm("");
            _granica = mPrintDocument.DefaultPageSettings.Bounds;
            _granicaPrint = Rectangle.Truncate(mPrintDocument.DefaultPageSettings.PrintableArea);
            mPictureBox.Size = _granica.Size;
        }

        private void deleteObiektToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedObiekt == null) return;

            if (MessageBox.Show(this, 
                "Usunąć obiekt ze szkicu?\n" + string.Format("{0}", _selectedObiekt.ToString()), 
                "Usuń obiekt", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.No) return;

            _selectedObiekt.RysujNaSzkicu = false;
            _selectedObiekt = null;
            _szkicPrzekroju.WymagaZapisu(true);

            AktualizujForm("");
            mPictureBox.Refresh();
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;

            Bitmap image = new Bitmap(_granica.Width, (int)(_granica.Height * Properties.Settings.Default.ScaleHeight));
            mPictureBox.DrawToBitmap(image, _granica);
            
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Zapisz szkic jako obraz...";
            dialog.Filter = "*.png|*.png";
            dialog.FileName = _szkicPrzekroju.Plik.Replace(".xml", ".png");

            if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

            image.Save(dialog.FileName);
        }

        private void edytorTerenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;

            EdytorTerenu edytor = new EdytorTerenu();
            edytor.Szkic = _szkicPrzekroju;
            edytor.ShowDialog(this);

            mPictureBox.Refresh();
        }

        private void skalujPikietyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;

            if (MessageBox.Show(this, "Rozmieścić pikiety na szkicu wyskalowane?", "Skalowanie pikiet",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            _szkicPrzekroju.SkalujPikiety(false, zachowajPionPikietToolStripMenuItem.Checked);
            mPictureBox.Refresh();
        }

        private void opisyTerenuToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            mPictureBox.Refresh();
        }

        private void alignPikietyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;

            if (MessageBox.Show(this, "Rozmieścić pikiety na szkicu równomiernie?", "Wyrównanie pikiet",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            _szkicPrzekroju.SkalujPikiety(true, zachowajPionPikietToolStripMenuItem.Checked);
            mPictureBox.Refresh();
        }

        private void dodajBudynekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null)
                _newObiekt = new Zabudowa(30, 30, true);
            else
                _newObiekt = new Zabudowa(_szkicPrzekroju.Alignment, _szkicPrzekroju.Alignment, true);
        }

        private void widokPikietToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;

            //PikietyForm form = new PikietyForm(_szkicPrzekroju.Punkty);
            PikietyForm form = new PikietyForm(_szkicPrzekroju.Punkty);

            form.ShowDialog(this);
        }

        private void strzalkaPolnocyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_szkicPrzekroju == null) return;
            _szkicPrzekroju.RysujStrzalkaPolnocy(strzalkaPolnocyToolStripMenuItem.Checked);
            mPictureBox.Refresh();
        }

        private void zapiszWszystkieJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ZamknijSzkic()) return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Wybierz szkice do konwersji...";
            dialog.Filter = "*.xml|*.xml";
            dialog.Multiselect = true;

            if (dialog.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

            foreach (string fileName in dialog.FileNames)
            {
                SzkicPrzekroju szkic = SzkicPrzekroju.FromXML(fileName);
                szkic.NowaGranica(_granica);

                _szkicPrzekroju = szkic;
                AktualizujForm(_szkicPrzekroju.ToString());

                mPictureBox.Refresh();

                Bitmap image = new Bitmap(_granica.Width, (int)(_granica.Height * Properties.Settings.Default.ScaleHeight));
                mPictureBox.DrawToBitmap(image, _granica);

                string obrazName = fileName.Replace(".xml", ".png");

                image.Save(obrazName);

                image.Dispose();
            }
        }
        
    }
}
