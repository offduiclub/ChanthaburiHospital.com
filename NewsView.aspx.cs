using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsView : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    public clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public NewsView()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindNews();
            BindContent();

            clsDefault clsDefault = new clsDefault();
            Response.Write(clsDefault.URLRouting("id"));
            Response.Write(clsDefault.URLRouting("name"));

            if (Security.LoginGroup == "Admin")
            {
                pnAdminButton.Visible = true;
                //btAdmin.Visible = true;
            }
        }
    }
    private void BindNews()
    {
        var tbNews = from n in db.News
                     where n.StatusFlag == "A" && DateTime.Today >= n.ActiveDateFrom && (DateTime.Today <= n.ActiveDateTo || n.ActiveDateTo == null)
                     && n.LanguageUID == findLangUID(strLang)
                     orderby n.ActiveDateFrom descending
                     select n;
        gvNews.DataSource = tbNews;
        gvNews.DataBind();
    }
    private void BindContent()
    {
        //Check Title
        if (strLang == "th-TH")
        {
            lblTitle.Text = "ข่าวประชาสัมพันธ์";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > ข่าวประชาสัมพันธ์";
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "News";
            lblSiteMap.Text = "Hospital News > News";
        }
        else
        {
            lblTitle.Text = "ข่าวประชาสัมพันธ์";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > ข่าวประชาสัมพันธ์";
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
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        return LangUID;
    }
    protected void gvNews_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gvNews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UID = Convert.ToInt32(e.CommandArgument);
        //ถ้ากดปุ่ม Delete
        if (e.CommandName == "DeleteNews")
        {
            var tbNews = from n in db.News
                          where n.UID == UID
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
        //ถ้ากดปุ่ม Edit
        else if (e.CommandName == "EditNews")
        {
            ucColorBox1.IFrame("NewsForm.aspx?UID=" + UID + "", "90%", "90%", false);
        }
    }
    //protected void btAdmin_Click(object sender, EventArgs e)
    //{
    //    ucColorBox1.IFrame("NewsForm.aspx", "90%", "90%", true);
    //}
}