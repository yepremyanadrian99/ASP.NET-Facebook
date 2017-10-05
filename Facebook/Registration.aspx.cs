using System;
using System.Web;
using System.Web.UI;
using System.IO;

public partial class Registration : System.Web.UI.Page
{
    static Action act = new Action();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Directory.Exists(MapPath("~/Data/Users")))
        {
            Directory.CreateDirectory(MapPath("~/Data/Users"));
        }
        HttpCookie user = new HttpCookie("User");
        act.path = MapPath("~/Data");
        user = Request.Cookies["User"];
        if (user != null)
        {
            Response.Redirect("Profile.aspx");
        }
        if (!IsPostBack)
        {
            dayListBox.Items.Clear();
            monthListBox.Items.Clear();
            yearListBox.Items.Clear();
            dayListBox.Items.Add("Day");
            monthListBox.Items.Add("Month");
            yearListBox.Items.Add("Year");
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                yearListBox.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 31; i++)
            {
                dayListBox.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 12; i++)
            {
                monthListBox.Items.Add(i.ToString());
            }
        }
    }

    protected void loginButton_Click(object sender, EventArgs e)
    {
        if (loginEmailTextBox.Text.Length == 0) return;
        if (loginPasswordTextBox.Text.Length == 0) return;
        if (!act.Login(loginEmailTextBox.Text, loginPasswordTextBox.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Wrong email or password!\");", true);
            return;
        }
        HttpCookie user = new HttpCookie("User");
        user["Email"] = act.GetUserData(loginEmailTextBox.Text)["Email"].ToString();
        if (rememberCheckBox.Checked)
            user.Expires = DateTime.Now.AddYears(1);
        Response.Cookies.Add(user);
        Response.Redirect("Profile.aspx");
    }

    protected void signUpButton_Click(object sender, EventArgs e)
    {
        if (registerPasswordTextBox.Text != registerConfirmPasswordTextBox.Text)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Passwords don't match!\");", true);
            return;
        }
        string gender = "";
        if (Male.Checked) gender = "Male";
        else if (Female.Checked) gender = "Female";
        DateTime birth = new DateTime(int.Parse(yearListBox.SelectedItem.Text), int.Parse(monthListBox.SelectedItem.Text), int.Parse(dayListBox.SelectedItem.Text));
        string mess = act.Register(registerNameTextBox.Text, registerSurnameTextBox.Text, registerEmailTextBox.Text, registerPasswordTextBox.Text, birth, gender);
        if (mess == "You are registered\nYou can now login using your E-mail and Password")
        {
            HttpCookie user = new HttpCookie("User");
            user["Email"] = act.GetUserData(registerEmailTextBox.Text)["Email"].ToString();
            Response.Cookies.Add(user);
            Response.Redirect("Profile.aspx");
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\" " + mess + " \");", true);
    }

    internal void ResetReg()
    {
        registerNameTextBox.Text = "First Name";
        registerSurnameTextBox.Text = "Last Name";
        registerEmailTextBox.Text = "Email";
        registerPasswordTextBox.Text = "Password";
        registerConfirmPasswordTextBox.Text = "Password";
        yearListBox.SelectedIndex = 0;
        monthListBox.SelectedIndex = 0;
        dayListBox.SelectedIndex = 0;
        Male.Checked = false;
        Female.Checked = false;
        loginEmailTextBox.Focus();
    }
}