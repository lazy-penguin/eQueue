using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Server
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Pages",
                url: "{action}/{id}",
                defaults: new { controller = "Pages", action = "Main", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "Main"
            );
        }
    }
}
