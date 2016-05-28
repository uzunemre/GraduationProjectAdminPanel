using AdminPanel.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AdminPanel
{
    public class MvcApplication : System.Web.HttpApplication
    {

        string connString = ConfigurationManager.ConnectionStrings["GraduationProjectContext"].ConnectionString;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            SqlDependency.Start(connString);
            
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}
