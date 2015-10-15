using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ucMenuMega : System.Web.UI.UserControl
{
    #region Example
    /* Design
    <uc1:ucMenuMega ID="ucMenuMega1" runat="server" 
        HighlightColor="#F44B76"
        Enable="true" 
        Visible="true" 
        EnableHomeIcon="false" 
        IconHomeName="icHome.png" 
        CssClassName="sm-oofwhite"
    />
    */
    /* CodeBehind สร้างเมนู
    ucMenuMega.Item item = new ucMenuMega.Item();
            
    item.UID = 1;
    item.Name = "HOME";
    item.Detail = "หน้าแรกของเว็บไซต์";
    item.URL = "/";
    item.Current = true;
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 2;
    item.Name = "โรงพยาบาลกรุงเทพ";
    item.Detail = "รายชื่อโรงพยาบาลในเครือโรงพยาบาลกรุงเทพ";
    item.URL = "";
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 3;
    item.Name = "โรงพยาบาลกรุงเทพจันทบุรี";
    item.URL = "http://www.bangkokrayong.com";
    item.ParentUID = 2;
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 4;
    item.Name = "โรงพยาบาลกรุงเทพตราด";
    item.URL = "http://www.bangkoktrat.com";
    item.ParentUID = 2;
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 5;
    item.Name = "โรงพยาบาลกรุงเทพพัทยา";
    item.URL = "http://www.bangkokpattayahospital.com";
    item.ParentUID = 2;
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 6;
    item.Name = "โรงพยาบาลกรุงเทพจันทบุรี";
    item.URL = "http://www.bangkokchantaburi.com";
    item.ParentUID = 2;
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 7;
    item.Name = "คลินิกเวชกรรมศรีจันทบุรี";
    item.URL = "http://www.srh.co.th";
    item.ParentUID = 3;
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 8;
    item.Name = "คลินิกเวชกรรมศรีจันทบุรี 2";
    item.URL = "http://www.srh.co.th";
    item.ParentUID = 3;
    item.Active = false;
    ucMenuMega1.MenuMegaItems.Add(item);

    item = new ucMenuMega.Item();
    item.UID = 9;
    item.Name = "MEGA Menu";
    item.Detail = "รายละเอียด MegaMenu";
    item.URL = "";
    item.MegaData = "<div style='padding:10px;'>ทดสอบ MEGA MENU นะค้าบบบบ อิอิ<br/>ได้ป่าวนะ<img src='http://www.goodesign.in.th/Upload/Portfolio/portfolio_2.png'/></div>";
    ucMenuMega1.MenuMegaItems.Add(item);
    */
    #endregion
    #region Property
    private bool _enableHomeIcon=true;
    public bool EnableHomeIcon
    {
        get { return _enableHomeIcon; }
        set { _enableHomeIcon = value; }
    }

    private string _highlighColor = "#86DBE0";
    public string HighlightColor
    {
        get { return _highlighColor; }
        set { _highlighColor = value; }
    }
    private bool _enable=true;
    public bool Enable
    {
        get { return _enable; }
        set { _enable = value; }
    }
    private bool _visible=true;
    public bool Visible
    {
        get { return _visible; }
        set { _visible = value; }
    }
    private List<Item> _items=new List<Item>();
    public List<Item> Items
    {
        get { return _items; }
        set { _items = value; }
    }
    private string _cssClassName="sm-oofwhite";
    public string CssClassName
    {
        get { return _cssClassName; }
        set { _cssClassName = value; }
    }
    private string _iconHomeName = "icHome.png";
    public string IconHomeName
    {
        get { return _iconHomeName; }
        set { _iconHomeName = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_enable && _visible)
            {
                BindMenu();
            }
        }
    }

    private void BindMenu()
    {
        StringBuilder strMenu = new StringBuilder();

        if (_items.Count > 0)
        {
            strMenu.Append("<ul id='main-menu' class='sm sm-default'>");

            if (_enableHomeIcon)
            {
                strMenu.Append("<li id='sm-home'>");
                strMenu.Append("<a href='/' title='Home'>");
                strMenu.Append("<img src='" + ResolveClientUrl("Images/" + _iconHomeName) + "'/>");
                strMenu.Append("</a>");
                strMenu.Append("</li>");
            }

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ParentUID != 0) continue;//ถ้าเป็น SubMenu ให้ข้ามไป

                strMenu.Append("<li>");
                strMenu.Append("<a href='" + _items[i].URL + "' title='" + _items[i].Detail + "'" + (_items[i].Current ? " class='actived'" : "") + (_items[i].Active == false ? " class='disabled'" : "") + ">");
                strMenu.Append(_items[i].Name);
                strMenu.Append("</a>");

                if (!string.IsNullOrEmpty(_items[i].MegaData))
                {
                    strMenu.Append("<ul class='mega-menu'>");
                    strMenu.Append("<li>");
                    strMenu.Append(_items[i].MegaData);
                    strMenu.Append("</li>");
                    strMenu.Append("</ul>");
                }
                else
                {
                    strMenu.Append(BindSubMenu(_items[i].UID));
                }
                strMenu.Append("</li>");
            }
            strMenu.Append("</ul>");
            lblMenuMega.Text = strMenu.ToString();
        }
        else
        {
            lblMenuMega.Text = "ไม่พบข้อมูล";
        }
    }

    private string BindSubMenu(int uid)
    {
        StringBuilder strSubMenu = new StringBuilder();
        bool founded = false;

        if (_items.Count > 0)
        {
            strSubMenu.Append("<ul>");
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ParentUID == uid)
                {
                    founded = true;
                    strSubMenu.Append("<li>");
                    strSubMenu.Append("<a href='" + _items[i].URL + "' title='" + _items[i].Detail + "'" + (_items[i].Current ? " class='actived'" : "") + (_items[i].Active == false ? " class='disabled'" : "") + ">");
                    strSubMenu.Append(_items[i].Name);
                    strSubMenu.Append("</a>");

                    strSubMenu.Append(BindSubMenu(_items[i].UID));

                    strSubMenu.Append("</li>");
                }
            }
            strSubMenu.Append("</ul>");

            if (!founded)
            {
                strSubMenu.Length = 0; strSubMenu.Capacity = 0;
            }
        }

        return strSubMenu.ToString();
    }

    public class Item
    {
        private int _uid;
        public int UID
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _detail = "";
        public string Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
        private string _megaData = "";
        public string MegaData
        {
            get { return _megaData; }
            set { _megaData = value; }
        }
        private string _url = "";
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }
        private bool _current = false;
        public bool Current
        {
            get { return _current; }
            set { _current = value; }
        }
        private bool _active = true;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        private int _parentUID;
        public int ParentUID
        {
            get { return _parentUID; }
            set { _parentUID = value; }
        }
    }
}