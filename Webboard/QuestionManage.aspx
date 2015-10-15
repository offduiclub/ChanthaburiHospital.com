<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionManage.aspx.cs" Inherits="Webboard_QuestionManage" ValidateRequest="false"%>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc2" %>
<%@ Register src="../UserControl/ucTextEditor/ucTextEditor.ascx" tagname="ucTextEditor" tagprefix="uc3" %>
<%@ Register src="../UserControl/ucCaptcha/ucCaptchaEncrypt.ascx" tagname="ucCaptchaEncrypt" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage</title>
    <link href="/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssControl.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssCustom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="ucManage"/>
        <uc2:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server"/>

        <asp:Label ID="lblDefault" runat="server" />
        <asp:Panel ID="pnDetail" runat="server">
            <div class="GridView" style="padding:10px;">
                <div class="GridViewHeader">
                    <h2><asp:Label ID="lblHeader" runat="server" Text="ตั้งคำถาม" /></h2>
                </div>
                <table cellpadding="0" cellspacing="0">
                    <tr class="GridViewSubHeader">
                        <td>
                            Detail<span class="Arrow" />
                        </td>
                    </tr>
                    <!--Start Loop-->
                    <tr class="GridViewItemNormal">
			            <td style="text-align:left;padding:10px;">
                            <h5>
                                <asp:Label ID="lblHeaderName" runat="server" Text="หัวข้อคำถาม" />
                            </h5>
				            <asp:TextBox ID="txtName" runat="server" Width="98%" MaxLength="500" CssClass="txtDefault"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtName" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                            <h5 style="margin-top:5px;">
                                <asp:Label ID="lblHeaderDetail" runat="server" Text="รายละเอียดคำถาม" />
                            </h5>
                            <uc3:ucTextEditor ID="ucDetail" runat="server" />

                            <h5 style="margin-top:5px;"><asp:Label ID="lblHeaderPhoto" runat="server" Text="ภาพประกอบ" /></h5>
				            <asp:Label ID="lblPhoto" runat="server"></asp:Label>
                            <asp:FileUpload ID="fuPhoto" runat="server" />
                            <span class="fontComment"> * Width=<%=photoWidth.ToString() %>px Height=<%=photoHeight.ToString()%>px</span>
                            <asp:RequiredFieldValidator 
                                ID="vdPhoto" runat="server" 
                                ControlToValidate="fuPhoto" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalAttachment0" runat="server" 
                                ClientValidationFunction="UploadFileCheck" ControlToValidate="fuPhoto" 
                                CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" SetFocusOnError="true" 
                                ValidationGroup="vgSubmit"></asp:CustomValidator>

                            <div id="dvAnonymous" runat="server" visible="true">
                                <div style="float:left;margin-right:20px;">
                                    <h5 style="margin-top:5px;">
                                        <asp:Label ID="lblHeaderCName" runat="server" Text="ผู้ตั้งคำถาม" />
                                    </h5>
                                    <asp:TextBox ID="txtCName" runat="server" MaxLength="100" width="100px" CssClass="txtDefault"/>
                                    <asp:RequiredFieldValidator ID="vadCName" runat="server" 
                                        ControlToValidate="txtCName" CssClass="validDefault" Display="Dynamic" 
                                        ErrorMessage="กรุณากรอก" SetFocusOnError="True" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                </div>
                                <div style="float:left;">
                                    <h5 style="margin-top:5px;">
                                        <asp:Label ID="lblHeaderCEmail" runat="server" Text="อีเมล์" />
                                    </h5>
                                    <asp:TextBox ID="txtCEmail" runat="server" MaxLength="100" width="150px" CssClass="txtDefault"/>
                                    <asp:RequiredFieldValidator ID="vadCEmail" runat="server" 
                                        ControlToValidate="txtCEmail" CssClass="validDefault" Display="Dynamic" 
                                        ErrorMessage="กรุณากรอก" SetFocusOnError="True" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ErrorMessage="รูปแบบอีเมล์ไม่ถูกต้อง" ControlToValidate="txtCEmail" 
                                        CssClass="validDefault" Display="Dynamic" ValidationGroup="vgSubmit" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                                <div style="clear:left;"></div>
                            </div>
                            <div style="text-align:left;">
                                <uc4:ucCaptchaEncrypt ID="ucCaptchaEncrypt1" runat="server" ErrorMessage="โปรดกรอก" ValidateGroup="vgSubmit"/>
                                <asp:Label ID="lblCaptcha" runat="server" />
                            </div>
                            <div id="dvAdmin" runat="server" visible="false">
                                <hr />
                                <div>
                                    <b>Webboard Group</b><br />
                                    <asp:DropDownList ID="ddlWebboardGroup" runat="server" CssClass="txtDefault"/>
                                </div>
                                <div>
                                    <b>Meta Keywords</b><br />
                                    <asp:TextBox ID="txtMetaKeywords" runat="server" MaxLength="500" width="98%" CssClass="txtDefault"/>
                                </div>
                                <div>
                                    <b>Meta Description</b><br />
                                    <asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="500" width="98%" CssClass="txtDefault"/>
                                </div>
                                <div>
                                    <b>Status</b><br />
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtDefault">
                                        <asp:ListItem Value="N">Normal</asp:ListItem>
                                        <asp:ListItem Value="I">Important</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <b>ลำดับ</b> <asp:TextBox ID="txtSort" runat="server" MaxLength="3" Width="30px" CssClass="txtDefault txtCenter">0</asp:TextBox>
                                    <span class="font_comment">* ระบุตัวเลข</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtSort" CssClass="validDefault" Display="Dynamic" 
                                        ErrorMessage="กรุณากรอก" SetFocusOnError="True" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                        ControlToValidate="txtSort" CssClass="validDefault" Display="Dynamic" 
                                        ErrorMessage="กรอกเฉพาะตัวเลข" MaximumValue="999" MinimumValue="0" 
                                        SetFocusOnError="True" Type="Integer" ValidationGroup="vgSubmit"></asp:RangeValidator>
                                </div>
                                <div>
                                    <b>แสดงผล</b> <asp:CheckBox ID="cbActive" runat="server" Checked="True" Text="เปิด" Enable="false"/>
                                </div>
                            </div>
			            </td>
		            </tr>
                    <!--End Loop-->
                </table>
                <div class="GridViewFooter">
                    <asp:Button ID="btSubmit" runat="server" 
                        ValidationGroup="vgSubmit" onclick="btSubmit_Click" 
                        CssClass="Button SubmitEN"/>
                    <asp:Button ID="btCancel" runat="server" 
                        CssClass="Button CancelEN" CausesValidation="False" 
                        onclick="btCancel_Click" />
			        <asp:Label ID="lblSQL" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </form>

    <!-- FileUpload Checker -->
    <script type="text/javascript">
        function UploadFileCheck(source, arguments) {
            var sFile = arguments.Value;
            arguments.IsValid =
                ((sFile.match(/\.jpe?g$/i)) ||
                (sFile.match(/\.jpg$/i)) ||
                (sFile.match(/\.gif$/i)) ||
                (sFile.match(/\.png$/i)));
        } 
    </script>
</body>
</html>