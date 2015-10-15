using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_ContentManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();
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
            if (Request.QueryString["id"] != null)
            {
                if (clsDefault.QueryStringChecker("command") == "edit")
                {
                    BindDetail(Request.QueryString["id"].ToString());
                }
                else
                {
                    ucColorBox1.Redirect("/Management/Content.aspx", "ไม่พบหน้าเว็บที่คุณต้องการ");
                }
            }
            else
            {
                ucColorBox1.Redirect("/Management/Content.aspx", "ไม่พบหน้าเว็บที่คุณต้องการ");
            }
        }
    }

    protected void BindDetail(string id)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Language.Icon,");
        strSQL.Append("Content.Name,");
        strSQL.Append("Content.Detail,");
        strSQL.Append("Content.Content,");
        strSQL.Append("Content.Sort,");
        strSQL.Append("Content.Active ");
        strSQL.Append("FROM ");
        strSQL.Append("Content ");
        strSQL.Append("INNER JOIN ");
        strSQL.Append("Language ON Content.LanguageUID=Language.UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("Content.UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar+"ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            lblLanguage.Text = "<img src='"+dt.Rows[0]["Icon"].ToString()+"'/>";
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            ucContent.Text = dt.Rows[0]["Content"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["Active"].ToString() == "1" ? true : false);
        }
        else
        {
            ucColorBox1.Redirect("/Management/Content.aspx", "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion

        StringBuilder strSQL = new StringBuilder();
        int id = 0;
        string outSQL;

        #region Update
        if (Request.QueryString["id"] != null && clsDefault.QueryStringChecker("command") == "edit")
        {
            id = int.Parse(Request.QueryString["id"].ToString());

            if (clsSQL.Update("Content",
                new string[,]{
                    {"Content","N'" + clsSQL.CodeFilter(ucContent.Text) + "'"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsDefault.CodeFilter(txtSort.Text)},
                    {"Active","'" + (cbActive.Checked ? "1" : "0") + "'"}
                }, new string[,] { { } },
                "UID=" + id.ToString(),
                dbType, cs, out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }
}