<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="avatarUpload.ascx.cs" Inherits="nc.Controls.avatarUpload" %>
<link href="../style/avUpload.css" type="text/css" rel="stylesheet" />
<asp:HiddenField ID="uID" runat="server" />
&emsp;<span runat="server" id="upAvatar" class="avBtn_cls">
</span>
<div id="backdrop" class="backdrop_cls">
    <div id='innerPanel' class="innerPanel_cls">
        Select your image:<br />
        <br />
        <asp:FileUpload ID="AvatarLoad" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="#24323E" Height="30px" /><br />
        <br />
        <asp:LinkButton ID="Cancel" runat="server" Text="Cancel" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="#24323E" OnClick="Cancel_Click"></asp:LinkButton>&emsp;
        <asp:LinkButton ID="upload" runat="server" Text="Upload File" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="#24323E" OnClick="upload_Click"></asp:LinkButton>
    </div>
</div>