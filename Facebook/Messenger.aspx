<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Messenger.aspx.cs" Inherits="Messenger" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<LINK REL="SHORTCUT ICON"
       HREF="~/Data/messenger-icon.ico">
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
            <asp:ScriptManager ID="scriptManager" runat="server" />
            <center>
                <table style="align-items:center">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server"
                             UpdateMode="Conditional" >
                             <ContentTemplate>
                                 <asp:Label ID="Messages" runat="server" BorderStyle="Solid" BackColor="LightGray" />
                             </ContentTemplate>
                             <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="timerRefresh" EventName="Tick" />
                             </Triggers>
                             </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="Message" runat="server" Wrap="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="sendButton" Text="Send" runat="server" OnClick="sendButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="deleteMessagesButton" runat="server" Text="Delete Conversation" OnClick="deleteMessagesButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="deleteMessagesLabel" Text="NOTE: Conversation will be deleted only for you" ForeColor="Red" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="deleteMessagesOk" Text="Ok" OnClick="deleteMessagesOk_Click" runat="server" Visible="false" />
                            <asp:Button ID="deleteMessagesCancel" Text="Cancel" OnClick="deleteMessagesCancel_Click" runat="server" Visible="false" />
                        </td>
                    </tr>
                </table>
            </center>
            <asp:Timer ID="timerRefresh" runat="server" Enabled="true" Interval="1000" />
        </div>
    </form>
</body>
</html>
