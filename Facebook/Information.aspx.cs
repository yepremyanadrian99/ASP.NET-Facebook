using System;
using System.Web;

public partial class Information : System.Web.UI.Page
{
    static Action act = new Action();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie user = Request.Cookies["User"];
        if (user == null)
        {
            Response.Redirect("Registration.aspx");
        }
        act.MainUser = act.GetUserData(user["Email"].ToString());
        act.path = MapPath("~/Data");
        if (!IsPostBack)
        {
            picture.ImageUrl = act.GetProfilePicture(act.MainUser["Email"].ToString());
            dayDropDown.Items.Clear();
            monthDropDown.Items.Clear();
            yearDropDown.Items.Clear();
            for (int i = 1; i <= 31; i++)
            {
                dayDropDown.Items.Add(i.ToString());
                if (i <= 12) monthDropDown.Items.Add(i.ToString());
            }
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                yearDropDown.Items.Add(i.ToString());
            }
            nameTextBox.Text = act.MainUser["Name"].ToString();
            surnameTextBox.Text = act.MainUser["Surname"].ToString();
            emailTextBox.Text = act.MainUser["Email"].ToString();
            dayDropDown.Text = act.MainUser["Day"].ToString();
            monthDropDown.Text = act.MainUser["Month"].ToString();
            yearDropDown.Text = act.MainUser["Year"].ToString();
            hometownTextBox.Text = act.MainUser["Hometown"].ToString();
            jobTextBox.Text = act.MainUser["Job"].ToString();
            schoolTextBox.Text = act.MainUser["School"].ToString();
            collegeTextBox.Text = act.MainUser["College"].ToString();
            universityTextBox.Text = act.MainUser["University"].ToString();
        }
        try
        {
            Title = act.MainUser["Name"] + " " + act.MainUser["Surname"];
        }
        catch { }
    }

    protected void okButton_Click(object sender, EventArgs e)
    {
        Save();
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Information.aspx");
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        if (!deleteLabel.Visible)
        {
            deleteLabel.Visible = true;
            cancelDeleteButton.Visible = true;
            okButton.Visible = false;
            cancelButton.Visible = false;
            return;
        }
        act.DeleteUser(act.MainUser["Email"].ToString());
        Logout();
    }

    private void Logout()
    {
        HttpCookie user = new HttpCookie("User");
        user.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(user);
        Response.Redirect("Registration.aspx");
    }

    private void Save()
    {
        if (passwordTextBox.Text == act.MainUser["Password"].ToString() && newPasswordTextBox.Text == confirmPasswordTextBox.Text)
            act.Edit(nameTextBox.Text, surnameTextBox.Text, act.MainUser["Email"].ToString(), newPasswordTextBox.Text, new DateTime(int.Parse(yearDropDown.SelectedValue), int.Parse(monthDropDown.SelectedValue), int.Parse(dayDropDown.SelectedValue)), hometownTextBox.Text, jobTextBox.Text, schoolTextBox.Text, collegeTextBox.Text, universityTextBox.Text);
        else
            act.Edit(nameTextBox.Text, surnameTextBox.Text, act.MainUser["Email"].ToString(), act.MainUser["Password"].ToString(), new DateTime(int.Parse(yearDropDown.SelectedValue), int.Parse(monthDropDown.SelectedValue), int.Parse(dayDropDown.SelectedValue)), hometownTextBox.Text, jobTextBox.Text, schoolTextBox.Text, collegeTextBox.Text, universityTextBox.Text);
    }

    protected void changeProfilePicture_Click(object sender, EventArgs e)
    {
        Save();
        Response.Redirect("Photos.aspx?Profile=" + act.MainUser["Email"].ToString());
    }

    protected void logoutButton_Click(object sender, EventArgs e)
    {
        Logout();
    }
}