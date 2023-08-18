using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarWashManagementSystem
{
    public partial class InvoiceFrm : Form
    {
        public InvoiceFrm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=PCKAKHANYISILE\SQLEXPRESS;Initial Catalog=DbCarWashManagementSystem;Integrated Security=True");

        private void DisplayInvoice()
        {
            string sql = "SELECT * FROM tblServices";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvInvoice.DataSource = dt;
        }
        private void lblLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void InvoiceFrm_Load(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayInvoice();
        }

    }
}
