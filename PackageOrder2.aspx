<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PackageOrder2.aspx.cs" Inherits="PackageOrder1" ValidateRequest="false" %>
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
    <style type="text/css">
        .style2
        {
            width: 763px;
        }
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 155px;
        }
        .style5
        {
            width: 80%;
            border-style: solid;
            border-width: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div>
        <asp:Image ID="imgLineOrder" runat="server" ImageUrl="~/Images/Icon/LineOrder2-TH.png" />
    </div>
    <br />
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td align="right">
                <p>
                วันที่ :
                <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td align="right">
                <p>
                    Order No :
                    <asp:Label ID="lblOrderNo" runat="server" Font-Size="Medium" Font-Bold="True" 
                        ForeColor="Red"></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td class="style2" align="left">
                <p>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblSurname" runat="server"></asp:Label>
                </p>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" align="left">
                <p>
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    ต.
                    <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                    อ.
                    <asp:Label ID="lblPrefecture" runat="server"></asp:Label>
                    จ.
                    <asp:Label ID="lblProvince" runat="server"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblZipcode" runat="server"></asp:Label>
                </p>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" align="left">
                <p>
                    &nbsp;อีเมล์
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    &nbsp;
                </p>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="GridView">
        <div class="GridViewHeader">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Icon/CartPackage.png" />
            &nbsp;
            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="#3366CC" Text="รายการแพ็คเกจที่เลือก"
                Font-Size="Medium"></asp:Label>
        </div>
        <table cellpadding="0" cellspacing="0" class="tbGridView" style="font-weight: normal"
            width="100%">
            <tr class="GridViewItem">
                <td style="text-align: left">
                    <p>
                        <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Horizontal" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="PackageCode" HeaderText="Code">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PackageName" HeaderText="Package Name">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UnitPrice" HeaderText="Price">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qty" HeaderText="Qty">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UnitPrice" HeaderText="Total">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#3283B6" Font-Bold="True" ForeColor="White" Height="40px" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle Height="40px" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                    </p>
                </td>
            </tr>
            <tr class="GridViewItem">
                <td style="text-align: right">
                    <table cellpadding="0" cellspacing="0" style="font-weight: normal; width: 253px;"
                        border="0" align="right">
                        <tr>
                            <td style="text-align: left" class="style4">
                                <asp:Label ID="lblTotal" runat="server" Text="ราคารวม"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <p align="right">
                                    <asp:Label ID="txtTotal" runat="server" Text="0"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="style4">
                                <p>
                                    <asp:Label ID="lblVat" runat="server" Text="ภาษี"></asp:Label>
                                </p>
                            </td>
                            <td style="text-align: right">
                                <p align="right">
                                    <asp:Label ID="txtVat" runat="server" Text="7%"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="style4">
                                <p>
                                    <asp:Label ID="lblGrandTotal" runat="server" Text="รวมทั้งสิ้น"></asp:Label>
                                </p>
                            </td>
                            <td style="text-align: right">
                                <p align="right">
                                    <asp:Label ID="txtGrandTotal" runat="server" Text="0"></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="GridViewFooter" style="text-align: right;">
    </div>
    
    <br />
    <asp:Button ID="Button1" runat="server" Text="ยืนยันการสั่งซื้อ" 
        CssClass="ButtonRed" onclick="Button1_Click" />
    <uc3:ucgridviewtemplate id="ucGridViewTemplate1" runat="server" />
</asp:Content>

