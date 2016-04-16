<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="chatHistory.ascx.cs" Inherits="nc.Controls.chatHistory" %>
<link href="../style/chStyle.css" type="text/css" rel="stylesheet" />
<asp:GridView ID="ChatUsers" DataKeyNames="uName" AutoGenerateColumns="False" ShowHeader="False" runat="server" Width="435px" OnRowDataBound="ChatUsers_RowDataBound" GridLines="None">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate><span class="userItem_cls"><%# Eval("uName") %></span></ItemTemplate>
            <ItemStyle BackColor="White" BorderStyle="None" Font-Bold="True" Width="75px" Font-Names="Verdana" Font-Size="10pt" ForeColor="#24323E" HorizontalAlign="Left" VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <div>
                    <asp:DataList ID="Messages" CssClass="msgStyle_cls" runat="server">
                        <ItemTemplate>
                            <%# Eval("incoming") + ": "%> &emsp;&emsp;&emsp;&emsp;<%#  Eval("D","{0:d}") + "&nbsp;" + Eval("D","{0:t}") %><br />
                            <%# Eval("msg") %><br />
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </ItemTemplate>
            <ItemStyle BackColor="White" BorderStyle="None" Font-Names="Verdana" Font-Size="10pt" ForeColor="#24323E" HorizontalAlign="Left" VerticalAlign="Top" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>