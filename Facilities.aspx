<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Facilities.aspx.cs" Inherits="Facilities" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="~/UserControl/ucContent/ucContent.ascx" tagname="ucContent" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div id="dvHeader">
        <div style="float:left;margin-right:10px;">
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/Icon/icService.png'/>" meta:resourcekey="lblIconResource1"/>
        </div>
        <div style="float:left;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>Facillities</h1>" meta:resourcekey="lblNameResource1"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="Facillities" meta:resourcekey="lblDetailResource1"/>
                </div>
                <div style="padding-top:5px;">
                    <div class="share42init"></div>
                    <script type="text/javascript" src="/Plugin/share42/share42.js"></script>
                </div>
            </div>
        </div>
        <div style="float:right;padding:5px;"></div>
        <div style="clear:both;">
        </div>
    </div>
    <div style="margin-top:10px;">
        <uc1:ucContent ID="ucContent2" runat="server" ContentName="Facilities"/>
    </div>
</asp:Content>