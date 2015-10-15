<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobsDetail.aspx.cs" Inherits="JobsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JobDetail</title>
    <link href="/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssCustom.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/cssControl.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding:10px;">
            <div style="color:#13608E;">
                <h3><img src="/Images/logo24.png" /> <asp:Label ID="lblName" runat="server" /></h3>
            </div>
            <div style="border-top:1px solid #DDD;padding-top:5px;margin-top:5px;">
                <asp:Label ID="lblDetail" runat="server" />
            </div>
            <div style="border-top:1px dashed #DDD;padding-top:10px;margin-top:10px;;text-align:right;">
                <a href='/Jobs/<%=clsDefault.URLRouting("id")%>/<%=clsDefault.URLRouting("name")%>/' target="_parent"><span class="Icon16 True Normal"></span> กรอกใบสมัคร</a>
            </div>
        </div>
    </form>
</body>
</html>
