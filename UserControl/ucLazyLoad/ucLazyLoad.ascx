<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLazyLoad.ascx.cs" Inherits="UserControl_ucLazyLoad_ucLazyLoad" %>

<link href="<%=this.ResolveClientUrl("Plugin/jquery.lazyloadxt."+Effect.ToString().ToLower()+".css")%>" rel="stylesheet" type="text/css" />

<style type="text/css">
    img, video, iframe[data-src] 
    {
        box-shadow: inset 0 0 0 1px #ccc;
    }
</style>

<script type = "text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>

<script type = "text/javascript">
    if (typeof jQuery.lazyloadxt == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("Plugin/jquery.lazyloadxt.js")%>'><" + "/script>");
    }
</script>