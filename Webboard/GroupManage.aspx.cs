using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Webboard_GroupManage : System.Web.UI.Page
{
    #region Global Variable
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();
    clsDefault clsDefault = new clsDefault();

    public int photoWidth = 64;
    public int photoHeight = 64;
    public string pathUpload = "/Webboard/Upload/";

    public string tableDefault = "WebboardGroup";
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
            if (clsSecurity.LoginGroup != "Admin") ucColorBox1.Close();

            if (clsDefault.URLRouting("type") != "")
            {
                BindControl(clsDefault.URLRouting("type"));

                if (clsDefault.URLRouting("id") != "")
                {
                    if (clsDefault.URLRouting("command") == "Edit")
                    {
                        ucColorBox1.SizeChange();
                        vdPhoto.Enabled = false;
                        BindDetail(clsDefault.URLRouting("id"));
                    }
                    else if (clsDefault.URLRouting("command") == "Delete")
                    {
                        Delete(clsDefault.URLRouting("id"));
                    }
                    else
                    {
                        ucColorBox1.Redirect(webDefault, "ไม่พบหน้าเว็บที่คุณต้องการ");
                    }
                }
                else
                {
                    ucColorBox1.SizeChange();
                }
            }
            else
            {
                ucColorBox1.Close();
            }
        }
    }

    private void BindControl(string type)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region Data Builder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("Name ");
        strSQL.Append("FROM ");
        strSQL.Append("WebboardType ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort,Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlWebboardType.DataSource = dt;
            ddlWebboardType.DataTextField = "Name";
            ddlWebboardType.DataValueField = "UID";
            ddlWebboardType.DataBind();

            if (type != "")
            {
                ddlWebboardType.SelectedValue = type;
            }
        }
        #endregion
    }

    protected void BindDetail(string id)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append(tableDefault + ".WebboardTypeUID,");
        strSQL.Append(tableDefault + ".GlobalUID,");
        strSQL.Append(tableDefault + ".Icon,");
        strSQL.Append(tableDefault + ".Name,");
        strSQL.Append(tableDefault + ".Detail,");
        strSQL.Append(tableDefault + ".MetaKeywords,");
        strSQL.Append(tableDefault + ".MetaDescription,");
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
            lblPhoto.Text = "<img src='"+dt.Rows[0]["Icon"].ToString()+"'/><br/>";
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtMetaKeywords.Text = dt.Rows[0]["MetaKeywords"].ToString();
            txtMetaDescription.Text = dt.Rows[0]["MetaDescription"].ToString();
            txtGlobalUID.Text = dt.Rows[0]["GlobalUID"].ToString();
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
            ucColorBox1.Redirect("/", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
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
        photoName=clsSQL.Return(
            "SELECT Icon FROM "+tableDefault+" WHERE UID="+parameterChar+"UID",
            new string[,]{{parameterChar+"UID",id}},
            dbType,
            cs);
        clsIO.FileExist(photoName, true);
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
            ucColorBox1.Redirect(webDefault);
        }
        else
        {
            ucColorBox1.Redirect(webDefault, "เกิดข้อผิดพลาดขณะลบข้อมูล");
        }
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect(webDefault, "กรุณาล็อคอินด้วยสิทธิ์ Admin");
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
                    "Group" + id.ToString(),
                    out outErrorMessage,
                    out outFilename,
                    maxWidth: photoWidth,
                    maxHeight: photoHeight,
                    resizeMode: clsIO.ResizeMode.crop))
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
                    {"WebboardTypeUID",ddlWebboardType.SelectedItem.Value},
                    {"GlobalUID","'"+clsSQL.CodeFilter(txtGlobalUID.Text)+"'"},
                    {"Icon",(photoName!=""?"'"+pathUpload+photoName+"'":"Icon")},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"MetaKeywords","'"+clsSQL.CodeFilter(txtMetaKeywords.Text)+"'"},
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
                if (clsIO.UploadPhoto(
                    fuPhoto, pathUpload,
                    "Group" + id.ToString(),
                    out outErrorMessage,
                    out outFilename,
                    maxWidth: photoWidth,
                    maxHeight: photoHeight,
                    resizeMode: clsIO.ResizeMode.crop))
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
                    {"WebboardTypeUID",clsDefault.URLRouting("type")},
                    {"GlobalUID","'"+clsSQL.CodeFilter(txtGlobalUID.Text)+"'"},
                    {"Icon","'"+pathUpload+photoName+"'"},
                    {"Name","'"+clsSQL.CodeFilter(txtName.Text)+"'"},
                    {"Detail","'"+clsSQL.CodeFilter(txtDetail.Text)+"'"},
                    {"MetaKeywords","'"+clsSQL.CodeFilter(txtMetaKeywords.Text)+"'"},
                    {"MetaDescription","'"+clsSQL.CodeFilter(txtMetaDescription.Text)+"'"},
                    {"Views","0"},
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