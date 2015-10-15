using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class JobsDetail : System.Web.UI.Page
{
    #region GlobalVariable
    public clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL();
    private clsLanguage clsLanguage = new clsLanguage();

    private clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    private string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (clsDefault.URLRouting("id") != "")
            {
                DefaultBuilder(clsDefault.URLRouting("id"));
            }
        }
    }

    private void DefaultBuilder(string id)
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("Name,Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("Jobs ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID="+id+";");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblName.Text = dt.Rows[0]["Name"].ToString();
            lblDetail.Text = dt.Rows[0]["Detail"].ToString();
        }
        else
        {
            clsColorBox clsColorBox = new clsColorBox();
            clsColorBox.Refresh();
        }
        #endregion
    }
}