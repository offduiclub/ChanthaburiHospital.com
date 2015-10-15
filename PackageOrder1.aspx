<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PackageOrder1.aspx.cs" Inherits="PackageOrder1" ValidateRequest="false" %>
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
     <div align="left">
     <p>
          <asp:Label runat="server" ID="lblSiteMap"></asp:Label></p>
     </div>
    <div>
        <asp:Image ID="imgLineOrder" runat="server" ImageUrl="~/Images/Icon/LineOrder1-TH.png" />
    </div>
    <br />
    <table>
        <tr>
            <td width="700px" align="left" valign="top">
                <div class="GridView">
                    <div class="GridViewHeader">
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="ข้อมูลผู้สั่งซื้อ"
                            Font-Size="Medium"></asp:Label>
                    </div>
                    <table cellpadding="0" cellspacing="0" class="style1" width="600px" align="center"
                        bgcolor="white" class="tbGridView">
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblName" runat="server" Text="ชื่อ : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                                &nbsp;
                                <asp:Label ID="lblSurname" runat="server" Text="นามสกุล : "></asp:Label>
                                <asp:TextBox ID="txtSurname" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                &nbsp;<asp:RequiredFieldValidator ID="RFVName" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="กรุณากรอกชื่อด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFVSurname" runat="server" ControlToValidate="txtSurname"
                                    ErrorMessage="กรุณากรอกนามสกุลด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblSex" runat="server" Text="เพศ : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:RadioButton ID="rdbMale" runat="server" Checked="True" GroupName="SEX" Text="ชาย" />
                                <asp:RadioButton ID="rdbFemale" runat="server" GroupName="SEX" Text="หญิง" />
                                <asp:Label ID="Label2" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblIDCard" runat="server" Text="เลขที่บัตรประชาชน : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtIDCardNo" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label3" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblAddress" runat="server" Text="ที่อยู่ : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtAddress" runat="server" Width="400px"></asp:TextBox>
                                <asp:Label ID="Label4" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVAddress" runat="server" ControlToValidate="txtAddress"
                                    ErrorMessage="กรุณากรอกที่อยู่ด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblDistrict" runat="server" Text="ตำบล : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtDistrict" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label5" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVDistrict" runat="server" ControlToValidate="txtDistrict"
                                    ErrorMessage="กรุณากรอกตำบลด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblPrefecture" runat="server" Text="อำเภอ : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtPrefecture" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label6" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVPrefecture" runat="server" ControlToValidate="txtPrefecture"
                                    ErrorMessage="กรุณากรอกอำเภอด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblProvince" runat="server" Text="จังหวัด : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtProvince" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label7" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVProvince" runat="server" ControlToValidate="txtProvince"
                                    ErrorMessage="กรุณากรอกจังหวัดด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblZipcode" runat="server" Text="รหัสไปรษณีย์ : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtZipcode" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label8" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVZipcode" runat="server" ControlToValidate="txtZipcode"
                                    ErrorMessage="กรุณากรอกรหัสไปรษณีย์ด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblEmail" runat="server" Text="อีเมล : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label9" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RVFEmail" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="กรุณากรอกอีเมล์ด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="กรุณาระบุอีเมล์ให้ถูกต้องด้วย" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblTel" runat="server" Text="โทรศัพท์มือถือ : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <asp:TextBox ID="txtTel" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label10" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVTel" runat="server" ControlToValidate="txtTel"
                                    ErrorMessage="กรุณากรอกเบอร์โทรศัพท์มือถือด้วย" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr height="30px" class="trGridView">
                            <td style="text-align: right; padding-right: 10; vertical-align: top;" width="150px">
                                <asp:Label ID="lblDetail" runat="server" Text="รายละเอียดเพิ่มเติม : "></asp:Label>
                            </td>
                            <td style="text-align: left; padding-left: 10; vertical-align: top;">
                                <uc1:ucTextEditor ID="txtDetail" runat="server" Width="400" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="GridViewFooter" style="text-align: center;">
                    <asp:Button ID="btNext" runat="server" Text="ขั้นตอนต่อไป" CssClass="ButtonRed" OnClick="btNext_Click" />
                </div>
            </td>
            <td width="300px" align="left" valign="top">
                <table bgcolor="white" cellpadding="2" class="tbMenuPackagePromotion" width="100%"
                    border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            <p style="font-size: medium; font-weight: bold">
                                <a href="PackageView.aspx">
                                    <img src="Images/Icon/icNews.png" /><asp:Label ID="lblMenuPackage" runat="server"
                                        Text=""></asp:Label>
                                </a>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p style="font-size: medium">
                                <a href="PromotionView.aspx">
                                    <img src="Images/Icon/icNews.png" /><asp:Label ID="lblMenuPromotion" runat="server"
                                        Text=""></asp:Label>
                                </a>
                            </p>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="GridView">
                    <div class="GridViewHeader">
                        <p>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Icon/CartPackage.png" />
                            <asp:Label ID="lblTitleSelectedPackage" runat="server" Font-Size="Medium" Text="แพ็คเกจที่เลือก"></asp:Label></p>
                    </div>
                    <table bgcolor="#F2F2F2" cellpadding="2" width="100%" class="tbGridView" border="0"
                        cellpadding="2" cellspacing="2">
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvSelectedPackage" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                    BorderWidth="0px" ShowFooter="True" CellPadding="0" CellSpacing="0" GridLines="None"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellpadding="5" border="0">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align:left">
                                                        <p style="font-size: small; font-weight: normal;">
                                                            <asp:Label ID="lblSelectedPackagecode" runat="server" Text="Code : "></asp:Label>
                                                            <%# DataBinder.Eval(Container.DataItem, "PackageCode")%></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:left">
                                                        <p style="font-size: small; font-weight: normal; color: Blue;">
                                                            <%# DataBinder.Eval(Container.DataItem, "PackageName")%></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:left">
                                                        <p style="font-size: small; font-weight: normal">
                                                            <asp:Label ID="lblSelectedUnitPrice" runat="server" Text="Price : "></asp:Label>
                                                            <%# DataBinder.Eval(Container.DataItem, string.Format("{0:#,#.#}", "UnitPrice"))%>
                                                            <asp:Label ID="lblSelectedCurency" runat="server" Text=" Bath"></asp:Label>
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td  style="text-align:right">
                                <asp:Label ID="lblTotal" runat="server" Text="ราคารวม"></asp:Label>
                            </td>
                            <td>
                                <p align="right">
                                    <asp:Label ID="txtTotal" runat="server" Text="0"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <p>
                                    <asp:Label ID="lblVat" runat="server" Text="ภาษี"></asp:Label>
                                </p>
                            </td>
                            <td>
                                <p align="right">
                                    <asp:Label ID="txtVat" runat="server" Text="7%"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <p>
                                    <asp:Label ID="lblGrandTotal" runat="server" Text="รวมทั้งสิ้น"></asp:Label>
                                </p>
                            </td>
                            <td>
                                <p align="right">
                                    <asp:Label ID="txtGrandTotal" runat="server" Text="0"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <p>
                                    &nbsp;</p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="GridViewFooter" style="text-align: right;">
                </div>
                <uc3:ucGridViewTemplate ID="ucGridViewTemplate2" runat="server" />
            </td>
        </tr>
    </table>
    <uc3:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server" />
</asp:Content>

