using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsSecurity clsSecurity = new clsSecurity();

            if (clsSecurity.LoginGroup.ToLower() != "admin" && clsSecurity.LoginGroup.ToLower() != "hr")
            {
                ucColorBox1.Redirect("/", "กรุณาล็อคอินก่อน", PreloaderImage: ucColorBox.Preloaders.Recycle);
            }

            MenuBuilder();
        }
    }

    private void MenuBuilder()
    {
        ucMenuMega.Item item = new ucMenuMega.Item();

        item = new ucMenuMega.Item();
        item.UID = 1;
        //item.ParentUID = 1;
        item.Name = "จัดการข้อมูลเว็บไซต์";
        item.Detail = "";
        item.URL = "";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 27;
        item.ParentUID = 1;
        item.Name = "IntroPage Manage";
        item.Detail = "";
        item.URL = "IntroPage.aspx";
        ucMenuMega1.Items.Add(item);
        /*
        item = new ucMenuMega.Item();
        item.UID = 11;
        item.ParentUID = 1;
        item.Name = "Template Manage";
        item.Detail = "";
        item.URL = "Template.aspx";
        ucMenuMega1.Items.Add(item);
        */
        item = new ucMenuMega.Item();
        item.UID = 12;
        item.ParentUID = 1;
        item.Name = "Content Manage";
        item.Detail = "";
        item.URL = "Content.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 13;
        item.ParentUID = 1;
        item.Name = "Medical Center Manage";
        item.Detail = "";
        item.URL = "MedicalCenterGroup.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 14;
        item.ParentUID = 1;
        item.Name = "Service Manage";
        item.Detail = "";
        item.URL = "ServiceGroup.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 15;
        item.ParentUID = 1;
        item.Name = "Slider Manage";
        item.Detail = "";
        item.URL = "Slider.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 26;
        item.ParentUID = 1;
        item.Name = "Highlight Manage";
        item.Detail = "";
        item.URL = "Highlight.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 16;
        item.ParentUID = 1;
        item.Name = "E-Mail Template";
        item.Detail = "";
        item.URL = "EmailTemplate.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 17;
        item.ParentUID = 1;
        item.Name = "E-Mail Manage";
        item.Detail = "";
        item.URL = "EmailList.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 2;
        //item.ParentUID = 1;
        item.Name = "จัดการข้อมูลการติดต่อกับลูกค้า";
        item.Detail = "";
        item.URL = "";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 21;
        item.ParentUID = 2;
        item.Name = "User Manage";
        item.Detail = "";
        item.URL = "User.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 22;
        item.ParentUID = 2;
        item.Name = "Inquire Manage";
        item.Detail = "";
        item.URL = "Inquiry.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 23;
        item.ParentUID = 2;
        item.Name = "Doctor Appointment Manage";
        item.Detail = "";
        item.URL = "DoctorAppointment.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 24;
        item.ParentUID = 2;
        item.Name = "Jobs Manage";
        item.Detail = "";
        item.URL = "Jobs.aspx";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 25;
        item.ParentUID = 2;
        item.Name = "Jobs History Manage";
        item.Detail = "";
        item.URL = "JobsHistory.aspx";
        ucMenuMega1.Items.Add(item);
    }
}
