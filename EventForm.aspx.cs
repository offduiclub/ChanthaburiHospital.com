using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmEvent : ucLanguage
{
    DBClassDataContext db = new DBClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Request.QueryString["UID"]))
            {
                lblTitle.Text = "เพิ่มกิจกรรม";
            }
            else
            {
                lblTitle.Text = "แก้ไขกิจกรรม";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        if (!Page.IsPostBack)
        {
            BindLanguage();
            BindEvent();
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        clsSecurity security = new clsSecurity();
        clsIO IO = new clsIO();

        string strDetailSub = string.Empty;
        string strDetail = txtDetail.Text.Replace("<p>", "");
        strDetail = strDetail.Replace("</p>", "");
        string PicFull = "null";
        string PicThumb = "null";
        string pathPhoto = "/Images/Event/";
        string pathImages = @"\Images\Event\";
        string outError;

        //upload Photo to server
        if(txtImgFull.HasFile)
        {
            //Full Images
            if (IO.UploadPhoto(txtImgFull, pathPhoto, "f_pic" + DateTime.Now.ToString("yyyyMMddHHmmss"), 5120, 0, 0, "", 0, out outError, out PicFull) == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + outError.ToString() + "')", true);
                return;
            }
            else
            {
                PicFull = pathImages + "" + PicFull;
            }
        }
        else
        {
            PicFull = lblImagesFull.Text.Trim();
        }
        if (txtImgThum.HasFile)
        {
            //Thumb Images
            if (IO.UploadPhoto(txtImgThum, pathPhoto, "t_pic" + DateTime.Now.ToString("yyyyMMddHHmmss"), 2048, 0, 0, "", 0, out outError, out PicThumb) == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + outError.ToString() + "')", true);
                return;
            }
            else
            {
                PicThumb = pathImages + "" + PicThumb;
            }
        }
        else
        {
            PicThumb = lblImagesThumb.Text.Trim();
        }

        //insert data to database
        try
        {
            if (txtDetail.Text.Length >= 200)
            {
                strDetailSub = "<p>" + strDetail.Substring(0, 200) + ".....</p>";
            }
            else
            {
                strDetailSub = "<p>" + strDetail + ".....</p>";
            }

            //Insert new Event
            if (string.IsNullOrEmpty(lblUID.Text) == true)
            {
                Event tbevent = new Event();

                //Assign values for insert into database
                tbevent.Subject = txtSubject.Text.Trim();
                tbevent.Detail = txtDetail.Text;
                tbevent.PicFull = PicFull;
                tbevent.PicThumbnail = PicThumb;
                tbevent.DepartmentUID = 1;
                tbevent.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                tbevent.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                tbevent.Remark = txtRemark.Text.Trim();
                tbevent.CWhen = DateTime.Now;
                tbevent.CUser = Convert.ToInt32(security.LoginUID);
                tbevent.MWhen = DateTime.Now;
                tbevent.MUser = Convert.ToInt32(security.LoginUID);
                tbevent.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                tbevent.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue);
                tbevent.DetailSub = strDetailSub;
                tbevent.MetaKeywords = txtMetaKeywords.Text.Trim();
                tbevent.MetaDescription = txtMetaDescription.Text.Trim();

                //Insert data of Event to database
                db.Events.InsertOnSubmit(tbevent);
            }
            else //Update existing Event
            {
                var tbEvent = from ev in db.Events
                              where ev.UID == Convert.ToInt32(lblUID.Text.Trim())
                              select ev;
                foreach (Event ev in tbEvent)
                {
                    ev.Subject = txtSubject.Text.Trim();
                    ev.Detail = txtDetail.Text;
                    ev.PicFull = PicFull;
                    ev.PicThumbnail = PicThumb;
                    ev.DepartmentUID = 1;
                    ev.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    ev.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                    ev.Remark = txtRemark.Text.Trim();
                    ev.MWhen = DateTime.Now;
                    ev.MUser = Convert.ToInt32(security.LoginUID);
                    ev.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                    ev.MetaKeywords = txtMetaKeywords.Text.Trim();
                    ev.MetaDescription = txtMetaDescription.Text.Trim();
                    try
                    {
                       ev.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue); 
                    }
                    catch(Exception ex)
                    {
                        ex.ToString();
                    }
                    ev.DetailSub = strDetailSub;
                }
            }
            db.SubmitChanges();
            clsColorBox clsColorBox = new clsColorBox();
            clsColorBox.Refresh();
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "closeWindow", "<script language='javascript'>parent.$.colorbox.close();</script>");
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        
    }
    private void BindEvent()
    {
        var tbEvent = from ev in db.Events
                      where ev.UID == Convert.ToInt32(Request.QueryString["UID"])
                      select ev;
        try
        {
            foreach (Event ev in tbEvent)
            {
                lblUID.Text = ev.UID.ToString();
                txtSubject.Text = ev.Subject;
                txtDetail.Text = ev.Detail;
                lblImagesFull.Text = ev.PicFull;
                lblImagesThumb.Text = ev.PicThumbnail;
                txtDateFrom.Text = ev.ActiveDateFrom.ToString();
                txtDateTo.Text = ev.ActiveDateTo.ToString();
                txtRemark.Text = ev.Remark;
                lblLangUID.Text = ev.LanguageUID.ToString();
                txtMetaKeywords.Text = ev.MetaKeywords;
                txtMetaDescription.Text = ev.MetaDescription;
                if (!string.IsNullOrEmpty(lblLangUID.Text))
                {
                    BindLanguageByUID(Convert.ToInt32(lblLangUID.Text));
                }
                if (ev.StatusFlag == "A")
                {
                    rdbActive.Checked = true;
                }
                else
                {
                    rdbInactive.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
    }
    private void BindLanguage()
    {
        var tbLang = from l in db.Languages
                     where l.Active == '1' 
                     orderby l.Name ascending
                     select l;
        ddlLanguage.DataSource = tbLang;
        ddlLanguage.DataValueField = "UID";
        ddlLanguage.DataTextField = "Detail";
        ddlLanguage.DataBind();
    }
    private void BindLanguageByUID(int LangUID)
    {
        string Lang = string.Empty;
        var tbLang = from l in db.Languages
                     where l.Active == '1' && l.UID == LangUID
                     select l;
        try
        {
            foreach (Language l in tbLang)
            {
                Lang = l.Detail;
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        ddlLanguage.Items.Insert(0, Lang);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblUID.Text))
        {
            var tbEvent = from ev in db.Events
                          where ev.UID == Convert.ToInt32(lblUID.Text)
                          select ev;
            foreach (Event ev in tbEvent)
            {
                db.Events.DeleteOnSubmit(ev);
            }
            try
            {
                db.SubmitChanges();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "closeWindow", "<script language='javascript'>parent.$.colorbox.close();</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            }
        }
    }
}