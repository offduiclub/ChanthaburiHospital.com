<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTabs.ascx.cs" Inherits="ucTabs" %>

    <style type="text/css">
    #<%=UID %> ul,#<%=UID %> ol,#<%=UID %> li,#<%=UID %> p{
	    margin:0px;
	    padding:0px;
    }

    #<%=UID %> {
	    margin: 0 auto;
    }

    #<%=UID %> li{
	    z-index:2;
	    float: left;
	    margin-right: -1px;
	    list-style:none;
	
	    border-left:1px solid #DDD;
	    border-top:1px solid #DDD;
	    border-right:1px solid #DDD;
    }

    /*## เมนูบนแทป ##*/
    #<%=UID %> li a {
	    z-index:2;
	    display: block;
	    position:relative;
	    margin-bottom:-1px;
	    padding: 17px 30px;
	    text-decoration: none;
	    color: #4B4B4B;
	
	    background-color: #F3F1F1;
	    background-image: -webkit-gradient(linear, left top, left bottom, from(#FCFBFB), to(#F3F1F1));
	    background-image: -webkit-linear-gradient(top, #FCFBFB, #F3F1F1);
	    background-image: -moz-linear-gradient(top, #FCFBFB, #F3F1F1);
	    background-image: -ms-linear-gradient(top, #FCFBFB, #F3F1F1);
	    background-image: -o-linear-gradient(top, #FCFBFB, #F3F1F1);
	    background-image: linear-gradient(top, #FCFBFB, #F3F1F1);
	
    /* 	box-shadow:3px 3px 5px #E6E6E6;
	    -moz-box-shadow:3px 3px 5px #E6E6E6;
	    -webkit-box-shadow:3px 3px 5px #E6E6E6; */
    }
    /*
    #<%=UID %> li:first-child a{
	    -moz-border-radius:7px 0 0 0;
	    -webkit-border-radius:7px 0 0 0;
	    border-radius:7px 0 0 0;
    }
    #<%=UID %> li:last-child a{
	    -moz-border-radius:0 7px 0 0;
	    -webkit-border-radius:0 7px 0 0;
	    border-radius:0 7px 0 0;
    }
    */
    #<%=UID %> li a:hover {
	    background: #F3F1F1;
    }

    /*## Content Panel ##*/
    #tabs_container {
	    z-index:1;
	    overflow: hidden;
	    position: relative;
	    background: #FFF;
	    border-left:1px solid #DDD;	
	    border-right:1px solid #DDD;	
	    border-bottom:1px solid #DDD;
	    border-top:1px solid #DDD;
	    vertical-align:top;
	    padding:10px;
	    /* -moz-border-radius:0 7px 7px 7px;
	    -webkit-border-radius:0 7px 7px 7px;
	    border-radius:0 7px 7px 7px; */
	
	    box-shadow:3px 3px 5px #E6E6E6;
	    -moz-box-shadow:3px 3px 5px #E6E6E6;
	    -webkit-box-shadow:3px 3px 5px #E6E6E6;
    }

    #tabs_container div {
	
    }

    .transition {
	    -webkit-transition: all .3s ease-in-out;
	    -moz-transition: all .3s ease-in-out;
	    -o-transition: all .3s ease-in-out;
	    -ms-transition: all .3s ease-in-out;
	    transition: all .3s ease-in-out;

	    -webkit-transition-delay: .3s;
	    -moz-transition-delay: .3s;
	    -o-transition-delay: .3s;
	    -ms-transition-delay: .3s;
	    transition-delay: .3s;
    }

    .make_transist {
	    -webkit-transition: all .3s ease-in-out;
	    -moz-transition: all .3s ease-in-out;
	    -o-transition: all .3s ease-in-out;
	    -ms-transition: all .3s ease-in-out;
	    transition: all .3s ease-in-out;
    }

    .hidescale {
	    -webkit-transform: scale(0.9);
	    -moz-transform: scale(0.9);
	    -o-transform: scale(0.9);
	    -ms-transform: scale(0.9);
	    transform: scale(0.9);
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
	    filter: alpha(opacity=0);
	    filter: alpha(opacity=0);
	    opacity: 0;
    }

    .showscale {
	    -webkit-transform: scale(1);
	    -moz-transform: scale(1);
	    -o-transform: scale(1);
	    -ms-transform: scale(1);
	    transform: scale(1);
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
	    filter: alpha(opacity=100);
	    opacity: 1;

	    -webkit-transition-delay: .3s;
	    -moz-transition-delay: .3s;
	    -o-transition-delay: .3s;
	    -ms-transition-delay: .3s;
	    transition-delay: .3s;
    }

    .hideleft {
	    -webkit-transform: translateX(-100%);
	    -moz-transform: translateX(-100%);
	    -o-transform: translateX(-100%);
	    -ms-transform: translateX(-100%);
	    transform: translateX(-100%);
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
	    filter: alpha(opacity=0);
	    opacity: 0;
    }

    .showleft {
	    -webkit-transform: translateX(0px);
	    -moz-transform: translateX(0px);
	    -o-transform: translateX(0px);
	    -ms-transform: translateX(0px);
	    transform: translateX(0px);
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
	    filter: alpha(opacity=100);
	    opacity: 1;

	    -webkit-transition-delay: .3s;
	    -moz-transition-delay: .3s;
	    -o-transition-delay: .3s;
	    -ms-transition-delay: .3s;
	    transition-delay: .3s;
    }

    .hidescaleup {
	    -webkit-transform: scale(1.1);
	    -moz-transform: scale(1.1);
	    -o-transform: scale(1.1);
	    -ms-transform: scale(1.1);
	    transform: scale(1.1);
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
	    filter: alpha(opacity=0);
	    opacity: 0;
    }

    .showscaleup {
	    -webkit-transform: scale(1);
	    -moz-transform: scale(1);
	    -o-transform: scale(1);
	    -ms-transform: scale(1);
	    transform: scale(1);
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
	    filter: alpha(opacity=100);
	    opacity: 1;

	    -webkit-transition-delay: .3s;
	    -moz-transition-delay: .3s;
	    -o-transition-delay: .3s;
	    -ms-transition-delay: .3s;
	    transition-delay: .3s;
    }

    .hideflip {
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
	    filter: alpha(opacity=0);
	    opacity: 0;

	    -webkit-transform: rotatey(-90deg) scale(1.1);
	    -moz-transform: rotatey(-90deg) scale(1.1);
	    -o-transform: rotatey(-90deg) scale(1.1);
	    -ms-transform: rotatey(-90deg) scale(1.1);
	    transform: rotatey(-90deg) scale(1.1);

	    -webkit-transform-origin: 50% 50%;
	    -moz-transform-origin: 50% 50%;
	    -o-transform-origin: 50% 50%;
	    -ms-transform-origin: 50% 50%;
	    transform-origin: 50% 50%;
    }

    .showflip {
	    -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
	    filter: alpha(opacity=100);
	    opacity: 1;

	    -webkit-transition-delay: .3s;
	    -moz-transition-delay: .3s;
	    -o-transition-delay: .3s;
	    -ms-transition-delay: .3s;
	    transition-delay: .3s;

	    -webkit-transform: rotatey(0deg) scale(1);
	    -moz-transform: rotatey(0deg) scale(1);
	    -o-transform: rotatey(0deg) scale(1);
	    -ms-transform: rotatey(0deg) scale(1);
	    transform: rotatey(0deg) scale(1);

	    -webkit-transform-origin: 50% 50%;
	    -moz-transform-origin: 50% 50%;
	    -o-transform-origin: 50% 50%;
	    -ms-transform-origin: 50% 50%;
	    transform-origin: 50% 50%;
    }

    .tabulous_active {
	    /*border-top:4px solid red;*/
	    background: white !important;
	    color: #4B4B4B !important;
    }

    .tabulousclear {
	    display: block;
	    clear: both;
    }
</style>

<asp:Label ID="lblTabs" runat="server" />

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script src="<%=this.ResolveClientUrl("JS/tabulous.js") %>" type="text/javascript"></script>

<script type="text/javascript" src="JS/tabulous.js"></script>
<script type="text/javascript">
	$(document).ready(function ($) {
	    $('#<%=UID %>').tabulous({
	        effect: '<%=Effect.ToString() %>'
	    });
	});
</script>