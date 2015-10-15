<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArticleView.aspx.cs" Inherits="ArticleView" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagName="ucLanguageDB" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucColorBox/ucColorBox.ascx" TagName="ucColorBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
    <div align="center" style="font-weight: bold; font-size: large">
        <p>
            <asp:Label runat="server" ID="lblTitle" meta:resourcekey="lblTitleResource1"></asp:Label></p>
    </div>
    <br />
    <!-- ***************************************************************** -->
    <!-- ******************************** ปุ่ม Add **************************-->
    <!-- ***************************************************************** -->
    <div align="Right">
        <asp:Panel ID="pnAdminButton" runat="server" Visible="false">
            <a href="/ArticleForm.aspx" class="cbIFrame"><span class="Button AddTH"></span></a>
        </asp:Panel>
        <%--<asp:Button ID="btAdmin" runat="server" CssClass="Button AddTH" Text=""
            Visible="False" onclick="btAdmin_Click" />--%>
    </div>
    <!-- ***************************************************************** -->
    <!-- ****************************** Gridview Detail *******************-->
    <!-- ***************************************************************** -->
    <asp:GridView ID="gvArticle" runat="server" AutoGenerateColumns="False" BorderStyle="None"
        BorderWidth="0px" ShowFooter="True" CellPadding="0" CellSpacing="0" 
        GridLines="None" onrowcommand="gvArticle_RowCommand" 
        onrowdatabound="gvArticle_RowDataBound">
        <Columns>
            <asp:TemplateField >
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
                            <a href="Article/<%# DataBinder.Eval(Container.DataItem,"UID") %>/<%#DataBinder.Eval(Container.DataItem,"Subject") %>">
                                <p>
                                    <%#DataBinder.Eval(Container.DataItem,"Subject") %></p>
                            </a>
                        </td>
                        <td rowspan="2">
                            <asp:Label ID="UID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UID") %>'
                                Visible="False" ></asp:Label>
                            <a href="ArticleForm.aspx?UID=<%# DataBinder.Eval(Container.DataItem,"UID") %>" class="cbIFrame" style='display:<%#(Security.LoginGroup == "Admin"?"inline":"none")%>;'>
                                <span class="Icon16 Edit"></span>
                            </a>
                            <%--<asp:Button ID="btEdit" runat="server" CssClass="Icon16 Edit" Text="" CommandName="EditArticle"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                BorderStyle="None" />--%>
                            <asp:Button ID="btDelete" runat="server" CssClass="Icon16 Delete" Text="" CommandName="DeleteArticle"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                BorderStyle="None" OnClientClick="return confirm('Are you sure you want to delete this article?');" />
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
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="ucArticleView"/>
</asp:Content>

