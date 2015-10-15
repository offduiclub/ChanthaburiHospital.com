<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Webboard_Default" %>

<%@ Register src="Group.ascx" tagname="Group" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register src="../UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href='<%=this.ResolveClientUrl("CSS/cssWebboard.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ucColorBox ID="ucColorBox1" runat="server" UID="WebboardType" ColorBoxIframeName="cbIFrame"/>
    <uc3:ucLoader ID="ucLoader1" runat="server" />

    <div id="Header">
        <div id="HeaderIcon">
            <asp:Image ID="imgHeaderIcon" runat="server" AlternateText="เว็บบอร์ด" ImageUrl="/Webboard/Images/icWebboard.png" />
        </div>
        <div id="HeaderContent">
            <div id="HeaderName">
                <h2>
                    <asp:Label ID="lblHeaderName" runat="server" Text="เว็บบอร์ด" />
                </h2>
            </div>
            <div id="HeaderDetail">
                <asp:Label ID="lblHeaderDetail" runat="server" Text="เว็บบอร์ดตอบปัญหา" />
            </div>
        </div>
        <div id="HeaderCommand" style="text-align:right;visibility:<%=(clsSecurity.LoginGroup=="Admin"?"visible":"hidden")%>;">
            <a href='<%=webManageAdd%>' class="cbIFrame" title="Type : เพิ่ม"><div class="Button AddTH"></div></a>
            <div style="padding-top:5px;">
                <a href="/Webboard/WebboardConfig.aspx" class="cbIFrame"><div class="Icon16 Config"></div> Config</a>
                <span class="fontSeparate">|</span>
                <a href="/Webboard/WebboardReservedWords.aspx" class="cbIFrame"><div class="Icon16 Warn"></div> Reserved Words</a>
            </div>
        </div>
        <div style="clear:both;">
        </div>
    </div>
    <div>
        <asp:Label ID="lblDefault" runat="server" />
        <asp:GridView ID="gvDefault" runat="server" 
            AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" 
            CellPadding="0" GridLines="None" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="WebboardFrame">
                            <div class="WebboardFrameHeader">
                                <div class="WebboardFrameHeaderName">
                                    <img src="Images/icWebboard.png" alt="ประเภทเว็บบอร์ด" style="width:20px;"/> <%#DataBinder.Eval(Container.DataItem,"Name") %>
                                    <%#(DataBinder.Eval(Container.DataItem,"Active").ToString()=="0"?" <span style='font-weight:normal;padding-left:10px;'><span class='Icon16 Warn Normal'></span> Hidden</span>":"") %>
                                </div>
                                <div class="WebboardFrameHeaderCommand" style="visibility:<%#(clsSecurity.LoginGroup=="Admin"?"visible":"hidden")%>;">
                                    <a href='<%#string.Format(webGroupManage, DataBinder.Eval(Container.DataItem, "UID"))%>' class="cbIFrame" title="Group : เพิ่ม"><span class="Icon16 Add"></span></a>
                                    <a href='<%#string.Format(webManageCommand,DataBinder.Eval(Container.DataItem,"UID"),"Edit") %>' class="cbIFrame" title="Type : แก้ไข"><span class="Icon16 Edit"></span></a>
                                    <a href='<%#string.Format(webManageCommand,DataBinder.Eval(Container.DataItem,"UID"),"Delete") %>' title="Type : ลบ" onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
                                </div>
                                <div style="clear:both;"></div>
                            </div>
                            <div class="WebboardFrameContent">
                                <uc1:Group ID="Group1" runat="server" WebboardTypeUID='<%#DataBinder.Eval(Container.DataItem,"UID").ToString() %>'/>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>