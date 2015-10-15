<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PackageView.aspx.cs" Inherits="PackageView" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagName="ucLanguageDB" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucColorBox/ucColorBox.ascx" TagName="ucColorBox" TagPrefix="uc1" %>
<%@ Register src="~/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link href="~/UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />
<link href="/CSS/cssControl.css" rel="stylesheet" type="text/css" />
<link href="/CSS/cssTable.css" rel="stylesheet" type="text/css" />
<link href="~/UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .dvContent
        {
            /*border:1px solid #DDD;*/
            margin: 20px;
            padding: 10px;
            position: relative;
        }
        .dvContent:hover
        {
            border: 1px solid #DDD;
            background-color: #FFF;
        }
        .dvContent:hover .dvContentMenu
        {
            visibility: visible;
        }
        .dvContentMenu
        {
            border-left: 1px solid #DDD;
            border-bottom: 1px solid #DDD;
            background-color: #FAFAFA;
            position: absolute;
            top: 0;
            right: 0;
            padding: 5px;
            visibility: hidden;
        }
        .tbMenuPackagePromotion
        {
            width: 100%;
            background-color: #FAFAFA;
            border-color:White;
            border-width:inherit;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table>
        <tr>
            <td width="700px" align="left" valign="top">
                <!-- ***************************************************************** -->
                <!-- ******************************** Site Map ************************-->
                <!-- ***************************************************************** -->
                <div align="left">
                    <p>
                        <asp:Label runat="server" ID="lblSiteMap" meta:resourcekey="lblSiteMapResource1"></asp:Label></p>
                </div>
                <br />
                <!-- ***************************************************************** -->
                <!-- ******************************** Title ***************************-->
                <!-- ***************************************************************** -->
                <div align="left" style="font-weight: bold; font-size: large">
                    <p>
                        <asp:Label runat="server" ID="lblTitle" meta:resourcekey="lblTitleResource1"></asp:Label></p>
                </div>
                <br />
                <!-- ***************************************************************** -->
                <!-- ******************************** ปุ่ม Add **************************-->
                <!-- ***************************************************************** -->
                <div align="Right">
                    <asp:Panel ID="pnAdminButton" runat="server" Visible="false">
                        <a href="/PackageForm.aspx" class="cbIFrame"><span class="Button AddTH"></span></a>
                    </asp:Panel>
                    <%--<asp:Button ID="btAdmin" runat="server" CssClass="Button AddTH" Text="" Visible="False"
                        OnClick="btAdmin_Click" />--%>
                </div>
                <!-- ***************************************************************** -->
                <!-- ****************************** Gridview Detail *******************-->
                <!-- ***************************************************************** -->
                <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                    BorderWidth="0px" ShowFooter="True" CellPadding="0" CellSpacing="0" GridLines="None"
                    OnRowCommand="gvPackage_RowCommand" OnRowDataBound="gvPackage_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table cellpadding="5" border="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td rowspan="2">
                                        <img src='<%# DataBinder.Eval(Container.DataItem,"PicThumbnail") %>' alt="" />
                                    </td>
                                    <td rowspan="2" width="10">
                                    </td>
                                    <td align="left" valign="top" style="font-weight: bold; font-size: larger">
                                        <a href="Package/<%# DataBinder.Eval(Container.DataItem,"UID") %>/<%#DataBinder.Eval(Container.DataItem,"PackageName") %>">
                                            <p>
                                                <%#DataBinder.Eval(Container.DataItem, "PackageName")%></p>
                                        </a>
                                    </td>
                                    <td rowspan="2">
                                        <asp:Label ID="UID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="PackageCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PackageCode") %>' Visible="false"></asp:Label>
                                        <a href="PackageForm.aspx?UID=<%# DataBinder.Eval(Container.DataItem,"UID") %>" class="cbIFrame" style='display:<%#(Security.LoginGroup == "Admin"?"inline":"none")%>;'>
                                            <span class="Icon16 Edit"></span>
                                        </a>
                                        <%--<asp:Button ID="btEdit" runat="server" CssClass="Icon16 Edit" Text="" CommandName="EditPackage"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                            BorderStyle="None" />--%>
                                        <asp:Button ID="btDelete" runat="server" CssClass="Icon16 Delete" Text="" CommandName="DeletePackage"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                            BorderStyle="None" OnClientClick="return confirm('Are you sure you want to delete this package?');" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" style="font-weight: normal">
                                        <%# DataBinder.Eval(Container.DataItem, "DetailSub")%>
                                        <br />
                                        <p style="color: Green;">
                                            Price : &nbsp;
                                            <%# DataBinder.Eval(Container.DataItem,string.Format("{0:#,#.#}", "UnitPrice"))%>&nbsp;&nbsp; Bath&nbsp;
                                            <asp:Button ID="btBuy" runat="server" CssClass="Button BuyTH" CommandName="BuyPackage" Text = "ซื้อแพ็คเกจนี้"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>'/>
                                        </p>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
            </td>
            <td width="300px" align="left" valign="top">
                <table bgcolor="white" cellpadding="2" class="tbMenuPackagePromotion" width="100%"
                    border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            <p style="font-size: medium; font-weight: bold">
                                <a href="PackageView.aspx">
                                    <img src="Images/Icon/icNews.png" /><asp:Label ID="lblMenuPackage" runat="server" Text=""></asp:Label>
                                </a>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p style="font-size: medium">
                                <a href="PromotionView.aspx">
                                    <img src="Images/Icon/icNews.png" /><asp:Label ID="lblMenuPromotion" runat="server" Text=""></asp:Label>
                                </a>
                            </p>
                        </td>
                    </tr>
                </table>
                
                <br />
                <div class="GridView">
                    <div class="GridViewHeader">
                    <p><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Icon/CartPackage.png" />
                    <asp:Label ID="lblTitleSelectedPackage" runat="server" Font-Size="Medium" Text="แพ็คเกจที่เลือก"></asp:Label></p>
                    </div>
                    <table bgcolor="#F2F2F2" cellpadding="2" width="100%" class="tbGridView" border="0" cellpadding="2" cellspacing="2">
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvSelectedPackage" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                    BorderWidth="0px" ShowFooter="True" CellPadding="0" CellSpacing="0" GridLines="None"
                                    Width="100%" OnRowCommand="gvSelectedPackage_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellpadding="5" border="0">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align:left">
                                                        <p style="font-size: small; font-weight: normal;">
                                                            <asp:Label ID="lblSelectedPackagecode" runat="server" Text="Code : "></asp:Label>
                                                            <%# DataBinder.Eval(Container.DataItem, "PackageCode")%></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:left">
                                                        <p style="font-size: small; font-weight: normal; color: Blue;">
                                                            <%# DataBinder.Eval(Container.DataItem, "PackageName")%></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:left">
                                                        <p style="font-size: small; font-weight: normal">
                                                            <asp:Label ID="lblSelectedUnitPrice" runat="server" Text="Price : "></asp:Label>
                                                            <%# DataBinder.Eval(Container.DataItem, string.Format("{0:#,#.#}", "UnitPrice"))%>
                                                            <asp:Label ID="lblSelectedCurency" runat="server" Text=" Bath"></asp:Label>&nbsp;
                                                            <asp:Button ID="btRemove" runat="server" CssClass="Icon16 Delete" Text="" CommandName="RemovePackage"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Index") %>' BorderWidth="0"
                                                                BorderStyle="None" />
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <asp:Label ID="lblTotal" runat="server" Text="ราคารวม"></asp:Label>
                            </td>
                            <td>
                                <p align="right">
                                    <asp:Label ID="txtTotal" runat="server" Text="0"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <p>
                                    <asp:Label ID="lblVat" runat="server" Text="ภาษี"></asp:Label>
                                </p>
                            </td>
                            <td>
                                <p align="right">
                                    <asp:Label ID="txtVat" runat="server" Text="7%"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <p>
                                    <asp:Label ID="lblGrandTotal" runat="server" Text="รวมทั้งสิ้น"></asp:Label>
                                </p>
                            </td>
                            <td>
                                <p align="right">
                                    <asp:Label ID="txtGrandTotal" runat="server" Text="0"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <p>
                                    <asp:Button ID="btPayment" runat="server" Text="ชำระเงิน" CssClass="ButtonRed" OnClick="btPayment_Click" />
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="GridViewFooter" style="text-align: right;">
                </div>
                <uc3:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server" />
            </td>
        </tr>
    </table>
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="ucPackageView"/>
</asp:Content>

