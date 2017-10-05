<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<title>Forgot</title
<head runat="server">
    <LINK REL="SHORTCUT ICON"
       HREF="~/Data/logo.ico">
</head>
<body style="background-color:lightgray">
    <form id="form1" runat="server">
        <div style="font-size:xx-large; background-color:steelblue; height:65px">
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
    <div >
        <center>
            Enter the E-Mail address on which we will send the password for your account.<br />
            If the E-Mail is not registered in our database no action will be taken.<br />
            <br />
            <asp:TextBox ID="emailTextBox" TextMode="Email" runat="server" />
            <asp:Button ID="submitButton" Text="Submit" runat="server" OnClick="submitButton_Click" />
        </center>
    </div>
    </form>
</body>
</html>
