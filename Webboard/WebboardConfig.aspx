<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebboardConfig.aspx.cs" Inherits="Webboard_WebboardConfig" %>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>
<%@ Register src="../UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssControl.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssCustom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeName="cbIFrame"/>
        <uc3:ucLoader ID="ucLoader1" runat="server" />
        <asp:Label ID="lblSQL" runat="server"></asp:Label>
        <asp:Label ID="lblDG" runat="server" />

        <uc2:ucGridVIewDataTables ID="ucGridVIewDataTables1" runat="server" GridViewID="gvDefault"/>
        <div class="GridView" style="margin:10px;">
            <asp:Panel ID="pnDGHeader" runat="server">
                <div class="GridViewHeader">
                    <div style="float:right;">
                        <asp:Button ID="btDGSubmit" runat="server" ValidationGroup="vgDGSubmit" OnClick="btDGSubmit_Click" CssClass="Button SaveTH" />
                    </div>
                    <h3 style="margin-left:90px;">
                        Webboard Config
                    </h3>
                    <div style="clear:both;"></div>
                </div>
            </asp:Panel>
            <asp:GridView id="gvDefault" runat="server" AutoGenerateColumns="false" 
                ShowHeader="true" ShowFooter="true" CellPadding="0" Width="100%" GridLines="None">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <th class="GridViewSubHeader" style="width:30px;">
				                    
			                </th>
			                <th class="GridViewSubHeader">
				                ชื่อ / รายละเอียด
			                </th>
                            <th class="GridViewSubHeader" style="width:100px;">
				                ตั้งค่า
			                </th>
                            <th class="GridViewSubHeader" style="width:100px;">
				                วันเวลา
			                </th>
                            <th class="GridViewSubHeader" style="width:60px;">
				                ลำดับ
			                </th>
                            <th class="GridViewSubHeader" style="width:30px;display:none;">
				                    
			                </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td class="GridViewItem">
				                <asp:Label ID="lblDGID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UID") %>' Visible="false"/>
                                <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(DataBinder.Eval(Container.DataItem, "Active")).ToString()=="1"?true:false %>' ToolTip="เปิด/ปิด การแสดงผล"/>
			                </td>
                            <td class="GridViewItem">
                                <div style="text-align:left;padding:10px;">
                                    <h3><%#DataBinder.Eval(Container.DataItem,"Name") %></h3>
                                    <div>
                                        <%#DataBinder.Eval(Container.DataItem,"Detail") %>
                                    </div>
                                </div>
			                </td>
                            <td class="GridViewItem">
				                <asp:TextBox ID="txtDGValue" CssClass="txtCenter" Text='<%#DataBinder.Eval(Container.DataItem, "Value").ToString()%>' runat="server" Width="90px" />
			                </td>
                            <td class="GridViewItem">
				                <%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MWhen").ToString()).ToString("dd/MM/yyyy HH:mm") %>
			                </td>
                            <td class="GridViewItem">
                                <asp:TextBox ID="txtDGSort" runat="server" Width="45px" CssClass="txtCenter" Text='<%#DataBinder.Eval(Container.DataItem, "Sort")%>'></asp:TextBox>
                            </td>
                            <td class="GridViewItem" style="display:none;">
                                <a href="/Management/<%=webManage %>?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" title="แก้ไขข้อมูล" class="cbIFrame">
                                    <div class="Icon16 Edit"></div>
                                </a>
			                </td>
                        </ItemTemplate>
                        <FooterTemplate>
                            <th class="GridViewSubHeader" style="width:30px;">
				                    
			                </th>
			                <th class="GridViewSubHeader">
				                ชื่อ / รายละเอียด
			                </th>
                            <th class="GridViewSubHeader">
				                ภาษา
			                </th>
                            <th class="GridViewSubHeader">
				                วันเวลา
			                </th>
                            <th class="GridViewSubHeader">
				                ลำดับ
			                </th>
                            <th class="GridViewSubHeader" style="display:none;">
				                    
			                </th>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
