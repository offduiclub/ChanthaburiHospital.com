<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JobsApply.aspx.cs" Inherits="JobsApply" ValidateRequest="false" %>

<%@ Register src="~/UserControl/ucContent/ucContent.ascx" tagname="ucContent" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucDateTime/ucDateTimeFlat.ascx" tagname="ucDateTimeFlat" tagprefix="uc3" %>
<%@ Register src="~/UserControl/ucCaptcha/ucCaptchaEncrypt.ascx" tagname="ucCaptchaEncrypt" tagprefix="uc4" %>
<%@ Register src="~/UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc5" %>
<%@ Register src="~/UserControl/ucTextEditor/ucTextEditor.ascx" tagname="ucTextEditor" tagprefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .tdLeft
        {
            width:170px;text-align:right;padding:5px;
        }
        .tdRight
        {
            text-align:left;padding:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ucColorBox ID="ucColorBox1" runat="server" UID="Jobs"/>
    <uc5:ucLoader ID="ucLoader" runat="server" OnClickName="ucLoaderClicker" />
    
    <div id="dvHeader">
        <div style="float:left;margin-right:10px;">
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/Icon/icJobs.png'/>"/>
        </div>
        <div style="float:left;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>กรอกใบสมัคร</h1>"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="กรอกใบสมัครงานกับโรงพยาบาลกรุงเทพจันทบุรี"/>
                    <span> | </span><a href="/Jobs/">กลับหน้าหลัก</a>
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
                    <script type="text/javascript">                        var addthis_config = { "data_track_addressbar": false };</script>
                    <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=offduiclub"></script>
                    <!-- AddThis Button END -->
                </div>
            </div>
        </div>
        <div style="float:right;padding:5px;"></div>
        <div style="clear:both;">
        </div>
    </div>
    <div style="margin:10px 0 10px 0;">
        <asp:Label ID="lblMessage" runat="server" />
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td style="text-align:left;vertical-align:top;">
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td class="tdLeft">
                                ตำแหน่ง
                            </td>
                            <td class="tdRight">
                                <asp:DropDownList ID="ddlJobs" runat="server" CssClass="txtDefault" 
                                    onselectedindexchanged="ddlJobs_SelectedIndexChanged" AutoPostBack="true"/>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="ddlJobs" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault"
                                    ErrorMessage="โปรดเลือกตำแหน่ง" Operator="NotEqual" ValueToCompare="null"></asp:CompareValidator>
                                <asp:Panel ID="pnJobsName" runat="server" Visible="true">
                                    <asp:TextBox ID="txtJobsName" runat="server" CssClass="txtDefault" />
                                    <span class="fontComment"> * โปรดกรอกชื่อตำแหน่ง</span>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ภาพ
                                <div class="fontComment">
                                    Photo <span style="color:#0072B8;">(Size < 512 kb)</span>
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:FileUpload ID="fuPhoto" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="fuPhoto" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดเลือกภาพ" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="cvalAttachment0" runat="server" 
                                    ClientValidationFunction="UploadFileCheck" ControlToValidate="fuPhoto" 
                                    CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" SetFocusOnError="true"> </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="tdLeft">
                                เอกสารสมัครงาน
                                <div class="fontComment">
                                    Resume <span style="color:#0072B8;">(Size < 2 mb)</span>
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:FileUpload ID="fuResume" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                เงินเดือนขั้นต่ำที่ยอมรับได้
                                <div class="fontComment">
                                    Minimum salary you may accept.
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtSalary" runat="server" CssClass="txtDefault" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                สามารถเริ่มงานได้ตั้งแต่
                                <div class="fontComment">
                                    Start date for job.
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtStart" runat="server" CssClass="txtDefault" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ไปต่างจังหวัด
                                <div class="fontComment">
                                    Can you go to provinces ?
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:RadioButtonList id="rbProvinceChange" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Y" Selected="True">ได้ (Yes)</asp:ListItem>
                                    <asp:ListItem Value="N">ไม่ได้ (No)</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ชื่อ-นามสกุล (TH)
                                <div class="fontComment">
                                    Name in Thai.
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:DropDownList ID="ddlPrenameTH" runat="server" CssClass="txtDefault" Width="75px">
                                    <asp:ListItem>นาย</asp:ListItem>
                                    <asp:ListItem>นาง</asp:ListItem>
                                    <asp:ListItem>นางสาว</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtForenameTH" runat="server" CssClass="txtDefault" Width="100px" />
                                <asp:TextBox ID="txtSurnameTH" runat="server" CssClass="txtDefault" Width="100px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtForenameTH" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอกชื่อ" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtSurnameTH" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอกนามสกุล" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                Name (EN)
                                <div class="fontComment">
                                    Name in English.
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:DropDownList ID="ddlPrenameEN" runat="server" CssClass="txtDefault" Width="75px">
                                    <asp:ListItem>Mr.</asp:ListItem>
                                    <asp:ListItem>Ms.</asp:ListItem>
                                    <asp:ListItem>Mrs.</asp:ListItem>
                                    <asp:ListItem>Miss</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtForenameEN" runat="server" CssClass="txtDefault" Width="100px" />
                                <asp:TextBox ID="txtSurnameEN" runat="server" CssClass="txtDefault" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                สถานภาพสมรส
                                <div class="fontComment">
                                    Marital Status.
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:RadioButtonList id="rbMarriageStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">โสด (Single)</asp:ListItem>
                                    <asp:ListItem>แต่งงาน (Married)</asp:ListItem>
                                    <asp:ListItem>หม้าย (Windowed)</asp:ListItem>
                                    <asp:ListItem>หย่า (Divorced)</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                เพศ
                                <div class="fontComment">
                                    Gender
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:RadioButtonList id="rbGender" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="M" Selected="True">ชาย (Male)</asp:ListItem>
                                    <asp:ListItem Value="F">หญิง (Female)</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                วันเกิด
                                <div class="fontComment">
                                    Birthdate
                                </div>
                            </td>
                            <td class="tdRight">
                                <uc3:ucDateTimeFlat ID="ucBirthdate" runat="server" EnableTimePicker="false" 
                                    ErrorMessage="โปรดกรอก" ValidateRequire="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                สถานที่เกิด
                                <div class="fontComment">
                                    Birth Place
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtBirthplace" runat="server" CssClass="txtDefault" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                สัญชาติ
                                <div class="fontComment">
                                    Nationality
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtNationality" runat="server" CssClass="txtDefault" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                เชื้อชาติ
                                <div class="fontComment">
                                    Race
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtRace" runat="server" CssClass="txtDefault" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ศาสนา
                                <div class="fontComment">
                                    Religion
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtReligion" runat="server" CssClass="txtDefault" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                น้ำหนัก
                                <div class="fontComment">
                                    Weight
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtWeight" runat="server" CssClass="txtDefault" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ส่วนสูง
                                <div class="fontComment">
                                    Height
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtHeight" runat="server" CssClass="txtDefault" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                หมายเลขบัตรประชาชน
                                <div class="fontComment">
                                    Identification Card No.
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtNID" runat="server" CssClass="txtDefault" Width="100px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtNID" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอก" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ออกโดย
                                <div class="fontComment">
                                    Issued at
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtNIDCreateBy" runat="server" CssClass="txtDefault" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                วันหมดอายุ
                                <div class="fontComment">
                                    Expiration date
                                </div>
                            </td>
                            <td class="tdRight">
                                <uc3:ucDateTimeFlat ID="ucNIDExpire" runat="server" EnableTimePicker="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                โทรศัพท์
                                <div class="fontComment">
                                    Phone
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="txtDefault" Width="100px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="txtPhone" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอก" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                อีเมล์
                                <div class="fontComment">
                                    Email
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtDefault" Width="100px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="txtEmail" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอก" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="txtEmail" CssClass="vldDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="รูปแบบอีเมล์ไม่ถูกต้อง" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                    SetFocusOnError="True"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ที่อยู่ปัจจุบัน
                                <div class="fontComment">
                                    Address
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="txtDefault" Width="350px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                กรณีฉุกเฉินติดต่อได้ที่
                                <div class="fontComment">
                                    Name (Emergency Contact)
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtEmergencyName" runat="server" CssClass="txtDefault" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                โทรศัพท์ (กรณีฉุกเฉิน)
                                <div class="fontComment">
                                    Phone (Emergency Contact)
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtEmergencyPhone" runat="server" CssClass="txtDefault" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                อีเมล์ (กรณีฉุกเฉิน)
                                <div class="fontComment">
                                    Email (Emergency Contact)
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtEmergencyEmail" runat="server" CssClass="txtDefault" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ที่อยู่ (กรณีฉุกเฉิน)
                                <div class="fontComment">
                                    Address (Emergency Contact)
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtEmergencyAddress" runat="server" CssClass="txtDefault" Width="350px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ความสัมพันธ์ (กรณีฉุกเฉิน)
                                <div class="fontComment">
                                    Relationships (Emergency Contact)
                                </div>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtEmergencyRelationship" runat="server" CssClass="txtDefault" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ประวัติการศึกษา
                            </td>
                            <td>
                                <uc6:ucTextEditor ID="ucEducation" runat="server" Width="100px" Row="3"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                ประสบการณ์การทำงาน
                            </td>
                            <td>
                                <uc6:ucTextEditor ID="ucExperience" runat="server" Width="100px" Row="3"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                กรุณาตอบคำถาม
                                <div class="fontComment">
                                    Captcha
                                </div>
                            </td>
                            <td class="tdRight">
                                <uc4:ucCaptchaEncrypt ID="ucCaptchaEncrypt1" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                            </td>
                            <td class="tdRight">
                                <asp:Button ID="btSubmit" runat="server" CssClass="Button SaveEN" ValidationGroup="vgDefault" 
                                    onclick="btSubmit_Click" OnClientClick="ucLoaderClicker('vgDefault');"/>
                                <asp:Button ID="btCancel" runat="server" CssClass="Button CancelEN" 
                                    CausesValidation="False" onclick="btCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align:left;vertical-align:top;width:300px;">
                    <div class="RoundCorner" style="border:1px solid #DDD;background-color:#FAFAFA;text-align:left;vertical-align:top;">
                        <uc1:ucContent ID="ucContent2" runat="server" ContentName="Jobs"/>
                    </div>
                </td>
            </tr>
        </table>
    </div>
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