using System.Drawing;
using System.Windows.Forms;
using SzkicPrzekroju.Domena;

namespace SzkicPrzekroju
{
    /// <summary>
    /// Edytor terenu
    /// </summary>
    public partial class EdytorTerenu : Form
    {
        Szkic _szkic;

        /// <summary>
        /// Dodaj elementy terenu ze szkicu
        /// </summary>
        public Szkic Szkic
        {
            set
            {
                _szkic = value;

                Text += " [" + _szkic.ToString() + "]";

                mListView.SuspendLayout();

                foreach (Teren t in _szkic.Skarpy) DodajTerenItem(t);
                foreach (Teren t in _szkic.Drogi) DodajTerenItem(t);

                DodajTerenItem(_szkic.Koryto);

                mListView.ResumeLayout();
            }
        }

        /// <summary>
        /// Konstruktor edytora (domyślny)
        /// </summary>
        public EdytorTerenu()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Dodaj teren do listy
        /// </summary>
        /// <param name="teren"></param>
        void DodajTerenItem(Teren teren)
        {
            ListViewItem item = new ListViewItem(teren.Zakres);

            item.SubItems.Add(teren.ToString());
            item.SubItems.Add(teren.PierwszaPikieta.ToString());
            item.SubItems.Add(teren.DrugaPikieta.ToString());
            item.Tag = teren;

            if (!teren.RysujNaSzkicu) item.ForeColor = Color.Gray;

            mListView.Items.Add(item);
        }

        /// <summary>
        /// Aktualizuj wyświetlane atrybuty
        /// </summary>
        /// <param name="item"></param>
        void UpdateTerenItem(ListViewItem item)
        {
            Teren teren = item.Tag as Teren;

            item.SubItems[0].Text = teren.Zakres;
            item.SubItems[2].Text = (teren.PierwszaPikieta.ToString());
            item.SubItems[3].Text = (teren.DrugaPikieta.ToString());

            if (teren.RysujNaSzkicu) item.ForeColor = Color.Black;
            else item.ForeColor = Color.Gray;
        }

        /// <summary>
        /// Pokaż edytor terenu po dwukrotnym kliknięciu na obiekt listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in mListView.SelectedItems)
            {
                Teren teren = item.Tag as Teren;

                teren.WymagaZapisu(false);

                EdytorForm edytor = new EdytorForm();
                edytor.ElementTerenu = teren;
                edytor.ShowDialog();

                if (teren.Zmieniony)
                {
                    _szkic.WymagaZapisu(true);
                    UpdateTerenItem(item);
                }
            }
        }
    }
}
