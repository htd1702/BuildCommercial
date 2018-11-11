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
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
               name: "Blog",
               url: "tin-tuc",
               defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "Web.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Client", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );
        }
    }
}