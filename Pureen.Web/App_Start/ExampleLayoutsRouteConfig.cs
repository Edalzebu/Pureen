using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapMvcSample.Controllers;
using NavigationRoutes;
using Pureen.Web.Controllers;

namespace BootstrapMvcSample
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           // routes.MapNavigationRoute<HomeController>("Äccount", c => c.Index());
            
            /*routes.MapNavigationRoute<ExampleLayoutsController>("Account", c => c.Starter())

                  .AddChildRoute<AccountController>("Logout", c => c.Logout())
                 ;
             * */
        }
    }
}
