using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_MedicalCenterManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    public string tableDefault = "MedicalCenter";
    public string webDefault = "MedicalCenter.aspx";
    public string webManage = "MedicalCenterManage.aspx";
    public string pathUpload = "/Upload/MedicalCenter/";
    public int photoWidth = 200;
    public int photoHeight = 150;
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
            if (Request.QueryString["id"] != null)
            {
                if (clsDefault.QueryStringChecker("command") == "edit")
                {
                    vdPhoto.Enabled = false;
                    BindControl();
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
            }
            else
            {
                BindControl();
            }
        }
    }

    private void BindControl()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Language
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("Detail,");
        strSQL.Append("Icon ");
        strSQL.Append("FROM ");
        strSQL.Append("Language ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rbLanguage.Items.Add(
                    new ListItem(
                        "<img src='" + dt.Rows[i]["Icon"].ToString() + "'/> " + dt.Rows[i]["Detail"].ToString(),
                        dt.Rows[i]["UID"].ToString()
                    )
                );
                if (i == 0) rbLanguage.Items[i].Selected = true;
            }
            try
            {
                rbLanguage.SelectedValue = clsDefault.QueryStringChecker("language");
                rbLanguage.Enabled = false;
            }
            catch (Exception ex)
            {}
            dt = null;
        }
        #endregion
    }

    protected void BindDetail(string id)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Language.UID LanguageUID,");
        strSQL.Append(tableDefault+".DepartmentUID,");
        strSQL.Append(tableDefault+".Icon,");
        strSQL.Append(tableDefault+".Name,");
        strSQL.Append(tableDefault+".Detail,");
        strSQL.Append(tableDefault+".[Content],");
        strSQL.Append(tableDefault+".Location,");
        strSQL.Append(tableDefault+".OfficeHours,");
        strSQL.Append(tableDefault+".Phone,");
        strSQL.Append(tableDefault+".EMail,");
        strSQL.Append(tableDefault+".MetaKeywords,");
        strSQL.Append(tableDefault+".MetaDescription,");
        strSQL.Append(tableDefault+".Sort,");
        strSQL.Append(tableDefault+".Active ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault+" ");
        strSQL.Append("INNER JOIN ");
        strSQL.Append("Language ON " + tableDefault + ".LanguageUID=Language.UID ");
        strSQL.Append("WHERE ");
        strSQL.Append(tableDefault+".UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Data Builder
            if (rbLanguage.Items.Count > 0)
            {
                rbLanguage.SelectedValue = dt.Rows[0]["LanguageUID"].ToString();
            }
            if (dt.Rows[0]["DepartmentUID"] != DBNull.Value)
            {
                txtDepartmentUID.Text = dt.Rows[0]["DepartmentUID"].ToString();
            }
            if (dt.Rows[0]["Icon"] != DBNull.Value)
            {
                lblPhoto.Text = "<img src='" + dt.Rows[0]["Icon"].ToString() + "'/><br/>";
            }
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            ucContent.Text = dt.Rows[0]["Content"].ToString();
            txtLocation.Text = dt.Rows[0]["Location"].ToString();
            txtOfficeHours.Text = dt.Rows[0]["OfficeHours"].ToString();
            txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            txtEMail.Text = dt.Rows[0]["EMail"].ToString();
            txtMetaKeyword.Text = dt.Rows[0]["MetaKeywords"].ToString();
            txtMetaDescription.Text = dt.Rows[0]["MetaDescription"].ToString();
            txtSort.Text = dt.Rows[0]["Sort"].ToString();
            cbActive.Checked = (dt.Rows[0]["Active"].ToString() == "1" ? true : false);
            #endregion
        }
        else
        {
            ucColorBox1.Redirect("/Management/" + webDefault + "?group="+clsDefault.QueryStringChecker("group"), "ไม่พบหน้าเว็บที่คุณต้องการ");
        }
    }

    private void Delete(string id)
    {
        pnDetail.Visible = false;
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        clsIO clsIO = new clsIO();
        StringBuilder strSQL = new StringBuilder();
        #endregion

        #region Delete Photo
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Icon ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault+" ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id);
        #endregion
        string photoDelete=clsSQL.Return(strSQL.ToString(), dbType, cs);
        if (!string.IsNullOrEmpty(photoDelete))
        {
            clsIO.FileExist(photoDelete, true);
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
        #region Delete Database
        #region SQL Query
        strSQL.Append("DELETE FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id);
        #endregion
        if (clsSQL.Execute(strSQL.ToString(), dbType, cs))
        {
            ucColorBox1.Redirect(webDefault + "?group=" + clsDefault.QueryStringChecker("group"));
        }
        else
        {
            ucColorBox1.Redirect(webDefault + "?group=" + clsDefault.QueryStringChecker("group"), "เกิดข้อผิดพลาดขณะลบข้อมูล");
            return;
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        int id = 0;
        string outSQL;
        string photoName = "";
        #endregion

        #region Update
        if (Request.QueryString["id"] != null && clsDefault.QueryStringChecker("command") == "edit")
        {
            id = int.Parse(Request.QueryString["id"].ToString());
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                clsIO clsIO = new clsIO();
                string outErrorMessage; string outFilename;
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    "MedicalCenter" + id.ToString(),
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
                    {"UID",id.ToString()},
                    {"LanguageUID",rbLanguage.SelectedItem.Value},
                    {"MedicalCenterGroupUID",clsDefault.QueryStringChecker("group")},
                    {"DepartmentUID",(txtDepartmentUID.Text!=""?"'"+clsSQL.CodeFilter(txtDepartmentUID.Text)+"'":"null")},
                    {"Icon",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"Icon")},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"[Content]","'"+clsSQL.CodeFilter(ucContent.Text)+"'"},
                    {"Location","'"+clsSQL.CodeFilter(txtLocation.Text)+"'"},
                    {"OfficeHours","'"+clsSQL.CodeFilter(txtOfficeHours.Text)+"'"},
                    {"Phone","'"+clsSQL.CodeFilter(txtPhone.Text)+"'"},
                    {"EMail","'"+clsSQL.CodeFilter(txtEMail.Text)+"'"},
                    {"MetaKeywords","'"+clsSQL.CodeFilter(txtMetaKeyword.Text)+"'"},
                    {"MetaDescription","'"+clsSQL.CodeFilter(txtMetaDescription.Text)+"'"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
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
                clsIO clsIO = new clsIO();
                string outErrorMessage;string outFilename;
                if(clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    "MedicalCenter" + id.ToString(),
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
                    {"LanguageUID",rbLanguage.SelectedItem.Value},
                    {"MedicalCenterGroupUID",clsDefault.QueryStringChecker("group")},
                    {"DepartmentUID",(txtDepartmentUID.Text.Trim()!=""?clsSQL.CodeFilter(txtDepartmentUID.Text):"null")},
                    {"Icon",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"null")},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"[Content]","'"+clsSQL.CodeFilter(ucContent.Text)+"'"},
                    {"Location","'"+clsSQL.CodeFilter(txtLocation.Text)+"'"},
                    {"OfficeHours","'"+clsSQL.CodeFilter(txtOfficeHours.Text)+"'"},
                    {"Phone","'"+clsSQL.CodeFilter(txtPhone.Text)+"'"},
                    {"EMail","'"+clsSQL.CodeFilter(txtEMail.Text)+"'"},
                    {"MetaKeywords","'"+clsSQL.CodeFilter(txtMetaKeyword.Text)+"'"},
                    {"MetaDescription","'"+clsSQL.CodeFilter(txtMetaDescription.Text)+"'"},
                    {"CUser","'" + clsSecurity.LoginUID + "'"},
                    {"CWhen","GETDATE()"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"Sort",clsSQL.CodeFilter(txtSort.Text)},
                    {"Active","'" + (cbActive.Checked ? "1" : "0") + "'"}
                }, new string[,] { { } },
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
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        ucColorBox1.Close();
    }
}