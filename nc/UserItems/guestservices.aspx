<%@ Page Title="" Language="C#" MasterPageFile="~/UserItems/minUser.Master" AutoEventWireup="true" CodeBehind="guestservices.aspx.cs" Inherits="nc.UserItems.guestServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gsContainer_Cls">
        <table style="width: 958px;">
            <tr>
                <td style="width: 635px;">
                    <asp:FormView ID="sEvent" runat="server">
                        <ItemTemplate>
                            <table style="width: 800px;">
                                <tr>
                                    <td colspan="2" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 12pt; color: black; font-weight: bold;"><%# Eval("Title") %></td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 435px; padding: 0; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: black; font-weight: bold;">
                                        <img src='<%# getEventImgs(Container.DataItem,Container.DataItem) %>' style="width: 435px;" /></td>
                                    <td style="vertical-align: top; padding: 5px 5px 5px 5px; width: 355px; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: black; font-weight: normal;"><%# Eval("eventDesc") %><br />
                                        <br />
                                        Date and time of event:<br />
                                        <%# Eval("eDate","{0:D}") %>&emsp;<%# Eval("Time","{0:t}") %></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </td>
                <td style="vertical-align: top;">

                    <p style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 12pt; font-weight: bold; color: #000000; font-style: normal">Send Invites to:</p>
                    <div style="overflow-y: auto;">
                        <asp:CheckBoxList ID="friends2Invite" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt">
                        </asp:CheckBoxList>
                    </div>
                    <span>Comments:</span>
                    <asp:TextBox ID="tbox" runat="server" TextMode="MultiLine" Height="91px" Width="156px"></asp:TextBox>
                    <asp:LinkButton ID="sendInvites" runat="server" Text="Send" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="Black" OnClick="sendInvites_Click"></asp:LinkButton>
                    <p style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt">Sent Invites:</p>
                    <asp:DataList ID="InvitesSent" DataKeyField="userID" runat="server">
                        <ItemStyle Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" />
                        <ItemTemplate>
                            <%# Eval("FN") %>&nbsp;<%# Eval("LN") %>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>