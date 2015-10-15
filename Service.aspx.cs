using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Service : ucLanguage
{
    #region Global Variable
    clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    clsLanguage clsLanguage = new clsLanguage();
    public clsSecurity clsSecurity = new clsSecurity();
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
            if (clsDefault.URLRouting("id") != "")
            {
                BindDefault(clsDefault.URLRouting("id"));
            }
        }
    }

    private void BindDefault(string UID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("ServiceGroupUID,");
        strSQL.Append("LanguageUID,");
        strSQL.Append("UID,");
        strSQL.Append("Icon,");
        strSQL.Append("Name,");
        strSQL.Append("Detail,");
        strSQL.Append("Content,");
        strSQL.Append("Location,");
        strSQL.Append("OfficeHours,");
        strSQL.Append("Phone,");
        strSQL.Append("EMail,");
        strSQL.Append("Price,");
        strSQL.Append("MetaKeywords,");
        strSQL.Append("MetaDescription ");
        strSQL.Append("FROM ");
        strSQL.Append("Service ");
        strSQL.Append("WHERE ");
        //strSQL.Append("UID=" + parameterChar + "UID ");
        strSQL.Append("DepartmentUID=" + parameterChar + "UID ");
        strSQL.Append("AND LanguageUID="+clsLanguage.LanguageUIDCurrent.ToString()+" ");
        strSQL.Append("AND Active='1'");
        #endregion

        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), new string[,] { { parameterChar + "UID", UID } }, dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            #region DataBuilder
            Page.Title = dt.Rows[0]["Name"].ToString();
            if (dt.Rows[0]["MetaKeywords"] != DBNull.Value && dt.Rows[0]["MetaKeywords"].ToString() != "")
            {
                Page.MetaKeywords = dt.Rows[0]["MetaKeywords"].ToString();
            }
            if (dt.Rows[0]["MetaDescription"] != DBNull.Value && dt.Rows[0]["MetaDescription"].ToString() != "")
            {
                Page.MetaDescription = dt.Rows[0]["MetaDescription"].ToString();
            }

            lblIcon.Text = "<img src='" + dt.Rows[0]["Icon"].ToString() +
                "' title='" + dt.Rows[0]["Name"].ToString() +
                "' alt='" + dt.Rows[0]["Name"].ToString() + "' style='width:120px;'/>";
            lblName.Text = "<h1>" + dt.Rows[0]["Name"].ToString() + "</h1>";
            lblDetail.Text = dt.Rows[0]["Detail"].ToString();
            lblContent.Text = dt.Rows[0]["Content"].ToString();
            lblLocationValue.Text = dt.Rows[0]["Location"].ToString();
            lblOfficeHoursValue.Text = dt.Rows[0]["OfficeHours"].ToString();
            lblPhoneValue.Text = dt.Rows[0]["Phone"].ToString();

            if (dt.Rows[0]["EMail"] != DBNull.Value)
            {
                lblEMailValue.Text = "<a href='mailto:" + dt.Rows[0]["EMail"].ToString() + "'>" + dt.Rows[0]["EMail"].ToString() + "</a>";
            }

            if (dt.Rows[0]["Price"] != DBNull.Value && !string.IsNullOrEmpty(dt.Rows[0]["Price"].ToString()))
            {
                lblPrice.Text = dt.Rows[0]["Price"].ToString();
            }
            else
            {
                lblPrice.Text = "<div style='text-align:center;'>-</div>";
            }
            #endregion
            #region GalleryBuilder
            BindGallery(dt.Rows[0]["UID"].ToString());
            #endregion
            #region Admin Menu
            if (clsSecurity.LoginChecker("admin"))
            {
                lblAdminMenu.Text = "<div class='dvContentMenu'>" +
                    "<a href='/Management/ServiceManage.aspx?group=" + dt.Rows[0]["ServiceGroupUID"].ToString() +
                    "&id=" + dt.Rows[0]["UID"].ToString() +
                    "&command=edit" +
                    "&language=" + dt.Rows[0]["LanguageUID"].ToString() + "' title='แก้ไขข้อมูล' class='cbIFrame'>" +
                    "<span class='Icon16 Edit' />" +
                    "</a>" +
                    "</div>";
            }
            else
            {
                lblAdminMenu.Text = "";
            }
            #endregion
        }
        else
        {
            //ucColorBox1.Redirect("/", "ไม่พบหน้าที่คุณต้องการ");
            ucColorBox1.Alert("ไม่พบข้อมูล", "ไม่พบหน้าที่คุณต้องการ");
        }
        #endregion
    }

    private void BindGallery(string UID)
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        #endregion
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("UID,");
        strSQL.Append("PhotoPreview,");
        strSQL.Append("Photo,");
        strSQL.Append("Name,");
        strSQL.Append("Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("PhotoGallery ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("AND GlobalName='Service' ");
        strSQL.Append("AND GlobalUID=" + UID + " ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort");
        #endregion
        #region Data Builder
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            #region PhotoGallery
            //pnPhotoGallery.Visible = true;
            //lblGallery.Text = "";
            //dlDefault.DataSource = dt;
            //dlDefault.DataBind();
            ucGallery1.Source = dt;
            ucGallery1.PreviewHeight = "auto";
            #endregion
            #region JCarousel
            pnJCarousel.Visible = true;
            ucJCarousel1.Source = dt;
            ucJCarousel1.SourceFieldName = "Name";
            ucJCarousel1.SourceFieldPhoto = "Photo";
            ucJCarousel1.SourceFieldPhotoPreview = "PhotoPreview";
            ucJCarousel1.PathPhoto = "";
            ucJCarousel1.AutoPlay = true;
            ucJCarousel1.Width = 600;
            ucJCarousel1.Height = 400;
            ucJCarousel1.NavigatorWidth = 240;
            ucJCarousel1.NavigatorHeight = 45;
            ucJCarousel1.Credit = "<a href='http://www.bangkokchanthaburi.com'>www.bangkokchanthaburi.com</a>";
            ucJCarousel1.Enable = true;
            ucJCarousel1.Visible = true;
            #endregion
        }
        else
        {
            lblJCarouselAdminMenu.Text = "ไม่พบภาพสไลด์";
            //lblGallery.Text = "-";
        }
        #endregion
        #region Admin Menu
        if (clsSecurity.LoginChecker("admin"))
        {
            lblAdminGalleryMenu.Text = "<div class='dvContentMenu'>" +
                "<a href='/Management/PhotoGallery.aspx?globalid=" + UID + "&globalname=Service' title='แก้ไขข้อมูล' target='_Blank'>" +
                "<span class='Icon16 Edit' />" +
                "</a>" +
                "</div>";
            lblJCarouselAdminMenu.Text += "<div class='dvContentMenu'>" +
                "<a href='/Management/PhotoGallery.aspx?globalid=" + UID + "&globalname=Service' title='แก้ไขข้อมูล' target='_Blank'>" +
                "<span class='Icon16 Edit' />" +
                "</a>" +
                "</div>";
        }
        else
        {
            lblAdminGalleryMenu.Text = "";
            lblJCarouselAdminMenu.Text = "";
        }
        #endregion
    }
}