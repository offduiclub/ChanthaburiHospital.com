<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArticleDetail.aspx.cs" Inherits="ArticleDetail" %>
<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 351px;
        }
        .style2
        {
            width: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div align="left">
        <p>
            <asp:Label ID="lblSiteMap" runat="server"></asp:Label></p>
    </div>
    <br />
    <div align="left" style="font-weight: bold; font-size: large">
        <p>
            <asp:Label runat="server" ID="lblTitle" ></asp:Label></p>
    </div>
    <br />
    <table>
        <tr>
            <td align="left" style="font-weight: bold; font-size: large">
                <asp:Label ID="lblUID" runat="server" Visible="false"></asp:Label>
                <asp:Panel ID="pnAdmin" runat="server" Visible="false">
                        <a href="/ArticleForm.aspx?UID=<%=lblUID.Text.Trim() %>" class="cbIFrame"><span class="Icon16 Edit"></span></a>
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
    <div>
    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table align="center" cellpadding="0" cellspacing="0" class="style1" 
                        width="300">
                        <tr>
                            <td align="center">
                                <asp:ImageButton ID="btLike" runat="server" ImageUrl="~/Images/Icon/Like.png" 
                                    onclick="btLike_Click" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="btDisLike" runat="server" 
                                    ImageUrl="~/Images/Icon/DisLike.png" onclick="btDisLike_Click" />
                            </td>
                            <td class="style2" rowspan="2">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Icon/Pipe.png" />
                            </td>
                            <td align="left" rowspan="2">
                                <asp:Label ID="NumberView" runat="server" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="NumberLike" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="NumberDisLike" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        
    </div>

    <uc1:ucColorBox ID="ucColorBox1" runat="server" />
</asp:Content>

