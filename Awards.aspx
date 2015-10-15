<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Awards.aspx.cs" Inherits="Awards" %>

<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagName="ucLanguageDB" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucColorBox/ucColorBox.ascx" TagName="ucColorBox" TagPrefix="uc1" %>
<%@ Register src="~/UserControl/ucContent/ucContent.ascx" tagname="ucContent" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div>
        <uc3:ucContent ID="ucContent1" runat="server" ContentName="Awards"/>
    </div>
    <uc1:ucColorBox ID="ucColorBox1" runat="server" />
</asp:Content>

