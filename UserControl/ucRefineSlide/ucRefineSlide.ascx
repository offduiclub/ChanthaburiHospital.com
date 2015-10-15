<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRefineSlide.ascx.cs" Inherits="UserControl_ucRefineSlide_ucRefineSlide" %>

<script type="text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write("<script type='text/javascript' src='<%=ResolveClientUrl("~/Plugin/jQuery/jquery.min.js")%>'><" + "/script>");
    }
</script>
<script type="text/javascript">
    if (typeof refineslide == 'undefined') {
        document.write("<script type='text/javascript' src='<%=ResolveClientUrl("JS/jquery.refineslide.js")%>'><" + "/script>");
    }
</script>

<link rel="stylesheet" href='<%=ResolveClientUrl("CSS/refineslide.css")%>' />
<link rel="stylesheet" href='<%=ResolveClientUrl("CSS/refineslide-theme-dark.css")%>' />
<!--[if lt IE 9]>
    <script src='<%=ResolveClientUrl("JS/respond.js")%>'></script>
<![endif]-->

<script type="text/javascript">
	$(function () {
	    var $upper = $('#upper');

	    $('#images').refineSlide({
	        transition: '<%=Transition.ToString() %>', // Transition type ('custom', random', 'cubeH', 'cubeV', 'fade', 'sliceH', 'sliceV', 'slideH', 'slideV', 'scale', 'blockScale', 'kaleidoscope', 'fan', 'blindH', 'blindV')
	        /*transitionDuration : 2000,*/
	        /*delay              : 3000,*/
	        /*maxWidth           : 600,*/
	        autoPlay: <%=AutoPlay.ToString().ToLower() %>,
	        keyNav: true,
	        controls: null,
	        useThumbs: true,     // Bool (default true): Navigation type thumbnails
	        useArrows: false,    // Bool (default false): Navigation type previous and next arrows
	        onInit: function () {
	            var slider = this.slider,
                    $triggers = $('.translist').find('> li > a');

	            $triggers.parent().find('a[href="#_' + this.slider.settings['transition'] + '"]').addClass('active');

	            $triggers.on('click', function (e) {
	                e.preventDefault();

	                if (!$(this).find('.unsupported').length) {
	                    $triggers.removeClass('active');
	                    $(this).addClass('active');
	                    slider.settings['transition'] = $(this).attr('href').replace('#_', '');
	                }
	            });

	            function support(result, bobble) {
	                var phrase = '';

	                if (!result) {
	                    phrase = ' not';
	                    $upper.find('div.bobble-' + bobble).addClass('unsupported');
	                    $upper.find('div.bobble-js.bobble-css.unsupported').removeClass('bobble-css unsupported').text('JS');
	                }
	            }
	        }
	    });
	});
</script>
<asp:Label ID="lblDefault" runat="server" />
<!--START RefineSlide-->
<%=strSlide.ToString() %>
<!--STOP RefineSlide-->