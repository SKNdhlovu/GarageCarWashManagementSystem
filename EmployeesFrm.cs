using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarWashManagementSystem
{
    public partial class EmployeesFrm : Form
    {
        public EmployeesFrm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=PCKAKHANYISILE\SQLEXPRESS;Initial Catalog=DbCarWashManagementSystem;Integrated Security=True");
        private void DisplayEmp()
        {
            string sql = "SELECT * FROM tblEmployee";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvEmployee.DataSource = dt;
        }
        private void EmployeesFrm_Load(object sender, EventArgs e)
        {
            DisplayEmp();
        }
        private void ClearContents()
        {
            txtBxEmpName.Clear();
            txtBxEmpSurname.Clear();
            txtBxEmpAddress.Clear();
            txtBxEmpAddress.Clear();
            txtBxPhoneNumber.Clear();
            cmbBxGender.Text = "";
        }
        private void lblLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtBxEmpName.Text ==""||txtBxEmpSurname.Text==""||cmbBxGender.SelectedItem==null||txtBxEmpAddress.Text==""||txtBxPhoneNumber.Text=="")
            {
                MessageBox.Show("Fill in all fields","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                if(conn.State!=ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                        string sql = "INSERT INTO tblEmployee VALUES (@EmpName,@EmpSurname,@EmpGender,@EmpAddress,@EmpPhone)";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@EmpName", txtBxEmpName.Text);
                        cmd.Parameters.AddWithValue("@EmpSurname", txtBxEmpSurname.Text);
                        cmd.Parameters.AddWithValue("@EmpGender", cmbBxGender.Text);
                        cmd.Parameters.AddWithValue("@EmpAddress", txtBxEmpAddress.Text);
                        cmd.Parameters.AddWithValue("@EmpPhone", txtBxPhoneNumber.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearContents();
                        //DisplayEmp();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting to database: "+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE tblEmployee SET EmpName=@EmpName, EmpSurname=@EmpSurname, EmpGender=@EmpGender," +
                        " EmpAddress=@EmpAddress, EmpPhone=@EmpPhone WHERE EmpID=@EmpID";
            
            for (int i = 0; i <= dgvEmployee.Rows.Count - 1; i++)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@EmpID", dgvEmployee.Rows[i].Cells[0].Value);
                cmd.Parameters.AddWithValue("@EmpName",dgvEmployee.Rows[i].Cells[1].Value);
                cmd.Parameters.AddWithValue("@EmpSurname", dgvEmployee.Rows[i].Cells[2].Value);
                cmd.Parameters.AddWithValue("@EmpGender", dgvEmployee.Rows[i].Cells[3].Value);
                cmd.Parameters.AddWithValue("@EmpAddress", dgvEmployee.Rows[i].Cells[4].Value);
                cmd.Parameters.AddWithValue("@EmpPhone", dgvEmployee.Rows[i].Cells[5].Value);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            MessageBox.Show("Information Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayEmp();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("Are you sure your want to delete", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                == DialogResult.Yes)
            {
                int rowIndex = dgvEmployee.CurrentCell.RowIndex;
                dgvEmployee.Rows.RemoveAt(rowIndex);
            }

        }
    }
}
