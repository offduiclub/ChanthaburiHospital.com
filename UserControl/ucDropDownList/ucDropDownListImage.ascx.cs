using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucDropDownListImage : System.Web.UI.UserControl
{
    #region Example
    /*
    ucDropDownListImage.Item ddlItem = new ucDropDownListImage.Item();
    ddlItem.Value = "TH";
    ddlItem.Text = "ภาษาไทย";
    ddlItem.Descriptioin = "แสดงข้อมูลทั้งหมดเป็นภาษาไทย";
    ddlItem.Image = "/Images/Icon/icLangTH.png";
    ucDropDownListImage1.Items.Add(ddlItem);

    ddlItem = new ucDropDownListImage.Item();
    ddlItem.Value = "EN";
    ddlItem.Text = "ภาษาอังกฤษ";
    ddlItem.Descriptioin = "แสดงข้อมูลทั้งหมดเป็นอังกฤษ";
    ddlItem.Image = "/Images/Icon/icLangEN.png";
    ddlItem.Selected = false;
    ucDropDownListImage1.Items.Add(ddlItem);

    ddlItem = new ucDropDownListImage.Item();
    ddlItem.Value = "AR";
    ddlItem.Text = "ภาษาอารบิก";
    ddlItem.Descriptioin = "ยังไม่เปิดให้ใช้งาน";
    ddlItem.Image = "/Images/Icon/icLangEN.png";
    ddlItem.Enable = false;
    ucDropDownListImage1.Items.Add(ddlItem);

    ucDropDownListImage1.SelectedValue = "EN";
    */
    /*
    ucDropDownListImage1.Value();
    ucDropDownListImage1.Text();
    */
    #endregion
    #region Property
    private List<Item> _items = new List<Item>();
    public List<Item> Items
    {
        get { return _items; }
        set { _items = value; }
    }
    private string _selectedValue;
    public string SelectedValue
    {
        get 
        {
            if (ddlDefault.Items.Count > 0)
            {
                //_selectedValue= ddlDefault.SelectedItem.Value;
                _selectedValue = txtValue.Text;
            }
            return _selectedValue; 
        }
        set
        {
            if (ddlDefault.Items.Count > 0)
            {
                ddlDefault.SelectedValue = value;
                txtValue.Text = value;
            }
            _selectedValue = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_items.Count > 0)
            {
                ddlDefault.Enabled = true;
                BindList();
            }
            else
            {
                ddlDefault.Enabled = false;
            }
        }
    }

    private void BindList()
    {
        ListItem li = new ListItem();

        for (int i = 0; i < _items.Count; i++)
        {
            li = new ListItem();
            li.Value = _items[i].Value;
            li.Text = _items[i].Text;
            if (!string.IsNullOrEmpty(_items[i].Image))
            {
                li.Attributes.Add("data-imagesrc", _items[i].Image);
            }
            if (!string.IsNullOrEmpty(_items[i].Descriptioin))
            {
                li.Attributes.Add("data-description", _items[i].Descriptioin);
            }
            if (!_items[i].Enable)
            {
                li.Attributes.Add("disabled", "true");
            }
            if (_items[i].Selected)
            {
                li.Selected = true;
            }
            if (!string.IsNullOrEmpty(_selectedValue))
            {
                if (_items[i].Value == _selectedValue)
                {
                    li.Selected = true;
                }
            }

            ddlDefault.Items.Add(li);
        }
    }

    public string Value()
    {
        string rtnValue = "";

        if (ddlDefault.Items.Count > 0)
        {
            //rtnValue = ddlDefault.SelectedItem.Value;
            rtnValue = txtValue.Text;
        }

        return rtnValue;
    }

    public string Text()
    {
        string rtnValue = "";

        if (ddlDefault.Items.Count > 0)
        {
            //rtnValue = ddlDefault.SelectedItem.Text;
            rtnValue = txtText.Text;
        }

        return rtnValue;
    }

    public class Item
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private string _description="";
        public string Descriptioin
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _image="";
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
        private bool _enable=true;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }
        private bool _selected=false;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

    }
}