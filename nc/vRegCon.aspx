<%@ Page Title="" Language="C#" MasterPageFile="~/rootVenue.master" AutoEventWireup="true" CodeBehind="vRegCon.aspx.cs" Inherits="nc.vRegCon" %>

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
                            <td class="formCell_cls">Summary Details:(Limit: 256 characters)<br />
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="regFormV_cls" Height="50px" Width="300px" ID="sDesc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Full Description:<br />
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="regFormV_cls" Height="100px" Width="300px" ID="fDesc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Web site(url):<br />
                                <asp:TextBox runat="server" Width="300px" ID="website"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Genre:<br />
                                <asp:TextBox runat="server" Width="300px" ID="genre"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Open @: (Seperate days with commas)<br />
                                <asp:TextBox runat="server" TextMode="MultiLine" Height="50px" Width="300px" ID="daysOpen"></asp:TextBox>
                                <br />
                                Times open:<div style="display: inline-block; height: 30px; vertical-align: bottom;">
                                    <table>
                                        <tr>
                                            <td style="height: 30px; vertical-align: bottom;">
                                                <asp:DropDownList ID="Hours" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; " Font-Size="9pt" BackColor="Transparent" Width="35pt" ForeColor="Black">
                                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="height: 30px; vertical-align: bottom;">:</td>
                                            <td style="height: 30px; vertical-align: bottom;">
                                                <asp:DropDownList ID="Minutes" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; " Font-Size="9pt" BackColor="Transparent" Width="35pt" ForeColor="Black">
                                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                    <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                                    <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                                    <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                                    <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                                    <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                                    <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                    <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                                    <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                                    <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                                    <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                    <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                                    <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                                    <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                                    <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                                    <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                                    <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                    <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                                    <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                                    <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                                    <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="height: 30px; vertical-align: bottom;">:</td>
                                            <td style="height: 30px; vertical-align: bottom;">
                                                <asp:DropDownList ID="Seconds" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; " Font-Size="9pt" BackColor="Transparent" Width="35pt" ForeColor="Black">
                                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                    <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                                    <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                                    <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                                    <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                                    <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                                    <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                    <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                                    <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                                    <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                                    <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                    <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                                    <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                                    <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                                    <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                                    <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                                    <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                    <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                                    <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                                    <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                                    <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="height: 30px; vertical-align: bottom;">
                                                <asp:DropDownList ID="DayPart" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; " Font-Size="9pt" BackColor="Transparent" Width="35pt" ForeColor="Black">
                                                    <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Crowd Capacity:<br />
                                <asp:TextBox runat="server" Width="300px" ID="cap"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Age limit:<br />
                                <asp:TextBox runat="server" Width="300px" ID="ageLimit"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Dress code:<br />
                                <asp:TextBox runat="server" Width="300px" ID="DressCode"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="Venue_register" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="Venue_register_Click">Continue</asp:LinkButton>
                                <asp:Label ID="Err" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
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