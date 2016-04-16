<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mDirections.ascx.cs" Inherits="nc.Controls.mDirections" %>
<script src="../scripts/jsResus.js" type="text/javascript"></script>
<script src="../scripts/jsDirections.js" type="text/javascript"></script>
<link href="../style/dirStyle.css" type="text/css" rel="stylesheet" />
<input type="hidden" id="jsVenueList" class="jsV_cls" runat="server" />
    <img alt="maping" id="mapBtn" src="../imgs/md.png" />
    <div id="mapSelection" class="mapSelection_cls">
        <table>
            <tr>
                <td colspan="3">Location Address:</td>
            </tr>
            <tr>
                <td colspan="3">Select a venue:<br />
                    <asp:TextBox ID="venue" AutoCompleteType="None" CssClass="venueName_cls" runat="server"></asp:TextBox>
                    <br />
                    ~or~
                </td>
            </tr>
            <tr>
                <td colspan="3">Street Address:<br />
                    <asp:TextBox ID="Street" CssClass="venueStreet_cls" runat="server" TextMode="SingleLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>City:<br />
                        <asp:TextBox ID="city" CssClass="venueAddress_cls" runat="server"></asp:TextBox>
                </td>
                <td>Prov./State:<br />
                        <asp:TextBox ID="psr" CssClass="venueAddress_cls" runat="server"></asp:TextBox></td>
                <td>Country:<br />
                        <asp:TextBox ID="country" CssClass="venueAddress_cls" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    Postal/Zip code:<br />
                    <asp:TextBox ID="pz" CssClass="venueAddress_cls" runat="server" TextMode="SingleLine"></asp:TextBox>
                </td>
                <td>
                    <span class="pBtn_cls" id="pbtn">Preview map</span>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="hidden" id="address" />
                    <div id="map" style="width:253px;height:200px;">
                    </div></td>
            </tr>
            <tr>
                <td colspan="3">
                <input type="hidden" id="destinationBox" class="destinationBox_cls" runat="server" /><span class="pMap_cls" id="pMap">Post Map</span></td>
            </tr>
        </table>
    </div>