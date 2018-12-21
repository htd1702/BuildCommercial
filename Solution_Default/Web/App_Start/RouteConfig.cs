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
                name: "OrderSuccess",
                url: "thank-you/{id}",
                defaults: new { controller = "Cart", action = "OrderSuccess", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
                name: "ProductByCategory",
                url: "product-category/{id}",
                defaults: new { controller = "Product", action = "ProductByCategory", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
           );

            routes.MapRoute(
                name: "ViewPostCategory",
                url: "category-news",
                defaults: new { controller = "PostCategory", action = "_viewPostCategory", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
                name: "ViewPostDetail",
                url: "post-detail/{id}",
                defaults: new { controller = "Post", action = "_viewPostDetails", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
                name: "GetPostByPostCategory",
                url: "post-category/{id}",
                defaults: new { controller = "Post", action = "GetPostByPostCategory", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
           );

            routes.MapRoute(
                name: "ViewCart",
                url: "view-cart",
                defaults: new { controller = "Cart", action = "ViewCart", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
           );

            routes.MapRoute(
              name: "CheckOut",
              url: "check-out",
              defaults: new { controller = "Cart", action = "CheckOut", id = UrlParameter.Optional },
              namespaces: new string[] { "Web.Controllers" }
           );

            routes.MapRoute(
               name: "ProductDetails",
               url: "product-details/{id}",
               defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional },
               namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
                name: "SaleProduct",
                url: "sale-product",
                defaults: new { controller = "Product", action = "SaleProduct", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
             );

            routes.MapRoute(
                name: "NewProduct",
                url: "new-product",
                defaults: new { controller = "Product", action = "NewProduct", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "contact",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Controllers" }
            );

            routes.MapRoute(
               name: "Blog",
               url: "news",
               defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional },
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