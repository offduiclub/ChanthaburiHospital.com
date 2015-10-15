using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Webboard_Answer : System.Web.UI.Page
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    public clsSecurity clsSecurity = new clsSecurity();

    public string tableDefault = "WebboardAnswer";
    public string webDefault = "/Webboard/{0}/";
    public string webManageAdd = "/WebboardManage/Answer/{0}/{1}/NewAnswer/";
    public string webManageCommand = "/WebboardManage/Answer/{0}/{1}/{2}/{3}/";
    public string webManageCommandQuestion = "/WebboardManage/Question/{0}/{1}/{2}/";
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
            if (clsDefault.URLRouting("group") != "" && clsDefault.URLRouting("id") != "")
            {
                webDefault = string.Format(webDefault, clsDefault.URLRouting("group"));
                WebboardQuestionBuilder(clsDefault.URLRouting("group"), clsDefault.URLRouting("id"));
                WebboardAnswerBuilder(clsDefault.URLRouting("id"));
            }
            else
            {
                webDefault = "/Webboard/";
                ucColorBox1.Redirect(webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
            }
        }
    }

    private void WebboardQuestionBuilder(string webboardGroupUID,string webboardQuestionUID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        string outSQL;
        #endregion
        #region Update Views
        if (!clsSQL.Update(
            "WebboardQuestion",
            new string[,] { { "Views", "Views+1" } },
            new string[,] { { parameterChar + "UID", webboardQuestionUID } },
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
        strSQL.Append("Q.WebboardGroupUID,");
        strSQL.Append("Q.Photo,");
        strSQL.Append("Q.Name,");
        strSQL.Append("Q.Detail,");
        strSQL.Append("Q.MetaKeywords,");
        strSQL.Append("Q.MetaDescription,");
        strSQL.Append("Q.Views,");
        strSQL.Append("Q.CName,");
        strSQL.Append("Q.CEmail,");
        strSQL.Append("Q.CIPAddress,");
        strSQL.Append("Q.CComputerName,");
        strSQL.Append("Q.MIPAddress,");
        strSQL.Append("Q.MComputerName,");
        strSQL.Append("Q.CWhen,");
        strSQL.Append("Q.MWhen,");
        strSQL.Append("Q.Active,");
        strSQL.Append("[User].Username,");
        strSQL.Append("[User].Photo UserPhoto ");
        strSQL.Append("FROM ");
        strSQL.Append("WebboardQuestion Q ");
        strSQL.Append("LEFT JOIN ");
        strSQL.Append("[User] ON Q.CUser=[User].UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("Q.UID=" + parameterChar + "UID ");
        if (clsSecurity.LoginGroup != "Admin")
        {
            strSQL.Append("AND Q.Active='1'");
        }
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "UID", webboardQuestionUID } }, dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            #region Header
            this.Title = "โรงพยาบาลกรุงเทพจันทบุรี | Webboard : " + dt.Rows[0]["Name"].ToString();
            if (dt.Rows[0]["MetaKeywords"].ToString().Trim() != "")
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
            lblUser.Text = (
                dt.Rows[0]["Username"] != DBNull.Value ?
                "<span class='UserMember'><b>" + dt.Rows[0]["Username"].ToString() + "</b></span>" :
                dt.Rows[0]["CName"].ToString()
                );
            lblCWhen.Text = "<span title='" + DateTime.Parse(dt.Rows[0]["CWhen"].ToString()).ToString("dd/MM/yyyy HH:mm") + "'>" +
                DateTime.Parse(dt.Rows[0]["CWhen"].ToString()).ToString("dd/MM/yyyy") +
                "</span>";
            imgHeaderIcon.ImageUrl = (
                dt.Rows[0]["UserPhoto"] != DBNull.Value ?
                dt.Rows[0]["UserPhoto"].ToString() :
                "/Webboard/Images/icUser.png"
                );
            lblIPAddress.Text = dt.Rows[0]["CIPAddress"].ToString();
            if (dt.Rows[0]["Active"].ToString() == "0")
            {
                lblStatus.Text = "<span style='font-weight:normal;padding-left:10px;'><span class='Icon16 Warn Normal'></span> Hidden</span>";
            }
            #endregion

            lblHeaderName.Text = dt.Rows[0]["Name"].ToString();
            if (dt.Rows[0]["Photo"] != DBNull.Value && !string.IsNullOrEmpty(dt.Rows[0]["Photo"].ToString()))
            {
                lblDetail.Text = "<div style='text-align:center;padding:10px;background-color:#fcfcfc;border-bottom:1px dashed #EEE;'><img src='" + dt.Rows[0]["Photo"].ToString() + "' alt='" + dt.Rows[0]["Name"].ToString() + "'/></div>";
            }
            lblDetail.Text += "<div style='padding:20px;'>"+dt.Rows[0]["Detail"].ToString()+"</div>";
        }
        #endregion
    }

    private void WebboardAnswerBuilder(string webboardQuestionUID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Data Builder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("A.UID,");
        strSQL.Append("A.Photo,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.Status,");
        strSQL.Append("A.CName,");
        strSQL.Append("A.CEmail,");
        strSQL.Append("A.CIPAddress,");
        strSQL.Append("A.CComputerName,");
        strSQL.Append("A.MIPAddress,");
        strSQL.Append("A.MComputerName,");
        strSQL.Append("A.CWhen,");
        strSQL.Append("A.MWhen,");
        strSQL.Append("A.Active,");
        strSQL.Append("[User].Username,");
        strSQL.Append("[User].Photo UserPhoto ");
        strSQL.Append("FROM ");
        strSQL.Append("WebboardAnswer A ");
        strSQL.Append("LEFT JOIN ");
        strSQL.Append("[User] ON A.CUser=[User].UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("A.WebboardQuestionUID=" + parameterChar + "UID ");
        if (clsSecurity.LoginGroup != "Admin")
        {
            strSQL.Append("AND A.Active='1' ");
        }
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "UID", webboardQuestionUID } }, dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvDefault.DataSource = dt;
            gvDefault.DataBind();
        }
        else
        {
            lblDefault.Text = "";
        }
        #endregion
    }
}