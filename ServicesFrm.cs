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
    public partial class ServicesFrm : Form
    {
        public ServicesFrm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=PCKAKHANYISILE\SQLEXPRESS;Initial Catalog=DbCarWashManagementSystem;Integrated Security=True");
        private void ServicesFrm_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblEmployee", conn);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                lstBxEmpList.Items.Add(dr["EmpName"]);
            }
            conn.Close();
        }
        private void ClearContents()
        {
            cmbBxServiceName.Text = "";
            cmbBxVehicleType.Text = "";
            lblOutputPrice.Text = "";
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
        private void btnShowPrice_Click(object sender, EventArgs e)
        {
            lblOutputPrice.Visible = true;
            if (cmbBxVehicleType.Text == "SUV/Bakkie")
            {
                string serviceName = cmbBxServiceName.Text;
                switch (serviceName)
                {
                    case "Full Wash":
                        lblOutputPrice.Text = "180";
                        break;
                    case "Car Body Polish & Tyre Shine":
                        lblOutputPrice.Text = "150";
                        break;
                    case "Engine Cleaning":
                        lblOutputPrice.Text = "100";
                        break;
                    case "Wash & Go":
                        lblOutputPrice.Text = "150";
                        break;
                    case "Vacuum Only":
                        lblOutputPrice.Text = "80";
                        break;
                    case "Seat Wash Only":
                        lblOutputPrice.Text = "100";
                        break;
                    default:
                        MessageBox.Show("Select a service");
                        break;
                }
            }
            else
            {
                string serviceName = cmbBxServiceName.Text;
                switch (serviceName)
                {
                    case "Full Wash":
                        lblOutputPrice.Text = "120";
                        break;
                    case "Car Body Polish & Tyre Shine":
                        lblOutputPrice.Text = "100";
                        break;
                    case "Engine Cleaning":
                        lblOutputPrice.Text = "100";
                        break;
                    case "Wash & Go":
                        lblOutputPrice.Text = "100";
                        break;
                    case "Vacuum Only":
                        lblOutputPrice.Text = "80";
                        break;
                    case "Seat Wash Only":
                        lblOutputPrice.Text = "100";
                        break;
                    default:
                        MessageBox.Show("Select a service name");
                        break;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbBxServiceName.SelectedIndex == -1 || cmbBxVehicleType.SelectedIndex == -1 || dtpDate.Text.Length == 0 ||lblOutputPrice.Text==null)
            {
                MessageBox.Show("Fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                        string sql = "INSERT INTO tblServices VALUES (@ServiceName,@ServiceVehicleType,@ServiceDate,@ServicePrice)";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ServiceName", cmbBxServiceName.Text);
                        cmd.Parameters.AddWithValue("@ServiceVehicleType", cmbBxVehicleType.Text);
                        cmd.Parameters.AddWithValue("@ServiceDate", dtpDate.Value);
                        cmd.Parameters.AddWithValue("@ServicePrice", Convert.ToDouble(lblOutputPrice.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sevice Informatioin Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearContents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting to database: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }
        }

        
    }
}
