using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackageOrder3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void BindOrderNo()
    {
        if (Session["OrderNo"] != null)
        {
            lblOrderNo.Text = Session["OrderNo"].ToString();
        }
    }
}