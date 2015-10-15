<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebboardReservedWords.aspx.cs" Inherits="Webboard_WebboardReservedWords" %>

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

    <style type="text/css">
        .txtRed{color:#EC2A04;}
        .txtBlue{color:#21A2B9;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeName="cbIFrame"/>
        <uc3:ucLoader ID="ucLoader1" runat="server" />

        <div style="margin:10px;">
            <div>
                <div style="float:left;margin-right:5px;">
                    <img src="Images/icReservedWords.png" />
                </div>
                <div style="text-align:left;float:left;">
                    <h1>Reserved Words</h1>
                    ระบบจัดการคำสงวน
                </div>
                <div style="float:right;margin-top:17px;margin-right:9px;">
                    <a href="<%=webManage %>" class="cbIFrame">
                        <div class="Button AddTH"></div>
                    </a>
                </div>
                <div style="clear:both;"></div>
                <hr />
            </div>

            <asp:Label ID="lblSQL" runat="server"></asp:Label>
            <asp:Label ID="lblDG" runat="server" />

            <uc2:ucGridVIewDataTables ID="ucGridVIewDataTables1" runat="server" GridViewID="gvDefault"/>
            <div class="GridView">
                <asp:Panel ID="pnDGHeader" runat="server">
                    <div class="GridViewHeader">
                        <div style="float:right;">
                            <asp:Button ID="btDGSubmit" runat="server" ValidationGroup="vgDGSubmit" OnClick="btDGSubmit_Click" CssClass="Button SaveTH" />
                        </div>
                        <h3 style="margin-left:90px;">
                            Reserved Words
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
				                    คำสงวน
			                    </th>
                                <th class="GridViewSubHeader">
				                    แทนที่ด้วย
			                    </th>
                                <th class="GridViewSubHeader" style="width:150px;">
				                    วันเวลา
			                    </th>
                                <th class="GridViewSubHeader" style="width:60px;display:none;">
				                    ลำดับ
			                    </th>
                                <th class="GridViewSubHeader" style="width:60px;">
				                    
			                    </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td class="GridViewItem">
				                    <asp:Label ID="lblDGID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UID") %>' Visible="false"/>
                                    <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(DataBinder.Eval(Container.DataItem, "Active")).ToString()=="1"?true:false %>' ToolTip="เปิด/ปิด การแสดงผล"/>
			                    </td>
                                <td class="GridViewItem">
                                    <asp:TextBox ID="txtDGWord" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Word") %>' Width="95%" CssClass="txtRed" />
			                    </td>
                                <td class="GridViewItem">
				                    <asp:TextBox ID="txtDGWordReplace" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"WordReplace") %>' Width="95%" CssClass="txtBlue" />
			                    </td>
                                <td class="GridViewItem">
				                    <%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MWhen").ToString()).ToString("dd/MM/yyyy HH:mm") %>
			                    </td>
                                <td class="GridViewItem" style="display:none;">
                                    <asp:TextBox ID="txtDGSort" runat="server" Width="45px" CssClass="txtCenter" Text='<%#DataBinder.Eval(Container.DataItem, "Sort")%>'></asp:TextBox>
                                </td>
                                <td class="GridViewItem">
                                    <a href="<%=webManage %>?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" title="แก้ไขข้อมูล" class="cbIFrame">
                                        <div class="Icon16 Edit"></div>
                                    </a>
				                    <a onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" 
                                        href="<%=webManage %>?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=delete" 
                                        class="Icon16 Delete" title="ลบข้อมูล">
                                    </a>
			                    </td>
                            </ItemTemplate>
                            <FooterTemplate>
                                <th class="GridViewSubHeader" style="width:30px;">
				                    
			                    </th>
			                    <th class="GridViewSubHeader">
				                    คำสงวน
			                    </th>
                                <th class="GridViewSubHeader">
				                    แทนที่ด้วย
			                    </th>
                                <th class="GridViewSubHeader">
				                    วันเวลา
			                    </th>
                                <th class="GridViewSubHeader" style="display:none;">
				                    ลำดับ
			                    </th>
                                <th class="GridViewSubHeader">
				                    
			                    </th>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
