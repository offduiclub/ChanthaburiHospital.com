using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserRegisterConfirm : System.Web.UI.Page
{
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    clsDefault clsDefault = new clsDefault();
    clsSecurity clsSecurity = new clsSecurity();
    clsSQL clsSQL = new clsSQL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = clsDefault.URLRouting("id");

            if (!string.IsNullOrEmpty(id))
            {
                string idDecrypt = clsSecurity.Decrypt(id);
                string active = clsSQL.Return(
                    "SELECT Active FROM [User] WHERE UID=" + parameterChar + "UID",
                    new string[,] { { parameterChar + "UID", idDecrypt } }, 
                    dbType, 
                    cs);

                if (!string.IsNullOrEmpty(active))
                {
                    if (active == "0")
                    {
                        string outSQL;
                        if (clsSQL.Update(
                            "[User]",
                            new string[,] { { "Active", "'1'" }, { "MWhen", "GETDATE()" } },
                            new string[,] { { "@UID", idDecrypt } },
                            "UID=@UID",
                            dbType,
                            cs,
                            out outSQL))
                        {
                            #region Mail to Admin
                            string outMessage;
                            string Name = clsSQL.Return(
                                "SELECT Username FROM [User] WHERE UID=" + parameterChar + "UID",
                                new string[,] { { parameterChar + "UID", idDecrypt } },
                                dbType,
                                cs);
                            clsMail clsMail = new clsMail();

                            if (!clsMail.SendTemplate(
                                "UserRegisterConfirmAdmin",
                                clsMail.GetEmailList("AutoSystemFrom"),
                                clsMail.GetEmailList("AdminTo"),
                                new string[,]{
                                    {"[Username]",Name}
                                },
                                out outMessage))
                            {
                                ucColorBox1.Alert("เกิดข้อผิดพลาดขณะบันทึกข้อมูล", "เกิดข้อผิดพลาดขณะส่งเมล์ยืนยัน<br/>" + outMessage, AlertImage: ucColorBox.Alerts.Fail);
                                return;
                            }
                            #endregion
                            ucColorBox1.Redirect(
                                "/",
                                "ดำเนินการเสร็จสิ้น",
                                "ระบบยืนยันสถานะสมาชิกของคุณเรียบร้อยแล้ว");
                        }
                        else
                        {
                            ucColorBox1.Redirect(
                                "/",
                                "เกิดข้อผิดพลาด",
                                "ไม่พบรหัสยืนยันของคุณ");
                        }
                    }
                    else
                    {
                        ucColorBox1.Redirect(
                            "/",
                            "ดำเนินการเสร็จสิ้น",
                            "คุณเคยทำการยืนยันอีเมล์ไว้แล้ว");
                    }
                }
                else
                {
                    ucColorBox1.Redirect(
                        "/",
                        "เกิดข้อผิดพลาด",
                        "ไม่พบรหัสยืนยันของคุณ");
                }
            }
            else
            {
                ucColorBox1.Redirect(
                    "/",
                    "เกิดข้อผิดพลาด",
                    "ไม่พบรหัสยืนยันของคุณ");
            }
        }
    }
}