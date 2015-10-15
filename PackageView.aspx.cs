using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackageView : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    public clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;
    clsPackage package = new clsPackage();
    dsPackage.dtSelectedPackageDataTable dtSelectedPackage = new dsPackage.dtSelectedPackageDataTable();
    dsPackage.dtSelectedPackageRow dtSelectedPackageRow;

    public PackageView()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindPackage();
            BindContent();
            BindSelectedPackageNotPostBack();

            clsDefault clsDefault = new clsDefault();

            if (Security.LoginGroup == "Admin")
            {
                pnAdminButton.Visible = true;
                //btAdmin.Visible = true;
            }
        }
        else
        {
            BindSelectedPackage();
        }
    }
    private void BindPackage()
    {
        var tbpackage = from p in db.Packages
                        where DateTime.Today >= p.ActiveDateFrom && (DateTime.Today <= p.ActiveDateTo || p.ActiveDateTo == null)
                      && p.StatusFlag == "A" && p.LanguageUID == findLangUID(strLang)
                        orderby p.ActiveDateFrom descending
                        select p;
        gvPackage.DataSource = tbpackage;
        gvPackage.DataBind();
        
    }
    private void BindContent()
    {
        //Check Title
        if (strLang == "th-TH")
        {
            lblTitle.Text = "แพ็คเกจ";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > แพ็คเกจ";
            lblMenuPackage.Text = "แพ็คเกจ";
            lblMenuPromotion.Text = "โปรโมชั่น";

            lblTitleSelectedPackage.Text = "แพ็คเกจที่เลือก";
            lblTotal.Text = "ราคารวม";
            lblVat.Text = "ภาษี";
            lblGrandTotal.Text = "รวมทั้งสิ้น";
            
        }
        else if (strLang == "en-US")
        {
            lblTitle.Text = "Package";
            lblSiteMap.Text = "Hospital News > Package";
            lblMenuPackage.Text = "Package";
            lblMenuPromotion.Text = "Promotion";

            lblTitleSelectedPackage.Text = "Selected Package";
            lblTotal.Text = "Total";
            lblVat.Text = "Vat";
            lblGrandTotal.Text = "Grand Total";
        }
        else
        {
            lblTitle.Text = "แพ็คเกจ";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > แพ็คเกจ";
            lblMenuPackage.Text = "แพ็คเกจ";
            lblMenuPromotion.Text = "โปรโมชั่น";

            lblTitleSelectedPackage.Text = "แพ็คเกจที่เลือก";
            lblTotal.Text = "ราคารวม";
            lblVat.Text = "ภาษี";
            lblGrandTotal.Text = "รวมทั้งสิ้น";
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
    //    ucColorBox1.IFrame("PackageForm.aspx", "90%", "90%", true);
    //}
    protected void gvPackage_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gvPackage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UID = Convert.ToInt32(e.CommandArgument);
        //ถ้ากดปุ่ม Delete
        if (e.CommandName == "DeletePackage")
        {
            //var tbpackage = from p in db.Packages
            //              where p.UID == UID
            //              select p;
            //foreach (Package p in tbpackage)
            //{
            //    db.Packages.DeleteOnSubmit(p);
            //}
            //try
            //{
            //    db.SubmitChanges();
            //}
            //catch (Exception ex)
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            //}
            //BindPackage();
        }
        //ถ้ากดปุ่ม Edit
        else if (e.CommandName == "EditPackage")
        {
            ucColorBox1.IFrame("PackageForm.aspx?UID=" + UID + "", "90%", "90%", true);
        }
        //ถ้ากดปุ่ม Buy Package
        else if (e.CommandName == "BuyPackage")
        {
            //เช็คว่า Package นี้ถูกเลือกแล้วหรือยัง
            if (FindRepeatPackage(UID) == false)
            {
                DataTable dt = new DataTable();
                dt = findPackagByUID(UID);
                package.UID = Convert.ToInt32(dt.Rows[0]["UID"]);
                package.PackageCode = dt.Rows[0]["PackageCode"].ToString();
                package.PackageName = dt.Rows[0]["PackageName"].ToString();
                package.UnitPrice = Convert.ToInt32(dt.Rows[0]["UnitPrice"]);
                package.Qty = 1;

                Session["Package"] = package;
                Session["selectedPackage"] = dtSelectedPackage;
                BindSelectedPackage();
            }
            else
            {
               ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('Already add this package.')", true);
            }
        }
    }
    private DataTable findPackagByUID(int UID)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UID", typeof(int));
        dt.Columns.Add("PackageCode",typeof(string));
        dt.Columns.Add("PackageName",typeof(string));
        dt.Columns.Add("UnitPrice",typeof(int));

        var tbpackage = from p in db.Packages
                        where p.UID == UID 
                        select p;

        foreach (var item in tbpackage)
        {
            DataRow dr = dt.NewRow();
            dr["UID"] = item.UID;
            dr["PackageCode"] = item.PackageCode;
            dr["PackageName"] = item.PackageName;
            dr["UnitPrice"] = item.UnitPrice;
            dt.Rows.Add(dr);
        }

        return dt;
    }
    private void BindSelectedPackage()
    {
        if ((clsPackage)Session["Package"] != null)
        {
            if ((dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"] != null)
            {
                dtSelectedPackage = (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"];
            }

            package = (clsPackage)Session["Package"];
            int index = dtSelectedPackage.Rows.Count;
            index++;

            dtSelectedPackageRow = dtSelectedPackage.NewdtSelectedPackageRow();
            dtSelectedPackageRow["Index"] = index;
            dtSelectedPackageRow["UID"] = package.UID;
            dtSelectedPackageRow["PackageCode"] = package.PackageCode;
            dtSelectedPackageRow["PackageName"] = package.PackageName;
            dtSelectedPackageRow["UnitPrice"] = package.UnitPrice;
            dtSelectedPackageRow["Qty"] = package.Qty;

            dtSelectedPackage.AdddtSelectedPackageRow(dtSelectedPackageRow);
            gvSelectedPackage.DataSource = dtSelectedPackage;
            gvSelectedPackage.DataBind();

            CalculatePackage();

            Session["Package"] = dtSelectedPackage;
            Session["Package"] = null;
        }
        if ((dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"] != null)
        {
            dtSelectedPackage = (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"];
        }
    }
    private void BindSelectedPackageNotPostBack()
    {
        if ((dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"] != null)
        {
            dtSelectedPackage = (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"];

            gvSelectedPackage.DataSource = dtSelectedPackage;
            gvSelectedPackage.DataBind();

            CalculatePackage();
        }
    }
    private void CalculatePackage()
    {
        if (dtSelectedPackage.Rows.Count > 0)
        {
            int rowIndex = gvSelectedPackage.Rows.Count;
            decimal total = 0;
            for (int i = 0; i < rowIndex; i++)
            {
                total = total + (Convert.ToDecimal(dtSelectedPackage.Rows[i]["UnitPrice"]) * Convert.ToInt32(dtSelectedPackage.Rows[i]["Qty"]));
            }
            txtTotal.Text = string.Format("{0:#,#.#}", total);

            decimal vat = Convert.ToDecimal(total * 7 / 100);
            txtVat.Text = string.Format("{0:#,#.#}", vat);
            txtGrandTotal.Text = string.Format("{0:#,#.#}", Convert.ToDecimal(total + vat));
        }
    }
    protected void gvSelectedPackage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "RemovePackage")
        {
            dtSelectedPackage = (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"];
            dtSelectedPackage.Rows[index-1].Delete();
            gvSelectedPackage.DataSource = dtSelectedPackage;
            gvSelectedPackage.DataBind();
            Session["selectedPackage"] = dtSelectedPackage;
            CalculatePackage();
        }
    }
    private bool FindRepeatPackage(int UID)
    {
        bool chk = false;
        if (dtSelectedPackage.Rows.Count > 0)
        {
            for (int i = 0; i <= dtSelectedPackage.Rows.Count - 1; i++)
            {
                if (UID == Convert.ToInt32(dtSelectedPackage.Rows[i]["UID"]))
                {
                    chk = true;
                }
            }
        }
        return chk;
    }
    protected void btPayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackageOrder1.aspx");
    }
}