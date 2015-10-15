using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class UserRegister : ucLanguage
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
                //clsDefault.Redirect("/Profile", "คุณสมัครสมาชิกไว้แล้ว");
                ucColorBox1.Redirect("/Profile", "คุณสมัครสมาชิกไว้แล้ว");
            }
        }
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        string outSQL;
        string outError;
        string outPhotoName = "null";
        #endregion

        #region Check Data
        #region Find Username
        if (int.Parse(clsSQL.Return("SELECT COUNT(UID) FROM [User] WHERE Username='" + clsDefault.CodeFilter(txtUsername.Text) + "'", dbType, cs)) > 0)
        {
            //lblUsername.Text = clsDefault.AlertMessageColor("Username นี้มีผู้ใช้งานแล้ว", clsDefault.AlertType.Warn);
            //lblUsername.Focus();
            txtUsername.Focus();
            ucColorBox1.Alert("ข้อมูลไม่ถูกต้อง", "Username นี้มีผู้ใช้งานแล้ว", AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        else { lblUsername.Text = ""; }
        #endregion
        #region Find Email
        if (int.Parse(clsSQL.Return("SELECT COUNT(UID) FROM [User] WHERE Email='" + clsDefault.CodeFilter(txtEMail.Text) + "'", dbType, cs)) > 0)
        {
            //lblEmail.Text = clsDefault.AlertMessageColor("Email นี้มีผู้ใช้งานแล้ว", clsDefault.AlertType.Warn);
            //lblEmail.Focus();
            txtEMail.Focus();
            ucColorBox1.Alert("ข้อมูลไม่ถูกต้อง", "E-Mail นี้มีผู้ใช้งานแล้ว", AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        else { lblEmail.Text = ""; }
        #endregion
        #region Find UID
        int UID = clsSQL.GetNewID("UID", "[User]", "", dbType, cs);
        if (UID == 0)
        {
            //lblSQL.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะหา UID", clsDefault.AlertType.Fail);
            //lblSQL.Focus();
            ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "ไม่สามารถหา UID ได้", AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        else{lblSQL.Text="";}
        #endregion
        #endregion

        #region Insert
        #region Photo Upload
        if (fuPhoto.HasFile)
        {
            if (!clsIO.UploadPhoto(fuPhoto, pathPhoto, clsSecurity.LoginUID, 500, photoWidth, photoHeight, "", 0, out outError, out outPhotoName))
            {
                //lblSQL.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะอัพโหลดภาพ : " + outError, clsDefault.AlertType.Fail);
                //lblSQL.Focus();
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "เกิดข้อผิดพลาดขณะอัพโหลดภาพ", AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            else
            {
                outPhotoName = "'" + pathPhoto+outPhotoName + "'";
            }
        }
        #endregion
        #region SQL Insert
        if (clsSQL.Insert(
            "[USER]",
            new string[,]{
                {"UID",UID.ToString()},
				{"UserGroupUID","2"},
                {"Username","'"+clsDefault.CodeFilter(txtUsername.Text)+"'"},
                {"Password","'"+clsSecurity.Encrypt(clsDefault.CodeFilter(txtPassword.Text))+"'"},
                {"Photo",outPhotoName},
                {"PName",ddlPName.SelectedItem.Value!="null"?"'"+ddlPName.SelectedItem.Value+"'":"null"},
                {"FName","'"+clsDefault.CodeFilter(txtFName.Text)+"'"},
                {"LName","'"+clsDefault.CodeFilter(txtLName.Text)+"'"},
                {"HN","'"+clsDefault.CodeFilter(HNConvert(txtHN.Text))+"'"},
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
                {"CUser",UID.ToString()},
                {"CWhen","GETDATE()"},
                {"MUser",UID.ToString()},
                {"MWhen","GETDATE()"},
                {"Sort",clsDefault.CodeFilter(txtSort.Text)},
                {"Active","'0'"/*cbActive.Checked?"'1'":"'0'"+"'"*/}
            },
            new string[,] { { } },
            dbType,
            cs,
            out outSQL
        ))
        {
            clsMail clsMail = new clsMail();
            string outMessage;
            string idEncode = Server.UrlEncode(clsSecurity.Encrypt(UID.ToString()));

            #region Mail to User
            if (!clsMail.SendTemplate(
                "UserRegisterConfirm",
                clsMail.GetEmailList("GlobalFrom"),
                txtEMail.Text,
                new string[,]{
                    {"[Username]",txtUsername.Text},
                    {"[UIDEncrypt]",idEncode}
                },
                out outMessage))
            {
                //lblSQL.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะส่งเมล์ยืนยัน<br/>"+outMessage, clsDefault.AlertType.Fail);
                //lblSQL.Focus();
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "เกิดข้อผิดพลาดขณะส่งเมล์ยืนยัน<br/>" + outMessage, AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            #endregion
            #region Mail to Admin
            if (!clsMail.SendTemplate(
                "UserRegisterAdmin",
                clsMail.GetEmailList("AutoSystemFrom"),
                clsMail.GetEmailList("AdminTo"),
                new string[,]{
                    {"[Username]",txtUsername.Text}
                },
                out outMessage))
            {
                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "เกิดข้อผิดพลาดขณะส่งเมล์ยืนยัน<br/>" + outMessage, AlertImage: ucColorBox.Alerts.Fail);
                return;
            }
            #endregion
        }
        else
        {
            //lblSQL.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะบันทึกลงฐานข้อมูล : " + outSQL, clsDefault.AlertType.Fail);
            //lblSQL.Focus();
            ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "เกิดข้อผิดพลาดขณะบันทึกข้อมูลลงฐานข้อมูล : "+outSQL, AlertImage: ucColorBox.Alerts.Fail);
            return;
        }
        #endregion
        //clsDefault.Redirect("/", "บันทึกข้อมูลเรียบร้อยแล้ว");
        ucColorBox1.Redirect("/");
        #endregion
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/");
    }

    private string HNConvert(string hn)
    {
        string rtnValue="";

        if (!string.IsNullOrEmpty(hn))
        {
            rtnValue = hn.Replace("-", "");
            rtnValue = rtnValue.Insert(2, "-");
            rtnValue = rtnValue.Insert(5, "-");
        }

        return rtnValue;
    }
}