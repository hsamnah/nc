<%@ Page Title="" Language="C#" MasterPageFile="~/UserItems/uiroot.Master" AutoEventWireup="true" CodeBehind="uiSys.aspx.cs" Inherits="nc.UserItems.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="rootTbl_cls">
        <tr>
            <td class="tl_cls"></td>
            <td class="tc_cls">&nbsp;</td>
            <td class="tr_cls">&nbsp;</td>
        </tr>
        <tr>
            <td class="ml2_cls">&nbsp;</td>
            <td class="mc_cls" rowspan="2">
                <div class="panel_cls">
                    <table class="formCell_cls">
                        <tr>
                            <td class="formCell_cls">According to our records you are using a different system then normal.  For your security please confirm your identity by answering your security question.
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Question:<br />
                                <asp:Label ID="SecQ" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Answer:<br />
                                <asp:TextBox runat="server" TextMode="SingleLine" CssClass="regFormV_cls" Width="300px" ID="secA"></asp:TextBox><br />
                                <asp:Label ID="secHint" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="ip_register" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="ip_register_Click">Continue</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="mr_cls"></td>
        </tr>
        <tr>
            <td class="bl_cls">&nbsp;</td>
            <td class="br_cls">&nbsp;</td>
        </tr>
    </table>
</asp:Content>