using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace CarWashManagementSystem
{
    public partial class ForgotPasswordFrm : Form
    {
        string rndCode;
        public static string toEmail;
        public ForgotPasswordFrm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string fromEmail, pass, messageBody;

            Random rnd = new Random();
            rndCode = rnd.Next(999999).ToString();

            MailMessage message = new MailMessage();
            toEmail = txtBxEmail.Text;
            fromEmail = "khanyisilendhlovu19@gmail.com";
            pass = "";//Insert password for email
            messageBody = "Your reset code is " + rndCode;
            message.To.Add(toEmail);
            message.From = new MailAddress(fromEmail);
            message.Body = messageBody;
            message.Subject = "Password reseting code ";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(fromEmail, pass);

            try
            {
                smtpClient.Send(message);
                MessageBox.Show("Code sent successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            if(rndCode==txtBxCode.Text)
            {
                toEmail = txtBxEmail.Text;
                txtBxEnterPassword.Focus();
            }
            else
            {
                MessageBox.Show("Wrong code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBxCode.Focus();
            }
        }
        //string username = SendCode.toEmail;
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtBxEnterPassword.Text == txtBxVerifyPassword.Text)
            {
                string sql = "UPDATE[dbo].[tblLogin] SET[Username] = < Username, varchar(50),>,[Password] = < Password, varchar(50),>WHERE < Search Conditions,,> ";
                SqlConnection conn = new SqlConnection(@"Data Source=PCKAKHANYISILE\SQLEXPRESS;Initial Catalog=DbCarWashManagementSystem;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reset Successfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Passwords do not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblReturnToLogin_Click(object sender, EventArgs e)
        {
            SignIn signIn = new SignIn();
            this.Hide();
            signIn.Show();
        }
    }
}
