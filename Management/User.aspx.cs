using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_User : System.Web.UI.Page
{
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsSecurity clsSecurity = new clsSecurity();
    public string pathUpload = "/Upload/User/";

    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["command"] != null)
            {
                if (Request.QueryString["command"].ToString() == "delete")
                {
                    if (clsSQL.Execute(
                        "UPDATE [User] SET Active='D' WHERE UID=@UID;",
                        new string[,] { { "@UID", Request.QueryString["id"].ToString() } },
                        dbType,
                        cs))
                    {
                        //ucColorBox1.Redirect("/Management/DoctorAppointment.aspx");
                        Response.Redirect("User.aspx");
                    }
                    else
                    {
                        ucColorBox1.Alert("เกิดข้อผิดพลาดขณะลบข้อมูล");
                    }
                }
            }
            else
            {
                BindDefault();
            }
        }
    }

    protected void BindDefault()
    {
        gvDefault.Visible = true; pnDGHeader.Visible = true;
        StringBuilder strSQL = new StringBuilder();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("[User].UserGroupUID,");
        strSQL.Append("UserGroup.Name AS UserGroupName,");
        strSQL.Append("[User].UID,");
        strSQL.Append("[User].Photo,");
        strSQL.Append("[User].Username,");
        strSQL.Append("[User].PName,");
        strSQL.Append("[User].FName,");
        strSQL.Append("[User].LName,");
        strSQL.Append("[User].Phone,");
        strSQL.Append("[User].Mobile,");
        strSQL.Append("[User].Email,");
        strSQL.Append("[User].Sort,");
        strSQL.Append("[User].Active ");
        strSQL.Append("FROM ");
        strSQL.Append("[User] ");
        strSQL.Append("INNER JOIN UserGroup ");
        strSQL.Append("ON [User].UserGroupUID=UserGroup.UID ");
        strSQL.Append("WHERE [User].Active<>'D' ");
        strSQL.Append("ORDER BY UserGroup.Sort,[User].Sort");
        #endregion

        DataTable dt = new DataTable();
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            lblDG.Text = "";
            gvDefault.DataSource = dt;
            if (Request.QueryString["page"] != null)
            {
                try
                {
                    gvDefault.PageIndex = int.Parse(Request.QueryString["page"].ToString());
                }
                catch (Exception ex)
                {
                    gvDefault.PageIndex = int.Parse(Request.QueryString["page"].ToString()) - 1;
                }
            }
            gvDefault.DataBind();
            dt = null;

            #region Bind UserGroup
            #region SQL Query
            strSQL.Append("SELECT ");
            strSQL.Append("UID,");
            strSQL.Append("Name ");
            strSQL.Append("FROM ");
            strSQL.Append("UserGroup ");
            strSQL.Append("WHERE ");
            strSQL.Append("Active='1' ");
            strSQL.Append("ORDER BY ");
            strSQL.Append("Sort");
            #endregion

            dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { } }, dbType, cs);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < gvDefault.Rows.Count; i++)
                {
                    Label lblDGUserGroupUID = (Label)gvDefault.Rows[i].FindControl("lblDGUserGroupUID");
                    DropDownList ddlDGUserGroup = (DropDownList)gvDefault.Rows[i].FindControl("ddlDGUserGroup");

                    if (lblDGUserGroupUID != null && ddlDGUserGroup != null)
                    {
                        ddlDGUserGroup.DataSource = dt;
                        ddlDGUserGroup.DataTextField = "Name";
                        ddlDGUserGroup.DataValueField = "UID";
                        ddlDGUserGroup.DataBind();

                        ddlDGUserGroup.SelectedValue = lblDGUserGroupUID.Text;
                    }
                }
            }
            #endregion
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
        }

        strSQL.Length = 0; strSQL.Capacity = 0;
    }

    protected void btDGSubmit_Click(object sender, EventArgs e)
    {
        if (!clsSecurity.LoginChecker("admin"))
        {
            //clsDefault.Redirect("/Management/Login.aspx", "กรุณาล็อคอินด้วยสิทธิ์ Admin");
            ucColorBox1.Redirect("/", "เกิดข้อผิดพลาด", "คุณไม่ได้รับสิทธิ์ในการบันทึกข้อมูล กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }

        StringBuilder strSQL = new StringBuilder();

        for (int i = 0; i < gvDefault.Rows.Count; i++)
        {
            #region CurrentPageChecker
            var cbDGCurrentPage = (CheckBox)gvDefault.Rows[i].FindControl("cbDGCurrentPage");
            if (!cbDGCurrentPage.Checked) continue;
            #endregion
            Label lblDGID = (Label)gvDefault.Rows[i].FindControl("lblDGID");
            DropDownList ddlDGUserGroup = (DropDownList)gvDefault.Rows[i].FindControl("ddlDGUserGroup");
            CheckBox cbDGActive = (CheckBox)gvDefault.Rows[i].FindControl("cbDGActive");

            if (lblDGID != null && ddlDGUserGroup != null && cbDGActive != null)
            {
                #region SQL Query
                strSQL.Append("UPDATE ");
                strSQL.Append("[User] ");
                strSQL.Append("SET ");
                strSQL.Append("UserGroupUID=" + ddlDGUserGroup.SelectedItem.Value + ",");
                strSQL.Append("Active='" + (cbDGActive.Checked ? "1" : "0") + "' ");
                strSQL.Append("WHERE ");
                strSQL.Append("UID=" + lblDGID.Text);
                strSQL.Append(";");
                #endregion
            }
        }

        if (clsSQL.Execute(strSQL.ToString(), dbType, cs))
        {
            //clsDefault.Redirect("/Management/User.aspx", "แก้ไขข้อมูลเสร็จสิ้น");
            ucColorBox1.Redirect("/Management/User.aspx", "ดำเนินการเสร็จสิ้น", "แก้ไขข้อมูลเสร็จเรียบร้อย");
        }
        else
        {
            //lblDG.Text = clsDefault.AlertMessageColor("เกิดข้อผิดพลาดขณะบันทึกข้อมูลในฐานข้อมูล กรุณาลองใหม่" + "<br/>" + strSQL.ToString(), "fail");
            ucColorBox1.Alert("เกิดข้อผิดพลาด", "เกิดข้อผิดพลาดขณะบันทึกข้อมูลลงฐานข้อมูล<br/>" + strSQL.ToString(), AlertImage: ucColorBox.Alerts.Fail);
        }
    }
}