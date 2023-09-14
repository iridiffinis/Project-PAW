
namespace Inchirieri_de_casete_video
{
    partial class FormFilme
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stergeSelectiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaRandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acceseazaBazaDeDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaCostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imDb1 = new Inchirieri_de_casete_video.IMDb();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1218, 537);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Moccasin;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stergeSelectiaToolStripMenuItem,
            this.adaugaRandToolStripMenuItem,
            this.acceseazaBazaDeDateToolStripMenuItem,
            this.adaugaCostToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1218, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stergeSelectiaToolStripMenuItem
            // 
            this.stergeSelectiaToolStripMenuItem.Name = "stergeSelectiaToolStripMenuItem";
            this.stergeSelectiaToolStripMenuItem.Size = new System.Drawing.Size(105, 21);
            this.stergeSelectiaToolStripMenuItem.Text = "Sterge selectia";
            this.stergeSelectiaToolStripMenuItem.Click += new System.EventHandler(this.stergeSelectiaToolStripMenuItem_Click);
            // 
            // adaugaRandToolStripMenuItem
            // 
            this.adaugaRandToolStripMenuItem.Name = "adaugaRandToolStripMenuItem";
            this.adaugaRandToolStripMenuItem.Size = new System.Drawing.Size(137, 21);
            this.adaugaRandToolStripMenuItem.Text = "Salveaza randul nou";
            this.adaugaRandToolStripMenuItem.Click += new System.EventHandler(this.adaugaRandToolStripMenuItem_Click);
            // 
            // acceseazaBazaDeDateToolStripMenuItem
            // 
            this.acceseazaBazaDeDateToolStripMenuItem.Name = "acceseazaBazaDeDateToolStripMenuItem";
            this.acceseazaBazaDeDateToolStripMenuItem.Size = new System.Drawing.Size(161, 21);
            this.acceseazaBazaDeDateToolStripMenuItem.Text = "Acceseaza baza de date";
            this.acceseazaBazaDeDateToolStripMenuItem.Click += new System.EventHandler(this.acceseazaBazaDeDateToolStripMenuItem_Click);
            // 
            // adaugaCostToolStripMenuItem
            // 
            this.adaugaCostToolStripMenuItem.Name = "adaugaCostToolStripMenuItem";
            this.adaugaCostToolStripMenuItem.Size = new System.Drawing.Size(99, 21);
            this.adaugaCostToolStripMenuItem.Text = "Modifica cost";
            this.adaugaCostToolStripMenuItem.Click += new System.EventHandler(this.adaugaCostToolStripMenuItem_Click);
            // 
            // imDb1
            // 
            this.imDb1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.imDb1.BackColor = System.Drawing.Color.Transparent;
            this.imDb1.Location = new System.Drawing.Point(933, 440);
            this.imDb1.Name = "imDb1";
            this.imDb1.Size = new System.Drawing.Size(285, 122);
            this.imDb1.TabIndex = 2;
            // 
            // FormFilme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1218, 562);
            this.Controls.Add(this.imDb1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormFilme";
            this.Text = "Colectie filme";
            this.Load += new System.EventHandler(this.FormFilme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stergeSelectiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaugaRandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acceseazaBazaDeDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaugaCostToolStripMenuItem;
        private IMDb imDb1;
    }
}