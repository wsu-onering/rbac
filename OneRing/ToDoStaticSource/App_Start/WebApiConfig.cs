using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ToDoStaticSource
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Remove all but the JSON media type content formatter
			config.Formatters.Clear();
			config.Formatters.Add(new JsonMediaTypeFormatter());
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
