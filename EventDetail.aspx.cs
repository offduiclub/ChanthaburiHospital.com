using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

public partial class EventDetail : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public EventDetail()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindEvent();
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
    private void BindEvent()
    {
        clsDefault clsDefault = new clsDefault();
        int UID = Convert.ToInt32(clsDefault.URLRouting("id")); //Convert.ToInt32(Request.QueryString["UID"]);
        if (!string.IsNullOrEmpty(UID.ToString()))
        {
            var tbEvent = from ev in db.Events
                          where ev.UID == UID
                          select ev;
            foreach (Event ev in tbEvent)
            {
                lblUID.Text = ev.UID.ToString();
                lblSubject.Text = ev.Subject;
                lblDetail.Text = ev.Detail;
                PicFull.ImageUrl = ev.PicFull;
                lblSiteMap.Text = ev.Subject;
                Page.MetaKeywords = ev.MetaKeywords;
                Page.MetaDescription = ev.MetaDescription;
            }
        }
        else
        {
            Response.Redirect("EventView.aspx");
        }
    }
    protected void btEdit_Click(object sender, EventArgs e)
    {
        ucColorBox1.IFrame("EventForm.aspx?UID=" + lblUID.Text.Trim() + "", "90%", "90%", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        var tbEvent = from ev in db.Events
                      where ev.UID == Convert.ToInt32(lblUID.Text)
                      select ev;
        foreach (Event evt in tbEvent)
        {
            db.Events.DeleteOnSubmit(evt);
        }
        try
        {
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        BindEvent();
        
    }
    private void BindContent()
    {
        if (strLang == "th-TH")
        {
            lblTitle.Text = "กิจกรรม";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > กิจกรรม > " + lblSubject.Text;
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "Events";
            lblSiteMap.Text = "Hospital News > Events > " + lblSubject.Text;
        }
        else
        {
            lblTitle.Text = "กิจกรรม";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > กิจกรรม > " + lblSubject.Text;
        }
    }
}