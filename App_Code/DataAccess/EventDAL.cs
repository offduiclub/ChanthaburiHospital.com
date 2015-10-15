using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for EventDAL
/// </summary>
public class EventDAL
{
    ExcData exc;
	public EventDAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable SelectEventAll()
    {
        ExcData exc = new ExcData();
        return exc.data_Table(EventSQL.SelectEventAll);
    }

    public bool InsertEvent(EventModel eventModel)
    {
        exc = new ExcData();
        string strSQLInsert = EventSQL.InsertEvent;
        strSQLInsert = strSQLInsert.Replace("@Subject", "'" + eventModel.Subject + "'");
        strSQLInsert = strSQLInsert.Replace("@Detail", "'" + eventModel.Detail + "'");
        strSQLInsert = strSQLInsert.Replace("@PicThumbnail", "'" + eventModel.PicThumbnail + "'");
        strSQLInsert = strSQLInsert.Replace("@PicFull", "'" + eventModel.PicFull + "'");
        strSQLInsert = strSQLInsert.Replace("@DepartmentUID", "'" + eventModel.DepartmentUID + "'");
        strSQLInsert = strSQLInsert.Replace("@ActiveDateFrom", "'" + eventModel.ActiveDateFrom + "'");
        strSQLInsert = strSQLInsert.Replace("@ActiveDateTo", "'" + eventModel.ActiveDateTo + "'");
        strSQLInsert = strSQLInsert.Replace("@Remark", "'" + eventModel.Remark + "'");
        strSQLInsert = strSQLInsert.Replace("@CUser", "'" + eventModel.CUser + "'");
        strSQLInsert = strSQLInsert.Replace("@MUser", "'" + eventModel.MUser + "'");
        strSQLInsert = strSQLInsert.Replace("@StatusFlag", "'" + eventModel.StatusFlag + "'");
        strSQLInsert = strSQLInsert.Replace("@LanguageUID", "'" + eventModel.LanguageUID + "'");
        return exc.ExecData(strSQLInsert);
    }
    public bool DeleteEvent(int UID)
    {
        exc = new ExcData();
        string strSQLDelete = EventSQL.DeleteEvent;
        strSQLDelete = strSQLDelete.Replace("@Subject", "'" + UID + "'");
        return exc.ExecData(strSQLDelete);
    }
}