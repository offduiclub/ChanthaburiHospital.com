<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PackageDetail.aspx.cs" Inherits="PackageDetail" %>
<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
<link href="~/UserControl/ucGridView/Style/cssGridView.css" rel="stylesheet" type="text/css" />
<link href="/CSS/cssControl.css" rel="stylesheet" type="text/css" />
<link href="/CSS/cssTable.css" rel="stylesheet" type="text/css" />
<link href="~/UserControl/ucGridView/Style/cssGridView_.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table>
        <tr>
        <td width="800px" align="left" valign="top">
                <div align="left">
        <p>
            <asp:Label ID="lblSiteMap" runat="server"></asp:Label></p>
    </div>
    <br />
    <div align="left" style="font-weight: bold; font-size: large">
        <p>
            <asp:Label runat="server" ID="lblTitle" meta:resourcekey="lblTitleResource1"></asp:Label></p>
    </div>
    <br />
    <table>
        <tr>
            <td align="left" style="font-weight: bold; font-size: large">
                <asp:Label ID="lblUID" runat="server" Visible="false"></asp:Label>
                <asp:Panel ID="pnAdmin" runat="server" Visible="false">
                    <a href="/PackageForm.aspx?UID=<%=lblUID.Text.Trim() %>" class="cbIFrame"><span class="Icon16 Edit"></span></a>
                    <%--<asp:Button ID="btEdit" runat="server" CssClass="Icon16 Edit" Text="" Visible="False"
                        BorderStyle="None" BorderWidth="0" onclick="btEdit_Click" />--%>
                    <asp:Button ID="btDelete" runat="server" CssClass="Icon16 Delete" Text="" BorderStyle="None"
                        BorderWidth="0" Visible="True" 
                        OnClientClick="return confirm('Are you sure you want to delete this promotion?');" 
                        onclick="btDelete_Click" />
                </asp:Panel>
                <p>
                    <asp:Label ID="lblSubject" runat="server" Text="" ForeColor="#3399ff"></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Image ID="PicFull" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <p style="font-size:large; font-weight:bold; color:Blue;>
                    <br />
                    <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblUnitPrice" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Button ID="btBuy" runat="server" Text="" CssClass="ButtonGreen" 
                    onclick="btBuy_Click" />
            </td>
        </tr>
    </table>
        </td>
        <td width="200px" align="left" valign="top">
                        <table bgcolor="#F2F2F2" cellpadding="2" class="tbMenuPackagePromotion" width="100%"
                    border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            <p style="font-size: medium; font-weight: bold">
                                <a href="/PackageView.aspx">
                                    <img src="/Images/Icon/icNews.png" /><asp:Label ID="lblMenuPackage" runat="server" Text=""></asp:Label>
                                </a>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p style="font-size: medium">
                                <a href="/PromotionView.aspx">
                                    <img src="/Images/Icon/icNews.png" /><asp:Label ID="lblMenuPromotion" runat="server" Text=""></asp:Label>
                                </a>
                            </p>
                        </td>
                    </tr>
                </table>
                
                <br />
                <div class="GridView">
                    <div class="GridViewHeader">
                    <p><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Icon/CartPackage.png" />
                    <asp:Label ID="lblTitleSelectedPackage" runat="server" Font-Size="Medium" Text="แพ็คเกจที่เลือก"></asp:Label></p>
                    </div>
                <table bgcolor="#F2F2F2" cellpadding="2" class="tbMenuPackagePromotion" width="100%"
                    border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td colspan="2">
                                <asp:GridView ID="gvSelectedPackage" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                    BorderWidth="0px" ShowFooter="True" CellPadding="0" CellSpacing="0" GridLines="None" 
                                    Width="100%" onrowcommand="gvSelectedPackage_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table cellpadding="5" border="0">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left">
                                                        <p style="font-size:small; font-weight:normal;">
                                                            <asp:Label ID="lblSelectedPackagecode" runat="server" Text="รหัสแพ็คเกจ : "></asp:Label>
                                                            <%# DataBinder.Eval(Container.DataItem, "PackageCode")%></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <p style="font-size:small; font-weight:normal; color:Green;">
                                                            <%# DataBinder.Eval(Container.DataItem, "PackageName")%></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <p style="font-size:small; font-weight:normal">
                                                            <asp:Label ID="lblSelectedUnitPrice" runat="server" Text="ราคา "></asp:Label>
                                                            <%# DataBinder.Eval(Container.DataItem, "UnitPrice")%>
                                                            <asp:Label ID="lblSelectedCurency" runat="server" Text=" บาท"></asp:Label>&nbsp;
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
                        <td>
                            <asp:Label ID="lblTotal" runat="server" Text="ราคารวม"></asp:Label>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="txtTotal" runat="server" Text="0"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <asp:Label ID="lblVat" runat="server" Text="ภาษี"></asp:Label>
                            </p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="txtVat" runat="server" Text="7%"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <asp:Label ID="lblGrandTotal" runat="server" Text="รวมทั้งสิ้น"></asp:Label>
                            </p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="txtGrandTotal" runat="server" Text="0"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <p>
                                <asp:Button ID="btPayment" runat="server" Text ="ชำระเงิน" CssClass = "ButtonRed"/>
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
    <uc1:ucColorBox ID="ucColorBox1" runat="server" />
</asp:Content>

