<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Service.aspx.cs" Inherits="Service" %>

<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>
<%@ Register src="~/UserControl/ucJCarousel/ucJCarousel.ascx" tagname="ucJCarousel" tagprefix="uc4" %>

<%@ Register src="UserControl/ucGallery/ucGallery.ascx" tagname="ucGallery" tagprefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .dvContent,.dvContentNormal
        {
            padding:10px;
            position:relative;
        }
        .dvContent:hover
        {
            padding:9px;
            border:1px solid #DDD;
            box-shadow:3px 3px 5px #A3A3A3;
        }
        .dvContent:hover .dvContentMenu
        {
            visibility:visible;
        }
        .dvContentMenu
        {
            border-left:1px solid #DDD;border-bottom:1px solid #DDD;
            background-color:#FAFAFA;
            position:absolute;top:0;right:0;
            padding:5px;
            visibility:hidden;
        }
        #dvContent
        {
            display:table;
            width:100%;
        }
        #dvContentMain
        {
            padding:10px 0px;
            text-align:left;
        }
        #dvContentReference
        {
            display:table-cell;
            width:250px;
            padding:5px;
            background-color:#F4F4F4;
            text-align:left;
        }
        .dvContentReferenceItem
        {
            padding:0 5px 0 5px;
        }
        .dvContentRow
        {
            display:table-row;
            vertical-align:middle;
        }
        .dvContentName
        {
            display:table-cell;text-align:left;color:#326585;font-weight:bold;width:100px;
        }
        .dvContentValue
        {
            display:table-cell;text-align:left;
        }
        .dvPhotoFrame
        {
            border:1px solid #ddd;
            margin:5px;
            float:left;
            display:block;
            width:150px;
        }
        .dvPhotoFramePhoto
        {
            border-bottom:1px solid #E6E6E6;
            width:150px;
            overflow:hidden;float:left;margin-right:10px;
            background-color:#fff;
        }
        .dvPhotoFramePhoto img
        {
            filter:alpha(opacity=80);
            -moz-opacity:.80;opacity:.80;
            width:150px;
        }
        .dvPhotoFramePhoto:hover img
        {
            filter:alpha(opacity=100);
            -moz-opacity:1;opacity:1;
        }
        .dvPhotoFrameName
        {
            background-color:#fafafa;
            padding:5px;
            text-align:center;
        }
    </style>

    <script type="text/javascript">
        if (typeof jQuery == 'undefined') 
        {
            document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/jQuery/jquery.min.js") %>'><" + "/script>");
        }
    </script>
    <script type="text/javascript">
        if (typeof colorbox == 'undefined') 
        {
            document.write("<script type='text/javascript' src='<%=this.ResolveClientUrl("~/Plugin/ColorBox/jquery.colorbox.js") %>'><" + "/script>");
            document.write("<link rel='stylesheet' type='text/css' href='<%=this.ResolveClientUrl("~/Plugin/ColorBox/colorbox.css") %>'" + "/>");
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".cbIFrame").colorbox({
                iframe: true,
                rel: 'Default',
                width: "800px", height: "90%",
                rel: "nofollow"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ucColorBox ID="ucColorBox1" runat="server" ColorBoxPhotoName="cbPhoto"/>
    <uc3:ucLoader ID="ucLoader1" runat="server" />

    <div style="display:table;width:100%;vertical-align:top;">
        <div style="display:table-cell;vertical-align:top;">
            <div class="<%=(clsSecurity.LoginChecker("Admin") ? "dvContent" : "dvContentNormal") %>">
                <asp:Label ID="lblAdminMenu" runat="server" />
                <div id="dvHeader">
                    <div style="float:left;margin-right:10px;">
                        <asp:Label ID="lblIcon" runat="server" />
                    </div>
                    <div style="float:left;">
                        <div style="text-align:left;">
                            <div>
                                <asp:Label ID="lblName" runat="server"/>
                            </div>
                            <div>
                                <asp:Label ID="lblDetail" runat="server"/>
                            </div>
                            <div style="padding-top:5px;">
                                <div class="share42init"></div>
                                <script type="text/javascript" src="/Plugin/share42/share42.js"></script>
                                <!-- AddThis Button BEGIN 
                                <div class="addthis_toolbox addthis_default_style ">
                                    <a class="addthis_button_preferred_1"></a>
                                    <a class="addthis_button_preferred_2"></a>
                                    <a class="addthis_button_preferred_3"></a>
                                    <a class="addthis_button_preferred_4"></a>
                                    <a class="addthis_button_compact"></a>
                                    <a class="addthis_counter addthis_bubble_style"></a>
                                </div>
                                <script type="text/javascript">                                    var addthis_config = { "data_track_addressbar": false };</script>
                                <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=offduiclub"></script>
                                <!-- AddThis Button END -->
                            </div>
                        </div>
                    </div>
                    <div style="float:right;padding:5px;"></div>
                    <div style="clear:both;"></div>
                </div>
            </div>
            <asp:Panel ID="pnJCarousel" runat="server">
                <div class="<%=(clsSecurity.LoginChecker("Admin") ? "dvContent" : "dvContentNormal") %>">
                    <asp:Label ID="lblJCarouselAdminMenu" runat="server" />
                    <uc4:ucJCarousel ID="ucJCarousel1" runat="server" Visible="false"/>
                </div>
            </asp:Panel>
            <div id="dvContentMain">
                <asp:Label ID="lblContent" runat="server" />
            </div>
            
            <asp:Panel ID="pnPhotoGallery" runat="server" Visible="true">
                <div id="dvGallery" style="text-align:left;">
                    <div class="<%=(clsSecurity.LoginChecker("Admin") ? "dvContent" : "dvContentNormal") %>">
                        <asp:Label ID="lblAdminGalleryMenu" runat="server" />
                        <h4><asp:Label ID="lblGalleryHeader" runat="server" Text="ภาพที่เกี่ยวข้อง"/></h4>
                        <uc5:ucGallery ID="ucGallery1" runat="server" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div style="display:table-cell;vertical-align:top;width:250px;">
            <div id="dvContent">
                <div id="dvContentReference">
                    <div class="dvContentReferenceItem">
                        <h4><asp:Label ID="lblContactHeader" runat="server" Text="ติดต่อเรา"/></h4>
                        <div style="display:table;width:100%;">
                            <div class="dvContentRow">
                                <div class="dvContentName">
                                    <asp:Label ID="lblLocation" runat="server" Text="สถานที่ตั้ง" />
                                </div>
                                <div class="dvContentValue">
                                    <asp:Label ID="lblLocationValue" runat="server" />
                                </div>
                            </div>
                            <div class="dvContentRow">
                                <div class="dvContentName">
                                    <asp:Label ID="lblOfficeHours" runat="server" Text="วันเวลาทำการ" />
                                </div>
                                <div class="ContentValue">
                                    <asp:Label ID="lblOfficeHoursValue" runat="server" />
                                </div>
                            </div>
                            <div class="dvContentRow">
                                <div class="dvContentName">
                                    <asp:Label ID="lblPhone" runat="server" Text="โทรศัพท์" />
                                </div>
                                <div class="ContentValue">
                                    <asp:Label ID="lblPhoneValue" runat="server" />
                                </div>
                            </div>
                            <div class="dvContentRow">
                                <div class="dvContentName">
                                    <asp:Label ID="lblEMail" runat="server" Text="อีเมล์" />
                                </div>
                                <div class="ContentValue">
                                    <asp:Label ID="lblEMailValue" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="dvContentReferenceItem" style="margin-top:10px;border-top:1px solid #FFF;">
                        <h4><asp:Label ID="lblPriceHeader" runat="server" Text="ค่าบริการ"/></h4>
                        <asp:Label ID="lblPrice" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>