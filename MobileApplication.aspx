<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileApplication.aspx.cs" Inherits="MobileApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CSS/cssDefault.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body style="background-color:#FFF;">
    <form id="form1" runat="server">
        <div style="text-align:center;">
            <h3>สามารถโหลด Application<br />ของโรงพยาบาลได้ดังนี้</h3>
            <table style="width:100%;padding-top:20px;">
                <tr>
                    <td style="width:50%;">
                        <a href="https://play.google.com/store/apps/details?id=com.pheonec.bangkokhospital"><img src="/Images/icPlayStore.png" /></a>
                        <h2>Android</h2>
                    </td>
                    <td style="width:50%;">
                        <a href="https://itunes.apple.com/us/app/b+-health/id977580562?mt=8"><img src="/Images/icAppStore.png" /></a>
                        <h2>IOS</h2>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
