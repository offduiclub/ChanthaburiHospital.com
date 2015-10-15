using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for clsCustomer
/// </summary>
public class clsCustomer
{
    DBClassDataContext db = new DBClassDataContext();
	public clsCustomer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable SearchCustomerByUsername(string Username)
    {
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(Username))
        {
            dt.Columns.Add("Forename", typeof(string));
            dt.Columns.Add("Surname", typeof(string));
            dt.Columns.Add("SEX", typeof(string));
            dt.Columns.Add("IDCard", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Tel", typeof(string));
            dt.Columns.Add("Detail", typeof(string));
            dt.Columns.Add("District", typeof(string));
            dt.Columns.Add("Prefecture", typeof(string));
            dt.Columns.Add("Province", typeof(string));
            dt.Columns.Add("Zipcode", typeof(string));
            try
            {
                var tbUser = from u in db.Users
                             where u.Username == Username
                             select u;
                foreach (var item in tbUser)
                {
                    DataRow dr = dt.NewRow();
                    dr["Forename"] = item.FName;
                    dr["Surname"] = item.LName;
                    dr["SEX"] = item.Gender;
                    dr["IDCard"] = "";
                    dr["Address"] = item.Address;
                    dr["Email"] = item.Email;
                    dr["Tel"] = item.Mobile;
                    dr["Detail"] = "";
                    dr["District"] = item.AddressDistrict;
                    dr["Prefecture"] = item.AddressPrefecture;
                    dr["Province"] = item.AddressProvince;
                    dr["Zipcode"] = item.AddressPostal;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        return dt;
    }
}