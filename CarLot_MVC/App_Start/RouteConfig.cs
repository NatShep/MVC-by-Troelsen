﻿using System.Web.Mvc;
using System.Web.Routing;

namespace CarLot_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Inventory", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}