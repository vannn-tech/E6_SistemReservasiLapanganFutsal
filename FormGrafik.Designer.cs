namespace ReservasiFutsal02
{
    partial class FormGrafik
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components   = new System.ComponentModel.Container();
            this.pnlTop       = new System.Windows.Forms.Panel();
            this.lblTitle     = new System.Windows.Forms.Label();
            this.lblTampilan  = new System.Windows.Forms.Label();
            this.cmbTampilan  = new System.Windows.Forms.ComboBox();
            this.lblTahun     = new System.Windows.Forms.Label();
            this.cmbTahun     = new System.Windows.Forms.ComboBox();
            this.btnLoad      = new System.Windows.Forms.Button();
            this.btnReset     = new System.Windows.Forms.Button();
            this.chartReservasi = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartReservasi)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.lblTampilan);
            this.pnlTop.Controls.Add(this.cmbTampilan);
            this.pnlTop.Controls.Add(this.lblTahun);
            this.pnlTop.Controls.Add(this.cmbTahun);
            this.pnlTop.Controls.Add(this.btnLoad);
            this.pnlTop.Controls.Add(this.btnReset);
            this.pnlTop.Dock     = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name     = "pnlTop";
            this.pnlTop.Size     = new System.Drawing.Size(840, 64);
            this.pnlTop.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(12, 8);
            this.lblTitle.Name      = "lblTitle";
            this.lblTitle.Size      = new System.Drawing.Size(260, 20);
            this.lblTitle.TabIndex  = 0;
            this.lblTitle.Text      = "📊  Dashboard & Grafik Reservasi";

            // 
            // lblTampilan
            // 
            this.lblTampilan.AutoSize = true;
            this.lblTampilan.Font     = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTampilan.Location = new System.Drawing.Point(12, 36);
            this.lblTampilan.Name     = "lblTampilan";
            this.lblTampilan.Size     = new System.Drawing.Size(56, 15);
            this.lblTampilan.TabIndex = 1;
            this.lblTampilan.Text     = "Tampilan";

            // 
            // cmbTampilan
            // 
            this.cmbTampilan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTampilan.Location = new System.Drawing.Point(75, 32);
            this.cmbTampilan.Name     = "cmbTampilan";
            this.cmbTampilan.Size     = new System.Drawing.Size(140, 23);
            this.cmbTampilan.TabIndex = 2;
            this.cmbTampilan.SelectedIndexChanged += new System.EventHandler(this.cmbTampilan_SelectedIndexChanged);

            // 
            // lblTahun
            // 
            this.lblTahun.AutoSize = true;
            this.lblTahun.Font     = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTahun.Location = new System.Drawing.Point(228, 36);
            this.lblTahun.Name     = "lblTahun";
            this.lblTahun.Size     = new System.Drawing.Size(38, 15);
            this.lblTahun.TabIndex = 3;
            this.lblTahun.Text     = "Tahun";

            // 
            // cmbTahun
            // 
            this.cmbTahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTahun.Location = new System.Drawing.Point(272, 32);
            this.cmbTahun.Name     = "cmbTahun";
            this.cmbTahun.Size     = new System.Drawing.Size(110, 23);
            this.cmbTahun.TabIndex = 4;
            this.cmbTahun.SelectedIndexChanged += new System.EventHandler(this.cmbTahun_SelectedIndexChanged);

            // 
            // btnLoad
            // 
            this.btnLoad.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Location = new System.Drawing.Point(396, 31);
            this.btnLoad.Name     = "btnLoad";
            this.btnLoad.Size     = new System.Drawing.Size(80, 25);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text     = "Load";
            this.btnLoad.Click   += new System.EventHandler(this.btnLoad_Click);

            // 
            // btnReset
            // 
            this.btnReset.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReset.Location = new System.Drawing.Point(484, 31);
            this.btnReset.Name     = "btnReset";
            this.btnReset.Size     = new System.Drawing.Size(80, 25);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text     = "Reset";
            this.btnReset.Click   += new System.EventHandler(this.btnReset_Click);

            // 
            // chartReservasi
            // 
            this.chartReservasi.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.chartReservasi.Location = new System.Drawing.Point(0, 64);
            this.chartReservasi.Name     = "chartReservasi";
            this.chartReservasi.Size     = new System.Drawing.Size(840, 439);
            this.chartReservasi.TabIndex = 1;
            this.chartReservasi.Text     = "chartReservasi";

            // 
            // FormGrafik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize     = new System.Drawing.Size(840, 503);
            this.Controls.Add(this.chartReservasi);
            this.Controls.Add(this.pnlTop);
            this.Name           = "FormGrafik";
            this.Text           = "Dashboard & Grafik";
            this.Load          += new System.EventHandler(this.FormGrafik_Load);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartReservasi)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel      pnlTop;
        private System.Windows.Forms.Label      lblTitle;
        private System.Windows.Forms.Label      lblTampilan;
        private System.Windows.Forms.ComboBox   cmbTampilan;
        private System.Windows.Forms.Label      lblTahun;
        private System.Windows.Forms.ComboBox   cmbTahun;
        private System.Windows.Forms.Button     btnLoad;
        private System.Windows.Forms.Button     btnReset;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReservasi;
    }
}
