<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobsHistoryManage.aspx.cs" Inherits="Management_JobsHistoryManage" %>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucDateTime/ucDateTimeFlat.ascx" tagname="ucDateTimeFlat" tagprefix="uc3" %>

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
        <uc1:ucColorBox ID="ucColorBox1" runat="server" />
        <uc2:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server"/>

        <asp:Panel ID="pnDetail" runat="server">
            <div class="GridView" style="padding:10px;">
                <div class="GridViewHeader">
                    <h2>ดูข้อมูล</h2>
                </div>
                <table cellpadding="0" cellspacing="0">
                    <tr class="GridViewSubHeader">
                        <td style="width:150px;">
                            ชื่อ<span class="Arrow" />
                        </td>
                        <td>
                            ข้อมูล<span class="Arrow" />
                        </td>
                    </tr>
                    <!--Start Loop-->
                    <tr class="GridViewItemNormal">
                        <td>
                            ตำแหน่ง
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlJobs" runat="server" CssClass="txtDefault"/>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ภาพ
                            <div class="fontComment">
                                Photo <span style="color:#0072B8;">(Size < 512 kb)</span>
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblPhoto" runat="server" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            เงินเดือนขั้นต่ำที่ยอมรับได้
                            <div class="fontComment">
                                Minimum salary you may accept.
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtSalary" runat="server" CssClass="txtDefault" Width="200px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            สามารถเริ่มงานได้ตั้งแต่
                            <div class="fontComment">
                                Start date for job.
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtStart" runat="server" CssClass="txtDefault" Width="200px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ไปต่างจังหวัด
                            <div class="fontComment">
                                Can you go to provinces ?
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:RadioButtonList id="rbProvinceChange" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y" Selected="True">ได้ (Yes)</asp:ListItem>
                                <asp:ListItem Value="N">ไม่ได้ (No)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ชื่อ-นามสกุล (TH)
                            <div class="fontComment">
                                Name in Thai.
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlPrenameTH" runat="server" CssClass="txtDefault" Width="75px">
                                <asp:ListItem>นาย</asp:ListItem>
                                <asp:ListItem>นาง</asp:ListItem>
                                <asp:ListItem>นางสาว</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtForenameTH" runat="server" CssClass="txtDefault" Width="100px" />
                            <asp:TextBox ID="txtSurnameTH" runat="server" CssClass="txtDefault" Width="100px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtForenameTH" CssClass="vldDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอกชื่อ" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtSurnameTH" CssClass="vldDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอกนามสกุล" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            Name (EN)
                            <div class="fontComment">
                                Name in English.
                            </div>
                        </td>
                        <td style="text-align:left;">
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
                    <tr class="GridViewItemNormal">
                        <td>
                            สถานภาพสมรส
                            <div class="fontComment">
                                Marital Status.
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:RadioButtonList id="rbMarriageStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">โสด (Single)</asp:ListItem>
                                <asp:ListItem>แต่งงาน (Married)</asp:ListItem>
                                <asp:ListItem>หม้าย (Windowed)</asp:ListItem>
                                <asp:ListItem>หย่า (Divorced)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            เพศ
                            <div class="fontComment">
                                Gender
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:RadioButtonList id="rbGender" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="M" Selected="True">ชาย (Male)</asp:ListItem>
                                <asp:ListItem Value="F">หญิง (Female)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            วันเกิด
                            <div class="fontComment">
                                Birthdate
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <uc3:ucDateTimeFlat ID="ucBirthdate" runat="server" EnableTimePicker="false" 
                                ErrorMessage="โปรดกรอก" ValidateRequire="True" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            สถานที่เกิด
                            <div class="fontComment">
                                Birth Place
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtBirthplace" runat="server" CssClass="txtDefault" Width="200px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            สัญชาติ
                            <div class="fontComment">
                                Nationality
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtNationality" runat="server" CssClass="txtDefault" Width="200px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            เชื้อชาติ
                            <div class="fontComment">
                                Race
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtRace" runat="server" CssClass="txtDefault" Width="200px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ศาสนา
                            <div class="fontComment">
                                Religion
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtReligion" runat="server" CssClass="txtDefault" Width="200px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            น้ำหนัก
                            <div class="fontComment">
                                Weight
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtWeight" runat="server" CssClass="txtDefault" Width="100px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ส่วนสูง
                            <div class="fontComment">
                                Height
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtHeight" runat="server" CssClass="txtDefault" Width="100px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            หมายเลขบัตรประชาชน
                            <div class="fontComment">
                                Identification Card No.
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtNID" runat="server" CssClass="txtDefault" Width="100px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtNID" CssClass="vldDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอก" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ออกโดย
                            <div class="fontComment">
                                Issued at
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtNIDCreateBy" runat="server" CssClass="txtDefault" Width="200px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            วันหมดอายุ
                            <div class="fontComment">
                                Expiration date
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <uc3:ucDateTimeFlat ID="ucNIDExpire" runat="server" EnableTimePicker="false" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            โทรศัพท์
                            <div class="fontComment">
                                Phone
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="txtDefault" Width="100px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="txtPhone" CssClass="vldDefault" Display="Dynamic" 
                                ErrorMessage="โปรดกรอก" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            อีเมล์
                            <div class="fontComment">
                                Email
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtDefault" Width="100px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ที่อยู่ปัจจุบัน
                            <div class="fontComment">
                                Address
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="txtDefault" Width="350px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            กรณีฉุกเฉินติดต่อได้ที่
                            <div class="fontComment">
                                Name (Emergency Contact)
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmergencyName" runat="server" CssClass="txtDefault" Width="100px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            โทรศัพท์ (กรณีฉุกเฉิน)
                            <div class="fontComment">
                                Phone (Emergency Contact)
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmergencyPhone" runat="server" CssClass="txtDefault" Width="100px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            อีเมล์ (กรณีฉุกเฉิน)
                            <div class="fontComment">
                                Email (Emergency Contact)
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmergencyEmail" runat="server" CssClass="txtDefault" Width="100px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ที่อยู่ (กรณีฉุกเฉิน)
                            <div class="fontComment">
                                Address (Emergency Contact)
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmergencyAddress" runat="server" CssClass="txtDefault" Width="350px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ความสัมพันธ์ (กรณีฉุกเฉิน)
                            <div class="fontComment">
                                Relationships (Emergency Contact)
                            </div>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEmergencyRelationship" runat="server" CssClass="txtDefault" Width="100px" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            สมัครเมื่อ
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblCWhen" runat="server" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            &nbsp;
			            </td>
			            <td>
			                <asp:Label ID="lblSQL" runat="server"></asp:Label>
			            </td>
		            </tr>
                    <!--End Loop-->
                </table>
                <div class="GridViewFooter">
                    -
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