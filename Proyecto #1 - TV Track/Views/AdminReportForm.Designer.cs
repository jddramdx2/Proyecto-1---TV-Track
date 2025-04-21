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
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.SuspendLayout();
            // 
            // Reportes
            // 
            this.dgvReports.ColumnHeadersHeight = 29;
            this.dgvReports.Location = new System.Drawing.Point(12, 12);
            this.dgvReports.Name = "dgvReports";
            this.dgvReports.ReadOnly = true;
            this.dgvReports.RowHeadersWidth = 51;
            this.dgvReports.Size = new System.Drawing.Size(859, 300);
            this.dgvReports.TabIndex = 0;
            // 
            // Boton de regresar
            // 
            this.btnBack.Location = new System.Drawing.Point(388, 333);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Formulario de Reportes del Administrador
            // 
            this.ClientSize = new System.Drawing.Size(883, 400);
            this.Controls.Add(this.dgvReports);
            this.Controls.Add(this.btnBack);
            this.Name = "AdminReportForm";
            this.Text = "Admin Reports";
            this.Load += new System.EventHandler(this.AdminReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.ResumeLayout(false);

        }
    }
}