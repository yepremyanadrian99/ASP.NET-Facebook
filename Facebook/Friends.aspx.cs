using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Friends : System.Web.UI.Page
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
        if (act.GetFriends().Count == act.GetFriendRequests().Count && act.GetFriends().Count == 0) Response.Redirect("Profile.aspx");
        Refresh();
        RefreshRequests();
    }

    protected void buttonRefresh_Click(object sender, EventArgs e)
    {
        //Refresh();
        //RefreshRequests();
    }

    void Refresh()
    {
        int online = 0;
        List<DataRow> arr = new List<DataRow>();
        foreach (string email in act.GetFriends())
        {
            DataRow row = act.GetUserData(email);
            try
            {
                row.Table.Columns.Remove("Password");
                row.Table.Columns.Remove("Pic");
                arr.Add(row);
                if (act.IsUserOnline(row["Email"].ToString())) online++;
            }
            catch
            {
                act.DeleteFriend(email);
            }
        }
        Title = string.Format("{0} Friends, {1} Online", arr.Count, online);
        if (!IsPostBack)
        {
            try
            {
                gridViewFriends.DataSource = arr.CopyToDataTable();
            }
            catch { }
            DataBind();
            return;
        }
        if (gridViewFriends.DataSource == null) return;
        try
        {
            if (((List<DataRow>)gridViewFriends.DataSource).Count != arr.Count)
            {
                gridViewFriends.DataSource = arr.CopyToDataTable();
                DataBind();
                return;
            }
            int k = 0;
            for (int i = 0; i < ((List<DataRow>)gridViewFriends.DataSource).Count; i++)
            {
                for (int j = 0; j < arr.Count; j++)
                {
                    if (((List<DataRow>)gridViewFriends.DataSource)[i] == arr[j])
                    {
                        k++;
                        continue;
                    }
                }
            }
            if (k == arr.Count * 2) return;
            gridViewFriends.DataSource = arr.CopyToDataTable();
            DataBind();
        }
        catch
        {
            if (!IsPostBack)
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"You have no friends!\");", true);
        }
    }

    private void RefreshRequests()
    {
        List<DataRow> arr = new List<DataRow>();
        foreach (string email in act.GetFriendRequests())
        {
            try
            {
                DataRow row = act.GetUserData(email);
                row.Table.Columns.Remove("Password");
                row.Table.Columns.Remove("Pic");
                arr.Add(row);
            }
            catch
            {
                act.DeleteFriendRequest(email, true);
            }
        }
        if (arr.Count == 0)
        {
            requestsLabel.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            try
            {
                gridViewRequests.DataSource = arr.CopyToDataTable();
            }
            catch { }
            DataBind();
            return;
        }
        if (gridViewRequests.DataSource == null) return;
        try
        {
            if (((List<DataRow>)gridViewRequests.DataSource).Count != arr.Count)
            {
                gridViewRequests.DataSource = arr.CopyToDataTable();
                DataBind();
                return;
            }
            int k = 0;
            for (int i = 0; i < ((List<DataRow>)gridViewRequests.DataSource).Count; i++)
            {
                for (int j = 0; j < arr.Count; j++)
                {
                    if (((List<DataRow>)gridViewRequests.DataSource)[i] == arr[j])
                    {
                        k++;
                        continue;
                    }
                }
            }
            if (k == arr.Count * 2) return;
            gridViewRequests.DataSource = arr.CopyToDataTable();
            DataBind();
        }
        catch { }
    }

    protected void gridViewFriends_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "View":
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gridViewFriends.Rows[index];
                    Response.Redirect("Search.aspx?Search=" + row.Cells[3].Text);
                    break;
                }
        }
    }

    protected void gridViewRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "View":
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gridViewRequests.Rows[index];
                    Response.Redirect("Search.aspx?Search=" + row.Cells[3].Text);
                    break;
                }
        }
    }
}