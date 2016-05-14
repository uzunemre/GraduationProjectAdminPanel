using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AdminPanel
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
