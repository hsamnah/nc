<%@ Page Title="" Language="C#" MasterPageFile="~/root.Master" AutoEventWireup="true" CodeBehind="vRegistry2.aspx.cs" Inherits="nc.vReg" %>

<%@ Register Src="Controls/timeControl.ascx" TagName="timeControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 { width: 171px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="wizContainer_cls">
        <tr>
            <td class="wizHeader_cls">New Venue Registration</td>
        </tr>
        <tr>
            <td class="regContainer_cls">
                <asp:Wizard ID="vRegWizard" OnActiveStepChanged="vRegWizard_ActiveStepChanged" BackColor="#EFF3FB" BorderWidth="0px" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" Width="100%" ActiveStepIndex="5" runat="server" OnFinishButtonClick="vRegWizard_FinishButtonClick">
                    <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Right" />
                    <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="'Segoe UI', Tahoma, Geneva, Verdana, sans-serif" Font-Size="9pt" ForeColor="#284E98" />
                    <SideBarButtonStyle BackColor="#8095A8" CssClass="sideBarBtn_cls" Font-Names="Verdana" ForeColor="#3D4C5A" />
                    <SideBarStyle BackColor="#8095A8" CssClass="sideBar_cls" Font-Size="0.9em" HorizontalAlign="Right" VerticalAlign="Top" Width="150px" />
                    <StepStyle Font-Size="0.8em" ForeColor="#333333" VerticalAlign="Top" CssClass="wizStep_cls" />
                    <WizardSteps>
                        <asp:WizardStep ID="AccountID" runat="server" Title="Create Account">
                            <asp:Table runat="server" CssClass="venue_obj_cls" ID="natbl">
                                <asp:TableRow runat="server" ID="TableRow10">
                                    <asp:TableCell runat="server" ID="TableCell10">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Name of Venue:&nbsp;<asp:Label ID="Label8" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="VenueName"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="VenueName" runat="server" AutoPostBack="false" CssClass="regFormV_cls" OnTextChanged="Unique_UN_TextChanged"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="naTR1">
                                    <asp:TableCell runat="server" ID="naTC1">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Choose a primary username:&nbsp;<asp:Label ID="errun" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="UN"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="UN" runat="server" AutoPostBack="false" CssClass="regFormV_cls" OnTextChanged="Unique_UN_TextChanged"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="naTR3">
                                    <asp:TableCell runat="server" ID="naTCUser">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Primary User Email Address:&nbsp;<asp:Label ID="Label1" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="pEmail"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="pEmail" runat="server" AutoPostBack="false" CssClass="regFormV_cls"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow1">
                                    <asp:TableCell runat="server" ID="TableCell1">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Choose a primary password:&nbsp;<asp:Label ID="Label2" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="pwd"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="pwd" runat="server" TextMode="Password" AutoPostBack="false" CssClass="regFormV_cls"></asp:TextBox>
                                                    </td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow3">
                                    <asp:TableCell runat="server" ID="TableCell3">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Confirm your password:&nbsp;<asp:Label ID="Label3" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="confirm_pwd"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="confirm_pwd" runat="server" TextMode="Password" AutoPostBack="false" CssClass="regFormV_cls"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="sqRow">
                                    <asp:TableCell runat="server" ID="sqCell">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Ask a security question:<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="secQuest"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="secQuest" runat="server" CssClass="regFormV_cls"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow4">
                                    <asp:TableCell runat="server" ID="TableCell4">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Provide the answer:<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="secAnswer"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="secAnswer" runat="server" CssClass="regFormV_cls"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow2">
                                    <asp:TableCell runat="server" ID="TableCell2">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Provide a hint:<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="secHint"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="secHint" runat="server" CssClass="regFormV_cls"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="EmailConfirm" OnActivate="EmailConfirm_Activate" OnDeactivate="EmailConfirm_Deactivate" runat="server" Title="Email Confirmation">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:HiddenField ID="newVenueID" runat="server" />
                                                                    <asp:Label ID="test" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" Title="Description" OnDeactivate="Unnamed_Deactivate">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Summary Details: (256 character limit)</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="sDesc" runat="server" AutoPostBack="false" CssClass="regFormV_cls" TextMode="MultiLine" Height="100px" Width="650px"></asp:TextBox>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Full Description:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="fDesc" runat="server" AutoPostBack="false" CssClass="regFormV_cls" TextMode="MultiLine" Height="200px" Width="650px"></asp:TextBox>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Web site:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="website" runat="server" Width="640px"></asp:TextBox>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Genre:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="genre" runat="server" Width="640px"></asp:TextBox>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Open @:<br />
                                                                    (Seperate days with commas)</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="daysOpen" runat="server" Height="100px" TextMode="MultiLine" Width="650px"></asp:TextBox>
                                                                    <br />
                                                                    Time Open:&emsp;<uc1:timeControl ID="timeControl1" runat="server" />
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Crowd Capacity:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="cap" runat="server" Width="640px"></asp:TextBox>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Age limit:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="ageLimit" runat="server" Width="640px"></asp:TextBox>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="vformCell_cls">
                                                        <table class="vformtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Dress Code:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="DressCode" runat="server" Width="640px"></asp:TextBox>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="vInfo" runat="server" OnActivate="vLocation_Activate" OnDeactivate="vInfo_Deactivate" Title="Location">
                            <asp:Table runat="server" CssClass="venue_obj_cls" ID="Table1">
                                <asp:TableRow runat="server" ID="TableRow5">
                                    <asp:TableCell runat="server" ID="TableCell5">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Street Address 1:&nbsp;<asp:Label runat="server" Font-Names="&#39;Lucida Sans&#39;,&#39;Lucida Sans Regular&#39;,&#39;Lucida Grande&#39;,&#39;Lucida Sans Unicode&#39;,Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" ID="Label4" Visible="False"></asp:Label><asp:RequiredFieldValidator runat="server" ForeColor="Red" ControlToValidate="TextBox2" ErrorMessage="*" EnableClientScript="False" Font-Names="&#39;Lucida Sans&#39;,&#39;Lucida Sans Regular&#39;,&#39;Lucida Grande&#39;,&#39;Lucida Sans Unicode&#39;,Geneva,Verdana,sans-serif" ID="RequiredFieldValidator8"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="regFormV_cls" ID="TextBox2"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow6">
                                    <asp:TableCell runat="server" ID="TableCell6">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Street Address 2:&nbsp;<asp:Label runat="server" Font-Names="&#39;Lucida Sans&#39;,&#39;Lucida Sans Regular&#39;,&#39;Lucida Grande&#39;,&#39;Lucida Sans Unicode&#39;,Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" ID="Label5" Visible="False"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="regFormV_cls" ID="TextBox3"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow7">
                                    <asp:TableCell runat="server" ID="TableCell7">
                                        <div class="vformCell_cls">
                                            <table>
                                                <tr>
                                                    <td style="width: 200px;">Country:</td>
                                                    <td style="width: 200px;">Province/State:</td>
                                                    <td style="width: 245px;">City:</td>
                                                    <td class="imgCell_Cls" rowspan="2">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 200px;">
                                                        <asp:DropDownList runat="server" AutoPostBack="True" CssClass="regForm_cls" Width="200px" ID="country" OnSelectedIndexChanged="country_SelectedIndexChanged"></asp:DropDownList></td>
                                                    <td style="width: 200px;">
                                                        <asp:DropDownList runat="server" AutoPostBack="True" CssClass="regForm_cls" Width="200px" ID="subdivision" Visible="False" OnSelectedIndexChanged="subdivision_SelectedIndexChanged"></asp:DropDownList></td>
                                                    <td style="width: 245px;">
                                                        <asp:DropDownList runat="server" CssClass="regForm_cls" Width="200px" ID="City" Visible="False"></asp:DropDownList></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow8">
                                    <asp:TableCell runat="server" ID="TableCell8">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Postal/Zip Code:&nbsp;<asp:Label runat="server" Font-Names="&#39;Lucida Sans&#39;,&#39;Lucida Sans Regular&#39;,&#39;Lucida Grande&#39;,&#39;Lucida Sans Unicode&#39;,Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" ID="Label6" Visible="False"></asp:Label><asp:RequiredFieldValidator runat="server" ForeColor="Red" ControlToValidate="TextBox5" ErrorMessage="*" EnableClientScript="False" Font-Names="&#39;Lucida Sans&#39;,&#39;Lucida Sans Regular&#39;,&#39;Lucida Grande&#39;,&#39;Lucida Sans Unicode&#39;,Geneva,Verdana,sans-serif" ID="RequiredFieldValidator11"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="regFormV_cls" ID="TextBox5"></asp:TextBox></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRow9">
                                    <asp:TableCell runat="server" ID="TableCell9">
                                        <div class="vformCell_cls">
                                            <table class="vformtblCell_cls">
                                                <tr>
                                                    <td colspan="2">Contact Information:&nbsp;
                                        <asp:Label ID="Label7" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="None" ID="DataList1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Line:">
                                                                    <ItemTemplate></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="regForm_cls" ID="TextBox6"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" AutoPostBack="True" CssClass="regForm_cls" Width="150px" ID="ddlContact" OnSelectedIndexChanged="ddlContact_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select phone line" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Primary #" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 1:" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 2:" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 3:" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 4:" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 5:" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 6:" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 7:" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 8:" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 9:" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="Tel # 10:" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="Mobile:" Value="Mobile"></asp:ListItem>
                                                            <asp:ListItem Text="Fax #:" Value="Fax #:"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td class="imgCell_Cls">
                                                        <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="cs" runat="server" OnDeactivate="cs_Deactivate" OnActivate="cs_Activate" Title="Choose Services">
                            <div class="outershell_cls">
                                <span>Explain how our services will both increase the venue customer base and how it will make it easier for the management to manage their venue.</span>
                                <br />
                                <br />
                                <div class="vformCell_cls">
                                    Venue Marketing:<br />
                                    Describe the features within Venue Marketing.  What it is and how it will benefit the venue.
                        <asp:CheckBoxList ID="VM" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                                </div>
                                <br />
                                <div class="vformCell_cls">
                                    Event promotions:<br />
                                    Describe the features within Event Promotions.  Explain what it is and how it will bring new customers to the venue while maintaining the current customer base.
                        <asp:CheckBoxList ID="EP" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                                </div>
                                <br />
                                <div class="vformCell_cls">
                                    Customer Relations<br />
                                    Describe customer relations.  Explain what Customer relations is and how it will benefit the venue.
                        <asp:CheckBoxList ID="CR" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                                </div>
                                <br />
                                <div class="vformCell_cls">
                                    Recruiting/Staffing:<br />
                                    Describe Recruiting and staffing.  Explain the job board, other features available and scheduling.  Explain how this will benefit the venue and how it will make things easier.
                        <asp:CheckBoxList ID="RS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                                </div>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="proDesign" runat="server" OnDeactivate="proDesign_Deactivate" Title="Venue Identity">
                            <div class="outershell_cls">
                                <br />
                                This area allows you to customize the way you will appear to the users on our network.&nbsp;&nbsp; You wil be able to see the results on the next page step.<br />
                                As well, the design can be edited in your settings once you have logged on.<br />
                                &nbsp;&nbsp;<div class="vformCell_cls">
                                    <table class="vformtblCell_cls">
                                        <tr>
                                            <td>Background: (Can only be a solid color or an image not both).</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="bgUpload" runat="server" CssClass="AvUpload_cls" />&nbsp;&nbsp;
                                                                    <asp:TextBox ID="clrSelector" runat="server" CssClass="clrSelector_cls" Text="Select Color"></asp:TextBox>
                                                <img alt="" id="colorPlt" src="imgs/clrbtn.png" />
                                            </td>
                                            <td class="imgCell_Cls">
                                                <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:LinkButton ID="UploadBkgrndLink" runat="server" CssClass="AvUpload_cls" OnClick="UploadBkgrndLink_Click">Upload</asp:LinkButton>&nbsp;&nbsp;<!--<asp:HiddenField ID="imgBkgrdSrc" runat="server" />//--><br />
                                                <asp:Label ID="UpbgStatus" runat="server" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="#24323E">Transfer Status</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="outerBoundry">
                                                <div class="innerBoundry">
                                                    <div class="vPlaceHolder_cls" id="bgContainer" runat="server"></div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <div class="vformCell_cls">
                                    <table class="vformtblCell_cls">
                                        <tr>
                                            <td>Logo:</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="logoUpload" runat="server" CssClass="AvUpload_cls" />
                                            </td>
                                            <td class="imgCell_Cls">
                                                <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:LinkButton ID="UploadLogoLink" runat="server" CssClass="AvUpload_cls" OnClick="UploadLogoLink_Click">Upload</asp:LinkButton>&nbsp;&nbsp;<!--<asp:HiddenField ID="imgLogoSrc" runat="server" />//--><br />
                                                <asp:Label ID="uplgStatus" runat="server" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="#24323E">Transfer Status</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="outerBoundry">
                                                <div class="innerBoundry">
                                                    <div class="vPlaceHolder_cls" id="logoContainer" runat="server"></div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />

                                <div class="vformCell_cls">
                                    <table class="vformtblCell_cls">
                                        <tr>
                                            <td>Banner:</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1">
                                                <asp:FileUpload ID="bannerUpload" runat="server" CssClass="AvUpload_cls" />
                                            </td>
                                            <td class="imgCell_Cls" style="height: 30px">
                                                <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:LinkButton ID="UploadBannerLink" runat="server" CssClass="AvUpload_cls" OnClick="UploadBannerLink_Click">Upload</asp:LinkButton>&nbsp;&nbsp;<!--<asp:HiddenField ID="imgBannerSrc" runat="server" />//--><br />
                                                <asp:Label ID="bnrUpload" runat="server" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="#24323E">Transfer Status</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="outerBoundry">
                                                <div class="innerBoundry">
                                                    <div class="vPlaceHolder_cls" id="bnrContainer" runat="server"></div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="fini" runat="server" StepType="Finish" Title="Complete">
                            <div class="complete_vreg_cls">
                                <h1>Congratulations!</h1>
                                You have completed the registration of your venue with The Underground.  Once you have logged in you will be able to do the following:
                            <ul>
                                <li>Set events.</li>
                                <li>Post news releases.</li>
                                <li>Post images.</li>
                                <li>Communicate with your clientele</li>
                            </ul>
                            </div>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </td>
        </tr>
    </table>
</asp:Content>