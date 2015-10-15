<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AboutChanthaburi.aspx.cs" Inherits="AboutChanthaburi" %>

<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagPrefix="uc1" TagName="ucLanguageDB" %>
<%@ Register Src="~/UserControl/ucContent/ucContent.ascx" TagPrefix="uc1" TagName="ucContent" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucContent runat="server" ID="ucContent" ContentName="AboutChanthaburi"/>
</asp:Content>