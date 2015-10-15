using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Webboard_Default : System.Web.UI.Page
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    public clsSecurity clsSecurity = new clsSecurity();

    public string tableDefault = "WebboardType";
    public string webDefault = "Default.aspx";
    public string webManageAdd = "/WebboardManage/Type/NewType/";
    public string webManageCommand = "/WebboardManage/Type/{0}/{1}/";
    public string webGroupManage = "/WebboardManage/Group/{0}/NewGroup/";
    #endregion
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (clsSecurity.LoginGroup != "Admin") webManageAdd = "";
            WebboardTypeBuilder();
        }
    }

    private void WebboardTypeBuilder()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Data Builder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("Name,");
        strSQL.Append("Detail,");
        strSQL.Append("Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault+" ");
        if (clsSecurity.LoginGroup!="Admin")
        {
            strSQL.Append("WHERE ");
            strSQL.Append("Active='1' ");
        }
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort,Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvDefault.Visible = true;
            gvDefault.DataSource = dt;
            gvDefault.DataBind();
            lblDefault.Text = "";
        }
        else
        {
            gvDefault.Visible = false;
            lblDefault.Text = clsDefault.AlertMessageColor(
                "ไม่พบข้อมูลที่ต้องการ", 
                clsDefault.AlertType.Info);
        }
        #endregion
    }
}