using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Data;

/// <summary>
/// Summary description for clsMail
/// </summary>
public class clsMail
{
    #region Example
    /*
    clsMail clsMail = new clsMail();
        string outMessage;
        clsMail.SmtpMailHost = "DC-EXCHC.BDMS.CO.TH";
        clsMail.Send(
            "AutoSystem@glsict.com", 
            "nithi.re@glsict.com", 
            "Test", 
            "Test", 
            out outMessage, 
            Priority: System.Net.Mail.MailPriority.High, 
            Authentication: false);
    */
    #endregion
    #region Configuration
    private string _smtpMailHost = System.Configuration.ConfigurationManager.AppSettings["smtpMailHost"];
    public string SmtpMailHost
    {
        get { return _smtpMailHost; }
        set { _smtpMailHost = value; }
    }
    private string _smtpMailUsername = System.Configuration.ConfigurationManager.AppSettings["smtpMailUsername"];
    public string SmtpMailUsername
    {
        get { return _smtpMailUsername; }
        set { _smtpMailUsername = SmtpMailUsername; }
    }
    private string _smtpMailPassword = System.Configuration.ConfigurationManager.AppSettings["smtpMailPassword"];
    public string SmtpMailPassword
    {
        get { return _smtpMailPassword; }
        set { _smtpMailPassword = SmtpMailPassword; }
    }
    private bool _smtpMailAuthentication = true;
    public bool SmtpMailAuthentication
    {
        get { return _smtpMailAuthentication; }
        set { _smtpMailAuthentication = value; }
    }
    #endregion
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    public clsMail()
    {
    }
    public clsMail(string smtpMailHost, string smtpMailUsername, string smtpMailPassword, bool smtpMailAuthentication)
    {
        _smtpMailHost = smtpMailHost;
        _smtpMailUsername = smtpMailUsername;
        _smtpMailPassword = smtpMailPassword;
        _smtpMailAuthentication = smtpMailAuthentication;
    }

    public bool Send(string From, string To, string Subject, string Message, out string outMessage, string FromAliasName = "", string Cc = "", string Bcc = "",string Signature="",MailPriority Priority=MailPriority.Normal,bool Authentication=true)
    {
        #region Variable
        MailMessage myMail = new MailMessage();
        StringBuilder strMailBody = new StringBuilder();
        outMessage = "";
        #endregion
        #region DataChecker
        if (string.IsNullOrEmpty(To))
        {
            outMessage = "โปรดระบุเมล์ที่จะส่งหา";
            return false;
        }
        if (string.IsNullOrEmpty(From))
        {
            outMessage = "โปรดระบุเมล์ที่จะใช้ส่ง";
            return false;
        }
        #endregion

        #region Mail Builder
        #region Address
        myMail.From = new MailAddress(From, FromAliasName);
        myMail.To.Add(To);
        if (!string.IsNullOrEmpty(Cc))
        {
            myMail.CC.Add(Cc);
        }
        if (!string.IsNullOrEmpty(Bcc))
        {
            myMail.Bcc.Add(Bcc);
        }
        #endregion
        #region Detail
        #region Subject
        if (!string.IsNullOrEmpty(Subject))
        {
            myMail.Subject = Subject;
        }
        else
        {
            myMail.Subject = "";
        }
        #endregion
        #region Body
        strMailBody.Append("<html>");
        strMailBody.Append("<head></head>");
        strMailBody.Append("<body>");
        if (!string.IsNullOrEmpty(Message))
        {
            strMailBody.Append(Message);
        }
        if (!string.IsNullOrEmpty(Signature))
        {
            strMailBody.Append("<hr/>");
            strMailBody.Append(Signature);
        }
        strMailBody.Append("</body>");
        strMailBody.Append("</html>");

        myMail.Body = strMailBody.ToString();
        #endregion
        myMail.Priority = Priority;
        myMail.IsBodyHtml = true;
        myMail.BodyEncoding = System.Text.Encoding.GetEncoding("windows-874");
        #endregion
        #endregion
        #region Server Config
        SmtpClient smtpMail = new SmtpClient();
        if (Authentication)
        {
            NetworkCredential Auth = new NetworkCredential(_smtpMailUsername, _smtpMailPassword);
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = Auth;
        }
        else
        {
            smtpMail.UseDefaultCredentials = true;
        }
        smtpMail.Host = _smtpMailHost;
        #endregion
        #region Send Mail
        try
        {
            smtpMail.Send(myMail);
            return true;
        }
        catch (Exception ex)
        {
            outMessage = ex.Message;
            return false;
        }
        #endregion
    }

