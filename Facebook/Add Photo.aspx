<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add Photo.aspx.cs" Inherits="Add_Photo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<title>Photos</title
<head runat="server">
    <LINK REL="SHORTCUT ICON"
       HREF="~/Data/logo.ico">
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
        <div>
            <center>
                <table>
                    <tr>
                        <td>
                            <input id="Photo" type="file" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Title" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="titleTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Location" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="locationTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="uploadButton" runat="server" Text="Upload" OnClick="uploadButton_Click" />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
