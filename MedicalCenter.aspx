<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MedicalCenter.aspx.cs" Inherits="MedicalCenter" %>

<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>
<%@ Register src="~/UserControl/ucGallery/ucGallery.ascx" tagname="ucGallery" tagprefix="uc4" %>

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
        
        .DoctorTable
        {
            display: table;
            width: 100%;
            font-size:9pt;
        }
        .DoctorRow
        {
            display: table-row;
            border: 1px solid #F4F4F4;
        }
        .DoctorRow:hover
        {
            border: 1px solid #EAEAEA;
            background-color: #FAFAFA;
        }
        .DoctorCellPhoto
        {
            display: table-cell;
            width: 60px;
            text-align: left;
            vertical-align: top;
        }
        .DoctorCellDetail
        {
            padding: 5px;
            display: table-cell;
            width: 100%;
            text-align: left;
            vertical-align: top;
        }
        .DoctorCellDetail .Separate
        {
            color: #DDD;
            padding: 0 10px;
        }
        
        .DoctorPhoto img
        {
            width: 50px;
            border: 1px solid #DDD;
            cursor: pointer;
            padding: 4px;
            box-shadow: 0 0 3px #E5E5E5;
            -moz-box-shadow: 0 0 3px #E5E5E5;
            -webkit-box-shadow: 0 0 3px #E5E5E5;
            filter: alpha(opacity=100);
            -moz-opacity: 1;
            opacity: 1;
        }
        .DoctorPhoto:hover img
        {
            filter: alpha(opacity=80);
            -moz-opacity: .8;
            opacity: .8;
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
                fixed: true,
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
                                <script type="text/javascript">                    var addthis_config = { "data_track_addressbar": false };</script>
                                <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=offduiclub"></script>
                                <!-- AddThis Button END -->
                            </div>
                        </div>
                    </div>
                    <div style="float:right;padding:5px;"></div>
                    <div style="clear:both;"></div>
                </div>
                <div id="dvContentMain">
                    <asp:Label ID="lblContent" runat="server" />
                </div>
            </div>
            <div id="dvGallery" style="text-align:left;">
                <div class="<%=(clsSecurity.LoginChecker("Admin") ? "dvContent" : "dvContentNormal") %>">
                    <asp:Label ID="lblAdminGalleryMenu" runat="server" />
                    <h4><asp:Label ID="lblGalleryHeader" runat="server" Text="ภาพที่เกี่ยวข้อง"/></h4>
                    <asp:Label ID="lblGallery" runat="server"/>
                    <uc4:ucGallery ID="ucGallery1" runat="server" />
                </div>
            </div>
        </div>
        <div style="display:table-cell;vertical-align:top;width:250px;">
            <div id="dvContent">
                <div id="dvContentReference">
                    <div class="dvContentReferenceItem">
                        <h4><img src="/Images/icContact.png" alt="Contact"/> <asp:Label ID="lblContactHeader" runat="server" Text="ติดต่อเรา"/></h4>
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
                        <h4><img src="/Images/icPromotion.png" alt="Promotion"/> <asp:Label ID="lblPromotionHeader" runat="server" Text="โปรโมชันเพื่อสุขภาพ"/></h4>
                        <div class="DoctorTable">
                            <asp:Label ID="lblPromotion" runat="server" />
                            <asp:GridView ID="gvPromotion" runat="server" ShowHeader="false" 
                                ShowFooter="false" AutoGenerateColumns="False" BorderStyle="None" 
                                CellPadding="0" GridLines="None" CssClass="DoctorTable">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="DoctorRow">
                                                <div class="DoctorCellPhoto">
                                                    <div class="DoctorPhoto">
                                                        <a href="/Promotion/<%#DataBinder.Eval(Container.DataItem, "UID")%>/<%#clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "PromotionName").ToString())%>/">
                                                            <img src='<%#DataBinder.Eval(Container.DataItem,"PicThumbnail") %>' alt='<%#DataBinder.Eval(Container.DataItem,"PromotionName") %>' title='<%#DataBinder.Eval(Container.DataItem,"PromotionName") %>'/>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="DoctorCellDetail">
                                                    <a href="/Promotion/<%#DataBinder.Eval(Container.DataItem, "UID")%>/<%#clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "PromotionName").ToString())%>/">
                                                        <b><%#DataBinder.Eval(Container.DataItem, "PromotionName")%></b>
                                                    </a>
                                                    <div style="padding-top:5px;font-size:8pt;">
                                                        <%#DataBinder.Eval(Container.DataItem, "DetailSub")%>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPager" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="dvContentReferenceItem" style="margin-top:10px;border-top:1px solid #FFF;">
                        <h4><img src="/Images/icPackage.png" alt="Package"/> <asp:Label ID="lblPackageHeader" runat="server" Text="แพคเกจตรวจสุขภาพ"/></h4>
                        <div class="DoctorTable">
                            <asp:Label ID="lblPackage" runat="server" />
                            <asp:GridView ID="gvPackage" runat="server" ShowHeader="false" 
                                ShowFooter="false" AutoGenerateColumns="False" BorderStyle="None" 
                                CellPadding="0" GridLines="None" CssClass="DoctorTable">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="DoctorRow">
                                                <div class="DoctorCellPhoto">
                                                    <div class="DoctorPhoto">
                                                        <a href="/Package/<%#DataBinder.Eval(Container.DataItem, "UID")%>/<%#clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "PackageName").ToString())%>/">
                                                            <img src='<%#DataBinder.Eval(Container.DataItem,"PicThumbnail") %>' alt='<%#DataBinder.Eval(Container.DataItem,"PackageName") %>' title='<%#DataBinder.Eval(Container.DataItem,"PackageName") %>'/>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="DoctorCellDetail">
                                                    <a href="/Package/<%#DataBinder.Eval(Container.DataItem, "UID")%>/<%#clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "PackageName").ToString())%>/">
                                                        <b><%#DataBinder.Eval(Container.DataItem,"PackageName") %></b>
                                                    </a>
                                                    <div style="padding-top:5px;font-size:8pt;">
                                                        <%#DataBinder.Eval(Container.DataItem, "DetailSub")%>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPager" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="dvContentReferenceItem" style="margin-top:10px;border-top:1px solid #FFF;">
                        <h4><img src="/Images/icDoctor.png" alt="Doctor"/> <asp:Label ID="lblDoctorHeader" runat="server" Text="แพทย์ประจำศูนย์"/></h4>
                        <div class="DoctorTable">
                            <asp:Label ID="lblDoctor" runat="server" />
                            <asp:GridView ID="gvDoctor" runat="server" ShowHeader="false" 
                                ShowFooter="false" AutoGenerateColumns="False" BorderStyle="None" 
                                CellPadding="0" GridLines="None" CssClass="DoctorTable">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="DoctorRow">
                                                <div class="DoctorCellPhoto">
                                                    <div class="DoctorPhoto">
                                                        <a href="/DoctorSchedule/<%#DataBinder.Eval(Container.DataItem, "UID")%>/<%#(DataBinder.Eval(Container.DataItem, "DepartmentUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "DepartmentUID"):"0")%>/<%#(DataBinder.Eval(Container.DataItem, "MedicalCenterUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "MedicalCenterUID"):"0")%>/" class="cbIFrame">
                                                            <img src='<%#DoctorPhotoPath+(DataBinder.Eval(Container.DataItem,"Photo").ToString()==""?"default.jpg":DataBinder.Eval(Container.DataItem,"Photo")) %>' alt='<%#DataBinder.Eval(Container.DataItem,"Name1") %>' title='<%#DataBinder.Eval(Container.DataItem,"Name1") %>'/>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="DoctorCellDetail">
                                                    <a href="/DoctorSchedule/<%#DataBinder.Eval(Container.DataItem, "UID")%>/<%#(DataBinder.Eval(Container.DataItem, "DepartmentUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "DepartmentUID"):"0")%>/<%#(DataBinder.Eval(Container.DataItem, "MedicalCenterUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "MedicalCenterUID"):"0")%>/" class="cbIFrame">
                                                        <b><%#DataBinder.Eval(Container.DataItem,"Name1") %></b>
                                                    </a>
                                                    <div style="padding-top:5px;font-size:8pt;">
                                                        <div>
                                                            <b><%=SpecialtyText %></b> : <%#DataBinder.Eval(Container.DataItem, "Specialty")%><br />
                                                            <b><%=DepartmentText %></b> : <%#DataBinder.Eval(Container.DataItem, "Department")%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPager" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>