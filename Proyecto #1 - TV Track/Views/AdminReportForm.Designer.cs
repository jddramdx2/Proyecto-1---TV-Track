namespace Proyecto__1___TV_Track.Views
{
    partial class AdminReportForm
    {
        private System.Windows.Forms.DataGridView dgvReports;
        private System.Windows.Forms.Button btnBack;

        private void InitializeComponent()
        {
            this.dgvReports = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();

            // 📌 DataGridView - Tabla donde se verán los reportes
            this.dgvReports.Location = new System.Drawing.Point(12, 12);
            this.dgvReports.Size = new System.Drawing.Size(600, 300);
            this.dgvReports.ReadOnly = true; // No se puede editar

            // 📌 Botón de "Volver" para regresar al login
            this.btnBack.Text = "Back";
            this.btnBack.Location = new System.Drawing.Point(500, 320);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // 📌 Propiedades del formulario
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.dgvReports);
            this.Controls.Add(this.btnBack);
            this.Text = "Admin Reports"; // Título de la ventana
            this.Load += new System.EventHandler(this.AdminReportForm_Load);
        }
    }
}