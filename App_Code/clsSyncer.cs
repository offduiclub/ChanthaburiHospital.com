using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for clsSyncer
/// </summary>
public class clsSyncer
{
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

	public clsSyncer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool DoctorScheduleSyncer()
    {
        #region Variable
        clsSQL clsSQL = new clsSQL();
        FileInfo fi = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("/Upload/Doctor/SQLQuery.txt"));
        string sqlQuery = "";
        bool result = false;
        #endregion

        if (fi.Exists)
        {
            StreamReader StrWer;
            try
            {
                StrWer = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("/Upload/Doctor/SQLQuery.txt"));
                while (!(StrWer.EndOfStream))
                {
                    sqlQuery = sqlQuery + StrWer.ReadLine();
                }
                StrWer.Close();
            }
            catch (Exception) { }

            if (sqlQuery != "")
            {
                clsMail clsMail = new clsMail();
                string outMail = ""; string outSQLError = "";
                if (!clsSQL.Execute(sqlQuery, dbType, cs,out outSQLError))
                {
                    clsMail.Send("AutoSystem@glsict.com", "nithi.re@glsict.com",
                        "DoctorScheduleSyncer : Error Insert (OnWeb)",
                        outSQLError + "<hr/>"+sqlQuery, out outMail);
                    //lblDoctorScheduleSyncer.Text = "DoctorScheduleSyncer : เกิดข้อผิดพลาดขณะรันคำสั่ง<br/>"+sqlQuery;
                    fi.Delete();
                }
                else
                {
                    fi.Delete();
                    result = true;
                }
            }
        }
        else
        {
            result = true;
        }

        return result;
    }
}