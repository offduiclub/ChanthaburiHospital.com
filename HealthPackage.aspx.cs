using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class HealthPackage : ucLanguage
{
    #region GlobalVariable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsLanguage clsLanguage = new clsLanguage();
    public clsSecurity clsSecurity = new clsSecurity();
    #endregion
    #region DatabaseConfig
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (clsSecurity.LoginChecker("admin"))
            {
                pnManage.Visible = true;
            }
            if (clsDefault.URLRouting("id") == "")
            {
                DefaultBuilder();
            }
            else
            {
                DetailBuilder(clsDefault.URLRouting("id"));
            }
        }
    }

    private void DefaultBuilder()
    {
        gvDefault.Visible = true; pnDetail.Visible = false;
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("B.Name LanguageName,");
        strSQL.Append("A.UID,");
        strSQL.Append("A.Name,");
        strSQL.Append("A.DetailSub,");
        strSQL.Append("A.PicThumbnail,");
        strSQL.Append("A.ActiveDateFrom,");
        strSQL.Append("A.ActiveDateTo,");
        strSQL.Append("A.UnitPrice ");
        strSQL.Append("FROM ");
        strSQL.Append("HealthPackage A INNER JOIN Language B ON A.LanguageUID=B.UID AND B.Active='1' ");
        if (clsSecurity.LoginChecker("admin"))
        {
            strSQL.Append(";");
        }
        else
        {
            strSQL.Append("WHERE ");
            strSQL.Append("StatusFlag='A' ");
            strSQL.Append("AND B.Name='" + clsLanguage.LanguageCurrent + "' ");
            strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= GETDATE()) ");
            strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= GETDATE());");
        }
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvDefault.DataSource = dt;
            gvDefault.DataBind();
        }
        else
        {
            lblDefault.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล");
        }
        #endregion
    }

    private void DetailBuilder(string uid)
    {
        gvDefault.Visible = false; pnManage.Visible = false; pnDetail.Visible = true;
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("A.Name,");
        strSQL.Append("A.DetailSub,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.PicThumbnail,");
        strSQL.Append("A.PicFull,");
        strSQL.Append("A.ActiveDateFrom,");
        strSQL.Append("A.ActiveDateTo,");
        strSQL.Append("A.MetaKeywords,");
        strSQL.Append("A.MetaDescription,");
        strSQL.Append("A.MWhen,");
        strSQL.Append("A.UnitPrice ");
        strSQL.Append("FROM ");
        strSQL.Append("HealthPackage A ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND UID="+uid+" ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= GETDATE()) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= GETDATE());");
        #endregion
        //lblDefault.Text = strSQL.ToString(); return;
        dt = clsSQL.Bind(strSQL.ToString(),dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            #region PageBuilder
            Page.Title = dt.Rows[0]["Name"].ToString();
            if (dt.Rows[0]["MetaKeywords"] != DBNull.Value && dt.Rows[0]["MetaKeywords"].ToString() != "")
            {
                Page.MetaKeywords = dt.Rows[0]["MetaKeywords"].ToString();
            }
            if (dt.Rows[0]["MetaDescription"] != DBNull.Value && dt.Rows[0]["MetaDescription"].ToString() != "")
            {
                Page.MetaDescription = dt.Rows[0]["MetaDescription"].ToString();
            }
            #endregion
            lblName.Text = "<h1>"+dt.Rows[0]["Name"].ToString()+"</h1>";
            lblDetail.Text = dt.Rows[0]["DetailSub"].ToString()+" | <a href='/HealthPackage/'>กลับสู่หน้าหลัก</a>";
            if (dt.Rows[0]["PicFull"] != DBNull.Value)
            {
                lblPhotoFull.Text = "<img src='" + dt.Rows[0]["PicFull"].ToString() + "' alt='" + dt.Rows[0]["Name"].ToString() + "' title='" + dt.Rows[0]["Name"].ToString() + "'/>";
            }
            lblContent.Text = dt.Rows[0]["Detail"].ToString();
            lblDateFrom.Text=(dt.Rows[0]["ActiveDateFrom"]!=DBNull.Value?DateTime.Parse(dt.Rows[0]["ActiveDateFrom"].ToString()).ToString("dd/MM/yyyy"):"-");
            lblDateTo.Text = (dt.Rows[0]["ActiveDateTo"] != DBNull.Value ? DateTime.Parse(dt.Rows[0]["ActiveDateTo"].ToString()).ToString("dd/MM/yyyy") : "-");
            lblPrice.Text = (double.Parse(dt.Rows[0]["UnitPrice"].ToString()).ToString("#,#.##")!=""?double.Parse(dt.Rows[0]["UnitPrice"].ToString()).ToString("#,#.##"):"-");
        }
        else
        {
            lblDefault.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล");
        }
        #endregion
    }
}