using System;
using System.Web;
using System.Web.UI;
using System.Data;

public partial class Search : System.Web.UI.Page
{
    static Action act = new Action();
    static DataRow user;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie user = Request.Cookies["User"];
        if (user == null)
        {
            Response.Redirect("Registration.aspx");
        }
        act.MainUser = act.GetUserData(user["Email"].ToString());
        act.path = MapPath("~/Data");
        try
        {
            Refresh();
        }
        catch { }
        Hide();
        try
        {
            if (Request.QueryString["Search"] != null)
            {
                if (!IsPostBack)
                {
                    searchTextBox.Text = Request.QueryString["Search"];
                    SearchFriend();
                }
                Title = act.GetUserData(Request.QueryString["Search"])["Name"] + " " + act.GetUserData(Request.QueryString["Search"])["Surname"];
            }
            else Title = "Search";
        }
        catch { }
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        if (searchTextBox.Text == String.Empty)
            Response.Redirect("Search.aspx");
        DataRow user = act.GetUserData(searchTextBox.Text);
        if (user == null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"User not found\");", true);
            return;
        }
        Response.Redirect("Search.aspx?Search=" + user["Email"]);
    }

    void SearchFriend()
    {
        try
        {
            Session["Search"] = act.GetUserData(searchTextBox.Text);
            user = (DataRow)Session["Search"];
            if (user == null)
            {
                Hide();
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"User not found!\");", true);
                return;
            }
            picture.ImageUrl = act.GetProfilePicture(user["Email"].ToString());
            nameLabel.Text = user["Name"].ToString();
            surnameLabel.Text = user["Surname"].ToString();
            Show();
            Refresh();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Error occured!\");", true);
        }
    }

    void Refresh()
    {
        user = (DataRow)Session["Search"];
        HttpCookie us = Request.Cookies["User"];
        if (us != null) act.MainUser = act.GetUserData(us["Email"].ToString());
        act.path = MapPath("~/Data");
        addRespondFriendButton.Text = "Add Friend";
        likesLabel.Text = "Likes:" + act.GetLikes(user["Email"].ToString()).Count;
        if (act.GetLikes(user["Email"].ToString()).Contains(act.MainUser["Email"].ToString()))
            likeButton.Text = "Unlike";
        else
            likeButton.Text = "Like";
        if (user["Email"].ToString() == act.MainUser["Email"].ToString())
        {
            onlineLabel.Visible = true;
            onlineLabel.Text = "Online";
            return;
        }
        if (act.GetFriends().Contains(user["Email"].ToString()))
        {
            addRespondFriendButton.Visible = false;
            deleteFriendButton.Visible = true;
            messageButton.Visible = true;
            photosButton.Visible = true;
            onlineLabel.Visible = true;
            if (act.IsUserOnline(user["Email"].ToString()))
            {
                onlineLabel.Text = "Online";
            }
            else
            {
                if (DateTime.Parse(act.UserLastOnline(user["Email"].ToString())).ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    onlineLabel.Text = "Last seen Today " + DateTime.Parse(act.UserLastOnline(user["Email"].ToString())).ToLongTimeString();
                }
                else
                {
                    onlineLabel.Text = "Last seen " + DateTime.Parse(act.UserLastOnline(user["Email"].ToString())).ToShortDateString() + " " + DateTime.Parse(act.UserLastOnline(user["Email"].ToString())).ToShortTimeString();
                }
            }
        }
        else if (act.GetFriendRequests().Contains(user["Email"].ToString()))
        {
            addRespondFriendButton.Text = "Respond to Request";
            addRespondFriendButton.Enabled = false;
            deleteFriendButton.Visible = false;
            yesRespondButton.Visible = true;
            noRespondButton.Visible = true;
        }
        else if (act.GetFriendRequests(user["Email"].ToString()).Contains(act.MainUser["Email"].ToString()))
        {
            addRespondFriendButton.Text = "Pending Request";
            deleteFriendButton.Visible = false;
        }
    }

    void Show()
    {
        nameLabel.Visible = true;
        surnameLabel.Visible = true;
        picture.Visible = true;
        if (user["Email"].ToString() != act.MainUser["Email"].ToString())
        {
            addRespondFriendButton.Visible = true;
            addRespondFriendButton.Enabled = true;
        }
        likeButton.Visible = true;
        likesLabel.Visible = true;
    }

    void Hide()
    {
        nameLabel.Visible = false;
        surnameLabel.Visible = false;
        picture.Visible = false;
        addRespondFriendButton.Visible = false;
        deleteFriendButton.Visible = false;
        yesRespondButton.Visible = false;
        noRespondButton.Visible = false;
        likeButton.Visible = false;
        likesLabel.Visible = false;
        messageButton.Visible = false;
        photosButton.Visible = false;
        onlineLabel.Visible = false;
    }

    protected void addRespondFriendButton_Click(object sender, EventArgs e)
    {
        switch (addRespondFriendButton.Text)
        {
            case "Add Friend":
                {
                    Refresh();
                    act.SendFriendRequest(user["Email"].ToString());
                    break;
                }
            case "Pending Request":
                {
                    Refresh();
                    act.DeleteFriendRequest(user["Email"].ToString(), false);
                    break;
                }
            case "Respond to Request":
                {
                    Refresh();
                    break;
                }
        }
        Hide();
        searchButton_Click(searchButton, EventArgs.Empty);
    }

    protected void deleteFriendButton_Click(object sender, EventArgs e)
    {
        Refresh();
        act.DeleteFriend(user["Email"].ToString());
        Hide();
        searchButton_Click(searchButton, EventArgs.Empty);
    }

    protected void yesRespondButton_Click(object sender, EventArgs e)
    {
        Refresh();
        act.AddFriend(user["Email"].ToString());
        Hide();
        searchButton_Click(searchButton, EventArgs.Empty);
    }

    protected void noRespondButton_Click(object sender, EventArgs e)
    {
        Refresh();
        act.DeleteFriendRequest(user["Email"].ToString(), true);
        Hide();
        searchButton_Click(searchButton, EventArgs.Empty);
    }

    protected void likeButton_Click(object sender, EventArgs e)
    {
        Refresh();
        act.LikeUnlike(user["Email"].ToString());
        Hide();
        searchButton_Click(searchButton, EventArgs.Empty);
    }

    protected void messageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Messenger.aspx");
    }

    protected void photosButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Photos.aspx?Profile=" + user["Email"]);
    }
}