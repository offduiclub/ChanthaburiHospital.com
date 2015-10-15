using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for clsPackage
/// </summary>
public class clsPackage
{
    DBClassDataContext db = new DBClassDataContext();
	public clsPackage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private int _uid;
    public int UID
    {
        get { return _uid; }
        set { _uid = value; }
    }

    private string _packageCode;
    public string PackageCode
    {
        get { return _packageCode; }
        set { _packageCode = value; }
    }

    private string _packageName;
    public string PackageName
    {
        get { return _packageName; }
        set { _packageName = value; }
    }

    private double _unitPrice = 0;
    public double UnitPrice
    {
        get { return _unitPrice; }
        set { _unitPrice = value; }
    }

    private int _qty;
    public int Qty
    {
        get { return _qty; }
        set { _qty = value; }
    }

    public DataTable SearchPackageOrdeByOrderNo(string OrderNo)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UID", typeof(int));
        dt.Columns.Add("OrderNo", typeof(string));
        dt.Columns.Add("CustomerUID", typeof(string));
        dt.Columns.Add("Total", typeof(string));
        dt.Columns.Add("VAT", typeof(string));
        dt.Columns.Add("GrandTotal", typeof(string));

        var tbOp = from op in db.OrderPackages
                   where op.OrderNo == OrderNo
                   select op;
        foreach (OrderPackage o in tbOp)
        {
            DataRow dr = dt.NewRow();
            dr["UID"] = o.UID;
            dr["OrderNo"] = o.OrderNo;
            dr["CustomerUID"] = o.CustomerUID;
            dr["Total"] = o.Total;
            dr["VAT"] = o.VAT;
            dr["GrandTotal"] = o.GrandTotal;
            dt.Rows.Add(dr);
        }
        return dt;
    }
}