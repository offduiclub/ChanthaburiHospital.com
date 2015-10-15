<%@ Page Title="ค้นหาและนัดหมายแพทย์" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DoctorSchedule.aspx.cs" Inherits="DoctorSchedule" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc2" %>
<%@ Register src="~/UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .dvContent, .dvContentNormal
        {
            padding: 10px;
            position: relative;
        }
        .dvContent:hover
        {
            padding: 9px;
            border: 1px solid #DDD;
            box-shadow: 3px 3px 5px #A3A3A3;
        }
        .dvContent:hover .dvContentMenu
        {
            visibility: visible;
        }
        .dvContentMenu
        {
            border-left: 1px solid #DDD;
            border-bottom: 1px solid #DDD;
            background-color: #FAFAFA;
            position: absolute;
            top: 0;
            right: 0;
            padding: 5px;
            visibility: hidden;
        }
        #dvContent
        {
            display: table;
            width: 100%;
        }
        #dvContentMain
        {
            padding: 20px 0px;
            text-align: left;
        }
        #dvContentReference
        {
            display: table-cell;
            width: 250px;
            padding: 5px;
            background-color: #F4F4F4;
            text-align: left;
        }
        .dvContentReferenceItem
        {
            padding: 0 5px 0 5px;
        }
        .dvContentRow
        {
            display: table-row;
            vertical-align: middle;
        }
        .dvContentName
        {
            display: table-cell;
            text-align: left;
            color: #326585;
            font-weight: bold;
            width: 100px;
        }
        .dvContentValue
        {
            display: table-cell;
            text-align: left;
        }
        .DoctorTable
        {
            display: table;
            width: 100%;
        }
        .DoctorRow
        {
            display: table-row;
            border: 1px solid #FFF;
        }
        .DoctorRow:hover
        {
            border: 1px solid #EAEAEA;
            background-color: #FAFAFA;
        }
        .DoctorCellPhoto
        {
            padding: 5px;
            display: table-cell;
            width: 120px;
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
        .DoctorCellDetail A:link
        {
            color: #236492;
            text-decoration: none;
        }
        .DoctorCellDetail A:visited
        {
            color: #236492;
            text-decoration: none;
        }
        .DoctorCellDetail A:active
        {
            color: #4DB5D3;
            text-decoration: none;
        }
        .DoctorCellDetail A:hover
        {
            color: #4DB5D3;
            text-decoration: none;
        }
        
        .DoctorPhoto img
        {
            width: 100px;
            border: 1px solid #DDD;
            cursor: pointer;
            padding: 7px;
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
        .DoctorSchedule table
        {
            font-size: 8pt;
            width: 100%;
            margin-top: 5px;
        }
        .DoctorScheduleHeader td
        {
            width: 14%;
            text-align: center;
            background-color: #EFEFEF;
            padding: 3px;
            border: 1px solid #DDD;
        }
        .DoctorScheduleHeader td:hover
        {
            background-color: #E5E5E5;
            cursor: pointer;
        }
        .DoctorScheduleHeader td .Day
        {
            padding-left: 5px;
            font-size: 7pt;
            color: #B4B4B4;
        }
        .DoctorScheduleItem td
        {
            text-align: center;
            background-color: #FFF;
            padding: 3px;
            border: 1px solid #DDD;
        }
        .DoctorScheduleItem td:hover
        {
            background-color: #FAFAFA;
            cursor: pointer;
        }
        .DoctorScheduleItem .DayActive
        {
            background-color: #80C8F5;
            color: #FFF;
        }
        .DoctorScheduleItem .DayActive:hover
        {
            background-color: #0072B8;
            color: #FFF;
        }
        .SearchControl
        {
            width:230px;
            border:1px solid #DDD;
            padding:3px;
            background-color:#FFF;
            font-family:Tahoma;
            font-size:10pt;
            color:#464646;
        }
        .SearchControlDropDownList
        {
            width:237px;
            border:1px solid #DDD;
            padding:3px;
            background-color:#FFF;
            font-family:Tahoma;
            font-size:10pt;
            color:#464646;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" ColorBoxIframeName="cbIFrame"/>
    <uc3:ucLoader ID="ucLoader1" runat="server" />
    <div style="display:table;width:100%;vertical-align:top;">
        <div style="display:table-cell;vertical-align:top;">
            <div id="dvHeader">
                <div style="float:left;margin-right:10px;">
                    <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/Icon/icDoctorSchedule.png'/>" meta:resourcekey="lblIconResource1"/>
                </div>
                <div style="float:left;width:600px;">
                    <div style="text-align:left;">
                        <div>
                            <asp:Label ID="lblName" runat="server" Text="<h1>Find a Doctor and Appointment</h1>" meta:resourcekey="lblNameResource1"/>
                        </div>
                        <div>
                            <asp:Label ID="lblDetail" runat="server" Text="Find a Doctor and Appointment" meta:resourcekey="lblDetailResource1"/>
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
                             AddThis Button END -->
                        </div>
                    </div>
                </div>
                <div style="float:right;padding:5px;"></div>
                <div style="clear:both;">
                </div>
            </div>
            <div id="dvContentMain">
                <div class="DoctorTable">
                    <asp:Label ID="lblDoctor" runat="server" meta:resourcekey="lblDoctorResource1" />
                    <asp:GridView ID="gvDoctor" runat="server" ShowHeader="False" AutoGenerateColumns="False" BorderStyle="None" 
                        CellPadding="0" GridLines="None" CssClass="DoctorTable" AllowPaging="True" 
                        PageSize="20" OnPageIndexChanging="gvDoctor_PageIndexChanging" meta:resourcekey="gvDoctorResource1">
                        <Columns>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <div class="DoctorRow">
                                        <div class="DoctorCellPhoto">
                                            <div class="DoctorPhoto">
                                                <a href='/DoctorSchedule/<%# DataBinder.Eval(Container.DataItem, "UID") %>/<%# (DataBinder.Eval(Container.DataItem, "DepartmentUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "DepartmentUID"):"0") %>/<%# (DataBinder.Eval(Container.DataItem, "MedicalCenterUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "MedicalCenterUID"):"0") %>/' class="cbIFrame">
                                                    <img src='<%# DoctorPhotoPath+(DataBinder.Eval(Container.DataItem,"Photo").ToString()==""?"default.jpg":DataBinder.Eval(Container.DataItem,"Photo")) %>' alt='<%# DataBinder.Eval(Container.DataItem,"Name1") %>' title='<%# DataBinder.Eval(Container.DataItem,"Name1") %>'/>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="DoctorCellDetail">
                                            <div>
                                                <div style="float:left;">
                                                    <a href='/DoctorSchedule/<%# DataBinder.Eval(Container.DataItem, "UID") %>/<%# (DataBinder.Eval(Container.DataItem, "DepartmentUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "DepartmentUID"):"0") %>/<%# (DataBinder.Eval(Container.DataItem, "MedicalCenterUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "MedicalCenterUID"):"0") %>/' class="cbIFrame">
                                                        <h4><%#DataBinder.Eval(Container.DataItem,"Name1") %></h4>
                                                    </a>
                                                </div>
                                                <div style="float:right;font-size:8pt;">
                                                    <a href='/DoctorSchedule/<%# DataBinder.Eval(Container.DataItem, "UID") %>/<%# (DataBinder.Eval(Container.DataItem, "DepartmentUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "DepartmentUID"):"0") %>/<%# (DataBinder.Eval(Container.DataItem, "MedicalCenterUID").ToString()!=""?DataBinder.Eval(Container.DataItem, "MedicalCenterUID"):"0") %>/' class="cbIFrame" title="นัดหมายแพทย์">
                                                        <img src="/Images/Icon/icAppointment.png" style="width:16px;"/> Appointment
                                                    </a>
                                                </div>
                                                <div style="clear:both;"></div>
                                            </div>
                                            <div>
                                                <%#(Eval("Name2")!=DBNull.Value?(Eval("Name2").ToString().Length>7?Eval("Name2").ToString():"-"):"") %>
                                            </div>
                                            <div style="padding-top:5px;">
                                                <div>
                                                    <b><%=SpecialtyText %></b> : <%#DataBinder.Eval(Container.DataItem, "Specialty")%>
                                                    <span class="Separate">|</span>
                                                    <b><%=DepartmentText %></b> : <%#DataBinder.Eval(Container.DataItem, "Department")%>
                                                    <br />
                                                    <b><%=EducationText %></b> : <%#DataBinder.Eval(Container.DataItem, "Education")%>
                                                </div>
                                                <div class="DoctorSchedule">
                                                    <asp:Label ID="gvDoctorUID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UID") %>' Visible="False" meta:resourcekey="gvDoctorUIDResource1"/>
                                                    <asp:Label ID="gvDepartmentUID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentUID") %>' Visible="False" meta:resourcekey="gvDepartmentUIDResource1"/>
                                                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                                                        <tr class="DoctorScheduleHeader">
                                                            <td>
                                                                <%=DayText[0]%><span class="Day" title="วันที่">(<%=Day[0] %>)</span>
                                                            </td>
                                                            <td>
                                                                <%=DayText[1]%><span class="Day" title="วันที่">(<%=Day[1] %>)</span>
                                                            </td>
                                                            <td>
                                                                <%=DayText[2]%><span class="Day" title="วันที่">(<%=Day[2] %>)</span>
                                                            </td>
                                                            <td>
                                                                <%=DayText[3]%><span class="Day" title="วันที่">(<%=Day[3] %>)</span>
                                                            </td>
                                                            <td>
                                                                <%=DayText[4]%><span class="Day" title="วันที่">(<%=Day[4] %>)</span>
                                                            </td>
                                                            <td>
                                                                <%=DayText[5]%><span class="Day" title="วันที่">(<%=Day[5] %>)</span>
                                                            </td>
                                                            <td>
                                                                <%=DayText[6]%><span class="Day" title="วันที่">(<%=Day[6] %>)</span>
                                                            </td>
                                                        </tr>
                                                        <tr class="DoctorScheduleItem">
                                                            <td>
                                                                <asp:Label ID="gvSchedule0" runat="server" meta:resourcekey="gvSchedule0Resource1" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="gvSchedule1" runat="server" meta:resourcekey="gvSchedule1Resource1" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="gvSchedule2" runat="server" meta:resourcekey="gvSchedule2Resource1" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="gvSchedule3" runat="server" meta:resourcekey="gvSchedule3Resource1" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="gvSchedule4" runat="server" meta:resourcekey="gvSchedule4Resource1" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="gvSchedule5" runat="server" meta:resourcekey="gvSchedule5Resource1" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="gvSchedule6" runat="server" meta:resourcekey="gvSchedule6Resource1" />
                                                            </td>
                                                        </tr>
                                                    </table>
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
        <div style="display:table-cell;vertical-align:top;width:250px;">
            <div id="dvContent">
                <div id="dvContentReference">
                    <div class="dvContentReferenceItem">
                        <h4>
                            <span class='Icon24 Search Normal'></span>
                            <asp:Label ID="lblSearchHeader" runat="server" Text="Search" meta:resourcekey="lblSearchHeaderResource1"/>
                        </h4>
                        <asp:Panel id="pnSearch" runat="server" Width="100%" DefaultButton="btSearch" meta:resourcekey="pnSearchResource1">
                            <div style="padding-top:10px;">
                                <asp:Label ID="lblSearchName" runat="server" Text="DoctorName" meta:resourcekey="lblSearchNameResource1" />
                            </div>
                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="SearchControl" meta:resourcekey="txtSearchNameResource1"/>
                            <div style="">
                                <asp:Label ID="lblSearchSpecialty" runat="server" Text="Specialty" meta:resourcekey="lblSearchSpecialtyResource1" />
                            </div>
                            <asp:DropDownList ID="ddlSearchSpecialty" runat="server" CssClass="SearchControlDropDownList" meta:resourcekey="ddlSearchSpecialtyResource1"/>
                            <div style="">
                                <asp:Label ID="lblSearchMedicalCenter" runat="server" Text="MedicalCenter" meta:resourcekey="lblSearchMedicalCenterResource1" />
                            </div>
                            <asp:DropDownList ID="ddlSearchMedicalCenter" runat="server" CssClass="SearchControlDropDownList" meta:resourcekey="ddlSearchMedicalCenterResource1"/>
                            <div style="">
                                <asp:Label ID="lblSearchSchedule" runat="server" Text="Schedule" meta:resourcekey="lblSearchScheduleResource1" />
                            </div>
                            <asp:CheckBoxList ID="cbSearchSchedule" runat="server" RepeatColumns="4" 
                                RepeatDirection="Horizontal" meta:resourcekey="cbSearchScheduleResource1">
                                <asp:ListItem Value="1" meta:resourcekey="ListItemResource1">Sun</asp:ListItem>
                                <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">Mon</asp:ListItem>
                                <asp:ListItem Value="3" meta:resourcekey="ListItemResource3">Tue</asp:ListItem>
                                <asp:ListItem Value="4" meta:resourcekey="ListItemResource4">Wed</asp:ListItem>
                                <asp:ListItem Value="5" meta:resourcekey="ListItemResource5">Thu</asp:ListItem>
                                <asp:ListItem Value="6" meta:resourcekey="ListItemResource6">Fri</asp:ListItem>
                                <asp:ListItem Value="7" meta:resourcekey="ListItemResource7">Sat</asp:ListItem>
                            </asp:CheckBoxList>
                            <div style="text-align:right;padding-top:10px;">
                                <asp:Button ID="btSearch" runat="server" CssClass="Button SearchEN" 
                                    onclick="btSearch_Click" meta:resourcekey="btSearchResource1" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="dvContentReferenceItem" style="margin-top:10px;border-top:1px solid #FFF;display:none;">
                        <h4><asp:Label ID="lblOtherHeader" runat="server" Text="อะไรดี" meta:resourcekey="lblOtherHeaderResource1"/></h4>
                        <asp:Label ID="lblOther" runat="server" meta:resourcekey="lblOtherResource1" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>