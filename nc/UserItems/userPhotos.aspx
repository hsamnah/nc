<%@ Page Title="" Language="C#" MasterPageFile="~/UserItems/minUser.Master" AutoEventWireup="true" CodeBehind="userPhotos.aspx.cs" Inherits="nc.UserItems.userPhotos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="imgContainerPhotos" style="background-color: #535b6d">
        <table style="width: 100%; border-collapse: collapse; border-spacing: 0;">
            <tr>
                <td style="padding: 5px 5px 5px 5px; vertical-align: top; width: 30%">
                    <div>
                        <div style="padding: 5px 5px 5px 5px; vertical-align: top;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="newFolderName" ClientIDMode="Static" BorderStyle="None" Font-Names=" 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="8pt" Height="10px" Width="200px"></asp:TextBox>&nbsp;<asp:ImageButton ID="AddDirectory" ClientIDMode="Static" runat="server" ImageUrl="~/imgs/add_folder.png" Width="15px" OnClick="AddDirectory_Click" AlternateText="Add directory" />
                                    </td>
                                    <td style="text-align: right; vertical-align: top;">
                                        <asp:ImageButton ID="DeleteDirectory" ClientIDMode="Static" runat="server" ImageUrl="~/imgs/close.png" Width="15px" OnClick="DeleteDirectory_Click" AlternateText="Delete selected directory" /></td>
                                </tr>
                            </table>
                        </div>
                        <div style="padding: 2px 2px 2px 2px; border: 1px solid #3a3b40; height: 500px; overflow-y: scroll;">
                            <asp:TreeView ID="DirectoryListing" runat="server" ImageSet="Msdn" NodeIndent="10" Width="150px" OnSelectedNodeChanged="DirecorySelect_Change">
                                <HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="Solid" Font-Underline="True" />
                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="1px" VerticalPadding="2px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle BackColor="#6F7797" BorderStyle="None" BorderWidth="1px" Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
                            </asp:TreeView>
                            <asp:Label ID="dirErr" runat="server" Font-Names=" 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif" Font-Size="8pt" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                </td>
                <td style="padding: 5px 5px 5px 5px; vertical-align: top; width: 70%;">
                    <div>
                        <div style="padding: 5px 5px 5px 5px; vertical-align: top;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="FileLoad" runat="server" />&nbsp;&nbsp;<asp:ImageButton ID="addImg" ClientIDMode="static" runat="server" ImageUrl="~/imgs/add.png" Width="15px" OnClick="addImg_Click" AlternateText="Upload file" />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:ImageButton ID="deleteFile" ClientIDMode="Static" runat="server" ImageUrl="~/imgs/close.png" Width="15px" OnClick="deleteFile_Click" Style="height: 17px" AlternateText="Delete selected file" /></td>
                                </tr>
                            </table>
                        </div>
                        <div style="padding: 2px 2px 2px 2px; border: 1px solid #3a3b40; height: 500px; overflow-y: scroll;">
                            <asp:DataList ID="Files" runat="server" GridLines="None" RepeatColumns="4" DataKeyField="FullName">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgsThumb" ImageUrl='<%# urlConversion(Container.DataItem) %>' Style="width: 150px;" ClientIDMode="Static" runat="server" OnClick="ImgsThumb_Click" />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="background-color: #535b6d"></div>
</asp:Content>