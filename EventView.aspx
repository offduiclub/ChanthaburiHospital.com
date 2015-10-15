<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EventView.aspx.cs" Inherits="EventView" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagName="ucLanguageDB" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucColorBox/ucColorBox.ascx" TagName="ucColorBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style type="text/css">
        .dvContent,.dvContentNormal
        {
            padding:10px;
            position:relative;
        }
        .dvContent:hover
        {
            padding:9px;
            border:1px solid #DDD;
            box-shadow:3px 3px 5px #A3A3A3;
        }
        .dvContent:hover .dvContentMenu
        {
            visibility:visible;
        }
        .dvContentMenu
        {
            border-left:1px solid #DDD;border-bottom:1px solid #DDD;
            background-color:#FAFAFA;
            position:absolute;top:0;right:0;
            padding:5px;
            visibility:hidden;
        }
        #dvContent
        {
            display:table;
            width:100%;
        }
        #dvContentMain
        {
            padding:10px 0px;
            text-align:left;
        }
        #dvContentReference
        {
            display:table-cell;
            width:250px;
            padding:5px;
            background-color:#F4F4F4;
            text-align:left;
        }
        .dvContentReferenceItem
        {
            padding:0 5px 0 5px;
        }
        .dvContentRow
        {
            display:table-row;
            vertical-align:middle;
        }
        .dvContentName
        {
            display:table-cell;text-align:left;color:#326585;font-weight:bold;width:100px;
        }
        .dvContentValue
        {
            display:table-cell;text-align:left;
        }
        .dvPhotoFrame
        {
            border:1px solid #ddd;
            margin:5px;
            float:left;
            display:block;
            width:150px;
        }
        .dvPhotoFramePhoto
        {
            border-bottom:1px solid #E6E6E6;
            width:150px;
            overflow:hidden;float:left;margin-right:10px;
            background-color:#fff;
        }
        .dvPhotoFramePhoto img
        {
            filter:alpha(opacity=80);
            -moz-opacity:.80;opacity:.80;
            width:150px;
        }
        .dvPhotoFramePhoto:hover img
        {
            filter:alpha(opacity=100);
            -moz-opacity:1;opacity:1;
        }
        .dvPhotoFrameName
        {
            background-color:#fafafa;
            padding:5px;
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
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
            <a href="/EventForm.aspx" class="cbIFrame"><span class="Button AddTH"></span></a>
        </asp:Panel>
        <%--<asp:Button ID="btAdmin" runat="server" CssClass="Button AddTH" Text="" OnClick="btAdmin_Click"
            Visible="False" />--%>
    </div>
    <!-- ***************************************************************** -->
    <!-- ****************************** Gridview Detail *******************-->
    <!-- ***************************************************************** -->
    <asp:GridView ID="gvEvent" runat="server" AutoGenerateColumns="False" BorderStyle="None"
        BorderWidth="0px" ShowFooter="True" OnRowDataBound="gvEvent_RowDataBound" OnRowCommand="gvEvent_RowCommand"
        meta:resourcekey="gvEventResource1" CellPadding="0" CellSpacing="0" GridLines="None">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
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
                            <a href="Event/<%# DataBinder.Eval(Container.DataItem,"UID") %>/<%#DataBinder.Eval(Container.DataItem,"Subject") %>">
                                <p>
                                    <%#DataBinder.Eval(Container.DataItem,"Subject") %></p>
                            </a>
                        </td>
                        <td rowspan="2">
                            <asp:Label ID="UID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UID") %>'
                                Visible="False" meta:resourcekey="UIDResource1"></asp:Label>
                            <a href="EventForm.aspx?UID=<%# DataBinder.Eval(Container.DataItem,"UID") %>" class="cbIFrame" style='display:<%#(Security.LoginGroup == "Admin"?"inline":"none")%>;'>
                                <span class="Icon16 Edit"></span>
                            </a>
                            <%--<asp:Button ID="btEdit" runat="server" CssClass="Icon16 Edit" Text="" CommandName="EditEvent"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                BorderStyle="None" />--%>
                            <asp:Button ID="btDelete" runat="server" CssClass="Icon16 Delete" Text="" CommandName="DeleteEvent"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>' BorderWidth="0"
                                BorderStyle="None" OnClientClick="return confirm('Are you sure you want to delete this event?');" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" style="font-weight: normal;vertical-align:top;">
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
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="ucEventView"/>
</asp:Content>

