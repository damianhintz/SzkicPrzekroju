using System;
using System.Windows.Forms;
using SzkicPrzekroju.Domena;

namespace SzkicPrzekroju
{
    public partial class EdytorForm : Form
    {
        Szkic _szkic = null;
        
        public Szkic Szkic
        {
            set
            {
                if (value == null) return;
                mPropertyGrid.SelectedObject = _szkic = value;
                this.Text = string.Format("SzkicPP - Edytor [{0}]", _szkic.ToString());
            }
        }

        ElementSzkicu _elementSzkicu = null;

        /// <summary>
        /// Element szkicu
        /// </summary>
        public ElementSzkicu ElementSzkicu
        {
            set
            {
                if (value == null) return;

                mPropertyGrid.SelectedObject = _elementSzkicu = value;

                this.Text = string.Format("SzkicPP - Edytor [{0}]", _elementSzkicu.ToString());
            }
        }

        Teren _teren = null;

        /// <summary>
        /// Element terenu
        /// </summary>
        public Teren ElementTerenu
        {
            set
            {
                if (value == null) return;

                mPropertyGrid.SelectedObject = _teren = value;

                this.Text = string.Format("SzkicPrzekroju - Edytor [{0}]", _teren.ToString());
            }
        }

        public EdytorForm()
        {
            InitializeComponent();
        }
    }
}