    /// <summary>
    /// ส่งเมล์จาก EmailTemplate โดยระบุค่าตัวแปรได้
    /// </summary>
    /// <param name="TemplateName">ชื่อ Template</param>
    /// <param name="From">เมล์ต้นทาง</param>
    /// <param name="To">เมล์ปลายทาง</param>
    /// <param name="Parameter">ค่าที่ใช้แทนในเมล์ เช่น [Username],nithi.re</param>
    /// <param name="outMessage">ข้อความแจ้งเตือนเมื่อเกิดข้อผิดพลาด</param>
    /// <param name="FromAliasName">ชื่อเมล์ต้นทาง</param>
    /// <param name="Cc">เมล์แนบ</param>
    /// <param name="Bcc">เมล์ไม่เปิดเผย</param>
    /// <param name="Signature">ข้อความท้ายเมล์</param>
    /// <returns>true=ส่งผ่าน , false=ส่งไม่ผ่าน</returns>
    /// <example>
    /// clsMail clsMail=new clsMail();
    /// string outMessage;
    /// clsMail.SendTemplate(
    ///     "UserRegisterConfirm",
    ///     "off_dui@hotmail.com",
    ///     "nithi.re@glsict.com",
    ///     new string[,] { { "[Username]", "ยูสเซอร์เนม" }, { "[UIDEncrypt]", "ยูไอดีเอ็นคริบ" } },
    ///     out outMessage);
    /// </example>
    public bool SendTemplate(string TemplateName, string From, string To, string[,] Parameter, out string outMessage, string FromAliasName = "", string Cc = "", string Bcc = "", string Signature = "")
    {
        #region Variable
        bool rtnValue = false;
        outMessage = "";

        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        clsSQL clsSQL = new clsSQL();
        clsLanguage clsLanguage = new clsLanguage();
        clsData clsData=new clsData();
        clsDefault clsDefault = new clsDefault();
        #endregion

        #region Found Language
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("EmailTemplate.UID,Language.Name LanguageName,EmailTemplate.Subject,EmailTemplate.Message ");
        strSQL.Append("FROM ");
        strSQL.Append("EmailTemplate ");
        strSQL.Append("INNER JOIN Language ON EmailTemplate.LanguageUID=Language.UID ");
        strSQL.Append("AND Language.Active='1' ");
        //strSQL.Append("AND Language.Name='" + clsLanguage.LanguageCurrent + "' ");
        strSQL.Append("WHERE ");
        strSQL.Append("EmailTemplate.Active='1' ");
        strSQL.Append("AND EmailTemplate.Name='" + TemplateName + "' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Language.Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Find LanguageRow
            bool languageMatch = false;
            int i;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LanguageName"].ToString() == clsLanguage.LanguageCurrent)
                {
                    languageMatch = true;
                    break;
                }
            }
            if (!languageMatch) i = 0;
            #endregion

            #region Parameter Replace
            string subject = dt.Rows[i]["Subject"].ToString();
            string message = dt.Rows[i]["Message"].ToString();

            subject = clsData.Replacer(subject, Parameter);
            message = clsData.Replacer(message, Parameter);

            if (Send(From, To, subject, message, out outMessage, FromAliasName, Cc, Bcc, Signature))
            {
                SendTemplateLog(dt.Rows[i]["UID"].ToString(), From, To, Cc, Bcc, "Complete");
                rtnValue = true;
            }
            else
            {
                SendTemplateLog(dt.Rows[i]["UID"].ToString(), From, To, Cc, Bcc, clsDefault.Left(outMessage,90));
                rtnValue = false;
            }
            #endregion
        }
        else
        {
            outMessage = "ไม่พบข้อมูล EmailTemplate";
            return false;
        }

