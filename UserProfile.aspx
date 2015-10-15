<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" ValidateRequest="false" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>

<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucTextEditor/ucTextEditor.ascx" tagname="ucTextEditor" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucTextEditor/ucTextEditorFull.ascx" tagname="ucTextEditorFull" tagprefix="uc3" %>
<%@ Register src="~/UserControl/ucCaptcha/ucCaptcha.ascx" tagname="ucCaptcha" tagprefix="uc4" %>
<%@ Register src="~/UserControl/ucDateTime/ucDateTimeFlat.ascx" tagname="ucDateTimeFlat" tagprefix="uc5" %>
<%@ Register src="~/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc6" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc7:ucColorBox ID="ucColorBox1" runat="server" />

    <div id="dvHeader">
        <div style="float:left;">
            <img src="/Images/Icon/icProfile.png" alt="โรงพยาบาลกรุงเทพจันทบุรี"/>
        </div>
        <div style="float:left;">
            <div style="text-align:left;width:500px;">
                <div style="font-size:16pt;padding-top:5px;">
                    <asp:Label ID="lblHeader" runat="server" Text="User Profile" 
                        meta:resourcekey="lblHeaderResource1" />
                </div>
                <div style="">
                    <asp:Label ID="lblSubHeader" runat="server" Text="ข้อมูลส่วนตัวผู้ใช้งาน" 
                        meta:resourcekey="lblSubHeaderResource1" />
                    <span style="color:#dddddd;padding:0px 5px;">«</span>
                    <a href="/"><asp:Label ID="lblLinkBack" runat="server" Text="กลับหน้าหลัก" 
                        meta:resourcekey="lblLinkBackResource1" /></a>
                </div>
            </div>
        </div>
        <div style="float:right;padding:5px;">
        </div>
        <div style="clear:both;"></div>
        <hr />
    </div>
    <uc6:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server"/>
    <div class="GridView" style="margin-top:10px;margin-bottom:10px;">
        <div class="GridViewHeader">
            <h2>
                <asp:Label ID="lblFormHeader" runat="server" Text="ข้อมูลส่วนตัวผู้ใช้งาน" 
                    meta:resourcekey="lblFormHeaderResource1" />
            </h2>
        </div>
        <table cellpadding="0" cellspacing="0">
		    <tr class="GridViewSubHeader">
                <td style="width:150px;">
				    Name<span class="Arrow" />
			    </td>
			    <td>
				    Data<span class="Arrow" />
			    </td>
		    </tr>
            <!--Start Loop-->
            <tr class="GridViewItemNormal">
                <td>
				    <b><asp:Label ID="lblFUsername" runat="server" Text="ชื่อผู้ใช้งาน" 
                        meta:resourcekey="lblFUsernameResource1" /></b></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtUsername" runat="server" MaxLength="100" Width="150px" 
                        meta:resourcekey="txtUsernameResource1"></asp:TextBox>
			        <span class="Icon16 Star Normal"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtUsername" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="โปรดกรอก" SetFocusOnError="True" 
                        meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td>
				    <b><asp:Label ID="lblFPassword" runat="server" Text="รหัสผ่าน" 
                        meta:resourcekey="lblFPasswordResource1" /></b></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtPassword" runat="server" MaxLength="100" 
                        TextMode="Password" Width="150px" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                    <span class="Icon16 Star Normal"></span>
			        <asp:RequiredFieldValidator ID="vldPassword" runat="server" 
                        ControlToValidate="txtPassword" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="โปรดกรอก" SetFocusOnError="True" 
                        meta:resourcekey="vldPasswordResource1"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblPassword" runat="server" 
                        meta:resourcekey="lblPasswordResource1"></asp:Label>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td>
				    <b><asp:Label ID="lblFPasswordChange" runat="server" Text="รหัสผ่านใหม่" 
                        meta:resourcekey="lblFPasswordChangeResource1" /></b>
			    </td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtPasswordChange" runat="server" MaxLength="100" 
                        TextMode="Password" Width="150px" 
                        meta:resourcekey="txtPasswordChangeResource1"></asp:TextBox>
			        <span class="fontComment">
                        <asp:Label ID="lblPasswordComment" runat="server" 
                        Text=" * กรอกเฉพาะกรณีต้องการเปลี่ยนรหัสผ่าน" 
                        meta:resourcekey="lblPasswordCommentResource1" />
                    </span>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td>
				    <b><asp:Label ID="lblFPasswordChangeConfirm" runat="server" 
                        Text="รหัสผ่านใหม่ อีกครั้ง" 
                        meta:resourcekey="lblFPasswordChangeConfirmResource1" /></b>
			    </td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtPasswordChangeConfirm" runat="server" MaxLength="100" 
                        TextMode="Password" Width="150px" 
                        meta:resourcekey="txtPasswordChangeConfirmResource1"></asp:TextBox>
			            &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtPasswordChange" 
                        ControlToValidate="txtPasswordChangeConfirm" CssClass="vldDefault" 
                        Display="Dynamic" ErrorMessage="รหัสผ่านที่คุณกรอกไม่ตรงกัน" 
                        meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><b><asp:Label ID="lblFEmail" runat="server" Text="อีเมล์" 
                        meta:resourcekey="lblFEmailResource1" /></b></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtEMail" runat="server" MaxLength="200" Width="150px" 
                        meta:resourcekey="txtEMailResource1"></asp:TextBox>
                    <span class="Icon16 Star Normal"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="txtEMail" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="โปรดกรอก" SetFocusOnError="True" 
                        meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEMail" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="รูปแบบอีเมล์ไม่ถูกต้อง" SetFocusOnError="True" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td colspan="2"><asp:Label ID="lblFDetail" runat="server" Text="ข้อมูลทั่วไป" 
                        meta:resourcekey="lblFDetailResource1" /></td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFPhoto" runat="server" Text="รูปประจำตัว" 
                        meta:resourcekey="lblFPhotoResource1" /></td>
                <td style="text-align:left;padding-left:10px;">
                    <asp:Label ID="lblPhoto" runat="server" meta:resourcekey="lblPhotoResource1"></asp:Label>
                    <asp:FileUpload ID="fuPhoto" runat="server" 
                        meta:resourcekey="fuPhotoResource1" />
                    <span class="fontComment"> * width=<%=photoWidth %> px , height=<%=photoHeight %> px</span>
                    <asp:CustomValidator ID="cvalAttachment0" runat="server" 
                        ClientValidationFunction="UploadFileCheck" ControlToValidate="fuPhoto" 
                        CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" 
                        SetFocusOnError="True" meta:resourcekey="cvalAttachment0Resource1"></asp:CustomValidator>
                </td>
            </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFName" runat="server" Text="ชื่อ นามสกุล" 
                        meta:resourcekey="lblFNameResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:DropDownList ID="ddlPName" runat="server" 
                        meta:resourcekey="ddlPNameResource1">
                        <asp:ListItem Value="null" meta:resourcekey="ListItemResource1"> - </asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource2">นาย</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource3">นางสาว</asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItemResource4">นาง</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFName" runat="server" MaxLength="100" Width="150px" 
                        meta:resourcekey="txtFNameResource1"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtLName" runat="server" MaxLength="100" Width="150px" 
                        meta:resourcekey="txtLNameResource1"></asp:TextBox>
                    <span class="Icon16 Star Normal"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="txtFName" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="โปรดกรอกชื่อ" SetFocusOnError="True" 
                        meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtLName" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="โปรดกรอกนามสกุล" SetFocusOnError="True" 
                        meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td>
				    <asp:Label ID="lblFHN" runat="server" Text="รหัสประจำตัวผู้ป่วย" 
                        meta:resourcekey="lblFHNResource1" />
                </td>
			    <td style="text-align:left;padding-left:10px;">
                    <asp:TextBox ID="txtHN" runat="server" Width="100px" MaxLength="12" 
                        meta:resourcekey="txtHNResource1" />
                    <span class="fontComment"> * Example 15-13-123456</span>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFBirthdate" runat="server" Text="วันเกิด" 
                        meta:resourcekey="lblFBirthdateResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
                    <uc5:ucDateTimeFlat ID="ucDateTimeFlat1" runat="server" 
                        EnableTimePicker="False" />
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFGender" runat="server" Text="เพศ" 
                        meta:resourcekey="lblFGenderResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
                    <asp:RadioButtonList ID="rbGender" runat="server" CellPadding="0" 
                        CellSpacing="0" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                        meta:resourcekey="rbGenderResource1">
                        <asp:ListItem Selected="True" Value="null" meta:resourcekey="ListItemResource5">ไม่ระบุ</asp:ListItem>
                        <asp:ListItem Value="M" meta:resourcekey="ListItemResource6">ชาย</asp:ListItem>
                        <asp:ListItem Value="F" meta:resourcekey="ListItemResource7">หญิง</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFPhone" runat="server" Text="โทรศัพท์บ้าน" 
                        meta:resourcekey="lblFPhoneResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtPhone" runat="server" MaxLength="100" Width="150px" 
                        meta:resourcekey="txtPhoneResource1"></asp:TextBox>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFMobile" runat="server" Text="โทรศัพท์มือถือ" 
                        meta:resourcekey="lblFMobileResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtMobile" runat="server" MaxLength="100" Width="150px" 
                        meta:resourcekey="txtMobileResource1"></asp:TextBox>
                    <span class="Icon16 Star Normal"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtMobile" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="โปรดกรอก" SetFocusOnError="True" 
                        meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFAddress" runat="server" Text="ที่อยู่" 
                        meta:resourcekey="lblFAddressResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtAddress" runat="server" MaxLength="500" Width="100%" 
                        meta:resourcekey="txtAddressResource1"></asp:TextBox>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFDistrict" runat="server" Text="ตำบล" 
                        meta:resourcekey="lblFDistrictResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtAddressDistrict" runat="server" MaxLength="100" 
                        Width="150px" meta:resourcekey="txtAddressDistrictResource1"></asp:TextBox>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFPrefecture" runat="server" Text="อำเภอ" 
                        meta:resourcekey="lblFPrefectureResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtAddressPrefecture" runat="server" MaxLength="100" 
                        Width="150px" meta:resourcekey="txtAddressPrefectureResource1"></asp:TextBox>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFProvince" runat="server" Text="จังหวัด" 
                        meta:resourcekey="lblFProvinceResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtAddressProvince" runat="server" MaxLength="100" 
                        Width="150px" meta:resourcekey="txtAddressProvinceResource1"></asp:TextBox>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFPostal" runat="server" Text="รหัสไปรษณีย์" 
                        meta:resourcekey="lblFPostalResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtAddressPostal" runat="server" MaxLength="10" Width="100px" 
                        meta:resourcekey="txtAddressPostalResource1"></asp:TextBox>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFProfile" runat="server" Text="ข้อมูลเกี่ยวกับคุณ" 
                        meta:resourcekey="lblFProfileResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <uc2:ucTextEditor ID="ucProfile" runat="server" Row="10"/>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFSignature" runat="server" Text="ข้อความแทนตัว" 
                        meta:resourcekey="lblFSignatureResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <uc3:ucTextEditorFull ID="ucSignature" runat="server" Row="5"/>
                </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFCaptcha" runat="server" Text="คำตอบยืนยัน" 
                        meta:resourcekey="lblFCaptchaResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
                    <uc4:ucCaptcha ID="ucCaptcha1" runat="server"/>
                </td>
		    </tr>
            <asp:Panel ID="pnAdmin" runat="server" Visible="False" 
                meta:resourcekey="pnAdminResource1">
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFStatus" runat="server" Text="กลุ่มผู้ใช้งาน" 
                        meta:resourcekey="lblFStatusResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:DropDownList ID="ddlUserGroup" runat="server" 
                        meta:resourcekey="ddlUserGroupResource1">
                    </asp:DropDownList>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFSort" runat="server" Text="ลำดับ" 
                        meta:resourcekey="lblFSortResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:TextBox ID="txtSort" runat="server" MaxLength="3" Width="30px" 
                        CssClass="txtCenter" meta:resourcekey="txtSortResource1">0</asp:TextBox>
                    <span class="font_comment"> * ระบุตัวเลข</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtSort" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="กรุณากรอก" SetFocusOnError="True" ValidationGroup="vgSubmit" 
                        meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                        ControlToValidate="txtSort" CssClass="validDefault" Display="Dynamic" 
                        ErrorMessage="กรอกเฉพาะตัวเลข" MaximumValue="999" MinimumValue="0" 
                        SetFocusOnError="True" Type="Integer" 
                        meta:resourcekey="RangeValidator2Resource1"></asp:RangeValidator>
			    </td>
		    </tr>
            <tr class="GridViewItemNormal">
                <td><asp:Label ID="lblFActive" runat="server" Text="เปิดการใช้งาน" 
                        meta:resourcekey="lblFActiveResource1" /></td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:CheckBox ID="cbActive" runat="server" Checked="True" Text="เปิด" 
                        meta:resourcekey="cbActiveResource1" />
			    </td>
		    </tr>
            </asp:Panel>
            <tr class="GridViewItemNormal">
                <td>
				    &nbsp;
			    </td>
			    <td style="text-align:left;padding-left:10px;">
				    <asp:Button ID="btSubmit" runat="server" onclick="btSubmit_Click" 
                            CssClass="Button SubmitTH" meta:resourcekey="btSubmitResource1"/>
                        <asp:Button ID="btCancel" runat="server" 
                            CausesValidation="False" onclick="btCancel_Click"
                            CssClass="Button CancelTH" meta:resourcekey="btCancelResource1"/>
			        <asp:Label ID="lblSQL" runat="server" meta:resourcekey="lblSQLResource1"></asp:Label>
			    </td>
		    </tr>
            <!--End Loop-->
        </table>
    </div>
</asp:Content>