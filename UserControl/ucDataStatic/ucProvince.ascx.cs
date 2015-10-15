using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ucProvince : System.Web.UI.UserControl
{
    private string _Name;
    public string Name
    {
        get
        {
            return ddlProvince.SelectedItem.Value;
        }
        set
        {
            _Name = value;
            try
            {
                ddlProvince.SelectedValue = value;
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        base.OnInit(e);

        string[] arrProvince = 
            { 
                "กรุงเทพมหานคร","กระบี่","กาญจนบุรี","กาฬสินธุ์","กำแพงเพชร","ขอนแก่น","จันทบุรี","ฉะเชิงเทรา","ชลบุรี","ชัยนาท","ชัยภูมิ","ชุมพร","ตรัง",
                "ตราด","ตาก","นครนายก","นครปฐม","นครพนม","นครราชสีมา","นครศรีธรรมราช","นครสวรรค์","นนทบุรี","นราธิวาส","น่าน","บึงกาฬ","บุรีรัมย์",
                "ปทุมธานี","ประจวบคีรีขันธ์","ปราจีนบุรี","ปัตตานี","พระนครศรีอยุธยา","พะเยา","พังงา","พัทลุง","พิจิตร","พิษณุโลก","ภูเก็ต","มหาสารคาม","มุกดาหาร",
                "ยะลา","ยโสธร","ร้อยเอ็ด","ระนอง","จันทบุรี","ราชบุรี","ลพบุรี","ลำปาง","ลำพูน","ศรีสะเกษ","สกลนคร","สงขลา","สตูล","สมุทรปราการ","สมุทรสงคราม",
                "สมุทรสาคร","สระบุรี","สระแก้ว","สิงห์บุรี","สุพรรณบุรี","สุราษฎร์ธานี","สุรินทร์","สุโขทัย","หนองคาย","หนองบัวลำภู","อ่างทอง","อำนาจเจริญ","อุดรธานี",
                "อุตรดิตถ์","อุทัยธานี","อุบลราชธานี","เชียงราย","เชียงใหม่","เพชรบุรี","เพชรบูรณ์","เลย","แพร่","แม่ฮ่องสอน"
            };
        for (int i = 0; i < arrProvince.Length; i++)
        {
            ddlProvince.Items.Add(arrProvince[i]);
        }
        ddlProvince.Items.Insert(0, new ListItem("-", "null"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}