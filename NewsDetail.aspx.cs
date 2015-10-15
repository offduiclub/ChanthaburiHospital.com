using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsDetail : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public NewsDetail()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BindNews();
        BindContent();

        clsDefault clsDefault = new clsDefault();
        //Response.Write(clsDefault.URLRouting("id"));
        //Response.Write(clsDefault.URLRouting("name"));

        if (Security.LoginGroup == "Admin")
        {
            pnAdmin.Visible = true;
            //btEdit.Visible = true;
            btDelete.Visible = true;
        }
    }

    private void BindNews()
    {
        clsDefault clsDefault = new clsDefault();
        if (!string.IsNullOrEmpty(clsDefault.URLRouting("id")))
        {
            int UID = Convert.ToInt32(clsDefault.URLRouting("id"));
            if (!string.IsNullOrEmpty(UID.ToString()))
            {
                var tbNews = from n in db.News
                             where n.UID == UID
                             select n;
                foreach (New n in tbNews)
                {
                    lblUID.Text = n.UID.ToString();
                    lblSubject.Text = n.Subject;
                    lblDetail.Text = n.Detail;
                    PicFull.ImageUrl = n.PicFull;
                    lblSiteMap.Text = n.Subject;
                    Page.MetaKeywords = n.MetaKeywords;
                    Page.MetaDescription = n.MetaDescription;
                }
            }
            else
            {
                Response.Redirect("NewsViews.aspx");
            }
        }
        else
        {
            Response.Redirect("NewsViews.aspx");
        }
    }
    private void BindContent()
    {
        if (strLang == "th-TH")
        {
            lblTitle.Text = "ข่าวประชาสัมพันธ์";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > ข่าวประชาสัมพันธ์ > " + lblSubject.Text;
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "News";
            lblSiteMap.Text = "Hospital News > News > " + lblSubject.Text;
        }
        else
        {
            lblTitle.Text = "ข่าวประชาสัมพันธ์";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > ข่าวประชาสัมพันธ์ > " + lblSubject.Text;
        }
    }
    protected void btEdit_Click(object sender, EventArgs e)
    {
        ucColorBox1.IFrame("NewsForm.aspx?UID=" + lblUID.Text.Trim() + "", "90%", "90%", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
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
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        BindNews();
    }
}