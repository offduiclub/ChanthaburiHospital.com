using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ArticleDetail : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public ArticleDetail()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindArticle();
            BindContent();

            clsDefault clsDefault = new clsDefault();

            if (Security.LoginGroup == "Admin")
            {
                pnAdmin.Visible = true;
                //btEdit.Visible = true;
                btDelete.Visible = true;
            }
        }
    }
    private void BindArticle()
    {
        clsDefault clsDefault = new clsDefault();
        int UID = Convert.ToInt32(clsDefault.URLRouting("id"));
        if (!string.IsNullOrEmpty(UID.ToString()))
        {
            try
            {
                var tbArticle = from a in db.Articles
                                where a.UID == UID
                                select a;
                foreach (Article a in tbArticle)
                {
                    lblUID.Text = a.UID.ToString();
                    lblSubject.Text = a.Subject;
                    lblDetail.Text = a.Detail;
                    PicFull.ImageUrl = a.PicFull;
                    lblSiteMap.Text = a.Subject;
                    Page.MetaKeywords = a.MetaKeywords;
                    Page.MetaDescription = a.MetaDescription;

                    //Update View Article
                    a.NumberView = a.NumberView + 1;
                    NumberView.Text = a.NumberView.ToString()+" View";
                    NumberLike.Text = a.NumberLike.ToString();
                    NumberDisLike.Text = a.NumberDislike.ToString();
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            }
        }
        else
        {
            Response.Redirect("ArticleView.aspx");
        }
    }
    private void BindContent()
    {
        if (strLang == "th-TH")
        {
            lblTitle.Text = "บทความสุขภาพ";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > บทความสุขภาพ > " + lblSubject.Text;
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "Events";
            lblSiteMap.Text = "Hospital News > Article > " + lblSubject.Text;
        }
        else
        {
            lblTitle.Text = "บทความสุขภาพ";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > บทความสุขภาพ > " + lblSubject.Text;
        }
    }
    protected void btEdit_Click(object sender, EventArgs e)
    {
        ucColorBox1.IFrame("ArticleForm.aspx?UID=" + lblUID.Text.Trim() + "", "90%", "90%", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
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
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        BindArticle();
        
    }
    protected void btLike_Click(object sender, ImageClickEventArgs e)
    {
        clsDefault clsDefault = new clsDefault();
        int UID = Convert.ToInt32(clsDefault.URLRouting("id"));

            try
            {
                var tbArticle = from a in db.Articles
                                where a.UID == UID
                                select a;
                foreach (Article a in tbArticle)
                {
                    //Update Like Article
                    a.NumberLike = a.NumberLike + 1;
                    NumberLike.Text = a.NumberLike.ToString();
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            }
    }
    protected void btDisLike_Click(object sender, ImageClickEventArgs e)
    {
        clsDefault clsDefault = new clsDefault();
        int UID = Convert.ToInt32(clsDefault.URLRouting("id"));

        try
        {
            //var tbArticle = from a in db.Articles
            //                where a.UID == UID
            //                select a;
            //foreach (Article a in tbArticle)
            //{
            //    //Update Like Article
            //    a.NumberDislike = a.NumberDislike + 1;
            //    NumberDisLike.Text = a.NumberDislike.ToString();
            //}
            //db.SubmitChanges();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
    }
}