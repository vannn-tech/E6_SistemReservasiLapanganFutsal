namespace ReservasiFutsal02
{
    partial class FormJadwal
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
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblJadwalID = new System.Windows.Forms.Label();
            this.txtJadwalID = new System.Windows.Forms.TextBox();
            this.lblLapangan = new System.Windows.Forms.Label();
            this.cmbLapangan = new System.Windows.Forms.ComboBox();
            this.lblTanggal = new System.Windows.Forms.Label();
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.lblJam = new System.Windows.Forms.Label();
            this.cmbJam = new System.Windows.Forms.ComboBox();
            this.lblStatusJadwal = new System.Windows.Forms.Label();
            this.cmbStatusJadwal = new System.Windows.Forms.ComboBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.dgvJadwal = new System.Windows.Forms.DataGridView();
            this.pnlForm.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.lblTitle);
            this.pnlForm.Controls.Add(this.lblTotal);
            this.pnlForm.Controls.Add(this.lblJadwalID);
            this.pnlForm.Controls.Add(this.txtJadwalID);
            this.pnlForm.Controls.Add(this.lblLapangan);
            this.pnlForm.Controls.Add(this.cmbLapangan);
            this.pnlForm.Controls.Add(this.lblTanggal);
            this.pnlForm.Controls.Add(this.dtpTanggal);
            this.pnlForm.Controls.Add(this.lblJam);
            this.pnlForm.Controls.Add(this.cmbJam);
            this.pnlForm.Controls.Add(this.lblStatusJadwal);
            this.pnlForm.Controls.Add(this.cmbStatusJadwal);
            this.pnlForm.Controls.Add(this.btnSimpan);
            this.pnlForm.Controls.Add(this.btnUbah);
            this.pnlForm.Controls.Add(this.btnHapus);
            this.pnlForm.Controls.Add(this.btnView);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(231, 503);
            this.pnlForm.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(213, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manajemen Jadwal";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotal.Location = new System.Drawing.Point(10, 38);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(213, 16);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total Jadwal: 0";
            // 
            // lblJadwalID
            // 
            this.lblJadwalID.AutoSize = true;
            this.lblJadwalID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblJadwalID.Location = new System.Drawing.Point(10, 66);
            this.lblJadwalID.Name = "lblJadwalID";
            this.lblJadwalID.Size = new System.Drawing.Size(60, 15);
            this.lblJadwalID.TabIndex = 2;
            this.lblJadwalID.Text = "ID Jadwal";
            // 
            // txtJadwalID
            // 
            this.txtJadwalID.Location = new System.Drawing.Point(10, 82);
            this.txtJadwalID.Name = "txtJadwalID";
            this.txtJadwalID.Size = new System.Drawing.Size(86, 20);
            this.txtJadwalID.TabIndex = 3;
            // 
            // lblLapangan
            // 
            this.lblLapangan.AutoSize = true;
            this.lblLapangan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLapangan.Location = new System.Drawing.Point(10, 116);
            this.lblLapangan.Name = "lblLapangan";
            this.lblLapangan.Size = new System.Drawing.Size(59, 15);
            this.lblLapangan.TabIndex = 4;
            this.lblLapangan.Text = "Lapangan";
            // 
            // cmbLapangan
            // 
            this.cmbLapangan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLapangan.Location = new System.Drawing.Point(10, 133);
            this.cmbLapangan.Name = "cmbLapangan";
            this.cmbLapangan.Size = new System.Drawing.Size(211, 21);
            this.cmbLapangan.TabIndex = 5;
            // 
            // lblTanggal
            // 
            this.lblTanggal.AutoSize = true;
            this.lblTanggal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTanggal.Location = new System.Drawing.Point(10, 166);
            this.lblTanggal.Name = "lblTanggal";
            this.lblTanggal.Size = new System.Drawing.Size(49, 15);
            this.lblTanggal.TabIndex = 6;
            this.lblTanggal.Text = "Tanggal";
            // 
            // dtpTanggal
            // 
            this.dtpTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTanggal.Location = new System.Drawing.Point(10, 183);
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Size = new System.Drawing.Size(125, 20);
            this.dtpTanggal.TabIndex = 7;
            // 
            // lblJam
            // 
            this.lblJam.AutoSize = true;
            this.lblJam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblJam.Location = new System.Drawing.Point(10, 217);
            this.lblJam.Name = "lblJam";
            this.lblJam.Size = new System.Drawing.Size(29, 15);
            this.lblJam.TabIndex = 8;
            this.lblJam.Text = "Jam";
            // 
            // cmbJam
            // 
            this.cmbJam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJam.Location = new System.Drawing.Point(10, 233);
            this.cmbJam.Name = "cmbJam";
            this.cmbJam.Size = new System.Drawing.Size(86, 21);
            this.cmbJam.TabIndex = 9;
            // 
            // lblStatusJadwal
            // 
            this.lblStatusJadwal.AutoSize = true;
            this.lblStatusJadwal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusJadwal.Location = new System.Drawing.Point(10, 267);
            this.lblStatusJadwal.Name = "lblStatusJadwal";
            this.lblStatusJadwal.Size = new System.Drawing.Size(42, 15);
            this.lblStatusJadwal.TabIndex = 10;
            this.lblStatusJadwal.Text = "Status";
            // 
            // cmbStatusJadwal
            // 
            this.cmbStatusJadwal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusJadwal.Location = new System.Drawing.Point(10, 283);
            this.cmbStatusJadwal.Name = "cmbStatusJadwal";
            this.cmbStatusJadwal.Size = new System.Drawing.Size(121, 21);
            this.cmbStatusJadwal.TabIndex = 11;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.Location = new System.Drawing.Point(10, 319);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(99, 31);
            this.btnSimpan.TabIndex = 12;
            this.btnSimpan.Text = "💾  Simpan";
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnUbah.Location = new System.Drawing.Point(118, 319);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(99, 31);
            this.btnUbah.TabIndex = 13;
            this.btnUbah.Text = "✏️  Ubah";
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnHapus.Location = new System.Drawing.Point(10, 359);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(99, 31);
            this.btnHapus.TabIndex = 14;
            this.btnHapus.Text = "🗑️  Hapus";
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnView
            // 
            this.btnView.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnView.Location = new System.Drawing.Point(118, 359);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(99, 31);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "👁  Refresh";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.txtCari);
            this.pnlGrid.Controls.Add(this.btnCari);
            this.pnlGrid.Controls.Add(this.dgvJadwal);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(231, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(609, 503);
            this.pnlGrid.TabIndex = 0;
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(10, 14);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(343, 20);
            this.txtCari.TabIndex = 0;
            // 
            // btnCari
            // 
            this.btnCari.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnCari.Location = new System.Drawing.Point(362, 14);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(69, 23);
            this.btnCari.TabIndex = 1;
            this.btnCari.Text = "Cari";
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // dgvJadwal
            // 
            this.dgvJadwal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJadwal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJadwal.Location = new System.Drawing.Point(10, 43);
            this.dgvJadwal.Name = "dgvJadwal";
            this.dgvJadwal.Size = new System.Drawing.Size(587, 448);
            this.dgvJadwal.TabIndex = 2;
            this.dgvJadwal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJadwal_CellClick);
            // 
            // FormJadwal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 503);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlForm);
            this.Name = "FormJadwal";
            this.Text = "Manajemen Jadwal";
            this.Load += new System.EventHandler(this.FormJadwal_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel           pnlForm;
        private System.Windows.Forms.Label           lblTitle;
        private System.Windows.Forms.Label           lblTotal;
        private System.Windows.Forms.Label           lblJadwalID;
        private System.Windows.Forms.TextBox         txtJadwalID;
        private System.Windows.Forms.Label           lblLapangan;
        private System.Windows.Forms.ComboBox        cmbLapangan;
        private System.Windows.Forms.Label           lblTanggal;
        private System.Windows.Forms.DateTimePicker  dtpTanggal;
        private System.Windows.Forms.Label           lblJam;
        private System.Windows.Forms.ComboBox        cmbJam;
        private System.Windows.Forms.Label           lblStatusJadwal;
        private System.Windows.Forms.ComboBox        cmbStatusJadwal;
        private System.Windows.Forms.Button          btnSimpan;
        private System.Windows.Forms.Button          btnUbah;
        private System.Windows.Forms.Button          btnHapus;
        private System.Windows.Forms.Button          btnView;
        private System.Windows.Forms.Panel           pnlGrid;
        private System.Windows.Forms.TextBox         txtCari;
        private System.Windows.Forms.Button          btnCari;
        private System.Windows.Forms.DataGridView    dgvJadwal;
    }
}
