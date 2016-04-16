<%@ Page Title="" Language="C#" MasterPageFile="~/root2.Master" AutoEventWireup="true" CodeBehind="reset.aspx.cs" Inherits="nc.reset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
     <table class="rootTbl_cls">
        <tr>
            <td class="tl_cls">
                <div id="backDrop" class="backdrop_cls">
                    <div class="functionBar_cls">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 97%"></td>
                                <td style="width: 3%;">
                                    <img id="closeBtn" src="imgs/closeInvert.gif" style="height: 50px; width: 40px; cursor: pointer;" /></td>
                            </tr>
                        </table>
                    </div>
                    <div id="innerContainer" class="internal_Container_cls">
                        <br />
                        <br />
                        <span id="rTitle" class="title"></span>
                        <br />
                        <hr class="break" />
                        <span id="rAuthor" class="author"></span>&emsp;<span id="rDate" class="date"></span><br />
                        <br />
                        <div style="width: 620px; height: 720px; overflow-y: auto;">
                            <span id="rDesc" class="description"></span>
                            <br />
                            <br />
                            <br />
                            <br />
                            <span id="rDetails" class="details"></span>
                        </div>
                    </div>
                </div>
            </td>
            <td class="tc_cls">&nbsp;</td>
            <td class="tr_cls">&nbsp;</td>
        </tr>
        <tr>
            <td class="ml_cls" rowspan="2">
                &nbsp;</td>
            <td class="mc_cls" rowspan="2">
                <div class="panelreset_cls">
                    <table class="formCell2_cls">
                        <tr>
                            <td class="tformCell_cls">
                                Account recovery:</td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:Label ID="secQ" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="9pt" ForeColor="White"></asp:Label><br />
                                <asp:TextBox ID="secA" runat="server" AutoCompleteType="Disabled"  Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="9pt" ForeColor="Black" Width="250px"></asp:TextBox>
                                <br />
                                Hint:&nbsp;<asp:Label ID="secH" runat="server"  Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="9pt" ForeColor="White"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="Submit" Visible="true" OnClick="Submit_Click" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="9pt" ForeColor="White">Submit</asp:LinkButton>&emsp;
                                <asp:Label ID="resultmsg" runat="server" Visible="False" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="9pt" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="mr_cls"></td>
        </tr>
        <tr>
            <td class="br_cls">&nbsp;</td>
        </tr>
    </table>


</asp:Content>
