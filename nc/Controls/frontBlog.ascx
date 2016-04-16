<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frontBlog.ascx.cs" Inherits="nc.Controls.frontBlog" %>
<link href="../style/gblog.css" type="text/css" rel="stylesheet" />
<div class="primaryContainer_cls">
    <asp:DataList ID="primRep" DataKeyField="catID" runat="server" OnItemDataBound="primRep_ItemDataBound" RepeatLayout="Flow" RepeatColumns="0" RepeatDirection="Horizontal">
        <ItemTemplate>
            <div class="container_items_cls">
                <asp:Repeater ID="catItems" runat="server">
                    <HeaderTemplate>
                        <table>
                            <tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <td>
                            <div class="bcontainer_cls">
                                <span class="bTitle_cls"><%# Eval("Title") %></span>
                            </div>
                            <img src='<%# "/anima/" + getRandomImg() %>' class="bgImg_cls" /></td>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tr></table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:DataList>
</div>