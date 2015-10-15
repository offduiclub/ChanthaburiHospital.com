using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PromotionView : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    public clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;

    public PromotionView()
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
                pnAdminButton.Visible = true;
                //btAdmin.Visible = true;
            }
        }
    }
    private void BindPromotion()
    {
        var tbpromotion = from p in db.Promotions
                        where DateTime.Today >= p.ActiveDateFrom && (DateTime.Today <= p.ActiveDateTo || p.ActiveDateTo == null)
                      && p.StatusFlag == "A" && p.LanguageUID == findLangUID(strLang)
                        orderby p.ActiveDateFrom descending
                        select p;
        gvPromotion.DataSource = tbpromotion;
        gvPromotion.DataBind();
    }
    private void BindContent()
    {
        //Check Title
        if (strLang == "th-TH")
        {
            lblTitle.Text = "โปรโมชั่น";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > โปรโมชั่น";
            lblMenuPackage.Text = "แพ็คเกจ";
            lblMenuPromotion.Text = "โปรโมชั่น";
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "Promotion";
            lblSiteMap.Text = "Hospital News > Promotion";
            lblMenuPackage.Text = "Package";
            lblMenuPromotion.Text = "Promotion";
        }
        else
        {
            lblTitle.Text = "โปรโมชั่น";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > โปรโมชั่น";
            lblMenuPackage.Text = "แพ็คเกจ";
            lblMenuPromotion.Text = "โปรโมชั่น";
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
    //protected void btAdmin_Click(object sender, EventArgs e)
    //{
    //    ucColorBox1.IFrame("PromotionForm.aspx", "90%", "90%", true);
    //}
    protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UID = Convert.ToInt32(e.CommandArgument);
        //ถ้ากดปุ่ม Delete
        if (e.CommandName == "DeletePromotion")
        {
            var tbpromotion = from p in db.Promotions
                            where p.UID == UID
                            select p;
            foreach (Promotion p in tbpromotion)
            {
                //db.Promotions.DeleteOnSubmit(p);
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
        //ถ้ากดปุ่ม Edit
        else if (e.CommandName == "EditPromotion")
        {
            ucColorBox1.IFrame("PromotionForm.aspx?UID=" + UID + "", "90%", "90%", true);
        }
    }
    protected void gvPromotion_RowDataBound(object sender, GridViewRowEventArgs e)
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
}