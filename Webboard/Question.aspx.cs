using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Webboard_Question : System.Web.UI.Page
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    public clsSecurity clsSecurity = new clsSecurity();

    public string tableDefault = "WebboardQuestion";
    public string webDefault = "/Webboard/";
    public string webManageAdd = "/WebboardManage/Question/{0}/NewQuestion/";
    public string webManageCommand = "/WebboardManage/Question/{0}/{1}/{2}/";
    public string webChild = "/Webboard/{0}/{1}/{2}/";
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
            if (clsDefault.URLRouting("id")!="")
            {
                WebboardGroupBuilder(clsDefault.URLRouting("id"));
                WebboardQuestionImportantBuilder(clsDefault.URLRouting("id"),"I");
                WebboardQuestionBuilder(clsDefault.URLRouting("id"));
            }
            else
            {
                lblDefault.Text = "<div style='padding:10px;text-align:center;'>-</div>";
            }
        }
    }

    private void WebboardGroupBuilder(string webboardGroupUID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        string outSQL;
        #endregion
        #region Update Views
        if(!clsSQL.Update(
            "WebboardGroup",
            new string[,] { { "Views", "Views+1" } },
            new string[,] { { parameterChar + "UID", webboardGroupUID } },
            "UID=" + parameterChar + "UID",
            dbType,
            cs,
            out outSQL))
        {
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพเดทจำนวน Views<br/>" + outSQL, AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        #endregion
        #region Data Builder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Icon,");
        strSQL.Append("Name,");
        strSQL.Append("Detail,");
        strSQL.Append("MetaKeywords,");
        strSQL.Append("MetaDescription ");
        strSQL.Append("FROM ");
        strSQL.Append("WebboardGroup ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + parameterChar + "UID ");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(),new string[,]{{parameterChar+"UID",webboardGroupUID}}, dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.Title = "โรงพยาบาลกรุงเทพจันทบุรี | Webboard : "+dt.Rows[0]["Name"].ToString();
            if(dt.Rows[0]["MetaKeywords"].ToString().Trim()!="")
            {
                this.MetaKeywords = dt.Rows[0]["MetaKeywords"].ToString();
            }
            if (dt.Rows[0]["MetaDescription"].ToString().Trim() != "")
            {
                this.MetaDescription = dt.Rows[0]["MetaDescription"].ToString();
            }
            else
            {
                this.MetaDescription = dt.Rows[0]["Name"].ToString();
            }

            lblHeaderName.Text = dt.Rows[0]["Name"].ToString();
            lblHeaderDetail.Text = dt.Rows[0]["Detail"].ToString();
            imgHeaderIcon.ImageUrl = dt.Rows[0]["Icon"].ToString();
        }
        #endregion
    }

    private void WebboardQuestionImportantBuilder(string webboardGroupUID,string status)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Data Builder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("T.UID,");
        strSQL.Append("T.Status,");
        strSQL.Append("T.Name,");
        strSQL.Append("T.CName,");
        strSQL.Append("T.CEmail,");
        strSQL.Append("[User].Username,");
        strSQL.Append("T.CWhen,");
        strSQL.Append("T.Views,");
        strSQL.Append("(SELECT COUNT(UID) FROM WebboardAnswer WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND Active='1')AnswerCount,");
        strSQL.Append("(SELECT TOP 1 CName FROM WebboardAnswer WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND Active='1' ORDER BY MWhen DESC)AnswerCName,");
        strSQL.Append("(SELECT TOP 1 [User].Username FROM WebboardAnswer LEFT JOIN [User] ON WebboardAnswer.CUser=[User].UID WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND WebboardAnswer.Active='1' ORDER BY WebboardAnswer.MWhen DESC)AnswerUsername,");
        strSQL.Append("(SELECT TOP 1 MWhen FROM WebboardAnswer WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND Active='1' ORDER BY MWhen DESC)AnswerMWhen,");
        strSQL.Append("T.Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " T ");
        strSQL.Append("LEFT JOIN [User] ");
        strSQL.Append("ON T.CUser=[User].UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("T.WebboardGroupUID=" + webboardGroupUID + " ");
        strSQL.Append("AND T.Status = '"+status+"' ");
        if (clsSecurity.LoginGroup != "Admin")
        {
            strSQL.Append("AND T.Active='1' ");
        }
        strSQL.Append("ORDER BY ");
        strSQL.Append("T.Sort ASC,T.CWhen DESC");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            dvImportant.Visible = true;
            gvImportant.DataSource = dt;
            gvImportant.DataBind();
        }
        else
        {
            dvImportant.Visible = false;
        }
        #endregion
    }

    private void WebboardQuestionBuilder(string webboardGroupUID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Data Builder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("T.UID,");
        strSQL.Append("T.Status,");
        strSQL.Append("T.Name,");
        strSQL.Append("T.CName,");
        strSQL.Append("T.CEmail,");
        strSQL.Append("[User].Username,");
        strSQL.Append("T.CWhen,");
        strSQL.Append("T.Views,");
        strSQL.Append("(SELECT COUNT(UID) FROM WebboardAnswer WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND Active='1')AnswerCount,");
        strSQL.Append("(SELECT TOP 1 CName FROM WebboardAnswer WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND Active='1' ORDER BY MWhen DESC)AnswerCName,");
        strSQL.Append("(SELECT TOP 1 [User].Username FROM WebboardAnswer LEFT JOIN [User] ON WebboardAnswer.CUser=[User].UID WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND WebboardAnswer.Active='1' ORDER BY WebboardAnswer.MWhen DESC)AnswerUsername,");
        strSQL.Append("(SELECT TOP 1 MWhen FROM WebboardAnswer WHERE WebboardAnswer.WebboardQuestionUID=T.UID AND Active='1' ORDER BY MWhen DESC)AnswerMWhen,");
        strSQL.Append("T.Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " T ");
        strSQL.Append("LEFT JOIN [User] ");
        strSQL.Append("ON T.CUser=[User].UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("T.WebboardGroupUID=" + webboardGroupUID + " ");
        strSQL.Append("AND T.Status IS NULL ");
        if (clsSecurity.LoginGroup != "Admin")
        {
            strSQL.Append("AND T.Active='1' ");
        }
        strSQL.Append("ORDER BY ");
        strSQL.Append("T.Sort ASC,T.CWhen DESC");
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
            lblDefault.Text = "<div style='padding:10px;text-align:center;'>-</div>";
        }
        #endregion
    }
}