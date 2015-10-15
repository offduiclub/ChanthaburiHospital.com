using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ucCalendarSpecialDays : System.Web.UI.UserControl
{
    #region Example
    /*
    <uc1:ucCalendarSpecialDays ID="ucCalendarSpecialDays1" runat="server" Width="300px" Height="200px"/>
    
    ucCalendarSpecialDays1.SpecialDays.Add(
        new SpecialDay() { Date = DateTime.Now, Message = "ทดสอบ", RepeatMonth = false, RepeatYear = false }
    );
    ucCalendarSpecialDays1.SpecialDays.Add(
        new SpecialDay() { Date = new DateTime(2014,11,30), Message = "ทดสอบ 2", RepeatMonth = false, RepeatYear = false }
    );
    ucCalendarSpecialDays1.SpecialDays.Add(
        new SpecialDay() { Date = new DateTime(2014, 11, 10), Message = "ทดสอบ 3", RepeatMonth = false, RepeatYear = false }
    );
    */
    #endregion
    #region Property
    private string _width="400px";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }
    private string _height="300px";
    public string Height
    {
        get { return _height; }
        set { _height = value; }
    }
    private List<SpecialDay> _specialDays=new List<SpecialDay>();
    public List<SpecialDay> SpecialDays
    {
        get { return _specialDays; }
        set { _specialDays = value; }
    }
    public string strSpecialDays = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SpecialDaysBuilder();
        }
    }

    private void SpecialDaysBuilder()
    {
        #region Variable
        var result = new StringBuilder();
        #endregion
        #region Procedure
        if (_specialDays!=null && _specialDays.Count > 0)
        {
            for (int i = 0; i < _specialDays.Count; i++)
            {
                if (i > 0) result.Append(",");
                result.Append("{date: new Date(" +
                    _specialDays[i].Date.Year.ToString() + ", " +
                    (_specialDays[i].Date.Month-1).ToString() + ", " +
                    (_specialDays[i].Date.Day).ToString() + "),"+
                    "data: { message: '" + _specialDays[i].Message + "' },"+
                    "repeatMonth: " + _specialDays[i].RepeatMonth.ToString().ToLower() + ","+
                    "repeatYear: " + _specialDays[i].RepeatYear.ToString().ToLower() + "}");
            }
        }
        if(result.Length>0)
        {
            strSpecialDays = result.ToString();
        }
        #endregion
    }
}

public class SpecialDay
{
    private DateTime _date;
    public DateTime Date
    {
        get { return _date; }
        set { _date = value; }
    }
    private string _message="";
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }
    private bool _repeatMonth=false;
    public bool RepeatMonth
    {
        get { return _repeatMonth; }
        set { _repeatMonth = value; }
    }
    private bool _repeatYear=false;
    public bool RepeatYear
    {
        get { return _repeatYear; }
        set { _repeatYear = value; }
    }
}