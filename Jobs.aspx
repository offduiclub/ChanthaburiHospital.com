<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Jobs.aspx.cs" Inherits="Jobs" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="UserControl/ucContent/ucContent.ascx" tagname="ucContent" tagprefix="uc1" %>
<%@ Register src="UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagPrefix="uc1" TagName="ucLanguageDB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .dvJobsMain
        {
            font-family:thaisans_neuebold,tahoma;
            font-size:12pt;
            text-align:left;
            margin-right:10px;
            padding:5px;
            cursor:pointer;
        }
        .dvJobsMain:hover
        {
            border:1px solid #DDD;
            padding:4px;
            background-color:#FAFAFA;
        }
        .gvWidth100
        {
            width:100%;
            font-weight:normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ucColorBox ID="ucColorBox1" runat="server" UID="Jobs" ColorBoxIframeName="cbJobDetail" Width="500px"/>
    <div id="dvHeader">
        <div style="float:left;margin-right:10px;">
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/Icon/icJobs.png'/>" meta:resourcekey="lblIconResource1"/>
        </div>
        <div style="float:left;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>ร่วมงานกับเรา</h1>" meta:resourcekey="lblNameResource1"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="ตำแหน่งงานว่างของโรงพยาบาลกรุงเทพจันทบุรี" meta:resourcekey="lblDetailResource1"/>
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
                    <script type="text/javascript">                        var addthis_config = { "data_track_addressbar": false };</script>
                    <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=offduiclub"></script>
                    <!-- AddThis Button END -->
                </div>
            </div>
        </div>
        <div style="float:right;padding:5px;"></div>
        <div style="clear:both;">
        </div>
    </div>
    <div style="margin:10px 0 10px 0;">
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td style="text-align:left;vertical-align:top;">
                    <asp:GridView ID="gvDefault" runat="server" AutoGenerateColumns="False" 
                        ShowHeader="False" Width="100%" CellPadding="0" 
                        GridLines="None" meta:resourcekey="gvDefaultResource1">
                        <Columns>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <div class="dvJobsMain">
                                        <div style="float:left;"><img src="/Images/logo24.png" style="width:24px;"/> <%#DataBinder.Eval(Container.DataItem,"Name") %></div>
                                        <div style="float:right;border-left:1px solid #DDD;padding-left:5px;margin-left:5px;"><a href='/JobsDetail/<%# DataBinder.Eval(Container.DataItem,"UID") %>/<%# DataBinder.Eval(Container.DataItem,"Name") %>/' class="cbJobDetail">รายละเอียด</a> </div>
                                        <div style="float:right;"><a href='/Jobs/<%# DataBinder.Eval(Container.DataItem,"UID") %>/<%# DataBinder.Eval(Container.DataItem,"Name") %>/'>กรอกใบสมัคร</a> </div>
                                        <div style="clear:both;"></div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td style="text-align:left;vertical-align:top;width:400px;">
                    <div class="RoundCorner" style="border:1px solid #DDD;background-color:#FAFAFA;text-align:left;vertical-align:top;">
                        <uc1:ucContent ID="ucContent2" runat="server" ContentName="Jobs"/>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>