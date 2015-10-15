<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EventDetail.aspx.cs" Inherits="EventDetail" %>
<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div align="left">
        <p>
            <asp:Label ID="lblSiteMap" runat="server"></asp:Label></p>
    </div>
    <br />
    <div align="center" style="font-weight: bold; font-size: large">
        <p>
            <asp:Label runat="server" ID="lblTitle" meta:resourcekey="lblTitleResource1"></asp:Label></p>
    </div>
    <br />
    <table>
        <tr>
            <td align="left" style="font-weight: bold; font-size: large">
                <asp:Label ID="lblUID" runat="server" Visible="false"></asp:Label>
                <asp:Panel ID="pnAdmin" runat="server" Visible="false">
                        <a href="/EventForm.aspx?UID=<%=lblUID.Text.Trim() %>" class="cbIFrame"><span class="Icon16 Edit"></span></a>
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
            <td align="center">
                <asp:Image ID="PicFull" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblDetail" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ucColorBox ID="ucColorBox1" runat="server" />
</asp:Content>

