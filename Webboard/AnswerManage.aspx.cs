using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Webboard_AnswerManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();
    clsNet clsNet = new clsNet();

    public int photoWidth = 800;
    public int photoHeight = 0;
    public string pathUpload = "/Webboard/Upload/";
    string approveEnable;

    public string tableDefault = "WebboardAnswer";
    public string webDefault = "/Webboard/";
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
            #region WebboardConfig
            approveEnable = WebboardConfig("AnonymousEnable");
            if (approveEnable != "1")
            {
                if (!clsSecurity.LoginChecker())
                {
                    pnDetail.Visible = false;
                    lblDefault.Text = clsDefault.AlertMessageColor("กรุณาสมัครสมาชิก หรือ ล็อคอิน ก่อนตอบคำถาม | <a href='/Register/' target='_Blank'>คลิกที่นี่เพื่อสมัครสมาชิก</a>", clsDefault.AlertType.Info);
                    ucColorBox1.SizeChange();
                    return;
                }
            }
            #endregion

            if (clsDefault.URLRouting("group") != "" && clsDefault.URLRouting("question") != "")
            {
                webDefault += clsDefault.URLRouting("group") + "/" + clsDefault.URLRouting("question") + "/";

                #region Authorize
                if (clsSecurity.LoginChecker())
                {
                    dvAnonymous.Visible = false;
                    vadCName.Enabled = false; vadCEmail.Enabled = false;
                }
                if (clsSecurity.LoginGroup == "Admin")
                {
                    dvAdmin.Visible = true;
                }
                #endregion

                if (clsDefault.URLRouting("id") != "")
                {
                    if (clsDefault.URLRouting("command") == "Edit")
                    {
                        vdPhoto.Enabled = false;
                        BindDetail(clsDefault.URLRouting("id"));
                    }
                    else if (clsDefault.URLRouting("command") == "Delete")
                    {
                        pnDetail.Visible = false;
                        Delete(clsDefault.URLRouting("id"));
                    }
                    else if (clsDefault.URLRouting("command") == "Approve")
                    {
                        pnDetail.Visible = false;
                        Approve(clsDefault.URLRouting("id"));
                    }
                    else
                    {
                        ucColorBox1.Redirect(webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
                    }
                }
                else
                {
                    
                }
            }
            else
            {
                ucColorBox1.Close();
            }
        }
    }

    private string WebboardConfig(string name)
    {
        string value = "";

        value = clsSQL.Return("SELECT Value FROM WebboardConfig WHERE Name='" + name + "' AND Active='1'", dbType, cs);

        return value;
    }

    private string ReservedWords(string content)
    {
        #region Variable
        string contentOut = content;
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region ReservedWords
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Word,WordReplace ");
        strSQL.Append("FROM ");
        strSQL.Append("WebboardReservedWords ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                contentOut = contentOut.Replace(
                                dt.Rows[i]["Word"].ToString(),
                                (
                                dt.Rows[i]["WordReplace"] != DBNull.Value ?
                                dt.Rows[i]["WordReplace"].ToString() :
                                "*"
                                )
                            );
            }
            dt = null;
        }
        #endregion

        return contentOut;
    }

    private void BindDetail(string id)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append(tableDefault + ".WebboardQuestionUID,");
        strSQL.Append(tableDefault + ".Photo,");
        strSQL.Append(tableDefault + ".Detail,");
        strSQL.Append(tableDefault + ".Status,");
        strSQL.Append(tableDefault + ".Sort,");
        strSQL.Append(tableDefault + ".Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append(tableDefault + ".UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Photo"].ToString() != "")
            {
                lblPhoto.Text = "<div style='width:300px;height:300px;'><img src='" + dt.Rows[0]["Photo"].ToString() + "' style='max-width:100%;max-height:100%;'/></div>";
            }
            ucDetail.Text = dt.Rows[0]["Detail"].ToString();
            if (dt.Rows[0]["Status"] != DBNull.Value)
            {
                if (dt.Rows[0]["Status"].ToString() == "I")
                {
                    ddlStatus.SelectedValue = "I";
                }
            }
            else
            {
                ddlStatus.SelectedValue = "N";
            }
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["Active"].ToString() == "1" ? true : false);
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
    }

    private void Delete(string id)
    {
        pnDetail.Visible = false;
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect(webDefault, "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        clsIO clsIO = new clsIO();
        string photoName;
        #endregion
        #region Delete Photo
        photoName = clsSQL.Return(
            "SELECT Photo FROM " + tableDefault + " WHERE UID=" + parameterChar + "UID",
            new string[,] { { parameterChar + "UID", id } },
            dbType,
            cs);
        if (photoName != "")
        {
            clsIO.FileExist(photoName, true);
        }
        #endregion
        #region Delete Database
        #region SQL Query
        strSQL.Append("DELETE FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + parameterChar + "UID");
        #endregion
        if (clsSQL.Execute(strSQL.ToString(), new string[,] { { parameterChar + "UID", id } }, dbType, cs))
        {
            ucColorBox1.ReloadParent();
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "เกิดข้อผิดพลาดขณะลบข้อมูล");
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
    }

    private void Approve(string id)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect(webDefault, "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        string outSQL;
        #endregion

        if (clsSQL.Update(
                tableDefault,
                new string[,] { { "Active", "'1'" } },
                new string[,] { { parameterChar + "UID", id } },
                "UID=" + parameterChar + "UID",
                dbType,
                cs,
                out outSQL))
        {
            ucColorBox1.Redirect(webDefault);
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "เกิดข้อผิดพลาดขณะอัพเดทข้อมูล");
        }
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Validation
        if (!ucCaptchaEncrypt1.Checker())
        {
            lblCaptcha.Text = clsDefault.AlertMessageColor("คำตอบไม่ถูกต้อง", clsDefault.AlertType.Fail);
            lblCaptcha.Focus();
            return;
        }
        #endregion
        #region Authorize
        if (!clsSecurity.LoginChecker() && (txtCName.Text.Trim() == "" || txtCEmail.Text.Trim() == ""))
        {
            //ucColorBox1.Redirect(webDefault, "กรุณาล็อคอิน หรือ ระบุชื่อและอีเมล์ก่อนส่งข้อมูล");
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "กรุณาล็อคอิน หรือ กรอกชื่อและอีเมล์ก่อน", AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        #endregion
        #region Variable
        clsIO clsIO = new clsIO();
        StringBuilder strSQL = new StringBuilder();
        int id = 0;
        string outSQL;
        string outErrorMessage;
        string outFilename;
        string photoName = "";
        #endregion

        #region Update
        if (clsDefault.URLRouting("id") != "" && clsDefault.URLRouting("command") == "Edit")
        {
            id = int.Parse(clsDefault.URLRouting("id"));
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    "A" + id.ToString(),
                    out outErrorMessage,
                    out outFilename,
                    maxWidth: photoWidth,
                    maxHeight: photoHeight))
                {
                    photoName = outFilename;
                }
                else
                {
                    ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพโหลดไฟล์รูปภาพ<br/>" + outErrorMessage, AlertImage: ucColorBox.Alerts.Fail);
                    return;
                }
            }
            #endregion
            if (clsSQL.Update(tableDefault,
                new string[,]{
                    {"Photo",(photoName==""?"Photo":"'"+pathUpload+photoName+"'")},
                    {"Detail","'"+clsSQL.CodeFilter(ReservedWords(ucDetail.Text))+"'"},
                    {"Status",(ddlStatus.SelectedItem.Value!="N"?"'"+ddlStatus.SelectedItem.Value+"'":"null")},
                    {"MUser",(clsSecurity.LoginChecker()?clsSecurity.LoginUID:"0")},
                    {"MWhen","GETDATE()"},
                    {"MIPAddress","'"+clsNet.IPGet()+"'"},
                    {"MComputername","'"+clsNet.ComNameGet()+"'"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"Active","'" + (cbActive.Checked ? "1" : "0") + "'"}
                }, new string[,] { { parameterChar + "UID", id.ToString() } },
                "UID=" + parameterChar + "UID",
                dbType, cs, out outSQL))
            {
                ucColorBox1.ReloadParent();
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
        #region Insert
        else
        {
            #region Find New ID
            id = clsSQL.GetNewID("UID", tableDefault, "", dbType, cs);
            if (id == 0)
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "ไม่สามารถหา UID ใหม่ได้", AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            #endregion
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    "A" + id.ToString(),
                    out outErrorMessage,
                    out outFilename,
                    maxWidth: photoWidth,
                    maxHeight: photoHeight))
                {
                    photoName = outFilename;
                }
                else
                {
                    ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะอัพโหลดไฟล์รูปภาพ<br/>" + outErrorMessage, AlertImage: ucColorBox.Alerts.Fail);
                    return;
                }
            }
            #endregion
            if (clsSQL.Insert(tableDefault,
                new string[,]{
                    {"UID",id.ToString()},
                    {"WebboardQuestionUID",clsDefault.URLRouting("question")},
                    {"Photo",(photoName==""?"''":"'"+pathUpload+photoName+"'")},
                    {"Detail","'"+clsSQL.CodeFilter(ReservedWords(ucDetail.Text))+"'"},
                    {"Status",(ddlStatus.SelectedItem.Value!="N"?"'"+ddlStatus.SelectedItem.Value+"'":"null")},
                    {"CName",(txtCName.Text.Trim()==""?"null":"'"+clsSQL.CodeFilter(txtCName.Text)+"'")},
                    {"CEmail",(txtCEmail.Text.Trim()==""?"null":"'"+clsSQL.CodeFilter(txtCEmail.Text)+"'")},
                    {"CUser",(clsSecurity.LoginChecker()?clsSecurity.LoginUID:"0")},
                    {"CWhen","GETDATE()"},
                    {"CIPAddress","'"+clsNet.IPGet()+"'"},
                    {"CComputername","'"+clsNet.ComNameGet()+"'"},
                    {"MUser",(clsSecurity.LoginChecker()?clsSecurity.LoginUID:"0")},
                    {"MWhen","GETDATE()"},
                    {"MIPAddress","'"+clsNet.IPGet()+"'"},
                    {"MComputername","'"+clsNet.ComNameGet()+"'"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"Active","'" + (approveEnable!="1"?"1":"0") + "'"}
                }, new string[,] { { } },
                dbType, cs, out outSQL))
            {
                string outMessage;
                clsMail clsMail = new clsMail();
                string questionName = clsSQL.Return("SELECT Name FROM Question WHERE UID=" + clsDefault.URLRouting("question"), dbType, cs);
                if (approveEnable != "1")
                {
                    clsMail.SendTemplate(
                        "WebboardAnswerAlert",
                        clsMail.GetEmailList("GlobalFrom"),
                        clsMail.GetEmailList("WebboardTo"),
                        new string[,]{
                        {"[Username]",clsSecurity.LoginUsername},
                        {"[CName]",clsSQL.CodeFilter(txtCName.Text)},
                        {"[CEmail]",clsSQL.CodeFilter(txtCName.Text)},
                        {"[IPAddress]",clsNet.IPGet()},
                        {"[ComputerName]",clsNet.ComNameGet()},
                        {"[Name]",clsDefault.URLRoutingFilter(questionName)},
                        {"[Detail]",clsSQL.CodeFilter(ucDetail.Text)},
                        {"[CWhen]",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")},
                        {"[WebboardGroupUID]",clsDefault.URLRouting("group")},
                        {"[UID]",clsDefault.URLRouting("id")}
                    }, out outMessage);

                    ucColorBox1.ReloadParent();
                }
                else
                {
                    clsMail.SendTemplate(
                            "WebboardAnswerApprove",
                            clsMail.GetEmailList("GlobalFrom"),
                            clsMail.GetEmailList("WebboardTo"),
                            new string[,]{
                        {"[Username]",clsSecurity.LoginUsername},
                        {"[CName]",clsSQL.CodeFilter(txtCName.Text)},
                        {"[CEmail]",clsSQL.CodeFilter(txtCName.Text)},
                        {"[IPAddress]",clsNet.IPGet()},
                        {"[ComputerName]",clsNet.ComNameGet()},
                        {"[Name]",clsDefault.URLRoutingFilter(questionName)},
                        {"[Detail]",clsSQL.CodeFilter(ucDetail.Text)},
                        {"[CWhen]",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")},
                        {"[WebboardGroupUID]",clsDefault.URLRouting("group")},
                        {"[WebboardQuestionUID]",clsDefault.URLRouting("question")},
                        {"[UID]",id.ToString()}
                    }, out outMessage);

                    ucColorBox1.Redirect(webDefault + clsDefault.URLRouting("group") + "/",
                        "ดำเนินการเสร็จสิ้น",
                        "เมื่อเจ้าหน้าที่ทำการตรวจสอบข้อมูลคำถามของคุณเรียบร้อยแล้ว จะทำการเผยแพร่โดยเร็ว");
                }
            }
            else
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล<br/>", outSQL, AlertImage: ucColorBox.Alerts.Fail);
            }
        }
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }
}