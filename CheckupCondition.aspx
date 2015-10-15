<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckupCondition.aspx.cs" Inherits="CheckupResult" %>
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
        }
        .tbNoBorder
        {
        	border:0px none;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    

<br />
 <uc6:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server" />
    <div class="GridView">
        <div class="GridViewHeader">
            <h3>
                ระบุเงื่อนไขสำหรับการค้นหา</h3>
            <p>
                <asp:RadioButton ID="rdbHN" runat="server" GroupName="grpConditionSearch" Text="หมายเลขประจำตัวผู้ป่วย"
                    Checked="True" OnCheckedChanged="rdbHN_CheckedChanged" />
                <asp:TextBox ID="txtHN" runat="server"></asp:TextBox>&nbsp;<asp:RadioButton ID="rdbName"
                    runat="server" GroupName="grpConditionSearch" Text="ชื่อ-สกุล" OnCheckedChanged="rdbName_CheckedChanged" />
                <asp:TextBox ID="txtConName" runat="server"></asp:TextBox>
                -<asp:TextBox ID="txtConSurname" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="btFind" runat="server" OnClick="btFind_Click" Text="" CssClass="Button SearchTH" />
            </p>
        </div>
        <table>
            <tr class="GridViewItemNormal">
                <td>
                    <asp:GridView ID="gvPatient" runat="server" AutoGenerateColumns="False" CssClass="tbGridView"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" AllowPaging="True" OnPageIndexChanged="gvPatient_PageIndexChanged"
                        OnRowCommand="gvPatient_RowCommand" OnSelectedIndexChanged="gvPatient_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="rowguid" HeaderText="rowguid" Visible="false" />
                            <asp:BoundField DataField="HN" HeaderText="HN" />
                            <asp:BoundField DataField="Episode" HeaderText="Episode Number" />
                            <asp:BoundField DataField="Name" HeaderText="Forename" />
                            <asp:BoundField DataField="LastName" HeaderText="Surname" />
                            <asp:BoundField DataField="DOE" HeaderText="Checkup Date" />
                            <asp:CommandField ShowSelectButton="True" />
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
    </div>
<br />
<br />
</asp:Content>

