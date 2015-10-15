using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    #region Global Variable
    clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsLanguage clsLanguage = new clsLanguage();
    public clsSecurity clsSecurity = new clsSecurity();
    public string DoctorPhotoPath = "/Upload/Doctor/";
    public string SpecialtyText = "";
    public string DepartmentText = "";
    public string EducationText = "";
    public string[] Day = new string[7]; public string[] DayText = new string[7];
    public int DayOfWeek;
    #endregion
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Variable Builder
        DateTime dttm = DateTime.Now;
        DayOfWeek = (int)dttm.DayOfWeek;

        for (int d = 0; d < 7; d++)
        {
            int dow = (int)dttm.AddDays(d).DayOfWeek;
            int day = dttm.AddDays(d).Day;
            Day[dow] = day.ToString();
        }

        if (clsLanguage.LanguageCurrent == "th-TH")
        {
            SpecialtyText = "เชี่ยวชาญ";
            DepartmentText = "ประจำ";
            EducationText = "ประวัติการศึกษา";

            DayText[0] = "อาทิตย์";
            DayText[1] = "จันทร์";
            DayText[2] = "อังคาร";
            DayText[3] = "พุธ";
            DayText[4] = "พฤหัส";
            DayText[5] = "ศุกร์";
            DayText[6] = "เสาร์";
        }
        else
        {
            SpecialtyText = "Specialty";
            DepartmentText = "Center";
            EducationText = "Education";

            DayText[0] = "Sunday";
            DayText[1] = "Monday";
            DayText[2] = "Tuesday";
            DayText[3] = "Wednesday";
            DayText[4] = "Thursday";
            DayText[5] = "Friday";
            DayText[6] = "Saturday";
        }
        #endregion
        if (!IsPostBack)
        {
            if (Request.Browser.IsMobileDevice)
            {
                var clsColorBox = new clsColorBox();
                clsColorBox.Show("/MobileApplication.aspx", width: "90%", height: "90%");
            }
            DefaultBuilder();
            BindSearch();
            CarouselBuilder();
            DoctorBuilder();
            IntroPageBuilder();
        }
    }

    private void DefaultBuilder()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region NewsBuilder
        #region SQLQuery
        strSQL.Append("SELECT TOP 1 ");
        strSQL.Append("UID,Subject Name,DetailSub,PicThumbnail Photo ");
        strSQL.Append("FROM ");
        strSQL.Append("News ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= GETDATE()) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= GETDATE()) ");
        strSQL.Append("AND LanguageUID=" + clsLanguage.LanguageUIDCurrent.ToString() + " ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("CWhen DESC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            lblNewsPhoto.Text = string.Format(
                "<a href='/News/{0}/{1}/'><img src='" + dt.Rows[0]["Photo"].ToString() + "' width='320px' alt='" + dt.Rows[0]["Name"].ToString() + "' title='" + dt.Rows[0]["Name"].ToString() + "'/></a>", 
                dt.Rows[0]["UID"].ToString(), 
                clsDefault.URLRoutingFilter(dt.Rows[0]["Name"]));
            lblNewsName.Text = "<h5>" + dt.Rows[0]["Name"].ToString() + "</h5>";
            lblNewsDetailSub.Text = dt.Rows[0]["DetailSub"].ToString();
            lblNewsURL.Text = string.Format(
                "<a href='/News/{0}/{1}/'> » อ่านต่อ</a>",
                dt.Rows[0]["UID"].ToString(),
                clsDefault.URLRoutingFilter(dt.Rows[0]["Name"]));
            dt = null;
        }
        else
        {

        }
        #endregion
        #region PromotionBuilder
        #region SQLQuery
        strSQL.Append("SELECT TOP 1 ");
        strSQL.Append("UID,PromotionName Name,DetailSub,PicThumbnail Photo ");
        strSQL.Append("FROM ");
        strSQL.Append("Promotion ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= GETDATE()) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= GETDATE()) ");
        strSQL.Append("AND LanguageUID=" + clsLanguage.LanguageUIDCurrent.ToString() + " ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("MWhen DESC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            lblPromotionPhoto.Text = string.Format(
                "<a href='/Promotion/{0}/{1}/'><img src='" + dt.Rows[0]["Photo"].ToString() + "' width='320px' alt='" + dt.Rows[0]["Name"].ToString() + "' title='" + dt.Rows[0]["Name"].ToString() + "'/></a>",
                dt.Rows[0]["UID"].ToString(),
                clsDefault.URLRoutingFilter(dt.Rows[0]["Name"]));
            lblPromotionName.Text = "<h5>" + dt.Rows[0]["Name"].ToString() + "</h5>";
            lblPromotionDetailSub.Text = dt.Rows[0]["DetailSub"].ToString();
            lblPromotionURL.Text = string.Format(
                "<a href='/Promotion/{0}/{1}/'> » อ่านต่อ</a>",
                dt.Rows[0]["UID"].ToString(),
                clsDefault.URLRoutingFilter(dt.Rows[0]["Name"]));
            dt = null;
        }
        else
        {

        }
        #endregion
        #endregion
    }

    private void BindSearch()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        var languageCurrent = clsLanguage.LanguageCurrent;
        #endregion

        #region Specialty
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("DISTINCT ");
        #region Language
        if (languageCurrent == "th-TH")
        {
            strSQL.Append("SpecialtyTH");
        }
        else
        {
            strSQL.Append("SpecialtyEN");
        }
        #endregion
        strSQL.Append(" Specialty ");
        strSQL.Append("FROM Doctor ");
        strSQL.Append("WHERE Active='1' ");
        strSQL.Append("ORDER BY Specialty");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlSearchSpecialty.DataSource = dt;
            ddlSearchSpecialty.DataTextField = "Specialty";
            ddlSearchSpecialty.DataValueField = "Specialty";
            ddlSearchSpecialty.DataBind();
            dt = null;
        }
        ddlSearchSpecialty.Items.Insert(0, new ListItem("- ทั้งหมด -", "null"));
        #endregion
        #region MedicalCenter
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("MedicalCenter.DepartmentUID,MedicalCenter.Name ");
        strSQL.Append("FROM MedicalCenter ");
        strSQL.Append("INNER JOIN [Language] ON MedicalCenter.LanguageUID=[Language].UID ");
        strSQL.Append("AND [Language].Active='1' ");
        strSQL.Append("AND [Language].Name='" + languageCurrent + "' ");
        strSQL.Append("WHERE ");
        strSQL.Append("NOT MedicalCenter.DepartmentUID IS NULL ");
        strSQL.Append("AND MedicalCenter.Active='1' ");
        strSQL.Append("ORDER BY MedicalCenter.Sort,MedicalCenter.Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), clsSQL.DBType.SQLServer, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlSearchMedicalCenter.DataSource = dt;
            ddlSearchMedicalCenter.DataTextField = "Name";
            ddlSearchMedicalCenter.DataValueField = "DepartmentUID";
            ddlSearchMedicalCenter.DataBind();
        }
        ddlSearchMedicalCenter.Items.Insert(0, new ListItem("- ทั้งหมด -", "null"));
        #endregion
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        #region Variable
        //"DoctorSchedule/{name}/{special}/{dept}/{sun}/{mon}/{tue}/{wed}/{thu}/{fri}/{sat}/"
        var url = "/DoctorSchedule/{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}/{8}/{9}/";
        var urlRedirect = "";
        #endregion
        #region Procedure
        urlRedirect = string.Format(
            url,
            (txtSearchName.Text.Trim() != "" ? txtSearchName.Text.Trim() : "null"),
            ddlSearchSpecialty.SelectedItem.Value,
            ddlSearchMedicalCenter.SelectedItem.Value,
            (cbSearchSchedule.Items[0].Selected ? "1" : "0"),
            (cbSearchSchedule.Items[1].Selected ? "1" : "0"),
            (cbSearchSchedule.Items[2].Selected ? "1" : "0"),
            (cbSearchSchedule.Items[3].Selected ? "1" : "0"),
            (cbSearchSchedule.Items[4].Selected ? "1" : "0"),
            (cbSearchSchedule.Items[5].Selected ? "1" : "0"),
            (cbSearchSchedule.Items[6].Selected ? "1" : "0"));
        Response.Redirect(urlRedirect);
        #endregion
    }

    private void CarouselBuilder()
    {
        #region Variable
        var dt = new DataTable();
        var dtGlobal = new DataTable();
        var strSQL = new StringBuilder();
        var fieldName = "";
        #endregion
        #region Procedure
        dt = clsSQL.Bind("SELECT GlobalUID,GlobalName,''Photo,''Name,''URL FROM Highlight WHERE StatusFlag='A';", dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            #region GlobalBuilder
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                #region FildNameBuilder
                switch (dt.Rows[i]["GlobalName"].ToString())
                {
                    case "HealthPackage":
                        fieldName = "Name";
                        break;
                    case "Package":
                        fieldName = "PackageName";
                        break;
                    case "Event":
                        fieldName = "Subject";
                        break;
                    case "Promotion":
                        fieldName = "PromotionName";
                        break;
                    case "News":
                        fieldName = "Subject";
                        break;
                    default:
                        fieldName = "Subject";
                        break;
                }
                #endregion
                #region SQLQuery
                strSQL.Append("SELECT ");
                strSQL.Append("UID,"+fieldName+" Name,PicThumbnail Photo,'Package' GlobalName ");
                strSQL.Append("FROM ");
                strSQL.Append(dt.Rows[i]["GlobalName"].ToString()+" ");
                strSQL.Append("WHERE ");
                strSQL.Append("StatusFlag='A' ");
                strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= CONVERT(DATE,GETDATE())) ");
                strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= CONVERT(DATE,GETDATE())) ");
                strSQL.Append("AND UID="+dt.Rows[i]["GlobalUID"].ToString()+";");
                #endregion
                dtGlobal = clsSQL.Bind(strSQL.ToString(), dbType, cs);
                strSQL.Length = 0; strSQL.Capacity = 0;
                if (dtGlobal != null && dtGlobal.Rows.Count > 0)
                {
                    dt.Rows[i]["Photo"] = dtGlobal.Rows[0]["Photo"].ToString();
                    dt.Rows[i]["Name"] = dtGlobal.Rows[0]["Name"].ToString();
                    dt.Rows[i]["URL"] = "/" + dt.Rows[i]["GlobalName"].ToString() + "/" + 
                        dt.Rows[i]["GlobalUID"].ToString() + "/" +
                        clsDefault.URLRoutingFilter(dtGlobal.Rows[0]["Name"].ToString()) + "/";
                    dtGlobal = null;
                }
                else
                {
                    clsSQL.Execute("DELETE FROM Highlight WHERE GlobalUID=" + 
                        dt.Rows[i]["GlobalUID"].ToString() + " AND GlobalName='" + 
                        dt.Rows[i]["GlobalName"].ToString() + "';", 
                        dbType, cs);
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            #endregion
            #region CarouselBuilder
            ucOwlCarousel1.Source = dt;
            ucOwlCarousel1.SourceFieldName = "Name";
            ucOwlCarousel1.SourceFieldPhoto = "Photo";
            ucOwlCarousel1.SourceFieldURL = "URL";
            ucOwlCarousel1.PhotoWidth = "100%";
            ucOwlCarousel1.PhotoHeight = "auto";
            ucOwlCarousel1.PhotoWidthOverflow = "320px";
            ucOwlCarousel1.PhotoHeightOverflow = "140px";
            ucOwlCarousel1.PathPhoto = "";
            ucOwlCarousel1.AutoPlay = true;
            ucOwlCarousel1.AutoPlaySecond = 4;
            ucOwlCarousel1.ItemsMax = 3;
            ucOwlCarousel1.StopOnHover = true;
            ucOwlCarousel1.Enable = true;
            ucOwlCarousel1.Visible = true;
            #endregion
            #endregion
            dt = null;
        }
    }

    private void IntroPageBuilder()
    {
        //ucColorBox1.IFrame("Test.html","670","490"); return;
        //<iframe width="420" height="315" src="//www.youtube.com/embed/Eq5BDBYzbzE" frameborder="0" allowfullscreen></iframe>
        var clsIntroPage = new clsIntroPage();
        var dt = new DataTable();
        dt = clsIntroPage.IntroPageBuilder();

        if (dt != null && dt.Rows.Count > 0)
        {
            ucColorBox1.IFrame("/IntroPage.aspx","1000px","600px",false);
        }
    }

    private void DoctorBuilder()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region SQL Query
        /*
        strSQL.Append("SELECT TOP 5 ");
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
        strSQL.Append("ORDER BY ");
        strSQL.Append("D.CWhen DESC;");
        */
        #endregion
        #region SQLQuery new
        strSQL.Append("SELECT TOP 5 ");
        strSQL.Append("(");
        strSQL.Append("SELECT TOP 1 MedicalCenter.UID ");
        strSQL.Append("FROM DoctorDepartment ");
        strSQL.Append("INNER JOIN MedicalCenter ON DoctorDepartment.DepartmentUID = MedicalCenter.DepartmentUID AND MedicalCenter.LanguageUID = "+(clsLanguage.LanguageCurrent == "th-TH"?"1":"2") +" ");
        strSQL.Append("WHERE DoctorDepartment.DoctorUID = D.UID ");
        strSQL.Append(")MedicalCenterUID,");
        strSQL.Append("(");
        strSQL.Append("SELECT MC.Name + ' , ' ");
        strSQL.Append("FROM DoctorDepartment DD ");
        strSQL.Append("INNER JOIN MedicalCenter MC ON DD.DepartmentUID = MC.DepartmentUID ");
        if (clsLanguage.LanguageCurrent == "th-TH")
        {
            strSQL.Append("AND MC.LanguageUID=1 ");
        }
        else
        {
            strSQL.Append("AND MC.LanguageUID=2 ");
        }
        strSQL.Append("WHERE DD.DoctorUID = D.UID FOR XML PATH('')");
        strSQL.Append(")Department,");
        strSQL.Append("(");
        strSQL.Append("SELECT TOP 1 DoctorDepartment.DepartmentUID ");
        strSQL.Append("FROM DoctorDepartment ");
        strSQL.Append("INNER JOIN MedicalCenter ON DoctorDepartment.DepartmentUID = MedicalCenter.DepartmentUID AND MedicalCenter.LanguageUID = "+ (clsLanguage.LanguageCurrent == "th-TH" ? "1" : "2") + " ");
        strSQL.Append("WHERE DoctorDepartment.DoctorUID = D.UID ");
        strSQL.Append(")DepartmentUID,");
        strSQL.Append("D.UID,D.Photo,");
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
        strSQL.Append("D.MedID,D.Phone,D.EMail ");

        strSQL.Append("FROM Doctor D ");
        strSQL.Append("WHERE D.Active = '1' ORDER BY D.CWhen DESC;");
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
}