using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsForm : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Request.QueryString["UID"]))
            {
                lblTitle.Text = "เพิ่มข่าวประชาสัมพันธ์";
            }
            else
            {
                lblTitle.Text = "แก้ไขข่าวประชาสัมพันธ์";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        if (!Page.IsPostBack)
        {
            BindLanguage();
            BindNews();
        }
    }

    private void BindNews()
    {
        var tbNews = from n in db.News
                      where n.UID == Convert.ToInt32(Request.QueryString["UID"])
                      select n;
        try
        {
            foreach (New n in tbNews)
            {
                lblUID.Text = n.UID.ToString();
                txtSubject.Text = n.Subject;
                txtDetail.Text = n.Detail;
                lblImagesFull.Text = n.PicFull;
                lblImagesThumb.Text = n.PicThumbnail;
                txtDateFrom.Text = n.ActiveDateFrom.ToString();
                txtDateTo.Text = n.ActiveDateTo.ToString();
                txtRemark.Text = n.Remark;
                lblLangUID.Text = n.LanguageUID.ToString();
                txtMetaKeywords.Text = n.MetaKeywords;
                txtMetaDescription.Text = n.MetaDescription;
                if (!string.IsNullOrEmpty(lblLangUID.Text))
                {
                    BindLanguageByUID(Convert.ToInt32(lblLangUID.Text));
                }
                if (n.StatusFlag == "A")
                {
                    rdbActive.Checked = true;
                }
                else
                {
                    rdbInactive.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
    }
    private void BindLanguage()
    {
        var tbLang = from l in db.Languages
                     where l.Active == '1'
                     orderby l.Name ascending
                     select l;
        ddlLanguage.DataSource = tbLang;
        ddlLanguage.DataValueField = "UID";
        ddlLanguage.DataTextField = "Detail";
        ddlLanguage.DataBind();
    }
    private void BindLanguageByUID(int LangUID)
    {
        string Lang = string.Empty;
        var tbLang = from l in db.Languages
                     where l.Active == '1' && l.UID == LangUID
                     select l;
        try
        {
            foreach (Language l in tbLang)
            {
                Lang = l.Detail;
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        ddlLanguage.Items.Insert(0, Lang);
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        clsSecurity security = new clsSecurity();
        clsIO IO = new clsIO();

        string strDetailSub = string.Empty;
        string strDetail = txtDetail.Text.Replace("<p>", "");
        strDetail = strDetail.Replace("</p>", "");
        string PicFull = "null";
        string PicThumb = "null";
        string pathPhoto = "/Images/News/";
        string pathImages = @"\Images\News\";
        string outError;


        //upload Photo to server
        if (txtImgFull.HasFile)
        {
            //Full Images
            if (IO.UploadPhoto(txtImgFull, pathPhoto, "f_pic" + DateTime.Now.ToString("yyyyMMddHHmmss"), 5120, 0, 0, "", 0, out outError, out PicFull) == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + outError.ToString() + "')", true);
                return;
            }
            else
            {
                PicFull = pathImages + "" + PicFull;
            }
        }
        else
        {
            PicFull = lblImagesFull.Text.Trim();
        }
        if (txtImgThum.HasFile)
        {
            //Thumb Images
            if (IO.UploadPhoto(txtImgThum, pathPhoto, "t_pic" + DateTime.Now.ToString("yyyyMMddHHmmss"), 2048, 0, 0, "", 0, out outError, out PicThumb) == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + outError.ToString() + "')", true);
                return;
            }
            else
            {
                PicThumb = pathImages + "" + PicThumb;
            }
        }
        else
        {
            PicThumb = lblImagesThumb.Text.Trim();
        }
        //insert data to database
        try
        {
            if (txtDetail.Text.Length >= 200)
            {
                strDetailSub = "<p>" + strDetail.Substring(0, 200) + ".....</p>";
            }
            else
            {
                strDetailSub = "<p>" + strDetail + ".....</p>";
            }
            //Insert news
            if (string.IsNullOrEmpty(lblUID.Text) == true)
            {
                New tbnews = new New();

                //Assign values for insert into database
                tbnews.Subject = txtSubject.Text.Trim();
                tbnews.Detail = txtDetail.Text;
                tbnews.PicFull = PicFull;
                tbnews.PicThumbnail = PicThumb;
                tbnews.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                tbnews.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                tbnews.Remark = txtRemark.Text.Trim();
                tbnews.CWhen = DateTime.Now;
                tbnews.CUser = Convert.ToInt32(security.LoginUID);
                tbnews.MWhen = DateTime.Now;
                tbnews.MUser = Convert.ToInt32(security.LoginUID);
                tbnews.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                tbnews.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue);
                tbnews.MetaKeywords = txtMetaKeywords.Text.Trim();
                tbnews.MetaDescription = txtMetaDescription.Text.Trim();
                tbnews.DetailSub = strDetailSub;

                //Insert data of news to database
                db.News.InsertOnSubmit(tbnews);
            }
            else //Update existing Event
            {
                var tbnews = from n in db.News
                              where n.UID == Convert.ToInt32(lblUID.Text.Trim())
                              select n;
                foreach (New n in tbnews)
                {
                    n.Subject = txtSubject.Text.Trim();
                    n.Detail = txtDetail.Text;
                    n.PicFull = PicFull;
                    n.PicThumbnail = PicThumb;
                    n.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    n.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                    n.Remark = txtRemark.Text.Trim();
                    n.MWhen = DateTime.Now;
                    n.MUser = Convert.ToInt32(security.LoginUID);
                    n.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                    n.MetaKeywords = txtMetaKeywords.Text.Trim();
                    n.MetaDescription = txtMetaDescription.Text.Trim();
                    try
                    {
                        n.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    n.DetailSub = strDetailSub;
                }
            }
            db.SubmitChanges();
            clsColorBox clsColorBox = new clsColorBox();
            clsColorBox.Refresh();
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "closeWindow", "<script language='javascript'>parent.$.colorbox.close();</script>");
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblUID.Text))
        {
            var tbNews = from n in db.News
                          where n.UID == Convert.ToInt32(lblUID.Text)
                          select n;
            foreach (New n in tbNews)
            {
                db.News.DeleteOnSubmit(n);
            }
            try
            {
                db.SubmitChanges();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "closeWindow", "<script language='javascript'>parent.$.colorbox.close();</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            }
        }
    }
}