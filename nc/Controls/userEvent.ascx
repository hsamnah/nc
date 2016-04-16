<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="userEvent.ascx.cs" Inherits="nc.Controls.userEvent" %>
<%@ Register Src="~/Controls/datePicker.ascx" TagPrefix="uc1" TagName="datePicker" %>
<%@ Register Src="~/Controls/timeControl.ascx" TagPrefix="uc1" TagName="timeControl" %>

<link href="/style/ueStyle.css" type="text/css" rel="stylesheet" />
<table class="container_tbl">
    <tr>
        <td class="ab">Your organized events:<br />
            <span id="create" class="button_cls">Create event</span>
            <br />
        </td>
    </tr>
    <tr>
        <td class="eList_cls">
            <asp:GridView ID="EventListing" OnRowDataBound="EventListing_RowDataBound" DataKeyNames="eID" AutoGenerateColumns="False" ShowHeader="False" OnRowDeleting="EventListing_RowDeleting" runat="server" GridLines="None" Width="100%">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <!-- This area is for a list of invitees //-->
                            <div id='<%# "container_" + Eval("eID") %>' class="euManageB_cls">
                                <div style="padding: 5px 5px 5px 5px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="width: 50px; height:30px;">
                                                <img src="../imgs/closeInvert.gif" alt="close" id="cmdBtn1" class="cmdBtn2_cls" style="width: 50px;height:30px; cursor: pointer;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="innerC_cls" style="padding: 5px 5px 5px 5px;">
                                    <div style="background-image: url('/imgs/abstract.gif'); height: 75px; width: 100%;"></div>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="uEvent_Form_cls">
                                                <!-- This will include an auto suggest drop down.  If a user who is currently in the network the user will automaticaly be formatted and placed in the textbox with an identifier. //-->
                                                <br />
                                                List users to invite to this event:<br />
                                                <span style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 8pt; color: #24323e;">(Seperate each user/email pair with a ",", seperate the users first and last name from the email address with a ":"; ie, John Doe:jd@i-underground.com,Jane Doe:janedoe@i-underground.com.
                                            Users on your friend list will be appear in a drop down pop up menue that you can select.)</span>
                                                <br />
                                                <br />
                                                <div class="ddown2_cls" id='<%# "dd_" + Eval("eID") %>'></div>
                                                <div contenteditable="true" id='<%# "inviteList_" + Eval("eID") %>' class="inviteList2_cls"></div>
                                                <div id='<%# "intellisense_" + Eval("eID") %>' class="intellisense2_cls"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #B4C6D3; color: #24323e; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt;">Checkmark users you wish to uninvite to your event:<br />
                                                <div style="padding: 10px 10px 10px 10px; height: 150px; width: 583px; overflow-y: scroll;">
                                                    <asp:CheckBoxList AutoPostBack="false" ID="EventInvites" runat="server" RepeatColumns="3"></asp:CheckBoxList>
                                                </div>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="Cancel" runat="server" Text="Cancel" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="#24323e" OnClick="Cancel_Click"></asp:LinkButton>&emsp;&emsp;<asp:LinkButton ID="sInvitations" runat="server" Text="Submit Changes" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="10pt" ForeColor="#24323e" OnClick="sInvitations_Click"></asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <%# Eval("eDate","{0:d}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <span class="euManageB_cls" id='<%# "mgmtBtn_" + Eval("eID") %>'>Manage</span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField DeleteText="Cancel" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <input type="hidden" id="invitedMgmt2" runat="server" class="hiddenInvited2_cls" />
        </td>
    </tr>
</table>
<input type="hidden" id="ecState" value="false" class="state_cls" runat="server" />
<div id="createEC" class="CEC_cls">
    <input type="hidden" id="containerID" value="innerCContainer" runat="server" />
    <div>
        <table style="width: 100%;">
            <tr>
                <td>&nbsp;</td>
                <td style="width: 50px;">
                    <img src="../imgs/closeInvert.gif" alt="close" id="cmdBtn1" class="cmdBtn_cls" style="width: 50px; cursor: pointer;" /></td>
            </tr>
        </table>
    </div>
    <div id="innerCContainer" class="ICEC_cls">
        <div style="background-image: url('/imgs/abstract.gif'); height: 80px; width: 100%;"></div>
        <table style="z-index: 10003; width: 100%;">
            <tr>
                <td class="uEvent_Form_cls">Label event:</td>
            </tr>
            <tr>
                <td class="uEvent_Form_cls">
                    <asp:TextBox ID="elbl" runat="server" Width="100%" TextMode="SingleLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="uEvent_Form_cls">Location:</td>
            </tr>
            <tr>
                <td class="uEvent_Form_cls">(Apt) Street:<br />
                    <asp:TextBox ID="street" runat="server" Width="100%" TextMode="SingleLine"></asp:TextBox><br />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 33%;">City:</td>
                            <td style="width: 33%;">Province:</td>
                            <td style="width: 33%;">Country:</td>
                        </tr>
                        <tr>
                            <td style="width: 33%;">
                                <asp:TextBox ID="city" runat="server" Width="100%" TextMode="SingleLine"></asp:TextBox></td>
                            <td style="width: 33%;">
                                <asp:TextBox ID="province" runat="server" Width="100%" TextMode="SingleLine"></asp:TextBox></td>
                            <td style="width: 33%;">
                                <asp:TextBox ID="country" runat="server" Width="100%" TextMode="SingleLine"></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
                    Postal Code:<br />
                    <asp:TextBox ID="pz" runat="server" Width="100%" TextMode="SingleLine"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td class="uEvent_Form_cls">Date and Time:<br />
                    <uc1:datePicker runat="server" ID="datePicker" />
                    &nbsp;<uc1:timeControl runat="server" ID="timeControl" />
                </td>
            </tr>
            <tr>
                <td class="uEvent_Form_cls">
                    <!-- This will include an auto suggest drop down.  If a user who is currently in the network the user will automaticaly be formatted and placed in the textbox with an identifier. //-->
                    <br />
                    List users to invite to this event:<br />
                    <span style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 8pt; color: #24323e;">User format example: John Doe:jd@i-underground.com,Jane Doe:janeD@i-underground.com.</span>
                    <br />
                    <br />
                    <div class="ddown_cls" id="dd"></div>
                    <input type="hidden" id="friendList" class="hiddenFL_cls" runat="server" />
                    <div contenteditable="true" id="inviteList" class="inviteList_cls"></div>
                    <input type="hidden" id="invited" runat="server" class="hiddenInvited_cls" />
                    <div id="intellisense" class="intellisense_cls"></div>
                </td>
            </tr>
            <tr>
                <td class="uEvent_Form_cls">Event Description:
                    <asp:TextBox ID="desc" runat="server" TextMode="MultiLine" Width="99%" Height="65px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="uEvent_Form_cls">
                    <asp:LinkButton ID="sinvites" runat="server" Text="Send Invites" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="#24323E" OnClick="sinvites_Click"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</div>