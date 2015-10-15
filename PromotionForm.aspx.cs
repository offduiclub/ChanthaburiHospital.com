using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PromotionForm : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Request.QueryString["UID"]))
            {
                lblTitle.Text = "เพิ่มโปรโมชั่น";
            }
            else
            {
                lblTitle.Text = "แก้ไขโปรโมชั่น";
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        if (!Page.IsPostBack)
        {
            BindLanguage();
            BindDepartment();
            BindPromotion();
        }
    }
    private void BindPromotion()
    {
        var tbPromotion = from p in db.Promotions
                        where p.UID == Convert.ToInt32(Request.QueryString["UID"])
                        select p;
        try
        {
            foreach (Promotion p in tbPromotion)
            {
                lblUID.Text = p.UID.ToString();
                txtSubject.Text = p.PromotionName;
                txtDetail.Text = p.Detail;
                lblImagesFull.Text = p.PicFull;
                lblImagesThumb.Text = p.PicThumbnail;
                txtDateFrom.Text = p.ActiveDateFrom.ToString();
                txtDateTo.Text = p.ActiveDateTo.ToString();
                txtRemark.Text = p.Remark;
                lblLangUID.Text = p.LanguageUID.ToString();
                txtMetaKeywords.Text = p.MetaKeywords;
                txtMetaDescription.Text = p.MetaDescription;
                if (!string.IsNullOrEmpty(lblLangUID.Text))
                {
                    BindLanguageByUID(Convert.ToInt32(lblLangUID.Text));
                }
                if (!string.IsNullOrEmpty(lblDepartmentUID.Text))
                {
                    BindDepartmentByUID(Convert.ToInt32(lblDepartmentUID.Text));
                }
                if (p.StatusFlag == "A")
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
    private void BindDepartment()
    {
        var tbDept = from d in db.MedicalCenters
                     where d.Active == '1'
                     orderby d.Name ascending
                     select d;
        ddlDepartment.DataSource = tbDept;
        ddlDepartment.DataValueField = "DepartmentUID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
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
    private void BindDepartmentByUID(int DepartmentUID)
    {
        //string Dept = string.Empty;
        //var tbDept = from d in db.MedicalCenters
        //             where d.Active == '1' && d.DepartmentUID == DepartmentUID
        //             select d;
        //try
        //{
        //    foreach (MedicalCenter d in tbDept)
        //    {
        //        Dept = d.Name;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        //}
        //ddlDepartment.Items.Insert(0, Dept);
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        clsSecurity security = new clsSecurity();
        clsIO IO = new clsIO();

        string strDetailSub = string.Empty;
        string strDetail = txtDetail.Text.Replace("<p>", "");
        strDetail = strDetail.Replace("</p>", "");
        string PicFull = "null";
        string PicThumb = "null";
        string pathPhoto = "/Images/Promotion/";
        string pathImages = @"\Images\Promotion\";
        string outError;

        //upload Photo to server
        if (txtImgFull.HasFile)
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

            //Insert new Promotion
            if (string.IsNullOrEmpty(lblUID.Text) == true)
            {
                Promotion tbpromotion = new Promotion();

                //Assign values for insert into database
                tbpromotion.PromotionName = txtSubject.Text.Trim();
                tbpromotion.Detail = txtDetail.Text;
                tbpromotion.PicFull = PicFull;
                tbpromotion.PicThumbnail = PicThumb;
                tbpromotion.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                if (!string.IsNullOrEmpty(txtDateTo.Text))
                {
                    tbpromotion.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                }
                else
                {
                    tbpromotion.ActiveDateTo = null;
                }
                tbpromotion.Remark = txtRemark.Text.Trim();
                tbpromotion.CWhen = DateTime.Now;
                tbpromotion.CUser = Convert.ToInt32(security.LoginUID);
                tbpromotion.MWhen = DateTime.Now;
                tbpromotion.MUser = Convert.ToInt32(security.LoginUID);
                tbpromotion.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                tbpromotion.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue);
                tbpromotion.DepartmentUID = Convert.ToInt32(ddlDepartment.SelectedValue);
                tbpromotion.DetailSub = strDetailSub;
                tbpromotion.MetaKeywords = txtMetaKeywords.Text.Trim();
                tbpromotion.MetaDescription = txtMetaDescription.Text.Trim();

                //Insert data of Event to database
                db.Promotions.InsertOnSubmit(tbpromotion);
            }
            else //Update existing Event
            {
                var tbpromotion = from p in db.Promotions
                                where p.UID == Convert.ToInt32(lblUID.Text.Trim())
                                select p;
                foreach (Promotion p in tbpromotion)
                {
                    p.PromotionName = txtSubject.Text.Trim();
                    p.Detail = txtDetail.Text;
                    p.PicFull = PicFull;
                    p.PicThumbnail = PicThumb;
                    p.ActiveDateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    if (!string.IsNullOrEmpty(txtDateTo.Text))
                    {
                        p.ActiveDateTo = Convert.ToDateTime(txtDateTo.Text);
                    }
                    else
                    {
                        p.ActiveDateTo = null;
                    }
                    p.Remark = txtRemark.Text.Trim();
                    p.MWhen = DateTime.Now;
                    p.MUser = Convert.ToInt32(security.LoginUID);
                    p.StatusFlag = rdbActive.Checked == true ? "A" : "D";
                    try
                    {
                        p.LanguageUID = Convert.ToInt32(ddlLanguage.SelectedValue);
                        p.DepartmentUID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    p.DetailSub = strDetailSub;
                    p.MetaKeywords = txtMetaKeywords.Text.Trim();
                    p.MetaDescription = txtMetaDescription.Text.Trim();
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
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblUID.Text))
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
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "closeWindow", "<script language='javascript'>parent.$.colorbox.close();</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            }
        }
    }
}