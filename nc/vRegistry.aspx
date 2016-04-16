<%@ Page Title="" Language="C#" MasterPageFile="~/rootVenue.master" AutoEventWireup="true" CodeBehind="vRegistry.aspx.cs" Inherits="nc.vregistration" %>

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
                            <td class="tformCell_cls">Venue Registration:</td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Name of Venue:
                                <asp:RequiredFieldValidator ControlToValidate="vName" ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                <br />
                                <asp:TextBox ID="vName" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Primary username:&emsp;<asp:RequiredFieldValidator ControlToValidate="un" ID="unamVal" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                <br />
                                <asp:TextBox ID="un" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Primary email address:&emsp;<asp:RequiredFieldValidator ControlToValidate="email" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="email" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Password:<asp:RequiredFieldValidator ID="pr1" runat="server" ControlToValidate="pwd1" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="pwd1" CssClass="password_cls" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Confirm Password:<span id="err" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 9pt; color: #FF0000"></span><asp:RequiredFieldValidator ID="pr2" runat="server" ControlToValidate="pwd2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="pwd2" CssClass="password_cls" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Security question:&emsp;<asp:RequiredFieldValidator ControlToValidate="sq" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="sq" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Answer:&emsp;<asp:RequiredFieldValidator ControlToValidate="sa" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="sa" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Hint:&emsp;<asp:RequiredFieldValidator ControlToValidate="sh" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="sh" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="Venue_register" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="Venue_register_Click">Register</asp:LinkButton>&emsp;<asp:Label ID="errMsg" runat="server" Visible="False" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" ForeColor="Red" Font-Size="9pt"></asp:Label></td>
                        </tr>
                        <tr id="sTR" runat="server" visible="false">
                            <td class="formCell_cls">
                                <asp:Label ID="sMsg" runat="server" Visible="False" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" ForeColor="White" Font-Size="10pt"></asp:Label></td>
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