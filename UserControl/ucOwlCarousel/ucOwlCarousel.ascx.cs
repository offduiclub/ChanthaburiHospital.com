using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class UserControl_ucOwlCarousel_ucOwlCarousel : System.Web.UI.UserControl
{
    #region Example
    //กรณีเอา UserControl ใส่ใน Table ใน Table หลักต้องใส่ style="table-layout: fixed;width:100%;" ด้วย
    /*
    <div style="table-layout: fixed;width:100%;">
        <uc3:ucOwlCarousel ID="ucOwlCarousel1" runat="server" />
    </div>
    
    DataTable dt = new DataTable();
    dt = clsSQL.Bind("SELECT UID,PackageName Name,PicThumbnail Photo,'Package.aspx?id='+CONVERT(VARCHAR,UID) URL FROM Package;", dbType, cs);
    ucOwlCarousel1.Source = dt;
    ucOwlCarousel1.SourceFieldName = "Name";
    ucOwlCarousel1.SourceFieldPhoto = "Photo";
    ucOwlCarousel1.SourceFieldURL = "URL";
    ucOwlCarousel1.PhotoWidth = "100%";
    ucOwlCarousel1.PhotoHeight = "auto";
    ucOwlCarousel1.PhotoWidthOverflow = "320px";
    ucOwlCarousel1.PhotoHeightOverflow = "140px";
    ucOwlCarousel1.PathPhoto = "";
    ucOwlCarousel1.AutoPlay = true;
    ucOwlCarousel1.AutoPlaySecond = 4;
    ucOwlCarousel1.ItemsMax = 3;
    ucOwlCarousel1.StopOnHover = true;
    ucOwlCarousel1.Enable = true;
    ucOwlCarousel1.Visible = true;
    */
    #endregion
    #region Property
    private DataTable _source;
    public DataTable Source
    {
        get { return _source; }
        set { _source = value; }
    }
    private string _sourceFieldName = "Name";
    public string SourceFieldName
    {
        get { return _sourceFieldName; }
        set { _sourceFieldName = value; }
    }
    private string _sourceFieldPhoto = "Photo";
    public string SourceFieldPhoto
    {
        get { return _sourceFieldPhoto; }
        set { _sourceFieldPhoto = value; }
    }
    private string _sourceFieldURL="URL";
    public string SourceFieldURL
    {
        get { return _sourceFieldURL; }
        set { _sourceFieldURL = value; }
    }
    private string _pathPhoto = "";
    public string PathPhoto
    {
        get { return _pathPhoto; }
        set { _pathPhoto = value; }
    }
    private string _photoWidth = "100%";
    public string PhotoWidth
    {
        get { return _photoWidth; }
        set { _photoWidth = value; }
    }
    private string _photoHeight = "auto";
    public string PhotoHeight
    {
        get { return _photoHeight; }
        set { _photoHeight = value; }
    }
    private string _photoWidthOverflow="100%";
    public string PhotoWidthOverflow
    {
        get { return _photoWidthOverflow; }
        set { _photoWidthOverflow = value; }
    }
    private string _photoHeightOverflow="auto";
    public string PhotoHeightOverflow
    {
        get { return _photoHeightOverflow; }
        set { _photoHeightOverflow = value; }
    }

    private bool _autoPlay = true;
    public bool AutoPlay
    {
        get { return _autoPlay; }
        set { _autoPlay = value; }
    }
    private int _autoPlaySecond=5;
    public int AutoPlaySecond
    {
        get { return _autoPlaySecond; }
        set { _autoPlaySecond = value; }
    }
    private int _itemsMax=3;
    public int ItemsMax
    {
        get { return _itemsMax; }
        set { _itemsMax = value; }
    }
    private bool _stopOnHover=true;

    public bool StopOnHover
    {
        get { return _stopOnHover; }
        set { _stopOnHover = value; }
    }

    private bool _enable = true;
    public bool Enable
    {
        get { return _enable; }
        set { _enable = value; }
    }
    private bool _visible = true;
    public bool Visible
    {
        get { return _visible; }
        set { _visible = value; }
    }
    #endregion
    #region Global Variable
    public StringBuilder strGallery = new StringBuilder();
    private StringBuilder strPhoto = new StringBuilder();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_enable)
            {
                if (_source != null && _source.Rows.Count > 0)
                {
                    CarouselBuilder();
                }
            }
        }
    }

    private void CarouselBuilder()
    {
        int i;
        bool errorCheck = false;

        #region Error Check
        if (!_source.Columns.Contains(_sourceFieldName) && _visible)
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + _sourceFieldName + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        if (!_source.Columns.Contains(_sourceFieldPhoto) && _visible)
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + _sourceFieldPhoto + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        if (!_source.Columns.Contains(_sourceFieldURL) && _visible)
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + _sourceFieldURL + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        #endregion

        if (!errorCheck && _visible)
        {
            for (i = 0; i < _source.Rows.Count; i++)
            {
                #region PhotoBuilder
                strPhoto.Append("<div class='item' style='overflow:hidden;width:"+_photoWidthOverflow+";height:"+_photoHeightOverflow+";'>");
                strPhoto.Append("<a href='");
                strPhoto.Append(_source.Rows[i][_sourceFieldURL].ToString());
                strPhoto.Append("' title='");
                strPhoto.Append(_source.Rows[i][_sourceFieldName].ToString());
                strPhoto.Append("'>");
                strPhoto.Append("<img src='");
                strPhoto.Append(_pathPhoto + _source.Rows[i][_sourceFieldPhoto].ToString());
                strPhoto.Append("' ");
                strPhoto.Append("style='width:" + _photoWidth + ";height:" + _photoHeight + ";' ");
                strPhoto.Append("alt='");
                strPhoto.Append(_source.Rows[i][_sourceFieldName].ToString());
                strPhoto.Append("'/>");
                strPhoto.Append("</a>");
                strPhoto.Append("</div>");
                #endregion
            }

            strGallery.Append("<div id='owl-demo' class='owl-carousel'>");
            strGallery.Append(strPhoto.ToString());
            strGallery.Append("</div>");
        }
    }
}