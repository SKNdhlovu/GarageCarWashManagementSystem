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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=PCKAKHANYISILE\SQLEXPRESS;Initial Catalog=DbCarWashManagementSystem;Integrated Security=True");

        private void picBxExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            SignIn si = new SignIn();
            this.Hide();
            si.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtBxUsername.Text == "" || txtBxPassword.Text == "" || txtBxConPassword.Text == "")
            {
                MessageBox.Show("Provide in all information", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                        //checks if the username does not exist already
                        string sql = "SELECT * FROM tblLogin WHERE Username='" + txtBxUsername.Text + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show(txtBxUsername.Text+" already exists.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (txtBxPassword.Text==txtBxConPassword.Text)
                            {
                                string sqlInsert = "INSERT INTO tblLogin VALUES(@Username,@Password)";
                                SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
                                cmdInsert.Parameters.AddWithValue("@Username", txtBxUsername.Text);
                                cmdInsert.Parameters.AddWithValue("@Password", txtBxPassword.Text);
                                cmdInsert.ExecuteNonQuery();
                                MessageBox.Show("Registered Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                SignIn si = new SignIn();
                                this.Hide();
                                si.Show();
                            }
                            else
                            {
                                MessageBox.Show("Passwords do not match", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
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
