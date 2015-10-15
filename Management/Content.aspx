<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="Management_Content" %>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icContent.png" alt="Content Manage" title="Content Manage" />
    </div>
    <div style="text-align:left;">
        <h1>Content Manage</h1>
        ระบบจัดการข้อมูลเนื้อหาบนเว็บไซต์ | <a href="/Management/">กลับสู่หน้าหลัก</a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeRefreshOnCloseName="cbIFrame"/>
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
                    Content List
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
				            ภาษา
			            </th>
                        <th class="GridViewSubHeader" style="width:100px;">
				            วันเวลา
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
                            <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(DataBinder.Eval(Container.DataItem, "Active")).ToString()=="1"?true:false %>' ToolTip="เปิด/ปิด การแสดงผล"/>
			            </td>
                        <td class="GridViewItem">
                            <div style="text-align:left;padding:10px;">
                                <a href="/Management/ContentManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" class="cbIFrame">
                                    <h3><%#DataBinder.Eval(Container.DataItem,"Name") %></h3>
                                </a>
                                <div>
                                    <%#DataBinder.Eval(Container.DataItem,"Detail") %>
                                </div>
                            </div>
			            </td>
                        <td class="GridViewItem">
				            <img src="<%#DataBinder.Eval(Container.DataItem, "LanguageIcon")%>" 
                                alt="<%#DataBinder.Eval(Container.DataItem, "LanguageName")%>" 
                                title="<%#DataBinder.Eval(Container.DataItem, "LanguageName")%>"/>
			            </td>
                        <td class="GridViewItem">
				            <%#DateTime.Parse(DataBinder.Eval(Container.DataItem, "MWhen").ToString()).ToString("dd/MM/yyyy HH:mm") %>
			            </td>
                        <td class="GridViewItem">
                            <a href="/Management/ContentManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" title="ดูข้อมูล" class="cbIFrame">
                                <div class="Icon16 Edit"></div>
                            </a>
				            <%--<a onClick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" href="/Management/UserManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=delete" class="Icon16 Delete" title="ลบข้อมูล"></a>--%>
			            </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        <th class="GridViewSubHeader" style="width:30px;">
				                    
			            </th>
			            <th class="GridViewSubHeader">
				            ชื่อ / รายละเอียด
			            </th>
                        <th class="GridViewSubHeader" style="width:100px;">
				            ภาษา
			            </th>
                        <th class="GridViewSubHeader" style="width:100px;">
				            วันเวลา
			            </th>
                        <th class="GridViewSubHeader" style="width:30px;">
				                    
			            </th>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>