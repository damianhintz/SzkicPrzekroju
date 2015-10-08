using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SzkicPrzekroju.Domena;

namespace SzkicPrzekroju
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EdytorForm : Form
    {
        Szkic _szkic = null;

        /// <summary>
        /// Szkic
        /// </summary>
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

                this.Text = string.Format("SzkicPP - Edytor [{0}]", _teren.ToString());
            }
        }

        public EdytorForm()
        {
            InitializeComponent();
        }

        private void escapeButton_Click(object sender, EventArgs e)
        {
        }

        private void mPropertyGrid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }
    }
}
