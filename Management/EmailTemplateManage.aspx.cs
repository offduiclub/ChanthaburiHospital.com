using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class EmailTemplateManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    public string tableDefault = "EmailTemplate";
    public string webDefault = "EmailTemplate.aspx";
    public string webManage = "EmailTemplateManage.aspx";
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
            BindControl();

            if (Request.QueryString["id"] != null)
            {
                if (clsDefault.QueryStringChecker("command") == "edit")
                {
                    ucColorBox1.SizeChange();
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
            }
            else
            {
                ucColorBox1.SizeChange();
            }
        }
    }

    private void BindControl()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Name ");
        strSQL.Append("FROM ");
        strSQL.Append("Language ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort,Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlLanguage.DataSource = dt;
            ddlLanguage.DataValueField = "UID";
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataBind();
        }
        else
        {
            ddlLanguage.Enabled = false;
        }
    }

    protected void BindDetail(string id)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append(tableDefault + ".LanguageUID,");
        strSQL.Append(tableDefault + ".Name,");
        strSQL.Append(tableDefault + ".Detail,");
        strSQL.Append(tableDefault + ".Subject,");
        strSQL.Append(tableDefault + ".Message,");
        strSQL.Append(tableDefault + ".Sort,");
        strSQL.Append(tableDefault + ".Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append(tableDefault + ".UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlLanguage.SelectedValue = dt.Rows[0]["LanguageUID"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtSubject.Text = dt.Rows[0]["Subject"].ToString();
            ucTextEditor1.Text = dt.Rows[0]["Message"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["Active"].ToString() == "1" ? true : false);
        }
        else
        {
            ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
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
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        clsIO clsIO = new clsIO();
        #endregion

        #region Delete Database
        #region SQL Query
        strSQL.Append("DELETE FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + parameterChar + "UID");
        #endregion
        if (clsSQL.Execute(strSQL.ToString(), new string[,] { { parameterChar + "UID", id } }, dbType, cs))
        {
            ucColorBox1.Redirect(webDefault);
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "เกิดข้อผิดพลาดขณะลบข้อมูล");
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
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
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        int id = 0;
        string outSQL;
        #endregion

        #region Update
        if (Request.QueryString["id"] != null && clsDefault.QueryStringChecker("command") == "edit")
        {
            id = int.Parse(Request.QueryString["id"].ToString());

            if (clsSQL.Update(tableDefault,
                new string[,]{
                    {"LanguageUID",ddlLanguage.SelectedItem.Value},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"Subject","'"+clsSQL.CodeFilter(txtSubject.Text)+"'"},
                    {"Message","'"+clsSQL.CodeFilter(ucTextEditor1.Text)+"'"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"Active","'" + (cbActive.Checked ? "1" : "0") + "'"}
                }, new string[,] { { parameterChar + "UID", id.ToString() } },
                "UID=" + parameterChar + "UID",
                dbType, cs, out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
        #region Insert
        else
        {
            #region Find New ID
            id = clsSQL.GetNewID("UID", tableDefault, "", dbType, cs);
            if (id == 0)
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "ไม่สามารถหา UID ใหม่ได้", AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            #endregion

            if (clsSQL.Insert(tableDefault,
                new string[,]{
                    {"LanguageUID",ddlLanguage.SelectedItem.Value},
                    {"UID",id.ToString()},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"Subject","'"+clsSQL.CodeFilter(txtSubject.Text)+"'"},
                    {"Message","'"+clsSQL.CodeFilter(ucTextEditor1.Text)+"'"},
                    {"CUser","'" + clsSecurity.LoginUID + "'"},
                    {"CWhen","GETDATE()"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"Active","'" + (cbActive.Checked ? "1" : "0") + "'"}
                }, new string[,] { { } },
                dbType, cs, out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }
}