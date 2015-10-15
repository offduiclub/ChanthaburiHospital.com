using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Webboard_Group : System.Web.UI.UserControl
{
    #region Property
    private string _webboardTypeUID;
    public string WebboardTypeUID
    {
        get { return _webboardTypeUID; }
        set { _webboardTypeUID = value; }
    }
    #endregion
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    public clsSecurity clsSecurity = new clsSecurity();

    public string tableDefault = "WebboardGroup";
    public string webDefault = "/Webboard";
    public string webManage = "/WebboardManage/Group/{0}/{1}/{2}/";
    public string webLastUpdate = "/Webboard/{0}/{1}/{2}/";
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
            if (!string.IsNullOrEmpty(_webboardTypeUID))
            {
                WebboardGroupBuilder();
            }
            else
            {
                lblDefault.Text = "<div style='padding:10px;text-align:center;border-top:1px solid #E8E8E8;'>-</div>";
            }
        }
    }

    private void WebboardGroupBuilder()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Data Builder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("Icon,");
        strSQL.Append("Name,");
        strSQL.Append("Detail,");
        strSQL.Append("(SELECT COUNT(UID) FROM WebboardQuestion WHERE WebboardGroupUID=T.UID AND Active='1') QuestionCount,");
        strSQL.Append("(SELECT COUNT(WebboardAnswer.UID) FROM WebboardAnswer INNER JOIN WebboardQuestion ");
        strSQL.Append("ON WebboardAnswer.WebboardQuestionUID=WebboardQuestion.UID AND WebboardQuestion.Active='1' AND WebboardQuestion.WebboardGroupUID=T.UID ");
        strSQL.Append("WHERE WebboardAnswer.Active='1')AnswerCount,");
        #region Last Update
        strSQL.Append("(SELECT TOP 1 UID FROM WebboardQuestion WHERE WebboardGroupUID=T.UID AND Active='1' ORDER BY CWhen DESC) QuestionLastUID,");
        strSQL.Append("(SELECT TOP 1 Name FROM WebboardQuestion WHERE WebboardGroupUID=T.UID AND Active='1' ORDER BY CWhen DESC) QuestionLastName,");
        strSQL.Append("(SELECT TOP 1 CWhen FROM WebboardQuestion WHERE WebboardGroupUID=T.UID AND Active='1' ORDER BY CWhen DESC) QuestionLastCWhen,");
        strSQL.Append("(SELECT TOP 1 CName FROM WebboardQuestion WHERE WebboardGroupUID=T.UID AND Active='1' ORDER BY CWhen DESC) QuestionLastCName,");
        strSQL.Append("(SELECT TOP 1 [Username] FROM WebboardQuestion LEFT JOIN [User] ON WebboardQuestion.CUser=[User].UID WHERE WebboardGroupUID=T.UID AND WebboardQuestion.Active='1' ORDER BY WebboardQuestion.CWhen DESC) QuestionLastCUsername,");
        #endregion
        strSQL.Append("Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " T ");
        strSQL.Append("WHERE ");
        strSQL.Append("WebboardTypeUID=" + _webboardTypeUID + " ");
        if (clsSecurity.LoginGroup != "Admin")
        {
            
            strSQL.Append("AND Active='1' ");
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
            lblDefault.Text = "<div style='padding:10px;text-align:center;border-top:1px solid #E8E8E8;'>-</div>";
        }
        #endregion
    }
}