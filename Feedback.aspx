<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="~/UserControl/ucCaptcha/ucCaptchaEncrypt.ascx" tagname="ucCaptchaEncrypt" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucContent/ucContent.ascx" tagname="ucContent" tagprefix="uc3" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagPrefix="uc1" TagName="ucLanguageDB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .columnLeft
        {
            text-align:right;
            padding:3px;
            font-weight:bold;
        }
        .columnRight
        {
            text-align:left;
            padding:3px;
        }
        .txtWidth200
        {
            width:300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ucColorBox ID="ucColorBox1" runat="server" UID="cbFeedback"/>
    <div id="dvHeader">
        <div style="float:left;margin-right:10px;">
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/Icon/icContact.png'/>" meta:resourcekey="lblIconResource1"/>
        </div>
        <div style="float:left;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>แนะนำ / ติชม ถึงผู้บริหาร</h1>" meta:resourcekey="lblNameResource1"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="คุณสามารถแนะนำหรือติชมมายังคณะผู้บริหารของโรงพยาบาลได้โดยตรง" meta:resourcekey="lblDetailResource1"/>
                </div>
                <div style="padding-top:5px;">
                    <div class="share42init"></div>
                    <script type="text/javascript" src="/Plugin/share42/share42.js"></script>
                    <!-- AddThis Button BEGIN 
                    <div class="addthis_toolbox addthis_default_style ">
                        <a class="addthis_button_preferred_1"></a>
                        <a class="addthis_button_preferred_2"></a>
                        <a class="addthis_button_preferred_3"></a>
                        <a class="addthis_button_preferred_4"></a>
                        <a class="addthis_button_compact"></a>
                        <a class="addthis_counter addthis_bubble_style"></a>
                    </div>
                    <script type="text/javascript">                                var addthis_config = { "data_track_addressbar": false };</script>
                    <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=offduiclub"></script>
                    <!-- AddThis Button END -->
                </div>
            </div>
        </div>
        <div style="float:right;padding:5px;"></div>
        <div style="clear:both;">
        </div>
    </div>
    <table cellpadding="0" cellspacing="0" style="width:100%;">
        <tr>
            <td style="padding:10px;">
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td style="width:200px;" class="columnLeft">
                            <asp:Label ID="lblFromName" runat="server" Text="จากคุณ" meta:resourcekey="lblFromNameResource1" />
                        </td>
                        <td class="columnRight">
                            <asp:TextBox ID="txtFromName" runat="server" CssClass="txtDefault" Width="150px" meta:resourcekey="txtFromNameResource1"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtFromName" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอก" SetFocusOnError="True" ValidationGroup="vgDefault" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="columnLeft">
                            <asp:Label ID="lblFromPhone" runat="server" Text="โทรศัพท์" meta:resourcekey="lblFromPhoneResource1" />
                        </td>
                        <td class="columnRight">
                            <asp:TextBox ID="txtFromPhone" runat="server" CssClass="txtDefault" Width="150px" meta:resourcekey="txtFromPhoneResource1"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtFromPhone" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอก" SetFocusOnError="True" ValidationGroup="vgDefault" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="columnLeft">
                            <asp:Label ID="lblFromEmail" runat="server" Text="อีเมล์" meta:resourcekey="lblFromEmailResource1" />
                        </td>
                        <td class="columnRight">
                            <asp:TextBox ID="txtFromEmail" runat="server" CssClass="txtDefault txtWidth200" meta:resourcekey="txtFromEmailResource1"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtFromEmail" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอก" SetFocusOnError="True" ValidationGroup="vgDefault" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtFromEmail" CssClass="vldDefault" Display="Dynamic" 
                                ErrorMessage="รูปแบบอีเมล์ไม่ถูกต้อง" SetFocusOnError="True" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ValidationGroup="vgDefault" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="columnLeft">
                            <asp:Label ID="lblMessage" runat="server" Text="ข้อความถึงผู้บริหาร" meta:resourcekey="lblMessageResource1"/>
                        </td>
                        <td class="columnRight">
                            <asp:TextBox ID="txtMessage" runat="server" CssClass="txtDefault txtWidth200" TextMode="MultiLine" Rows="5" meta:resourcekey="txtMessageResource1"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtMessage" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอก" SetFocusOnError="True" ValidationGroup="vgDefault" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="columnLeft">
                            
                        </td>
                        <td class="columnRight">
                            <%--<uc1:ucCaptchaEncrypt ID="ucCaptchaEncrypt1" runat="server" ValidateGroup="vgDefault"/>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="columnLeft">
                            
                        </td>
                        <td class="columnRight">
                            <asp:Button ID="btSubmit" runat="server" CssClass="Button SubmitEN" 
                                ValidationGroup="vgDefault" onclick="btSubmit_Click" meta:resourcekey="btSubmitResource1"/>
                            <asp:Label ID="lblAlert" runat="server" meta:resourcekey="lblAlertResource1" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align:left;vertical-align:top;">
                <uc3:ucContent ID="ucContent2" runat="server" ContentName="Feedback"/>
            </td>
        </tr>
    </table>
</asp:Content>