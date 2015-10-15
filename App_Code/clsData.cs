using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;

public class clsData
{
    public enum JoinType
    {
        Inner = 0,
        Left = 1
    }

    /// <summary>
    /// ค้นหาข้อความใน List String
    /// </summary>
    /// <param name="lstSearch">List ที่ต้องการค้นหา</param>
    /// <param name="strWord">คำที่ต้องการค้นหา</param>
    /// <returns>True=พบ , False=ไม่พบ</returns>
    /// <remarks>2013-08-16</remarks>
    public bool Search(List<string> lstSearch, string strWord)
    {
        bool rtnBool = false;
        List<string> lstResult = new List<string>();

        #region LINQ
        lstResult = (from word in lstSearch
                     where word == strWord
                     select word).ToList();
        if (lstResult.Count > 0) rtnBool = true;
        #endregion

        return rtnBool;
    }

    /// <summary>
    /// ค้นหาข้อความใน DataTable
    /// </summary>
    /// <param name="dtSearch">DataTable ที่ต้องการค้นหา</param>
    /// <param name="strWord">คำที่ต้องการค้นหา</param>
    /// <param name="strField">Field ที่ต้องการค้นหา</param>
    /// <returns>True=พบ , False=ไม่พบ</returns>
    /// <remarks>2013-08-16</remarks>
    public bool Search(DataTable dtSearch, string strWord, string strField)
    {
        bool rtnBool = false;

        #region LINQ
        var varResult = (from word in dtSearch.AsEnumerable()
                         where word[strField].ToString() == strWord
                         select word);
        if (varResult != null && varResult.Count() > 0)
        {
            DataTable dtResult = new DataTable();
            dtResult = varResult.CopyToDataTable();
            rtnBool = true;
        }
        #endregion

        return rtnBool;
    }

    /// <summary>
    /// Joins 2 DataTables แบบเลือกวิธี Join ได้
    /// <para>Returns an appropriate DataTable with zero rows if the colToJoinOn does not exist in both tables.</para>
    /// </summary>
    /// <param name="dtblLeft"></param>
    /// <param name="dtblRight"></param>
    /// <param name="colToJoinOn"></param>
    /// <param name="joinType">clsData.JoinType.Left หรือ clsData.JoinType.Inner</param>
    /// <returns></returns>
    /// <example>
    /// dt = clsData.JoinTwoDataTable(dtHoldList, dtDepartment, "dept_id", clsData.JoinType.Left);
    /// </example>
    /// <remarks>
    /// <para>http://stackoverflow.com/questions/2379747/create-combined-datatable-from-two-datatables-joined-with-linq-c-sharp?rq=1</para>
    /// <para>http://msdn.microsoft.com/en-us/library/vstudio/bb397895.aspx</para>
    /// <para>http://www.codinghorror.com/blog/2007/10/a-visual-explanation-of-sql-joins.html</para>
    /// <para>http://stackoverflow.com/questions/406294/left-join-and-left-outer-join-in-sql-server</para>
    /// </remarks>
    public DataTable JoinTwoDataTable(DataTable dtblLeft, DataTable dtblRight, string colToJoinOn, JoinType joinType)
    {
        //Change column name to a temp name so the LINQ for getting row data will work properly.
        string strTempColName = colToJoinOn + "_2";
        if (dtblRight.Columns.Contains(colToJoinOn))
            dtblRight.Columns[colToJoinOn].ColumnName = strTempColName;

        //Get columns from dtblA
        DataTable dtblResult = dtblLeft.Clone();

        //Get columns from dtblB
        var dt2Columns = dtblRight.Columns.OfType<DataColumn>().Select(dc => new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));

        //Get columns from dtblB that are not in dtblA
        var dt2FinalColumns = from dc in dt2Columns.AsEnumerable()
                              where !dtblResult.Columns.Contains(dc.ColumnName)
                              select dc;

        //Add the rest of the columns to dtblResult
        dtblResult.Columns.AddRange(dt2FinalColumns.ToArray());

        //No reason to continue if the colToJoinOn does not exist in both DataTables.
        if (!dtblLeft.Columns.Contains(colToJoinOn) || (!dtblRight.Columns.Contains(colToJoinOn) && !dtblRight.Columns.Contains(strTempColName)))
        {
            if (!dtblResult.Columns.Contains(colToJoinOn))
                dtblResult.Columns.Add(colToJoinOn);
            return dtblResult;
        }

