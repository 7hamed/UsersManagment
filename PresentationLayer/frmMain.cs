using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsersManagment
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void _RefreshDataGridView()
        {
            dgvUsers.DataSource = clsUsers.GetAllUsers();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            _RefreshDataGridView();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form form = new frmAddUpdateUsers(-1);
            form.ShowDialog();

            _RefreshDataGridView();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmAddUpdateUsers((int)dgvUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();

            _RefreshDataGridView();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUsers.Delete((int)dgvUsers.CurrentRow.Cells[0].Value);

            _RefreshDataGridView();
        }
    }
}
