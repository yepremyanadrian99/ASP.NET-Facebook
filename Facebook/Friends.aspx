<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Friends.aspx.cs" Inherits="Friends" %>

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
                        <td style="text-align:center">
                            <asp:Label ID="requestsLabel" Text="Friend Requests" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridViewRequests" runat="server" OnRowCommand="gridViewRequests_RowCommand" EnableViewState="true">
                                <Columns>
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Button ID="showProfileButton" Text="View" runat="server" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label Text="Friends" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridViewFriends" runat="server" OnRowCommand="gridViewFriends_RowCommand" EnableViewState="true">
                                <Columns>
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:Button ID="showProfileButton" Text="View" runat="server" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="text-align:center">
                        <td>
                            <asp:Button ID="buttonRefresh" Text="Refresh" runat="server" OnClick="buttonRefresh_Click" />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
