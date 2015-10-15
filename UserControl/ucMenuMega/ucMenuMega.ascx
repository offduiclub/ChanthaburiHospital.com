<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenuMega.ascx.cs" Inherits="ucMenuMega" %>

<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>

<link href="<%=this.ResolveClientUrl("CSS/sm-core-css.css") %>" rel="stylesheet" type="text/css" />
<link href="<%=this.ResolveClientUrl("CSS/" + CssClassName + "/sm-default.css") %>" rel="stylesheet" type="text/css" />

<!-- SmartMenus jQuery init -->
<script type="text/javascript">
    if (typeof smartmenus == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("JS/jquery.smartmenus.js") %>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    $(function () {
        $('#main-menu').smartmenus({
            subMenusSubOffsetX: 1,
            subMenusSubOffsetY: -8
        });
    });
</script>
<style type="text/css">
    #main-menu
    {
        position: relative;
        z-index: 9999;
        width: auto;
    }
    #main-menu ul
    {
        width: 12em; /* fixed width only please - you can use the "subMenusMinWidth"/"subMenusMaxWidth" script options to override this if you like */
    }
    #sm-home
    {
        padding:12px 12px 12px 7px;
        border-right:0px solid #5298C3;
    }
    #sm-home a
    {
        color:#FFF;
        padding:0px;
    }
    #sm-home a:hover
    {
        background-color:transparent;
    }
    .sm-default a:hover, .sm-default a:focus, .sm-default a:active,.sm-default a.current,.sm-default a.actived {
		background:<%=HighlightColor%>;
		color:#FFF;
	}
	.sm-default-vertical a.highlighted {
		background:<%=HighlightColor%>;
		color:#FFF;
	}
	.sm-default ul a.highlighted {
		background:<%=HighlightColor%>;/*สีไฮไลค์ในเมนูย่อยที่มีเมนูย่อยอีกชั้น (DropDown)*/
		color:#FFF;
	}
	.sm-default a.current, .sm-default a.current:hover, .sm-default a.current:focus, .sm-default a.current:active {
		border-bottom-color:<%=HighlightColor%>;
	}
	.sm-default-vertical a.current, .sm-default-vertical a.current:hover, .sm-default-vertical a.current:focus, .sm-default-vertical a.current:active {
		border-right:2px solid <%=HighlightColor%>;
	}
	.sm-default ul a.current, .sm-default ul a.current:hover,.sm-default ul a.actived, .sm-default ul a.actived:hover, .sm-default ul a.current:focus, .sm-default ul a.current:active {
		background:<%=HighlightColor%>;/*สีไฮไลค์ในเมนูย่อยที่ Active อยู่*/
		color:#FFF;
	}
</style>

<asp:Label ID="lblMenuMega" runat="server" />