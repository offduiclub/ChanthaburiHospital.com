using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ArticleForm : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Request.QueryString["UID"]))
            {
                lblTitle.Text = "เพิ่มบทความสุขภาพ";
            }
            else
            {
                lblTitle.Text = "แก้ไขบทความสุขภาพ";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        if (!Page.IsPostBack)
        {
            BindLanguage();
            BindArticle();
        }
    }
    private void BindArticle()
    {
        //var tbArticle = from a in db.Articles
        //              where a.UID == Convert.ToInt32(Request.QueryString["UID"])
        //              select a;
        //try
        //{
        //    foreach (Article a in tbArticle)
        //    {
        //        lblUID.Text = a.UID.ToString();
        //        txtSubject.Text = a.Subject;
        //        txtDetail.Text = a.Detail;
        //        lblImagesFull.Text = a.PicFull;
        //        lblImagesThumb.Text = a.PicThumbnail;
        //        txtDateFrom.Text = a.ActiveDateFrom.ToString();
        //        txtDateTo.Text = a.ActiveDateTo.ToString();
        //        txtRemark.Text = a.Remark;
        //        lblLangUID.Text = a.LanguageUID.ToString();
        //        txtMetaKeywords.Text = a.MetaKeywords;
        //        txtMetaDescription.Text = a.MetaDescription;
        //        if (!string.IsNullOrEmpty(lblLangUID.Text))
        //        {
        //            BindLanguageByUID(Convert.ToInt32(lblLangUID.Text));
        //        }
        //        if (a.StatusFlag == "A")
        //        {
        //            rdbActive.Checked = true;
        //        }
        //        else
        //        {
        //            rdbInactive.Checked = true;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        //}
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
        clsSecurity security = new clsSecurity();
        clsIO IO = new clsIO();

        string strDetailSub = string.Empty;
        string strDetail = txtDetail.Text.Replace("<p>", "");
        strDetail = strDetail.Replace("</p>", "");
        string PicFull = "null";
        string PicThumb = "null";
        string pathPhoto = "/Images/Article/";
        string pathImages = @"\Images\Article\";
        string outError;

        //upload Photo to server
        if (txtImanges.HasFile)
        {
            //Full Images
            if (IO.UploadPhoto(txtImanges, pathPhoto, "f_pic" + DateTime.Now.ToString("yyyyMMddHHmmss"), 5120, 0, 0, "", 0, out outError, out PicFull) == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + outError.ToString() + "')", true);
                return;
            }
            else
            {
                PicFull = pathImages + "" + PicFull;
            }
            //Thumb Images
            if (IO.UploadPhoto(txtImanges, pathPhoto, "t_pic" + DateTime.Now.ToString("yyyyMMddHHmmss"), 1024, 100, 100, "", 0, out outError, out PicThumb) == false)
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
            PicFull = lblImagesFull.Text.Trim();
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

            //Insert new Article
            if (string.IsNullOrEmpty(lblUID.Text) == true)
            {
                Article tbarticle = new Article();

                //Assign values for insert into database
                tbarticle.Subject = txtSubject.Text.Trim();
                tbarticle.Detail = txtDetail.Text;
                tbarticle.PicFull = PicFull;
                tbarticle.PicThumbnail = PicThumb;
                tbarticle.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                tbarticle.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                tbarticle.Remark = txtRemark.Text.Trim();
                tbarticle.CWhen = DateTime.Now;
                tbarticle.CUser = Convert.ToInt32(security.LoginUID);
                tbarticle.MWhen = DateTime.Now;
                tbarticle.MUser = Convert.ToInt32(security.LoginUID);
                tbarticle.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                tbarticle.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue);
                tbarticle.DetailSub = strDetailSub;
                tbarticle.NumberDislike = 0;
                tbarticle.NumberLike = 0;
                tbarticle.NumberView = 0;
                tbarticle.Score = 0;
                tbarticle.SampleSize = 0;
                tbarticle.MetaKeywords = txtMetaKeywords.Text.Trim();
                tbarticle.MetaDescription = txtMetaDescription.Text.Trim();

                //Insert data of Event to database
                db.Articles.InsertOnSubmit(tbarticle);
            }
            else //Update existing Event
            {
                var tbarticle = from a in db.Articles
                              where a.UID == Convert.ToInt32(lblUID.Text.Trim())
                              select a;
                foreach (Article a in tbarticle)
                {
                    a.Subject = txtSubject.Text.Trim();
                    a.Detail = txtDetail.Text;
                    a.PicFull = PicFull;
                    a.PicThumbnail = PicThumb;
                    a.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    a.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                    a.Remark = txtRemark.Text.Trim();
                    a.MWhen = DateTime.Now;
                    a.MUser = Convert.ToInt32(security.LoginUID);
                    a.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                    a.MetaKeywords = txtMetaKeywords.Text.Trim();
                    a.MetaDescription = txtMetaDescription.Text.Trim();
                    try
                    {
                        a.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    a.DetailSub = strDetailSub;
                }
            }
            db.SubmitChanges();
            clsColorBox clsColorBox = new clsColorBox();
            clsColorBox.Refresh();
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "closeWindow", "<script language='javascript'>parent.$.colorbox.close();</script>");
        }
        catch (Exception ex)
        {
            clsColorBox clsColorBox = new clsColorBox();
            clsColorBox.Refresh();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblUID.Text))
        {
            var tbArticle = from a in db.Articles
                          where a.UID == Convert.ToInt32(lblUID.Text)
                          select a;
            foreach (Article a in tbArticle)
            {
                db.Articles.DeleteOnSubmit(a);
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