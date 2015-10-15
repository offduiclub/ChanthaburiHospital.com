using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AboutHospital : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public AboutHospital()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        clsDefault clsDefault = new clsDefault();
        if (!Page.IsPostBack)
        {
            BindContent();
        }
    }
    private void BindContent()
    {
        //Check Title
        //if (strLang == "th-TH")
        //{
        //    lblSiteMap.Text = "รู้จักเรา > โรงพยาบาลกรุงเทพจันทบุรี";
        //}
        //else if (strLang == "en-US")
        //{
        //    lblSiteMap.Text = "About Hospital > Overview";
        //}
        //else
        //{
        //    lblSiteMap.Text = "รู้จักเรา > โรงพยาบาลกรุงเทพจันทบุรี";
        //}
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
}