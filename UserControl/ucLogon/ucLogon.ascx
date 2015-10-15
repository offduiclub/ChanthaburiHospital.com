<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLogon.ascx.cs" Inherits="UserControl_ucLogon_ucLogon" %>

<link href="<%=ResolveClientUrl("CSS/cssControl.css")%>" rel="stylesheet" type="text/css" />
<style type="text/css">
    .dvLogon
    {
        text-align:left;
        float:left;
        margin-right:5px;
    }
    .lblLogon
    {
        padding-top:5px;
        font-size:10pt;
        color:#565656;
        font-weight:bold;
    }
    .txtLogon
    {
        width:100px;
        padding:3px;
        border:1px solid #DDD;
        background-color:#FFF;
        color:#565656;
    }
    .fontValid
    {
        font-family:Tahoma;
        font-weight:normal;
        font-size:10pt;
        color:#FE4242;
    }
</style>
<div style="padding:5px;text-align:left;">
    <asp:Panel ID="pnLogin" runat="server" Visible="false" DefaultButton="btLogin">
        <div class="dvLogon lblLogon">
            USERNAME
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                Display="Dynamic" ErrorMessage=" โปรดกรอก" 
                ControlToValidate="txtUsername" CssClass="fontValid" ValidationGroup="vdLogon"></asp:RequiredFieldValidator>
        </div>
        <div class="dvLogon" style="">
            <asp:TextBox ID="txtUsername" runat="server" CssClass="txtLogon" TabIndex="1"/>
        </div>
        <div class="dvLogon lblLogon">
            PASSWORD
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtPassword" CssClass="fontValid" Display="Dynamic" 
                ErrorMessage=" โปรดกรอก" ValidationGroup="vdLogon"></asp:RequiredFieldValidator>
        </div>
        <div class="dvLogon">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtLogon" 
                TextMode="Password" TabIndex="2"/>
        </div>
        <div class="dvLogon" style="padding-top:5px;">
            <asp:CheckBox ID="cbEnableCookie" runat="server" Text="" 
                TextAlign="Left" TabIndex="3" ToolTip="จดจำข้อมูลการล็อคอินในครั้งต่อไป" />
        </div>
        <div class="dvLogon">
            <asp:Button ID="btLogin" runat="server" CssClass="Button LoginEN" 
                TabIndex="4" onclick="btLogin_Click" ValidationGroup="vdLogon" />
            <asp:Label ID="lblLogin" runat="server" />
        </div>
        <div style="clear:both;"></div>
        <div>
            <a href='<%=UrlRegister %>'>
                <asp:Label ID="lblRegister" runat="server" Text="สมัครสมาชิก" />
            </a>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnLogout" runat="server" Visible="false">
        <div title="Username" style="font-size:11pt;font-weight:bold;float:left;">
            <a href='<%=UrlProfile %>' title='group : <%=clsSecurity.LoginGroup %>'>
                <div class="Icon32 User Normal" style="margin-right:0px;"></div>
                <asp:Label ID="lblUsername" runat="server" />
            </a>
        </div>
        <div style="float:left;">
            <asp:Label ID="lblGroupName" runat="server" />
            <asp:Label ID="lblAuthority" runat="server" />
        </div>
        <div style="float:left;margin-left:10px;">
            <asp:Button ID="btLogout" runat="server" CssClass="Button LogoutEN" 
                CausesValidation="False" onclick="btLogout_Click" />
        </div>
        <div style="clear:both;"></div>
    </asp:Panel>
</div>