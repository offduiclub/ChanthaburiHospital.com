<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCalendarSpecialDays.ascx.cs" Inherits="ucCalendarSpecialDays" %>

<link href="<%=this.ResolveClientUrl("CSS/glDatePicker.Oofdui.css") %>" rel="stylesheet" type="text/css" />
<script type = "text/javascript">
    if (typeof jQuery == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
    }
</script>
<script type = "text/javascript">
    if (typeof jQuery.glDatePicker == 'undefined') 
    {
        document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("JS/glDatePicker.min.js") %>'><" + "/script>");
    }
</script>

<style type="text/css">
	#dvGlDatePicker
	{
		border:1px solid #DDD;
		background-color:#00C5E8;
		color:#FFF;
		padding:5px;
		text-align:center;
		-moz-box-shadow:0 1px 2px rgba(0,0,0,0.25);
		-webkit-box-shadow:0 1px 2px rgba(0,0,0,0.25);
		box-shadow:0 1px 2px rgba(0,0,0,0.25);
	}
</style>

<div style="width:<%=Width%>;height:<%=Height%>;">
	<input type="text" id="glDatePicker" gldp-id="glDatePicker" style="display:collapse;visibility:hidden;height:0;"/>
	<div gldp-el="glDatePicker" style="height:100%;width:100%;"></div>
    <div id="dvGlDatePicker">-</div>
</div>
	
<script type="text/javascript">
	$(window).load(function () {
	    $('#glDatePicker').glDatePicker(
		{
			showAlways: true,
			cssName: 'Oofdui',
			/*selectedDate: new Date(2014, 11, 27),*/
			specialDates: [
				<%=strSpecialDays %>
            ],
			onHover: function (el, cell, date, data) {
			    if (data != null) {
			        document.getElementById("dvGlDatePicker").innerHTML = "<span style='color:#FFF871;font-size:9pt;'>"+date.getDate() + '/' + date.getMonth() + '/' + date.getFullYear() + "</span><br/>" + data.message;
			        //alert("วันที่ : "+date.getDate()+'/'+date.getMonth()+'/'+date.getFullYear()+"\n\n"+data.message);
			    }
			    else {
			        document.getElementById("dvGlDatePicker").innerHTML = "-";
			    }
			},
			onClick: function (target, cell, date, data) {
			    target.val(date.getFullYear() + ' - ' +
							date.getMonth() + ' - ' +
							date.getDate());

			    if (data != null) {
			        document.getElementById("dvGlDatePicker").innerHTML = "<span style='color:#FFF871;font-size:9pt;'>"+date.getDate() + '/' + date.getMonth() + '/' + date.getFullYear() + "</span><br/>" + data.message;
			    }
			    else {
			        document.getElementById("dvGlDatePicker").innerHTML = "-";
			    }
			}
		});
	});
</script>