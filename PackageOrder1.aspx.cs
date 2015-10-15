using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class PackageOrder1 : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    clsSecurity security = new clsSecurity();
    dsPackage.dtSelectedPackageDataTable dtSelectedPackage = new dsPackage.dtSelectedPackageDataTable();
    clsLanguage lang = new clsLanguage();
    private string IDCard = string.Empty;
    string strLang = string.Empty;
    public PackageOrder1()
    {
        strLang = lang.LanguageCurrent;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (security.LoginChecker() == true)
        {
            clsCustomer customer = new clsCustomer();
            DataTable dt = new DataTable();
            dt = customer.SearchCustomerByUsername(security.LoginUsername);
            if (dt.Rows.Count > 0 && dt != null)
            {
                txtName.Text = dt.Rows[0]["Forename"].ToString();
                txtSurname.Text = dt.Rows[0]["Surname"].ToString();
                if (dt.Rows[0]["SEX"].ToString() == "M")
                {
                    rdbMale.Checked = true;
                }
                else
                {
                    rdbFemale.Checked = true;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["IDCard"].ToString()))
                {
                    txtIDCardNo.Text = dt.Rows[0]["IDCard"].ToString();
                }
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtDistrict.Text = dt.Rows[0]["District"].ToString();
                txtPrefecture.Text = dt.Rows[0]["Prefecture"].ToString();
                txtProvince.Text = dt.Rows[0]["Province"].ToString();
                txtZipcode.Text = dt.Rows[0]["Zipcode"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtTel.Text = dt.Rows[0]["Tel"].ToString();
                txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            }
        }
        BindSelectedPackage();
        BindContent();
    }
    private void BindContent()
    {
        //Check Title
        if (strLang == "th-TH")
        {
            //lblTitle.Text = "แพ็คเกจ";
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
            //lblTitle.Text = "Package";
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
            //lblTitle.Text = "แพ็คเกจ";
            lblSiteMap.Text = "ข่าวสารโรงพยาบาล > แพ็คเกจ";
            lblMenuPackage.Text = "แพ็คเกจ";
            lblMenuPromotion.Text = "โปรโมชั่น";

            lblTitleSelectedPackage.Text = "แพ็คเกจที่เลือก";
            lblTotal.Text = "ราคารวม";
            lblVat.Text = "ภาษี";
            lblGrandTotal.Text = "รวมทั้งสิ้น";
        }
    }
    protected void btNext_Click(object sender, EventArgs e)
    {
        IDCard = txtIDCardNo.Text;
        CheckRepeatCustomer();
    }
    private void InsertCustomer()
    {
        try
        {
            if (VerifyIDCard(IDCard))
            {
                Customer cus = new Customer();
                cus.Name = txtName.Text.Trim();
                cus.Surname = txtSurname.Text.Trim();
                cus.SEX = rdbMale.Checked == true ? "M" : "F";
                cus.IDCard = txtIDCardNo.Text.Trim();
                cus.Address = txtAddress.Text.Trim();
                cus.District = txtDistrict.Text.Trim();
                cus.Prefecture = txtPrefecture.Text.Trim();
                cus.Province = txtProvince.Text.Trim();
                cus.Zipcode = txtZipcode.Text.Trim();
                cus.Email = txtEmail.Text.Trim();
                cus.Tel = txtTel.Text.Trim();
                cus.Detail = txtDetail.Text.Trim();
                cus.CUser = 1;
                cus.CWhen = DateTime.Now;
                cus.MUser = 1;
                cus.MWhen = DateTime.Now;
                cus.StatusFlag = "A";

                db.Customers.InsertOnSubmit(cus);
                db.SubmitChanges();
                Session["CustomerUID"] = cus.UID;
                Response.Redirect("PackageOrder2.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('กรุณากรอกหมายเลขบัตรประชาชนให้ถูกต้องด้วยครับ')", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
    }
    private void CheckRepeatCustomer()
    {
        try
        {
            var tbCus = from c in db.Customers
                        where (c.Name == txtName.Text.Trim() && c.Surname == txtSurname.Text.Trim()) || (c.IDCard == txtIDCardNo.Text.Trim())
                        select c;
            //ถ้า Query แล้วไม่มีข้อมูล Customer ให้ทำการ insert customer
            if (!tbCus.Any())
            {
                InsertCustomer();
            }
            //ถ้า Query แล้วมีข้อมูล Customer
            else
            {
                foreach (Customer cus in tbCus)
                {
                    Session["CustomerUID"] = cus.UID;
                    Response.Redirect("PackageOrder2.aspx");
                }
            }
            
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
        }
    }
    public Boolean VerifyIDCard(String PID)
    {
        if (this.chkNumber(PID) == false) return false;
        //ตรวจสอบว่ามี 13 ตัวอักษรหรือไม่
        if (PID.Trim().Length != 13) return false;
        int sumValue = 0;
        for (int i = 0; i < PID.Length - 1; i++)
            sumValue += int.Parse(PID[i].ToString()) * (13 - i);
        int v = 11 - (sumValue % 11);
        return PID[12].ToString() == v.ToString();
    }
    private Boolean chkNumber(string val)
    {
        Boolean err = true;
        RegexStringValidator reg = new RegexStringValidator("^[0-9]+$");
        try
        {
            reg.Validate(val);
        }
        catch
        {
            err = false;
        }
        return err;
    }
    private void BindSelectedPackage()
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
}