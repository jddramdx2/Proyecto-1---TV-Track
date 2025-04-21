namespace Proyecto__1___TV_Track.Views
{
    partial class AdminChoiceForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnOption1;
        private System.Windows.Forms.Button btnOption2;
        private System.Windows.Forms.Label lblMessage;

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
            this.btnOption1 = new System.Windows.Forms.Button();
            this.btnOption2 = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Boton Opción 1
            // 
            this.btnOption1.Location = new System.Drawing.Point(50, 50);
            this.btnOption1.Name = "btnOption1";
            this.btnOption1.Size = new System.Drawing.Size(200, 30);
            this.btnOption1.TabIndex = 1;
            this.btnOption1.Text = "Opción 1: Catálogo de Películas";
            this.btnOption1.UseVisualStyleBackColor = true;
            this.btnOption1.Click += new System.EventHandler(this.btnOption1_Click);
            // 
            // Boton Opción 1
            // 
            this.btnOption2.Location = new System.Drawing.Point(50, 90);
            this.btnOption2.Name = "btnOption2";
            this.btnOption2.Size = new System.Drawing.Size(200, 30);
            this.btnOption2.TabIndex = 2;
            this.btnOption2.Text = "Opción 2: Reportes de Administrador";
            this.btnOption2.UseVisualStyleBackColor = true;
            this.btnOption2.Click += new System.EventHandler(this.btnOption2_Click);
            // 
            // mensaje
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(50, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(176, 16);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Seleccione qué desea abrir:";
            // 
            // Formulario de opciones del administrador
            // 
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnOption1);
            this.Controls.Add(this.btnOption2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AdminChoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones para Administrador";
            this.Load += new System.EventHandler(this.AdminChoiceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}