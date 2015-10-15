using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_ServiceGroupManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    public string tableDefault = "ServiceGroup";
    public string webDefault = "ServiceGroup.aspx";
    public string webManage = "ServiceGroupManage.aspx";
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
                    ucColorBox1.SizeChange();
                    BindControl();
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
                BindControl();
            }
        }
    }

    private void BindControl()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion

        #region Language
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("Detail,");
        strSQL.Append("Icon ");
        strSQL.Append("FROM ");
        strSQL.Append("Language ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rbLanguage.Items.Add(
                    new ListItem(
                        "<img src='" + dt.Rows[i]["Icon"].ToString() + "'/> " + dt.Rows[i]["Detail"].ToString(),
                        dt.Rows[i]["UID"].ToString()
                    )
                );
                if (i == 0) rbLanguage.Items[i].Selected = true;
            }
            dt = null;
        }
        #endregion
    }

    protected void BindDetail(string id)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Language.UID LanguageUID,");
        strSQL.Append("Language.Icon,");
        strSQL.Append(tableDefault + ".Name,");
        strSQL.Append(tableDefault + ".Detail,");
        strSQL.Append(tableDefault + ".Sort,");
        strSQL.Append(tableDefault + ".Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("INNER JOIN ");
        strSQL.Append("Language ON " + tableDefault + ".LanguageUID=Language.UID ");
        strSQL.Append("WHERE ");
        strSQL.Append(tableDefault + ".UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            if (rbLanguage.Items.Count > 0)
            {
                rbLanguage.SelectedValue = dt.Rows[0]["LanguageUID"].ToString();
            }
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
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

        #region Delete Photo
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Icon ");
        strSQL.Append("FROM ");
        strSQL.Append("Service ");
        strSQL.Append("WHERE ");
        strSQL.Append("ServiceGroupUID=" + id);
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Icon"] != DBNull.Value)
                {
                    clsIO.FileExist(dt.Rows[i]["Icon"].ToString(), true);
                }
            }
            dt = null;
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
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
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
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
                    {"UID",id.ToString()},
                    {"LanguageUID",rbLanguage.SelectedItem.Value},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
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