<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucJCarousel.ascx.cs" Inherits="ucJCarousel" %>

<link rel="stylesheet" type="text/css" href='<%=ResolveClientUrl("CSS/jcarousel.connected-carousels.css")%>' />
<style type="text/css">
    /*Slide Width*/
    .stage
    {
        width: <%=Width.ToString()%>px;
    }
    /*Slide Height*/
    .connected-carousels .carousel-stage 
    {
        height: <%=Height.ToString()%>px;
    }
    /*Navigator Button Width*/
    .connected-carousels .navigation
    {
        width: <%=(NavigatorWidth+20).ToString()%>px;
    }
    /*Navigator Width*/
    .connected-carousels .carousel-navigation
    {
        height: <%=NavigatorHeight.ToString()%>px;
        width: <%=NavigatorWidth.ToString()%>px;
    }
    .connected-carousels .prev-stage,
    .connected-carousels .next-stage 
    {
        width: <%=(Width/2).ToString()%>px;/* stage/2 */
        height:<%=(Height+10).ToString()%>px;/* stage */
    }
    .connected-carousels .prev-navigation {
        left: -15px;/*ระยะแนวนอน ปุ่มบนภาพตัวอย่าง*/
        top: <%=((NavigatorHeight/2)-3).ToString()%>px;/*ระยะแนวตั้ง ปุ่มบนภาพตัวอย่าง*/
        text-indent:10px;
        z-index:0;
    }

    .connected-carousels .next-navigation {
        right: -15px;/*ระยะแนวนอน ปุ่มบนภาพตัวอย่าง*/
        top: <%=((NavigatorHeight/2)-3).ToString()%>px;/*ระยะแนวตั้ง ปุ่มบนภาพตัวอย่าง*/
        text-indent:15px;
        z-index:0;
    }
</style>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write("<script type='text/javascript' src='<%=ResolveClientUrl("~/Plugin/jQuery/jquery.min.js")%>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof jcarousel == 'undefined') {
        document.write("<script type='text/javascript' src='<%=ResolveClientUrl("JS/jquery.jcarousel.min.js")%>'><" + "/script>");
        document.write("<script type='text/javascript' src='<%=ResolveClientUrl("JS/jcarousel.connected-carousels.js")%>'><" + "/script>");
    }
</script>

<asp:Label ID="lblDefault" runat="server" />
<%=(Visible?strGallery.ToString():"") %>

<script type="text/javascript">
    $(document).ready(function () {
        $('.carousel').jcarousel({
            animation: 'slow',
            wrap: 'circular'
        })
		.jcarouselAutoscroll({
		    interval: 5000,
		    target: '+=1',
		    autostart: <%=AutoPlay.ToString().ToLower() %>,
		});
    });
</script>