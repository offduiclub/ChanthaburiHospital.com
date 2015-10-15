using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Data;


public partial class EventView : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    public clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public EventView()
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
                pnAdminButton.Visible = true;
                //btAdmin.Visible = true;
            }
        }
    }
    private void BindEvent()
    {
        var tbevent = from ev in db.Events
                      where (DateTime.Today >= ev.ActiveDateFrom || ev.ActiveDateFrom==null) && (DateTime.Today <= ev.ActiveDateTo || ev.ActiveDateTo == null)
                      && ev.StatusFlag == "A" && ev.LanguageUID == findLangUID(strLang)
                      orderby ev.ActiveDateFrom descending
                      select ev;
        //Response.Write("พบ "+tbevent.ToString());
        gvEvent.DataSource = tbevent;
        gvEvent.DataBind();
    }
    private void BindContent()
    {
        //Check Title
        if (strLang == "th-TH")
        {
            lblTitle.Text = "กิจกรรม";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > กิจกรรม";
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "Events";
            lblSiteMap.Text = "Hospital News > Events";
        }
        else
        {
            lblTitle.Text = "กิจกรรม";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > กิจกรรม";
        }
    }
    //protected void btAdmin_Click(object sender, EventArgs e)
    //{
    //    ucColorBox1.IFrame("EventForm.aspx", "90%", "90%", true);
    //}
    protected void gvEvent_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gvEvent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UID = Convert.ToInt32(e.CommandArgument);
        //ถ้ากดปุ่ม Delete
        if (e.CommandName == "DeleteEvent")
        {
            try
            {
                var tbEvent = from ev in db.Events
                              where ev.UID == UID
                              select ev;
                foreach (Event evt in tbEvent)
                {
                    db.Events.DeleteOnSubmit(evt);
                }

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            }
            BindEvent();
        }
        //ถ้ากดปุ่ม Edit
        else if (e.CommandName == "EditEvent")
        {
            ucColorBox1.IFrame("EventForm.aspx?UID=" + UID + "", "90%", "90%", true);
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
}