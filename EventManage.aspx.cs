using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class EventManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    public string tableDefault = "Event";
    public string webDefault = "Event/";
    public string webManage = "EventManage.aspx";
    public string pathUpload = "/Upload/Event/";
    public int photoWidth = 320;
    public int photoHeight = 140;
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
                    ucColorBox1.Redirect("/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
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
        var strSQL = new StringBuilder();
        var dt = new DataTable();
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
            { }
            dt = null;
        }
        #endregion
    }

    protected void BindDetail(string id)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        vdPhoto.Enabled = false;
        vdPhotoFull.Enabled = false;

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("B.UID LanguageUID,");
        strSQL.Append("A.Subject,");
        strSQL.Append("A.Detail,");
        strSQL.Append("A.DetailSub,");
        strSQL.Append("A.PicThumbnail,");
        strSQL.Append("A.PicFull,");
        strSQL.Append("A.ActiveDateFrom,");
        strSQL.Append("A.ActiveDateTo,");
        strSQL.Append("A.MetaKeywords,");
        strSQL.Append("A.MetaDescription,");
        strSQL.Append("A.StatusFlag ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " A ");
        strSQL.Append("INNER JOIN ");
        strSQL.Append("Language B ON A.LanguageUID=B.UID ");
        strSQL.Append("WHERE ");
        strSQL.Append("A.UID=" + parameterChar + "ID");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "ID", id } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Data Builder
            if (rbLanguage.Items.Count > 0)
            {
                rbLanguage.SelectedValue = dt.Rows[0]["LanguageUID"].ToString();
            }
            if (dt.Rows[0]["PicThumbnail"] != DBNull.Value)
            {
                lblPhoto.Text = "<img src='" + dt.Rows[0]["PicThumbnail"].ToString() + "'/><br/>";
            }
            if (dt.Rows[0]["PicFull"] != DBNull.Value)
            {
                lblPhotoFull.Text = "<img src='" + dt.Rows[0]["PicFull"].ToString() + "' width='300px'/><br/>";
            }
            if (dt.Rows[0]["ActiveDateFrom"] != DBNull.Value)
            {
                ucDateStart.DateTime = DateTime.Parse(dt.Rows[0]["ActiveDateFrom"].ToString());
            }
            if (dt.Rows[0]["ActiveDateTo"] != DBNull.Value)
            {
                ucDateEnd.DateTime = DateTime.Parse(dt.Rows[0]["ActiveDateTo"].ToString());
            }
            txtName.Text = dt.Rows[0]["Subject"].ToString();
            txtDetail.Text = dt.Rows[0]["DetailSub"].ToString();
            ucContent.Text = dt.Rows[0]["Detail"].ToString();
            txtMetaKeyword.Text = dt.Rows[0]["MetaKeywords"].ToString();
            txtMetaDescription.Text = dt.Rows[0]["MetaDescription"].ToString();
            cbActive.Checked = (dt.Rows[0]["StatusFlag"].ToString() == "A" ? true : false);
            #endregion
        }
        else
        {
            ucColorBox1.Redirect("/" + webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
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

        #region Delete PhotoThumbnail
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("PicThumbnail ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id);
        #endregion
        string photoDelete = clsSQL.Return(strSQL.ToString(), dbType, cs);
        if (!string.IsNullOrEmpty(photoDelete))
        {
            clsIO.FileExist(photoDelete, true);
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
        #region Delete PhotoFull
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("PicFull ");
        strSQL.Append("FROM ");
        strSQL.Append(tableDefault + " ");
        strSQL.Append("WHERE ");
        strSQL.Append("UID=" + id);
        #endregion
        photoDelete = clsSQL.Return(strSQL.ToString(), dbType, cs);
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
            ucColorBox1.Redirect(webDefault);
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "เกิดข้อผิดพลาดขณะลบข้อมูล");
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
        var strSQL = new StringBuilder();
        var id = 0;
        var outSQL = "";
        var photoName = "";
        var photoFullName = "";
        #endregion

        #region Update
        if (Request.QueryString["id"] != null && clsDefault.QueryStringChecker("command") == "edit")
        {
            id = int.Parse(Request.QueryString["id"].ToString());
            clsIO clsIO = new clsIO();
            string outErrorMessage; string outFilename;
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    tableDefault + id.ToString() + "_T",
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
            #region PhotoFull Upload
            if (fuPhotoFull.HasFile)
            {
                if (clsIO.UploadPhoto(
                    fuPhotoFull, pathUpload,
                    tableDefault + id.ToString(),
                    out outErrorMessage,
                    out outFilename))
                {
                    photoFullName = outFilename;
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
                    {"LanguageUID",rbLanguage.SelectedItem.Value},
                    {"PicThumbnail",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"PicThumbnail")},
                    {"PicFull",(!string.IsNullOrEmpty(photoFullName)?"'"+pathUpload+photoFullName+"'":"PicFull")},
                    {"Subject","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+ucContent.Text.SQLQueryFilter()+"'"},
                    {"DetailSub","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"ActiveDateFrom",(ucDateStart.Text!=""?"'"+ucDateStart.Text.SQLQueryFilter()+"'":"null")},
                    {"ActiveDateTo",(ucDateEnd.Text!=""?"'"+ucDateEnd.Text.SQLQueryFilter()+"'":"null")},
                    {"MetaKeywords","'"+clsSQL.CodeFilter(txtMetaKeyword.Text)+"'"},
                    {"MetaDescription","'"+clsSQL.CodeFilter(txtMetaDescription.Text)+"'"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "D") + "'"}
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

            clsIO clsIO = new clsIO();
            string outErrorMessage; string outFilename;
            #region Photo Upload
            if (fuPhoto.HasFile)
            {
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    tableDefault + id.ToString() + "_T",
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
            #region PhotoFull Upload
            if (fuPhotoFull.HasFile)
            {
                if (clsIO.UploadPhoto(
                    fuPhotoFull, pathUpload,
                    tableDefault + id.ToString(),
                    out outErrorMessage,
                    out outFilename))
                {
                    photoFullName = outFilename;
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
                    {"LanguageUID",rbLanguage.SelectedItem.Value},
                    {"PicThumbnail",(!string.IsNullOrEmpty(photoName)?"'"+pathUpload+photoName+"'":"null")},
                    {"PicFull",(!string.IsNullOrEmpty(photoFullName)?"'"+pathUpload+photoFullName+"'":"null")},
                    {"Subject","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+ucContent.Text.SQLQueryFilter()+"'"},
                    {"DetailSub","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"ActiveDateFrom",(ucDateStart.Text!=""?"'"+ucDateStart.Text.SQLQueryFilter()+"'":"null")},
                    {"ActiveDateTo",(ucDateEnd.Text!=""?"'"+ucDateEnd.Text.SQLQueryFilter()+"'":"null")},
                    {"MetaKeywords","'"+clsSQL.CodeFilter(txtMetaKeyword.Text)+"'"},
                    {"MetaDescription","'"+clsSQL.CodeFilter(txtMetaDescription.Text)+"'"},
                    {"CUser","'" + clsSecurity.LoginUID + "'"},
                    {"CWhen","GETDATE()"},
                    {"MUser","'" + clsSecurity.LoginUID + "'"},
                    {"MWhen","GETDATE()"},
                    {"StatusFlag","'" + (cbActive.Checked ? "A" : "D") + "'"}
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