        switch (joinType)
        {

            default:
            case JoinType.Inner:
                #region Inner
                //get row data
                //To use the DataTable.AsEnumerable() extension method you need to add a reference to the System.Data.DataSetExtension assembly in your project. 
                var rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                       join rowRight in dtblRight.AsEnumerable() on rowLeft[colToJoinOn] equals rowRight[strTempColName]
                                       select rowLeft.ItemArray.Concat(rowRight.ItemArray).ToArray();


                //Add row data to dtblResult
                foreach (object[] values in rowDataLeftInner)
                    dtblResult.Rows.Add(values);

                #endregion
                break;
            case JoinType.Left:
                #region Left
                var rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                       join rowRight in dtblRight.AsEnumerable() on rowLeft[colToJoinOn] equals rowRight[strTempColName] into gj
                                       from subRight in gj.DefaultIfEmpty()
                                       select rowLeft.ItemArray.Concat((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();


                //Add row data to dtblResult
                foreach (object[] values in rowDataLeftOuter)
                    dtblResult.Rows.Add(values);

                #endregion
                break;
        }

        //Change column name back to original
        dtblRight.Columns[strTempColName].ColumnName = colToJoinOn;

        //Remove extra column from result
        dtblResult.Columns.Remove(strTempColName);

        return dtblResult;
    }
	
	public void ExportToExcel(DataTable dt,string Title,Boolean EnableAlternateRowColor)
    {
        #region Build FileHeader
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Export.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        #endregion

        #region Build Table
        HttpContext.Current.Response.Write("<table border='1' bgColor='#fff' " +
          "borderColor='#DADADA' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10pt; font-family:tahoma; background:#fff;'>");
        #region Build Title
        HttpContext.Current.Response.Write("<tr>");
        HttpContext.Current.Response.Write("<td colspan='" + dt.Columns.Count.ToString() + "' ");
        HttpContext.Current.Response.Write("style='text-align:center;background-color:#00B1F2;color:#fff;font-weight:bold;font-size:12pt;'");
        HttpContext.Current.Response.Write(">");
        if (string.IsNullOrEmpty(Title))
        {
            HttpContext.Current.Response.Write("Export");
        }
        else
        {
            HttpContext.Current.Response.Write(Title);
        }
        HttpContext.Current.Response.Write("</td>");
        HttpContext.Current.Response.Write("</tr>");
        #endregion
        #region Build ContentHeader
        HttpContext.Current.Response.Write("<tr>");
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            HttpContext.Current.Response.Write("<td style='background-color:#56C4FD;text-align:center;color:#fff;font-weight:bold;padding:5px;'>");
            HttpContext.Current.Response.Write(dt.Columns[j].ColumnName.ToString());
            HttpContext.Current.Response.Write("</td>");
        }
        HttpContext.Current.Response.Write("</tr>");
        #endregion
        #region Build Content
        int loopCount = 0;
        foreach (DataRow row in dt.Rows)
        {
            loopCount += 1;
            HttpContext.Current.Response.Write("<tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                #region Build Row Style
                string alternateRowColor = "";
                if (EnableAlternateRowColor)
                {
                    if (loopCount % 2 == 0)
                    {
                        alternateRowColor = "background-color:#FFF;";
                    }
                    else
                    {
                        alternateRowColor = "background-color:#FAFAFA;";
                    }
                }
                else
                {
                    alternateRowColor = "background-color:#FFF;";
                }
                #endregion
                HttpContext.Current.Response.Write("<td style='text-align:center;"+alternateRowColor+"'>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
        }
        #endregion
        HttpContext.Current.Response.Write("</table>");
        #endregion
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
	
	public void ExportToExcel(GridView gv, string Title, Boolean EnableAlternateRowColor)
    {
        DataTable dt = new DataTable();
        dt = GridViewToDataTable(gv);

        if (dt != null && dt.Rows.Count > 0)
        {
            ExportToExcel(dt, Title, EnableAlternateRowColor);
        }
    }
	
    public DataTable GridViewToDataTable(GridView gv)
    {
        DataTable dt = new DataTable();

        if (gv.HeaderRow != null)
        {
            #region Build Header
            string headerBefore="";
            for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
            {
                //ตรวจสอบไม่ให้ชื่อ Header ซ้ำกัน
                if (gv.HeaderRow.Cells[i].Text == headerBefore)
                {
                    dt.Columns.Add(gv.HeaderRow.Cells[i].Text+" "+i.ToString());
                }
                else
                {
                    dt.Columns.Add(gv.HeaderRow.Cells[i].Text);
                }
                headerBefore = gv.HeaderRow.Cells[i].Text;
            }
            #endregion
            #region Build Row
            foreach (GridViewRow gvr in gv.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();
                #region Build Column
                for (int i = 0; i < gvr.Cells.Count; i++)
                {
                    dr[i] = gvr.Cells[i].Text.Trim();
                }
                #endregion
                dt.Rows.Add(dr);
            }
            #endregion
        }

        return dt;
    }
	
    /// <summary>
    /// แปลงไฟล์ CSV ที่ต้องการเป็น DataTable
    /// </summary>
    /// <param name="FilePath">Path ไฟล์ CSV พร้อมชื่อไฟล์</param>
    /// <returns>DataTable ที่แปลงได้</returns>
    /// <example>
    /// clsData.CSVToDataTable("DB\FileList.csv");
    /// </example>
    public DataTable CSVToDataTable(string FilePath)
    {
        DataTable dt = new DataTable();
        FileInfo fi = new FileInfo(@FilePath);

        #region File Exist
        if (fi.Exists)
        {
            #region CSV Extension Check
            if (fi.Extension.ToLower() == ".csv")
            {
                try
                {
                    string line;

                    using (StreamReader reader = new StreamReader(Path.GetFullPath(FilePath), Encoding.GetEncoding(874)))
                    {
                        line = reader.ReadLine();
                        #region Loop Line
                        do
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                string[] arrLine = line.Trim().Split(',');
                                DataRow dr;

                                #region Build Column
                                if (dt.Columns.Count == 0)
                                {
                                    for (int i = 0; i < arrLine.Length; i++)
                                    {
                                        dt.Columns.Add(i.ToString());
                                    }
                                }
                                else if (dt.Columns.Count < arrLine.Length)
                                {
                                    for (int i = dt.Columns.Count; i < arrLine.Length; i++)
                                    {
                                        dt.Columns.Add(i.ToString());
                                    }
                                }
                                #endregion
                                #region Build Row
                                dr = dt.NewRow();
                                for (int i = 0; i < arrLine.Length; i++)
                                {
                                    dr[i] = arrLine[i].Trim();
                                }
                                dt.Rows.Add(dr);
                                #endregion
                            }
                            line = reader.ReadLine();
                        } while (!string.IsNullOrEmpty(line));
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    //Error : Read File
                }
            }
            #endregion
        }
        #endregion

        return dt;
    }

    /// <summary>
    /// เขียนไฟล์ CSV
    /// </summary>
    /// <param name="PathFile">Path ไฟล์ CSV พร้อมชื่อไฟล์</param>
    /// <param name="Values">Array ค่าที่ต้องการใส่</param>
    /// <param name="EnableAddDateTime">true=บันทึกเวลาที่ฟิลด์แรก , false=ไม่บันทึก</param>
    /// <returns>true=งานสำเร็จ , false=ไม่สำเร็จ</returns>
    /// <example>
    /// clsData.CSVWriter("Path\Log.csv", new string[] { "ทดสอบ 1", "Test 1", "Test 2" });
    /// </example>
    public bool CSVWriter(string PathFile,string[] Values,bool EnableAddDateTime=true,bool EnableDeleteOldFile=false)
    {
        FileInfo fi = new FileInfo(PathFile);
        bool rtnValue = false;

        #region Delete Old File
        if (EnableDeleteOldFile)
        {
            try
            {
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Write File
        try
        {
            StreamWriter sw = new StreamWriter(PathFile, true, System.Text.Encoding.UTF8);
            StringBuilder strValue = new StringBuilder();

            #region Build Data
            if (EnableAddDateTime)
            {
                strValue.Append(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                strValue.Append(",");
            }

            #region Build Data by Array
            for (int i = 0; i < Values.Length; i++)
            {
                strValue.Append(Values[i].Trim().Replace(",", "#"));

                if (i < Values.Length - 1)
                {
                    strValue.Append(",");
                }
            }
            #endregion
            #endregion

            sw.WriteLine(strValue.ToString());
            strValue.Length = 0; strValue.Capacity = 0;
            sw.Close();
            rtnValue = true;
        }
        catch (Exception ex)
        {

        }
        #endregion

        return rtnValue;
    }

    /// <summary>
    /// แทนที่คำด้วยชุดของ Array 2 มิติ
    /// </summary>
    /// <param name="Text">ข้อความที่ใช้ตั้งต้นในการแทนที่</param>
    /// <param name="Parameter">{ข้อความเก่า,ข้อความใหม่}</param>
    /// <returns>ข้อความที่ถูกแทนที่แล้ว</returns>
    public string Replacer(string Text,string[,] Parameter)
    {
        string rtnValue = Text;

        if (Parameter != null && Parameter.Length > 0)
        {
            for (int i = 0; i < Parameter.Length / Parameter.Rank; i++)
            {
                rtnValue = rtnValue.Replace(Parameter[i, 0], Parameter[i, 1]);
            }
        }
        
        return rtnValue;
    }
}
