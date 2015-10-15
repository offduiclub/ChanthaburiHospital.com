<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PackageOrder3.aspx.cs" Inherits="PackageOrder3" ValidateRequest="false" %>
<%@ Register src="UserControl/ucTextEditor/ucTextEditor.ascx" tagname="ucTextEditor" tagprefix="uc1" %>
<%@ Register src="UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc2" %>
<%@ Register src="UserControl/ucCaptcha/ucCaptcha.ascx" tagname="ucCaptcha" tagprefix="uc4" %>
<%@ Register src="UserControl/ucDateTime/ucDateTimeFlat.ascx" tagname="ucDateTimeFlat" tagprefix="uc5" %>
<%@ Register src="UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />
<link href="CSS/cssControl.css" rel="stylesheet" type="text/css" />
<link href="CSS/cssTable.css" rel="stylesheet" type="text/css" />
<link href="UserControl/ucGridView/Style/cssGridView_.css" rel="stylesheet" type="text/css" />
<link href="~/UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div>
        <asp:Image ID="imgLineOrder" runat="server" ImageUrl="~/Images/Icon/LineOrder3-TH.png" />
    </div>
    <br />
    <div align="center">
        <table class="tbGridView" style="width:500px">
            <tr class="trGridView">
                <td style="text-align:center; font-size:medium; font-weight:bold" bgcolor="#FFD7D7"><br />
                    <p style="color:Green">
                        ขอบคุณสำหรับความไว้วางใจที่ท่านมีให้กับโรงพบาลกรุงเทพจันทบุรี
                    </p><br />
                </td>
            </tr>

        </table>
    </div>
    <br />
    <br />
    <div align="center">
        <table class="tbGridView" style="width: 500px">
            <tr class="trGridView">
                <td bgcolor="#E6E6E6">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="ข้อมูลการชำระเงิน" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr class="trGridView">
                <td style="text-align: left">
                    <br />
                    <p>
                        1. ชำระเงินผ่านทางตู้ ATM, เค้าท์เตอร์ธนาคาร ที่ท่านสะดวก โดยโอนเข้าบัญชี<br />
                        <br />
                    </p>
                    <p style="color:Blue">
                        ชื่อบัญชี&nbsp; บริษัท โรงพยาบาลกรุงเทพจันทบุรี จำกัด<br />
                    </p>
                    <div align="center">
                        <table border="1" style="width: 80%">
                            <tr>
                                <td>
                                    <p>
                                        ธนาคาร</p>
                                </td>
                                <td>
                                    <p>
                                        สาขา</p>
                                </td>
                                <td>
                                    <p>
                                        ประเภทบัญชี</p>
                                </td>
                                <td>
                                    <p>
                                        เลขที่บัญชี</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <p>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Icon/BankSCB.png" />&nbsp;
                                        ไทยพาณิชย์</p>
                                </td>
                                <td>
                                    <p>
                                        จันทบุรี</p>
                                </td>
                                <td>
                                    <p>
                                        ออมทรัพย์</p>
                                </td>
                                <td>
                                    <p>
                                        000-0-00000-0</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <p>
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/BankK.png" />&nbsp;
                                        กสิกรไทย</p>
                                </td>
                                <td>
                                    <p>
                                        จันทบุรี</p>
                                </td>
                                <td>
                                    <p>
                                        ออมทรัพย์</p>
                                </td>
                                <td>
                                    <p>
                                        000-0-00000-0</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <p>
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Icon/BankBAY.png" />&nbsp;
                                        กรุงศรีอยุธยา</p>
                                </td>
                                <td>
                                    <p>
                                        จันทบุรี</p>
                                </td>
                                <td>
                                    <p>
                                        กระแสรายวัน</p>
                                </td>
                                <td>
                                    <p>
                                        000-0-00000-0</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <p>
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Icon/BankBK.png" />&nbsp;
                                        กรุงเทพ</p>
                                </td>
                                <td>
                                    <p>
                                        จันทบุรี</p>
                                </td>
                                <td>
                                    <p>
                                        กระแสรายวัน</p>
                                </td>
                                <td>
                                    <p>
                                        000-0-00000-0</p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <p>
                        2. แจ้งการชำระเงินผ่านทางหน้าเว็บไซต์ &nbsp; <a href="#">แจ้งชำระเงิน</a> &nbsp;
                        แล้วรอการติดต่อกลับจากทางเจ้าหน้าที่ของโรงพยาบาลภายใน 1 วันทำการ</p>
                    <br />
                    <br />
                    <p>
                        3. รับคูปองแพ็คเกจทางไปรษณีย์หรือสามารถติดต่อรับได้ที่โรงพยาบาล โดยแจ้งหมายเลข Order
                        No : </p> <p style="font-size:medium; font-weight:bold; color:Red;"><asp:Label ID="lblOrderNo" Text="" runat="server"></asp:Label></p>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

