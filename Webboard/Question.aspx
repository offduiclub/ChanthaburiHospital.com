<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Question.aspx.cs" Inherits="Webboard_Question" %>

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
                <h1>
                    <asp:Label ID="lblHeaderName" runat="server" Text="เว็บบอร์ด" />
                </h1>
            </div>
            <div id="HeaderDetail">
                <asp:Label ID="lblHeaderDetail" runat="server" Text="เว็บบอร์ดตอบปัญหา" />
                <span class="fontSeparate">|</span>
                <a href="/Webboard">
                    กลับหน้าหลัก
                </a>
            </div>
        </div>
        <div id="HeaderCommand">
            <a href='<%=string.Format(webManageAdd,clsDefault.URLRouting("id")) %>' class="cbIFrame" title="Question">
                <div class="ButtonWebboard QuestionEN"></div>
            </a>
        </div>
        <div style="clear:both;"></div>
    </div>
    <div>
        <div class="WebboardFrame" id="dvImportant" runat="server">
            <div class="WebboardFrameHeader">
                <div class="WebboardFrameHeaderName">
                    <img src='<%=this.ResolveClientUrl("Images/icImportant.png") %>' alt="ปักหมุด" title="" style="width:20px;"/> 
                    <asp:Label ID="lblHeaderImportant" runat="server" Text="คำถามที่พบบ่อย" />
                </div>
                <div style="clear:both;"></div>
            </div>
            <div class="WebboardFrameContent">
                <asp:GridView ID="gvImportant" runat="server" 
                    AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" 
                    CellPadding="0" GridLines="None" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="WebboardList">
                                    <div class="WebboardListItem">
                                        <div class="WebboardListItemStatus">
                                            <a href="<%#string.Format(webChild,clsDefault.URLRouting("id"),DataBinder.Eval(Container.DataItem, "UID").ToString(),clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "Name"))) %>">
                                                <img src='<%#this.ResolveClientUrl("Images/icImportant.png") %>' alt="คำถามที่พบบ่อย" title=""/>
                                            </a>
                                        </div>
                                        <div class="WebboardListItemDetail">
                                            <a href="<%#string.Format(webChild,clsDefault.URLRouting("id"),DataBinder.Eval(Container.DataItem, "UID").ToString(),clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "Name"))) %>">
                                                <b><%#DataBinder.Eval(Container.DataItem,"Name") %></b>
                                            </a>
                                            <%#(DataBinder.Eval(Container.DataItem,"Active").ToString()=="0"?" <span style='font-weight:normal;padding-left:10px;'><span class='Icon16 Warn Normal'></span> Hidden</span>":"") %>
                                            <div style="float:right;text-align:right;visibility:<%#(clsSecurity.LoginGroup=="Admin"?"visible":"hidden")%>;">
                                                <a href='<%#string.Format(webManageCommand, clsDefault.URLRouting("id"), DataBinder.Eval(Container.DataItem, "UID"), "Edit")%>' class="cbIFrame"><span class="Icon16 Edit"></span></a>
                                                <a href='<%#string.Format(webManageCommand, clsDefault.URLRouting("id"), DataBinder.Eval(Container.DataItem, "UID"), "Delete")%>' onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
                                            </div>
                                        </div>
                                        <div class="WebboardListItemUser">
                                            <%#(DataBinder.Eval(Container.DataItem, "Username") != DBNull.Value ? 
                                                "<span class='UserMember' title='Member'>"+DataBinder.Eval(Container.DataItem, "Username")+"</span>" : "<span class='UserAnonymous' title='None-Member'>"+DataBinder.Eval(Container.DataItem, "CName")+"</span>")%>
                                            <div class="UserDate" title="เริ่มเมื่อเวลา">
                                                <%#DateTime.Parse(DataBinder.Eval(Container.DataItem,"CWhen").ToString()).ToString("dd/MM/yyyy HH:mm") %>
                                            </div>
                                        </div>
                                        <div class="WebboardListItemUser">
                                            <%#(DataBinder.Eval(Container.DataItem, "AnswerCount").ToString()=="0"?"-":"")%>
                                            <%#(DataBinder.Eval(Container.DataItem, "AnswerUsername") != DBNull.Value ?
                                                "<span class='UserMember' title='Member'>" + DataBinder.Eval(Container.DataItem, "AnswerUsername") + "</span>" : "<span class='UserAnonymous' title='None-Member'>" + DataBinder.Eval(Container.DataItem, "AnswerCName") + "</span>")%>
                                            <div class="UserDate" title="ล่าสุดเมื่อเวลา">
                                                <%#(DataBinder.Eval(Container.DataItem, "AnswerMWhen")!=DBNull.Value?
                                                    DateTime.Parse(DataBinder.Eval(Container.DataItem, "AnswerMWhen").ToString()).ToString("dd/MM/yyyy HH:mm"):
                                                    "") %>
                                            </div>
                                        </div>
                                        <div class="WebboardListItemCounter">
                                            <span class="WebboardListQuestion" title="จำนวนเปิดอ่าน"><%#DataBinder.Eval(Container.DataItem, "Views")%></span>
                                            <span class="WebboardListCounterSeparate">|</span>
                                            <span class="WebboardListAnswer" title="จำนวนคำตอบ"><%#DataBinder.Eval(Container.DataItem, "AnswerCount")%></span>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="WebboardFrame">
            <div class="WebboardFrameHeader">
                <div class="WebboardFrameHeaderName">
                    <img src='<%=this.ResolveClientUrl("Images/icNormal.png") %>' alt="ทั่วไป" title="" style="width:20px;"/> 
                    <asp:Label ID="lblHeaderNormal" runat="server" Text="คำถามทั่วไป" />
                </div>
                <div style="clear:both;"></div>
            </div>
            <div class="WebboardFrameContent">
                <asp:Label ID="lblDefault" runat="server" />
                <asp:GridView ID="gvDefault" runat="server" 
                    AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" 
                    CellPadding="0" GridLines="None" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="WebboardList">
                                    <div class="WebboardListItem">
                                        <div class="WebboardListItemStatus">
                                            <a href="<%#string.Format(webChild,clsDefault.URLRouting("id"),DataBinder.Eval(Container.DataItem, "UID").ToString(),clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "Name"))) %>">
                                                <img src='<%#this.ResolveClientUrl("Images/icNormal.png") %>' alt="คำถามทั่วไป" title=""/>
                                            </a>
                                        </div>
                                        <div class="WebboardListItemDetail">
                                            <a href="<%#string.Format(webChild,clsDefault.URLRouting("id"),DataBinder.Eval(Container.DataItem, "UID").ToString(),clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem, "Name"))) %>">
                                                <%#DataBinder.Eval(Container.DataItem,"Name") %>
                                            </a>
                                            <%#(DataBinder.Eval(Container.DataItem,"Active").ToString()=="0"?" <span style='font-weight:normal;padding-left:10px;'><span class='Icon16 Warn Normal'></span> Hidden</span>":"") %>
                                            <div style="float:right;text-align:right;visibility:<%#(clsSecurity.LoginGroup=="Admin"?"visible":"hidden")%>;">
                                                <a href='<%#string.Format(webManageCommand, clsDefault.URLRouting("id"), DataBinder.Eval(Container.DataItem, "UID"), "Edit")%>' class="cbIFrame"><span class="Icon16 Edit"></span></a>
                                                <a href='<%#string.Format(webManageCommand, clsDefault.URLRouting("id"), DataBinder.Eval(Container.DataItem, "UID"), "Delete")%>' onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
                                            </div>
                                        </div>
                                        <div class="WebboardListItemUser">
                                            <%#(DataBinder.Eval(Container.DataItem, "Username") != DBNull.Value ? 
                                                "<span class='UserMember' title='Member'>"+DataBinder.Eval(Container.DataItem, "Username")+"</span>" : "<span class='UserAnonymous' title='None-Member'>"+DataBinder.Eval(Container.DataItem, "CName")+"</span>")%>
                                            <div class="UserDate" title="เริ่มเมื่อเวลา">
                                                <%#DateTime.Parse(DataBinder.Eval(Container.DataItem,"CWhen").ToString()).ToString("dd/MM/yyyy HH:mm") %>
                                            </div>
                                        </div>
                                        <div class="WebboardListItemUser">
                                            <%#(DataBinder.Eval(Container.DataItem, "AnswerCount").ToString()=="0"?"-":"")%>
                                            <%#(DataBinder.Eval(Container.DataItem, "AnswerUsername") != DBNull.Value ?
                                                "<span class='UserMember' title='Member'>" + DataBinder.Eval(Container.DataItem, "AnswerUsername") + "</span>" : "<span class='UserAnonymous' title='None-Member'>" + DataBinder.Eval(Container.DataItem, "AnswerCName") + "</span>")%>
                                            <div class="UserDate" title="ล่าสุดเมื่อเวลา">
                                                <%#(DataBinder.Eval(Container.DataItem, "AnswerMWhen")!=DBNull.Value?
                                                    DateTime.Parse(DataBinder.Eval(Container.DataItem, "AnswerMWhen").ToString()).ToString("dd/MM/yyyy HH:mm"):
                                                    "") %>
                                            </div>
                                        </div>
                                        <div class="WebboardListItemCounter">
                                            <span class="WebboardListQuestion" title="จำนวนเปิดอ่าน"><%#DataBinder.Eval(Container.DataItem, "Views")%></span>
                                            <span class="WebboardListCounterSeparate">|</span>
                                            <span class="WebboardListAnswer" title="จำนวนคำตอบ"><%#DataBinder.Eval(Container.DataItem, "AnswerCount")%></span>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>