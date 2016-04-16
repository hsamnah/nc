<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="imgGallery.ascx.cs" Inherits="nc.venueCntrls.imgGallery" %>
<script src="../venueScript/tuScript.js" type="text/javascript"></script>
<script src="../venueScript/vImgGallery.js" type="text/javascript"></script>
<asp:Panel ID="GalleryToolbar" ClientIDMode="Static" Width="432px" runat="server">
    <asp:LinkButton ID="rootDir" runat="server" OnClick="rootDir_Click" Text="..."></asp:LinkButton>&nbsp;
    <asp:ImageButton ID="uploadImgs" runat="server" OnClick="uploadImgs_Click" ImageUrl="~/imgs/paperClip.png" Width="15px" />
</asp:Panel>
<hr />
<asp:Panel ID="GDC" ClientIDMode="Static" Width="432px" runat="server"></asp:Panel>
<asp:Panel ID="GC" ClientIDMode="Static" Width="432px" runat="server"></asp:Panel>