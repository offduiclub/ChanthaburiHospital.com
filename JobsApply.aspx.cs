using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class JobsApply : System.Web.UI.Page
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL();
    private clsLanguage clsLanguage = new clsLanguage();
    private clsMail clsMail = new clsMail();
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
            #region Language
            switch (clsLanguage.LanguageCurrent)
            {
                case "th-TH":
                    this.Title = this.Title + "กรอกใบสมัคร";
                    break;
                case "en-US":
                    this.Title = this.Title + "Jobs Apply";
                    break;
                default:
                    break;
            }
            #endregion
            if (clsDefault.URLRouting("id") != "")
            {
                DefaultBuilder(clsDefault.URLRouting("id"));
            }
            else
            {
                ucColorBox1.Redirect("/Jobs/", "เกิดข้อผิดพลาด", "ไม่พบหน้าที่คุณต้องการ");
            }
            #endregion
        }
    }

    private void DefaultBuilder(string id)
    {
        ControlBuilder(id);
    }

    private void ControlBuilder(string id)
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Name ");
        strSQL.Append("FROM ");
        strSQL.Append("Jobs ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort,Name;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlJobs.DataSource = dt;
            ddlJobs.DataTextField = "Name";
            ddlJobs.DataValueField = "UID";
            ddlJobs.DataBind();
            ddlJobs.SelectedValue = id;

            if (ddlJobs.SelectedItem.Text.ToLower().Contains("other"))
            {
                pnJobsName.Visible = true;
                txtJobsName.Text = "";
            }
            else
            {
                pnJobsName.Visible = false;
            }
        }
        else
        {
            ddlJobs.Items.Add(new ListItem("- ไม่พบตำแหน่งว่าง -", "null"));
        }
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        #region Procedure
        Response.Redirect("/Jobs/");
        #endregion
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Variable
        var outSQL = "";
        var clsIO = new clsIO();
        var outError="";
        var outPhoto="";
        var outMail = "";
        #endregion
        #region Procedure
        if (fuPhoto.HasFile)
        {
            if(!clsIO.UploadPhoto(
                fuPhoto,
                "/Upload/Jobs/",
                clsSQL.GetNewID("UID","JobsHistory","",dbType,cs).ToString(),
                out outError,
                out outPhoto,
                512, 200, 200))
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพโหลดภาพ : " + outError);
                return;
            }
        }
        #region InsertData
        if (!clsSQL.Insert(
            "JobsHistory",
            new string[,]{
                {"JobsUID",ddlJobs.SelectedItem.Value},
                {"JobsName","'"+txtJobsName.Text.SQLQueryFilter()+"'"},
                {"Salary","'"+txtSalary.Text.SQLQueryFilter()+"'"},
                {"Start","'"+txtStart.Text.SQLQueryFilter()+"'"},
                {"ProvinceChange","'"+rbProvinceChange.SelectedItem.Value+"'"},
                {"PrenameTH","'"+ddlPrenameTH.SelectedItem.Value+"'"},
                {"ForenameTH","'"+txtForenameTH.Text.SQLQueryFilter()+"'"},
                {"SurnameTH","'"+txtSurnameTH.Text.SQLQueryFilter()+"'"},
                {"PrenameEN","'"+ddlPrenameEN.SelectedItem.Value+"'"},
                {"ForenameEN","'"+txtForenameEN.Text.SQLQueryFilter()+"'"},
                {"SurnameEN","'"+txtSurnameEN.Text.SQLQueryFilter()+"'"},
                {"MarriageStatus","'"+rbMarriageStatus.SelectedItem.Value+"'"},
                {"Gender","'"+rbGender.SelectedItem.Value+"'"},
                {"Birthdate","'"+(ucBirthdate.Text.Trim()!=""?ucBirthdate.DateTime.ToString("yyyy-MM-dd"):"1900-01-01 00:00")+"'"},
                {"Birthplace","'"+txtBirthplace.Text.SQLQueryFilter()+"'"},
                {"Nationality","'"+txtNationality.Text.SQLQueryFilter()+"'"},
                {"Race","'"+txtRace.Text.SQLQueryFilter()+"'"},
                {"Religion","'"+txtReligion.Text.SQLQueryFilter()+"'"},
                {"Weight","'"+txtWeight.Text.SQLQueryFilter()+"'"},
                {"Height","'"+txtHeight.Text.SQLQueryFilter()+"'"},
                {"NID","'"+txtNID.Text.SQLQueryFilter()+"'"},
                {"NIDCreateBy","'"+txtNIDCreateBy.Text.SQLQueryFilter()+"'"},
                {"NIDExpire","'"+(ucNIDExpire.Text!=""?ucNIDExpire.DateTime.ToString("yyyy-MM-dd"):"")+"'"},
                {"Phone","'"+txtPhone.Text.SQLQueryFilter()+"'"},
                {"Email","'"+txtEmail.Text.SQLQueryFilter()+"'"},
                {"Address","'"+txtAddress.Text.SQLQueryFilter()+"'"},
                {"EmergencyName","'"+txtEmergencyName.Text.SQLQueryFilter()+"'"},
                {"EmergencyPhone","'"+txtEmergencyPhone.Text.SQLQueryFilter()+"'"},
                {"EmergencyEmail","'"+txtEmergencyEmail.Text.SQLQueryFilter()+"'"},
                {"EmergencyAddress","'"+txtEmergencyAddress.Text.SQLQueryFilter()+"'"},
                {"EmergencyRelationship","'"+txtEmergencyRelationship.Text.SQLQueryFilter()+"'"},
                {"Photo",(outPhoto!=""?"'"+outPhoto+"'":"null")},
                {"Education","'"+ucEducation.Text.SQLQueryFilter()+"'"},
                {"Experience","'"+ucExperience.Text.SQLQueryFilter()+"'"},
                {"Resume","null"},
                {"CWhen","GETDATE()"},
                {"CUser","0"},
                {"MWhen","GETDATE()"},
                {"MUser","0"},
                {"Sort","0"},
                {"StatusFlag","'A'"}
            },
            new string[,] { { } },
            dbType,
            cs,
            out outSQL))
        {
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะรันคำสั่ง : " + Server.HtmlEncode(outSQL));
            return;
        }
        else
        {
            #region MailSender
            string JobsUID = clsSQL.Return("SELECT MAX(UID) FROM JobsHistory", dbType, cs);

            if (!clsMail.SendTemplate(
                "JobsApply",
                clsMail.GetEmailList("AutoSystemFrom"),
                clsMail.GetEmailList("JobsTo"),
                new string[,] 
                { 
                    {"[Photo]","<img src='"+System.Configuration.ConfigurationManager.AppSettings["website"]+"/Upload/Jobs/"+outPhoto+"'/>"},
                    {"[UID]", JobsUID},
                    {"[JobsName]", ddlJobs.SelectedItem.Text+(txtJobsName.Text.Trim()!=""?" ("+txtJobsName.Text.SQLQueryFilter()+")":"") },
                    {"[PrenameTH]",ddlPrenameTH.SelectedItem.Text},
                    {"[ForenameTH]",txtForenameTH.Text},
                    {"[SurnameTH]",txtSurnameTH.Text},
                    {"[Birthdate]",ucBirthdate.DateTime.ToString("dd/MM/yyyy")},
                    {"[Birthplace]",txtBirthplace.Text},
                    {"[Gender]",rbGender.SelectedItem.Text},
                    {"[Phone]",txtPhone.Text},
                    {"[Email]",txtEmail.Text},
                    {"[Start]",txtStart.Text.SQLQueryFilter()},
                    {"[Education]",ucEducation.Text.SQLQueryFilter()},
                    {"[Experience]",ucExperience.Text.SQLQueryFilter()},
                    {"[CWhen]",DateTime.Now.ToString("dd/MM/yyyy HH:mm")}
                },
                out outMail))
            {
                lblMessage.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะพยายามส่งอีเมล์<br/>" + outMail, clsDefault.AlertType.Fail);
                lblMessage.Focus();
                return;
            }
            #endregion
            ucColorBox1.Redirect("/Jobs/");
        }
        #endregion
        #endregion
    }
    protected void ddlJobs_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("/Jobs/"+ddlJobs.SelectedItem.Value+"/Other/");
    }
}