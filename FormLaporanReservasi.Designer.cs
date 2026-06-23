namespace ReservasiFutsal02
{
    partial class FormLaporanReservasi
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
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLapangan = new System.Windows.Forms.Label();
            this.cmbLapangan = new System.Windows.Forms.ComboBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.lblTahunFilter = new System.Windows.Forms.Label();
            this.cmbTahunFilter = new System.Windows.Forms.ComboBox();
            this.btnTampilkan = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvLaporan = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.lblTitle);
            this.pnlFilter.Controls.Add(this.lblLapangan);
            this.pnlFilter.Controls.Add(this.cmbLapangan);
            this.pnlFilter.Controls.Add(this.lblStatusFilter);
            this.pnlFilter.Controls.Add(this.cmbStatusFilter);
            this.pnlFilter.Controls.Add(this.lblTahunFilter);
            this.pnlFilter.Controls.Add(this.cmbTahunFilter);
            this.pnlFilter.Controls.Add(this.btnTampilkan);
            this.pnlFilter.Controls.Add(this.lblTotal);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(840, 88);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(167, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🧾  Laporan Reservasi";
            // 
            // lblLapangan
            // 
            this.lblLapangan.AutoSize = true;
            this.lblLapangan.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLapangan.Location = new System.Drawing.Point(12, 38);
            this.lblLapangan.Name = "lblLapangan";
            this.lblLapangan.Size = new System.Drawing.Size(59, 15);
            this.lblLapangan.TabIndex = 1;
            this.lblLapangan.Text = "Lapangan";
            // 
            // cmbLapangan
            // 
            this.cmbLapangan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLapangan.Location = new System.Drawing.Point(12, 56);
            this.cmbLapangan.Name = "cmbLapangan";
            this.cmbLapangan.Size = new System.Drawing.Size(160, 21);
            this.cmbLapangan.TabIndex = 2;
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatusFilter.Location = new System.Drawing.Point(184, 38);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(39, 15);
            this.lblStatusFilter.TabIndex = 3;
            this.lblStatusFilter.Text = "Status";
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Location = new System.Drawing.Point(184, 56);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(130, 21);
            this.cmbStatusFilter.TabIndex = 4;
            // 
            // lblTahunFilter
            // 
            this.lblTahunFilter.AutoSize = true;
            this.lblTahunFilter.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTahunFilter.Location = new System.Drawing.Point(326, 38);
            this.lblTahunFilter.Name = "lblTahunFilter";
            this.lblTahunFilter.Size = new System.Drawing.Size(40, 15);
            this.lblTahunFilter.TabIndex = 5;
            this.lblTahunFilter.Text = "Tahun";
            // 
            // cmbTahunFilter
            // 
            this.cmbTahunFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTahunFilter.Location = new System.Drawing.Point(326, 56);
            this.cmbTahunFilter.Name = "cmbTahunFilter";
            this.cmbTahunFilter.Size = new System.Drawing.Size(110, 21);
            this.cmbTahunFilter.TabIndex = 6;
            // 
            // btnTampilkan
            // 
            this.btnTampilkan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTampilkan.Location = new System.Drawing.Point(450, 55);
            this.btnTampilkan.Name = "btnTampilkan";
            this.btnTampilkan.Size = new System.Drawing.Size(110, 25);
            this.btnTampilkan.TabIndex = 7;
            this.btnTampilkan.Text = "📋  Tampilkan";
            this.btnTampilkan.Click += new System.EventHandler(this.btnTampilkan_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotal.Location = new System.Drawing.Point(580, 60);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(240, 16);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total: 0 reservasi";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvLaporan
            // 
            this.dgvLaporan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLaporan.Location = new System.Drawing.Point(0, 88);
            this.dgvLaporan.Name = "dgvLaporan";
            this.dgvLaporan.Size = new System.Drawing.Size(840, 365);
            this.dgvLaporan.TabIndex = 1;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnExportCsv);
            this.pnlBottom.Controls.Add(this.btnExportPdf);
            this.pnlBottom.Controls.Add(this.btnPrintPreview);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 453);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(840, 50);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExportCsv.Location = new System.Drawing.Point(12, 10);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(130, 30);
            this.btnExportCsv.TabIndex = 0;
            this.btnExportCsv.Text = "⬇️  Export CSV";
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExportPdf.Location = new System.Drawing.Point(150, 10);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(130, 30);
            this.btnExportPdf.TabIndex = 1;
            this.btnExportPdf.Text = "⬇️  Export PDF";
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPrintPreview.Location = new System.Drawing.Point(288, 10);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(130, 30);
            this.btnPrintPreview.TabIndex = 2;
            this.btnPrintPreview.Text = "🖨️  Print Preview";
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // FormLaporanReservasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 503);
            this.Controls.Add(this.dgvLaporan);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlFilter);
            this.Name = "FormLaporanReservasi";
            this.Text = "Laporan Reservasi";
            this.Load += new System.EventHandler(this.FormLaporanReservasi_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaporan)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel       pnlFilter;
        private System.Windows.Forms.Label       lblTitle;
        private System.Windows.Forms.Label       lblLapangan;
        private System.Windows.Forms.ComboBox    cmbLapangan;
        private System.Windows.Forms.Label       lblStatusFilter;
        private System.Windows.Forms.ComboBox    cmbStatusFilter;
        private System.Windows.Forms.Label       lblTahunFilter;
        private System.Windows.Forms.ComboBox    cmbTahunFilter;
        private System.Windows.Forms.Button      btnTampilkan;
        private System.Windows.Forms.Label       lblTotal;
        private System.Windows.Forms.DataGridView dgvLaporan;
        private System.Windows.Forms.Panel       pnlBottom;
        private System.Windows.Forms.Button      btnExportCsv;
        private System.Windows.Forms.Button      btnExportPdf;
        private System.Windows.Forms.Button      btnPrintPreview;
    }
}
