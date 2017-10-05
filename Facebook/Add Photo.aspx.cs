using System;
using System.Web;

public partial class Add_Photo : System.Web.UI.Page
{
    static Action act = new Action();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie user = Request.Cookies["User"];
        if (user == null)
        {
            Response.Redirect("Registration.aspx");
        }
        act.path = MapPath("~/Data");
        act.MainUser = act.GetUserData(user["Email"].ToString());
    }

    protected void uploadButton_Click(object sender, EventArgs e)
    {
        act.UploadPhoto(Photo.PostedFile, titleTextBox.Text, locationTextBox.Text);
        Response.Redirect("Photos.aspx?Profile=" + act.MainUser["Email"].ToString());
    }
}