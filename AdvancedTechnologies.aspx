<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdvancedTechnologies.aspx.cs" Inherits="AdvancedTechnologies" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="~/UserControl/ucContent/ucContent.ascx" tagname="ucContent" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagPrefix="uc1" TagName="ucLanguageDB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div id="dvHeader">
        <div style="float:left;margin-right:10px;">
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/Icon/icService.png'/>" meta:resourcekey="lblIconResource1"/>
        </div>
        <div style="float:left;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>Advanced Technology</h1>" meta:resourcekey="lblNameResource1"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="Advanced Technology" meta:resourcekey="lblDetailResource1"/>
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
    <div style="margin-top:10px;text-align:left;">
        <uc1:ucContent ID="ucContent2" runat="server" ContentName="AdvancedTechnologies"/>
    </div>
</asp:Content>