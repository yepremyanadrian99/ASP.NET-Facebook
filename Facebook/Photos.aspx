<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Photos.aspx.cs" Inherits="Photos" %>

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
        <div>
            <asp:Button ID="addPhotoButton" Text="Upload Photo" runat="server" Height="30" Width="100" BorderColor="White" OnClick="addPhotoButton_Click" />
            <center>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="previousButton" Text="Prev" runat="server" OnClick="previousButton_Click" />
                        </td>
                        <td>
                            <asp:Image ID="picture" runat="server" Height="476" Width="476" BorderColor="White" BorderStyle="Solid" />
                        </td>
                        <td>
                            <asp:Button ID="nextButton" Text="Next" runat="server"  OnClick="nextButton_Click"/>
                        </td>
                    </tr>
                    <tr style="text-align:center">
                        <td></td>
                        <td>
                            <asp:Label ID="likesLabel" Text="Likes: 0" runat="server" />
                            <asp:Button ID="likeUnlikeButton" Text="Like" runat="server" OnClick="likeUnlikeButton_Click" />
                            <asp:Button ID="makeProfileButton" Text="Make Profile" runat="server" OnClick="makeProfileButton_Click" />
                            <asp:Button ID="deleteButton" Text="Delete" runat="server" OnClick="deleteButton_Click" />
                        </td>
                    </tr>
                </table>
            </center>
            <br /><br />
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Panel ID="imagesPanel" runat="server" Wrap="true"></asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
