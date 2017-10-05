<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Information.aspx.cs" Inherits="Information" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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
                            <asp:Image ID="picture" runat="server" Width="250" Height="250"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:bottom">
                            <asp:Button ID="changeProfilePicture" Text="Change Profile Picture" runat="server" OnClick="changeProfilePicture_Click" />
                            <asp:Button ID="logoutButton" Text="Log out" runat="server" OnClick="logoutButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Name" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="nameTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Surname" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="surnameTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Email" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="emailTextBox" Enabled="false" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Birthday" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="dayDropDown" runat="server" />
                            <asp:DropDownList ID="monthDropDown" runat="server" />
                            <asp:DropDownList ID="yearDropDown" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Hometown" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="hometownTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Job" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="jobTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="School" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="schoolTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="College" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="collegeTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="University" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="universityTextBox" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Password"  runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="passwordTextBox" TextMode="Password" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="New Password" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="newPasswordTextBox" TextMode="Password" runat="server" />
                            <asp:TextBox ID="confirmPasswordTextBox" TextMode="Password" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="labelPassword" ForeColor="Red" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="okButton" Text="OK" OnClick="okButton_Click" runat="server" />
                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="deleteLabel" Text="Are you sure?" runat="server" ForeColor="Red" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="deleteButton" Text="Delete Account" runat="server" OnClick="deleteButton_Click" />
                            <asp:Button ID="cancelDeleteButton" Text="Cancel" runat="server" Visible="false" OnClick="cancelButton_Click" />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>