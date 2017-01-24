using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "services/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ArticlesStoresControllerHttpRoute",
                routeTemplate: "services/Articles/Stores/{store_id}",
                defaults: new { controller = "ArticlesStores" });
            //config.Routes.MapHttpRoute("ArticlesByStoreApi", "services/{controller}/{controller}/{id}");
        }
    }
}
