<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<title>Facebook - Log In or Sign Up</title>
<head runat="server">
</head>
<body style="background-color:lightgray">
    <form id="form1" runat="server">
        <div style="background-color:steelblue; color:white">
            <table>
                <tr>
                    <th style="font-size:42px ;color:white; height:37px; text-align:left; text-shadow:1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000"> 
                        <asp:HyperLink Font-Underline="false" ForeColor="White" Font-Bold="true" runat="server" NavigateUrl="~/Registration.aspx">
                            facebook
                        </asp:HyperLink>
                    </th>
                </tr>
                <tr>
                    <td style="width:700px"></td>
                    <td>
                        <asp:Label Text="Email" runat="server"/>
                    </td>
                    <td>
                        <asp:TextBox ID="loginEmailTextBox" TextMode="Email" runat="server" Height="18" BorderStyle="Solid" BorderColor="DarkBlue"/>
                    </td>
                    <td>
                        <asp:Label Text="Password" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="loginPasswordTextBox" TextMode="Password" runat="server" Height="18"/>
                    </td>
                    <td>
                        <asp:Button ID="loginButton" Text="Log In" runat="server" OnClick="loginButton_Click" BorderStyle="Double" BorderColor="DarkBlue" ForeColor="White" BackColor="SteelBlue"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="rememberCheckBox" Text="Remember me" runat="server" />
                    </td>
                    <td colspan="1"></td>
                    <td>
                        <asp:HyperLink ID="forgotPasswordHyperlink" Text="Forgot account?" NavigateUrl="~/ForgotPassword.aspx" runat="server" Font-Underline="false" style="color:lightsteelblue; font-size:14px" />
                    </td>
                </tr>
            </table>
        </div>
        <br /><br /><br />
        <div>
            <table style="empty-cells:hide; text-align:right">
                <tr>
                    <td style="width:1000px"></td>
                    <td colspan="2"></td>
                    <th style="font-size:x-large">Sign Up</th>
                </tr>
                <tr>
                    <td colspan="3"></td>
                    <td>
                        <asp:TextBox ID="registerNameTextBox" Text="Your Name" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="registerSurnameTextBox" Text="Your Surname" runat="server" />
                    </td>
                </tr>
                <tr>
                <td colspan="3"></td>
                    <td colspan="1">
                        <asp:TextBox ID="registerEmailTextBox" Text="YourEmail@example.ex" TextMode="Email" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                    <td>
                        <asp:TextBox ID="registerPasswordTextBox" TextMode="Password" Text="Password" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="registerConfirmPasswordTextBox" TextMode="Password" Text="Password" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                    <th style="font-size:large">Birthday</th>
                </tr>
                <tr>
                    <td colspan="3"></td>
                    <td>
                        <asp:DropDownList ID="dayListBox" runat="server" />
                        <asp:DropDownList ID="monthListBox" runat="server" />
                        <asp:DropDownList ID="yearListBox" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                    <td>
                        <asp:RadioButton ID="Male" GroupName="Gender" Text="Male" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="Female" GroupName="Gender" Text="Female" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                    <td style="border:thin; border-color:black">
                        <asp:Button ID="signUpButton" Text="Sign Up" runat="server" OnClick="signUpButton_Click" BackColor="ForestGreen" ForeColor="White" Width="150" Height="30"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
