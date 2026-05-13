namespace ReservasiFutsal02
{
    partial class FormLapangan
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
            this.components = new System.ComponentModel.Container();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnTestInjection = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblLapanganID = new System.Windows.Forms.Label();
            this.txtLapanganID = new System.Windows.Forms.TextBox();
            this.vwLapanganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBFutsalADODataSet = new ReservasiFutsal02.DBFutsalADODataSet();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNamaLapangan = new System.Windows.Forms.TextBox();
            this.lblLokasi = new System.Windows.Forms.Label();
            this.txtLokasi = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTampilkanData = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.dgvLapangan = new System.Windows.Forms.DataGridView();
            this.vw_LapanganTableAdapter = new ReservasiFutsal02.DBFutsalADODataSetTableAdapters.vw_LapanganTableAdapter();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vwLapanganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBFutsalADODataSet)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLapangan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.btnReset);
            this.pnlForm.Controls.Add(this.btnTestInjection);
            this.pnlForm.Controls.Add(this.lblTitle);
            this.pnlForm.Controls.Add(this.lblTotal);
            this.pnlForm.Controls.Add(this.lblLapanganID);
            this.pnlForm.Controls.Add(this.txtLapanganID);
            this.pnlForm.Controls.Add(this.lblNama);
            this.pnlForm.Controls.Add(this.txtNamaLapangan);
            this.pnlForm.Controls.Add(this.lblLokasi);
            this.pnlForm.Controls.Add(this.txtLokasi);
            this.pnlForm.Controls.Add(this.lblStatus);
            this.pnlForm.Controls.Add(this.cmbStatus);
            this.pnlForm.Controls.Add(this.btnSimpan);
            this.pnlForm.Controls.Add(this.btnUbah);
            this.pnlForm.Controls.Add(this.btnHapus);
            this.pnlForm.Controls.Add(this.btnTampilkanData);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(231, 503);
            this.pnlForm.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReset.Location = new System.Drawing.Point(118, 309);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(99, 31);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "❤️  Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnTestInjection
            // 
            this.btnTestInjection.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTestInjection.Location = new System.Drawing.Point(10, 309);
            this.btnTestInjection.Name = "btnTestInjection";
            this.btnTestInjection.Size = new System.Drawing.Size(99, 31);
            this.btnTestInjection.TabIndex = 16;
            this.btnTestInjection.Text = "🗿  Inject";
            this.btnTestInjection.Click += new System.EventHandler(this.btnTestInjection_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(213, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manajemen Lapangan";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotal.Location = new System.Drawing.Point(10, 38);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(213, 16);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total: 0   |   Tersedia: 0";
            // 
            // lblLapanganID
            // 
            this.lblLapanganID.AutoSize = true;
            this.lblLapanganID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLapanganID.Location = new System.Drawing.Point(10, 66);
            this.lblLapanganID.Name = "lblLapanganID";
            this.lblLapanganID.Size = new System.Drawing.Size(75, 15);
            this.lblLapanganID.TabIndex = 2;
            this.lblLapanganID.Text = "ID Lapangan";
            // 
            // txtLapanganID
            // 
            this.txtLapanganID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.vwLapanganBindingSource, "LapanganID", true));
            this.txtLapanganID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLapanganID.Location = new System.Drawing.Point(10, 82);
            this.txtLapanganID.Name = "txtLapanganID";
            this.txtLapanganID.Size = new System.Drawing.Size(103, 23);
            this.txtLapanganID.TabIndex = 3;
            // 
            // vwLapanganBindingSource
            // 
            this.vwLapanganBindingSource.DataMember = "vw_Lapangan";
            this.vwLapanganBindingSource.DataSource = this.dBFutsalADODataSet;
            // 
            // dBFutsalADODataSet
            // 
            this.dBFutsalADODataSet.DataSetName = "DBFutsalADODataSet";
            this.dBFutsalADODataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNama.Location = new System.Drawing.Point(10, 116);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(94, 15);
            this.lblNama.TabIndex = 4;
            this.lblNama.Text = "Nama Lapangan";
            // 
            // txtNamaLapangan
            // 
            this.txtNamaLapangan.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.vwLapanganBindingSource, "NamaLapangan", true));
            this.txtNamaLapangan.Location = new System.Drawing.Point(10, 133);
            this.txtNamaLapangan.Name = "txtNamaLapangan";
            this.txtNamaLapangan.Size = new System.Drawing.Size(211, 20);
            this.txtNamaLapangan.TabIndex = 5;
            this.txtNamaLapangan.TextChanged += new System.EventHandler(this.txtNamaLapangan_TextChanged);
            // 
            // lblLokasi
            // 
            this.lblLokasi.AutoSize = true;
            this.lblLokasi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLokasi.Location = new System.Drawing.Point(10, 166);
            this.lblLokasi.Name = "lblLokasi";
            this.lblLokasi.Size = new System.Drawing.Size(41, 15);
            this.lblLokasi.TabIndex = 6;
            this.lblLokasi.Text = "Lokasi";
            // 
            // txtLokasi
            // 
            this.txtLokasi.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.vwLapanganBindingSource, "Lokasi", true));
            this.txtLokasi.Location = new System.Drawing.Point(10, 183);
            this.txtLokasi.Name = "txtLokasi";
            this.txtLokasi.Size = new System.Drawing.Size(211, 20);
            this.txtLokasi.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 217);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.vwLapanganBindingSource, "Status", true));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbStatus.Location = new System.Drawing.Point(10, 233);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(138, 23);
            this.cmbStatus.TabIndex = 9;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.Location = new System.Drawing.Point(10, 269);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(99, 31);
            this.btnSimpan.TabIndex = 10;
            this.btnSimpan.Text = "💾  Simpan";
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnUbah.Location = new System.Drawing.Point(118, 269);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(99, 31);
            this.btnUbah.TabIndex = 11;
            this.btnUbah.Text = "✏️  Ubah";
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnHapus.Location = new System.Drawing.Point(10, 346);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(99, 31);
            this.btnHapus.TabIndex = 12;
            this.btnHapus.Text = "🗑️  Hapus";
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnTampilkanData
            // 
            this.btnTampilkanData.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnTampilkanData.Location = new System.Drawing.Point(118, 346);
            this.btnTampilkanData.Name = "btnTampilkanData";
            this.btnTampilkanData.Size = new System.Drawing.Size(99, 31);
            this.btnTampilkanData.TabIndex = 13;
            this.btnTampilkanData.Text = "📋  Tampilkan Data";
            this.btnTampilkanData.Click += new System.EventHandler(this.btnTampilkanData_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.txtCari);
            this.pnlGrid.Controls.Add(this.btnCari);
            this.pnlGrid.Controls.Add(this.dgvLapangan);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(231, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(609, 503);
            this.pnlGrid.TabIndex = 0;
            // 
            // txtCari
            // 
            this.txtCari.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCari.Location = new System.Drawing.Point(10, 14);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(343, 23);
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
            // dgvLapangan
            // 
            this.dgvLapangan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLapangan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLapangan.Location = new System.Drawing.Point(10, 43);
            this.dgvLapangan.Name = "dgvLapangan";
            this.dgvLapangan.Size = new System.Drawing.Size(587, 430);
            this.dgvLapangan.TabIndex = 2;
            this.dgvLapangan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLapangan_CellClick);
            // 
            // vw_LapanganTableAdapter
            // 
            this.vw_LapanganTableAdapter.ClearBeforeFill = true;
            // 
            // FormLapangan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 503);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlForm);
            this.Name = "FormLapangan";
            this.Text = "Manajemen Lapangan";
            this.Load += new System.EventHandler(this.FormLapangan_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vwLapanganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBFutsalADODataSet)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLapangan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel           pnlForm;
        private System.Windows.Forms.Label           lblTitle;
        private System.Windows.Forms.Label           lblTotal;
        private System.Windows.Forms.Label           lblLapanganID;
        private System.Windows.Forms.TextBox         txtLapanganID;
        private System.Windows.Forms.Label           lblNama;
        private System.Windows.Forms.TextBox         txtNamaLapangan;
        private System.Windows.Forms.Label           lblLokasi;
        private System.Windows.Forms.TextBox         txtLokasi;
        private System.Windows.Forms.Label           lblStatus;
        private System.Windows.Forms.ComboBox        cmbStatus;
        private System.Windows.Forms.Button          btnSimpan;
        private System.Windows.Forms.Button          btnUbah;
        private System.Windows.Forms.Button          btnHapus;
        private System.Windows.Forms.Button          btnTampilkanData;
        private System.Windows.Forms.Panel           pnlGrid;
        private System.Windows.Forms.TextBox         txtCari;
        private System.Windows.Forms.Button          btnCari;
        private System.Windows.Forms.DataGridView    dgvLapangan;
        private System.Windows.Forms.Button btnTestInjection;
        private System.Windows.Forms.Button btnReset;
        private DBFutsalADODataSet dBFutsalADODataSet;
        private System.Windows.Forms.BindingSource vwLapanganBindingSource;
        private DBFutsalADODataSetTableAdapters.vw_LapanganTableAdapter vw_LapanganTableAdapter;
    }
}