        return rtnValue;
    }

    /// <summary>
    /// ส่งเมล์ด้วย Template และใช้ MailFrom จาก EmailList ที่ตั้งค่าไว้ในฐานข้อมูล
    /// </summary>
    /// <param name="TemplateName">ชื่อ Template</param>
    /// <param name="EmailListsFrom">เมล์ต้นทางจาก EmailList เช่น clsMail.GetEmailList("GlobalFrom")</param>
    /// <param name="To">เมล์ปลายทาง</param>
    /// <param name="Parameter">ค่าที่ใช้แทนในเมล์ เช่น new string[,]{{"[Username]","Oofdui"},{"[UIDEncrypt]","xxx"}}</param>
    /// <param name="outMessage">ข้อความแจ้งเตือนเมื่อเกิดข้อผิดพลาด</param>
    /// <param name="FromAliasName">ชื่อเมล์ต้นทาง ใช้ค่านี้ก็ต่อเมื่อค่าใน EmailList ไม่มี</param>
    /// <param name="Cc">เมล์แนบ</param>
    /// <param name="Bcc">เมล์ไม่เปิดเผย</param>
    /// <param name="Signature">ข้อความท้ายเมล์</param>
    /// <returns>ส่งผ่าน , false=ส่งไม่ผ่าน</returns>
    /// <example>
    /// clsMail clsMail = new clsMail();
    /// string outMessage;
    /// 
    /// clsMail.SendTemplate(
    ///     "UserRegisterConfirm",
    ///     clsMail.GetEmailList("GlobalFrom"),
    ///     "off_dui@hotmail.com",
    ///     new string[,]{{"[Username]","Oofdui"},{"[UIDEncrypt]","xxx"}},
    ///     out outMessage);
    /// </example>
    public bool SendTemplate(string TemplateName, List<EmailList> EmailListsFrom, string To, string[,] Parameter, out string outMessage, string FromAliasName = "", string Cc = "", string Bcc = "", string Signature = "")
    {
        #region Variable
        bool rtnValue = true;
        outMessage = "";
        #endregion
        #region Data Checker
        if (EmailListsFrom.Count == 0)
        {
            outMessage = "ไม่พบข้อมูล EmailList";
            return false;
        }
        #endregion
        #region Send
        for (int i = 0; i < EmailListsFrom.Count; i++)
        {
            if(!SendTemplate(
                TemplateName,
                EmailListsFrom[i].EmailAddress,
                To,
                Parameter,
                out outMessage,
                (EmailListsFrom[i].EmailAliasName != "" ? EmailListsFrom[i].EmailAliasName : FromAliasName),
                Cc, 
                Bcc, 
                Signature
                ))
            {
                rtnValue = false;
            }
        }
        #endregion
        return rtnValue;

        #region Backup
        /* ####### Original FullCode #######
                #region Variable
        bool rtnValue = false;
        outMessage = "";

        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        clsSQL clsSQL = new clsSQL();
        clsLanguage clsLanguage = new clsLanguage();
        clsData clsData = new clsData();
        clsDefault clsDefault = new clsDefault();
        #endregion
        #region Find EmailList
        if (EmailListsFrom.Count == 0)
        {
            outMessage = "ไม่พบข้อมูล EmailList"; return false;
        }
        #endregion
        #region Found Language
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("EmailTemplate.UID,Language.Name LanguageName,EmailTemplate.Subject,EmailTemplate.Message ");
        strSQL.Append("FROM ");
        strSQL.Append("EmailTemplate ");
        strSQL.Append("INNER JOIN Language ON EmailTemplate.LanguageUID=Language.UID ");
        strSQL.Append("AND Language.Active='1' ");
        //strSQL.Append("AND Language.Name='" + clsLanguage.LanguageCurrent + "' ");
        strSQL.Append("WHERE ");
        strSQL.Append("EmailTemplate.Active='1' ");
        strSQL.Append("AND EmailTemplate.Name='" + TemplateName + "' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Language.Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Find LanguageRow
            bool languageMatch = false;
            int i;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LanguageName"].ToString() == clsLanguage.LanguageCurrent)
                {
                    languageMatch = true;
                    break;
                }
            }
            if (!languageMatch) i = 0;
            #endregion

            #region Parameter Replace
            string subject = dt.Rows[i]["Subject"].ToString();
            string message = dt.Rows[i]["Message"].ToString();

            subject = clsData.Replacer(subject, Parameter);
            message = clsData.Replacer(message, Parameter);

            for (int f = 0; f < EmailListsFrom.Count; f++)
            {
                if (Send(
                        EmailListsFrom[f].EmailAddress,
                        To, subject, message, out outMessage,
                        (EmailListsFrom[f].EmailAliasName != "" ? EmailListsFrom[f].EmailAddress : FromAliasName),
                        Cc, Bcc, Signature)
                    )
                {
                    SendTemplateLog(dt.Rows[i]["UID"].ToString(), EmailListsFrom[f].EmailAddress, To, Cc, Bcc, "Complete");
                    rtnValue = true;
                }
                else
                {
                    SendTemplateLog(dt.Rows[i]["UID"].ToString(), EmailListsFrom[f].EmailAddress, To, Cc, Bcc, clsDefault.Left(outMessage, 90));
                    rtnValue = false;
                }
            }
            #endregion
        }
        else
        {
            outMessage = "ไม่พบข้อมูล EmailTemplate";
            return false;
        }

        return rtnValue;
        */
        #endregion
    }

    /// <summary>
    /// ส่งเมล์ด้วย Template และใช้ MailFrom MailTo จาก EmailList ที่ตั้งค่าไว้ในฐานข้อมูล
    /// </summary>
    /// <param name="TemplateName">ชื่อ Template</param>
    /// <param name="EmailListsFrom">เมล์ต้นทางจาก EmailList เช่น clsMail.GetEmailList("GlobalFrom")</param>
    /// <param name="EmailListsTo">เมล์ปลายทางจาก EmailList เช่น clsMail.GetEmailList("DoctorScheduleTo")</param>
    /// <param name="Parameter">ค่าที่ใช้แทนในเมล์ เช่น new string[,]{{"[Username]","Oofdui"},{"[UIDEncrypt]","xxx"}}</param>
    /// <param name="outMessage">ข้อความแจ้งเตือนเมื่อเกิดข้อผิดพลาด</param>
    /// <param name="FromAliasName">ชื่อเมล์ต้นทาง ใช้ค่านี้ก็ต่อเมื่อค่าใน EmailList ไม่มี</param>
    /// <param name="Cc">เมล์แนบ</param>
    /// <param name="Bcc">เมล์ไม่เปิดเผย</param>
    /// <param name="Signature">ข้อความท้ายเมล์</param>
    /// <returns>ส่งผ่าน , false=ส่งไม่ผ่าน</returns>
    /// <example>
    /// clsMail clsMail = new clsMail();
    /// string outMessage;
    /// 
    /// clsMail.SendTemplate(
    ///     "UserRegisterConfirm",
    ///     clsMail.GetEmailList("GlobalFrom"),
    ///     clsMail.GetEmailList("DoctorScheduleTo"),
    ///     new string[,]{{"[Username]","Oofdui"},{"[UIDEncrypt]","xxx"}},
    ///     out outMessage);
    /// </example>
    public bool SendTemplate(string TemplateName, List<EmailList> EmailListsFrom, List<EmailList> EmailListsTo, string[,] Parameter, out string outMessage, string FromAliasName = "", string Cc = "", string Bcc = "", string Signature = "")
    {
        #region Variable
        bool rtnValue = true;
        outMessage = "";
        #endregion
        #region Data Checker
        if (EmailListsFrom.Count == 0 || EmailListsTo.Count==0)
        {
            outMessage = "ไม่พบข้อมูล EmailList";
            return false;
        }
        #endregion
        #region Send
        for (int i = 0; i < EmailListsFrom.Count; i++)
        {
            for (int j = 0; j < EmailListsTo.Count; j++)
            {
                if (!SendTemplate(
                    TemplateName,
                    EmailListsFrom[i].EmailAddress,
                    EmailListsTo[j].EmailAddress,
                    Parameter,
                    out outMessage,
                    (EmailListsFrom[i].EmailAliasName != "" ? EmailListsFrom[i].EmailAliasName : FromAliasName),
                    Cc,
                    Bcc,
                    Signature
                    ))
                {
                    rtnValue = false;
                }
            }
        }
        #endregion
        return rtnValue;

        #region Backup
        /*#### Original FullCode ####
        #region Variable
        bool rtnValue = false;
        outMessage = "";

        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        clsSQL clsSQL = new clsSQL();
        clsLanguage clsLanguage = new clsLanguage();
        clsData clsData = new clsData();
        clsDefault clsDefault = new clsDefault();
        #endregion
        #region Find EmailList
        if (EmailListsFrom.Count == 0 || EmailListsTo.Count==0)
        {
            outMessage = "ไม่พบข้อมูล EmailList"; return false;
        }
        #endregion
        #region Found Language
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("EmailTemplate.UID,Language.Name LanguageName,EmailTemplate.Subject,EmailTemplate.Message ");
        strSQL.Append("FROM ");
        strSQL.Append("EmailTemplate ");
        strSQL.Append("INNER JOIN Language ON EmailTemplate.LanguageUID=Language.UID ");
        strSQL.Append("AND Language.Active='1' ");
        //strSQL.Append("AND Language.Name='" + clsLanguage.LanguageCurrent + "' ");
        strSQL.Append("WHERE ");
        strSQL.Append("EmailTemplate.Active='1' ");
        strSQL.Append("AND EmailTemplate.Name='" + TemplateName + "' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Language.Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;
        #endregion

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Find LanguageRow
            bool languageMatch = false;
            int i;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LanguageName"].ToString() == clsLanguage.LanguageCurrent)
                {
                    languageMatch = true;
                    break;
                }
            }
            if (!languageMatch) i = 0;
            #endregion

            #region Parameter Replace
            string subject = dt.Rows[i]["Subject"].ToString();
            string message = dt.Rows[i]["Message"].ToString();

            subject = clsData.Replacer(subject, Parameter);
            message = clsData.Replacer(message, Parameter);

            for (int f = 0; f < EmailListsFrom.Count; f++)
            {
                for (int t = 0; t < EmailListsTo.Count; t++)
                {
                    if (Send(
                        EmailListsFrom[f].EmailAddress,
                        EmailListsTo[t].EmailAddress, 
                        subject, message, out outMessage,
                        (EmailListsFrom[f].EmailAliasName != "" ? EmailListsFrom[f].EmailAddress : FromAliasName),
                        Cc, Bcc, Signature)
                    )
                    {
                        SendTemplateLog(dt.Rows[i]["UID"].ToString(), EmailListsFrom[f].EmailAddress, EmailListsTo[t].EmailAddress, Cc, Bcc, "Complete");
                        rtnValue = true;
                    }
                    else
                    {
                        SendTemplateLog(dt.Rows[i]["UID"].ToString(), EmailListsFrom[f].EmailAddress, EmailListsTo[t].EmailAddress, Cc, Bcc, clsDefault.Left(outMessage, 90));
                        rtnValue = false;
                    }
                }
            }
            #endregion
        }
        else
        {
            outMessage = "ไม่พบข้อมูล EmailTemplate";
            return false;
        }

        return rtnValue;
        */
        #endregion
    }

    /// <summary>
    /// บันทึก Log การส่งอีเมล์จากระบบ Template
    /// </summary>
    /// <param name="UID">EmailTemplateUID</param>
    /// <param name="From">จากเมล์</param>
    /// <param name="To">ถึงเมล์</param>
    /// <param name="Cc">แนบเมล์</param>
    /// <param name="Bcc">ไม่เปิดเผยเมล์</param>
    /// <param name="Result">ผลการส่ง</param>
    /// <returns>true=บันทึกสำเร็จ , false=บันทึกไม่สำเร็จ</returns>
    private bool SendTemplateLog(string UID, string From, string To, string Cc, string Bcc, string Result)
    {
        #region Variable
        bool rtnValue = false;
        clsSQL clsSQL = new clsSQL();
        clsSecurity clsSecurity=new clsSecurity();
        clsNet clsNet = new clsNet();
        string outSQL;
        #endregion

        #region SQL Query
        if(clsSQL.Insert(
            "EmailTemplateLog",
            new string[,]{
                {"UID",clsSQL.GetNewID("UID","EmailTemplateLog","",dbType,cs).ToString()},
                {"EmailTemplateUID",UID},
                {"EmailFrom","'"+clsSQL.CodeFilter(From)+"'"},
                {"EmailTo","'"+clsSQL.CodeFilter(To)+"'"},
                {"EmailCc","'"+clsSQL.CodeFilter(Cc)+"'"},
                {"EmailBcc","'"+clsSQL.CodeFilter(Bcc)+"'"},
                {"Result","'"+clsSQL.CodeFilter(Result)+"'"},
                {"CWhen","GETDATE()"},
                {"CUser","0"},
                {"CIP","'"+clsNet.IPGet()+"'"},
                {"CHostname","'"+clsNet.ComNameGet()+"'"}},
            new string[,]{{}},
            dbType,
            cs,
            out outSQL))
        {
            rtnValue = true;
        }
        #endregion

        return rtnValue;
    }

    /// <summary>
    /// ดึงรายชื่ออีเมล์ในฐานข้อมูล EmailList โดยคืนค่าเป็น List<EmailList>
    /// </summary>
    /// <param name="EmailListName">ชื่ออ้างอิงข้อมูลเมล์ในฐานข้อมูล</param>
    /// <returns>List ของคลาส EmailList</returns>
    /// <example>
    /// clsMail clsMail = new clsMail();
    /// List<clsMail.EmailList> emailLists=new List<clsMail.EmailList>();
    /// emailLists = clsMail.GetEmailList("DoctorScheduleTo");
    /// </example>
    public List<EmailList> GetEmailList(string EmailListName)
    {
        #region Variable
        clsSQL clsSQL = new clsSQL();
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        List<EmailList> emailLists = new List<EmailList>();
        #endregion
        #region DataBuilder
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("EMail,");
        strSQL.Append("EMailAliasName ");
        strSQL.Append("FROM ");
        strSQL.Append("EmailList ");
        strSQL.Append("WHERE ");
        strSQL.Append("Active='1' ");
        strSQL.Append("AND Name="+parameterChar+"Name");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(),new string[,]{{parameterChar+"Name",EmailListName}}, dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmailList emailList = new EmailList();
                emailList.EmailAddress = dt.Rows[i]["EMail"].ToString();
                emailList.EmailAliasName = (dt.Rows[i]["EMailAliasName"] != DBNull.Value ? dt.Rows[i]["EMailAliasName"].ToString() : "");

                emailLists.Add(emailList);
            }
        }
        #endregion

        return emailLists;
    }

    /// <summary>
    /// คลาสที่ใช้คืนค่าอีเมล์จากฐานข้อมูล EmailList
    /// </summary>
    public class EmailList
    {
        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }
        private string _emailAliasName;
        public string EmailAliasName
        {
            get { return _emailAliasName; }
            set { _emailAliasName = value; }
        }
    }
}