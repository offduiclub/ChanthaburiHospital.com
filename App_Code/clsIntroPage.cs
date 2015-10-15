using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for clsIntroPage
/// </summary>
public class clsIntroPage
{
    #region Global Variable
    clsSQL clsSQL = new clsSQL();
    #endregion
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

	public clsIntroPage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable IntroPageBuilder()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("Photo,Name ");
        strSQL.Append("FROM ");
        strSQL.Append("IntroPage ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("AND ((");
        strSQL.Append("ActiveIgnoreYear='0' ");
        strSQL.Append("AND (ActiveFrom IS NULL OR ActiveFrom <= GETDATE()) ");
        strSQL.Append("AND (ActiveTo IS NULL OR ActiveTo >= GETDATE())");
        strSQL.Append(") ");
        strSQL.Append("OR (");
        strSQL.Append("ActiveIgnoreYear='1' ");
        strSQL.Append("AND (ActiveFrom IS NULL OR CONVERT(DATETIME,CONVERT(VARCHAR,DATEPART(YEAR,GETDATE()))+'-'+CONVERT(VARCHAR,DATEPART(MONTH,ActiveFrom))+'-'+CONVERT(VARCHAR,DATEPART(DAY,ActiveFrom))) <= GETDATE()) ");
        strSQL.Append("AND (ActiveTo IS NULL OR CONVERT(DATETIME,CONVERT(VARCHAR,DATEPART(YEAR,GETDATE()))+'-'+CONVERT(VARCHAR,DATEPART(MONTH,ActiveTo))+'-'+CONVERT(VARCHAR,DATEPART(DAY,ActiveTo))) >= GETDATE())");
        strSQL.Append(")) ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort ASC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        #endregion

        return dt;
    }
}