using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class UserControl_ucGallery_ucGalleryCarousel : System.Web.UI.UserControl
{
    /* ################## Example ##################
    dt = clsSQL.Bind("SELECT Photo As slide,Name As name FROM GalleryTest", "MySQL", "cs");
    ucGalleryCarousel1.Source = dt;
    ucGalleryCarousel1.Path = "~/UserControl/ucGalleryCarousel/Upload/";
    ucGalleryCarousel1.AutoPlay = false;
    */

    private DataTable _source;
    public DataTable Source
    {
        get { return _source; }
        set { _source = value; }
    }

    private string _path;
    public string Path
    {
        get { return _path; }
        set { _path = value; }
    }

    private int _thumbItemWidth=72;
    public int ThumbItemWidth
    {
        get { return _thumbItemWidth; }
        set { _thumbItemWidth = value; }
    }

    private int _thumbNumber=3;
    public int ThumbNumber
    {
        get { return _thumbNumber; }
        set { _thumbNumber = value; }
    }

    private bool _autoPlay=true;
    public bool AutoPlay
    {
        get { return _autoPlay; }
        set { _autoPlay = value; }
    }


    public StringBuilder strSlide = new StringBuilder();
    public StringBuilder strThumb = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_source != null && _source.Rows.Count > 0)
            {
                BindGallery();
            }
        }
    }

    private void BindGallery()
    {
        int i;
        bool errorCheck = false;
        string fieldName = "name";
        string fieldSlide = "slide";
        string fieldThumb = "slide";

        if (!_source.Columns.Contains(fieldName))
        {
            lblDefault.Text += "<div>ไม่พบคอลัม "+fieldName+" ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        if (!_source.Columns.Contains(fieldSlide))
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + fieldSlide + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        if (!_source.Columns.Contains(fieldThumb))
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + fieldThumb + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }

        if (!errorCheck)
        {
            for (i = 0; i < _source.Rows.Count; i++)
            {
                strSlide.Append("<div class='slide'>");
                strSlide.Append("<a href=''></a>");
                strSlide.Append("<img alt='" + _source.Rows[i][fieldName].ToString() + "' title='" + _source.Rows[i][fieldName].ToString() + "' ");
                strSlide.Append("src='" + _path + _source.Rows[i][fieldSlide].ToString() + "'>");
                strSlide.Append("</div>");

                strThumb.Append("<div class='thumb'>");
                strThumb.Append("<a href='' class=''></a>");
                strThumb.Append("<img alt='" + _source.Rows[i][fieldName].ToString() + "' title='" + _source.Rows[i][fieldName].ToString() + "' ");
                strThumb.Append("src='" + _path + _source.Rows[i][fieldThumb].ToString() + "' width='" + _thumbItemWidth + "px'>");
                strThumb.Append("</div>");
            }
        }
    }
}