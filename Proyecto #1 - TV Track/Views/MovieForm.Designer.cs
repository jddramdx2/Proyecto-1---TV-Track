    namespace Proyecto__1___TV_Track.Views
{
    partial class MovieForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvMovies;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cmbRating;
        private System.Windows.Forms.Button btnRate;
        private System.Windows.Forms.Label lblViewStatus; // 📌 Nuevo: Label para el estado de vista
        private System.Windows.Forms.ComboBox cmbViewStatus; // 📌 Nuevo: ComboBox para el estado de vista
        private System.Windows.Forms.Button btnMarkViewStatus; // 📌 Nuevo: Botón para marcar estado de vista

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvMovies = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblRating = new System.Windows.Forms.Label();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.btnRate = new System.Windows.Forms.Button();
            this.lblViewStatus = new System.Windows.Forms.Label();
            this.cmbViewStatus = new System.Windows.Forms.ComboBox();
            this.btnMarkViewStatus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).BeginInit();
            this.SuspendLayout();
            // 
            // Peliculas
            // 
            this.dgvMovies.ColumnHeadersHeight = 29;
            this.dgvMovies.Location = new System.Drawing.Point(12, 50);
            this.dgvMovies.Name = "dgvMovies";
            this.dgvMovies.ReadOnly = true;
            this.dgvMovies.RowHeadersWidth = 51;
            this.dgvMovies.Size = new System.Drawing.Size(951, 300);
            this.dgvMovies.TabIndex = 0;
            // 
            // caja de busqueda
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 22);
            this.txtSearch.TabIndex = 1;
            // 
            // boton de busqueda
            // 
            this.btnSearch.Location = new System.Drawing.Point(220, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // boton de cierre de sesion
            // 
            this.btnLogout.Location = new System.Drawing.Point(500, 400);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 30);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Cerrar Sesión";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // opciones de calificacion
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(12, 365);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(79, 16);
            this.lblRating.TabIndex = 4;
            this.lblRating.Text = "Calificación:";
            // 
            // grados de calificacion
            // 
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbRating.Location = new System.Drawing.Point(80, 360);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(80, 24);
            this.cmbRating.TabIndex = 5;
            // 
            // boton para realizar la calificacion
            // 
            this.btnRate.Location = new System.Drawing.Point(180, 360);
            this.btnRate.Name = "btnRate";
            this.btnRate.Size = new System.Drawing.Size(80, 23);
            this.btnRate.TabIndex = 4;
            this.btnRate.Text = "Calificar";
            this.btnRate.UseVisualStyleBackColor = true;
            this.btnRate.Click += new System.EventHandler(this.btnRate_Click);
            // 
            // estado de visualizacion
            // 
            this.lblViewStatus.AutoSize = true;
            this.lblViewStatus.Location = new System.Drawing.Point(12, 395);
            this.lblViewStatus.Name = "lblViewStatus";
            this.lblViewStatus.Size = new System.Drawing.Size(105, 16);
            this.lblViewStatus.TabIndex = 6;
            this.lblViewStatus.Text = "Estado de Vista:";
            // 
            // opociones de visualizacion
            // 
            this.cmbViewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewStatus.Items.AddRange(new object[] {
            "Visto",
            "Parcialmente Visto",
            "No Visto"});
            this.cmbViewStatus.Location = new System.Drawing.Point(110, 390);
            this.cmbViewStatus.Name = "cmbViewStatus";
            this.cmbViewStatus.Size = new System.Drawing.Size(120, 24);
            this.cmbViewStatus.TabIndex = 7;
            // 
            // Boton para registro de visualización
            // 
            this.btnMarkViewStatus.Location = new System.Drawing.Point(250, 390);
            this.btnMarkViewStatus.Name = "btnMarkViewStatus";
            this.btnMarkViewStatus.Size = new System.Drawing.Size(100, 23);
            this.btnMarkViewStatus.TabIndex = 5;
            this.btnMarkViewStatus.Text = "Actualizar Estado";
            this.btnMarkViewStatus.UseVisualStyleBackColor = true;
            this.btnMarkViewStatus.Click += new System.EventHandler(this.btnMarkViewStatus_Click);
            // 
            // acceso al formulario de peliculas
            // 
            this.ClientSize = new System.Drawing.Size(975, 450);
            this.Controls.Add(this.dgvMovies);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.cmbRating);
            this.Controls.Add(this.btnRate);
            this.Controls.Add(this.lblViewStatus);
            this.Controls.Add(this.cmbViewStatus);
            this.Controls.Add(this.btnMarkViewStatus);
            this.Name = "MovieForm";
            this.Text = "Catálogo de Películas";
            this.Load += new System.EventHandler(this.MovieForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}