<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<title>Facebook</title
<head runat="server">
	<LINK REL="SHORTCUT ICON"
       HREF="~/Data/logo.ico">
</head>
<body style="background-color:lightgray; ">
    <form id="form1" runat="server">
        <div style="background-color:steelblue; color:white; ">
            <table>
                <tr>
                    <th style="font-size:xx-large ;color:white; height:37px; text-shadow:1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000" >
                        <asp:HyperLink Font-Underline="false" ForeColor="White" Font-Bold="true" runat="server" NavigateUrl="~/Profile.aspx">
                            Facebook 
                        </asp:HyperLink>
                    </th>
                    <td>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:TextBox ID="searchTextBox" Text="Search Facebook" runat="server" Font-Size="Medium" TextMode="Search" Width="400" Height="30" />
                    </td>
                    <td>
                        <asp:Button ID="searchButton" Text="Search" TabIndex="0" runat="server" Height="30" Font-Size="Medium" OnClick="searchButton_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <table>
                <tr>
                    <td>
                       <asp:Image ID="picture" runat="server" Height="200" Width="200" BorderColor="White" BorderStyle="Solid" />
                    </td>
                    <td style="vertical-align:bottom">
                       <asp:Label ID="nameLabel" Text="Name" runat="server" Font-Size="X-Large" Font-Bold="true" style="color:white; text-shadow:2px 0 0 #000, 0 3px 0 #000, 0 2px 0 #000, 0px 0 0 #000" />
                       <asp:Label ID="surnameLabel" Text="Surname" runat="server" Font-Size="X-Large" Font-Bold="true" style="color:white; text-shadow:2px 0 0 #000, 0 3px 0 #000, 0 2px 0 #000, 0px 0 0 #000" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Label ID="likesLabel" Text="Likes:0" runat="server" />
                        <asp:Button ID="likeButton" Text="Like" runat="server" OnClick="likeButton_Click" />
                    </td>
                    <td>
                        <asp:Button ID="infoButton" Text="My Info" runat="server" OnClick="infoButton_Click" BorderStyle="Inset" Font-Size="Medium" />
                        <asp:Button ID="friendsButton" Text="Friends" runat="server" OnClick="friendsButton_Click" BorderStyle="Inset" Font-Size="Medium" />
                        <asp:Button ID="photosButton" Text="Photos" runat="server" OnClick="photosButton_Click" BorderStyle="Inset" Font-Size="Medium" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
