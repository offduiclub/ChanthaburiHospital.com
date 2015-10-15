<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="Highlight.aspx.cs" Inherits="Management_Highlight" %>

<%@ Register src="../UserControl/ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>
<%@ Register src="../UserControl/ucLoader/ucLoader.ascx" tagname="ucLoader" tagprefix="uc3" %>
<%@ Register src="../UserControl/ucGridView/ucGridViewTemplate.ascx" tagname="ucGridViewTemplate" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .dvPhotoFrame
        {
            border:1px solid #ddd;
            margin:5px;
            float:left;
            display:block;
            width:320px;
        }
        .dvPhotoFrameControl
        {
            padding:3px;
            background-color:#EEE;
            text-align:center;
        }
        .dvPhotoFramePhoto
        {
            border-bottom:1px solid #E6E6E6;
            width:320px;height:140px;
            overflow:hidden;float:left;margin-right:10px;
            background-color:#fff;
        }
        .dvPhotoFramePhoto img
        {
            filter:alpha(opacity=80);
            -moz-opacity:.80;opacity:.80;
        }
        .dvPhotoFramePhoto:hover img
        {
            filter:alpha(opacity=100);
            -moz-opacity:1;opacity:1;
        }
        .dvPhotoFrameName
        {
            background-color:#fafafa;
            padding:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" Runat="Server">
    <div style="float:left;margin-right:5px;">
        <img src="/Images/Icon/icMedicalCenter.png" alt="MedicalCenter Manage" title="MedicalCenter Manage" />
    </div>
    <div style="text-align:left;float:left;">
        <h1><asp:Label ID="lblHeader" runat="server" Text="ระบบจัดการไฮไลท์"/></h1>
        ระบบจัดการข้อมูลไฮไลท์ | <a href="/Management/Default.aspx">กลับสู่หน้าหลัก</a>
    </div>
    <div style="float:right;margin-top:17px;margin-right:9px;">
        
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ucColorBox ID="ucColorBox1" runat="server" UID="Child" ColorBoxIframeRefreshOnCloseName="cbIFrame" Height="90%" ColorBoxPhotoName="cbPhoto"/>
    <uc3:ucLoader ID="ucLoader1" runat="server" />
    <asp:Label ID="lblSQL" runat="server"></asp:Label>
    <asp:Label ID="lblDG" runat="server" />

    <uc4:ucGridViewTemplate ID="ucGridViewTemplate1" runat="server" />
    <div class="GridView">
        <asp:Panel ID="pnDGHeader" runat="server">
            <div class="GridViewHeader">
            <div style="float:right;">
                <asp:Button ID="btDGSubmit" runat="server" ValidationGroup="vgDGSubmit" OnClick="btDGSubmit_Click" CssClass="Button SaveTH" />
            </div>
            <h3 style="margin-left:90px;">
                Highlight</h3>
            <div style="clear:both;"></div>
        </div>
        <table cellpadding="0" cellspacing="0">
            <tr class="GridViewSubHeader">
                <td>
                    Photo<span class="Arrow"></span>
                </td>
            </tr>
            <tr class="GridViewItemNormal">
                <td align="center">
                    <asp:DataList ID="dlDefault" runat="server" BorderStyle="None" CellPadding="0" 
                        RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False" 
                        RepeatColumns="3" RepeatLayout="Flow" Visible="false" HorizontalAlign="Justify" Width="670px">
                        <ItemTemplate>
                            <div class="dvPhotoFrame">
                                <div class="dvPhotoFrameControl">
                                    <asp:Label ID="lblDGID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UID") %>' Visible="false"/>
                                    <asp:Label ID="lblDGName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GlobalName") %>' Visible="false" />
                                    <%--<asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(DataBinder.Eval(Container.DataItem, "Choose")).ToString()=="A"?true:false %>' ToolTip="เปิด/ปิด การแสดงผล"/>--%>
                                    <%--<asp:TextBox ID="txtDGSort" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "Sort"))%>' width="40px" CssClass="txtCenter"/>--%>
                                    <asp:CheckBox ID="cbDGActive" runat="server" Checked='<%#(DataBinder.Eval(Container.DataItem, "Choose")).ToString().ToLower()=="true"?true:false %>' Text="เลือก"/>
                                </div>
                                <div class="dvPhotoFramePhoto">
                                    <a href='<%#DataBinder.Eval(Container.DataItem,"Photo") %>' class="cbPhoto">
                                        <img src="<%#DataBinder.Eval(Container.DataItem, "Photo")%>" alt="<%#DataBinder.Eval(Container.DataItem,"Name") %>" title="<%#DataBinder.Eval(Container.DataItem,"Name") %>"/>
                                    </a>
                                </div>
                                <div class="dvPhotoFrameName">
                                    <%#DataBinder.Eval(Container.DataItem,"GlobalName") %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <div class="GridViewFooter">
            -
        </div>
        </asp:Panel>
    </div>
</asp:Content>