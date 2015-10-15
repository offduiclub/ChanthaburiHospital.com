using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_JobsManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    public string tableDefault = "Jobs";
    public string webDefault = "Jobs.aspx";
    public string webManage = "JobsManage.aspx";
    public string pathUpload = "/Upload/Jobs/";
    public int photoWidth = 200;
    public int photoHeight = 200;
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
            #region Procedure
            if (Request.QueryString["id"] != null)
            {
                #region Condition
                if (clsDefault.QueryStringChecker("command") == "edit")
                {
                    BindDetail(Request.QueryString["id"].ToString());
                }
                else if (clsDefault.QueryStringChecker("command") == "delete")
                {
                    Delete(Request.QueryString["id"].ToString());
                }
                else
                {
                    ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
                }
                #endregion
            }
            #endregion
        }
    }
    
    protected void BindDetail(string id)
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append(tableDefault + ".Name,");
        strSQL.Append(tableDefault + ".Detail,");
        strSQL.Append(tableDefault + ".Sort,");
        strSQL.Append(tableDefault + ".StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append(tableDefault + ".UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Data Builder
            txtName.Text = dt.Rows[0]["Name"].ToString();
            ucDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["StatusFlag"].ToString() == "A" ? true : false);
            #endregion
        }
        else
        {
            ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
        #endregion
    }

    private void Delete(string id)
    {
        pnDetail.Visible = false;
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        var clsIO = new clsIO();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
        #region Delete Photo
        /*
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Icon ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id);
        #endregion
        string photoDelete = clsSQL.Return(strSQL.ToString(), dbType, cs);
        if (!string.IsNullOrEmpty(photoDelete))
        {
            clsIO.FileExist(photoDelete, true);
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        */
        #endregion
        #region Delete Database
        #region SQL Query
        strSQL.Append("DELETE FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id);
        #endregion
        if (clsSQL.Execute(strSQL.ToString(), dbType, cs))
        {
            ucColorBox1.Redirect(webDefault);
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "เกิดข้อผิดพลาดขณะลบข้อมูล");
            return;
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
        #endregion
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin") && !clsSecurity.LoginChecker("hr"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        var strSQL = new StringBuilder();
        var id = 0;
        var outSQL="";
        #endregion
        #region Procedure
        #region Update
        if (Request.QueryString["id"] != null && clsDefault.QueryStringChecker("command") == "edit")
        {
            id = int.Parse(Request.QueryString["id"].ToString());

            if (clsSQL.Update(tableDefault,
                new string[,]{
                    {"Name","'"+txtName.Text.SQLQueryFilter()+"'"},
                    {"Detail","'"+ucDetail.Text.SQLQueryFilter()+"'"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "D") + "'"}
                }, new string[,] { { parameterChar + "UID", id.ToString() } },
                "UID=" + parameterChar + "UID",
                dbType, cs, out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>", Server.HtmlEncode(outSQL), AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
        #region Insert
        else
        {
            if (clsSQL.Insert(tableDefault,
                new string[,]{
                    {"Name","'"+txtName.Text.SQLQueryFilter()+"'"},
                    {"Detail","'"+ucDetail.Text.SQLQueryFilter()+"'"},
                    {"CUser","'" + clsSecurity.LoginUID + "'"},
                    {"CWhen","GETDATE()"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "D") + "'"}
                }, new string[,] { { } },
                dbType, cs, out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>", Server.HtmlEncode(outSQL), AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }
}