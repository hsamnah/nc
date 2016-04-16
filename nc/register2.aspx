<%@ Page Title="" Language="C#" MasterPageFile="~/root.Master" AutoEventWireup="true" CodeBehind="register2.aspx.cs" Inherits="nc.register2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="wizContainer_cls">
        <tr>
            <td class="wizHeader_cls">New User Registration</td>
        </tr>
        <tr>
            <td class="regContainer_cls">
                <asp:Wizard ID="regWizard" runat="server" BackColor="#EFF3FB" BorderWidth="0px" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" Width="100%" ActiveStepIndex="3" OnActiveStepChanged="ChangeStep_Change" OnFinishButtonClick="regWizard_FinishButtonClick">
                    <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Right" />
                    <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="'Segoe UI', Tahoma, Geneva, Verdana, sans-serif" Font-Size="9pt" ForeColor="#284E98" />
                    <SideBarButtonStyle BackColor="#8095A8" CssClass="sideBarBtn_cls" Font-Names="Verdana" ForeColor="#3D4C5A" />
                    <SideBarStyle BackColor="#8095A8" CssClass="sideBar_cls" Font-Size="0.9em" HorizontalAlign="Right" VerticalAlign="Top" Width="150px" />
                    <StepStyle Font-Size="0.8em" ForeColor="#333333" VerticalAlign="Top" CssClass="wizStep_cls" />
                    <WizardSteps>
                        <asp:WizardStep ID="reg" runat="server" Title="Register" StepType="Start">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Your date of birth:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select class="regFormd_cls" id="Day" runat="server">
                                                                        <option value="0">Day</option>
                                                                        <option value="1">01</option>
                                                                        <option value="2">02</option>
                                                                        <option value="3">03</option>
                                                                        <option value="4">04</option>
                                                                        <option value="5">05</option>
                                                                        <option value="6">06</option>
                                                                        <option value="7">07</option>
                                                                        <option value="8">08</option>
                                                                        <option value="9">09</option>
                                                                        <option value="10">10</option>
                                                                        <option value="11">11</option>
                                                                        <option value="12">12</option>
                                                                        <option value="13">13</option>
                                                                        <option value="14">14</option>
                                                                        <option value="15">15</option>
                                                                        <option value="16">16</option>
                                                                        <option value="17">17</option>
                                                                        <option value="18">18</option>
                                                                        <option value="19">19</option>
                                                                        <option value="20">20</option>
                                                                        <option value="21">21</option>
                                                                        <option value="22">22</option>
                                                                        <option value="23">23</option>
                                                                        <option value="24">24</option>
                                                                        <option value="25">25</option>
                                                                        <option value="26">26</option>
                                                                        <option value="27">27</option>
                                                                        <option value="28">28</option>
                                                                        <option value="29">29</option>
                                                                        <option value="30">30</option>
                                                                        <option value="31">31</option>
                                                                    </select>
                                                                    <select class="regFormd_cls" id="Month" runat="server">
                                                                        <option value="0">Month</option>
                                                                        <option value="1">01</option>
                                                                        <option value="2">02</option>
                                                                        <option value="3">03</option>
                                                                        <option value="4">04</option>
                                                                        <option value="5">05</option>
                                                                        <option value="6">06</option>
                                                                        <option value="7">07</option>
                                                                        <option value="8">08</option>
                                                                        <option value="9">09</option>
                                                                        <option value="10">10</option>
                                                                        <option value="11">11</option>
                                                                        <option value="12">12</option>
                                                                    </select>
                                                                    <select class="regFormd_cls" id="Year" runat="server">
                                                                        <option value="0">Year</option>
                                                                        <option>1996</option>
                                                                        <option>1995</option>
                                                                        <option>1994</option>
                                                                        <option>1993</option>
                                                                        <option>1992</option>
                                                                        <option>1991</option>
                                                                        <option>1990</option>
                                                                        <option>1989</option>
                                                                        <option>1988</option>
                                                                        <option>1987</option>
                                                                        <option>1986</option>
                                                                        <option>1985</option>
                                                                        <option>1984</option>
                                                                        <option>1983</option>
                                                                        <option>1982</option>
                                                                        <option>1981</option>
                                                                        <option>1980</option>
                                                                        <option>1979</option>
                                                                        <option>1978</option>
                                                                        <option>1977</option>
                                                                        <option>1976</option>
                                                                        <option>1975</option>
                                                                        <option>1974</option>
                                                                        <option>1973</option>
                                                                        <option>1972</option>
                                                                        <option>1971</option>
                                                                        <option>1970</option>
                                                                        <option>1969</option>
                                                                        <option>1968</option>
                                                                        <option>1967</option>
                                                                        <option>1966</option>
                                                                        <option>1965</option>
                                                                        <option>1964</option>
                                                                        <option>1963</option>
                                                                        <option>1962</option>
                                                                        <option>1961</option>
                                                                        <option>1960</option>
                                                                        <option>1959</option>
                                                                        <option>1958</option>
                                                                        <option>1957</option>
                                                                        <option>1956</option>
                                                                        <option>1955</option>
                                                                        <option>1954</option>
                                                                        <option>1953</option>
                                                                        <option>1952</option>
                                                                        <option>1951</option>
                                                                        <option>1950</option>
                                                                    </select>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Your are:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select class="regForme_cls" id="Sex" runat="server">
                                                                        <option>Male</option>
                                                                        <option>Female</option>
                                                                    </select>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Your are looking for a:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select class="regForme_cls" id="lookinfor" runat="server">
                                                                        <option>Male</option>
                                                                        <option>Female</option>
                                                                    </select>
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Your relationship status is:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select class="regForme_cls" id="status" runat="server">
                                                                        <option>Single</option>
                                                                        <option>Relationship</option>
                                                                        <option>Engaged</option>
                                                                        <option>Married</option>
                                                                        <option>Seperated</option>
                                                                        <option>Divorced</option>
                                                                        <option>Widowed</option>
                                                                        <option>Other</option>
                                                                    </select>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">You are interested in:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <select class="regForme_cls" id="rel" runat="server">
                                                                        <option>Friendship</option>
                                                                        <option>Relationship</option>
                                                                        <option>Companionship</option>
                                                                        <option>Other</option>
                                                                    </select>
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
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/bg-nightlife.png" /></td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="create" runat="server" StepType="Step" OnDeactivate="create_Deactivate" Title="Create Account">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Choose a username:&nbsp;<asp:Label ID="errun" runat="server" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="UN"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="UN" runat="server" AutoPostBack="true" CssClass="regForm_cls" OnTextChanged="isUnique_Click"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Your email address:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="Email"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="Email" runat="server" CssClass="regForm_cls" TextMode="Email"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Choose a password:&nbsp;<asp:Label ID="err" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ClientIDMode="Static"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="apwd"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="apwd" runat="server" CssClass="regFormpwd_cls" TextMode="Password"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Confirm your password:<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="bpwd"></asp:RequiredFieldValidator>
                                                                    <asp:Label ID="pcomp" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="bpwd" runat="server" CssClass="regFormpwd_cls" TextMode="Password"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2">Ask a security question:<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="secQuest"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="secQuest" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Answer for your security question:<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="secAns"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="secAns" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Hint:<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="secHint"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="secHint" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/354_2.jpg" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">By pressing next you agree to our privacy and usage policy [privacy] and [usage]:</td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="Confirmation" OnActivate="Confirmation_Activate" OnDeactivate="Send_Data" runat="server" Title="Email Confirmation">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td>
                                                                    <asp:HiddenField ID="newUserID" runat="server" />
                                                                    <asp:Label ID="test" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/nl.png" /></td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="Contact" OnDeactivate="Contact_Deactivate" runat="server" Title="Contact Information">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">FirstName:<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="FirstName"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="FirstName" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Last Name:<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" EnableClientScript="False" ErrorMessage="*" Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" ForeColor="Red" ControlToValidate="LastName"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="LastName" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">(Apt #) - Street Address 1:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="Add1" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">(Apt #) - Street Address 2 (Optional):</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="Add2" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Country</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList AutoPostBack="true" ID="country" Width="300px" runat="server" CssClass="regForm_cls" OnSelectedIndexChanged="getSub_Click"></asp:DropDownList>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Province/State:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList AutoPostBack="true" ID="subdivision" runat="server" Width="300px" CssClass="regForm_cls" OnSelectedIndexChanged="getCity_Click" Visible="false"></asp:DropDownList>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">City:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList CssClass="regForm_cls" Width="300px" ID="City" runat="server" Visible="false"></asp:DropDownList>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Postal/Zip Code:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="pz" runat="server" CssClass="regForm_cls"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Mobile #:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="mobile" runat="server" CssClass="regForm_cls" TextMode="Phone"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Home #:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="home" runat="server" CssClass="regForm_cls" TextMode="Phone"></asp:TextBox>
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
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/2007_08_21clubland.png" /></td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="about" runat="server" OnDeactivate="about_Deactivate" Title="About you">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Describe yourself:
                                                                    <br />
                                                                    <span class="smallTxt_cls">(In less then 256 characters.)</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="desc" runat="server" CssClass="regForm_cls" TextMode="MultiLine"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">List the venues you remember visiting:
                                                                    <br />
                                                                    <span class="smallTxt_cls">Seperate each venue with a comma.</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="venVisit" runat="server" CssClass="regForm_cls" TextMode="MultiLine"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Describe the best experience you've had out:
                                                                    <br />
                                                                    <span class="smallTxt_cls">In under 256 characters.</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="bExp" runat="server" CssClass="regForm_cls" TextMode="MultiLine"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Describe the worst experience you've had out:
                                                                    <br />
                                                                    <span class="smallTxt_cls">In under 256 characters.</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="wExp" runat="server" CssClass="regForm_cls" TextMode="MultiLine"></asp:TextBox>
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
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/bk.png" /></td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="Prefs" OnDeactivate="Prefs_Deactivate" runat="server" Title="Preferances">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">

                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Select your preferred type of Venue::</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBoxList ID="venueList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                                    </asp:CheckBoxList>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Select your preferred music genre::</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBoxList ID="musicGenre" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                                    </asp:CheckBoxList>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">List, as much as you can, the artists you prefer listening to:
                                                                    <br />
                                                                    <span class="smallTxt_cls">please seperate artists with a comma.</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="artists" runat="server" CssClass="regForm_cls" TextMode="MultiLine"></asp:TextBox>
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
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Select which information to make public to your friends and public::<br />
                                                                    <span class="smallTxt_cls">This can be changed in your settings at any time.</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBoxList ID="share" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"></asp:CheckBoxList>
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
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/mechucocktails_1.png" /></td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="Av" runat="server" OnDeactivate="Av_Deactivate" Title="Avatar">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <table>

                                            <tr>
                                                <td>
                                                    <div class="formCell_cls">
                                                        <table class="formtblCell_cls">
                                                            <tr>
                                                                <td colspan="2" class="formTitleCell_Cls">Your allowed one profile picture.  You'll be allowed to upload more in your app space.</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:FileUpload ID="avatarUpload" runat="server" CssClass="AvUpload_cls" />
                                                                </td>
                                                                <td class="imgCell_Cls">
                                                                    <img alt="" class="imgtxbx_cls" src="imgs/txbx.gif" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="AvUpload_cls" OnClick="upload_click">Upload</asp:LinkButton>&nbsp;&nbsp;<asp:HiddenField ID="imgSrc" runat="server" />
                                                                    <br />
                                                                    <asp:Label ID="UpStatus" runat="server" Font-Names="'Segoe UI',Tahoma,Geneva,Verdana,sans-serif" Font-Size="10pt" ForeColor="#24323E">Transfer Status</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="outerBoundry">
                                                                    <div class="innerBoundry">
                                                                        <div class="avPlaceHolder_cls" id="avatarContainer" runat="server"></div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/nc.png" /></td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                        <asp:WizardStep ID="Fini" runat="server" OnActivate="Fini_Activate" StepType="Complete" Title="Finish">
                            <table class="wizStepTbl_cls">
                                <tr>
                                    <td class="wizSteptd1_cls">
                                        <div id="uForm_comp">
                                            Thank you for registring with our application.&nbsp; From here you&#39;ll be able to do the following:<br />
                                            <br />
                                            The following is what members on your friend list will see about you. Please note, the information presented here is not what your portfolio will look like. You will be able to design your portfolio in your settings menu.<br />
                                            <br />
                                            <table>
                                                <tr>
                                                    <td id="formContainer">
                                                        <asp:DetailsView ID="userDetails" AutoGenerateRows="false" runat="server" Height="50px" Width="325px" GridLines="None">
                                                            <Fields>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>Name:</HeaderTemplate>
                                                                    <ItemTemplate><%# Eval("FirstName") %> &nbsp; <%# Eval("LastName") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>Username:</HeaderTemplate>
                                                                    <ItemTemplate><%# Eval("Username") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Birthdate:
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# Eval("BirthDate","{0:D}") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>Address:</HeaderTemplate>
                                                                    <ItemTemplate><%# Eval("StreetAddress1") %>,&nbsp;<%# Eval("City") %>, &nbsp; <%# Eval("prov") %>,&nbsp;<%# Eval("Postal") %>&nbsp;&nbsp; <%# Eval("Country") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>Mobile #:</HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# Eval("Mobile") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>Home #:</HeaderTemplate>
                                                                    <ItemTemplate><%# Eval("Home") %></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Fields>
                                                            <HeaderStyle Font-Names="'Lucida Sans','Lucida Sans Regular','Lucida Grande','Lucida Sans Unicode',Geneva,Verdana,sans-serif" HorizontalAlign="Left" VerticalAlign="Top" Font-Size="10pt" Font-Bold="True" />
                                                        </asp:DetailsView>
                                                    </td>
                                                    <td style="vertical-align: top; padding: 2px 2px 2px 2px;">
                                                        <asp:Image ID="avatarImg" runat="server" CssClass="avimg_cls" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="reg_obj_cls">
                                                <tr>
                                                    <td colspan="2" class="UserDesc_cls">
                                                        <asp:Repeater ID="Description" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="reg_obj_cls">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="UserDesc_cls">
                                                                        <div>
                                                                            <div class="heading_cls">Your description:</div>
                                                                            <div class="info_cls"><%# Eval("Desc") %></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="UserDesc_cls">
                                                                        <div>
                                                                            <div class="heading_cls">Best Experience out:</div>
                                                                            <div class="info_cls"><%# Eval("BestExperience") %></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="UserDesc_cls">
                                                                        <div>
                                                                            <div class="heading_cls">Worst Experience out:</div>
                                                                            <div class="info_cls"><%# Eval("WorstExperience") %></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="UserDesc_cls">
                                                        <asp:Repeater ID="vvRep" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="reg_obj_cls">
                                                                    <tr>
                                                                        <td class="reg_objH_cls">Venues you've visited:</td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="reg_obj_cls"><%# Eval("venue") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                    <td class="UserDesc_cls">
                                                        <asp:Repeater ID="vtRep" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="reg_obj_cls">
                                                                    <tr>
                                                                        <td class="reg_objH_cls">Preferred type of venue:</td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="reg_obj_cls"><%# Eval("venueType") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="UserDesc_cls">
                                                        <asp:Repeater ID="mgRep" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="reg_obj_cls">
                                                                    <tr>
                                                                        <td class="reg_objH_cls">Music Genre:</td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="reg_obj_cls"><%# Eval("musicGenre") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                    <td class="UserDesc_cls">
                                                        <asp:Repeater ID="artRep" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="reg_obj_cls">
                                                                    <tr>
                                                                        <td class="reg_objH_cls">Favorite Artists:</td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="reg_obj_cls"><%# Eval("Artist") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="UserDesc_cls">
                                                        <asp:DataList ID="shareRep" runat="server" RepeatColumns="2" Width="100%">
                                                            <HeaderTemplate>
                                                                <table class="reg_obj_cls">
                                                                    <tr>
                                                                        <td class="reg_objH_cls">Information willing to share:</td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="reg_obj_cls"><%# Eval("itemShare") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:DataList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <p>We hope you enjoy the use of this application and that it is useful in your entertainment planning.</p>
                                        </div>
                                    </td>
                                    <td class="wizSteptd2_cls">
                                        <img class="regImage_cls" src="imgs/bg-nightlife.png" /></td>
                                </tr>
                            </table>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>