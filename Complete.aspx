<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Complete.aspx.cs" Inherits="Complete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=msg%></title>
    <link href="/CSS/cssDefault.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">
        var count = <%=time%>
        var redirect = "<%=url%>"

        function countDown() {
            if (count <= 0) {
                window.location = redirect;
            } else {
                count--;
                document.getElementById("timer").innerHTML = "กรุณารอ " + count + " วินาที"
                setTimeout("countDown()", 1000)
            }
        }  
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <!--<meta http-equiv="REFRESH" content="3;url=<%=url%>"/>-->
        <div style="position:absolute;top: 50%;left: 50%;margin-top:-75px;margin-left:-250px;">
            <div style="width:500px; text-align:center; vertical-align:middle; border:1px solid #dddddd; background:#ffffff;">
                <div style="background:#AFAFAF;padding:10px;color:#ffffff;margin:5px;">
                    <h2><%=msg%></h2>
                </div>
                <div style="padding:10px;">
                    <img src="/Images/Animated/anLoadingIcon.gif" style="padding:5px 0px 10px 0px;" /><br />
                    <span id="timer" style="font-weight:bold;">
                        <script type="text/javascript">
                            countDown();
                        </script>
                    </span>
                    <br />
                    ระบบกำลังดำเนินการพาคุณไปหน้าเว็บต่อไป
                    <br />
                    หรือ <a href="<%=url%>">คลิกที่นี่</a> เพื่อไปยังหน้าต่อไป
                    <br />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
