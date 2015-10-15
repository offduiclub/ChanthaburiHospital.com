using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class UserControl_ucCaptcha_ucCaptchaEncrypt : System.Web.UI.UserControl
{
    /*################## Example ##################
    ตัวดักสแปม ใช้กรอกคำตอบ ให้ตรงกับคำถามที่ตั้ง โดยมีการดึงข้อมูลจากไฟล์ CSV
    มีการตรวจสอบคำตอบจากฝั่ง Server และมีการ Encrypt คำตอบ
    ##############################################*/
    clsDefault clsDefault = new clsDefault();
    clsSecurity clsSecurity = new clsSecurity();
    private string _tempFile = @"~\UserControl\ucCaptcha\DB\Captcha.csv";

    private string _validateGroup;
    public string ValidateGroup
    {
        get { return _validateGroup; }
        set { _validateGroup = value; }
    }

    private string _errorMessage = "คำตอบของคุณไม่ถูกต้อง !!";
    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(_validateGroup))
            {
                vldRequire.ValidationGroup = _validateGroup;
            }

            vldRequire.ErrorMessage = clsDefault.AlertMessageColor(_errorMessage, clsDefault.AlertType.Fail);

            GetCaptcha();
        }
    }

    private void GetCaptcha()
    {
        #region Create DataTable
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("Question");
        DataColumn dc2 = new DataColumn("Answer");
        DataColumn dc3 = new DataColumn("Remark");

        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);
        dt.Columns.Add(dc3);
        DataRow dr;
        #endregion

        #region Insert Data
        string fullPath = Server.MapPath(_tempFile);
        FileInfo fiTemp = new FileInfo(fullPath);
        if (fiTemp.Exists)
        {
            int i;
            string[] Line = File.ReadAllLines(fullPath);
            string[] Field;

            for (i = 0; i < Line.Length; i++)
            {
                Field = Line[i].Split(',');

                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["Question"] = Field[0];
                dr["Answer"] = Field[1];
                dr["Remark"] = Field[2];
                dt.AcceptChanges();
            }
        }
        #endregion

        if (dt != null && dt.Rows.Count > 0)
        {
            Random ran = new Random();
            int id_ran = ran.Next(0, dt.Rows.Count - 1);
            lblCaptcha.Text = dt.Rows[id_ran]["Question"].ToString();
            lblCaptchaRemark.Text = dt.Rows[id_ran]["Remark"].ToString();
            hidCaptcha.Value = clsSecurity.Encrypt(dt.Rows[id_ran]["Answer"].ToString());
        }
    }

    public bool Checker()
    {
        bool rtnValue = false;

        if (!string.IsNullOrEmpty(txtCaptcha.Text.Trim()))
        {
            if (txtCaptcha.Text.Trim() == clsSecurity.Decrypt(hidCaptcha.Value))
            {
                rtnValue = true;
            }
        }

        return rtnValue;
    }
}