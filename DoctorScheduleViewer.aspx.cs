using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class DoctorScheduleViewer : System.Web.UI.Page
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
    public string DoctorPhoto;
    public string Name1;
    public string Name2;
    public string Specialty;
    public string Department;
    #endregion
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ucColorBox1.SizeChange();

        if (!IsPostBack)
        {
            if (clsDefault.URLRouting("doctorUID") != "" && clsDefault.URLRouting("departmentUID") != "" && clsDefault.URLRouting("medicalCenterUID")!="")
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
                DoctorBuilder(clsDefault.URLRouting("doctorUID"), clsDefault.URLRouting("departmentUID"), clsDefault.URLRouting("medicalCenterUID"));
                DoctorScheduleBuilder(clsDefault.URLRouting("doctorUID"), clsDefault.URLRouting("departmentUID"), clsDefault.URLRouting("medicalCenterUID"));
                BindUser();
            }
            else
            {
                ucColorBox1.Close();
            }
        }
    }

    private void DoctorBuilder(string DoctorUID,string DepartmentUID,string MedicalCenterUID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
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
        strSQL.Append("INNER JOIN DoctorDepartment DD ON D.UID=DD.DoctorUID ");
        if (DepartmentUID != "0")
        {
            strSQL.Append("AND DD.DepartmentUID='" + DepartmentUID + "' ");
        }
        strSQL.Append("INNER JOIN MedicalCenter MC ON DD.DepartmentUID=MC.DepartmentUID ");
        if (MedicalCenterUID != "0")
        {
            strSQL.Append("AND MC.UID=" + MedicalCenterUID + " ");
        }
        strSQL.Append("WHERE ");
        strSQL.Append("D.Active='1' ");
        strSQL.Append("AND D.UID="+DoctorUID+" ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("D.FNameTH");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            DoctorDetail.Visible = true;
            lblDoctorPhoto.Text = "<img src='" + DoctorPhotoPath + (dt.Rows[0]["Photo"].ToString() != "" ? dt.Rows[0]["Photo"].ToString() : "default.jpg") +
                "' alt='" + dt.Rows[0]["Name1"].ToString() + "' title='" + dt.Rows[0]["Name1"].ToString() + "'/>";
            lblName1.Text = dt.Rows[0]["Name1"].ToString();
            lblName2.Text = dt.Rows[0]["Name2"].ToString();
            lblSpecialty.Text = dt.Rows[0]["Specialty"].ToString();
            lblDepartment.Text = dt.Rows[0]["Department"].ToString();
            lblEducation.Text = dt.Rows[0]["Education"].ToString();
        }
        else
        {
            DoctorDetail.Visible = false;
            lblDefault.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูล", clsDefault.AlertType.Warn);
        }
        #endregion
    }

    private void DoctorScheduleBuilder(string DoctorUID, string DepartmentUID, string MedicalCenterUID)
    {
        ucCalendarSpecialDays1.SpecialDays.Add(
                new SpecialDay() { Date = DateTime.Now, Message = "ทดสอบ", RepeatMonth = false, RepeatYear = false }
            );
        ucCalendarSpecialDays1.SpecialDays.Add(
            new SpecialDay() { Date = new DateTime(2014, 11, 30), Message = "ทดสอบ 2", RepeatMonth = false, RepeatYear = false }
        );
        ucCalendarSpecialDays1.SpecialDays.Add(
            new SpecialDay() { Date = new DateTime(2014, 11, 10), Message = "ทดสอบ 3", RepeatMonth = false, RepeatYear = false }
        );

        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("DateActive,TimeStart,TimeEnd,Comment ");
        strSQL.Append("FROM ");
        strSQL.Append("DoctorSchedule ");
        strSQL.Append("WHERE ");
        strSQL.Append("DoctorUID=" + DoctorUID + " ");
        strSQL.Append("AND DepartmentUID='" + DepartmentUID + "'");
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
                /*
                string gvSchedule = "<div title='" + (dt.Rows[day]["Comment"].ToString() != "" ? dt.Rows[day]["Comment"].ToString() : "") + "'>" +
                            DateTime.Parse(dt.Rows[day]["TimeStart"].ToString()).ToString("HH:mm") +
                            " - " +
                            DateTime.Parse(dt.Rows[day]["TimeEnd"].ToString()).ToString("HH:mm") +
                            (dt.Rows[day]["Comment"].ToString() != "" ? " *" : "") + "</div>";
                */
                string gvSchedule = "<div title='" + (dt.Rows[day]["Comment"].ToString() != "" ? dt.Rows[day]["Comment"].ToString() : "") + "'>" +
                                DateTime.Parse(dt.Rows[day]["TimeStart"].ToString()).ToString("HH:mm") +
                                " - " +
                                DateTime.Parse(dt.Rows[day]["TimeEnd"].ToString()).ToString("HH:mm") +
                                (dt.Rows[day]["Comment"].ToString() != "" ? " <div style='color:#FF3E59;font-size:7pt;'>" + dt.Rows[day]["Comment"].ToString() + "</div>" : "") + "</div>";

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

    private void BindUser()
    {
        if (clsSecurity.LoginChecker())
        {
            #region Variable
            StringBuilder strSQL = new StringBuilder();
            DataTable dt = new DataTable();
            #endregion
            #region DataBuilder
            #region SQL Query
            strSQL.Append("SELECT ");
            strSQL.Append("PName,FName,LName,HN,Mobile,Phone,Email ");
            strSQL.Append("FROM ");
            strSQL.Append("[User] ");
            strSQL.Append("WHERE ");
            strSQL.Append("[UID]="+parameterChar+"UID");
            #endregion
            dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "UID", clsSecurity.LoginUID } }, dbType, cs);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["PName"] != DBNull.Value && dt.Rows[0]["PName"].ToString() != "")
                {
                    ddlPName.SelectedValue = dt.Rows[0]["PName"].ToString();
                }
                if (dt.Rows[0]["FName"] != DBNull.Value && dt.Rows[0]["FName"].ToString() != "")
                {
                    txtFName.Text = dt.Rows[0]["FName"].ToString();
                }
                if (dt.Rows[0]["LName"] != DBNull.Value && dt.Rows[0]["LName"].ToString() != "")
                {
                    txtLName.Text = dt.Rows[0]["LName"].ToString();
                }
                if (dt.Rows[0]["Email"] != DBNull.Value && dt.Rows[0]["Email"].ToString() != "")
                {
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                }
                if (dt.Rows[0]["Phone"] != DBNull.Value && dt.Rows[0]["Phone"].ToString() != "")
                {
                    txtPhone.Text += dt.Rows[0]["Phone"].ToString()+",";
                }
                if (dt.Rows[0]["Mobile"] != DBNull.Value && dt.Rows[0]["Mobile"].ToString() != "")
                {
                    txtPhone.Text += dt.Rows[0]["Mobile"].ToString() + ",";
                }
                txtPhone.Text = clsDefault.LastStringRemover(txtPhone.Text, ",");
            }
            #endregion
        }
    }

    protected void btBook_Click(object sender, EventArgs e)
    {
        #region Variable
        string outSQL;
        string outMailMessage;
        clsMail clsMail;
        #endregion
        #region Insert Data
        if (clsSQL.Insert(
            "DoctorAppointment",
            new string[,]{
                {"UID",clsSQL.GetNewID("UID","DoctorAppointment","",dbType,cs).ToString()},
                {"HN","'"+clsSQL.CodeFilter(txtHN.Text)+"'"},
                {"PName","'"+clsSQL.CodeFilter(ddlPName.SelectedItem.Text)+"'"},
                {"FName","'"+clsSQL.CodeFilter(txtFName.Text)+"'"},
                {"LName","'"+clsSQL.CodeFilter(txtLName.Text)+"'"},
                {"Email","'"+clsSQL.CodeFilter(txtEmail.Text)+"'"},
                {"Phone","'"+clsSQL.CodeFilter(txtPhone.Text)+"'"},
                {"DoctorUID",clsDefault.URLRouting("doctorUID")},
                {"DoctorName","'"+clsSQL.CodeFilter(lblName1.Text)+"'"},
                {"DepartmentUID","'"+clsDefault.URLRouting("departmentUID")+"'"},
                {"DepartmentName","'"+clsSQL.CodeFilter(lblDepartment.Text)+"'"},
                {"AppointmentDate","'"+clsSQL.CodeFilter(ucDateTimeFlat1.DateTime.ToString("yyyy-MM-dd HH:mm"))+"'"},
                {"Comment","'"+clsSQL.CodeFilter(txtComment.Text)+"'"},
                {"BirthDate","'"+ucBirthDate.DateTime.ToString("yyyy-MM-dd")+"'"},
                {"NID","'"+txtNID.Text.SQLQueryFilter()+"'"},
                {"CWhen","GETDATE()"},
                {"CUser",(clsSecurity.LoginChecker()?clsSecurity.LoginUID:"0")},
                {"MWhen","GETDATE()"},
                {"MUser",(clsSecurity.LoginChecker()?clsSecurity.LoginUID:"0")},
                {"Sort","0"},
                {"Active","'1'"}
            },
            new string[,]{},
            dbType,
            cs,
            out outSQL))
        {
            lblBookAlert.Text = clsDefault.AlertMessageColor("ระบบบันทึกข้อมูลการทำนัดของคุณแล้ว", clsDefault.AlertType.Success);
            string doctor = lblName1.Text;
            #region Mail to Admin
            clsMail = new clsMail();
            if (!clsMail.SendTemplate(
                "DoctorScheduleAdmin",
                clsMail.GetEmailList("AutoSystemFrom"),
                clsMail.GetEmailList("DoctorScheduleTo"),
                new string[,]{
                    {"[PName]",ddlPName.SelectedItem.Text},
                    {"[FName]",txtFName.Text},
                    {"[LName]",txtLName.Text},
                    {"[HN]",txtHN.Text},
                    {"[Email]",txtEmail.Text},
                    {"[Phone]",txtPhone.Text},
                    {"[Doctor]",doctor},
                    {"[Department]",lblDepartment.Text},
                    {"[BookDateTime]",ucDateTimeFlat1.DateTime.ToString("dd/MM/yyyy HH:mm")},
                    {"[Comment]",txtComment.Text},
                    {"[BirthDate]",ucBirthDate.Text},
                    {"[NID]",txtNID.Text}
                },
                out outMailMessage))
            {
                lblBookAlert.Text += clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะส่งเมล์ไปยังลูกค้า", clsDefault.AlertType.Fail);
            }
            //List<clsMail.EmailList> mails = new List<global::clsMail.EmailList>();
            //mails = clsMail.GetEmailList("DoctorScheduleTo");
            //for (int i = 0; i < mails.Count; i++)
            //{
            //    lblBookAlert.Text+="<br/>"+mails[i].EmailAddress;
            //}
            lblBookAlert.Text += "<br/>"+outMailMessage;
            #endregion
                #region Mail to User
                clsMail = new clsMail();
            if(!clsMail.SendTemplate(
                "DoctorScheduleUser",
                clsMail.GetEmailList("GlobalFrom"),
                txtEmail.Text.Trim(),
                new string[,]{
                    {"[PName]",ddlPName.SelectedItem.Text},
                    {"[FName]",txtFName.Text},
                    {"[LName]",txtLName.Text},
                    {"[HN]",txtHN.Text},
                    {"[Email]",txtEmail.Text},
                    {"[Phone]",txtPhone.Text},
                    {"[Doctor]",doctor},
                    {"[Department]",lblDepartment.Text},
                    {"[BookDateTime]",ucDateTimeFlat1.DateTime.ToString("dd/MM/yyyy HH:mm")},
                    {"[Comment]",txtComment.Text}
                },
                out outMailMessage))
            {
                lblBookAlert.Text += clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะส่งเมล์ไปยังลูกค้า", clsDefault.AlertType.Fail);
            }
            #endregion
        }
        else
        {
            lblBookAlert.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>" + outSQL, clsDefault.AlertType.Fail);
        }
        #endregion
    }
}