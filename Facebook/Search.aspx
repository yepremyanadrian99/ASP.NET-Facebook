<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body style="background-color:lightgray">
    <form id="form1" runat="server">
         <div style="background-color:steelblue; color:white">
            <table>
                <tr>
                    <th style="font-size:xx-large ;color:white; height:37px; text-shadow:1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000" >
                        <asp:HyperLink Font-Underline="false" ForeColor="White" Font-Bold="true" runat="server" NavigateUrl="~/Profile.aspx">
                        Facebook 
                        </asp:HyperLink>
                    </th>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <center>
                <table>
                    <tr>
                        <td>
                            Email <asp:TextBox ID="searchTextBox" TextMode="Search" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="searchButton" Text="Search" OnClick="searchButton_Click" runat="server" />
                        </td>
                    </tr>
                </table>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                <asp:Image ID="picture" runat="server" Visible="false" Width="500" Height="500" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="nameLabel" runat="server" Visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="surnameLabel" runat="server" Visible="false" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="likesLabel" Text="Likes: 0" runat="server"  Visible="false" />&nbsp&nbsp
                <asp:Label ID="onlineLabel" Text="Last Seen: N/A" runat="server" Visible="false" />
                <table>
                    <tr>
                        <td><asp:Button ID="likeButton" Text="Like" runat="server" Visible="false" OnClick="likeButton_Click" /></td>
                        <td><asp:Button ID="addRespondFriendButton" Text="Add Friend" runat="server" OnClick="addRespondFriendButton_Click" /></td>
                        <td><asp:Button ID="deleteFriendButton" Text="Delete Friend" runat="server" OnClick="deleteFriendButton_Click" /></td>
                        <td><asp:Button ID="photosButton" Text="Photos" runat="server" Visible="false" OnClick="photosButton_Click" /></td>
                        <td><asp:Button ID="messageButton" Text="Send Message" runat="server" Visible="false" OnClick="messageButton_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="yesRespondButton" Text="Accept" runat="server" Visible="false" OnClick="yesRespondButton_Click" /></td>
                        <td><asp:Button ID="noRespondButton" Text="Decline" runat="server" Visible="false" OnClick="noRespondButton_Click" /></td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
