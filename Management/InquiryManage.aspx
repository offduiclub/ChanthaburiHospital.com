<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InquiryManage.aspx.cs" Inherits="Management_InquiryManage" %>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc2" %>

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
                    <h2>แก้ไขข้อมูล</h2>
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
				            ศูนย์ที่เกี่ยวข้อง
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblMedicalCenter" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            ชื่อ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblName" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            โทรศัพท์</td>
                        <td style="text-align:left;padding-left:10px;">
                            <asp:Label ID="lblPhone" runat="server" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            อีเมล์</td>
                        <td style="text-align:left;padding-left:10px;">
                            <asp:Label ID="lblEmail" runat="server" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            ข้อความ</td>
                        <td style="text-align:left;padding-left:10px;">
                            <asp:Label ID="lblMessage" runat="server" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            สถานะ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:RadioButtonList ID="rbStatus" runat="server">
                                <asp:ListItem Value="RECEIVED">RECEIVE</asp:ListItem>
                                <asp:ListItem Value="REPLIED">REPLIED</asp:ListItem>
                            </asp:RadioButtonList>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            &nbsp;
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Button ID="btSubmit" runat="server" 
                                ValidationGroup="vgSubmit" onclick="btSubmit_Click" 
                                CssClass="Button SubmitTH"/>
                            <asp:Button ID="btCancel" runat="server" 
                                CssClass="Button CancelTH" CausesValidation="False" 
                                onclick="btCancel_Click" />
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
</body>
</html>