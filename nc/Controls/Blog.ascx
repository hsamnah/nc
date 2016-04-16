<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Blog.ascx.cs" Inherits="nc.Controls.Blog" %>
<link href="../style/blog.css" type="text/css" rel="stylesheet" />
<table>
    <tr>
        <th><span>[&emsp;Add Blog&emsp;]</span></th>
    </tr>
    <tr>
        <td class="block_cls">
            <asp:DataList ID="DataList1" runat="server">
            </asp:DataList>
        </td>
    </tr>
</table>