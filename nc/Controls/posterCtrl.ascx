<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="posterCtrl.ascx.cs" Inherits="nc.Controls.posterCtrl" %>
<%@ Register src="imgMenu.ascx" tagname="imgMenu" tagprefix="uc1" %>
<%@ Register Src="~/Controls/mDirections.ascx" TagPrefix="uc1" TagName="mDirections" %>


<link href="../style/pcStyle.css" type="text/css" rel="stylesheet" />
<asp:Panel runat="server" CssClass="rootPanel_cls" ID="Incoming">
    <table class="prime_min_tbl_cls">
        <tr>
            <td class="toolbar_cls" colspan="2">
                <img alt="Bold" id="boldBtn" src="../imgs/Bold.png" class="ctrlButtons_cls" />&nbsp;
                  <img alt="Italic" id="italicBtn" src="../imgs/Italic.png" class="ctrlButtons_cls" />&nbsp;
                  <img alt="Underline" id="underlineBtn" src="../imgs/Underline.png" class="ctrlButtons_cls" />&nbsp;
                  <img alt="Left" id="leftAlignBtn" src="../imgs/leftAlign.png" class="ctrlButtons_cls" />&nbsp;
                  <img alt="Center" id="centerAlignBtn" src="../imgs/centerAlign.png" class="ctrlButtons_cls" />&nbsp;
                  <img alt="Right" id="rightAlignBtn" src="../imgs/rightAlign.png" class="ctrlButtons_cls" />&nbsp;
                  <img alt="Justify" id="justifyBtn" src="../imgs/FullJustify.png" class="ctrlButtons_cls" />&nbsp;|&nbsp;
                  <div id="clrSelection" class="clrSelection_cls"></div>
                <img id="clrPicker" src="../imgs/colorSwatch.png" />&nbsp;
                <uc1:imgMenu ID="imgMenu1" runat="server" />&nbsp;
                <uc1:mDirections runat="server" id="mDirections" />
            </td>
        </tr>
        <tr>
            <td class="editor_cls" colspan="2">
                <asp:HiddenField ID="postValue" ClientIDMode="Static" runat="server" />
                <div contenteditable="true" id="PostBox" clientidmode="Static" runat="server" class="InfoBox_cls"></div>
            </td>
        </tr>
        <tr>
            <td class="buttonContainer_cls">
                &nbsp;</td>
            <td class="btnSend_cls">
                <asp:ImageButton ID="sendPost" runat="server" ClientIDMode="Static" CausesValidation="false" CssClass="psImgSend_cls" ImageUrl="~/imgs/post.png" Width="50px" Height="30px" OnClick="sendPost_Click" />
            </td>
        </tr>
    </table>
    <div id="imgList" class="imgList_cls">
        <asp:Panel CssClass="DirList_cls" ID="dirList" runat="server"></asp:Panel>
        <div class="iList_cls" id="iList" runat="server"></div>
    </div>
</asp:Panel>