using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Client", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "ClientController" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Client", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ClientController" }
            );
        }
    }
}