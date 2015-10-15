using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class UserProfile : ucLanguage
{
    public int photoWidth = 150;
    public int photoHeight = 150;
    public string pathPhoto = "/Upload/User/";

    clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsSecurity clsSecurity = new clsSecurity();
    clsIO clsIO = new clsIO();

    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (clsSecurity.LoginChecker())
            {
                txtUsername.Enabled = false;
                #region Admin Checker
                if (clsSecurity.LoginChecker("admin"))
                {
                    pnAdmin.Visible = true;
                }
                else
                {
                    pnAdmin.Visible = false;
                }
                #endregion
                BindDefault();
            }
            else
            {
                //clsDefault.Redirect("/Register", "กรุณาสมัครสมาชิก หรือ ล็อคอิน ก่อนเข้าใช้งาน");
                ucColorBox1.Redirect("/Register", "กรุณาสมัครสมาชิก หรือ ล็อคอินก่อนเข้าใช้งาน");
            }
        }
    }

    protected void BindDefault()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion

        #region Bind UserGroup
        ddlUserGroup.DataSource = clsSQL.Bind("SELECT UID,Name FROM UserGroup WHERE Active='1' ORDER BY Sort", dbType, cs);
        ddlUserGroup.DataTextField = "Name";
        ddlUserGroup.DataValueField = "UID";
        ddlUserGroup.DataBind();
        #endregion

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("[User].UserGroupUID,");
        strSQL.Append("UserGroup.Name AS UserGroupName,");
        strSQL.Append("[User].UID,");
        strSQL.Append("[User].Username,");
        strSQL.Append("[User].Password,");
        strSQL.Append("[User].HN,");
        strSQL.Append("[User].Photo,");
        strSQL.Append("[User].PName,");
        strSQL.Append("[User].FName,");
        strSQL.Append("[User].LName,");
        strSQL.Append("[User].BirthDate,");
        strSQL.Append("[User].Gender,");
        strSQL.Append("[User].Phone,");
        strSQL.Append("[User].Mobile,");
        strSQL.Append("[User].Email,");
        strSQL.Append("[User].Address,");
        strSQL.Append("[User].AddressDistrict,");
        strSQL.Append("[User].AddressPrefecture,");
        strSQL.Append("[User].AddressProvince,");
        strSQL.Append("[User].AddressPostal,");
        strSQL.Append("[User].Profile,");
        strSQL.Append("[User].Signature,");
        strSQL.Append("[User].Sort,");
        strSQL.Append("[User].Active ");
        strSQL.Append("FROM ");
        strSQL.Append("[User] ");
        strSQL.Append("INNER JOIN UserGroup ");
        strSQL.Append("ON [User].UserGroupUID=UserGroup.UID AND UserGroup.Active='1' ");
        strSQL.Append("WHERE ");
        strSQL.Append("[User].UID=" + parameterChar + "UID ");
        strSQL.Append("AND (");
        strSQL.Append("UserGroup.Name='admin' OR (UserGroup.Name <> 'admin' AND [User].Active='1')");
        strSQL.Append(");");
        #endregion

        #region Bind Data
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { "" + parameterChar + "UID", clsSecurity.LoginUID } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtUsername.Text = dt.Rows[0]["Username"].ToString();
            ddlUserGroup.SelectedValue = dt.Rows[0]["UserGroupUID"].ToString();
            txtEMail.Text = dt.Rows[0]["Email"].ToString();
            #region Photo
            if (dt.Rows[0]["Photo"] != DBNull.Value)
            {
                lblPhoto.Text = "<div><img src='" + dt.Rows[0]["Photo"].ToString() + "'/></div>";
            }
            #endregion
            #region HN
            if (dt.Rows[0]["HN"] != DBNull.Value)
            {
                txtHN.Text = dt.Rows[0]["HN"].ToString();
            }
            #endregion
            #region Name
            if (dt.Rows[0]["PName"] != DBNull.Value)
            {
                ddlPName.SelectedValue = dt.Rows[0]["PName"].ToString();
            }
            txtFName.Text = dt.Rows[0]["FName"].ToString();
            txtLName.Text = dt.Rows[0]["LName"].ToString();
            #endregion
            #region BirthDate
            DateTime dttm = (dt.Rows[0]["BirthDate"] != DBNull.Value ? DateTime.Parse(dt.Rows[0]["BirthDate"].ToString()) : DateTime.MinValue);
            if (dttm != DateTime.MinValue)
            {
                ucDateTimeFlat1.DateTime = dttm;
            }
            #endregion
            #region Gender
            if (dt.Rows[0]["Gender"] != DBNull.Value)
            {
                rbGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            }
            #endregion
            #region Phone Mobile
            if (dt.Rows[0]["Phone"] != DBNull.Value)
            {
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            }
            if (dt.Rows[0]["Mobile"] != DBNull.Value)
            {
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
            }
            #endregion
            #region Address
            if (dt.Rows[0]["Address"] != DBNull.Value)
            {
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
            }
            if (dt.Rows[0]["AddressDistrict"] != DBNull.Value)
            {
                txtAddressDistrict.Text = dt.Rows[0]["AddressDistrict"].ToString();
            }
            if (dt.Rows[0]["AddressPrefecture"] != DBNull.Value)
            {
                txtAddressPrefecture.Text = dt.Rows[0]["AddressPrefecture"].ToString();
            }
            if (dt.Rows[0]["AddressProvince"] != DBNull.Value)
            {
                txtAddressProvince.Text = dt.Rows[0]["AddressProvince"].ToString();
            }
            if (dt.Rows[0]["AddressPostal"] != DBNull.Value)
            {
                txtAddressPostal.Text = dt.Rows[0]["AddressPostal"].ToString();
            }
            #endregion
            #region Profile & Signature
            if (dt.Rows[0]["Profile"] != DBNull.Value)
            {
                ucProfile.Text = dt.Rows[0]["Profile"].ToString();
            }
            if (dt.Rows[0]["Signature"] != DBNull.Value)
            {
                ucSignature.Text = dt.Rows[0]["Signature"].ToString();
            }
            #endregion
        }
        else
        {
            clsSecurity.LoginDelete();
            //clsDefault.Redirect("/", "ไม่พบข้อมูลของคุณ");
            ucColorBox1.Redirect("/", "ไม่พบข้อมูลของคุณ");
        }
        #endregion
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Security
        if (!clsSecurity.LoginChecker())
        {
            //clsDefault.Redirect("/Register", "กรุณาสมัครสมาชิก หรือ ล็อคอิน ก่อนเข้าใช้งาน");
            ucColorBox1.Redirect("/Register", "กรุณาสมัครสมาชิก หรือ ล็อคอินก่อนเข้าใช้งาน");
        }
        #endregion

        #region Variable
        StringBuilder strSQL = new StringBuilder();
        string outSQL;
        string outError;
        string outPhotoName = "Photo";
        #endregion

        #region Update
        #region Photo Upload
        if (fuPhoto.HasFile)
        {
            if (!clsIO.UploadPhoto(fuPhoto, pathPhoto, clsSecurity.LoginUID, 500, photoWidth,photoHeight, "", 0, out outError, out outPhotoName))
            {
                //lblSQL.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะอัพโหลดภาพ : " + outError, clsDefault.AlertType.Fail);
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "เกิดข้อผิดพลาดขณะอัพโหลดภาพ : "+outError, AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            else
            {
                outPhotoName = "'" + pathPhoto+outPhotoName + "'";
            }
        }
        #endregion
        #region Check Data
        if (int.Parse(clsSQL.Return("SELECT COUNT(UID) FROM [USER] WHERE UID='" + clsSecurity.LoginUID + "' AND Password='" + clsSecurity.Encrypt(clsDefault.CodeFilter(txtPassword.Text)) + "'", dbType, cs)) == 0)
        {
            //lblPassword.Text = clsDefault.AlertMessageColor("Password ที่คุณกรอกไม่ถูกต้อง", clsDefault.AlertType.Warn);
            ucColorBox1.Alert("ข้อมูลไม่ถูกต้อง", "Password ที่คุณกรอกไม่ถูกต้อง", AlertImage: ucColorBox.Alerts.Fail);
            lblPassword.Focus();
            return;
        }
        #endregion
        #region SQL Update
        if (!clsSQL.Update(
            "[User]",
            new string[,]{
                {"Password",txtPasswordChange.Text.Trim()!=""?"'"+clsSecurity.Encrypt(clsDefault.CodeFilter(txtPasswordChange.Text))+"'":"Password"},
                {"Photo",outPhotoName},
                {"UserGroupUID","'"+ddlUserGroup.SelectedItem.Value+"'"},
                {"HN","'"+clsDefault.CodeFilter(txtHN.Text)+"'"},
                {"PName",ddlPName.SelectedItem.Value!="null"?"'"+ddlPName.SelectedItem.Value+"'":"null"},
                {"FName","'"+clsDefault.CodeFilter(txtFName.Text)+"'"},
                {"LName","'"+clsDefault.CodeFilter(txtLName.Text)+"'"},
                {"BirthDate",ucDateTimeFlat1.DateTime!=DateTime.MinValue?"'"+ucDateTimeFlat1.DateTime.ToString("yyyy-MM-dd HH:mm:ss")+"'":"null"},
                {"Gender",rbGender.SelectedItem.Value!="null"?"'"+rbGender.SelectedItem.Value+"'":"null"},
                {"Phone","'"+clsDefault.CodeFilter(txtPhone.Text)+"'"},
                {"Mobile","'"+clsDefault.CodeFilter(txtMobile.Text)+"'"},
                {"Email","'"+clsDefault.CodeFilter(txtEMail.Text)+"'"},
                {"Address","'"+clsDefault.CodeFilter(txtAddress.Text)+"'"},
                {"AddressDistrict","'"+clsDefault.CodeFilter(txtAddressDistrict.Text)+"'"},
                {"AddressPrefecture","'"+clsDefault.CodeFilter(txtAddressPrefecture.Text)+"'"},
                {"AddressProvince","'"+clsDefault.CodeFilter(txtAddressProvince.Text)+"'"},
                {"AddressPostal","'"+clsDefault.CodeFilter(txtAddressPostal.Text)+"'"},
                {"Profile","'"+ucProfile.Text+"'"},
                {"Signature","'"+ucSignature.Text+"'"},
                {"MUser",clsSecurity.LoginUID},
                {"MWhen","GETDATE()"},
                {"Sort",clsDefault.CodeFilter(txtSort.Text)},
                {"Active",cbActive.Checked?"'1'":"'0'"+"'"}
            },
            new string[,] { 
                { "" + parameterChar + "UID", clsSecurity.LoginUID } 
            },
            "UID=" + parameterChar + "UID",
            dbType,
            cs,
            out outSQL
        ))
        {
            //lblSQL.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะบันทึกลงฐานข้อมูล : " + outSQL, clsDefault.AlertType.Fail);
            ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "เกิดข้อผิดพลาดขณะบันทึกลงฐานข้อมูล : " + outSQL, AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        #endregion
        //clsDefault.Redirect("/", "บันทึกข้อมูลเรียบร้อยแล้ว");
        ucColorBox1.Redirect("/", "บันทึกข้อมูลเรียบร้อยแล้ว");
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/");
    }
}