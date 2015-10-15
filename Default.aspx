<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucSlider/ucSlider.ascx" tagname="ucSlider" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucOwlCarousel/ucOwlCarousel.ascx" tagname="ucOwlCarousel" tagprefix="uc3" %>
<%@ Register Src="~/UserControl/ucContent/ucContent.ascx" TagPrefix="uc1" TagName="ucContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href="/Plugin/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <style type="text/css">
        .HeadLine
        {
            padding:10px;
        }
        .HeadLine:hover
        {
            /*padding:9px;background-color:#FAFAFA;border:1px solid #DDD;*/
        }
        .HeadLine h5
        {
            color:#0072B8;
        }
        .HeadLine .HeadLinePhoto
        {
            width:320px;/*height:200px;*/overflow:hidden;border:1px solid #DDD;
        }
        .HeadLine .HeadLineContent
        {
            
        }
        .SearchControl
        {
            width:300px;
            border:1px solid #DDD;
            padding:3px;
            background-color:#FFF;
            font-family:Tahoma;
            font-size:10pt;
            color:#464646;
        }
        .SearchControlDropDownList
        {
            width:307px;
            border:1px solid #DDD;
            padding:3px;
            background-color:#FFF;
            font-family:Tahoma;
            font-size:10pt;
            color:#464646;
        }
        .dvContentReferenceItem
        {
            padding:0 5px 0 5px;
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
            /*border: 1px solid #F4F4F4;*/
        }
        .DoctorRow:hover
        {
            /*border: 1px solid #EAEAEA;
            background-color: #FAFAFA;*/
        }
        .DoctorCellPhoto
        {
            display: table-cell;
            width: 80px;
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
            width: 70px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="ucDefault"/>
    <uc1:ucColorBox ID="ucColorBox2" runat="server" UID="ucYoutube" ColorBoxIframeName="cbYoutube" Width="600px" Height="500px"/>
    <div id="dvSlide" style="padding-top:10px;padding-bottom:10px;">
        <uc2:ucSlider ID="ucSlider1" runat="server" Width="1000" Height="275"/>
    </div>
    <div style="table-layout: fixed;width:100%;">
        <uc3:ucOwlCarousel ID="ucOwlCarousel1" runat="server" />
    </div>
    <div>
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td colspan="2" style="width:66%;text-align:left;vertical-align:top;">
                    <%--<div><uc1:ucContent runat="server" ID="ucContent" ContentName="HomeYoutube"/></div>--%>
                    <div style="display:table;width:100%;text-align:left;">
                        <div style="display:table-row;width:100%;">
                            <div style="display:table-cell;width:50%;">
                                <div class="HeadLine" style="width:100%;">
                                    <asp:Label ID="lblPromotionHeader" runat="server" Text="<h4><i class='fa fa-gift'></i> โปรโมชั่น</h4>" />
                                    <div>
                                        <div class="HeadLinePhoto">
                                            <asp:Label ID="lblPromotionPhoto" runat="server" />
                                        </div>
                                        <div class="HeadLineContent">
                                            <asp:Label ID="lblPromotionName" runat="server" />
                                            <asp:Label ID="lblPromotionDetailSub" runat="server" />
                                            <span style=""><asp:Label ID="lblPromotionURL" runat="server" /></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="HeadLine" style="width:100%;">
                                    <asp:Label ID="lblNewsHeader" runat="server" Text="<h4><i class='fa fa-newspaper-o'></i> ข่าวประชาสัมพันธ์</h4>" />
                                    <div>
                                        <div class="HeadLinePhoto">
                                            <asp:Label ID="lblNewsPhoto" runat="server" />
                                        </div>
                                        <div class="HeadLineContent">
                                            <asp:Label ID="lblNewsName" runat="server" />
                                            <asp:Label ID="lblNewsDetailSub" runat="server" />
                                            <span style=""><asp:Label ID="lblNewsURL" runat="server" /></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="display:table-cell;width:50%;">
                                <div class="HeadLine" style="width:100%;">
                                    <asp:Label ID="lblDoctorNew" runat="server" Text="<h4><i class='fa fa-user-md'></i> แนะนำแพทย์ใหม่</h4>" />
                                    <div>
                                        <div class="dvContentReferenceItem" style="margin-top:10px;border-top:1px solid #FFF;">
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
                                                                            <b style="font-family:thaisans_neuebold,tahoma;font-size:14pt;"><%#DataBinder.Eval(Container.DataItem,"Name1") %></b>
                                                                        </a>
                                                                        <div style="padding-top:5px;font-size:8pt;">
                                                                            <div>
                                                                                <b><%=SpecialtyText %></b> : <%#DataBinder.Eval(Container.DataItem, "Specialty")%><br />
                                                                                <b><%=DepartmentText %></b> : <%#(Eval("Department")!=DBNull.Value?(Eval("Department").ToString().Length>3?Eval("Department").ToString().Substring(0,Eval("Department").ToString().Length-3):Eval("Department").ToString()):"")%>
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
                    </div>
                </td>
                <td style="width:34%;text-align:left;vertical-align:top;">
                    <div class="HeadLine">
                        <div style="">
                            <h4>
                                <span class='Icon24 Search Normal'></span>
                                <asp:Label ID="lblSearchHeader" runat="server" Text="ค้นหาข้อมูลแพทย์"/>
                            </h4>
                            <asp:Panel id="pnSearch" runat="server" Width="100%" DefaultButton="btSearch">
                                <div style="padding-top:10px;">
                                    <asp:Label ID="lblSearchName" runat="server" Text="ชื่อแพทย์" />
                                </div>
                                <asp:TextBox ID="txtSearchName" runat="server" CssClass="SearchControl"/>
                                <div style="">
                                    <asp:Label ID="lblSearchSpecialty" runat="server" Text="ความเชี่ยวชาญ" />
                                </div>
                                <asp:DropDownList ID="ddlSearchSpecialty" runat="server" CssClass="SearchControlDropDownList"/>
                                <div style="">
                                    <asp:Label ID="lblSearchMedicalCenter" runat="server" Text="ศูนย์/แผนก" />
                                </div>
                                <asp:DropDownList ID="ddlSearchMedicalCenter" runat="server" CssClass="SearchControlDropDownList"/>
                                <div style="">
                                    <asp:Label ID="lblSearchSchedule" runat="server" Text="วันที่ออกตรวจ" />
                                </div>
                                <div style="display:table;">
                                    <div style="display:table-cell;">
                                        <asp:CheckBoxList ID="cbSearchSchedule" runat="server" RepeatColumns="4" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">อาทิตย์</asp:ListItem>
                                            <asp:ListItem Value="2">จันทร์</asp:ListItem>
                                            <asp:ListItem Value="3">อังคาร</asp:ListItem>
                                            <asp:ListItem Value="4">พุธ</asp:ListItem>
                                            <asp:ListItem Value="5">พฤหัส</asp:ListItem>
                                            <asp:ListItem Value="6">ศุกร์</asp:ListItem>
                                            <asp:ListItem Value="7">เสาร์</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="display:table-cell;padding-left:5px;">
                                        <asp:Button ID="btSearch" runat="server" CssClass="Button SearchEN" 
                                            onclick="btSearch_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div style="margin:10px 0;">
                            <a href="/EnergySaving/index_ES.html" target="_blank">
                                <img src="/Images/mnEnergySaving.jpg" alt="EnergySaving" title="EnergySaving"/>
                            </a>
                        </div>
                        <div style="margin:10px 0;">
                            <a href="https://www.youtube.com/embed/UCySY3ZHQOI" class="cbYoutube">
                                <img src="/Images/mnYoutubeChannel.png" alt="YoutubeChannel" title="YoutubeChannel"/>
                            </a>
                        </div>
                        <div>
                            <iframe src="//www.facebook.com/plugins/likebox.php?href=https%3A%2F%2Fwww.facebook.com%2Fchanthaburihospital&amp;width=300&amp;height=258&amp;colorscheme=light&amp;show_faces=true&amp;header=false&amp;stream=false&amp;show_border=false" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:300px; height:258px;" allowTransparency="true"></iframe>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>