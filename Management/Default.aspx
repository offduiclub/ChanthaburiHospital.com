<%@ Page Title="" Language="C#" MasterPageFile="~/Management/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Management_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .Menu
        {
            display:inline-block;
            padding:5px 8px 5px 8px;
            background-color:#ffffff;
            border:0px;
        }
        .Menu:hover
        {
            display:inline-block;
            padding:4px 7px 4px 7px;
            background-color:#fafafa;
            border:1px solid #dddddd;
        }
        .GroupMenu
        {
            text-align:left;border:1px solid #ddd;
        }
        .GroupMenu legend
        {
            font-family:thaisans_neuebold,tahoma;
            font-size:12pt;
            padding:0px 5px 0px 5px;
            color:#666666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyHead" runat="server">
    <div style="float:left;margin-right:5px;">
        <img src="Images/icManagement.png" alt="Management System" title="Management System" />
    </div>
    <div style="text-align:left;">
        <h1>Management System</h1>
        ระบบจัดการข้อมูลบนเว็บไซต์
    </div>
    <div style="clear:both;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
    <fieldset class="GroupMenu">
        <legend>
            จัดการข้อมูลเว็บไซต์
        </legend>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="IntroPage.aspx">
                <img src="/Images/Icon/icIntroPage.png" alt="IntroPage" title="IntroPage" width="32px"/>  IntroPage Manage
            </a>
        </div>
        <!--<div class="Menu">
            <a href="Template.aspx">
                <img src="Images/icTemplate.png" alt="Template" title="Template" width="32px"/>  Template Manage
            </a>
        </div>-->
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="Content.aspx">
                <img src="Images/icContent.png" alt="Content" title="Content" width="32px"/>  Content Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="MedicalCenterGroup.aspx">
                <img src="/Images/Icon/icMedicalCenter.png" alt="Medical Center" title="Medical Center" width="32px"/>  Medical Center Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="ServiceGroup.aspx">
                <img src="/Images/Icon/icService.png" alt="Service" title="Service" width="32px"/>  Service Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="Slider.aspx">
                <img src="/Images/Icon/icSlider.png" alt="Slider" title="Slider" width="32px"/>  Slider Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="Highlight.aspx">
                <img src="/Images/Icon/icSlider.png" alt="Highlight" title="Highlight" width="32px"/>  Highlight Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="EmailTemplate.aspx">
                <img src="/Images/Icon/icEMail.png" alt="EmailTemplate" title="EmailTemplate" width="32px"/>  E-Mail Template Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="EmailList.aspx">
                <img src="/Images/Icon/icEMail.png" alt="EmailList" title="EmailList" width="32px"/>  E-Mail Manage
            </a>
        </div>
    </fieldset>
    <fieldset class="GroupMenu">
        <legend>
            จัดการข้อมูลการสื่อสารกับลูกค้า
        </legend>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="User.aspx">
                <img src="Images/icUser.png" alt="User" title="User" width="32px"/>  User Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="Inquiry.aspx">
                <img src="Images/icInquire.png" alt="Inquire" title="Inquire" width="32px"/>  Inquiry Manage
            </a>
        </div>
        <div class="Menu" style='<%=(clsSecurity.LoginChecker("admin")?"":"display:none;") %>'>
            <a href="DoctorAppointment.aspx">
                <img src="/Images/Icon/icDoctorAppointment.png" alt="DoctorAppointment" title="DoctorAppointment" width="32px"/>  Doctor Appointment Manage
            </a>
        </div>
        <div class="Menu">
            <a href="Jobs.aspx">
                <img src="/Images/Icon/icJobs.png" alt="JobsManagement" title="JobsManagement" width="32px"/>  Jobs Manage
            </a>
        </div>
        <div class="Menu">
            <a href="JobsHistory.aspx">
                <img src="/Images/Icon/icJobs.png" alt="JobsManagement" title="JobsManagement" width="32px"/>  Jobs History Manage
            </a>
        </div>
    </fieldset>
</asp:Content>

