using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Complete : System.Web.UI.Page
{
    public string msg;
    public string url;
    public string time;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["url"] != null)
            {
                url = Session["url"].ToString();
                Session.Remove("url");
            }
            else
            {
                url = "Default.aspx";
            }
            if (Session["msg"] != null)
            {
                msg = Session["msg"].ToString();
                Session.Remove("msg");
            }
            else
            {
                msg = "ดำเนินการเสร็จสิ้น";
            }
            if (Session["time"] != null)
            {
                time = Session["time"].ToString();
                Session.Remove("time");
            }
            else
            {
                time = "5";
            }
        }
    }
}
