using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Awards : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;
    
    public Awards()
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