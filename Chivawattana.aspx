<%@ Page Title="สมาชิกบัตรชีววัฒนะ โรงพยาบาลกรุงเทพจันทบุรี" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Chivawattana.aspx.cs" Inherits="Chivawattana" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagPrefix="uc1" TagName="ucLanguageDB" %>
<%@ Register Src="~/UserControl/ucContent/ucContent.ascx" TagPrefix="uc1" TagName="ucContent" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div id="dvHeader">
        <div style="float:left;margin-right:10px;">
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/icChivawattana.png'/>" meta:resourcekey="lblIconResource1"/>
        </div>
        <div style="float:left;width:600px;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>Chivawattana Member Card</h1>" meta:resourcekey="lblNameResource1"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="Chivawattana Member Card" meta:resourcekey="lblDetailResource1"/>
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
    <div>
        <uc1:ucContent runat="server" ID="ucContent" ContentName="Chivawattana"/>
    </div>
</asp:Content>