using System;
using System.Web.UI;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;

public partial class ForgotPassword : System.Web.UI.Page
{
    DataSet ds;
    SqlDataAdapter dtp;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (emailTextBox.Text.Length == 0) return;
        if (!new Action().CheckEmail(emailTextBox.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Email not found!\");", true);
            return;
        }
        string password = new Action().RecoverPassword(emailTextBox.Text);
        if (password == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Password???!\");", true);
            return;
        }

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        smtpClient.Credentials = new System.Net.NetworkCredential("yeranikoshamo@gmail.com", "Jailbreak8.1.1");
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        MailMessage mail = new MailMessage();

        //Setting From , To and Sub
        mail.From = new MailAddress("yeranikoshamo@gmail.com", "Facebook");
        mail.To.Add(new MailAddress(emailTextBox.Text));
        mail.Subject = "Facebook Password Recovery";
        mail.Body = string.Format("Your password for {0} is\n\n{1}\n\nThanks for using Facebook\n-Adrian Yepremyan", emailTextBox.Text, password);
        smtpClient.Send(mail);
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Password sent to Email address!\");", true);
    }
}