using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Management_Highlight : System.Web.UI.Page
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsSecurity clsSecurity = new clsSecurity();
    DataTable dtHighlight = new DataTable();
    bool highlightSkip = false;
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
            BindDefault();
        }
    }

    protected void BindDefault()
    {
        dlDefault.Visible = true; pnDGHeader.Visible = true;
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region SQL Query
        #region HealthPackage
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Name,PicThumbnail Photo,'HealthPackage' GlobalName ");
        strSQL.Append("FROM ");
        strSQL.Append("HealthPackage ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= CONVERT(DATE,GETDATE())) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= CONVERT(DATE,GETDATE())) ");
        #endregion
        strSQL.Append("UNION ALL ");
        #region Package
        strSQL.Append("SELECT ");
        strSQL.Append("UID,PackageName Name,PicThumbnail Photo,'Package' GlobalName ");
        strSQL.Append("FROM ");
        strSQL.Append("Package ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= CONVERT(DATE,GETDATE())) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= CONVERT(DATE,GETDATE())) ");
        #endregion
        strSQL.Append("UNION ALL ");
        #region Event
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Subject Name,PicThumbnail Photo,'Event' GlobalName ");
        strSQL.Append("FROM ");
        strSQL.Append("Event ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= CONVERT(DATE,GETDATE())) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= CONVERT(DATE,GETDATE())) ");
        #endregion
        strSQL.Append("UNION ALL ");
        #region Promotion
        strSQL.Append("SELECT ");
        strSQL.Append("UID,PromotionName Name,PicThumbnail Photo,'Promotion' GlobalName ");
        strSQL.Append("FROM ");
        strSQL.Append("Promotion ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= CONVERT(DATE,GETDATE())) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= CONVERT(DATE,GETDATE())) ");
        #endregion
        strSQL.Append("UNION ALL ");
        #region News
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Subject Name,PicThumbnail Photo,'News' GlobalName ");
        strSQL.Append("FROM ");
        strSQL.Append("News ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND (ActiveDateFrom IS NULL OR ActiveDateFrom <= CONVERT(DATE,GETDATE())) ");
        strSQL.Append("AND (ActiveDateTo IS NULL OR ActiveDateTo >= CONVERT(DATE,GETDATE())) ");
        #endregion
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            DataColumn dc = dt.Columns.Add("Choose", typeof(string));
            #region HighlightBuilder
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Choose"] = HighlightFinder(
                                        dt.Rows[i]["UID"].ToString(), 
                                        dt.Rows[i]["GlobalName"].ToString());
            }
            dt.AcceptChanges();
            #endregion
            DataView dv = new DataView(dt);
            dv.Sort = "Choose DESC";
            dt = dv.ToTable();

            lblDG.Text = "";
            dlDefault.DataSource = dt;
            dlDefault.DataBind();
            dt = null;
        }
        else
        {
            pnDGHeader.Visible = false;
            lblDG.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลที่ต้องการ", clsDefault.AlertType.Info);
        }
        #endregion
    }

    private bool HighlightFinder(string GlobalUID, string GlobalName)
    {
        if (highlightSkip) return false;

        if (dtHighlight == null || dtHighlight.Rows.Count == 0)
        {
            dtHighlight = clsSQL.Bind(
                "SELECT GlobalUID,GlobalName FROM Highlight WHERE StatusFlag='A' ORDER BY GlobalName;",
                dbType,
                cs);
        }

        if (dtHighlight != null && dtHighlight.Rows.Count > 0)
        {
            for (int i = 0; i < dtHighlight.Rows.Count; i++)
            {
                if (dtHighlight.Rows[i]["GlobalName"].ToString() == GlobalName &&
                    dtHighlight.Rows[i]["GlobalUID"].ToString() == GlobalUID)
                {
                    return true;
                }
            }
        }
        else
        {
            highlightSkip = true;
        }

        return false;
    }

    protected void btDGSubmit_Click(object sender, EventArgs e)
    {
        #region Authorize
        if (!clsSecurity.LoginChecker("admin"))
        {
            ucColorBox1.Redirect("/", "เกิดข้อผิดพลาด", "คุณไม่ได้รับสิทธิ์ในการบันทึกข้อมูล กรุณาล็อคอินด้วยสิทธิ์ Admin");
            return;
        }
        #endregion
        #region Variable
        var strSQL = new StringBuilder();
        var outError="";
        var statusFlag = "";
        #endregion
        #region SQL Builder
        for (int i = 0; i < dlDefault.Items.Count; i++)
        {
            Label lblDGID = (Label)dlDefault.Items[i].FindControl("lblDGID");
            Label lblDGName = (Label)dlDefault.Items[i].FindControl("lblDGName");
            CheckBox cbDGActive = (CheckBox)dlDefault.Items[i].FindControl("cbDGActive");

            if (lblDGID != null && lblDGName!=null && cbDGActive != null)
            {
                statusFlag = clsSQL.Return("SELECT StatusFlag FROM Highlight WHERE GlobalUID=" + lblDGID.Text + " AND GlobalName='" + lblDGName.Text + "';", dbType, cs);
                if (statusFlag!="" && statusFlag!=(cbDGActive.Checked?"A":"D"))
                {
                    #region UPDATE SQL Query
                    strSQL.Append("UPDATE ");
                    strSQL.Append("Highlight ");
                    strSQL.Append("SET ");
                    strSQL.Append("MWhen=GETDATE(),");
                    strSQL.Append("MUser=" + clsSecurity.LoginUID + ",");
                    strSQL.Append("StatusFlag='" + (cbDGActive.Checked?"A":"D") + "' ");
                    strSQL.Append("WHERE ");
                    strSQL.Append("GlobalUID=" + lblDGID.Text+" ");
                    strSQL.Append("AND GlobalName='"+lblDGName.Text+"'");
                    strSQL.Append(";");
                    #endregion
                }
                else if(statusFlag=="")
                {
                    if (cbDGActive.Checked)
                    {
                        #region INSERT SQL Query
                        strSQL.Append("INSERT INTO ");
                        strSQL.Append("Highlight ");
                        strSQL.Append("(GlobalUID,GlobalName,CWhen,CUser,MWhen,MUser,Sort,StatusFlag)");
                        strSQL.Append("VALUES(");
                        strSQL.Append(lblDGID.Text + ",");
                        strSQL.Append("'" + lblDGName.Text + "',");
                        strSQL.Append("GETDATE(),");
                        strSQL.Append(clsSecurity.LoginUID + ",");
                        strSQL.Append("GETDATE(),");
                        strSQL.Append(clsSecurity.LoginUID + ",");
                        strSQL.Append("0,");
                        strSQL.Append("'A'");
                        strSQL.Append(")");
                        strSQL.Append(";");
                        #endregion
                    }
                }
                if (strSQL.Length > 0)
                {
                    if (!clsSQL.Execute(strSQL.ToString(), dbType, cs, out outError))
                    {
                        ucColorBox1.Alert("SQL Error", outError, AlertImage: ucColorBox.Alerts.Fail);
                        return;
                    }
                    strSQL.Length = 0; strSQL.Capacity = 0;
                }
            }
        }
        #endregion

        ucColorBox1.Redirect("/Management/Highlight.aspx" + clsDefault.QueryStringMerge(), "ดำเนินการเสร็จสิ้น", "แก้ไขข้อมูลเสร็จเรียบร้อย");
    }
}