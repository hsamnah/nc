<%@ Page Title="" Language="C#" MasterPageFile="~/rootVenue.Master" AutoEventWireup="true" CodeBehind="vregCon3.aspx.cs" Inherits="nc.vregCon3" %>

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
                <div class="panel_cls" style="height: 800px; overflow-y: scroll; padding: 10px 5px 10px 5px;">
                    <table class="formCell_cls">
                        <tr>
                            <td class="tformCell_cls">Venue Registration:
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <table style="width: 100%; border-collapse: collapse; border-spacing: 0;">
                                    <tr>
                                        <td>Background: (Can only be a solid color or an image not both).</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="bgUpload" runat="server" CssClass="AvUpload_cls" Height="26px" Width="181px" />&nbsp;
                                                                    <asp:TextBox ID="clrSelector" runat="server" CssClass="clrSelector_cls" Width="88px" Height="18px"></asp:TextBox>
                                            <img alt="" id="colorPlt" src="imgs/clrbtn.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:LinkButton ID="UploadBkgrndLink" runat="server" CssClass="AvUpload_cls" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="White" OnClick="UploadBkgrndLink_Click">Upload</asp:LinkButton>&nbsp;&nbsp;<br />
                                            <asp:Label ID="UpbgStatus" runat="server" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="White">Transfer Status</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="outerBoundry">
                                            <div class="innerBoundry" style="padding: 15px; border: 2px solid White; border-radius: 8px 8px 8px 8px; position: static;">
                                                <div class="vPlaceHolder_cls" style="border: 1px solid White; border-radius: 8px 8px 8px 8px; background-color: rgba(94, 107, 130,0.5); width: 645px; height: 300px; background-size: cover;" id="bgContainer" runat="server"></div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <table style="width: 100%; border-collapse: collapse; border-spacing: 0;">
                                    <tr>
                                        <td>Logo:</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="logoUpload" runat="server" CssClass="AvUpload_cls" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="UploadLogoLink" runat="server" CssClass="AvUpload_cls" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="White" OnClick="UploadLogoLink_Click">Upload</asp:LinkButton><br />
                                            <asp:Label ID="uplgStatus" runat="server" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="White">Transfer Status</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="outerBoundry">
                                            <div class="innerBoundry" style="padding: 15px; border: 2px solid White; border-radius: 8px 8px 8px 8px; position: static;">
                                                <div class="vPlaceHolder_cls" id="logoContainer" runat="server" style="border: 1px solid White; border-radius: 8px 8px 8px 8px; background-color: rgba(94, 107, 130,0.5); width: 645px; height: 300px; background-size: cover;"></div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <table style="width: 100%; border-collapse: collapse; border-spacing: 0;">
                                    <tr>
                                        <td>Banner:</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:FileUpload ID="bannerUpload" runat="server" CssClass="AvUpload_cls" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="UploadBannerLink" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="White" OnClick="UploadBannerLink_Click">Upload</asp:LinkButton><br />
                                            <asp:Label ID="bnrUpload" runat="server" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="White">Transfer Status</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="outerBoundry">
                                            <div class="innerBoundry" style="padding: 15px; border: 2px solid White; border-radius: 8px 8px 8px 8px; position: static;">
                                                <div class="vPlaceHolder_cls" id="bnrContainer" style="border: 1px solid White; border-radius: 8px 8px 8px 8px; background-color: rgba(94, 107, 130,0.5); width: 645px; height: 300px; background-size: cover;" runat="server"></div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="Venue_register" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="Venue_register_Click">Complete and log in</asp:LinkButton>
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