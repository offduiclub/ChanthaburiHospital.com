using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_Content : System.Web.UI.Page
{
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsSecurity clsSecurity = new clsSecurity();
    public string pathUpload = "/Upload/PhotoInsert/";

    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDefault();

            if (Request.QueryString["id"] != null)
            {
                //clsColorBox clsColorBox = new clsColorBox();
                //clsColorBox.Show("/Management/UserManage.aspx?id=" + Request.QueryString["id"].ToString() + "&command=edit", iframe: true);
            }
        }
    }

    protected void BindDefault()
    {
        gvDefault.Visible = true; pnDGHeader.Visible = true;
        StringBuilder strSQL = new StringBuilder();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Content.UID,");
        strSQL.Append("Content.Name,");
        strSQL.Append("Content.Detail,");
        strSQL.Append("Content.MWhen,");
        strSQL.Append("Content.Active,");
        strSQL.Append("Language.Name LanguageName,");
        strSQL.Append("Language.Icon LanguageIcon ");
        strSQL.Append("FROM ");
        strSQL.Append("Content ");
        strSQL.Append("INNER JOIN Language ON Content.LanguageUID=Language.UID ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Content.Name,");
        strSQL.Append("Language.Sort");
        #endregion

        DataTable dt = new DataTable();
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            lblDG.Text = "";
            gvDefault.DataSource = dt;
            if (Request.QueryString["page"] != null)
            {
                try
                {
                    gvDefault.PageIndex = int.Parse(Request.QueryString["page"].ToString());
                }
                catch (Exception ex)
                {
                    gvDefault.PageIndex = int.Parse(Request.QueryString["page"].ToString()) - 1;
                }
            }
            gvDefault.DataBind();
            dt = null;
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
        }

        strSQL.Length = 0; strSQL.Capacity = 0;
    }

    protected void btDGSubmit_Click(object sender, EventArgs e)
    {
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "เกิดข้อผิดพลาด", "คุณไม่ได้รับสิทธิ์ในการบันทึกข้อมูล กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }

        StringBuilder strSQL = new StringBuilder();

        for (int i = 0; i < gvDefault.Rows.Count; i++)
        {
            #region CurrentPageChecker
            var cbDGCurrentPage = (CheckBox)gvDefault.Rows[i].FindControl("cbDGCurrentPage");
            if (!cbDGCurrentPage.Checked) continue;
            #endregion

            Label lblDGID = (Label)gvDefault.Rows[i].FindControl("lblDGID");
            CheckBox cbDGActive = (CheckBox)gvDefault.Rows[i].FindControl("cbDGActive");

            if (lblDGID != null && cbDGActive != null)
            {
                #region SQL Query
                strSQL.Append("UPDATE ");
                strSQL.Append("Content ");
                strSQL.Append("SET ");
                strSQL.Append("Active='" + (cbDGActive.Checked ? "1" : "0") + "' ");
                strSQL.Append("WHERE ");
                strSQL.Append("UID=" + lblDGID.Text);
                strSQL.Append(";");
                #endregion
            }
        }

        if (clsSQL.Execute(strSQL.ToString(), dbType, cs))
        {
            ucColorBox1.Redirect("/Management/Content.aspx", "ดำเนินการเสร็จสิ้น", "แก้ไขข้อมูลเสร็จเรียบร้อย");
        }
        else
        {
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะบันทึกข้อมูลลงฐานข้อมูล<br/>" + strSQL.ToString(), AlertImage: ucColorBox.Alerts.Fail);
        }
    }

    protected void gvDefault_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDefault.PageIndex = e.NewPageIndex;
        BindDefault();
    }
}