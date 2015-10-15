using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PackageOrder1 : System.Web.UI.Page
{
    dsPackage.dtSelectedPackageDataTable dtSelectedPackage = new dsPackage.dtSelectedPackageDataTable();
    DBClassDataContext db = new DBClassDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        BindPackageOrder();
        BindCustomer();
        lblOrderNo.Text = BindOrderNo();
    }
    private void BindPackageOrder()
    {
        if ((dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"] != null)
        {
            dtSelectedPackage = (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"];
            gvPackage.DataSource = dtSelectedPackage;
            gvPackage.DataBind();
            CalculatePackage();
        }
    }
    private void BindCustomer()
    {
        if (Session["CustomerUID"] != null)
        {
            try
            {
                var tbcus = from c in db.Customers
                            where c.UID == Convert.ToInt32(Session["CustomerUID"])
                            select c;
                foreach (Customer cus in tbcus)
                {
                    lblName.Text = cus.Name;
                    lblSurname.Text = cus.Surname;
                    lblAddress.Text = cus.Address;
                    lblDistrict.Text = cus.District;
                    lblPrefecture.Text = cus.Prefecture;
                    lblProvince.Text = cus.Province;
                    lblZipcode.Text = cus.Zipcode;
                    lblEmail.Text = cus.Email;
                }
                lblOrderDate.Text = DateTime.Today.ToString();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('" + ex.ToString() + "')", true);
            }
        }
    }
    private string BindOrderNo()
    {
        string OrderNo = string.Empty;
        string StrNo = string.Empty;
        int RunningNo = 0;
        string strMonth = string.Empty;
        if (DateTime.Today.Month.ToString().Length == 1)
        {
            strMonth = "0" + DateTime.Today.Month.ToString();
        }
        else
        {
            strMonth = DateTime.Today.Month.ToString();
        }

        var tbOrder = (from o in db.OrderPackages
                       where o.StatusFlage == "A"
                       orderby o.UID descending
                       select o).Take(1);

        if (tbOrder.Any())
        {
            foreach (OrderPackage op in tbOrder)
            {
                OrderNo = op.OrderNo;
                break;
            }
            RunningNo = Convert.ToInt32(OrderNo.Substring(6, 3));
            RunningNo++;
            if (RunningNo.ToString().Length == 1)
            {
                StrNo = "00" + RunningNo.ToString();
            }
            else if (RunningNo.ToString().Length == 2)
            {
                StrNo = "0" + RunningNo.ToString();
            }
            else if (RunningNo.ToString().Length == 3)
            {
                StrNo = RunningNo.ToString();
            }
            OrderNo = DateTime.Today.Year + "" + strMonth + "" + StrNo;
        }
        else
        {
            OrderNo = DateTime.Today.Year + "" + strMonth + "001";
        }
        return OrderNo;
    }
    private void CalculatePackage()
    {
        if (dtSelectedPackage.Rows.Count > 0)
        {
            int rowIndex = gvPackage.Rows.Count;
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["CustomerUID"] != null && (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"] != null)
        {
            OrderPackageDetail opd;
            OrderPackage op = new OrderPackage();
            clsPackage pk = new clsPackage();
            DataTable dt = new DataTable();
            DataTable dtOrderPackage = new DataTable();
            int OrderPackageUID = 0;

            //Insert Order
            op.CustomerUID = Convert.ToInt32(Session["CustomerUID"]);
            op.OrderNo = lblOrderNo.Text.Trim();
            op.OrderDate = Convert.ToDateTime(lblOrderDate.Text.Trim());
            op.Total = Convert.ToDecimal(txtTotal.Text.Trim());
            op.VAT = Convert.ToDecimal(txtVat.Text.Trim());
            op.GrandTotal = Convert.ToDecimal(txtGrandTotal.Text.Trim());
            op.StatusFlage = "A";
            op.CUser = 99;
            op.CWhen = DateTime.Now;
            op.MUser = 99;
            op.MWhen = DateTime.Now;
            db.OrderPackages.InsertOnSubmit(op);
            db.SubmitChanges();

            //Insert Order Detail
            dtOrderPackage = pk.SearchPackageOrdeByOrderNo(lblOrderNo.Text.Trim());
            if(dtOrderPackage.Rows.Count > 0 && dtOrderPackage !=null)
            {
                OrderPackageUID = Convert.ToInt32(dtOrderPackage.Rows[0]["UID"]);

                dt = (dsPackage.dtSelectedPackageDataTable)Session["selectedPackage"];
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    opd = new OrderPackageDetail();
                    opd.OrderPackageUID = OrderPackageUID;
                    opd.PackageUID = Convert.ToInt32(dt.Rows[i]["UID"]);
                    opd.UnitPrice = Convert.ToDecimal(dt.Rows[i]["UnitPrice"]);
                    opd.Qty = Convert.ToInt32(dt.Rows[i]["Qty"]);
                    opd.Total = opd.UnitPrice * opd.Qty;
                    opd.StatusFlag = "A";
                    opd.CUser = 99;
                    opd.CWhen = DateTime.Now;
                    opd.MUser = 99;
                    opd.MWhen = DateTime.Now;
                    db.OrderPackageDetails.InsertOnSubmit(opd);
                    db.SubmitChanges();
                    opd = null;
                }
                
            }
            Session["OrderNo"] = lblOrderNo.Text.Trim();
            Response.Redirect("PackageOrder3.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Information", "alert('Session หมดอายุ  กรุณาเลือกแพ็คเกจที่ต้องการใหม่อีกครั้งครับ')", true);
        }
    }
}