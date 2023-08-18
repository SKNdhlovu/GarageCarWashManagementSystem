using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWashManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblEmployees_Click(object sender, EventArgs e)
        {
            EmployeesFrm ef = new EmployeesFrm();
            this.Hide();
            ef.Show();
        }

        private void lblServices_Click(object sender, EventArgs e)
        {
            ServicesFrm sf = new ServicesFrm();
            this.Hide();
            sf.Show();
        }

        private void lblFinances_Click(object sender, EventArgs e)
        {
            InvoiceFrm invoiceFrm = new InvoiceFrm();
            this.Hide();
            invoiceFrm.Show();
        }
    }
}
