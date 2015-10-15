<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckupResult.aspx.cs" Inherits="CheckupCondition" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagName="ucLanguageDB" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucColorBox/ucColorBox.ascx" TagName="ucColorBox" TagPrefix="uc1" %>
<%@ Register src="~/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc6" %>
<%@ Register src="UserControl/ucTabs/ucTabs.ascx" tagname="ucTabs" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="~/UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />
<link href="/CSS/cssControl.css" rel="stylesheet" type="text/css" />
<link href="~/UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
            border-style: solid;
            border-width: 1px;
        }
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<br />
<uc6:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server" />
    <div class="GridView">
        <div class="GridViewHeader">
            <h3>
                ผลการตรวจสุขภาพ
            </h3>

        </div>
        <table>
            <tr class="GridViewItemNormal">
                <td>
                    
                    <table cellpadding="0" cellspacing="0" class="style1" border="0">
                        <tr>
                            <td>
                                <div style="text-align:left">
                                    
                                    <table cellpadding="0" cellspacing="0" class="tbNoBorder">
                                        <tr>
                                            <td style="text-align: left; padding-left: 10; width:34%;border-right:0 none;border-bottom:0 none;border-left:0 none;">
                                                <p><b>เลขประจำตัว (HN) :</b>
                                                <asp:Label ID="lblHN" runat="server" Text="Name"></asp:Label></p>
                                            </td>
                                            <td style="text-align: left; padding-left: 10; width:33%;border-right:0 none;border-bottom:0 none;border-left:0 none;">
                                                <p><b>Order No. :</b>
                                                <asp:Label ID="lblNo" runat="server" Text="Label"></asp:Label></p>
                                            </td>
                                            <td style="text-align: left; padding-left: 10; width:33%;border-right:0 none;border-bottom:0 none;border-left:0 none;">
                                                <p><b>วันที่ตรวจ (Test Date) :</b>
                                                <asp:Label ID="lblDOE" runat="server" Text="Label"></asp:Label></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; padding-left: 10;border-right:0 none;border-bottom:0 none;">
                                                <p><b>ชื่อ-สกุล (Name) :</b>&nbsp;<asp:Label ID="lblFullName" runat="server" Text="Label"></asp:Label></p>
                                            </td>
                                            <td style="text-align: left; padding-left: 10;border-right:0 none;border-bottom:0 none;">
                                                <p><b>รหัสพนักงาน : </b>
                                                <asp:Label ID="lblEmpID" runat="server" Text="Label"></asp:Label></p>
                                            </td>
                                            <td style="text-align: left; padding-left: 10;border-right:0 none;border-bottom:0 none;">
                                               <p><b>เพศ (Sex) :</b>&nbsp;<asp:Label 
                                                    ID="lblSex" runat="server" Text="Label"></asp:Label>
                                                &nbsp; <b>อายุ (Age) :</b>
                                                <asp:Label ID="lblAge" runat="server" Text="Label"></asp:Label></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; padding-left: 10;border-right:0 none;border-bottom:0 none;">
                                                <p><b>ที่อยู่ (Address) : </b>
                                                <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label></p>
                                            </td>
                                            <td colspan="2" style="text-align: left; padding-left: 10;border-right:0 none;border-bottom:0 none;">
                                                <p><b>สายงาน :</b>&nbsp;<asp:Label 
                                                    ID="lblLine" runat="server" Text="Label"></asp:Label>
&nbsp; <b>ฝ่าย :</b>&nbsp;<asp:Label ID="lblDiv" runat="server" Text="Label"></asp:Label>
                                                &nbsp; <b>แผนก :</b>&nbsp;
                                                <asp:Label ID="lblDep" runat="server" Text="Label"></asp:Label></p>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanelPE" runat="server">
                                        <ContentTemplate>
                                            <uc3:ucTabs ID="ucTabs1" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br />
                                </div>
                            </td>
                        </tr>
                    

                        <tr>
                            <td style="background-color:#66ccff;">
                                <p>ผลการตรวจสมรรถภาพการมองเห็น (Vision Test)</p></td>
                        </tr>

                       
                        <tr>
                            <td>
                                
                                <table cellpadding="0" cellspacing="0" class="style2">
                                    <tr>
                                        <td style="text-align:left; width:800px">
                    <asp:GridView ID="gvLab" runat="server" AutoGenerateColumns="False" CssClass="tbGridView"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" AllowPaging="True" EnableSortingAndPagingCallbacks="True" 
                                                onpageindexchanged="gvLab_PageIndexChanged" 
                                                onpageindexchanging="gvLab_PageIndexChanging" PageSize="30">
                        <Columns>
                            <asp:BoundField DataField="CTTC_Desc" HeaderText="รายการที่ตรวจ" HeaderStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="LabRange" HeaderText="ค่าปกติ" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="VISTD_TestData" HeaderText="ผลตรวจ" HeaderStyle-HorizontalAlign="Center" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Height="30px" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#666666" Height="30px" Width="600px" />
                        <SelectedRowStyle BackColor="#CCEEFF" Font-Bold="True" ForeColor="#1E78A7" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left; width:800px">
                                            <p><b>สรุปผลตรวจสุขภาพ (Conclusion)</b></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left; width:800px">
                    <asp:GridView ID="gvLabDetail" runat="server" AutoGenerateColumns="False" CssClass="tbGridView"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" ShowHeader="False">
                        <Columns>
                            <asp:BoundField DataField="Detail" HeaderText="แปรผล" 
                                HeaderStyle-HorizontalAlign="Left">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Height="30px" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#666666" Height="30px" Width="600px" />
                        <SelectedRowStyle BackColor="#CCEEFF" Font-Bold="True" ForeColor="#1E78A7" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
    </div>
<br />
</asp:Content>

