using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntroPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IntroPageBuilder();
        }
    }

    private void IntroPageBuilder()
    {
        #region Variable
        var dt = new DataTable();
        var clsIntroPage=new clsIntroPage();
        #endregion
        dt = clsIntroPage.IntroPageBuilder();
        if (dt != null && dt.Rows.Count > 0)
        {
            lblPhoto.Text = string.Format(
                "<img src='{1}' alt='{0}' title='{0}'/>", 
                dt.Rows[0]["Name"].ToString(), dt.Rows[0]["Photo"].ToString());
        }
        else
        {
            lblPhoto.Text = "ไม่พบข้อมูล IntroPage";
        }
    }
}