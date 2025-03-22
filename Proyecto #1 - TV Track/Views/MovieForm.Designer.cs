namespace Proyecto__1___TV_Track.Views
{
    partial class MovieForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvMovies;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;

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

            this.dgvMovies.Location = new System.Drawing.Point(12, 50);
            this.dgvMovies.Size = new System.Drawing.Size(600, 300);
            this.dgvMovies.ReadOnly = true;

            this.txtSearch.Location = new System.Drawing.Point(12, 12);
            this.txtSearch.Size = new System.Drawing.Size(200, 20);

            this.btnSearch.Text = "Buscar";
            this.btnSearch.Location = new System.Drawing.Point(220, 10);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.dgvMovies);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Text = "Catálogo de Películas";
            this.Load += new System.EventHandler(this.MovieForm_Load);
        }
    }
}
