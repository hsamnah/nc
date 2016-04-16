<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="datePicker.ascx.cs" Inherits="nc.Controls.datePicker" %>
<script src="../scripts/datepicker.js" type="text/javascript"></script>
<div runat="server" id="dpContainer" style="display: inline-block;" clientidmode="Static">
    <input type="hidden" id="cIdentifier" class="cIdentifier_cls" runat="server" />
    <input type="hidden" id="container" runat="server" class="container_cls" />
    <div id="mainContainer" class="mainContainer_Cls" style="height: 30px; border: solid 1px #24323e; padding: 2px 2px 2px 2px; background-color: #BED2FA; display: inline-block; vertical-align: top;">
        <asp:TextBox ID="eventDate" CssClass="eventDate_Cls" ClientIDMode="Static" Enabled="false" runat="server" BackColor="#BED2FA" BorderStyle="None" Height="20px" Width="150px"></asp:TextBox>&nbsp;
        <asp:ImageButton runat="server" ID="calImg" Height="20px" ImageUrl="~/imgs/Calendar.png" OnClick="calToggle_click" />
    </div>
    <div id="calContainer" class="calContainer_Cls" clientidmode="Static" runat="server" style="display: inline-block; position: absolute; z-index: 10000;" visible="false">
        <asp:Calendar ID="dpCal" OnSelectionChanged="dpCal_SelectionChanged" OnDayRender="dpCal_DayRender" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="180px" TitleFormat="Month" Width="188px" OnVisibleMonthChanged="month_change">
            <DayHeaderStyle BackColor="#24323E" Font-Bold="True" Font-Size="7pt" ForeColor="#4A5F76" Height="10pt" />
            <DayStyle Width="14%" />
            <NextPrevStyle Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#24323E" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
            <TitleStyle BackColor="#3E566A" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
            <TodayDayStyle BackColor="#3E566A" ForeColor="#BDCBCC" />
        </asp:Calendar>
    </div>
</div>