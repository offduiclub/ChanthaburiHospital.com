using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class MedicalCenter : ucLanguage
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsLanguage clsLanguage = new clsLanguage();
    public clsSecurity clsSecurity = new clsSecurity();

    public string DoctorPhotoPath = "/Upload/Doctor/";
    public string SpecialtyText = "";
    public string DepartmentText = "";
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
            if (clsDefault.URLRouting("id") != "")
            {
                #region Bind Variable
                if (clsLanguage.LanguageCurrent == "th-TH")
                {
                    SpecialtyText = "เชี่ยวชาญ";
                    DepartmentText = "ประจำ";
                }
                else
                {
                    SpecialtyText = "Specialty";
                    DepartmentText = "Center";
                }
                #endregion
                BindDefault(clsDefault.URLRouting("id"));
                BindGallery(clsDefault.URLRouting("id"));
                DoctorBuilder(clsDefault.URLRouting("id"));
                PromotionBuilder(clsDefault.URLRouting("id"));
                PackageBuilder(clsDefault.URLRouting("id"));
            }
        }
    }

    private void BindDefault(string UID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("MedicalCenterGroupUID,");
        strSQL.Append("LanguageUID,");
        strSQL.Append("UID,");
        strSQL.Append("Icon,");
        strSQL.Append("Name,");
        strSQL.Append("Detail,");
        strSQL.Append("Content,");
        strSQL.Append("Location,");
        strSQL.Append("OfficeHours,");
        strSQL.Append("Phone,");
        strSQL.Append("EMail,");
        strSQL.Append("MetaKeywords,");
        strSQL.Append("MetaDescription ");
        strSQL.Append("FROM ");
        strSQL.Append("MedicalCenter ");
        strSQL.Append("WHERE ");
        //strSQL.Append("UID=" + parameterChar + "UID ");
        strSQL.Append("DepartmentUID=" + parameterChar + "UID ");
        strSQL.Append("AND LanguageUID="+clsLanguage.LanguageUIDCurrent.ToString()+" ");
        strSQL.Append("AND Active='1'");
        #endregion

        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "UID", UID } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            Page.Title = dt.Rows[0]["Name"].ToString();
            if (dt.Rows[0]["MetaKeywords"] != DBNull.Value && dt.Rows[0]["MetaKeywords"].ToString() != "")
            {
                Page.MetaKeywords = dt.Rows[0]["MetaKeywords"].ToString();
            }
            if (dt.Rows[0]["MetaDescription"] != DBNull.Value && dt.Rows[0]["MetaDescription"].ToString() != "")
            {
                Page.MetaDescription = dt.Rows[0]["MetaDescription"].ToString();
            }

            lblIcon.Text = "<img src='" + dt.Rows[0]["Icon"].ToString() +
                "' title='" + dt.Rows[0]["Name"].ToString() +
                "' alt='" + dt.Rows[0]["Name"].ToString() + "' style='width:120px;'/>";
            lblName.Text = "<h1>" + dt.Rows[0]["Name"].ToString() + "</h1>";
            lblDetail.Text = dt.Rows[0]["Detail"].ToString();
            lblContent.Text = dt.Rows[0]["Content"].ToString();
            lblLocationValue.Text = dt.Rows[0]["Location"].ToString();
            lblOfficeHoursValue.Text = dt.Rows[0]["OfficeHours"].ToString();
            lblPhoneValue.Text = dt.Rows[0]["Phone"].ToString();

            if (dt.Rows[0]["EMail"] != DBNull.Value)
            {
                lblEMailValue.Text = "<a href='mailto:" + dt.Rows[0]["EMail"].ToString() + "'>" + dt.Rows[0]["EMail"].ToString() + "</a>";
            }

            #region Admin Menu
            if (clsSecurity.LoginChecker("admin"))
            {
                lblAdminMenu.Text = "<div class='dvContentMenu'>" +
                    "<a href='/Management/MedicalCenterManage.aspx?group=" + dt.Rows[0]["MedicalCenterGroupUID"].ToString() +
                    "&id=" + dt.Rows[0]["UID"].ToString() +
                    "&command=edit"+
                    "&language=" + dt.Rows[0]["LanguageUID"].ToString() + "' title='แก้ไขข้อมูล' class='cbIFrame'>" +
                    "<span class='Icon16 Edit' />" +
                    "</a>" +
                    "</div>";
            }
            else
            {
                lblAdminMenu.Text = "";
            }
            #endregion
        }
        else
        {
            //ucColorBox1.Redirect("/", "ไม่พบหน้าที่คุณต้องการ");
            ucColorBox1.Alert("ไม่พบข้อมูล", "ไม่พบหน้าที่คุณต้องการ");
        }
        #endregion
    }

    private void BindGallery(string UID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("PhotoPreview,");
        strSQL.Append("Photo,");
        strSQL.Append("Name,");
        strSQL.Append("Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("PhotoGallery ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("AND GlobalName='MedicalCenter' ");
        strSQL.Append("AND GlobalUID='"+UID+"' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblGallery.Text = "";
            ucGallery1.Source = dt;
            ucGallery1.PreviewHeight = "auto";
        }
        else
        {
            lblGallery.Text = "-";
            ucGallery1.Visible = false;
        }
        #endregion
        #region Admin Menu
            if (clsSecurity.LoginChecker("admin"))
            {
                lblAdminGalleryMenu.Text = "<div class='dvContentMenu'>" +
                    "<a href='/Management/PhotoGallery.aspx?globalid=" + UID + "&globalname=MedicalCenter' title='แก้ไขข้อมูล' target='_Blank'>" +
                    "<span class='Icon16 Edit' />" +
                    "</a>" +
                    "</div>";
            }
            else
            {
                lblAdminGalleryMenu.Text = "";
            }
            #endregion
    }

    private void DoctorBuilder(string UID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("MC.UID MedicalCenterUID,");
        strSQL.Append("ISNULL(MC.Name,'') Department,");
        strSQL.Append("DD.DepartmentUID,");
        strSQL.Append("D.UID,");
        strSQL.Append("D.Photo,");
        #region Language Select
        if (clsLanguage.LanguageCurrent == "th-TH")
        {
            strSQL.Append("D.PNameTH+' '+D.FNameTH+'  '+D.LNameTH Name1,");
            strSQL.Append("D.FNameEN+'  '+D.LNameEN+', '+D.PNameEN Name2,");
            strSQL.Append("D.SpecialtyTH Specialty,");
            strSQL.Append("D.EducationTH Education,");
            strSQL.Append("D.ExperianceTH Experiance,");
            strSQL.Append("D.TypeTH Type,");
        }
        else
        {
            strSQL.Append("D.PNameTH+' '+D.FNameTH+'  '+D.LNameTH Name2,");
            strSQL.Append("D.FNameEN+'  '+D.LNameEN+', '+D.PNameEN Name1,");
            strSQL.Append("D.SpecialtyEN Specialty,");
            strSQL.Append("D.EducationEN Education,");
            strSQL.Append("D.ExperianceEN Experiance,");
            strSQL.Append("D.TypeEN Type,");
        }
        #endregion
        strSQL.Append("D.MedID,");
        strSQL.Append("D.Phone,");
        strSQL.Append("D.EMail ");
        strSQL.Append("FROM ");
        strSQL.Append("Doctor D ");
        strSQL.Append("LEFT JOIN DoctorDepartment DD ON D.UID=DD.DoctorUID ");
        strSQL.Append("LEFT JOIN MedicalCenter MC ON DD.DepartmentUID=MC.DepartmentUID ");
        if (clsLanguage.LanguageCurrent == "th-TH")
        {
            strSQL.Append("AND MC.LanguageUID=1 ");
        }
        else
        {
            strSQL.Append("AND MC.LanguageUID=2 ");
        }
        strSQL.Append("WHERE ");
        strSQL.Append("D.Active='1' ");
        //strSQL.Append("AND MC.UID=" + UID + " ");
        strSQL.Append("AND DD.DepartmentUID='" + UID + "' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("D.FNameTH");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvDoctor.Visible = true;
            gvDoctor.DataSource = dt;
            gvDoctor.DataBind();
        }
        else
        {
            gvDoctor.Visible = false;
            lblDoctor.Text = "<div style='text-align:center;'>-</div>";
        }
        #endregion
    }

    private void PromotionBuilder(string UID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,PromotionName,DetailSub,PicThumbnail ");
        strSQL.Append("FROM ");
        strSQL.Append("Promotion ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= GETDATE()) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= GETDATE()) ");
        strSQL.Append("AND DepartmentUID='" + UID + "' ");
        strSQL.Append("AND LanguageUID=" + clsLanguage.LanguageUIDCurrent.ToString() + " ");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvPromotion.Visible = true;
            gvPromotion.DataSource = dt;
            gvPromotion.DataBind();
        }
        else
        {
            gvPromotion.Visible = false;
            lblPromotion.Text = "<div style='text-align:center;'>-</div>";
        }
        #endregion
    }

    private void PackageBuilder(string UID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,PackageName,DetailSub,PicThumbnail ");
        strSQL.Append("FROM ");
        strSQL.Append("Package ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND ActiveDateFrom <= GETDATE() ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= GETDATE()) ");
        strSQL.Append("AND DepartmentUID='"+UID+"' ");
        strSQL.Append("AND LanguageUID="+clsLanguage.LanguageUIDCurrent.ToString()+" ");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvPackage.Visible = true;
            gvPackage.DataSource = dt;
            gvPackage.DataBind();
        }
        else
        {
            gvPackage.Visible = false;
            lblPackage.Text = "<div style='text-align:center;'>-</div>";
        }
        #endregion
    }
}