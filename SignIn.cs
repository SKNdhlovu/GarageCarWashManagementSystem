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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=PCKAKHANYISILE\SQLEXPRESS;Initial Catalog=DbCarWashManagementSystem;Integrated Security=True");

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordFrm fpf = new ForgotPasswordFrm();
            this.Hide();
            fpf.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtBxUsername.Text == "" || txtBxPassword.Text=="")
            {
                MessageBox.Show("Provide in all information","Alert",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {

                if(conn.State!=ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                        string sql = "SELECT * FROM tblLogin WHERE Username = '" + txtBxUsername.Text + "' and Password = '" + txtBxPassword.Text + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //MessageBox.Show("Login Successfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MainForm mf = new MainForm();
                            this.Hide();
                            mf.Show();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect username or/and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBxPassword.Clear();
                            txtBxUsername.Focus();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error connecting to database: " +ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }
            
            


        }

        private void lblRegisterHere_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            this.Hide();
            signUp.Show();
        }

        private void chkBxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chkBxShowPassword.Checked==true)
            {
                txtBxPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtBxPassword.UseSystemPasswordChar = false;
            }
        }
    }
}
