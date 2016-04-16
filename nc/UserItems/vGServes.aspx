<%@ Page Title="" Language="C#" MasterPageFile="~/UserItems/minUser.Master" AutoEventWireup="true" CodeBehind="vGServes.aspx.cs" Inherits="nc.UserItems.vGuestServices" %>

<%@ Register Src="~/Controls/timeControl.ascx" TagPrefix="uc1" TagName="timeControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../style/vGSStyle.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="primaryTable_cls">
            <tr>
                <td class="leftCell_cls">
                    <div class="venue_title_cls">
                        <asp:Literal ID="venueName" runat="server"></asp:Literal>
                    </div>
                    <div class="venue_Divimg_cls">

                        <asp:Image ID="vBanner" runat="server" CssClass="venue_img_cls" />
                    </div>
                </td>
                <td class="centerCell_cls">
                    <asp:Calendar ID="DaysinOp" runat="server" Width="100%" OnDayRender="DaysinOp_DayRender" Height="294px">
                        <DayHeaderStyle Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#24323E" />
                        <DayStyle Font-Size="10pt" Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#678CA7" />
                        <OtherMonthDayStyle ForeColor="#666666" />
                        <SelectedDayStyle BackColor="#92999F" />
                        <TitleStyle Font-Size="10pt" Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#3E566A" ForeColor="#CEDAE3" Height="10px" />
                        <WeekendDayStyle Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" />
                    </asp:Calendar>
                    <br />
                    <div class="legend_cls">
                        <span style="background-color: rgb(56,70,82);">&emsp;</span> Days open.&nbsp;<span style="background-color: #678CA7;">&emsp;</span> Days closed<br />
                        Time:&emsp;<uc1:timeControl runat="server" ID="timeControl" />
                        <br />
                        <asp:CheckBox ID="ipub" runat="server" Text="Is Event Public?" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="#384652" />
                        <br />
                        <br />
                        &nbsp;Label Event:<br />
                        &nbsp;<asp:TextBox ID="eventLbl" runat="server" TextMode="SingleLine" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="#384652" Width="300px"></asp:TextBox>
                        <br />
                        <br />
                        Event description:<br />
                        <asp:TextBox ID="EventDescription" runat="server" Height="119px" TextMode="MultiLine" Width="364px"></asp:TextBox>
                    </div>
                </td>
                <td class="rightCell_cls">
                    <h1>Send invites to:</h1>
                    <br />
                    <div class="inviteContainer_cls">
                        <asp:CheckBoxList ID="friendList_chkbxlist" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="#384652">
                        </asp:CheckBoxList>
                    </div>
                    <br />
                    <h2>Comments:
                    </h2>
                    <asp:TextBox ID="comments" runat="server" TextMode="MultiLine" Width="100%" Height="100px"></asp:TextBox><br />
                    <asp:LinkButton ID="sendInvites" runat="server" OnClick="sendInvites_Click" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;" Font-Size="10pt" ForeColor="#384652">Send invites</asp:LinkButton>
                    <hr />
                    <h2>
                        <asp:Literal ID="eventTitleLbl" runat="server"></asp:Literal></h2>
                    <h1>Invites sent to:</h1>
                    <br />
                    <div class="inviteContainer_cls">
                        <asp:DataList DataKeyField="Identifier" ID="Invites" runat="server">
                            <ItemStyle Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="#24323E" />
                            <ItemTemplate>
                                <%# Eval("FN") %>&nbsp;<%# Eval("LN") %>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>