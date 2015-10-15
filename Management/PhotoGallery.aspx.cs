using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_PhotoGallery : System.Web.UI.Page
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsSecurity clsSecurity = new clsSecurity();
    public string pathUpload = "/Upload/PhotoGallery/";
    public string tableDefault = "PhotoGallery";
    public string webDefault = "PhotoGallery.aspx";
    public string webManage = "PhotoGalleryManage.aspx";
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
            if (Request.QueryString["globalname"] != null && Request.QueryString["globalid"] != null)
            {
                BindDefault();
            }
            else
            {
                ucColorBox1.Redirect(
                    (Request.QueryString["globalpage"] != null ? 
                    Request.QueryString["globalpage"]+clsDefault.QueryStringRemover(new string[]{"globalid","globalname","globalpage"}) : 
                    "/"),
                    "ไม่พบข้อมูลที่คุณต้องการ");
            }
        }
    }

    protected void BindDefault()
    {
        dlDefault.Visible = true; pnDGHeader.Visible = true;
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Header Builder
        lblHeader.Text = clsSQL.Return("SELECT Name FROM "+clsDefault.QueryStringChecker("globalname")+" WHERE UID=" + clsDefault.QueryStringChecker("globalid"), dbType, cs);
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append(tableDefault + ".UID,");
        strSQL.Append(tableDefault + ".PhotoPreview,");
        strSQL.Append(tableDefault + ".Photo,");
        strSQL.Append(tableDefault + ".Name,");
        strSQL.Append(tableDefault + ".Detail,");
        strSQL.Append(tableDefault + ".[View],");
        strSQL.Append(tableDefault + ".MWhen,");
        strSQL.Append(tableDefault + ".Sort,");
        strSQL.Append(tableDefault + ".Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        #region Where
        strSQL.Append("WHERE ");
        strSQL.Append("GlobalName='" + clsDefault.QueryStringChecker("globalname") + "' ");
        strSQL.Append("AND GlobalUID=" + clsDefault.QueryStringChecker("globalid") + " ");
        #endregion
        strSQL.Append("ORDER BY ");
        strSQL.Append(tableDefault + ".Sort");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            lblDG.Text = "";
            dlDefault.DataSource = dt;
            dlDefault.DataBind();
            dt = null;
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
        }
        #endregion
    }

    protected void btDGSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "เกิดข้อผิดพลาด", "คุณไม่ได้รับสิทธิ์ในการบันทึกข้อมูล กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        #endregion
        #region SQL Builder
        for (int i = 0; i < dlDefault.Items.Count; i++)
        {
            Label lblDGID = (Label)dlDefault.Items[i].FindControl("lblDGID");
            CheckBox cbDGActive = (CheckBox)dlDefault.Items[i].FindControl("cbDGActive");
            TextBox txtDGSort = (TextBox)dlDefault.Items[i].FindControl("txtDGSort");

            if (lblDGID != null && cbDGActive != null)
            {
                #region SQL Query
                strSQL.Append("UPDATE ");
                strSQL.Append(tableDefault + " ");
                strSQL.Append("SET ");
                strSQL.Append("Sort=" + clsSQL.CodeFilter(txtDGSort.Text) + ",");
                strSQL.Append("Active='" + (cbDGActive.Checked ? "1" : "0") + "' ");
                strSQL.Append("WHERE ");
                strSQL.Append("UID=" + lblDGID.Text);
                strSQL.Append(";");
                #endregion
            }
        }
        #endregion

        if (clsSQL.Execute(strSQL.ToString(), dbType, cs))
        {
            ucColorBox1.Redirect("/Management/" + webDefault + clsDefault.QueryStringMerge(), "ดำเนินการเสร็จสิ้น", "แก้ไขข้อมูลเสร็จเรียบร้อย");
        }
        else
        {
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะบันทึกข้อมูลลงฐานข้อมูล<br/>" + strSQL.ToString(), AlertImage: ucColorBox.Alerts.Fail);
        }
    }
}