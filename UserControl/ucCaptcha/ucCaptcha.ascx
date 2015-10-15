<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCaptcha.ascx.cs" Inherits="UserControl_ucCaptcha_ucCaptcha" %>

<style type="text/css">
    .vldCaptcha
    {
        text-decoration:blink;
        /*color:#FF0000;*/
    }
    .dvCaptcha
    {
        background-image:url('<%=ResolveClientUrl("Images/icLock.png") %>');
        background-repeat: no-repeat;
        background-position:left center;
        background-color:#F7BD57;
        padding:4px 4px 4px 22px;
        border:1px solid #979797;
        color:#fff;
        font-weight:bold;
        -webkit-touch-callout: none;
        -webkit-user-select: none;
        -khtml-user-select: none;
        -moz-user-select: moz-none;
        -ms-user-select: none;
        user-select: none;
        cursor:pointer;
    }
    .ctrlAnswer
    {
        background-image:url('<%=ResolveClientUrl("Images/icUnlock.png") %>');
        background-repeat: no-repeat;
        background-position:left center;
        background-color:#fff;
        border:1px solid #ddd;
        padding:4px 4px 4px 22px;
        width:100px;
    }
</style>

<div>
    <span class="dvCaptcha">
        <asp:Label ID="lblCaptcha" runat="server" />
    </span>
    <span style="padding:0px 5px 0px 5px;">=</span>
    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="ctrlAnswer"/>
    <span style="padding-left:10px;color:#6B6B6B;">
        <asp:Label ID="lblCaptchaRemark" runat="server" />
    </span>
    <asp:CompareValidator
        ID="vldCaptcha" runat="server" CssClass="vldCaptcha" 
        ControlToValidate="txtCaptcha"  
        SetFocusOnError="true" Display="Dynamic">
    </asp:CompareValidator>
    <asp:RequiredFieldValidator 
        ID="vldRequire" runat="server" CssClass="vldCaptcha" 
        ControlToValidate="txtCaptcha" SetFocusOnError="true" Display="Dynamic">
    </asp:RequiredFieldValidator>
</div>