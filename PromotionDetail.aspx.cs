using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PromotionDetial : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;
    public PromotionDetial()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindPromotion();
            BindContent();

            clsDefault clsDefault = new clsDefault();

            if (Security.LoginGroup == "Admin")
            {
                pnAdmin.Visible = true;
                //btEdit.Visible = true;
                //btDelete.Visible = true;
            }
        }
    }
    private void BindPromotion()
    {
        clsDefault clsDefault = new clsDefault();
        int UID = int.Parse(clsDefault.URLRouting("id"));
        if (!string.IsNullOrEmpty(UID.ToString()))
        {
            var tbPromotion = from p in db.Promotions
                            where p.UID == UID
                            select p;
            foreach (Promotion p in tbPromotion)
            {
                lblUID.Text = p.UID.ToString();
                lblSubject.Text = p.PromotionName;
                lblDetail.Text = p.Detail.Replace("'Upload/","'/Upload/");
                PicFull.ImageUrl = p.PicFull;
                lblSiteMap.Text = p.PromotionName;
                Page.MetaKeywords = p.MetaKeywords;
                Page.MetaDescription = p.MetaDescription;
            }
        }
        else
        {
            Response.Redirect("PromotionView.aspx");
        }
    }
    private void BindContent()
    {
        if (strLang == "th-TH")
        {
            lblTitle.Text = "โปรโมชั่น";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > <a href=\"PromotionView.aspx\">โปรโมชั่น</a> > " + lblSubject.Text;
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "Promotion";
            lblSiteMap.Text = "Hospital News > Promotion > " + lblSubject.Text;
        }
        else
        {
            lblTitle.Text = "โปรโมชั่น";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > โปรโมชั่ > " + lblSubject.Text;
        }
    }
    protected void btEdit_Click(object sender, EventArgs e)
    {
        ucColorBox1.IFrame("PromotionForm.aspx?UID=" + lblUID.Text.Trim() + "", "90%", "90%", false);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        var tbPromotion = from p in db.Promotions
                        where p.UID == Convert.ToInt32(lblUID.Text)
                        select p;
        foreach (Promotion p in tbPromotion)
        {
            db.Promotions.DeleteOnSubmit(p);
        }
        try
        {
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        BindPromotion();
    }
}