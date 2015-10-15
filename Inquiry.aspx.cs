using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Inquiry : ucLanguage
{
    #region GlobalVariable
    private clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL();
    private clsMail clsMail = new clsMail();
    private clsLanguage clsLanguage = new clsLanguage();
    private clsSecurity clsSecurity = new clsSecurity();

    private clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    private string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MedicalCenterBuilder();
        }
    }
    private void MedicalCenterBuilder()
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
        strSQL.Append("MedicalCenter ");
        strSQL.Append("WHERE ");
        strSQL.Append("LanguageUID="+clsLanguage.LanguageUIDCurrent+" ");
        strSQL.Append("AND Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort,Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlMedicalCenter.DataSource = dt;
            ddlMedicalCenter.DataTextField = "Name";
            ddlMedicalCenter.DataValueField = "UID";
            ddlMedicalCenter.DataBind();
        }
        ddlMedicalCenter.Items.Insert(0,new ListItem("- ไม่ระบุ -","null"));
        #endregion
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Variable
        string outMail = "";
        string outSQL="";
        string UID = "";
        #endregion

        if (txtFromName.Text.Trim().Length > 0 &&
            txtFromPhone.Text.Trim().Length > 0 &&
            txtFromEmail.Text.Trim().Length > 0 &&
            txtMessage.Text.Trim().Length > 0)
        {
            UID = clsSQL.GetNewID("UID", "Inquiry", "", dbType, cs).ToString();
            if(!clsSQL.Insert(
                "Inquiry",
                new string[,]{
                    {"UID",UID},
                    {"MedicalCenterUID",(ddlMedicalCenter.SelectedItem.Value=="null"?"null":ddlMedicalCenter.SelectedItem.Value)},
                    {"Name","'"+clsSQL.CodeFilter(txtFromName.Text)+"'"},
                    {"Email","'"+clsSQL.CodeFilter(txtFromEmail.Text)+"'"},
                    {"Phone","'"+clsSQL.CodeFilter(txtFromPhone.Text)+"'"},
                    {"Message","'"+clsSQL.CodeFilter(txtMessage.Text)+"'"},
                    {"Status","'RECEIVED'"},
                    {"CWhen","GETDATE()"},
                    {"CUser",/*clsSecurity.LoginUID*/"0"},
                    {"MWhen","GETDATE()"},
                    {"MUser",/*clsSecurity.LoginUID*/"0"},
                    {"Sort","0"},
                    {"Active","'1'"}
                },
                new string[,]{{}},
                dbType,
                cs,
                out outSQL))
            {
                ucColorBox1.Alert(Message: "เกิดข้อผิดพลาดขณะบันทึกข้อมูล", AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            #region MailToUser
            try
            {
                //string outMailMessage="";
                //clsMail.Send(
                //    "brhercc@brh.co.th",
                //    txtFromEmail.Text.Trim(),
                //    "โรงพยาบาลกรุงเทพจันทบุรี ได้รับข้อมูลของคุณแล้ว",
                //    "คุณได้ส่งคำถาม '" + txtMessage.Text.Trim() + "' มายังโรงพยาบาล",
                //    out outMailMessage);
                if (clsMail.SendTemplate(
                "InquiryConfirm",
                clsMail.GetEmailList("GlobalFrom"),
                txtFromEmail.Text,
                new string[,] 
                { 
                    { "[UID]", UID },
                    { "[MedicalCenter]", ddlMedicalCenter.SelectedItem.Text },
                    { "[FromName]", txtFromName.Text },
                    { "[FromPhone]", txtFromPhone.Text },
                    { "[FromEmail]", txtFromEmail.Text },
                    { "[CWhen]", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") },
                    { "[Message]", txtMessage.Text }
                },
                out outMail))
                {
                    ucColorBox1.Redirect("/Inquiry/", "ได้รับข้อความของคุณแล้ว");
                }
                else
                {
                    lblAlert.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะพยายามส่งอีเมล์<br/>" + outMail, clsDefault.AlertType.Fail);
                    lblAlert.Focus();
                }
            }
            catch (Exception) { }
            #endregion
            #region MailToAdmin
            if (clsMail.SendTemplate(
                "Inquiry",
                clsMail.GetEmailList("AutoSystemFrom"),
                clsMail.GetEmailList("InquiryTo"),
                new string[,] 
                { 
                    { "[UID]", UID },
                    { "[MedicalCenter]", ddlMedicalCenter.SelectedItem.Text },
                    { "[FromName]", txtFromName.Text },
                    { "[FromPhone]", txtFromPhone.Text },
                    { "[FromEmail]", txtFromEmail.Text },
                    { "[CWhen]", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") },
                    { "[Message]", txtMessage.Text }
                },
                out outMail))
            {
                ucColorBox1.Redirect("/Inquiry/", "ได้รับข้อความของคุณแล้ว");
            }
            else
            {
                lblAlert.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะพยายามส่งอีเมล์<br/>" + outMail, clsDefault.AlertType.Fail);
                lblAlert.Focus();
            }
            #endregion
        }
        else
        {
            lblAlert.Text = clsDefault.AlertMessageColor("กรุณากรอกข้อมูลให้ครบก่อนค่ะ", clsDefault.AlertType.Warn);
        }
    }
}