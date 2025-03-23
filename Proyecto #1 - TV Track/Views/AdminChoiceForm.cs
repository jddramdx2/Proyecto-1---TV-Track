using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto__1___TV_Track.Views
{
    public partial class AdminChoiceForm : Form
    {
        // Enum to represent admin's selection
        public enum AdminOption
        {
            None,
            MovieCatalog,
            Reports
        }

        public AdminOption SelectedOption { get; private set; } = AdminOption.None;

        public AdminChoiceForm()
        {
            InitializeComponent();
        }

        private void btnOption1_Click(object sender, EventArgs e)
        {
            SelectedOption = AdminOption.MovieCatalog;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            SelectedOption = AdminOption.Reports;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}