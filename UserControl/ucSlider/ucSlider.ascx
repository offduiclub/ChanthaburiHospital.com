<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSlider.ascx.cs" Inherits="UserControl_ucSlider" %>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type = "text/javascript">
    if (typeof jcobbSlider == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/jcobbSlider/js/bjqs.min.js") %>'><" + "/script>");
    document.write("<link rel='stylesheet' type='text/css' href='<%=this.ResolveClientUrl("Plugin/jcobbSlider/css/bjqs.css") %>'" + "/>");
    }
</script>

<asp:Label ID="lblSlider" runat="server"/>

<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $("#Slider").bjqs({
            height: <%=Height %>,
            width: <%=Width %>,
            animation: '<%=Animation %>',
            animationDuration: <%=Duration %>,
            showControls: false,
            showMarkers: false,
            centerControls: false,
            useCaptions: false,
            keyboardNav: false,
            nexttext:'',
            prevtext:''
        });
    });
</script>