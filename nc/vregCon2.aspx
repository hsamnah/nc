<%@ Page Title="" Language="C#" MasterPageFile="~/rootVenue.Master" AutoEventWireup="true" CodeBehind="vregCon2.aspx.cs" Inherits="nc.vRegCon2" %>

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
                            <td class="tformCell_cls">Venue Registration:
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Street Address:<br />
                                <asp:TextBox runat="server" TextMode="SingleLine" CssClass="regFormV_cls" Width="300px" ID="Addr1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Address 2:<br />
                                <asp:TextBox runat="server" TextMode="SingleLine" CssClass="regFormV_cls" Width="300px" ID="Addr2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <table style="width: 300px; border-collapse: collapse; border-spacing: 0;">
                                    <tr>
                                        <td>Country:</td>
                                        <td>Province:</td>
                                        <td>City:</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 0; margin: 0; width: 100px;">
                                            <asp:DropDownList ID="country" runat="server" Width="100px" BackColor="Transparent" ForeColor="Black" AutoPostBack="true" OnSelectedIndexChanged="country_SelectedIndexChanged"></asp:DropDownList></td>
                                        <td style="padding: 0; margin: 0; width: 100px;">
                                            <asp:DropDownList ID="subdivision" Width="100px" BackColor="Transparent" ForeColor="Black" AutoPostBack="true" runat="server" Visible="false" OnSelectedIndexChanged="subdivision_SelectedIndexChanged"></asp:DropDownList></td>
                                        <td style="padding: 0; margin: 0; width: 100px;">
                                            <asp:DropDownList ID="city" Width="100px" BackColor="Transparent" ForeColor="Black" runat="server" Visible="false"></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Postal/Zip Code:<br />
                                <asp:TextBox runat="server" TextMode="SingleLine" Width="300px" ID="pz"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">General Information:<br />
                                <table style="width: 100%; border-collapse: collapse; border-spacing: 0;">
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="gvLineList" AutoGenerateColumns="false" runat="server" CellPadding="5">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Line:" DataField="numType" />
                                                    <asp:BoundField HeaderText="Number" DataField="number" />
                                                </Columns>
                                                <HeaderStyle BackColor="Transparent" BorderStyle="None" Font-Names="Verdana" Font-Size="10pt" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Bottom" />
                                                <RowStyle BackColor="Transparent" BorderColor="White" BorderStyle="None" BorderWidth="0px" Font-Names="Verdana" Font-Size="10pt" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 0; margin: 0; width: 150px;">
                                            <asp:TextBox runat="server" TextMode="singleLine" Width="150px" ID="number"></asp:TextBox></td>
                                        <td style="padding: 0; margin: 0;">
                                            <asp:DropDownList ID="lineType" Width="150px" AutoPostBack="true" BackColor="Transparent" ForeColor="Black" runat="server" OnSelectedIndexChanged="lineType_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select line--"></asp:ListItem>
                                                <asp:ListItem Text="Main #:" Value="Main #:"></asp:ListItem>
                                                <asp:ListItem Text="Fax #:" Value="Fax #:"></asp:ListItem>
                                                <asp:ListItem Text="Line 1:" Value="Line 1:"></asp:ListItem>
                                                <asp:ListItem Text="Line 2:" Value="Line 2:"></asp:ListItem>
                                                <asp:ListItem Text="Line 3:" Value="Line 3:"></asp:ListItem>
                                                <asp:ListItem Text="Line 4:" Value="Line 4:"></asp:ListItem>
                                                <asp:ListItem Text="Line 5:" Value="Line 5:"></asp:ListItem>
                                                <asp:ListItem Text="Line 6:" Value="Line 6:"></asp:ListItem>
                                                <asp:ListItem Text="Line 7:" Value="Line 7:"></asp:ListItem>
                                                <asp:ListItem Text="Line 8:" Value="Line 8:"></asp:ListItem>
                                                <asp:ListItem Text="Line 9:" Value="Line 9:"></asp:ListItem>
                                                <asp:ListItem Text="Line 10:" Value="Line 10:"></asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="Venue_register" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="Venue_register_Click">Continue</asp:LinkButton>
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