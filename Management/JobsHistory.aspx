<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="JobsHistory.aspx.cs" Inherits="Management_JobsHistory" %>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>
<%@ Register src="../UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .txtCenter
        {
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="/Images/Icon/icMedicalCenter.png" alt="MedicalCenter Manage" title="MedicalCenter Manage" />
    </div>
    <div style="text-align:left;float:left;">
        <h1><asp:Label ID="lblHeader" runat="server"/></h1>
        ประวัติการส่งใบสมัครงาน | <a href="/Management/Default.aspx">กลับสู่หน้าหลัก</a>
    </div>
    <div style="float:right;margin-top:17px;margin-right:9px;display:none;">
        <a href="/Management/<%=webManage %>" class="cbIFrame">
            <div class="Button AddTH"></div>
        </a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeName="cbIFrame" Height="90%"/>
    <uc3:ucLoader ID="ucLoader1" runat="server" />
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
                    ประวัติการส่งใบสมัครงาน
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
                        <th class="GridViewSubHeader" style="width:100px;">
				            ภาพ
			            </th>
                        <th class="GridViewSubHeader" style="width:120px;">
				            ตำแหน่ง
			            </th>
			            <th class="GridViewSubHeader">
				            ชื่อ / รายละเอียด
			            </th>
                        <th class="GridViewSubHeader" style="width:100px;">
				            วันเวลา
			            </th>
                        <th class="GridViewSubHeader" style="width:60px;">
				            ลำดับ
			            </th>
                        <th class="GridViewSubHeader" style="width:30px;">
				                    
			            </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <td class="GridViewItem">
                            <!--### CurrentPageChecker:START ###-->
                            <div style="display:none;">
                                <asp:CheckBox ID="cbDGCurrentPage" runat="server" Checked="true"/>
                            </div>
                            <!--### CurrentPageChecker:END ###-->
				            <asp:Label ID="lblDGID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UID") %>' Visible="false"/>
                            <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(DataBinder.Eval(Container.DataItem, "StatusFlag")).ToString()=="A"?true:false %>' ToolTip="เปิด/ปิด การแสดงผล"/>
			            </td>
                        <td class="GridViewItem">
                            <a href="/Management/<%=webManage %>?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" title="แก้ไขข้อมูล" class="cbIFrame">
                                <img src='/Upload/Jobs/<%#DataBinder.Eval(Container.DataItem,"Photo") %>' style="width:100px;"/>
                            </a>
                        </td>
                        <td class="GridViewItem">
                            <a href="/Management/<%=webManage %>?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" title="แก้ไขข้อมูล" class="cbIFrame">
                                <%#DataBinder.Eval(Container.DataItem,"JobsName") %>
                            </a>
                            <%#(DataBinder.Eval(Container.DataItem, "JobsNameManual").ToString()!=""?" ("+DataBinder.Eval(Container.DataItem,"JobsNameManual").ToString()+")":"")%>
                        </td>
                        <td class="GridViewItem">
                            <div style="text-align:left;padding:10px;">
                                <h4><a href="/Management/<%=webManage %>?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" title="แก้ไขข้อมูล" class="cbIFrame">
                                    <%#DataBinder.Eval(Container.DataItem,"PrenameTH") %> 
                                    <%#DataBinder.Eval(Container.DataItem,"ForenameTH") %>  
                                    <%#DataBinder.Eval(Container.DataItem,"SurnameTH") %>
                                </a></h4>
                            </div>
			            </td>
                        <td class="GridViewItem">
				            <%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MWhen").ToString()).ToString("dd/MM/yyyy HH:mm") %>
			            </td>
                        <td class="GridViewItem">
                            <asp:TextBox ID="txtDGSort" runat="server" Width="45px" CssClass="txtCenter" Text='<%#DataBinder.Eval(Container.DataItem, "Sort")%>'></asp:TextBox>
                        </td>
                        <td class="GridViewItem">
                            <a href="/Management/<%=webManage %>?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" title="ดูข้อมูล" class="cbIFrame">
                                <div class="Icon24 Search"></div>
                            </a>
                            <a onClick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" href="/Management/JobsHistory.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=delete" class="Icon16 Delete" title="ลบข้อมูล"></a>
			            </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        <th class="GridViewSubHeader">
				                    
			            </th>
                        <th class="GridViewSubHeader">
				            ภาพ
			            </th>
                        <th class="GridViewSubHeader">
				            ตำแหน่ง
			            </th>
			            <th class="GridViewSubHeader">
				            ชื่อ / รายละเอียด
			            </th>
                        <th class="GridViewSubHeader">
				            วันเวลา
			            </th>
                        <th class="GridViewSubHeader">
				            ลำดับ
			            </th>
                        <th class="GridViewSubHeader">
				                    
			            </th>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>