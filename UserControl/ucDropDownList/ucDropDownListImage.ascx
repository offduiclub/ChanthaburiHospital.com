<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDropDownListImage.ascx.cs" Inherits="ucDropDownListImage" %>

<style type="text/css">
    .hide
    {
        display:none;
    }
</style>
<script type = "text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type = "text/javascript">
    if (typeof ddslick == 'undefined') {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/jquery.ddslick.min.js") %>'><" + "/script>");
    }
</script>

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=ddlDefault.ClientID %>').ddslick({
            //width:"inherit",/*default value '260'*/
            //height:"100",/*default value 'null'*/
            //selectText: "Select your favorite social network",/*default value 'Select...'*/
            background: "#FAFAFA", /*default value '#eee'*/
            showSelectedHTML: false, /*default value 'true'*/
            imagePosition: "left", /*default value 'left'*/
            onSelected: function (data) {
                document.getElementById('<%=txtValue.ClientID %>').value = data.selectedData.value;
                document.getElementById('<%=txtText.ClientID %>').value = data.selectedData.text;
            }
        });
    });
</script>

<asp:DropDownList ID="ddlDefault" runat="server">
</asp:DropDownList>
<asp:TextBox ID="txtValue" runat="server" CssClass="hide"/>
<asp:TextBox ID="txtText" runat="server" CssClass="hide"/>