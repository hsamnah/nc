<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eventGallery.ascx.cs" Inherits="nc.Controls.eventGallery" %>
<link href="../style/veListing.css" type="text/css" rel="stylesheet" />
<asp:GridView ID="venue2" DataKeyNames="vIdentifier" ShowHeader="false" GridLines="None" AutoGenerateColumns="false" runat="server" OnRowDataBound="Venue2_ItemDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <div style="width: 425px; background-color: #24323e; height: 50px; color: white; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 12pt; font-weight: 700; padding: 5px 5px 5px 5px;">
                    <%# Eval("VenueName_fld") %><br />
                    <span style="color: white; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 10pt; font-weight: 500;"><%# Eval("street1") %>&nbsp;<%# Eval("street2") %>,&nbsp;<%# Eval("city") %>,&nbsp;<%# Eval("region") %>&nbsp;<%# Eval("postal") %>,&nbsp;<%# Eval("Country") %></span>
                </div>
                <div id="primaryEventContainer" runat="server" class="primary_event_container_cls">
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>