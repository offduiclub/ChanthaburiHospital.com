<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Management_User"%>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucGridView/ucGridVIewDataTables.ascx" tagname="ucGridVIewDataTables" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icUser.png" alt="User Manage" title="User Manage" />
    </div>
    <div style="text-align:left;">
        <h1>User Manage</h1>
        ระบบจัดการข้อมูลสมาชิก | <a href="/Management/">กลับสู่หน้าหลัก</a>
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child"/>
    <uc2:ucGridVIewDataTables ID="ucGridVIewDataTables1" runat="server" GridViewID="gvDefault"/>

    <asp:Label ID="lblSQL" runat="server"></asp:Label>
    <asp:Label ID="lblDG" runat="server" />
    <div class="GridView">
        <asp:Panel ID="pnDGHeader" runat="server">
            <div class="GridViewHeader">
                <div style="float:right;">
                    <asp:Button ID="btDGSubmit" runat="server" ValidationGroup="vgDGSubmit" OnClick="btDGSubmit_Click" CssClass="Button SaveTH" />
                </div>
                <h3 style="margin-left:90px;">
                    User List
                </h3> 
                <div style="clear:both;"></div>
            </div>
        </asp:Panel>
        <asp:GridView id="gvDefault" runat="server" AutoGenerateColumns="false" 
            ShowHeader="true" ShowFooter="true" CellPadding="0" Width="100%" 
            GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                                    <th class="GridViewSubHeader" style="width:30px;">
				                    
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
                                        ภาพ
                                    </th>
			                        <th class="GridViewSubHeader">
				                        รายละเอียด
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
				                        กลุ่ม
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
                                        <div class="imgMouse">
                                            <a href="/Management/UserManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>" class="cbMaxHeight">
                                                <%#(DataBinder.Eval(Container.DataItem, "Photo")!=DBNull.Value?
                                                    "<img src='" + DataBinder.Eval(Container.DataItem, "Photo") + "' title='" + DataBinder.Eval(Container.DataItem, "Username") + "'/>" :
                                                    "")%>
                                            </a>
                                        </div>
                                    </td>
                                    <td class="GridViewItem">
                                        <div style="text-align:left;padding:10px;">
                                            <a href="/Management/UserManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>" class="cbMaxHeight">
                                                <h3><%#DataBinder.Eval(Container.DataItem,"Username") %></h3>
                                            </a>
                                            <%#DataBinder.Eval(Container.DataItem,"PName") %> <%#DataBinder.Eval(Container.DataItem,"FName") %> <%#DataBinder.Eval(Container.DataItem,"LName") %>
                                            <div>
                                                Phone : <%#DataBinder.Eval(Container.DataItem,"Phone") %>
                                            </div>
                                            <div>
                                                Mobile : <%#DataBinder.Eval(Container.DataItem,"Mobile") %>
                                            </div>
                                            <div>
                                                Email : <%#DataBinder.Eval(Container.DataItem,"Email") %>
                                            </div>
                                        </div>
			                        </td>
                                    <td class="GridViewItem">
				                        <asp:DropDownList ID="ddlDGUserGroup" runat="server" />
                                        <asp:Label ID="lblDGUserGroupUID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UserGroupUID") %>' Visible="false"/>
			                        </td>
                                    <td class="GridViewItem">
				                        <a onClick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')" href="/Management/User.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=delete" class="Icon16 Delete" title="ลบข้อมูล"></a>
			                        </td>
                    </ItemTemplate>
                    <FooterTemplate>
                                    <th class="GridViewSubHeader" style="width:30px;">
				                    
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
                                        ภาพ
                                    </th>
			                        <th class="GridViewSubHeader">
				                        รายละเอียด
			                        </th>
                                    <th class="GridViewSubHeader" style="width:100px;">
				                        กลุ่ม
			                        </th>
                                    <th class="GridViewSubHeader" style="width:30px;">
				                    
			                        </th>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>