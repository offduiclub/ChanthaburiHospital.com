using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Feedback : ucLanguage
{
    #region GlobalVariable
    private clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL();
    private clsMail clsMail = new clsMail();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Variable
        string outMail = "";
	    #endregion

        if(txtFromName.Text.Trim().Length>0 &&
            txtFromPhone.Text.Trim().Length>0 &&
            txtFromEmail.Text.Trim().Length>0 &&
            txtMessage.Text.Trim().Length > 0)
        {
            if(clsMail.SendTemplate(
                "Feedback",
                clsMail.GetEmailList("AutoSystemFrom"),
                clsMail.GetEmailList("FeedbackTo"),
                new string[,] 
                { 
                    { "[FromName]", txtFromName.Text },
                    { "[FromPhone]", txtFromPhone.Text },
                    { "[FromEmail]", txtFromEmail.Text },
                    { "[CWhen]", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") },
                    { "[Message]", txtMessage.Text }
                },
                out outMail))
            {
                txtFromName.Text = "";
                txtFromEmail.Text = "";
                txtFromPhone.Text = "";
                txtMessage.Text = "";

                //lblAlert.Text = clsDefault.AlertMessageColor("ระบบได้รับข้อความของคุณแล้ว");
                //lblAlert.Focus();
                ucColorBox1.Redirect("/Feedback/", "ได้รับข้อความของคุณแล้ว");
            }
            else
            {
                lblAlert.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะพยายามส่งอีเมล์<br/>" + outMail, clsDefault.AlertType.Fail);
                lblAlert.Focus();
            }
        }
        else
        {
            lblAlert.Text = clsDefault.AlertMessageColor("กรุณากรอกข้อมูลให้ครบก่อนค่ะ", clsDefault.AlertType.Warn);
        }
    }
}