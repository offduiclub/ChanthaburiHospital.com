using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Data;

public partial class ucLanguageDB : System.Web.UI.UserControl
{
    private clsSQL clsSQL = new clsSQL();
    private string _width = "120px";

    #region Property
    private string _cookieName = "language";
    public string CookieName
    {
        get { return _cookieName; }
        set { _cookieName = value; }
    }

    private string _languageDefault = "th-TH";
    public string LanguageDefault
    {
        get { return _languageDefault; }
        set { _languageDefault = value; }
    }

    private string _languageCurrent;
    public string LanguageCurrent
    {
        get
        {
            #region Find Cookie
            HttpCookie cookie = Request.Cookies[_cookieName];
            if (cookie != null && cookie.Value != null)
            {
                _languageCurrent = cookie.Value;
            }
            else
            {
                _languageCurrent = _languageDefault;
            }
            #endregion
            return _languageCurrent;
        }
    }
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
            HttpCookie cookie = Request.Cookies[_cookieName];

            #region BindLanguage
            BindLanguage();
            #endregion
            #region Language Choose
            if (cookie != null && cookie.Value != null)
            {
                if (cookie.Value == "th-TH")
                {
                    ddlLanguage.SelectedValue = "th-TH";
                }
                else if (cookie.Value == "en-US")
                {
                    ddlLanguage.SelectedValue = "en-US";
                }
                else if (cookie.Value == "km-KH")
                {
                    ddlLanguage.SelectedValue = "km-KH";
                }
                else
                {
                    ddlLanguage.SelectedValue = _languageDefault;
                }
            }
            #endregion
        }
    }

    private void BindLanguage()
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Icon,Name,Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("Language ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem li = new ListItem();
                ddlLanguage.Width = Unit.Parse(_width);

                li = new ListItem();
                li.Value = dt.Rows[i]["Name"].ToString();
                li.Text = dt.Rows[i]["Detail"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[i]["Icon"].ToString()))
                {
                    li.Attributes.Add("data-image", dt.Rows[i]["Icon"].ToString());
                }

                ddlLanguage.Items.Add(li);
            }
        }
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedItem.Value != "null")
        {
            HttpCookie cookie = new HttpCookie(_cookieName);
            cookie.Value = ddlLanguage.SelectedItem.Value;
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }
    }
}

/// <summary>
/// คลาสที่ใช้สำหรับทุก Page Inherit แทน System.Web.UI.Page เช่น public partial class EventManage : ucLanguage
/// </summary>
public class ucLanguage : System.Web.UI.Page
{
    protected override void InitializeCulture()
    {
        ucLanguageDB ucLanguage = new ucLanguageDB();
        string language = string.Empty;
        HttpCookie cookie = Request.Cookies[ucLanguage.CookieName];

        if (cookie != null && cookie.Value != null)
        {
            #region Found Cookie
            language = cookie.Value;
            CultureInfo ci = CultureInfo.CreateSpecificCulture(language);

            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            #endregion
        }
        else
        {
            #region Not Found Cookie
            if (string.IsNullOrEmpty(language)) language = ucLanguage.LanguageDefault;
            CultureInfo ci = CultureInfo.CreateSpecificCulture(language);

            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;

            HttpCookie cookieNew = new HttpCookie(ucLanguage.CookieName);
            cookieNew.Value = language;
            Response.SetCookie(cookieNew);
            #endregion
        }

        base.InitializeCulture();
    }
}