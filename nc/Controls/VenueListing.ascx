<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VenueListing.ascx.cs" Inherits="nc.Controls.VenueListing" %>
<link href="../style/venueListing.css" type="text/css" rel="stylesheet" />

<table class="primaryContainer_cls">
    <tr>
        <td class="primaryHeader_cls">
            <table class="locationSelector_cls">
                <tr>
                    <td class="current_cls">Current Location:</td>
                    <td class="country_cls">Country:</td>
                    <td class="region_cls">Region:</td>
                    <td class="city_cls">City/Town:</td>
                </tr>
                <tr>
                    <td class="current_cls">
                        <asp:Label ID="CurrentLocation" runat="server"></asp:Label>
                    </td>
                    <td class="country_cls">
                        <asp:DropDownList ID="country" CssClass="ddm_cls" DataTextField="CountryName" DataValueField="CountryCode" AutoPostBack="true" OnSelectedIndexChanged="country_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="region_cls">
                        <asp:DropDownList ID="region" CssClass="ddm_cls" DataTextField="Subdivision" DataValueField="Subdivision" AutoPostBack="true" OnSelectedIndexChanged="region_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="city_cls">
                        <asp:DropDownList ID="city" CssClass="ddm_cls" DataTextField="City_Town" DataValueField="recordID" AutoPostBack="true" OnSelectedIndexChanged="city_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="primaryContentContainer_cls">
            <div id="venueContainer" runat="server" class="venueContainer_cls">
            </div>
        </td>
    </tr>
</table>