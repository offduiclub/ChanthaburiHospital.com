using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class UserControl_ucCaptcha_ucCaptcha : System.Web.UI.UserControl
{
    /*################## Example ##################
    
    ##############################################*/
    clsDefault clsDefault = new clsDefault();
    private string _tempFile=@"\UserControl\ucCaptcha\DB\Captcha.csv";

    private string _validateGroup;
    public string ValidateGroup
    {
        get { return _validateGroup; }
        set { _validateGroup = value; }
    }

    private string _errorMessage;
    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_validateGroup))
        {
            vldCaptcha.ValidationGroup = _validateGroup;
            vldRequire.ValidationGroup = _validateGroup;
        }
        if (!string.IsNullOrEmpty(_errorMessage))
        {
            vldCaptcha.ErrorMessage = clsDefault.AlertMessageColor(_errorMessage, clsDefault.AlertType.Fail);
            vldRequire.ErrorMessage = clsDefault.AlertMessageColor(_errorMessage, clsDefault.AlertType.Fail);
        }
        else
        {
            vldCaptcha.ErrorMessage = clsDefault.AlertMessageColor("คำตอบของคุณไม่ถูกต้อง !!", clsDefault.AlertType.Fail);
            vldRequire.ErrorMessage = clsDefault.AlertMessageColor("คำตอบของคุณไม่ถูกต้อง !!", clsDefault.AlertType.Fail);
        }

        GetCaptcha();
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
            vldCaptcha.ValueToCompare = dt.Rows[id_ran]["Answer"].ToString();
            lblCaptchaRemark.Text = dt.Rows[id_ran]["Remark"].ToString();
        }
    }
}