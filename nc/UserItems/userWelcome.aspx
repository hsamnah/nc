<%@ Page Title="" Language="C#" MasterPageFile="~/UserItems/root.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="userWelcome.aspx.cs" Inherits="nc.UserItems.userwelcome" %>

<%@ Register Src="../Controls/eventGallery.ascx" TagName="eventGallery" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/memberSearch.ascx" TagPrefix="uc1" TagName="memberSearch" %>
<%@ Register Src="../Controls/VenueListing.ascx" TagName="VenueListing" TagPrefix="uc2" %>
<%@ Register Src="../Controls/Connect.ascx" TagName="Connect" TagPrefix="uc3" %>
<%@ Register Src="../Controls/Blog.ascx" TagName="Blog" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/avatarUpload.ascx" TagPrefix="uc1" TagName="avatarUpload" %>
<%@ Register Src="~/Controls/posterCtrl.ascx" TagPrefix="uc1" TagName="posterCtrl" %>
<%@ Register Src="../Controls/userPosts.ascx" TagName="userPosts" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/userPosts.ascx" TagPrefix="uc1" TagName="userPosts" %>
<%@ Register Src="~/Controls/chatHistory.ascx" TagPrefix="uc1" TagName="chatHistory" %>

<%@ Register Src="../Controls/userEvent.ascx" TagName="userEvent" TagPrefix="uc6" %>
<%@ Register Src="~/venueCntrls/imgGallery.ascx" TagPrefix="uc1" TagName="imgGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="mainContainer" runat="server">
        <table class="MainTableU_cls">
            <tr>
                <td colspan="3" class="u">
                    <uc1:memberSearch runat="server" id="memberSearch" />
                    &nbsp;
                    <asp:LinkButton ID="profile" CssClass="icons" OnClick="profile_Click" runat="server">profile</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="homeinfo" CssClass="icons" OnClick="homeinfo_Click" runat="server">shared</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="gallery" CssClass="icons" OnClick="gallery_Click" runat="server">Gallery</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="Venue" CssClass="icons" OnClick="Venue_Click" runat="server">Venues</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="ClubEvents" CssClass="icons" OnClick="ClubEvents_Click" runat="server">Upcoming Events</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="wReview" CssClass="icons" OnClick="wReview_Click" runat="server">reviews/comments</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="msgs" CssClass="icons" OnClick="msgs_Click" runat="server">messages</asp:LinkButton>&nbsp;<asp:Label ID="cr" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="8pt" Visible="False" BackColor="#24323E" ForeColor="#B4C6D3" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="ua">
                    <table class="side_panel_cls">
                        <tr>
                            <td class="side_panel_cls">
                                <asp:Image ID="userImg" Width="250px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="side_panel_cls">User:&emsp;<asp:Label ID="currentUser1" runat="server"></asp:Label>
                                <uc1:avatarUpload runat="server" id="avatarUpload" />
                            </td>
                        </tr>
                        <tr>
                            <td id="followRow" class="side_panel_cls" runat="server" visible="false">
                                <asp:LinkButton ID="myPLace" OnClick="myPLace_Click" runat="server" Text="My Place"></asp:LinkButton><uc3:Connect ID="Connect1" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="VenueAssoc" CssClass="side_panel_cls" runat="server">
                        <asp:DataList ID="uv" DataKeyField="vid" runat="server" ShowFooter="False" ShowHeader="True" RepeatDirection="Vertical" RepeatColumns="1">
                            <ItemStyle BackColor="#24323e" ForeColor="#CEDAE3" />
                            <HeaderStyle BackColor="#3e566a" />
                            <HeaderTemplate>
                                <span style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; color: #000000">Selecting a venue sends messages only to that group. </span>
                                <br />
                                <span style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 8pt; color: #000000">click </span>
                                <asp:LinkButton ID="Unselect" CommandName="Unselect" OnCommand="Unselect_Command" ForeColor="#24323e" Font-Bold="true" Font-Size="12pt" Font-Names="Verdana" runat="server" Text="..."></asp:LinkButton><span style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 8pt; color: #000000"> to unselect.</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="vshort_cls">
                                    <tr>
                                        <td colspan="2" class="vshortname_cls">
                                            <asp:LinkButton ID="vlbtn" runat="server" Text='<%# Eval("Venue") %>' OnCommand="vlbtn_Command" CommandName="Select" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" Font-Bold="True" ForeColor="Black"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <asp:HiddenField ID="selectVenue" runat="server" Value='<%# Eval("vid") %>' />
                                        <td class="vshortimg_cls"><%# getProperImg(Container.DataItem,Container.DataItem) %></td>
                                        <td class="vshortDes_cls"><%# getPropertyDescription(Container.DataItem) %></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </asp:Panel>
                    <table class="side_panel_cls">
                        <tr>
                            <td class="side_panelTitle_cls">Event Schedule</td>
                        </tr>
                        <tr>
                            <td class="side_panel_cls">
                                <asp:Calendar ID="Calendar1" runat="server" Width="250px" OnDayRender="eventDay_Render" OnSelectionChanged="eventsAvailable_Change">
                                    <DayHeaderStyle Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#24323E" />
                                    <DayStyle Font-Size="10pt" Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#B4C6D3" />
                                    <OtherMonthDayStyle ForeColor="#666666" />
                                    <SelectedDayStyle BackColor="#92999F" />
                                    <TitleStyle Font-Size="10pt" Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#3E566A" ForeColor="#CEDAE3" Height="10px" />
                                    <WeekendDayStyle Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" />
                                </asp:Calendar>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="eventPanel" CssClass="side_panel_cls" runat="server" Visible="true">
                        <table>
                            <tr>
                                <td class="side_panel_cls">
                                    <asp:Label ID="availableEvents" runat="server" Visible="False" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="8pt" ForeColor="#24323E"></asp:Label>
                                    <asp:GridView ID="eventList" DataKeyNames="EventIdentifier" Width="250px" OnRowCommand="eventList_RowCommand" runat="server" AutoGenerateColumns="False" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Venue">
                                                <ItemTemplate>
                                                    <%# Eval("Venue") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Event">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lb" Text='<%# Eval("Title") %>' CommandName="selectEvent" CommandArgument='<%# Eval("inviteIdentifier") %>' CssClass="EventLink_cls"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" Width="15px" Height="15px" runat="server" ImageUrl='<%# "/imgs/" + getStatus(Container.DataItem) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Bold="True" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="8pt" />
                                        <RowStyle Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="8pt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td class="ub">
                    <asp:MultiView ID="UserInformation" runat="server">
                        <asp:View ID="UserProfile" runat="server" OnActivate="userProfile_Activate">
                            <table class="innerUser_tbl">
                                <tr>
                                    <td class="user_c">

                                        <uc1:posterCtrl runat="server" id="posterCtrl" />

                                        <asp:Panel runat="server" ID="pSelectedEvent" Visible="false">
                                            <table class="partc_tbl">
                                                <tr>
                                                    <td class="abc">Selected Event:</td>
                                                </tr>
                                                <tr>
                                                    <td class="psb_cls">
                                                        <asp:DetailsView ID="DetailsView1" AutoGenerateRows="false" runat="server" GridLines="None" Width="100%">
                                                            <FieldHeaderStyle Font-Bold="True" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" />
                                                            <Fields>
                                                                <asp:TemplateField HeaderText="Event:">
                                                                    <ItemTemplate><%# Eval("Title") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Event Poster:">
                                                                    <ItemTemplate>
                                                                        <img src="<%# getEventImg(Container.DataItem,Container.DataItem) %>" style="width: 320px; height: 400px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description:">
                                                                    <ItemTemplate><%# Eval("eventDesc") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Venue:">
                                                                    <ItemTemplate><%# Eval("VenueName") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Location Address:">
                                                                    <ItemTemplate>
                                                                        ,<br />
                                                                        &nbsp;&nbsp;<%# Eval("City") %>,
                                                                        <br />
                                                                        &nbsp;&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date And Time:">
                                                                    <ItemTemplate>
                                                                        &nbsp;@&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Event Organizer:">
                                                                    <ItemTemplate><%# Eval("Organizer") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status:">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image1" runat="server" Height="15px" ImageUrl='<%# "/imgs/" + getStatus(Container.DataItem) %>' Width="15px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="RSVP">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="rsvp" runat="server" CommandArgument='<%# Eval("inviteID") %>' CommandName="RSVP" OnCommand="rsvp_Command" Text="Yes"></asp:LinkButton>
                                                                        &nbsp;\&nbsp;
                                                                        <asp:LinkButton ID="unrsvp" runat="server" CommandArgument='<%# Eval("inviteID") %>' CommandName="unRSVP" OnCommand="rsvp_Command" Text="No"></asp:LinkButton>
                                                                        <asp:Image ID="Image2" runat="server" Height="15px" ImageUrl='<%# "/imgs/" + getRSVP(Container.DataItem) %>' Width="15px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Fields>
                                                            <RowStyle Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" VerticalAlign="Top" />
                                                        </asp:DetailsView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="userPosts" runat="server">
                                            <table class="partc_tbl">
                                                <tr>
                                                    <td class="psb_cls">
                                                        <uc5:userPosts ID="userPosts1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ShareInfo" runat="server" OnActivate="Share_Activate">
                            <uc1:userPosts runat="server" ID="userPosts2" />
                        </asp:View>
                        <asp:View ID="UpcomingEvents" runat="server">
                            <uc1:eventGallery ID="eventGallery1" runat="server" />
                        </asp:View>
                        <asp:View ID="eventGallery" runat="server">
                        </asp:View>
                        <asp:View ID="PhotoGallery" runat="server" OnActivate="Gallery_Activate">
                            <uc1:imgGallery runat="server" ID="imgGallery" />
                        </asp:View>
                        <asp:View ID="venueList" runat="server" OnActivate="venueList_Activate">
                            <uc2:VenueListing ID="VenueListing1" runat="server" />
                        </asp:View>
                        <asp:View ID="reviews" runat="server" OnActivate="Review_Activate">

                            <uc4:Blog ID="Blog1" runat="server" />
                        </asp:View>
                        <asp:View ID="Messages" runat="server" OnActivate="Messages_Activate">
                            <table class="innerUser_tbl">
                                <tr>
                                    <td class="user_c">
                                        <div style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; background-color: #24323e; color: #bed2fa; padding: 2px 2px 2px 2px;">Friend Requests:</div>
                                        <asp:GridView ID="friendRequests" DataKeyNames="Identifier" OnRowCommand="friendRequests_RowCommand" runat="server" AutoGenerateColumns="false" Width="100%" ShowHeader="False" BorderColor="#BED2FA" CellPadding="5">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Eval("rFN") + " " + Eval("rLN") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Eval("Com") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField CommandName="AcceptRequest" Text="Accept" />
                                                <asp:ButtonField CommandName="DeleteRequest" Text="Delete" />
                                            </Columns>
                                            <RowStyle BackColor="#BED2FA" ForeColor="#24323E" VerticalAlign="Top" />
                                        </asp:GridView>
                                        <div style="height: 575px; overflow-y: scroll; background-color: white;">
                                            <uc1:chatHistory runat="server" id="chatHistory" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
                <td class="uc">
                    <table class="part_tbl">
                        <tr>
                            <td class="psc_cls">
                                <uc6:userEvent ID="userEvent1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>