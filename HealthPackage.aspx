<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HealthPackage.aspx.cs" Inherits="HealthPackage" %>

<%@ Register src="~/UserControl/ucLanguage/ucLanguageDB.ascx" tagname="ucLanguageDB" tagprefix="uc1" %>
<%@ Register src="~/UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>

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
            <asp:Label ID="lblIcon" runat="server" Text="<img src='/Images/Icon/icHealthPackage.png'/>"/>
        </div>
        <div style="float:left;">
            <div style="text-align:left;">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="<h1>แพคเกจตรวจสุขภาพ</h1>"/>
                </div>
                <div>
                    <asp:Label ID="lblDetail" runat="server" Text="ข้อมูลแพคเกจตรวจสุขภาพของโรงพยาบาล"/>
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
        <div style="float:right;padding:5px;">
            <asp:Panel ID="pnManage" runat="server" Visible="false">
                <a href="/HealthPackageManage.aspx" class="cbIFrame"><span class="Button AddTH"></span></a>
            </asp:Panel>
        </div>
        <div style="clear:both;">
        </div>
    </div>
    <div id="dvContent" style="margin-top:10px;">
        <div class="gvDefault">
            <asp:Label ID="lblDefault" runat="server" />
            <asp:GridView ID="gvDefault" runat="server" AutoGenerateColumns="false" 
                CssClass="width100" Width="100%" BorderStyle="None" GridLines="None" 
                ShowHeader="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="gvRow">
                                <div class="gvCell" style="width:350px;">
                                    <img src='<%#DataBinder.Eval(Container.DataItem,"PicThumbnail") %>' alt='<%#DataBinder.Eval(Container.DataItem,"Name") %>' title='<%#DataBinder.Eval(Container.DataItem,"Name") %>'/>
                                </div>
                                <div class="gvCell" style="width:100%;padding-left:10px;">
                                    <h3>
                                        <a href='/HealthPackage/<%#DataBinder.Eval(Container.DataItem,"UID") %>/<%#DataBinder.Eval(Container.DataItem,"Name") %>/'><%#DataBinder.Eval(Container.DataItem,"Name") %></a>
                                        <span style='display:<%#(clsSecurity.LoginChecker("admin")?"inline":"none") %>'>
                                            <a href="/HealthPackageManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=edit" class="cbIFrame"><span class="Icon16 Edit"></span></a>
                                            <a href="/HealthPackageManage.aspx?id=<%#DataBinder.Eval(Container.DataItem,"UID") %>&command=delete" onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
                                        </span>
                                    </h3>
                                    <div>
                                        <%#DataBinder.Eval(Container.DataItem,"DetailSub") %>
                                    </div>
                                    <div style="padding-top:10px;margin-top:10px;border-top:1px dashed #DDD;">
                                        <span class="Icon16 Calendar Normal"></span> เริ่ม <span style="color:#EC1F46;"><%#(DataBinder.Eval(Container.DataItem, "ActiveDateFrom").ToString()!=""?DateTime.Parse(DataBinder.Eval(Container.DataItem,"ActiveDateFrom").ToString()).ToString("dd/MM/yyyy"):"-")%></span><span style="color:#DDD;"> | </span>
                                        <span class="Icon16 Calendar Normal"></span> สิ้นสุด <span style="color:#EC1F46;"><%#(DataBinder.Eval(Container.DataItem, "ActiveDateTo").ToString() != "" ? DateTime.Parse(DataBinder.Eval(Container.DataItem, "ActiveDateTo").ToString()).ToString("dd/MM/yyyy") : "-")%></span>
                                    </div>
                                    <div class="dvPrice">
                                        ราคา <span style="color:#5D90B0;"><%#(double.Parse(DataBinder.Eval(Container.DataItem, "UnitPrice").ToString()).ToString("#,#.##")!=""?double.Parse(DataBinder.Eval(Container.DataItem,"UnitPrice").ToString()).ToString("#,#.##"):"-")%></span> บาท
                                        <span style="color:#ED1C24;font-size:8pt;"> * ขอสงวนสิทธิ์ในการเปลี่ยนแปลงราคาโดยไม่ต้องแจ้งให้ทราบล่วงหน้า</span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Panel ID="pnDetail" runat="server" Visible="false">
                <div>
                    <asp:Label ID="lblPhotoFull" runat="server" />
                </div>
                <div>
                    <asp:Label ID="lblContent" runat="server" />
                </div>
                <div style="padding-top:10px;margin-top:10px;border-top:1px dashed #DDD;">
                    <span class="Icon16 Calendar Normal"></span> เริ่ม <span style="color:#EC1F46;"><asp:Label ID="lblDateFrom" runat="server" /></span><span style="color:#DDD;"> | </span>
                    <span class="Icon16 Calendar Normal"></span> สิ้นสุด <span style="color:#EC1F46;"><asp:Label ID="lblDateTo" runat="server" /></span>
                </div>
                <div class="dvPrice">
                    ราคา <span style="color:#5D90B0;"><asp:Label ID="lblPrice" runat="server" /></span> บาท
                    <div style="color:#ED1C24;font-size:8pt;"> * ขอสงวนสิทธิ์ในการเปลี่ยนแปลงราคาโดยไม่ต้องแจ้งให้ทราบล่วงหน้า</div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>