using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ViewSparkProject
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute("SharePres", "Presentation/Share/{members}/{div}", new { controller = "Presentation", action = "Share", div = UrlParameter.Optional });

            routes.MapRoute(
                "imageResize",
                "Image/Resize/{id}/{width}/{height}",
                new { controller = "Image", action = "Resize", width = 320, height = 200 }
                );

            routes.MapRoute(
                "Divid", // Route name
                "sub/{divid}/{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", divid = "", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}