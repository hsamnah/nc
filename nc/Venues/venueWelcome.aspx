<%@ Page Title="" Language="C#" MasterPageFile="~/Venues/root.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="venueWelcome.aspx.cs" Inherits="nc.Venues.welcome" %>

<%@ Register Src="../Controls/datePicker.ascx" TagName="datePicker" TagPrefix="uc1" %>
<%@ Register Src="../Controls/timeControl.ascx" TagName="timeControl" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/posterCtrl.ascx" TagPrefix="uc1" TagName="posterCtrl" %>
<%@ Register Src="~/Controls/userPosts.ascx" TagPrefix="uc1" TagName="userPosts" %>
<%@ Register Src="~/venueCntrls/imgGallery.ascx" TagPrefix="uc1" TagName="imgGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="VenueC" runat="server">
        <table class="MainTable_cls">
            <tr>
                <td colspan="3" class="v">
                    <div class="viewbtns_cls">
                        <table>
                            <tr>
                                <td id="viconContainer">
                                    <asp:LinkButton ID="venueProfileBtn" OnCommand="venueBtn_Command" CommandName="vProfile" CssClass="icons" runat="server">[ profile ]</asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="venueSharedBtn" OnCommand="venueBtn_Command" CommandName="vShared" CssClass="icons" runat="server">[ shared ]</asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="venueGalleryBtn" OnCommand="venueBtn_Command" CommandName="vGallery" CssClass="icons" runat="server">[ gallery ]</asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="venueEventBtn" OnCommand="venueBtn_Command" CommandName="vEvents" CssClass="icons" runat="server">[ events ]</asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="venueReviewsBtn" OnCommand="venueBtn_Command" CommandName="vReviews" CssClass="icons" runat="server">[ reviews/comments ]</asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="venueMessagesBtn" OnCommand="venueBtn_Command" CommandName="vMessages" CssClass="icons" runat="server">[ messages ]</asp:LinkButton>&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="va">
                    <div style="padding: 2px 2px 2px 5px; width: 97%; background-color: #21262b;">
                        <br />
                        <span style="color: white;">Upcoming events:</span><br />
                        <br />
                    </div>
                    <asp:Calendar ID="VenueEvents0" runat="server" CellPadding="5" CssClass="mainCalendar_cls" Height="150px" OnDayRender="render_vDay" Width="240px" OnSelectionChanged="Event_Change">
                        <DayHeaderStyle Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#24323E" />
                        <DayStyle Font-Size="10pt" Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#B4C6D3" />
                        <SelectedDayStyle BackColor="#92999F" />
                        <TitleStyle Font-Size="10pt" Font-Names="'Lucida Sans','Lucida Sans Regular' ,'Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#3E566A" ForeColor="#CEDAE3" Height="10px" />
                        <WeekendDayStyle Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" />
                    </asp:Calendar>
                    <asp:Label ID="errLbl" ForeColor="Red" Visible="false" runat="server"></asp:Label>
                    <asp:DataList ID="eventDetails" runat="server" CellPadding="2" ShowFooter="False" ShowHeader="False">
                        <ItemTemplate>
                            <table style="width: 240px;">
                                <tr>
                                    <td colspan="2" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; font-weight: 700; background-color: #21262b; color: white; padding: 2px 2px 4px 4px; width: 250px;"><%# Eval("eTitle") %></td>
                                </tr>
                                <tr>
                                    <td style="width: 105px;">
                                        <img src='<%# getEventImage(Container.DataItem,Container.DataItem) %>' style="width: 100px" /></td>
                                    <td style="vertical-align: top; padding: 2px 2px 2px 2px; width: 145px; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; font-weight: 400; text-align: left;"><%# subDescription(Container.DataItem) %></td>
                                </tr>
                                <tr>
                                    <td>Date:</td>
                                    <td><%# Eval("evDate","{0:d}") %></td>
                                </tr>
                                <tr>
                                    <td>Time:</td>
                                    <td><%# Eval("eTime") %></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: right;">
                                        <asp:LinkButton ID="DeleteEvent" runat="server" Text="Delete Event" OnCommand="rootDir_Command" CommandName="DeleteEvent" CommandArgument='<%# Eval("eid") %>' Font-Names=" 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="White"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
                <td class="vb">
                    <asp:MultiView ID="venueMV" runat="server" EnableTheming="False" Visible="False">
                        <asp:View ID="venueView" runat="server" OnActivate="venueView_Activate">
                            <asp:Panel ID="venueAddContainer" runat="server" BackColor="#B4C6D3" CssClass="FormContainer_cls">
                                <asp:Panel ID="StaffPanel" CssClass="VATConainer_cls" runat="server">
                                    Issued user memberships with this venue account:
                                </asp:Panel>
                                <asp:GridView ID="cVUsers" DataKeyNames="vuaID" runat="server" AutoGenerateColumns="False" OnRowDeleting="cVUsers_RowDeleting" Width="100%" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <%# Eval("FirstName") %>&nbsp;<%# Eval("LastName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Address">
                                            <ItemTemplate>
                                                <%# Eval("EmailAddress") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Role">
                                            <ItemTemplate>
                                                <%# Eval("vRole") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img id="isAck" src="<%# isAcknowledged(Container.DataItem) %>" width="15px" />
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True">
                                            <ControlStyle Font-Names="Verdana" Font-Size="10pt" ForeColor="#24323E" />
                                        </asp:CommandField>
                                    </Columns>
                                    <EditRowStyle Wrap="False" />
                                </asp:GridView>
                                <br />
                                <br />
                                <fieldset id="addUser_fs" runat="server">
                                    <legend>Add new user</legend>
                                    <table id="addVUser_tbl" runat="server">
                                        <tr id="head0" runat="server">
                                            <td id="emailHead" runat="server">Email:</td>
                                            <td id="fNameHead" runat="server">First Name:</td>
                                            <td id="lNameHead" runat="server">Last Name:</td>
                                            <td id="roleHad" runat="server">Role:</td>
                                        </tr>
                                        <tr id="inputRow" runat="server">
                                            <td id="inputEmail" runat="server">
                                                <asp:TextBox ID="emailVAddy" runat="server" Width="130px"></asp:TextBox>
                                            </td>
                                            <td id="inputFName" runat="server">
                                                <asp:TextBox ID="UserFName" runat="server" Width="75px"></asp:TextBox>
                                            </td>
                                            <td runat="server" id="intputLName">
                                                <asp:TextBox ID="UserLName" runat="server" Width="75px"></asp:TextBox>
                                            </td>
                                            <td id="inputRole" runat="server">
                                                <asp:DropDownList ID="VenueRoles" runat="server" Width="140px">
                                                    <asp:ListItem Text="Select role--"></asp:ListItem>
                                                    <asp:ListItem Text="General Staff" Value="General Staff"></asp:ListItem>
                                                    <asp:ListItem Text="Marketing" Value="Marketing"></asp:ListItem>
                                                    <asp:ListItem Text="Security" Value="Security"></asp:ListItem>
                                                    <asp:ListItem Text="Administration" Value="Administration"></asp:ListItem>
                                                    <asp:ListItem Text="Inventory Control" Value="Inventory Control"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="eventRow" runat="server">
                                            <td class="addUserCell_cls" runat="server" colspan="4">
                                                <asp:Label ID="eCheck" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="#24323E"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="addVenueUser" runat="server" OnClick="addVenueUser_Click" Text="Add" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="#24323E"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                            <uc1:posterCtrl runat="server" ID="posterCtrl" />
                            <uc1:userPosts runat="server" ID="userPosts" />
                        </asp:View>
                        <asp:View ID="venueShared" runat="server">
                            <asp:Panel ID="SharedPanel" runat="server" Width="432px">
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="venueGallery" runat="server">
                            <uc1:imgGallery runat="server" ID="imgGallery" />
                        </asp:View>
                        <asp:View ID="venueEvent" runat="server">
                            <asp:Panel ID="Events" runat="server" Width="432px">
                                <table class="eventStructure_cls">
                                    <tr>
                                        <td class="bannerLoad_cls">Find banner:
                                            <asp:FileUpload ID="bannerLoad" runat="server" />&#8195;<asp:LinkButton ID="upload" runat="server" Text="Upload" OnCommand="rootDir_Command" CommandName="UploadBanner" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="White"></asp:LinkButton>
                                            <br />
                                            <asp:Label ID="transNotice" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="uploadedBanner" runat="server" class="BannerDis_cls">
                                            <asp:Image ID="imgBanner" runat="server" Visible="false" Width="400px" /><asp:HiddenField ID="imgID" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="specifics_cls">
                                            <div class="eventBorder_cls">
                                                <table class="eventBorder_cls">
                                                    <tr>
                                                        <td class="eventBorder_cls">Event:</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="eventBorder_cls">
                                                            <asp:TextBox ID="eventTitle" runat="server" Width="400px" BorderStyle="None" BackColor="#BED2FA" Height="30px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="eventBorder_cls">
                                                <table class="eventBorder_cls">
                                                    <tr>
                                                        <td class="eventBorder_cls">Date and time:</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="eventBorder_cls">
                                                            <uc1:datePicker ID="datePicker1" runat="server" />
                                                            <uc2:timeControl ID="timeControl1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="eventBorder_cls">
                                                            <asp:RadioButton ID="recuring0" runat="server" Text="Recurring" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dfield_cls">
                                            <div class="eventBorder_cls">
                                                <table class="eventBorder_cls">
                                                    <tr>
                                                        <td class="eventBorder_cls">Description:</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="eventBorder_cls">
                                                            <asp:Panel runat="server" ID="Panel1">
                                                                <table class="prime_min_tbl_cls">
                                                                    <tr>
                                                                        <td class="toolbar_cls">Post a message to staff with an account:</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="toolbar_cls">
                                                                            <img alt="Bold" id="boldBtn2" src="../imgs/Bold.png" class="ctrlButtons_cls" />&nbsp;
                                                    <img alt="Italic" id="italicBtn2" src="../imgs/Italic.png" class="ctrlButtons_cls" />&nbsp;
                                                    <img alt="Underline" id="underlineBtn2" src="../imgs/Underline.png" class="ctrlButtons_cls" />&nbsp;
                                                    <img alt="Left" id="leftAlignBtn2" src="../imgs/leftAlign.png" class="ctrlButtons_cls" />&nbsp;
                                                    <img alt="Center" id="centerAlignBtn2" src="../imgs/centerAlign.png" class="ctrlButtons_cls" />&nbsp;
                                                    <img alt="Right" id="rightAlignBtn2" src="../imgs/rightAlign.png" class="ctrlButtons_cls" />&nbsp;
                                                    <img alt="Justify" id="justifyBtn2" src="../imgs/FullJustify.png" class="ctrlButtons_cls" />&nbsp;|&nbsp;

                                                    <div id="clrSelection2" class="clrSelection_cls"></div>
                                                                            <img id="clrPicker2" src="../imgs/colorSwatch.png" />&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="editor_cls">
                                                                            <asp:HiddenField ID="eventDescription" ClientIDMode="Static" runat="server" />
                                                                            <div contenteditable="true" id="Div1" clientidmode="Static" runat="server" class="InfoBox_cls"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="buttonContainer_cls">
                                                                            <table class="btnCollect_tbl_cls">
                                                                                <tr>
                                                                                    <td class="btnContain_cls">
                                                                                        <asp:Panel ID="Panel2" ClientIDMode="Static" CssClass="listContainer_cls" runat="server">
                                                                                            <asp:Repeater ID="Repeater1" runat="server">
                                                                                                <ItemTemplate>
                                                                                                    <table id='<%# Eval("eventID") %>' class="tblDDL_cls">
                                                                                                        <tr>
                                                                                                            <td id="e" class="tdDDL_cls">
                                                                                                                <%# "Event: " + Eval("eventTitle") %>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="tdAltDDL_cls"><%# "Venue: " + Eval("VenueName") %></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td class="sendBtnContainer_cls">
                                                                                        <asp:ImageButton ID="postEvent" runat="server" ClientIDMode="Static" CausesValidation="false" CssClass="psImgSend_cls" ImageUrl="~/imgs/postSend.png" OnCommand="rootDir_Command" CommandName="postEvent" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:Label ID="test" runat="server"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="venueReviews" runat="server">
                            <asp:Panel ID="Reviews" runat="server" Width="432px">
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="venueMessages" runat="server">
                            <asp:Panel ID="Messages" runat="server" Width="432px">
                            </asp:Panel>
                        </asp:View>
                    </asp:MultiView>
                </td>
                <td class="vc">&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>