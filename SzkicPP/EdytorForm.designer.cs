namespace SzkicPrzekroju
{
    partial class EdytorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.escapeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mPropertyGrid
            // 
            this.mPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mPropertyGrid.Location = new System.Drawing.Point(3, 0);
            this.mPropertyGrid.Name = "mPropertyGrid";
            this.mPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.mPropertyGrid.Size = new System.Drawing.Size(527, 402);
            this.mPropertyGrid.TabIndex = 0;
            this.mPropertyGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.mPropertyGrid_PreviewKeyDown);
            // 
            // escapeButton
            // 
            this.escapeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.escapeButton.Location = new System.Drawing.Point(455, 408);
            this.escapeButton.Name = "escapeButton";
            this.escapeButton.Size = new System.Drawing.Size(75, 23);
            this.escapeButton.TabIndex = 1;
            this.escapeButton.Text = "Escape";
            this.escapeButton.UseVisualStyleBackColor = true;
            this.escapeButton.Visible = false;
            this.escapeButton.Click += new System.EventHandler(this.escapeButton_Click);
            // 
            // mEdytorOperatu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.escapeButton;
            this.ClientSize = new System.Drawing.Size(532, 434);
            this.Controls.Add(this.escapeButton);
            this.Controls.Add(this.mPropertyGrid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mEdytorOperatu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SzkicPP - Edytor obiektu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid mPropertyGrid;
        private System.Windows.Forms.Button escapeButton;
    }
}