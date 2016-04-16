<%@ Page Title="" Language="C#" MasterPageFile="~/root2.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="nc.root.defaultroot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jsBlog.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="rootTbl_cls">
        <tr>
            <td class="tl_cls">
                <div id="backDrop" class="backdrop_cls">
                    <div class="functionBar_cls">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 97%"></td>
                                <td style="width: 3%;">
                                    <img id="closeBtn" src="imgs/closeInvert.gif" style="height: 50px; width: 40px; cursor: pointer;" /></td>
                            </tr>
                        </table>
                    </div>
                    <div id="innerContainer" class="internal_Container_cls">
                        <br />
                        <br />
                        <span id="rTitle" class="title"></span>
                        <br />
                        <hr class="break" />
                        <span id="rAuthor" class="author"></span>&emsp;<span id="rDate" class="date"></span><br />
                        <br />
                        <div style="width: 620px; height: 720px; overflow-y: auto;">
                            <span id="rDesc" class="description"></span>
                            <br />
                            <br />
                            <br />
                            <br />
                            <span id="rDetails" class="details"></span>
                        </div>
                    </div>
                </div>
            </td>
            <td class="tc_cls">&nbsp;</td>
            <td class="tr_cls">&nbsp;</td>
        </tr>
        <tr>
            <td class="ml_cls" rowspan="2">
                <div class="blogContainer_cls">

                    <asp:DataList ID="g_blog" DataKeyField="Identifier" runat="server">
                        <ItemTemplate>
                            <input class="hidIdentifier" type="hidden" id="bIdentifier" value='<%# Eval("Identifier") %>' />
                            <input class="hidDesc" type="hidden" id="desc" value='<%# Eval("BD") %>' />
                            <input class="hidArticle" type="hidden" id="bArticle" value="<%# Eval("Article") %>" />
                            <input class="hidAuthor" type="hidden" id="hAuth" value="" />
                            <span class="bTitle_cls"><%# Eval("Title")%></span><br />
                            <span class="bDate_cls"><%# Eval("bDate","{0:d}") %>&nbsp;♦</span><br />
                            <br />
                            <%# getDesc(Container.DataItem) %><br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </td>
            <td class="mc_cls" rowspan="2">
                <div id="mainPanel" class="panel_cls">
                    <table class="formCell_cls">
                        <tr>
                            <td class="innerContent_cls" rowspan="7"><br /><br />
                                    <span class="Enlarge_cls">Organize events</span><br /><span class="Reduced_cls">at either a private location or at a member venue.</span><br /><br />
                                    <span class="Enlarge_cls">Share details</span><br /><span class="Reduced_cls">of events you've been to or planning to attend.</span><br /><br />
                                    <span class="Enlarge_cls">RSVP or invite friends</span><br /><span class="Reduced_cls">to events in your area.</span><br /><br />
                                    <span class="Enlarge_cls">Network</span><br /><span class="Reduced_cls">with friends or members you'd like to meet.</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tformCell_cls">
                                <asp:Label ID="regCom" runat="server" Visible="False"></asp:Label>
                                Login:</td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Username:<br />
                                <asp:TextBox ID="un" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">Password:<br />
                                <asp:TextBox ID="pwd" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">User type:<br />
                                <select id="uType" runat="server" style="width: 100px; background-color: transparent; color: white;">
                                    <option style="color: black;">--User Type--</option>
                                    <option style="color: black;" value="User">General User</option>
                                    <option style="color: black;" value="Venue">Venue</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">
                                <asp:LinkButton ID="login" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="12pt" ForeColor="White" OnClick="login_Click">Login</asp:LinkButton>&emsp;
                                <asp:Label ID="selectUtype" runat="server" Visible="False" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="9pt" ForeColor="White"></asp:Label>
                                <asp:Label ID="accessCor" runat="server" Visible="False" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="9pt" ForeColor="White"></asp:Label>&emsp;
                                <asp:LinkButton ID="pwdRec" runat="server" Font-Names="'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="9pt" Font-Bold="true" ForeColor="White" Visible="false" OnClick="pwdRec_Click">Click here.</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="formCell_cls">New user: <a class="r_btn_cls" href="register.aspx">here</a><br />
                                New Venue: <a class="r_btn_cls" href="vRegistry.aspx">here</a>.
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="mr_cls"></td>
        </tr>
        <tr>
            <td class="br_cls">&nbsp;</td>
        </tr>
    </table>
</asp:Content>