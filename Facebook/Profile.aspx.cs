using System;
using System.Web;
using System.Web.UI;
using System.IO;

public partial class _Default : Page
{
    static Action act = new Action();
    static HttpCookie user = new HttpCookie("User");

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Request.Cookies["User"];
        if (user == null)
        {
            Response.Redirect("Registration.aspx");
            return;
        }
        act.path = MapPath("~/Data");
        act.MainUser = act.GetUserData(user["Email"].ToString());
        Refresh();
    }

    void Refresh()
    {
        try
        {
            nameLabel.Text = act.MainUser["Name"].ToString();
            surnameLabel.Text = act.MainUser["Surname"].ToString();
            //if (!File.Exists(act.GetUserDirectory() + "/Photos/" + act.GetProfilePicture()))
            //    picture.ImageUrl = "~/Data/" + act.MainUser["Gender"] + ".jpg";
            //else
            //    picture.ImageUrl = "~/Data/Users/" + act.MainUser["Email"] + "/Photos/" + act.GetProfilePicture();
            picture.ImageUrl = act.GetProfilePicture(act.MainUser["Email"].ToString());
            act.WriteUserOnline();
            int n = act.GetFriendRequests().Count;
            int m = act.GetFriends().Count;
            friendsButton.Text = m + " Friends";
            if (m == 0) friendsButton.Text = "No Friends";
            else if (m == 1) friendsButton.Text = m + " Friend";
            else if (m > 1) friendsButton.Text = m + " Friends";
            if (n == 1) friendsButton.Text += " (" + n + " Request)";
            else if (n > 1) friendsButton.Text += " (" + n + " Requests)";
            likesLabel.Text = "Likes:" + act.GetLikes(user["Email"].ToString()).Count;
            if (act.GetLikes(user["Email"].ToString()).Contains(act.MainUser["Email"].ToString())) likeButton.Text = "Unlike";
            else likeButton.Text = "Like";
        }
        catch { }
    }

    protected void infoButton_Click(object sender, EventArgs e)
    {
        if (act.MainUser == null) return;
        Response.Redirect("Information.aspx");
    }

    protected void friendsButton_Click(object sender, EventArgs e)
    {
        if (act.MainUser == null) return;
        Response.Redirect("Friends.aspx");
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        if (act.MainUser == null) return;
        if (searchTextBox.Text == "Search Facebook") return;
        Response.Redirect("Search.aspx?Search=" + searchTextBox.Text);
    }

    protected void photosButton_Click(object sender, EventArgs e)
    {
        if (act.MainUser == null) return;
        Response.Redirect("Photos.aspx?Profile=" + act.MainUser["Email"].ToString());
    }

    protected void likeButton_Click(object sender, EventArgs e)
    {
        if (act.MainUser == null) return;
        act.LikeUnlike(user["Email"].ToString());
        Refresh();
    }
}