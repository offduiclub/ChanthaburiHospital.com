<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        RegisterRoutes(RouteTable.Routes);
    }

    void RegisterRoutes(RouteCollection routes)
    {
        #region Webboard Routing
        routes.MapPageRoute("Webboard",
            "Webboard",
            "~/Webboard/Default.aspx");

        #region WebboardType
        routes.MapPageRoute("WebboardTypeAdd",
            "WebboardManage/Type/NewType/",
            "~/Webboard/TypeManage.aspx");
        routes.MapPageRoute("WebboardTypeManage",
            "WebboardManage/Type/{id}/{command}/",
            "~/Webboard/TypeManage.aspx");
        #endregion
        #region WebboardGroup
        routes.MapPageRoute("WebboardGroupAdd",
            "WebboardManage/Group/{type}/NewGroup/",
            "~/Webboard/GroupManage.aspx");
        routes.MapPageRoute("WebboardGroupManage",
            "WebboardManage/Group/{type}/{id}/{command}/",
            "~/Webboard/GroupManage.aspx");
        #endregion
        #region WebboardQuestion
        routes.MapPageRoute("WebboardGroup",
            "Webboard/{id}/{name}/",
            "~/Webboard/Question.aspx");
        routes.MapPageRoute("WebboardGroupNoName",
            "Webboard/{id}/",
            "~/Webboard/Question.aspx");
        
        routes.MapPageRoute("WebboardQuestionNew",
            "WebboardManage/Question/{group}/NewQuestion/",
            "~/Webboard/QuestionManage.aspx");
        routes.MapPageRoute("WebboardQuestionManage",
            "WebboardManage/Question/{group}/{id}/{command}/",
            "~/Webboard/QuestionManage.aspx");
        #endregion
        #region WebboardAnswer
        routes.MapPageRoute("WebboardAnswer",
            "Webboard/{group}/{id}/{name}/",
            "~/Webboard/Answer.aspx");
        routes.MapPageRoute("WebboardAnswer2",
            "Webboard/{group}/{id}/",
            "~/Webboard/Answer.aspx");

        routes.MapPageRoute("WebboardAnswerNew",
            "WebboardManage/Answer/{group}/{question}/NewAnswer/",
            "~/Webboard/AnswerManage.aspx");
        routes.MapPageRoute("WebboardAnswerManage",
            "WebboardManage/Answer/{group}/{question}/{id}/{command}/",
            "~/Webboard/AnswerManage.aspx");
        #endregion
        #endregion
        
        //routes.MapPageRoute("0",
        //    "",
        //    "~/Intro.aspx");
        routes.MapPageRoute("1",
            "Home",
            "~/Default.aspx");
        routes.MapPageRoute("2",
            "Profile",
            "~/UserProfile.aspx");
        routes.MapPageRoute("3",
            "Register",
            "~/UserRegister.aspx");
        routes.MapPageRoute("4",
            "RegisterConfirm/{id}",
            "~/UserRegisterConfirm.aspx");
        //routes.MapPageRoute("5",
        //    "Event",
        //    "~/EventView.aspx");
        routes.MapPageRoute("5",
            "Event",
            "~/Event.aspx");
        //routes.MapPageRoute("6",
        //    "Event/{id}/{name}",
        //    "~/EventDetail.aspx");
        routes.MapPageRoute("6",
            "Event/{id}/{name}",
            "~/Event.aspx");
        routes.MapPageRoute("7", 
            "EventDetail", 
            "~/EventDetail.aspx");
        //routes.MapPageRoute("8",
        //    "News",
        //    "~/NewsView.aspx");
        routes.MapPageRoute("8",
            "News",
            "~/News.aspx");
        //routes.MapPageRoute("9",
        //    "News/{id}/{name}",
        //    "~/NewsDetail.aspx");
        routes.MapPageRoute("9",
            "News/{id}/{name}",
            "~/News.aspx");
        routes.MapPageRoute("10",
            "AboutHospital",
            "~/AboutHospital.aspx");
        routes.MapPageRoute("13",
            "Awards",
            "~/Awards.aspx");
        //routes.MapPageRoute("14",
        //    "Article",
        //    "~/ArticleView.aspx");
        routes.MapPageRoute("14",
            "Article",
            "~/Article.aspx");
        //routes.MapPageRoute("15",
        //    "Article/{id}/{name}",
        //    "~/ArticleDetail.aspx");
        routes.MapPageRoute("15",
            "Article/{id}/{name}",
            "~/Article.aspx");
        //routes.MapPageRoute("16",
        //    "Promotion",
        //    "~/PromotionView.aspx");
        //routes.MapPageRoute("17",
        //    "Promotion/{id}/{name}",
        //    "~/PromotionDetail.aspx");
        routes.MapPageRoute("16",
            "Promotion",
            "~/Promotion.aspx");
        routes.MapPageRoute("17",
            "Promotion/{id}/{name}",
            "~/Promotion.aspx");
        routes.MapPageRoute("18",
            "MedicalCenter/{id}/{name}",
            "~/MedicalCenter.aspx");
        routes.MapPageRoute("19",
            "Service/{id}/{name}",
            "~/Service.aspx");
        routes.MapPageRoute("20",
            "DoctorSchedule",
            "~/DoctorSchedule.aspx");
        routes.MapPageRoute("21",
            "DoctorSchedule/{doctorUID}/{departmentUID}/{medicalCenterUID}/",
            "~/DoctorScheduleViewer.aspx");
        routes.MapPageRoute("22",
            "DoctorSchedule/{deptid}/{deptname}/{specialty}/{type}/{name}",
            "~/DoctorSchedule.aspx");
        routes.MapPageRoute("35",
            "DoctorSchedule/{name}/{special}/{dept}/{sun}/{mon}/{tue}/{wed}/{thu}/{fri}/{sat}/",
            "~/DoctorSchedule.aspx");
        routes.MapPageRoute("23",
            "CheckupCondition/",
            "~/CheckupCondition.aspx");
        routes.MapPageRoute("24",
            "CheckupResult/",
            "~/CheckupResult.aspx");
        routes.MapPageRoute("25",
            "Feedback/",
            "~/Feedback.aspx");
        routes.MapPageRoute("26",
            "Inquiry/",
            "~/Inquiry.aspx");
        routes.MapPageRoute("27",
            "Jobs/",
            "~/Jobs.aspx");
        routes.MapPageRoute("28",
            "JobsDetail/{id}/{name}/",
            "~/JobsDetail.aspx");
        routes.MapPageRoute("29",
            "Jobs/{id}/{name}/",
            "~/JobsApply.aspx");
        routes.MapPageRoute("30",
            "AdvancedTechnologies/",
            "~/AdvancedTechnologies.aspx");
        routes.MapPageRoute("31",
            "Maps/",
            "~/Maps.aspx");
        routes.MapPageRoute("32",
            "Facilities/",
            "~/Facilities.aspx");
        routes.MapPageRoute("33",
            "HospitalNetwork/",
            "~/HospitalNetwork.aspx");
        routes.MapPageRoute("34",
            "Chivawattana/",
            "~/Chivawattana.aspx");
        routes.MapPageRoute("11",
            "Package",
            "~/Package.aspx");
        routes.MapPageRoute("12",
            "Package/{id}/{name}",
            "~/Package.aspx");
        routes.MapPageRoute("36",
            "HealthPackage",
            "~/HealthPackage.aspx");
        routes.MapPageRoute("37",
            "HealthPackage/{id}/{name}/",
            "~/HealthPackage.aspx");
        routes.MapPageRoute("38",
            "en/",
            "~/Default.aspx");
		routes.MapPageRoute("39",
            "th/",
            "~/Default.aspx");
		routes.MapPageRoute("40",
            "th/{id}/",
            "~/Default.aspx");
        routes.MapPageRoute("41",
            "VisionMission",
            "~/VisionMission.aspx");
        routes.MapPageRoute("42",
            "AboutChanthaburi",
            "~/AboutChanthaburi.aspx");
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
