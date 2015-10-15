<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorScheduleViewer.aspx.cs" Inherits="DoctorScheduleViewer" %>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucDateTime/ucDateTimeFlat.ascx" tagname="ucDateTimeFlat" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucCaptcha/ucCaptchaEncrypt.ascx" tagname="ucCaptchaEncrypt" tagprefix="uc4" %>
<%@ Register src="~/UserControl/ucCalendarSpecialDays/ucCalendarSpecialDays.ascx" tagname="ucCalendarSpecialDays" tagprefix="uc3" %>
<%@ Register src="~/UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาและนัดหมายแพทย์</title>
    <link href="/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssCustom.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssControl.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .DoctorTable
        {
            display:table;width:100%;
        }
        .DoctorRow
        {
            display:table-row;
            border:1px solid #FFF;
        }
        .DoctorRow:hover
        {
            border:1px solid #EAEAEA;
            background-color:#FAFAFA;
        }
        .DoctorCellPhoto
        {
            padding:5px;
            display:table-cell;width:120px;text-align:left;vertical-align:top;
        }
        .DoctorCellDetail
        {
            padding:5px;
            display:table-cell;width:100%;text-align:left;vertical-align:top;
        }
        .DoctorCellCalendar
        {
            padding:5px;
            display:table-cell;width:200px;text-align:left;vertical-align:top;
        }
        .DoctorCellDetail .Separate
        {
            color:#DDD;padding:0 10px;
        }
        .DoctorCellDetail A:link {color:#236492; text-decoration: none;}
        .DoctorCellDetail A:visited {color:#236492; text-decoration: none;}
        .DoctorCellDetail A:active {color:#4DB5D3; text-decoration: none;}
        .DoctorCellDetail A:hover {color:#4DB5D3; text-decoration:none;}
        
        .DoctorPhoto img
        {
            width:100px;
            border:1px solid #DDD;
            cursor:pointer;
            padding:7px;
            box-shadow:0 0 3px #E5E5E5;
	        -moz-box-shadow:0 0 3px #E5E5E5;
	        -webkit-box-shadow:0 0 3px #E5E5E5;
	        filter:alpha(opacity=100);
            -moz-opacity:1;opacity:1;
        }
        .DoctorPhoto:hover img
        {
	        filter:alpha(opacity=80);
            -moz-opacity:.8;opacity:.8;
        }
        .DoctorSchedule table
        {
            font-size:8pt;
            width:100%;
            margin-top:5px;
        }
        .DoctorScheduleHeader td
        {
            width:14%;text-align:center;
            background-color:#EFEFEF;padding:3px;
            border:1px solid #DDD;
        }
        .DoctorScheduleHeader td:hover
        {
            background-color:#E5E5E5;
            cursor:pointer;
        }
        .DoctorScheduleHeader td .Day
        {
            padding-left:5px;
            font-size:7pt;
            color:#B4B4B4;
        }
        .DoctorScheduleItem td
        {
            text-align:center;
            background-color:#FFF;
            padding:3px;
            border:1px solid #DDD;
        }
        .DoctorScheduleItem td:hover
        {
            background-color:#FAFAFA;
            cursor:pointer;
        }
        .DoctorScheduleItem .DayActive
        {
            background-color:#80C8F5;
            color:#FFF;
        }
        .DoctorScheduleItem .DayActive:hover
        {
            background-color:#0072B8;
            color:#FFF;
        }
        .BookControl
        {
            border:1px solid #DDD;
            padding:3px;
            background-color:#FFF;
            font-family:Tahoma;
            font-size:10pt;
            color:#464646;
        }
        .BookTDLeft
        {
            width:120px;text-align:right;font-weight:bold;
        }
        .BookTDRight
        {
            padding-left:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:ucColorBox ID="ucColorBox1" runat="server" />
         <uc5:ucLoader ID="ucLoader" runat="server" OnClickName="ucLoaderClicker" />
        <div style="padding:10px;">
            <asp:Label ID="lblDefault" runat="server" />
            <div class="DoctorRow" id="DoctorDetail" runat="server">
                <div class="DoctorCellPhoto">
                    <div class="DoctorPhoto">
                        <asp:Label ID="lblDoctorPhoto" runat="server"/>
                    </div>
                </div>
                <div class="DoctorCellDetail">
                    <h4><asp:Label ID="lblName1" runat="server"/></h4>
                    <div>
                        <asp:Label ID="lblName2" runat="server"/>
                    </div>
                    <div style="padding-top:5px;">
                        <div>
                            <b><%=SpecialtyText %></b> : <asp:Label ID="lblSpecialty" runat="server" />
                            <span class="Separate">|</span>
                            <b><%=DepartmentText %></b> : <asp:Label ID="lblDepartment" runat="server"/>
                            <br />
                            <b><%=EducationText %></b> : <asp:Label ID="lblEducation" runat="server"/>
                        </div>
                        <div class="DoctorSchedule">
                            <table cellpadding="0" cellspacing="0" style="width:100%;">
                                <tr class="DoctorScheduleHeader">
                                    <td>
                                        <%=DayText[0] %><span class="Day" title="วันที่">(<%=Day[0] %>)</span>
                                    </td>
                                    <td>
                                        <%=DayText[1]%><span class="Day" title="วันที่">(<%=Day[1]%>)</span>
                                    </td>
                                    <td>
                                        <%=DayText[2]%><span class="Day" title="วันที่">(<%=Day[2]%>)</span>
                                    </td>
                                    <td>
                                        <%=DayText[3]%><span class="Day" title="วันที่">(<%=Day[3]%>)</span>
                                    </td>
                                    <td>
                                        <%=DayText[4]%><span class="Day" title="วันที่">(<%=Day[4]%>)</span>
                                    </td>
                                    <td>
                                        <%=DayText[5]%><span class="Day" title="วันที่">(<%=Day[5]%>)</span>
                                    </td>
                                    <td>
                                        <%=DayText[6]%><span class="Day" title="วันที่">(<%=Day[6]%>)</span>
                                    </td>
                                </tr>
                                <tr class="DoctorScheduleItem">
                                    <td <%=(DayOfWeek==0?"class='DayActive'":"") %>>
                                        <asp:Label ID="gvSchedule0" runat="server" />
                                    </td>
                                    <td <%=(DayOfWeek==1?"class='DayActive'":"") %>>
                                        <asp:Label ID="gvSchedule1" runat="server" />
                                    </td>
                                    <td <%=(DayOfWeek==2?"class='DayActive'":"") %>>
                                        <asp:Label ID="gvSchedule2" runat="server" />
                                    </td>
                                    <td <%=(DayOfWeek==3?"class='DayActive'":"") %>>
                                        <asp:Label ID="gvSchedule3" runat="server" />
                                    </td>
                                    <td <%=(DayOfWeek==4?"class='DayActive'":"") %>>
                                        <asp:Label ID="gvSchedule4" runat="server" />
                                    </td>
                                    <td <%=(DayOfWeek==5?"class='DayActive'":"") %>>
                                        <asp:Label ID="gvSchedule5" runat="server" />
                                    </td>
                                    <td <%=(DayOfWeek==6?"class='DayActive'":"") %>>
                                        <asp:Label ID="gvSchedule6" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="DoctorCellCalendar" style="display:none;">
                    <uc3:ucCalendarSpecialDays ID="ucCalendarSpecialDays1" runat="server" Width="300px" Height="200px"/>
                </div>
            </div>
            <div id="Schedule" style="margin-top:20px;border:1px solid #DDD;">
                <div style="background-color:#EFEFEF;padding:5px;border-bottom:1px solid #DDD;">
                    <b><asp:Label ID="lblScheduleHeader" runat="server" Text="นัดหมายแพทย์" /></b>
                </div>
                <div style="padding:5px;">
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblHN" runat="server" Text="เลขที่ประจำตัวผู้ป่วย"/>
                            </td>
                            <td class="BookTDRight">
                                <asp:TextBox ID="txtHN" runat="server" MaxLength="12" Width="100px" 
                                    CssClass="BookControl"/>
                                <span class="fontComment"> * เช่น 15-14-000001</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblName" runat="server" Text="ชื่อ-นามสกุล"/>
                            </td>
                            <td class="BookTDRight">
                                <asp:DropDownList ID="ddlPName" runat="server" CssClass="BookControl">
                                    <asp:ListItem>นาย</asp:ListItem>
                                    <asp:ListItem>นาง</asp:ListItem>
                                    <asp:ListItem>นางสาว</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFName" runat="server" MaxLength="100" Width="120px" CssClass="BookControl"/> - 
                                <asp:TextBox ID="txtLName" runat="server" MaxLength="100" Width="120px" CssClass="BookControl"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtFName" CssClass="validDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอกชื่อ" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtLName" CssClass="validDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอกนามสกุล" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblBirthDate" runat="server" Text="วันเกิด"/>
                            </td>
                            <td class="BookTDRight">
                                <uc2:ucDateTimeFlat ID="ucBirthDate" runat="server" ValidateRequire="true" EnableTimePicker="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblNID" runat="server" Text="เลขที่บัตรประชาชน"/>
                            </td>
                            <td class="BookTDRight">
                                <asp:TextBox ID="txtNID" runat="server" MaxLength="20" Width="170px" CssClass="BookControl"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="txtNID" CssClass="validDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอก" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblEmail" runat="server" Text="อีเมล์"/>
                            </td>
                            <td class="BookTDRight">
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="170px" CssClass="BookControl"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtEmail" CssClass="validDefault" Display="Dynamic" ValidationGroup="vgDefault" 
                                    ErrorMessage="โปรดกรอกอีเมล์" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="txtEmail" CssClass="vldDefault" Display="Dynamic" 
                                    ErrorMessage="อีเมล์ผิดรูปแบบ" ValidationGroup="vgDefault" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblPhone" runat="server" Text="เบอร์โทรศัพท์"/>
                            </td>
                            <td class="BookTDRight">
                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="100" Width="170px" CssClass="BookControl"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="txtPhone" CssClass="validDefault" Display="Dynamic" 
                                    ErrorMessage="โปรดกรอกเบอร์โทรศัพท์" SetFocusOnError="True" ValidationGroup="vgDefault"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblBookDate" runat="server" Text="วันเวลานัด"/>
                            </td>
                            <td class="BookTDRight">
                                <uc2:ucDateTimeFlat ID="ucDateTimeFlat1" runat="server" ValidateRequire="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="BookTDLeft">
                                <asp:Label ID="lblComment" runat="server" Text="รายละเอียดเพิ่มเติม"/>
                            </td>
                            <td class="BookTDRight">
                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="5" MaxLength="500" 
                                    Width="98%" CssClass="BookControl"/>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="BookTDLeft">
                                <asp:Label ID="lblCaptcha" runat="server" Text="กรุณาตอบคำถาม"/>
                            </td>
                            <td class="BookTDRight">
                                <%--<uc4:ucCaptchaEncrypt ID="ucCaptchaEncrypt1" runat="server" />--%>
								<span style="color:#FF7F27;"> * โปรดกรอกผลการคำนวน</span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="background-color:#FAFAFA;padding:5px;text-align:center;border-top:1px solid #DDD;">
                    <asp:Button ID="btBook" runat="server" CssClass="Button SubmitEN" ValidationGroup="vgDefault" 
                        onclick="btBook_Click" OnClientClick="ucLoaderClicker('vgDefault');"/>
                    <asp:Label ID="lblBookAlert" runat="server" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
