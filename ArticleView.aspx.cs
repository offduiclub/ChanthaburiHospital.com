using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ArticleView : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    public clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public ArticleView()
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
                pnAdminButton.Visible = true;
                //btAdmin.Visible = true;
            }
        }
    }
    private void BindArticle()
    {
        var tbarticle = from a in db.Articles
                      where DateTime.Today >= a.ActiveDateFrom && (DateTime.Today <= a.ActiveDateTo || a.ActiveDateTo == null)
                      && a.StatusFlag == "A" && a.LanguageUID == findLangUID(strLang)
                      orderby a.ActiveDateFrom descending
                      select a;
        gvArticle.DataSource = tbarticle;
        gvArticle.DataBind();
    }
    private void BindContent()
    {
        //Check Title
        if (strLang == "th-TH")
        {
            lblTitle.Text = "บทความสุขภาพ";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > บทความสุขภาพ";
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "Events";
            lblSiteMap.Text = "Hospital News > Article";
        }
        else
        {
            lblTitle.Text = "บทความสุขภาพ";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > บทความสุขภาพ";
        }
    }
    private int findLangUID(string LangShort)
    {
        int LangUID = 0;
        try
        {
            var tbLang = from l in db.Languages
                         where l.Name == LangShort.Trim() && l.Active == '1'
                         select l;
            foreach (Language l in tbLang)
            {
                LangUID = l.UID;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        return LangUID;
    }
    protected void gvArticle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ถ้า Login ให้แสดงปุ่ม Delete
            if (Security.LoginGroup == "Admin")
            {
                ((Button)e.Row.FindControl("btDelete")).Visible = true;
                //((Button)e.Row.FindControl("btEdit")).Visible = true;
            }
            else//ถ้าไม่ได้ Login ให้ซ่อนปุ่ม Delete
            {
                ((Button)e.Row.FindControl("btDelete")).Visible = false;
                //((Button)e.Row.FindControl("btEdit")).Visible = false;
            }
        }
    }
    protected void gvArticle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UID = Convert.ToInt32(e.CommandArgument);
        //ถ้ากดปุ่ม Delete
        //if (e.CommandName == "DeleteArticle")
        //{
        //    var tbArticle = from a in db.Articles
        //                  where a.UID == UID
        //                  select a;
        //    foreach (Article a in tbArticle)
        //    {
        //        db.Articles.DeleteOnSubmit(a);
        //    }
        //    try
        //    {
        //        db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        //    }
        //    BindArticle();
        //}
        ////ถ้ากดปุ่ม Edit
        //else if (e.CommandName == "EditArticle")
        //{
        //    ucColorBox1.IFrame("ArticleForm.aspx?UID=" + UID + "", "90%", "90%", true);
        //}
    }

    //protected void btAdmin_Click(object sender, EventArgs e)
    //{
    //    ucColorBox1.IFrame("ArticleForm.aspx", "90%", "90%", true);
    //}
}