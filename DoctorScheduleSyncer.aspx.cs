using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DoctorScheduleSyncer : System.Web.UI.Page
{
    DateTime dttmPageStart = DateTime.Now;
    public string pageLoadCounter = "";

    protected override void OnPreRender(EventArgs e)
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pageLoadCounter = (DateTime.Now - dttmPageStart).TotalMilliseconds.ToString() + " millisecond.";

            try
            {
                var clsSyncer = new clsSyncer();
                lblDefault.Text = "ผล : " + clsSyncer.DoctorScheduleSyncer() +
                    "<br/>เมื่อเวลา : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
                    "<br/>ใช้เวลาทั้งสิ้น : " + pageLoadCounter;
            }
            catch(Exception){}

            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}