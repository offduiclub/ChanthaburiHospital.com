<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Answer.aspx.cs" Inherits="Webboard_Answer"%>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc2" %>
<%@ Register src="../UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link href='<%=this.ResolveClientUrl("CSS/cssWebboard.css") %>' rel="stylesheet" type="text/css" />
    <style type="text/css">
        .WebboardAnswerImportant
        {
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc2:ucColorBox ID="ucColorBox1" runat="server" UID="WebboardType" ColorBoxIframeName="cbIFrame"/>
    <uc3:ucLoader ID="ucLoader1" runat="server" />
    
    <div class="WebboardFrame" style="text-align:left;vertical-align:top;">
        <div class="WebboardFrameHeader" style="border-bottom:1px solid #DDD;">
            <div id="HeaderIcon">
                <asp:Image ID="imgHeaderIcon" runat="server" AlternateText="เว็บบอร์ด" ImageUrl="/Webboard/Images/icWebboard.png" Width="64px"/>
            </div>
            <div id="HeaderContent">
                <div id="HeaderName">
                    <h1>
                        <asp:Label ID="lblHeaderName" runat="server" Text="เว็บบอร์ด" />
                    </h1>
                </div>
                <div id="HeaderDetail">
                    <img src='<%=ResolveClientUrl("Images/icUser16.png") %>' style="height:15px;" alt="ผู้ตั้งคำถาม"/>
                    <asp:Label ID="lblUser" runat="server" Text="ผู้ตั้งคำถาม" />
                    <img src="<%=ResolveClientUrl("Images/icClock16.png") %>" style="height:15px;padding-left:10px;" alt="เวลา"/>
                    <asp:Label ID="lblCWhen" runat="server" Text="เวลา" />
                    <img src="<%=ResolveClientUrl("Images/icPin16.png") %>" style="height:15px;padding-left:10px;" alt="IP Address"/>
                    <asp:Label ID="lblIPAddress" runat="server" Text="IP Address" />
                    <asp:Label ID="lblStatus" runat="server" />
                    <span class="fontSeparate">|</span>
                    <a href="<%=webDefault %>">
                        กลับหน้าหลัก
                    </a>
                </div>
            </div>
            <div style="float:right;text-align:right;<%=(clsSecurity.LoginGroup=="Admin"?"display:inline;":"display:none;") %>">
                <a href='<%=string.Format(webManageCommandQuestion, clsDefault.URLRouting("group"), clsDefault.URLRouting("id"), "Edit")%>' class="cbIFrame"><span class="Icon16 Edit"></span></a>
                <a href='<%=string.Format(webManageCommandQuestion, clsDefault.URLRouting("group"), clsDefault.URLRouting("id"), "Delete")%>' onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
            </div>
            <div id="HeaderCommand">
                <a href='<%=string.Format(webManageAdd,clsDefault.URLRouting("group"),clsDefault.URLRouting("id")) %>' class="cbIFrame" title="Question"><div class="ButtonWebboard AnswerEN"></div></a>
            </div>
            <div style="clear:both;"></div>
        </div>
        <asp:Label ID="lblDetail" runat="server" />
    </div>
    <div>
        <asp:Label ID="lblDefault" runat="server" />
        <asp:GridView ID="gvDefault" runat="server" 
            AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" 
            CellPadding="0" GridLines="None" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="WebboardFrame" style="text-align:left;vertical-align:top;">
                            <div class="WebboardListHeader" style="position:relative;">
                                <div class="WebboardListTag" style='<%#(DataBinder.Eval(Container.DataItem,"Status").ToString()=="I"?"":"display:none;") %>'></div>
                                <div class="WebboardListHeaderPhoto">
                                    <img src='<%#(DataBinder.Eval(Container.DataItem,"UserPhoto")!=DBNull.Value?DataBinder.Eval(Container.DataItem,"UserPhoto").ToString():"/Webboard/Images/icUser.png") %>' alt=""/>
                                </div>
                                <div class="WebboardListHeaderContent">
                                    <b><%#(DataBinder.Eval(Container.DataItem, "Username") != DBNull.Value ? "<span class='UserMember'>"+DataBinder.Eval(Container.DataItem, "Username").ToString()+"</span>" : DataBinder.Eval(Container.DataItem, "CName"))%></b>
                                    <span class="fontSeparate">|</span>
                                    <img src="<%=ResolveClientUrl("Images/icClock16.png") %>" style="height:15px;" alt="เวลา"/>
                                    <%#DataBinder.Eval(Container.DataItem,"CWhen") %>
                                    <img src="<%=ResolveClientUrl("Images/icPin16.png") %>" style="height:15px;padding-left:10px;" alt="IP Address"/>
                                    <%#DataBinder.Eval(Container.DataItem,"CIPAddress") %>
                                </div>
                                <div class="WebboardListHeaderCommand" style="<%=(clsSecurity.LoginGroup=="Admin"?"":"display:none;") %>">
                                    <%#(DataBinder.Eval(Container.DataItem,"Active").ToString()=="0"?" <span style='font-weight:normal;padding-left:10px;'><span class='Icon16 Warn Normal'></span> Hidden</span>":"") %>
                                    <a href='<%#string.Format(webManageCommand,clsDefault.URLRouting("group"), clsDefault.URLRouting("id"), DataBinder.Eval(Container.DataItem, "UID"), "Edit")%>' class="cbIFrame"><span class="Icon16 Edit"></span></a>
                                    <a href='<%#string.Format(webManageCommand,clsDefault.URLRouting("group"), clsDefault.URLRouting("id"), DataBinder.Eval(Container.DataItem, "UID"), "Delete")%>' onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
                                </div>
                                <div style="clear:both;"></div>
                            </div>
                            <div>
                                <div style='text-align:center;padding:10px;background-color:#fcfcfc;border-bottom:1px dashed #EEE;<%#(DataBinder.Eval(Container.DataItem,"Photo").ToString()!=""?"":"display:none;") %>'>
                                    <img src='<%#DataBinder.Eval(Container.DataItem,"Photo") %>' alt='Answer'/>
                                </div>
                                <div style="padding:10px;">
                                    <%#DataBinder.Eval(Container.DataItem,"Detail") %>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>