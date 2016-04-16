<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="memberSearch.ascx.cs" Inherits="nc.Controls.memberSearch" %>
<link href="../style/autoSugStyle.css" type="text/css" rel="stylesheet" />

<div class="container_cls">
    <input type="text" class="tboxInput_cls" name="autosuggest" id="autosuggest" autocomplete="off" />
    <asp:ImageButton CssClass="btn_cls" ID="viewMember" runat="server" ImageUrl="~/imgs/comm-icon.png" OnClick="viewMember_Click" />
    <input type="hidden" id="uIdentifier" clientidmode="Static" runat="server" />
</div>
<div class="suggest_cls" id="dropDownContainer"></div>