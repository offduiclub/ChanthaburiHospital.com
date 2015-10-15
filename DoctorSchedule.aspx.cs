using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class DoctorSchedule : ucLanguage
{
    #region Global Variable
    clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsLanguage clsLanguage = new clsLanguage();
    public clsSecurity clsSecurity = new clsSecurity();
    public string DoctorPhotoPath = "/Upload/Doctor/";
    public string SpecialtyText="";
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
            //clsSyncer clsSyncer = new clsSyncer();
            //clsSyncer.DoctorScheduleSyncer();
            var lastUpdate=clsSQL.Return("SELECT TOP 1 CWhen FROM DoctorSchedule", dbType, cs);
            lblDetail.Text += "<div style='font-size:8pt;color:#E89D2D;'>LastUpdate : " + (!string.IsNullOrEmpty(lastUpdate) ? DateTime.Parse(lastUpdate).ToString("dd/MM/yyyy HH:mm") : "-") + "</div>";
            BindSearch();
            #region SearchByURLRouting
            //"DoctorSchedule/{name}/{special}/{dept}/{sun}/{mon}/{tue}/{wed}/{thu}/{fri}/{sat}/"
            if (clsDefault.URLRouting("sat") != "")
            {
                if (clsDefault.URLRouting("name") != "null")
                {
                    txtSearchName.Text = clsDefault.URLRouting("name");
                }
                if (clsDefault.URLRouting("special") != "null")
                {
                    ddlSearchSpecialty.SelectedValue = clsDefault.URLRouting("special");
                }
                if (clsDefault.URLRouting("dept") != "null")
                {
                    ddlSearchMedicalCenter.SelectedValue = clsDefault.URLRouting("dept");
                }
                if (clsDefault.URLRouting("sun") != "0")
                {
                    cbSearchSchedule.Items[0].Selected = true;
                }
                if (clsDefault.URLRouting("mon") != "0")
                {
                    cbSearchSchedule.Items[1].Selected = true;
                }
                if (clsDefault.URLRouting("tue") != "0")
                {
                    cbSearchSchedule.Items[2].Selected = true;
                }
                if (clsDefault.URLRouting("wed") != "0")
                {
                    cbSearchSchedule.Items[3].Selected = true;
                }
                if (clsDefault.URLRouting("thu") != "0")
                {
                    cbSearchSchedule.Items[4].Selected = true;
                }
                if (clsDefault.URLRouting("fri") != "0")
                {
                    cbSearchSchedule.Items[5].Selected = true;
                }
                if (clsDefault.URLRouting("sat") != "0")
                {
                    cbSearchSchedule.Items[6].Selected = true;
                }
            }
            #endregion
            DoctorBuilder();
        }
    }

    private void DoctorBuilderDeptOneLine()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("(");
        strSQL.Append("SELECT TOP 1 MedicalCenter.UID ");
        strSQL.Append("FROM DoctorDepartment ");
        strSQL.Append("INNER JOIN MedicalCenter ON DoctorDepartment.DepartmentUID = MedicalCenter.DepartmentUID AND MedicalCenter.LanguageUID = " + (clsLanguage.LanguageCurrent == "th-TH" ? "1" : "2") + " ");
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
        strSQL.Append("INNER JOIN MedicalCenter ON DoctorDepartment.DepartmentUID = MedicalCenter.DepartmentUID AND MedicalCenter.LanguageUID = " + (clsLanguage.LanguageCurrent == "th-TH" ? "1" : "2") + " ");
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
        strSQL.Append("WHERE D.Active = '1' ");
        #region SearchBuilder
        #region Name
        if (txtSearchName.Text.Trim() != "")
        {
            if (clsLanguage.LanguageCurrent == "th-TH")
            {
                strSQL.Append("AND (");
                strSQL.Append("D.FNameTH LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%' ");
                strSQL.Append("OR D.LNameTH LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%'");
                strSQL.Append(") ");
            }
            else
            {
                strSQL.Append("AND (");
                strSQL.Append("D.FNameEN LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%' ");
                strSQL.Append("OR D.LNameEN LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%'");
                strSQL.Append(") ");
            }
        }
        #endregion
        #region Specialty
        if (ddlSearchSpecialty.SelectedItem.Value != "null")
        {
            strSQL.Append("AND (");
            strSQL.Append("SpecialtyTH LIKE '%" + ddlSearchSpecialty.SelectedItem.Text + "%' ");
            strSQL.Append("OR SpecialtyEN LIKE '%" + ddlSearchSpecialty.SelectedItem.Text + "%' ");
            strSQL.Append(") ");
        }
        #endregion
        #region MedicalCenter
        if (ddlSearchMedicalCenter.SelectedItem.Value != "null")
        {
            strSQL.Append("AND (");
            strSQL.Append("SELECT COUNT(MedicalCenter.UID) ");
            strSQL.Append("FROM DoctorDepartment ");
            strSQL.Append("INNER JOIN MedicalCenter ON DoctorDepartment.DepartmentUID = MedicalCenter.DepartmentUID AND MedicalCenter.LanguageUID = " + (clsLanguage.LanguageCurrent == "th-TH" ? "1" : "2") + " ");
            strSQL.Append("WHERE DoctorDepartment.DoctorUID = D.UID ");
            strSQL.Append("AND MC.Name LIKE '%" + ddlSearchMedicalCenter.SelectedItem.Text + "%'");
            strSQL.Append(")>0,");
        }
        #endregion
        #region Schedule
        #region Checked
        bool cbChecked = false;
        for (int c = 0; c < cbSearchSchedule.Items.Count; c++)
        {
            if (cbSearchSchedule.Items[c].Selected)
            {
                cbChecked = true;
                break;
            }
        }
        #endregion
        if (cbChecked)
        {
            strSQL.Append("AND (");
            strSQL.Append("SELECT ");
            strSQL.Append("COUNT(UID) ");
            strSQL.Append("FROM ");
            strSQL.Append("DoctorSchedule ");
            strSQL.Append("WHERE ");
            strSQL.Append("DoctorUID=D.UID ");
            //strSQL.Append("AND DATEPART(DW,DoctorSchedule.DateActive)=1");
            strSQL.Append("AND "+clsSQL.QueryBuilderWhere(cbSearchSchedule,"DATEPART(DW,DoctorSchedule.DateActive)","OR",false,true));
            strSQL.Append(")>0 ");
        }
        #endregion
        #endregion
        strSQL.Append("ORDER BY ");
        strSQL.Append("D.FNameTH;");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvDoctor.Visible = true;
            gvDoctor.DataSource = dt;
            gvDoctor.DataBind();

            DoctorScheduleBuilder();
        }
        else
        {
            gvDoctor.Visible = false;
            lblDoctor.Text = (clsLanguage.LanguageCurrent=="th-TH"?clsDefault.AlertMessageColor("ไม่พบข้อมูลแพทย์ที่ต้องการ.", clsDefault.AlertType.Info):clsDefault.AlertMessageColor("Not found data.", clsDefault.AlertType.Info));
        }
        #endregion
    }
    private void DoctorBuilder()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        /*
        strSQL.Append("(ISNULL((");
        strSQL.Append("SELECT TOP 1 MedicalCenter.Name FROM DoctorDepartment INNER JOIN MedicalCenter ON DoctorDepartment.DepartmentUID=MedicalCenter.DepartmentUID AND DoctorDepartment.DoctorUID=D.UID WHERE MedicalCenter.LanguageUID="+clsLanguage.LanguageUIDCurrent.ToString());
        strSQL.Append("),''))Department,");
        */
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
        strSQL.Append("AND NOT MC.UID IS NULL ");
        #region SearchBuilder
        #region Name
        if (txtSearchName.Text.Trim() != "")
        {
            if (clsLanguage.LanguageCurrent == "th-TH")
            {
                strSQL.Append("AND (");
                strSQL.Append("D.FNameTH LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%' ");
                strSQL.Append("OR D.LNameTH LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%'");
                strSQL.Append(") ");
            }
            else
            {
                strSQL.Append("AND (");
                strSQL.Append("D.FNameEN LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%' ");
                strSQL.Append("OR D.LNameEN LIKE '%" + clsSQL.CodeFilter(txtSearchName.Text) + "%'");
                strSQL.Append(") ");
            }
        }
        #endregion
        #region Specialty
        if (ddlSearchSpecialty.SelectedItem.Value != "null")
        {
            strSQL.Append("AND (");
            strSQL.Append("SpecialtyTH LIKE '%" + ddlSearchSpecialty.SelectedItem.Text + "%' ");
            strSQL.Append("OR SpecialtyEN LIKE '%" + ddlSearchSpecialty.SelectedItem.Text + "%' ");
            strSQL.Append(") ");
        }
        #endregion
        #region MedicalCenter
        if (ddlSearchMedicalCenter.SelectedItem.Value != "null")
        {
            strSQL.Append("AND MC.Name LIKE '%" + ddlSearchMedicalCenter.SelectedItem.Text + "%' ");
        }
        #endregion
        #region Schedule
        #region Checked
        bool cbChecked = false;
        for (int c = 0; c < cbSearchSchedule.Items.Count; c++)
        {
            if (cbSearchSchedule.Items[c].Selected)
            {
                cbChecked = true;
                break;
            }
        }
        #endregion
        if (cbChecked)
        {
            strSQL.Append("AND (");
            strSQL.Append("SELECT ");
            strSQL.Append("COUNT(UID) ");
            strSQL.Append("FROM ");
            strSQL.Append("DoctorSchedule ");
            strSQL.Append("WHERE ");
            strSQL.Append("DoctorUID=D.UID ");
            //strSQL.Append("AND DATEPART(DW,DoctorSchedule.DateActive)=1");
            strSQL.Append("AND " + clsSQL.QueryBuilderWhere(cbSearchSchedule, "DATEPART(DW,DoctorSchedule.DateActive)", "OR", false, true));
            strSQL.Append(")>0 ");
        }
        #endregion
        #endregion
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

            DoctorScheduleBuilder();
        }
        else
        {
            gvDoctor.Visible = false;
            lblDoctor.Text = (clsLanguage.LanguageCurrent == "th-TH" ? clsDefault.AlertMessageColor("ไม่พบข้อมูลแพทย์ที่ต้องการ.", clsDefault.AlertType.Info) : clsDefault.AlertMessageColor("Not found data.", clsDefault.AlertType.Info));
        }
        #endregion
    }

    private void DoctorScheduleBuilder()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion

        for (int i = 0; i < gvDoctor.Rows.Count; i++)
        {
            #region Find Control
            Label gvDoctorUID = (Label)gvDoctor.Rows[i].FindControl("gvDoctorUID");
            Label gvDepartmentUID = (Label)gvDoctor.Rows[i].FindControl("gvDepartmentUID");
            Label gvSchedule0 = (Label)gvDoctor.Rows[i].FindControl("gvSchedule0");
            Label gvSchedule1 = (Label)gvDoctor.Rows[i].FindControl("gvSchedule1");
            Label gvSchedule2 = (Label)gvDoctor.Rows[i].FindControl("gvSchedule2");
            Label gvSchedule3 = (Label)gvDoctor.Rows[i].FindControl("gvSchedule3");
            Label gvSchedule4 = (Label)gvDoctor.Rows[i].FindControl("gvSchedule4");
            Label gvSchedule5 = (Label)gvDoctor.Rows[i].FindControl("gvSchedule5");
            Label gvSchedule6 = (Label)gvDoctor.Rows[i].FindControl("gvSchedule6");
            #endregion

            #region SQL Query
            strSQL.Append("SELECT ");
            strSQL.Append("DateActive,TimeStart,TimeEnd,Comment ");
            strSQL.Append("FROM ");
            strSQL.Append("DoctorSchedule ");
            strSQL.Append("WHERE ");
            strSQL.Append("DoctorUID=" + gvDoctorUID.Text + " ");
            strSQL.Append("AND DepartmentUID='"+gvDepartmentUID.Text+"'");
            strSQL.Append("AND DateActive >= GETDATE()-1 ");
            strSQL.Append("AND DateActive <= GETDATE()+6 ");
            strSQL.Append("AND Active='1' ");
            strSQL.Append("ORDER BY ");
            strSQL.Append("DateActive,TimeStart");
            #endregion
            #region Data Builder
            dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
            strSQL.Length = 0; strSQL.Capacity = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int day = 0; day < dt.Rows.Count; day++)
                {
                    DateTime dttm = DateTime.Parse(dt.Rows[day]["DateActive"].ToString());
                    string gvSchedule="<div title='"+(dt.Rows[day]["Comment"].ToString()!=""?dt.Rows[day]["Comment"].ToString():"")+"'>"+
                                DateTime.Parse(dt.Rows[day]["TimeStart"].ToString()).ToString("HH:mm") + 
                                " - " +
                                DateTime.Parse(dt.Rows[day]["TimeEnd"].ToString()).ToString("HH:mm")+
                                (dt.Rows[day]["Comment"].ToString() != "" ? " <div style='color:#FF3E59;font-size:7pt;'>"+dt.Rows[day]["Comment"].ToString()+"</div>" : "") + "</div>";

                    switch ((int)dttm.DayOfWeek)
                    {
                        default:
                            break;
                        case 0:
                            gvSchedule0.Text += gvSchedule;
                            break;
                        case 1:
                            gvSchedule1.Text += gvSchedule;
                            break;
                        case 2:
                            gvSchedule2.Text += gvSchedule;
                            break;
                        case 3:
                            gvSchedule3.Text += gvSchedule;
                            break;
                        case 4:
                            gvSchedule4.Text += gvSchedule;
                            break;
                        case 5:
                            gvSchedule5.Text += gvSchedule;
                            break;
                        case 6:
                            gvSchedule6.Text += gvSchedule;
                            break;
                    }
                }
                dt = null;
            }

            if (gvSchedule0.Text.Trim() == "") gvSchedule0.Text = "-";
            if (gvSchedule1.Text.Trim() == "") gvSchedule1.Text = "-";
            if (gvSchedule2.Text.Trim() == "") gvSchedule2.Text = "-";
            if (gvSchedule3.Text.Trim() == "") gvSchedule3.Text = "-";
            if (gvSchedule4.Text.Trim() == "") gvSchedule4.Text = "-";
            if (gvSchedule5.Text.Trim() == "") gvSchedule5.Text = "-";
            if (gvSchedule6.Text.Trim() == "") gvSchedule6.Text = "-";
            #endregion
        }
    }

    protected void gvDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDoctor.PageIndex = e.NewPageIndex;

        DoctorBuilder();
    }

    private void BindSearch()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        string languageCurrent = clsLanguage.LanguageCurrent;
        #endregion

        #region Specialty
        #region SQL Query
        strSQL.Append("");
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
        DoctorBuilder();
    }
}