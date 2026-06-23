/* ════════════════════════════════════════════════════════════════════════
   FormCetakLaporan.Designer.cs — lihat catatan lengkap di FormCetakLaporan.cs
   tentang cara mengaktifkan file ini.

   Designer di bawah mengasumsikan reference ke
   CrystalDecisions.Windows.Forms SUDAH ditambahkan ke project (lihat
   langkah b pada FormCetakLaporan.cs). Sebelum reference itu ada,
   file ini TIDAK akan compile — itulah sebabnya folder
   "OptionalCrystalReport" ini SENGAJA tidak diikutkan ke .csproj utama.
   ════════════════════════════════════════════════════════════════════════ */
namespace ReservasiFutsal02
{
    partial class FormCetakLaporan
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
            this.pnlPlaceholder = new System.Windows.Forms.Panel();
            this.lblPlaceholder = new System.Windows.Forms.Label();
            this.pnlPlaceholder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPlaceholder
            // 
            this.pnlPlaceholder.Controls.Add(this.lblPlaceholder);
            this.pnlPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.pnlPlaceholder.Name = "pnlPlaceholder";
            this.pnlPlaceholder.Size = new System.Drawing.Size(900, 600);
            this.pnlPlaceholder.TabIndex = 0;
            // 
            // lblPlaceholder
            // 
            this.lblPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlaceholder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.lblPlaceholder.Name = "lblPlaceholder";
            this.lblPlaceholder.Size = new System.Drawing.Size(900, 600);
            this.lblPlaceholder.TabIndex = 0;
            this.lblPlaceholder.Text = "CrystalReportViewer akan muncul di sini setelah\nCrystal Reports runtime diaktifka" +
    "n.\nLihat instruksi di FormCetakLaporan.cs";
            this.lblPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlaceholder.Click += new System.EventHandler(this.lblPlaceholder_Click);
            // 
            // FormCetakLaporan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pnlPlaceholder);
            this.Name = "FormCetakLaporan";
            this.Text = "Cetak Laporan Reservasi — Crystal Report";
            this.pnlPlaceholder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPlaceholder;
        private System.Windows.Forms.Label lblPlaceholder;
    }
}
