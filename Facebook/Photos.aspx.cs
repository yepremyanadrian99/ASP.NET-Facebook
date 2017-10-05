using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

public partial class Photos : System.Web.UI.Page
{
    static Action act = new Action();
    static DataRow user;
    static int imgIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie us = Request.Cookies["User"];
        if (us == null) Response.Redirect("Registration.aspx");
        user = act.GetUserData(Request.QueryString["Profile"]);
        if (user == null) Response.Redirect("Profile.aspx");
        Title = "Photos - " + user["Name"] + " " + user["Surname"];
        act.path = MapPath("~/Data");
        act.MainUser = act.GetUserData(us["Email"].ToString());
        if (user["Email"].ToString() != act.MainUser["Email"].ToString() && !act.GetFriends(act.MainUser["Email"].ToString()).Contains(user["Email"].ToString()))
            Response.Redirect("Profile.aspx");
        Refresh();
    }

    void Refresh()
    {
        imagesPanel.Controls.Clear();
        imagesPanel.Style["position"] = "relative";
        List<Photo> arr = act.GetUserPhotos(user["Email"].ToString());
        if (arr.Count == 0)
        {
            imgIndex = 0;
            picture.Visible = false;
            nextButton.Visible = false;
            previousButton.Visible = false;
            likeUnlikeButton.Visible = false;
            makeProfileButton.Visible = false;
            deleteButton.Visible = false;
            if (user["Email"].ToString() != act.MainUser["Email"].ToString())
                addPhotoButton.Visible = false;
            likesLabel.Text = "No Photos";
            return;
        }
        if (imgIndex > arr.Count - 1) imgIndex = 0;
        for (int i = 0; i < arr.Count; i++)
        {
            Image img = new Image();
            img.Width = 220;
            img.Height = 220;
            img.ID = arr[i].Date.ToString();
            img.ImageUrl = "~/Data/Users/" + user["Email"].ToString() + "/Photos/" + arr[i].Path + "/Photo.jpg";
            imagesPanel.Controls.Add(img);
        }
        likesLabel.Text = "Likes: " + act.GetPhotoLikes(user["Email"].ToString(), arr[imgIndex].Path).Count;
        if (Request.QueryString["Photo"] != arr[imgIndex].Path)
            Response.Redirect("Photos.aspx?Profile=" + user["Email"] + "&&Photo=" + arr[imgIndex].Path);
        picture.ImageUrl = "~/Data/Users/" + user["Email"] + "/Photos/" + arr[imgIndex].Path + "/Photo.jpg";
        if (act.GetPhotoLikes(user["Email"].ToString(), arr[imgIndex].Path).Contains(act.MainUser["Email"].ToString()))
            likeUnlikeButton.Text = "Unlike";
        else
            likeUnlikeButton.Text = "Like";
        if (user["Email"].ToString() != act.MainUser["Email"].ToString())
        {
            addPhotoButton.Visible = false;
            makeProfileButton.Visible = false;
            deleteButton.Visible = false;
        }
        else
        {
            addPhotoButton.Visible = true;
            if (act.IsProfilePicture(act.GetUserPhotos(user["Email"].ToString())[imgIndex].Path + "/Photo.jpg"))
                makeProfileButton.Visible = false;
        }
    }

    protected void addPhotoButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Add Photo.aspx");
    }

    protected void nextButton_Click(object sender, EventArgs e)
    {
        List<Photo> arr = act.GetUserPhotos(user["Email"].ToString());
        if (imgIndex == arr.Count - 1)
            imgIndex = 0;
        else
            imgIndex++;
        Response.Redirect("Photos.aspx?Profile=" + user["Email"] + "&&Photo=" + arr[imgIndex].Path);
    }

    protected void previousButton_Click(object sender, EventArgs e)
    {
        List<Photo> arr = act.GetUserPhotos(user["Email"].ToString());
        if (imgIndex == 0)
            imgIndex = arr.Count - 1;
        else
            imgIndex--;
        Response.Redirect("Photos.aspx?Profile=" + user["Email"] + "&&Photo=" + arr[imgIndex].Path);
    }

    protected void likeUnlikeButton_Click(object sender, EventArgs e)
    {
        List<Photo> arr = act.GetUserPhotos(user["Email"].ToString());
        Photo ph = arr[imgIndex];
        act.LikeUnlikePhoto(user["Email"].ToString(), ph.Path);
        Refresh();
    }

    protected void makeProfileButton_Click(object sender, EventArgs e)
    {
        act.MakeProfilePicture(act.GetUserPhotos(user["Email"].ToString())[imgIndex].Path);
        Refresh();
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        act.DeletePhoto(act.GetUserPhotos(user["Email"].ToString())[imgIndex].Path);
        Refresh();
    }
}