<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucOwlCarousel.ascx.cs" Inherits="UserControl_ucOwlCarousel_ucOwlCarousel" %>

<link rel="stylesheet" type="text/css" href='<%=ResolveClientUrl("CSS/owl.carousel.css")%>' />
<link rel="stylesheet" type="text/css" href='<%=ResolveClientUrl("CSS/owl.theme.css")%>' />

<style type="text/css">
    #owl-demo .item
    {
        margin: 3px;
    }
    #owl-demo .item img
    {
        width: 100%;
        height:auto;
        display: block;
    }
</style>

<asp:Label ID="lblDefault" runat="server" />
<%=strGallery.ToString()%>
<!--
<div id="owl-demo" class="owl-carousel">
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl1.jpg" alt="Owl Image"></div>
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl2.jpg" alt="Owl Image"></div>
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl3.jpg" alt="Owl Image"></div>
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl4.jpg" alt="Owl Image"></div>
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl5.jpg" alt="Owl Image"></div>
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl6.jpg" alt="Owl Image"></div>
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl7.jpg" alt="Owl Image"></div>
	<div class="item"><img src="http://owlgraphic.com/owlcarousel/demos/assets/owl8.jpg" alt="Owl Image"></div>
</div>
-->
<script type="text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write("<script type='text/javascript' src='<%=ResolveClientUrl("~/Plugin/jQuery/jquery.min.js")%>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof carousel == 'undefined') {
        document.write("<script type='text/javascript' src='<%=ResolveClientUrl("JS/owl.carousel.min.js")%>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    $(document).ready(function() {
      $("#owl-demo").owlCarousel({
        autoPlay: <%=(AutoPlay?(AutoPlaySecond*1000).ToString():"false") %>,
        items: <%=ItemsMax.ToString() %>,
        itemsDesktop: [1199,3],
        itemsDesktopSmall: [979,3],
        stopOnHover: <%=StopOnHover.ToString().ToLower() %>,
        pagination: false
      });
    });
</script>