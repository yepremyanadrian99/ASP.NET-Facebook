using System;
using System.Web;
using System.Data;

public partial class Messenger : System.Web.UI.Page
{
    static Action act = new Action();
    DataRow us;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie user = Request.Cookies["User"];
        if (user == null)
        {
            Response.Redirect("Registration.aspx");
        }
        if (Session["Search"] == null)
        {
            Response.Redirect("Profile.aspx");
        }
        act.MainUser = act.GetUserData(user["Email"].ToString());
        act.path = MapPath("~/Data");
        us = (DataRow)Session["Search"];
        if (us["Email"].ToString() != act.MainUser["Email"].ToString() && !act.GetFriends(act.MainUser["Email"].ToString()).Contains(us["Email"].ToString()))
            Response.Redirect("Profile.aspx");
        Messages.Text = act.ReadMessages(us);
        CloseDelete();
        Title = us["Name"] + " " + us["Surname"];
    }

    protected void sendButton_Click(object sender, EventArgs e)
    {
        if (Message.Text.Length != 0) act.WriteMessage(us, Message.Text);
        Message.Text = "";
    }

    protected void timerRefresh_Tick(object sender, EventArgs e)
    {
        Messages.Text = act.ReadMessages(us);
    }

    protected void deleteMessagesButton_Click(object sender, EventArgs e)
    {
        OpenDelete();
    }

    protected void deleteMessagesOk_Click(object sender, EventArgs e)
    {
        act.DeleteAllMessages(us);
    }

    protected void deleteMessagesCancel_Click(object sender, EventArgs e)
    {
        CloseDelete();
    }

    void OpenDelete()
    {
        deleteMessagesLabel.Visible = true;
        deleteMessagesOk.Visible = true;
        deleteMessagesCancel.Visible = true;
        deleteMessagesButton.Enabled = false;
    }

    void CloseDelete()
    {
        deleteMessagesLabel.Visible = false;
        deleteMessagesOk.Visible = false;
        deleteMessagesCancel.Visible = false;
        deleteMessagesButton.Enabled = true;
    }
}