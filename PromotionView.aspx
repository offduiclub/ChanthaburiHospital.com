<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PromotionView.aspx.cs" Inherits="PromotionView" ValidateRequest="false" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagName="ucLanguageDB" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucColorBox/ucColorBox.ascx" TagName="ucColorBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
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
            <td width="800px">
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
                        <a href="/NewsForm.aspx" class="cbIFrame"><span class="Button AddTH"></span></a>
                    </asp:Panel>
                    <%--<asp:Button ID="btAdmin" runat="server" CssClass="Button AddTH" Text="" Visible="False"
                        OnClick="btAdmin_Click" />--%>
                </div>
                <!-- ***************************************************************** -->
                <!-- ****************************** Gridview Detail *******************-->
                <!-- ***************************************************************** -->
                <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                    BorderWidth="0px" ShowFooter="True" CellPadding="0" CellSpacing="0" GridLines="None"
                    OnRowCommand="gvPromotion_RowCommand" OnRowDataBound="gvPromotion_RowDataBound">
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
                                        <a href="Promotion/<%# DataBinder.Eval(Container.DataItem,"UID") %>/<%#DataBinder.Eval(Container.DataItem,"PromotionName") %>">
                                            <p>
                                                <%#DataBinder.Eval(Container.DataItem, "PromotionName")%></p>
                                        </a>
                                    </td>
                                    <td rowspan="2">
                                        <asp:Label ID="UID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UID") %>'
                                            Visible="false"></asp:Label>
                                        <a href="PromotionForm.aspx?UID=<%# DataBinder.Eval(Container.DataItem,"UID") %>" class="cbIFrame" style='display:<%#(Security.LoginGroup == "Admin"?"inline":"none")%>;'>
                                            <span class="Icon16 Edit"></span>
                                        </a>
                                        <%--<asp:Button ID="btEdit" runat="server" CssClass="Icon16 Edit" Text="" CommandName="EditPromotion"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                            BorderStyle="None" />--%>
                                        <asp:Button ID="btDelete" runat="server" CssClass="Icon16 Delete" Text="" CommandName="DeletePromotion"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                            BorderStyle="None" OnClientClick="return confirm('Are you sure you want to delete this promotion?');" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" style="font-weight: normal">
                                        <%# DataBinder.Eval(Container.DataItem, "DetailSub")%>
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
            <td width="200px" align="left" valign="top">
                <table bgcolor="#F2F2F2" cellpadding="2" class="tbMenuPackagePromotion" width="100%"
                    border="2">
                    <tr>
                        <td>
                            <p style="font-size: medium">
                                <a href="PackageView.aspx">
                                    <img src="Images/Icon/icNews.png" /><asp:Label ID="lblMenuPackage" runat="server" Text=""></asp:Label></a></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p style="font-size: medium; font-weight:bold">
                                <a href="PromotionView.aspx">
                                    <img src="Images/Icon/icNews.png" /><asp:Label ID="lblMenuPromotion" runat="server" Text=""></asp:Label></a></p>
                        </td>
                    </tr>
                </table>
                </p>
            </td>
        </tr>
    </table>
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="ucPromotionView"/>
</asp:Content>

