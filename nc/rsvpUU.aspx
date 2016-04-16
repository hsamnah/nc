<%@ Page Title="" Language="C#" MasterPageFile="~/root3.Master" AutoEventWireup="true" CodeBehind="rsvpUU.aspx.cs" Inherits="nc.rsvpuu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jsRegister.js" type="text/javascript"></script>
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
                            <td class="tformCell_cls">User Registration:</td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Birthdate:<br />
                                <select id="Day" runat="server" style="width: 100px; background-color: transparent; color: white;">
                                    <option style="color: black;" value="0">Day</option>
                                    <option style="color: black;" value="1">01</option>
                                    <option style="color: black;" value="2">02</option>
                                    <option style="color: black;" value="3">03</option>
                                    <option style="color: black;" value="4">04</option>
                                    <option style="color: black;" value="5">05</option>
                                    <option style="color: black;" value="6">06</option>
                                    <option style="color: black;" value="7">07</option>
                                    <option style="color: black;" value="8">08</option>
                                    <option style="color: black;" value="9">09</option>
                                    <option style="color: black;" value="10">10</option>
                                    <option style="color: black;" value="11">11</option>
                                    <option style="color: black;" value="12">12</option>
                                    <option style="color: black;" value="13">13</option>
                                    <option style="color: black;" value="14">14</option>
                                    <option style="color: black;" value="15">15</option>
                                    <option style="color: black;" value="16">16</option>
                                    <option style="color: black;" value="17">17</option>
                                    <option style="color: black;" value="18">18</option>
                                    <option style="color: black;" value="19">19</option>
                                    <option style="color: black;" value="20">20</option>
                                    <option style="color: black;" value="21">21</option>
                                    <option style="color: black;" value="22">22</option>
                                    <option style="color: black;" value="23">23</option>
                                    <option style="color: black;" value="24">24</option>
                                    <option style="color: black;" value="25">25</option>
                                    <option style="color: black;" value="26">26</option>
                                    <option style="color: black;" value="27">27</option>
                                    <option style="color: black;" value="28">28</option>
                                    <option style="color: black;" value="29">29</option>
                                    <option style="color: black;" value="30">30</option>
                                    <option style="color: black;" value="31">31</option>
                                </select>
                                <select id="Month" runat="server" style="width: 100px; background-color: transparent; color: white;">
                                    <option style="color: black;" value="0">Month</option>
                                    <option style="color: black;" value="1">01</option>
                                    <option style="color: black;" value="2">02</option>
                                    <option style="color: black;" value="3">03</option>
                                    <option style="color: black;" value="4">04</option>
                                    <option style="color: black;" value="5">05</option>
                                    <option style="color: black;" value="6">06</option>
                                    <option style="color: black;" value="7">07</option>
                                    <option style="color: black;" value="8">08</option>
                                    <option style="color: black;" value="9">09</option>
                                    <option style="color: black;" value="10">10</option>
                                    <option style="color: black;" value="11">11</option>
                                    <option style="color: black;" value="12">12</option>
                                </select>
                                <select id="Year" runat="server" style="width: 100px; background-color: transparent; color: white;">
                                    <option style="color: black;" value="0">Year</option>
                                    <option style="color: black;">1996</option>
                                    <option style="color: black;">1995</option>
                                    <option style="color: black;">1994</option>
                                    <option style="color: black;">1993</option>
                                    <option style="color: black;">1992</option>
                                    <option style="color: black;">1991</option>
                                    <option style="color: black;">1990</option>
                                    <option style="color: black;">1989</option>
                                    <option style="color: black;">1988</option>
                                    <option style="color: black;">1987</option>
                                    <option style="color: black;">1986</option>
                                    <option style="color: black;">1985</option>
                                    <option style="color: black;">1984</option>
                                    <option style="color: black;">1983</option>
                                    <option style="color: black;">1982</option>
                                    <option style="color: black;">1981</option>
                                    <option style="color: black;">1980</option>
                                    <option style="color: black;">1979</option>
                                    <option style="color: black;">1978</option>
                                    <option style="color: black;">1977</option>
                                    <option style="color: black;">1976</option>
                                    <option style="color: black;">1975</option>
                                    <option style="color: black;">1974</option>
                                    <option style="color: black;">1973</option>
                                    <option style="color: black;">1972</option>
                                    <option style="color: black;">1971</option>
                                    <option style="color: black;">1970</option>
                                    <option style="color: black;">1969</option>
                                    <option style="color: black;">1968</option>
                                    <option style="color: black;">1967</option>
                                    <option style="color: black;">1966</option>
                                    <option style="color: black;">1965</option>
                                    <option style="color: black;">1964</option>
                                    <option style="color: black;">1963</option>
                                    <option style="color: black;">1962</option>
                                    <option style="color: black;">1961</option>
                                    <option style="color: black;">1960</option>
                                    <option style="color: black;">1959</option>
                                    <option style="color: black;">1958</option>
                                    <option style="color: black;">1957</option>
                                    <option style="color: black;">1956</option>
                                    <option style="color: black;">1955</option>
                                    <option style="color: black;">1954</option>
                                    <option style="color: black;">1953</option>
                                    <option style="color: black;">1952</option>
                                    <option style="color: black;">1951</option>
                                    <option style="color: black;">1950</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Username:&emsp;<asp:RequiredFieldValidator ControlToValidate="un" ID="unamVal" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                <br />
                                <asp:TextBox ID="un" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Password:<asp:RequiredFieldValidator ID="pr1" runat="server" ControlToValidate="pwd1" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="pwd1" CssClass="password_cls" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Confirm Password:<span id="err" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: 9pt; color: #FF0000"></span><asp:RequiredFieldValidator ID="pr2" runat="server" ControlToValidate="pwd2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="pwd2" CssClass="password_cls" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Security question:&emsp;<asp:RequiredFieldValidator ControlToValidate="sq" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="sq" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Answer:&emsp;<asp:RequiredFieldValidator ControlToValidate="sa" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="sa" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Hint:&emsp;<asp:RequiredFieldValidator ControlToValidate="sh" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="sh" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="User_register" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="User_register_Click">Next</asp:LinkButton>&emsp;<asp:Label ID="errMsg" runat="server" Visible="False" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" ForeColor="Red" Font-Size="9pt"></asp:Label></td>
                        </tr>
                        <tr id="sTR" runat="server" visible="false">
                            <td class="formCell_cls">
                                <asp:Label ID="sMsg" runat="server" Visible="False" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" ForeColor="White" Font-Size="10pt"></asp:Label></td>
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