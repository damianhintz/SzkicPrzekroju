namespace SzkicPP
{
    partial class EdytorTerenu
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
            this.mListView = new System.Windows.Forms.ListView();
            this.zakresColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.endColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // mListView
            // 
            this.mListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.zakresColumnHeader,
            this.typColumnHeader,
            this.startColumnHeader,
            this.endColumnHeader});
            this.mListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mListView.FullRowSelect = true;
            this.mListView.Location = new System.Drawing.Point(0, 0);
            this.mListView.MultiSelect = false;
            this.mListView.Name = "mListView";
            this.mListView.Size = new System.Drawing.Size(497, 249);
            this.mListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.mListView.TabIndex = 0;
            this.mListView.UseCompatibleStateImageBehavior = false;
            this.mListView.View = System.Windows.Forms.View.Details;
            this.mListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mListView_MouseDoubleClick);
            // 
            // zakresColumnHeader
            // 
            this.zakresColumnHeader.Text = "Zakres pikiet";
            this.zakresColumnHeader.Width = 102;
            // 
            // typColumnHeader
            // 
            this.typColumnHeader.Text = "Typ terenu";
            this.typColumnHeader.Width = 131;
            // 
            // startColumnHeader
            // 
            this.startColumnHeader.Text = "Piersza pikieta";
            this.startColumnHeader.Width = 112;
            // 
            // endColumnHeader
            // 
            this.endColumnHeader.Text = "Druga pikieta";
            this.endColumnHeader.Width = 94;
            // 
            // EdytorTerenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 249);
            this.Controls.Add(this.mListView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EdytorTerenu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SzkicPP - Edytor terenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView mListView;
        private System.Windows.Forms.ColumnHeader typColumnHeader;
        private System.Windows.Forms.ColumnHeader startColumnHeader;
        private System.Windows.Forms.ColumnHeader endColumnHeader;
        private System.Windows.Forms.ColumnHeader zakresColumnHeader;
    }
}