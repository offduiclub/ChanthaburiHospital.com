using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_JobsHistoryManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    public string tableDefault = "JobsHistory";
    public string webDefault = "JobsHistory.aspx";
    public string webManage = "JobsHistoryManage.aspx";
    public string pathUpload = "/Upload/Jobs/";
    public int photoWidth = 200;
    public int photoHeight = 200;
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
            if (Request.QueryString["id"] != null)
            {
                #region Condition
                if (clsDefault.QueryStringChecker("command") == "edit")
                {
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
                #endregion
            }
            #endregion
        }
    }

    protected void BindDetail(string id)
    {
        ControlBuilder(id);
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("JobsUID,");
        strSQL.Append("Salary,");
        strSQL.Append("Start,");
        strSQL.Append("ProvinceChange,");
        strSQL.Append("PrenameTH,");
        strSQL.Append("ForenameTH,");
        strSQL.Append("SurnameTH,");
        strSQL.Append("PrenameEN,");
        strSQL.Append("ForenameEN,");
        strSQL.Append("SurnameEN,");
        strSQL.Append("MarriageStatus,");
        strSQL.Append("Gender,");
        strSQL.Append("Birthdate,");
        strSQL.Append("Birthplace,");
        strSQL.Append("Nationality,");
        strSQL.Append("Race,");
        strSQL.Append("Religion,");
        strSQL.Append("Weight,");
        strSQL.Append("Height,");
        strSQL.Append("NID,");
        strSQL.Append("NIDCreateBy,");
        strSQL.Append("NIDExpire,");
        strSQL.Append("Phone,");
        strSQL.Append("Email,");
        strSQL.Append("Address,");
        strSQL.Append("EmergencyName,");
        strSQL.Append("EmergencyPhone,");
        strSQL.Append("EmergencyEmail,");
        strSQL.Append("EmergencyAddress,");
        strSQL.Append("EmergencyRelationship,");
        strSQL.Append("Photo,");
        strSQL.Append("Resume,");
        strSQL.Append("CWhen ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append(tableDefault + ".UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Data Builder
            ddlJobs.SelectedValue = dt.Rows[0]["JobsUID"].ToString();
            lblPhoto.Text = "<img src='" + pathUpload + dt.Rows[0]["Photo"].ToString() + "'/>";
            txtSalary.Text = dt.Rows[0]["Salary"].ToString();
            txtStart.Text = dt.Rows[0]["Start"].ToString();
            rbProvinceChange.SelectedValue = dt.Rows[0]["ProvinceChange"].ToString();
            ddlPrenameTH.SelectedValue = dt.Rows[0]["PrenameTH"].ToString();
            txtForenameTH.Text = dt.Rows[0]["ForenameTH"].ToString();
            txtSurnameTH.Text = dt.Rows[0]["SurnameTH"].ToString();
            ddlPrenameEN.SelectedValue = dt.Rows[0]["PrenameEN"].ToString();
            txtForenameEN.Text = dt.Rows[0]["ForenameEN"].ToString();
            txtSurnameEN.Text = dt.Rows[0]["SurnameEN"].ToString();
            rbMarriageStatus.SelectedValue = dt.Rows[0]["MarriageStatus"].ToString();
            rbGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            ucBirthdate.Text = DateTime.Parse(dt.Rows[0]["Birthdate"].ToString()).ToString("yyyy-MM-dd");
            txtBirthplace.Text = dt.Rows[0]["Birthplace"].ToString();
            txtNationality.Text = dt.Rows[0]["Nationality"].ToString();
            txtRace.Text = dt.Rows[0]["Race"].ToString();
            txtReligion.Text = dt.Rows[0]["Religion"].ToString();
            txtWeight.Text = dt.Rows[0]["Weight"].ToString();
            txtHeight.Text = dt.Rows[0]["Height"].ToString();
            txtNID.Text = dt.Rows[0]["NID"].ToString();
            txtNIDCreateBy.Text = dt.Rows[0]["NIDCreateBy"].ToString();
            ucNIDExpire.Text = (dt.Rows[0]["NIDExpire"].ToString()==""?"":DateTime.Parse(dt.Rows[0]["NIDExpire"].ToString()).ToString("yyyy-MM-dd"));
            txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtEmergencyName.Text = dt.Rows[0]["EmergencyName"].ToString();
            txtEmergencyPhone.Text = dt.Rows[0]["EmergencyPhone"].ToString();
            txtEmergencyEmail.Text = dt.Rows[0]["EmergencyEmail"].ToString();
            txtEmergencyAddress.Text = dt.Rows[0]["EmergencyAddress"].ToString();
            txtEmergencyRelationship.Text = dt.Rows[0]["EmergencyRelationship"].ToString();
            lblCWhen.Text = dt.Rows[0]["CWhen"].ToString();
            #endregion
        }
        else
        {
            ucColorBox1.Redirect("/Management/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
        #endregion
    }

    private void Delete(string id)
    {
        pnDetail.Visible = false;
        #region Authorize
        if (!clsSecurity.LoginChecker("admin") && !clsSecurity.LoginChecker("hr"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        var clsIO = new clsIO();
        var strSQL = new StringBuilder();
        #endregion
        #region Procedure
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
            return;
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
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
        }
        else
        {
            ddlJobs.Items.Add(new ListItem("- ไม่พบตำแหน่งว่าง -", "null"));
        }
        #endregion
    }
}