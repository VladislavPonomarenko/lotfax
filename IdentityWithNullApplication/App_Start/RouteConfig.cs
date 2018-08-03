using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IdentityWithNullApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "FullVersionArticle",
            url: "Article/FullVersion/{slug}/{fullId}",
             defaults: new { controller = "Article", action = "FullVersionArticle", slug = UrlParameter.Optional },
             constraints: new { fullId = @"\d+" }
             );

            routes.MapRoute(null, "", new
            {
                controller = "Article",
                action = "ListArticles",
                category = (string)null,
                page = 1
            });

            routes.MapRoute(
               name: null,
               url: "Page{page}",
               defaults: new { controller = "Article", action = "ListArticles", category = (string)null },
               constraints: new { page = @"\d+" }
           );

            routes.MapRoute(null,
               "{category}",
               new { controller = "Article", action = "ListArticles", page = 1 }
           );

            routes.MapRoute(null,
                 "{category}/Page{page}",
                 new { controller = "Article", action = "ListArticles" },
                 new { page = @"\d+" }
             );

            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}
