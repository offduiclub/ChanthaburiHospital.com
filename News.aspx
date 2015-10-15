<%@ Page Title="ข่าวประชาสัมพันธ์ โรงพยาบาลกรุงเทพจันทบุรี" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register Src="~/UserControl/ucLanguage/ucLanguageDB.ascx" TagPrefix="uc1" TagName="ucLanguageDB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .width100
        {
            width:100%;
        }
        .gvDefault
        {
            display:table;
            width:100%;
        }
        .gvRow
        {
            display:table-row;
            font-weight:normal;
        }
        .gvRow:hover
        {
            background-color:#FAFAFA;
        }
        .gvCell
        {
            display:table-cell;
            padding:3px;
            text-align:left;
            vertical-align:top;
        }
        .dvPrice
        {
            font-family:thaisans_neuebold,tahoma;
            font-size:14pt;
            color:#686868;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ucColorBox ID="ucColorBox1" runat="server" UID="HealthPackage" ColorBoxIframeName="cbIFrame"/>
    <div id="dvHeader" runat="server">
        <div style="float:left;margin-right:10px;">
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/icNews.png'/>" meta:resourcekey="lblIconResource1"/>
        </div>
        <div style="float:left;width:800px;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>News</h1>" meta:resourcekey="lblNameResource1"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="News" meta:resourcekey="lblDetailResource1"/>
                </div>
                <div style="padding-top:5px;">
                    <div class="share42init"></div>
                    <script type="text/javascript" src="/Plugin/share42/share42.js"></script>
                </div>
            </div>
        </div>
        <div style="float:right;padding:5px;">
            <asp:Panel ID="pnManage" runat="server" Visible="False" meta:resourcekey="pnManageResource1">
                <a href="/NewsManage.aspx" class="cbIFrame"><span class="Button AddTH"></span></a>
            </asp:Panel>
        </div>
        <div style="clear:both;">
        </div>
    </div>
    <div id="dvContent" style="margin-top:10px;">
        <div class="gvDefault">
            <asp:Label ID="lblDefault" runat="server" meta:resourcekey="lblDefaultResource1" />
            <asp:GridView ID="gvDefault" runat="server" AutoGenerateColumns="False" 
                CssClass="width100" Width="100%" BorderStyle="None" GridLines="None" 
                ShowHeader="False" meta:resourcekey="gvDefaultResource1">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <div class="gvRow" style='background-color:<%# (AnonymousViewChecker(DataBinder.Eval(Container.DataItem,"ActiveDateFrom").ToString(),DataBinder.Eval(Container.DataItem,"ActiveDateTo").ToString(),DataBinder.Eval(Container.DataItem,"StatusFlag").ToString())?"#FFF":"#DDD;") %>'>
                                <div class="gvCell" style="width:350px;">
                                    <img src='<%# DataBinder.Eval(Container.DataItem,"PicThumbnail") %>' alt='<%# DataBinder.Eval(Container.DataItem,"Subject") %>' title='<%# DataBinder.Eval(Container.DataItem,"Subject") %>'/>
                                </div>
                                <div class="gvCell" style="width:100%;padding-left:10px;">
                                    <h3>
                                        <a href='/News/<%# DataBinder.Eval(Container.DataItem,"UID") %>/<%# clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem,"Subject")) %>/'><%#DataBinder.Eval(Container.DataItem, "Subject")%></a>
                                        <span style='display:<%# (clsSecurity.LoginChecker("admin")?"inline":"none") %>'>
                                            <a href='/NewsManage.aspx?id=<%# DataBinder.Eval(Container.DataItem,"UID") %>&amp;command=edit' class="cbIFrame"><span class="Icon16 Edit"></span></a>
                                            <a href='/NewsManage.aspx?id=<%# DataBinder.Eval(Container.DataItem,"UID") %>&amp;command=delete' onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
                                        </span>
                                    </h3>
                                    <div>
                                        <%#DataBinder.Eval(Container.DataItem,"DetailSub") %>
                                    </div>
                                    <div style="padding-top:10px;margin-top:10px;border-top:1px dashed #DDD;">
                                        <span class="Icon16 Calendar Normal"></span> เริ่ม <span style="color:#EC1F46;"><%#(DataBinder.Eval(Container.DataItem, "ActiveDateFrom").ToString()!=""?DateTime.Parse(DataBinder.Eval(Container.DataItem,"ActiveDateFrom").ToString()).ToString("dd/MM/yyyy"):"-")%></span><span style="color:#DDD;"> | </span>
                                        <span class="Icon16 Calendar Normal"></span> สิ้นสุด <span style="color:#EC1F46;"><%#(DataBinder.Eval(Container.DataItem, "ActiveDateTo").ToString() != "" ? DateTime.Parse(DataBinder.Eval(Container.DataItem, "ActiveDateTo").ToString()).ToString("dd/MM/yyyy") : "-")%></span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Panel ID="pnDetail" runat="server" Visible="False" meta:resourcekey="pnDetailResource1">
                <div>
                    <asp:Label ID="lblPhotoFull" runat="server" meta:resourcekey="lblPhotoFullResource1" />
                </div>
                <div>
                    <asp:Label ID="lblContent" runat="server" meta:resourcekey="lblContentResource1" />
                </div>
                <div style="padding-top:10px;margin-top:10px;border-top:1px dashed #DDD;">
                    <span class="Icon16 Calendar Normal"></span> เริ่ม <span style="color:#EC1F46;"><asp:Label ID="lblDateFrom" runat="server" meta:resourcekey="lblDateFromResource1" /></span><span style="color:#DDD;"> | </span>
                    <span class="Icon16 Calendar Normal"></span> สิ้นสุด <span style="color:#EC1F46;"><asp:Label ID="lblDateTo" runat="server" meta:resourcekey="lblDateToResource1" /></span>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>