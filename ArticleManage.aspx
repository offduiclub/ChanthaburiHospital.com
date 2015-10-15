<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticleManage.aspx.cs" Inherits="ArticleManage" ValidateRequest="false" %>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucTextEditor/ucTextEditorFull.ascx" tagname="ucTextEditorFull" tagprefix="uc3" %>
<%@ Register src="~/UserControl/ucDateTime/ucDateTimeFlat.ascx" tagname="ucDateTimeFlat" tagprefix="uc4" %>

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
        <uc1:ucColorBox ID="ucColorBox1" runat="server" Width="80%"/>
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
				            ภาษา
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <div class="ClearStyle">
                                <asp:RadioButtonList ID="rbLanguage" runat="server" CellPadding="0" 
                                    CellSpacing="0"/>
                            </div>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            ภาพหัวข้อ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblPhoto" runat="server"></asp:Label>
                            <asp:FileUpload ID="fuPhoto" runat="server" />
                            <span class="fontComment"> * Width=<%=photoWidth.ToString() %>Height=<%=photoHeight.ToString()%></span><asp:RequiredFieldValidator 
                                ID="vdPhoto" runat="server" 
                                ControlToValidate="fuPhoto" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalAttachment0" runat="server" 
                                ClientValidationFunction="UploadFileCheck" ControlToValidate="fuPhoto" 
                                CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" SetFocusOnError="true" 
                                ValidationGroup="vgSubmit"></asp:CustomValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            ภาพประกอบ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:Label ID="lblPhotoFull" runat="server"></asp:Label>
                            <asp:FileUpload ID="fuPhotoFull" runat="server" />
                            <asp:RequiredFieldValidator 
                                ID="vdPhotoFull" runat="server" 
                                ControlToValidate="fuPhotoFull" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ClientValidationFunction="UploadFileCheck" ControlToValidate="fuPhotoFull" 
                                CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="เลือกได้เฉพาะไฟล์ดังนี้ jpg jpeg gif png" SetFocusOnError="true" 
                                ValidationGroup="vgSubmit"></asp:CustomValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            ชื่อ
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtName" runat="server" Width="400px" MaxLength="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtName" CssClass="validDefault" Display="Dynamic" 
                                ErrorMessage="กรุณากรอก" ValidationGroup="vgSubmit" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
                            คำอธิบาย</td>
                        <td style="text-align:left;padding-left:10px;">
                            <asp:TextBox ID="txtDetail" runat="server" width="99%" TextMode="MultiLine" Rows="3" />
                        </td>
                    </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Meta Keyword
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtMetaKeyword" runat="server" MaxLength="500" width="70%" />
                            <span class="fontComment"> * คั่นด้วยเครื่องหมาย ,</span>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            Meta Description
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="500" width="99%" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            เนื้อหา
			            </td>
			            <td style="text-align:left;padding-left:10px;">
			                <uc3:ucTextEditorFull ID="ucContent" runat="server" />
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            เริ่ม
			            </td>
			            <td style="text-align:left;padding-left:10px;">
			                <uc4:ucDateTimeFlat ID="ucDateStart" runat="server" EnableTimePicker="false"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            สิ้นสุด
			            </td>
			            <td style="text-align:left;padding-left:10px;">
				            <uc4:ucDateTimeFlat ID="ucDateEnd" runat="server" EnableTimePicker="false"/>
			            </td>
		            </tr>
                    <tr class="GridViewItemNormal">
                        <td>
				            แสดงผล
                        </td>
			            <td style="text-align:left;padding-left:10px;">
				            <asp:CheckBox ID="cbActive" runat="server" Checked="True" Text="เปิด" Enable="false"/>
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