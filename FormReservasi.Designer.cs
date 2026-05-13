namespace ReservasiFutsal02
{
    partial class FormReservasi
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
            this.lblReservasiID = new System.Windows.Forms.Label();
            this.txtReservasiID = new System.Windows.Forms.TextBox();
            this.lblTanggalReservasi = new System.Windows.Forms.Label();
            this.txtTanggalReservasi = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblLapangan = new System.Windows.Forms.Label();
            this.cmbLapangan = new System.Windows.Forms.ComboBox();
            this.lblJadwal = new System.Windows.Forms.Label();
            this.cmbJadwal = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtJadwalIDRef = new System.Windows.Forms.TextBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnUbahStatus = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.dgvReservasi = new System.Windows.Forms.DataGridView();
            this.pnlForm.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.lblTitle);
            this.pnlForm.Controls.Add(this.lblTotal);
            this.pnlForm.Controls.Add(this.lblReservasiID);
            this.pnlForm.Controls.Add(this.txtReservasiID);
            this.pnlForm.Controls.Add(this.lblTanggalReservasi);
            this.pnlForm.Controls.Add(this.txtTanggalReservasi);
            this.pnlForm.Controls.Add(this.lblUser);
            this.pnlForm.Controls.Add(this.cmbUser);
            this.pnlForm.Controls.Add(this.lblLapangan);
            this.pnlForm.Controls.Add(this.cmbLapangan);
            this.pnlForm.Controls.Add(this.lblJadwal);
            this.pnlForm.Controls.Add(this.cmbJadwal);
            this.pnlForm.Controls.Add(this.lblStatus);
            this.pnlForm.Controls.Add(this.cmbStatus);
            this.pnlForm.Controls.Add(this.txtJadwalIDRef);
            this.pnlForm.Controls.Add(this.btnSimpan);
            this.pnlForm.Controls.Add(this.btnUbahStatus);
            this.pnlForm.Controls.Add(this.btnHapus);
            this.pnlForm.Controls.Add(this.btnView);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(240, 503);
            this.pnlForm.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(219, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manajemen Reservasi";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotal.Location = new System.Drawing.Point(10, 38);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(219, 16);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total: 0   |   Aktif: 0";
            // 
            // lblReservasiID
            // 
            this.lblReservasiID.AutoSize = true;
            this.lblReservasiID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReservasiID.Location = new System.Drawing.Point(10, 66);
            this.lblReservasiID.Name = "lblReservasiID";
            this.lblReservasiID.Size = new System.Drawing.Size(76, 15);
            this.lblReservasiID.TabIndex = 2;
            this.lblReservasiID.Text = "ID Reservasi";
            // 
            // txtReservasiID
            // 
            this.txtReservasiID.Location = new System.Drawing.Point(10, 82);
            this.txtReservasiID.Name = "txtReservasiID";
            this.txtReservasiID.Size = new System.Drawing.Size(73, 20);
            this.txtReservasiID.TabIndex = 3;
            // 
            // lblTanggalReservasi
            // 
            this.lblTanggalReservasi.AutoSize = true;
            this.lblTanggalReservasi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTanggalReservasi.Location = new System.Drawing.Point(10, 116);
            this.lblTanggalReservasi.Name = "lblTanggalReservasi";
            this.lblTanggalReservasi.Size = new System.Drawing.Size(82, 15);
            this.lblTanggalReservasi.TabIndex = 4;
            this.lblTanggalReservasi.Text = "Tgl. Reservasi";
            // 
            // txtTanggalReservasi
            // 
            this.txtTanggalReservasi.Location = new System.Drawing.Point(10, 133);
            this.txtTanggalReservasi.Name = "txtTanggalReservasi";
            this.txtTanggalReservasi.Size = new System.Drawing.Size(134, 20);
            this.txtTanggalReservasi.TabIndex = 5;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.Location = new System.Drawing.Point(10, 166);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(62, 15);
            this.lblUser.TabIndex = 6;
            this.lblUser.Text = "Pengguna";
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.Location = new System.Drawing.Point(10, 183);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(219, 21);
            this.cmbUser.TabIndex = 7;
            // 
            // lblLapangan
            // 
            this.lblLapangan.AutoSize = true;
            this.lblLapangan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLapangan.Location = new System.Drawing.Point(10, 215);
            this.lblLapangan.Name = "lblLapangan";
            this.lblLapangan.Size = new System.Drawing.Size(59, 15);
            this.lblLapangan.TabIndex = 8;
            this.lblLapangan.Text = "Lapangan";
            // 
            // cmbLapangan
            // 
            this.cmbLapangan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLapangan.Location = new System.Drawing.Point(10, 231);
            this.cmbLapangan.Name = "cmbLapangan";
            this.cmbLapangan.Size = new System.Drawing.Size(219, 21);
            this.cmbLapangan.TabIndex = 9;
            this.cmbLapangan.SelectedIndexChanged += new System.EventHandler(this.cmbLapangan_SelectedIndexChanged);
            // 
            // lblJadwal
            // 
            this.lblJadwal.AutoSize = true;
            this.lblJadwal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblJadwal.Location = new System.Drawing.Point(10, 263);
            this.lblJadwal.Name = "lblJadwal";
            this.lblJadwal.Size = new System.Drawing.Size(44, 15);
            this.lblJadwal.TabIndex = 10;
            this.lblJadwal.Text = "Jadwal";
            // 
            // cmbJadwal
            // 
            this.cmbJadwal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJadwal.Location = new System.Drawing.Point(10, 280);
            this.cmbJadwal.Name = "cmbJadwal";
            this.cmbJadwal.Size = new System.Drawing.Size(219, 21);
            this.cmbJadwal.TabIndex = 11;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 312);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new System.Drawing.Point(10, 328);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(129, 21);
            this.cmbStatus.TabIndex = 13;
            // 
            // txtJadwalIDRef
            // 
            this.txtJadwalIDRef.Location = new System.Drawing.Point(0, 0);
            this.txtJadwalIDRef.Name = "txtJadwalIDRef";
            this.txtJadwalIDRef.Size = new System.Drawing.Size(1, 20);
            this.txtJadwalIDRef.TabIndex = 14;
            this.txtJadwalIDRef.Visible = false;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.Location = new System.Drawing.Point(10, 364);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(106, 31);
            this.btnSimpan.TabIndex = 15;
            this.btnSimpan.Text = "💾  Simpan";
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnUbahStatus
            // 
            this.btnUbahStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnUbahStatus.Location = new System.Drawing.Point(123, 364);
            this.btnUbahStatus.Name = "btnUbahStatus";
            this.btnUbahStatus.Size = new System.Drawing.Size(106, 31);
            this.btnUbahStatus.TabIndex = 16;
            this.btnUbahStatus.Text = "✏️  Ubah Status";
            this.btnUbahStatus.Click += new System.EventHandler(this.btnUbahStatus_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnHapus.Location = new System.Drawing.Point(10, 404);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(106, 31);
            this.btnHapus.TabIndex = 17;
            this.btnHapus.Text = "🗑️  Hapus";
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnView
            // 
            this.btnView.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnView.Location = new System.Drawing.Point(123, 404);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(106, 31);
            this.btnView.TabIndex = 18;
            this.btnView.Text = "👁  Refresh";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.txtCari);
            this.pnlGrid.Controls.Add(this.btnCari);
            this.pnlGrid.Controls.Add(this.dgvReservasi);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(240, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(600, 503);
            this.pnlGrid.TabIndex = 0;
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(10, 14);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(335, 20);
            this.txtCari.TabIndex = 0;
            // 
            // btnCari
            // 
            this.btnCari.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnCari.Location = new System.Drawing.Point(353, 14);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(69, 23);
            this.btnCari.TabIndex = 1;
            this.btnCari.Text = "Cari";
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // dgvReservasi
            // 
            this.dgvReservasi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReservasi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReservasi.Location = new System.Drawing.Point(10, 40);
            this.dgvReservasi.Name = "dgvReservasi";
            this.dgvReservasi.Size = new System.Drawing.Size(578, 431);
            this.dgvReservasi.TabIndex = 2;
            this.dgvReservasi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReservasi_CellClick);
            this.dgvReservasi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReservasi_CellContentClick);
            // 
            // FormReservasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 503);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlForm);
            this.Name = "FormReservasi";
            this.Text = "Manajemen Reservasi";
            this.Load += new System.EventHandler(this.FormReservasi_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel          pnlForm;
        private System.Windows.Forms.Label          lblTitle;
        private System.Windows.Forms.Label          lblTotal;
        private System.Windows.Forms.Label          lblReservasiID;
        private System.Windows.Forms.TextBox        txtReservasiID;
        private System.Windows.Forms.Label          lblTanggalReservasi;
        private System.Windows.Forms.TextBox        txtTanggalReservasi;
        private System.Windows.Forms.Label          lblUser;
        private System.Windows.Forms.ComboBox       cmbUser;
        private System.Windows.Forms.Label          lblLapangan;
        private System.Windows.Forms.ComboBox       cmbLapangan;
        private System.Windows.Forms.Label          lblJadwal;
        private System.Windows.Forms.ComboBox       cmbJadwal;
        private System.Windows.Forms.Label          lblStatus;
        private System.Windows.Forms.ComboBox       cmbStatus;
        private System.Windows.Forms.TextBox        txtJadwalIDRef;
        private System.Windows.Forms.Button         btnSimpan;
        private System.Windows.Forms.Button         btnUbahStatus;
        private System.Windows.Forms.Button         btnHapus;
        private System.Windows.Forms.Button         btnView;
        private System.Windows.Forms.Panel          pnlGrid;
        private System.Windows.Forms.TextBox        txtCari;
        private System.Windows.Forms.Button         btnCari;
        private System.Windows.Forms.DataGridView   dgvReservasi;
    }
}
