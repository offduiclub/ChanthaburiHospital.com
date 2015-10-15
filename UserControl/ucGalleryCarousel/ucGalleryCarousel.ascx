<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGalleryCarousel.ascx.cs" Inherits="UserControl_ucGallery_ucGalleryCarousel" %>

<link href='<%=ResolveClientUrl("CSS/CSS.css") %>' rel="stylesheet" type="text/css" />

<script type="text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='~/Plugin/jQuery/jquery.min.js'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof carouFredSel == 'undefined') 
    {
        document.write("<script type='text/javascript' src='Plugin/jquery.carouFredSel-5.2.3-packed.js'><" + "/script>");
    }
</script>

<script language="javascript" type="text/javascript">
    $(function () {
        $('#thumbs .thumb a').each(function (i) {
            $(this).addClass('itm' + i);
            $(this).click(function () {
                $('#images').trigger('slideTo', [i, 0, true]);
                return false;
            });
        });
        $('#thumbs a.itm0').addClass('selected');

        $('#images').carouFredSel({
            direction: 'left',
            circular: true,
            infinite: false,
            items: 1,
            auto: <%=AutoPlay.ToString().ToLower() %>,
            scroll: {
                fx: 'directscroll',
                onBefore: function () {
                    var pos = $(this).triggerHandler('currentPosition');
                    $('#thumbs a').removeClass('selected');
                    $('#thumbs a.itm' + pos).addClass('selected');

                    var page = Math.floor(pos / 3);
                    $('#thumbs').trigger('slideToPage', page);
                }
            }
        });
        $('#thumbs').carouFredSel({
            direction: 'left',
            circular: true,
            infinite: false,
            items: <%=ThumbNumber %>, //จำนวน Thumb
            align: false,
            auto: false,
            prev: '#prev',
            next: '#next'
        });

    });
</script>

<asp:Label ID="lblDefault" runat="server" />
<!--กำหนดขนาดสไลด์ที่แสดง-->
<div id="gallery" style="width: 432px;height: 280px;position:relative;margin:0 auto;">
<div id="main" style="height:220px;">
    <!--กำหนดขนาดรูปที่แสดง-->
	<div class="caroufredsel_wrapper" style="width: 400px; height: 210px;">
		<div id="images" style="float: none; position: absolute; top: 0px; left: 0px; margin: 0px;">
            <%=strSlide.ToString() %>
            <!--<div class="slide">
				<a href=""></a>
				<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/nemo.jpg">				
			</div>
			<div class="slide">
				<a href=""></a>
				<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/walle.jpg">
			</div>
			<div class="slide">
				<a href=""></a>
				<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/toystory.jpg">
			</div>
			<div class="slide">
				<a href=""></a>
				<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/up.jpg">
			</div>
			<div class="slide">
				<a href=""></a>
				<img src="~/UserControl/ucGalleryCarousel/Upload/201361620559_1.jpg"/>
			</div>-->
		</div>
	</div>
</div>
<!--กำหนดขนาดกรอบ Thumb-->
<div class="caroufredsel_wrapper2" style="width:306px;height: 40px;">
	<div id="thumbs" style="float: none; position: absolute; top: 0px; left: 0px; margin: 0px; height: 40px;">
        <%=strThumb.ToString() %>
        <!--<div class="thumb">
			<a href="" class=""></a>
			<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/nemo(1).jpg" width="72">				
		</div>
		<div class="thumb">
			<a href="" class=""></a>
			<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/walle(1).jpg" width="72">
		</div>
		<div class="thumb">
			<a href="" class=""></a>
			<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/toystory(1).jpg" width="72">
		</div>
		<div class="thumb">
			<a href="" class=""></a>
			<img alt="" src="~/UserControl/ucGalleryCarousel/Upload/up(1).jpg" width="72">
		</div>
		<div class="thumb">
			<a href="" class=""></a>
			<img src="~/UserControl/ucGalleryCarousel/Upload/201361620559_1.jpg" width="72"/>
		</div>-->
	</div>
</div>
	<a class="thumbs" href="" id="prev" style="display: block;">Previous</a>
	<a class="thumbs" href="" id="next" style="display: block;">Next</a>
</div>