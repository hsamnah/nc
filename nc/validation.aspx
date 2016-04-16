<%@ Page Title="" Language="C#" MasterPageFile="~/root2.Master" AutoEventWireup="true" CodeBehind="validation.aspx.cs" Inherits="nc.root.validation" %>

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
                <div class="panel2_cls">
                    <table class="formCell2_cls">
                        <tr>
                            <td class="tformCell_cls">Thank you for validating your account.</td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">First Name:<br />
                                <asp:TextBox ID="FN" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Last Name:<br />
                                <asp:TextBox ID="LN" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Street Address:<br />
                                <asp:TextBox ID="strAdd" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Apt./Unit:<br />
                                <asp:TextBox ID="aptUnit" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Country/Region/City<br />
                                <asp:DropDownList ID="country" runat="server" OnSelectedIndexChanged="getSub_Click" AutoPostBack="true" CssClass="ddLocation_cls"></asp:DropDownList><asp:DropDownList ID="subdivision" runat="server" AutoPostBack="true" Visible="false" CssClass="ddLocation_cls" OnSelectedIndexChanged="getCity_Click"></asp:DropDownList><asp:DropDownList ID="City" runat="server" CssClass="ddLocation_cls" Visible="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Postal/Zip Code:<br />
                                <asp:TextBox ID="pz" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Phone #:<br />
                                <asp:TextBox ID="tel" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Mobile #:<br />
                                <asp:TextBox ID="mobile" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="Next" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="Next_Click">Complete</asp:LinkButton>&emsp;<asp:Label ID="errMsg" runat="server" Visible="False" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" ForeColor="Red" Font-Size="9pt"></asp:Label></td>
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