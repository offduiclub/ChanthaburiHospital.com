<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Group.ascx.cs" Inherits="Webboard_Group" %>

<link href='<%=this.ResolveClientUrl("CSS/cssWebboard.css") %>' rel="stylesheet" type="text/css" />

<asp:Label ID="lblDefault" runat="server" />
<asp:GridView ID="gvDefault" runat="server" 
    AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" 
    CellPadding="0" GridLines="None" Width="100%">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="WebboardList">
                    <div class="WebboardListItem">
                        <div class="WebboardListItemPhoto">
                            <a href="<%#DataBinder.Eval(Container.DataItem,"UID") %>/<%#clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem,"Name")) %>/">
                                <img src="<%#DataBinder.Eval(Container.DataItem,"Icon") %>" alt="<%#DataBinder.Eval(Container.DataItem,"Name") %>" style="min-widht:100%;min-height:100%;"/>
                            </a>
                        </div>
                        <div class="WebboardListItemDetail">
                            <a href="<%#DataBinder.Eval(Container.DataItem,"UID") %>/<%#clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem,"Name")) %>/">
                                <h3 style="float:left;">
                                    <%#DataBinder.Eval(Container.DataItem,"Name") %>
                                </h3>
                            </a>
                            <div style="float:right;text-align:right;visibility:<%#(clsSecurity.LoginGroup=="Admin"?"visible":"hidden")%>;">
                                <a href='<%#string.Format(webManage, WebboardTypeUID, DataBinder.Eval(Container.DataItem, "UID"), "Edit")%>' class="cbIFrame"><span class="Icon16 Edit"></span></a>
                                <a href='<%#string.Format(webManage, WebboardTypeUID, DataBinder.Eval(Container.DataItem, "UID"), "Delete")%>' onclick="return confirm('กดปุ่ม OK เพื่อยืนยันการลบข้อมูล')"><span class="Icon16 Delete"></span></a>
                            </div>
                            <div style="clear:both;">
                                <%#DataBinder.Eval(Container.DataItem,"Detail") %>
                            </div>
                        </div>
                        <div class="WebboardListItemCounter">
                            <span class="WebboardListQuestion" title="จำนวนคำถาม"><%#DataBinder.Eval(Container.DataItem,"QuestionCount") %></span>
                            <span class="WebboardListCounterSeparate">|</span>
                            <span class="WebboardListAnswer" title="จำนวนคำตอบ"><%#DataBinder.Eval(Container.DataItem,"AnswerCount") %></span>
                        </div>
                        <div class="WebboardListItemLastDetail">
                            <a href='<%#string.Format(webLastUpdate,DataBinder.Eval(Container.DataItem,"UID").ToString(),DataBinder.Eval(Container.DataItem,"QuestionLastUID").ToString(),clsDefault.URLRoutingFilter(DataBinder.Eval(Container.DataItem,"QuestionLastName").ToString())) %>'>
                                <%#DataBinder.Eval(Container.DataItem,"QuestionLastName") %>
                            </a>
                            <div style='font-size:8pt;margin-top:5px;<%#(DataBinder.Eval(Container.DataItem,"QuestionLastName")==DBNull.Value?"display:none;":"") %>'>
                                <span class='<%#(DataBinder.Eval(Container.DataItem, "QuestionLastCName")!=DBNull.Value?"UserAnonymous":"UserMember") %>'>
                                    <%#(DataBinder.Eval(Container.DataItem, "QuestionLastCName") != DBNull.Value ?
                                        DataBinder.Eval(Container.DataItem, "QuestionLastCName").ToString() : 
                                        DataBinder.Eval(Container.DataItem, "QuestionLastCUsername").ToString())%>
                                </span>
                                <span class="fontSeparate">|</span>
                                <span class="UserDate">
                                    <%#(
                                        DataBinder.Eval(Container.DataItem, "QuestionLastCWhen")!=DBNull.Value?
                                        DateTime.Parse(DataBinder.Eval(Container.DataItem,"QuestionLastCWhen").ToString()).ToString("dd/MM/yyyy HH:mm"):
                                        "")%>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>