<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Connect.ascx.cs" Inherits="nc.Controls.Connect" %>
<link href="../style/cStyle.css" type="text/css" rel="stylesheet" />
&emsp;&emsp;<span id="Connect_btn" class="connect_btn_cls">Connect</span>
<asp:Panel ID="backPanel" runat="server" CssClass="msg_bg_cls">
    <div class="cmContainer_cls" id="cmContainer">
        <div class="title_req_msg_cls">Attach Message with connect request:</div>
        <div>
            <asp:TextBox ID="msgTb" runat="server" TextMode="MultiLine" Height="147px" Width="481px"></asp:TextBox></div>
        <div class="btnFun_cls">
            <asp:LinkButton ID="cancel" runat="server" Text="Cancel Request" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="Black" OnClick="cancel_Click"></asp:LinkButton>&emsp;
            <asp:LinkButton ID="sendRequest" runat="server" Text="Send Request" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="Black" OnClick="sendRequest_Click"></asp:LinkButton>
        </div>
    </div>
</asp:Panel>