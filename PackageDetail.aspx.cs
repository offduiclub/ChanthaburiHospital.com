using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PackageDetail : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity Security = new clsSecurity();
    clsLanguage lang = new clsLanguage();
    string strLang = string.Empty;
    clsPackage package = new clsPackage();
    dsPackage.dtSelectedPackageDataTable dtSelectedPackage = new dsPackage.dtSelectedPackageDataTable();
    dsPackage.dtSelectedPackageRow dtSelectedPackageRow;

    public PackageDetail()
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
                //btEdit.Visible = true;
                pnAdmin.Visible = true;
                btDelete.Visible = true;
            }
        }
        else
        {
            BindSelectedPackage();
        }
    }
    private void BindPackage()
    {
        clsDefault clsDefault = new clsDefault();
        //int UID = Convert.ToInt32(clsDefault.URLRouting("id")); 
        //if (!string.IsNullOrEmpty(UID.ToString()))
        //{
        //    var tbPackage = from p in db.Packages
        //                  where p.UID == UID
        //                  select p;
        //    //foreach (Package p in tbPackage)
        //    //{
        //    //    lblUID.Text = p.UID.ToString();
        //    //    lblSubject.Text = p.PackageName;
        //    //    lblDetail.Text = p.Detail;
        //    //    PicFull.ImageUrl = p.PicFull;
        //    //    lblUnitPrice.Text = p.UnitPrice.ToString();
        //    //    lblSiteMap.Text = p.PackageName;
        //    //    Page.MetaKeywords = p.MetaKeywords;
        //    //    Page.MetaDescription = p.MetaDescription;

        //    //}
        //}
        //else
        //{
        //    Response.Redirect("PackageView.aspx");
        //}
    }
    private void BindContent()
    {
        if (strLang == "th-TH")
        {
            lblTitle.Text = "แพ็คเกจและโปรโมชั่น";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > แพ็คเกจและโปรโมชั่ > " + lblSubject.Text;
            lblPrice.Text = "ราคาแพ็คเกจ : ";
            lblCurrency.Text = " บาท";
            btBuy.Text = "ซื้อแพ็คเกจนี้";
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
            lblSiteMap.Text = "Hospital News > Package > " + lblSubject.Text;
            lblPrice.Text = "Package Price : ";
            lblCurrency.Text = " Bath";
            btBuy.Text = "Buy";
            lblMenuPackage.Text = "Package";
            lblMenuPromotion.Text = "Promotion";

            lblTitleSelectedPackage.Text = "Selected Package";
            lblTotal.Text = "Total";
            lblVat.Text = "Vat";
            lblGrandTotal.Text = "Grand Total";
        }
        else
        {
            lblTitle.Text = "แพ็คเกจและโปรโมชั่";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > แพ็คเกจและโปรโมชั่ > " + lblSubject.Text;
            lblPrice.Text = "ราคาแพ็คเกจ : ";
            lblCurrency.Text = " บาท";
            btBuy.Text = "ซื้อแพ็คเกจนี้";
            lblMenuPackage.Text = "แพ็คเกจ";
            lblMenuPromotion.Text = "โปรโมชั่น";

            lblTitleSelectedPackage.Text = "แพ็คเกจที่เลือก";
            lblTotal.Text = "ราคารวม";
            lblVat.Text = "ภาษี";
            lblGrandTotal.Text = "รวมทั้งสิ้น";
        }
    }
    protected void btEdit_Click(object sender, EventArgs e)
    {
        ucColorBox1.IFrame("PackageForm.aspx?UID=" + lblUID.Text.Trim() + "", "90%", "90%", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        var tbPackage = from p in db.Packages
                      where p.UID == Convert.ToInt32(lblUID.Text)
                      select p;
        //foreach (Package p in tbPackage)
        //{
        //    db.Packages.DeleteOnSubmit(p);
        //}
        try
        {
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
        BindPackage();
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
            txtTotal.Text = total.ToString("#0.00");

            decimal vat = Convert.ToDecimal(total * 7 / 100);
            txtVat.Text = vat.ToString("#0.00");
            txtGrandTotal.Text = Convert.ToDecimal(total + vat).ToString("#0.00");
        }
    }
    protected void btBuy_Click(object sender, EventArgs e)
    {
        int UID = Convert.ToInt32(lblUID.Text);
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
    private DataTable findPackagByUID(int UID)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UID", typeof(int));
        dt.Columns.Add("PackageCode", typeof(string));
        dt.Columns.Add("PackageName", typeof(string));
        dt.Columns.Add("UnitPrice", typeof(int));

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
    protected void gvSelectedPackage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "RemovePackage")
        {
            dtSelectedPackage = (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"];
            dtSelectedPackage.Rows[index - 1].Delete();
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
}